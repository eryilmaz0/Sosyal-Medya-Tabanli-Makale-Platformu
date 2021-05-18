using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Business.Abstract;
using Project.Business.Concrete;
using Project.DataAccess.Abstract;
using Project.DataAccess.EntityFramework;
using Project.DataAccess.EntityFrameworkWithSoftDelete;
using Project.DataAccess.UnitOfWork;
using Project.Entities.DbContext;
using Project.Entities.Entities;
using Project.WebAPI.Dtos;
using Project.WebAPI.IdentityCustomValidators;
using Project.WebAPI.Validations;
using Project.Core.DataAccess.Repository;
using Project.Entities.Dtos;
using Project.Entities.Dtos.PostDtos;
using Project.Business.RealTimeApps.SignalR;

namespace Project.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


    public void ConfigureServices(IServiceCollection services)
    {

      //IDENTITY CONFIGURATION
      services.AddIdentity<User, Role>(identityOptions =>
      {
        //PASSWORD CONFIGURATIONS
        identityOptions.Password.RequireDigit = false; //DIGIT
        identityOptions.Password.RequireLowercase = false; //LOWER CASE
        identityOptions.Password.RequireNonAlphanumeric = false; //NON ALPHA NUMERIC
        identityOptions.Password.RequireUppercase = false; //UPPER CASE
        identityOptions.Password.RequiredLength = 6; //MİN LENGTH

        //USER CONFIGURATIONS
        identityOptions.User.AllowedUserNameCharacters = "abcçdefgğhıijklmnoöpqrstuüvwxyzABCÇDEFGĞHİIJKLMNOÖPQRSTÜUVWXYZ0123456789 -.@_";
        identityOptions.User.RequireUniqueEmail = true; //EŞSİZ MAİL

        identityOptions.SignIn.RequireConfirmedEmail = true;

        identityOptions.ClaimsIdentity.RoleClaimType = "user_role";
        identityOptions.ClaimsIdentity.UserIdClaimType = "userId";
        identityOptions.ClaimsIdentity.UserNameClaimType = "email";


      }).AddPasswordValidator<IdentityPasswordValidator>()
          .AddUserValidator<IdentityUserValidator>()
          .AddErrorDescriber<IdentityCustomErrorDescriber>()
          .AddEntityFrameworkStores<ProjectDbContext>()
          .AddDefaultTokenProviders();



      //AUTHENTICATION CONFIGURATION
      services.AddAuthentication(o =>
      {
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      })
          .AddJwtBearer(o =>
          {
            o.TokenValidationParameters = new TokenValidationParameters
            {
              ClockSkew = TimeSpan.Zero,
              ValidateLifetime = true,
              ValidateAudience = true,
              ValidateIssuer = true,
              ValidateIssuerSigningKey = true,
              ValidIssuer = Configuration["JwtConfiguration:Issuer"],
              ValidAudience = Configuration["JwtConfiguration:Audience"],
              IssuerSigningKey = new SymmetricSecurityKey(
                          Encoding.ASCII.GetBytes(Configuration["JwtConfiguration:SecurityKey"]))
            };

            o.Events = new JwtBearerEvents
            {
              OnMessageReceived = context =>
              {
                var accessToken = context.Request.Query["access_token"];


                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                          (path.StartsWithSegments("/chatHub")))
                {

                  context.Token = accessToken;
                }
                return Task.CompletedTask;
              }
            };
          });



      services.AddControllers()
          .ConfigureApiBehaviorOptions(options =>
          {
                  //AUTO VALIDATION DISABLED
                  options.SuppressModelStateInvalidFilter = true;
          })
          .AddNewtonsoftJson(o =>
          {
                  //INCLUDE
                  o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
          })

          //VALIDATORS
          .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<RegisterDtoValidator>(); })
          .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<ForgotPasswordDtoValidator>(); })
          .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<ResetPasswordDtoValidator>(); })
          .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<LoginDtoValidator>(); })
          .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<ChangePasswordDtoValidator>(); })
          .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<UpdateUserDtoValidator>(); })
          .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<AddArticleDtoValidator>(); })
          .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<UpdateArticleDtoValidator>(); })
          .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<AddArticleCommentDtoValidator>(); })
          .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<UpdateArticleCommentDtoValidator>(); })
          .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<AddAuthorizationAppealDto>(); })
          .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<UpdateArticleCoverPhotoDtoValidator>(); })
          .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<AddFavoriteDtoValidator>(); })
          .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<AddTopicDtoValidator>(); })
          .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<UpdateTopicDtoValidator>(); })
          .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<AddTopicCommentDtoValidator>(); })
          .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<UpdateTopicCommentDtoValidator>(); })
          .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssemblyContaining<AddChatCommentDtoValidator>(); });

      services.AddCors(options =>
      {
        options.AddPolicy("_myAllowSpecificOrigins",
            builder =>
            {
              builder.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .SetIsOriginAllowed((x) => true)
                          .AllowCredentials();
            });
      });
      services.AddSignalR();

     
        }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {


      //CORS CONFIGURATION
      app.UseCors("_myAllowSpecificOrigins");


      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });


      app.UseSignalR(route =>
      {
        route.MapHub<ChatHub>("/chatHub");
      });



    }
  }
}

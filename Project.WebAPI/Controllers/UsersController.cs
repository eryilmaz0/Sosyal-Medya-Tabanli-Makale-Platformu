using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Business.Abstract;
using Project.Core.Business;
using Project.Core.Business.BusinessResultObjects.Enums;
using Project.Entities.DbContext;
using Project.Entities.Dtos;
using Project.Entities.Dtos.PostDtos;
using Project.Entities.Entities;
using Project.Entities.Enums;
using Project.WebAPI.Dtos;
using Project.WebAPI.Filters;

namespace Project.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;
      
     



        //DI
        public UsersController(IUserService userService,UserManager<User> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _userService = userService;
            _authService = authService;

        }



        [HttpPost]
        [ValidationFilter]
        [Route("[Action]")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized();
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);

            if (changePasswordResult.Succeeded)
            {
                return Ok("Şifreniz Başarıyla Değiştirildi.");
            }

            return BadRequest(changePasswordResult.Errors.FirstOrDefault().Description);
        }





        [HttpPut]
        [ValidationFilter]
        public IActionResult UpdateUser(UpdateUserDto updateUserDto)
        {
            var result = this._userService.UpdateUser(updateUserDto);

            if (result.ResultType == ResultType.UnAuthorized)
            {
                return Unauthorized();
            }

            if (result.ResultType == ResultType.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }





        [HttpPut]
        [Route("ProfilePicture")]
        [ValidationFilter]
        public IActionResult UpdateProfilePicture(IFormFile profilePicture)
        {
            var result = this._userService.UpdateProfilePicture(profilePicture);

            if (result.ResultType == ResultType.UnAuthorized)
            {
                return Unauthorized();
            }

            if (result.ResultType == ResultType.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }





        [HttpGet]
        [Route("{userId}")]
        [AllowAnonymous]
        public IActionResult GetUserProfile(long userId)
        {
            var result =  _userService.GetUserProfile(userId);

            if (result.ResultType == ResultType.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }



    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Project.Business.Abstract;
using Project.Core.Business.BusinessResultObjects.Enums;
using Project.Entities.Dtos.PostDtos;
using Project.WebAPI.Filters;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorizationAppealsController : ControllerBase
    {
        private readonly IAuthorizationAppealService _authorizationAppealService;


        //DI
        public AuthorizationAppealsController(IAuthorizationAppealService authorizationAppealService)
        {
            _authorizationAppealService = authorizationAppealService;
        }



        [HttpGet]
        public IActionResult GetAppeals()
        {
            var result = this._authorizationAppealService.GetAuthorizationAppealsByUser();


            if (result.ResultType == ResultType.UnAuthorized)
            {
                return Unauthorized();
            }

            if (result.ResultType == ResultType.Success)
            {
                return Ok(result);
            }


            return BadRequest(result.Message);
        }




        [HttpGet]
        [Route("{appealId}")]
        public IActionResult GetAppeal(long appealId)
        {
            var result = this._authorizationAppealService.GetAuthorizationAppealById(appealId);


            if (result.ResultType == ResultType.UnAuthorized)
            {
                return Unauthorized();
            }

            if (result.ResultType == ResultType.Success)
            {
                return Ok(result.Data);
            }


            return BadRequest(result.Message);
        }





        [HttpPost]
        [ValidationFilter]
        public IActionResult AddAppeal(AddAuthorizationAppealDto addAuthorizationAppealDto)
        {

            var result = this._authorizationAppealService.CreateAuthorizationAppeal(addAuthorizationAppealDto);


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

    }
}

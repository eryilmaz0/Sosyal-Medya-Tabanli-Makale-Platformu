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
using Project.Entities.Dtos;
using Project.Core.Utilities.Response;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;


        //DI
        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }




        [HttpGet]
        public IActionResult GetFavoriteArticlesByUser(int currentPage, int pageSize)
        {
            var result = this._favoriteService.GetFavoriteArticlesByUser(currentPage,pageSize);


            if (result.ResultType == ResultType.UnAuthorized)
            {
                return Unauthorized();
            }


            if (result.ResultType == ResultType.Success)
            {
                return Ok(new PagedDataResponse<FavoriteDto>(result.Data));
            }


            return BadRequest(result.Message);
        }





        [HttpPost]
        [ValidationFilter]
        public IActionResult CreateFavoriteArticle(AddFavoriteDto addFavoriteDto)
        {
            var result = this._favoriteService.CreateFavoriteArticle(addFavoriteDto);


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





        [HttpDelete]
        [Route("{favoriteId}")]
        public IActionResult RemoveFavoriteArticle(long favoriteId)
        {
            var result = this._favoriteService.RemoveFavoriteArticle(favoriteId);


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

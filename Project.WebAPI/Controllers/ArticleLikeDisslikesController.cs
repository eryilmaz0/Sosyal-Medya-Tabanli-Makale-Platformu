using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Project.Business.Abstract;
using Project.Core.Business.BusinessResultObjects.Enums;
using Project.Entities.Enums;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArticleLikeDisslikesController : ControllerBase
    {
        private readonly IArticleLikeDisslikeService _articleLikeDisslikeService;


        //DI
        public ArticleLikeDisslikesController(IArticleLikeDisslikeService articleLikeDisslikesService)
        {
            _articleLikeDisslikeService = articleLikeDisslikesService;
        }




        [HttpGet]
        [Route("Article/{articleId}/Like/Users")]
        [AllowAnonymous]
        public IActionResult GetUsersLikedArticle(long articleId)
        {
            var result = this._articleLikeDisslikeService.GetUsersLikedOrDisslikedArticle(articleId, LikeDisslikeType.Like);

            if (result.ResultType == ResultType.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }






        [HttpGet]
        [Route("Article/{articleId}/Disslike/Users")]
        [AllowAnonymous]
        public IActionResult GetUsersDisslikedArticle(long articleId)
        {
            var result = this._articleLikeDisslikeService.GetUsersLikedOrDisslikedArticle(articleId, LikeDisslikeType.Disslike);

            if (result.ResultType == ResultType.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }







        [HttpPost]
        [Route("Article/{articleId}/[Action]")]
        public IActionResult Like(long articleId)
        {
            var result = this._articleLikeDisslikeService.AddLike(articleId);

            if (result.ResultType == ResultType.UnAuthorized)
            {
                return Unauthorized();
            }

            if (result.ResultType == ResultType.Success)
            {
                return Ok();
            }

            return BadRequest("");
        }








        [HttpPost]
        [Route("Article/{articleId}/[Action]")]
        public IActionResult Disslike(long articleId)
        {
            var result = this._articleLikeDisslikeService.AddDisslike(articleId);

            if (result.ResultType == ResultType.UnAuthorized)
            {
                return Unauthorized();
            }

            if (result.ResultType == ResultType.Success)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}

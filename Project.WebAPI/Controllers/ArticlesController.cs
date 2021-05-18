using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Project.Business.Abstract;
using Project.Business.Concrete;
using Project.Core.Business.BusinessResultObjects.Enums;
using Project.Core.Utilities.Paging;
using Project.Core.Utilities.Response;
using Project.Entities.Dtos;
using Project.Entities.Dtos.PostDtos;
using Project.Entities.Entities;
using Project.WebAPI.Filters;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {

        private readonly IArticleService _articleService;
        
        

        //DI
        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }



        [HttpGet]
        public IActionResult GetAll(int currentPage, int pageSize)
        {
            var result = _articleService.GetAllArticles(currentPage,pageSize);

            if (result.ResultType == ResultType.Success)
            {
                return Ok(new PagedDataResponse<ArticleDto>(result.Data));
            }

            return BadRequest(result.Message);
        }



        [HttpGet]
        [Route("{articleId}")]
        public IActionResult GetArticle(long articleId)
        {
            var result = _articleService.GetArticle(articleId);

            if (result.ResultType == ResultType.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }




        [HttpGet]
        [Route("MostViewed")]
        public IActionResult GetMostViewedArticles(int currentPage, int pageSize)
        {
            var result = _articleService.GetMostViewedArticles(currentPage, pageSize);
            

            if (result.ResultType == ResultType.Success)
            {
                return Ok(new PagedDataResponse<ArticleDto>(result.Data));
            }

            return BadRequest(result.Message);
        }





        [HttpGet]
        [Route("MostLiked")]
        public IActionResult GetMostLikedArticles(int currentPage, int pageSize)
        {
            var result = _articleService.GetMostLikedArticles(currentPage, pageSize);


            if (result.ResultType == ResultType.Success)
            {
                return Ok(new PagedDataResponse<ArticleDto>(result.Data));
            }

            return BadRequest(result.Message);
        }




        [HttpGet]
        [Route("Filter")]
        public IActionResult GetFilteredArticles(string filterText, int currentPage, int pageSize)
        {
            var result = _articleService.GetFilteredArticles(filterText, currentPage, pageSize);

            if (result.ResultType == ResultType.Success)
            {
                return Ok(new PagedDataResponse<ArticleDto>(result.Data));
            }

            return BadRequest();
        }





        [HttpGet]
        [Route("byUserInterest")]
        [Authorize]
        public IActionResult GetArticlesByUserInterest()
        {
            var result = _articleService.GetArticlesByUserInterest();


            if (result.ResultType == ResultType.Success)
            {
                return Ok(result.Data);
            }


            return BadRequest(result.Message);
        }



        //MAKALE OLUŞTURMA
        [HttpPost]
        [Authorize(Roles = "Writer")]
        [ValidationFilter]
        public IActionResult CreateArticle(AddArticleDto addArticleDto)
        {
            var result = _articleService.CreateArticle(addArticleDto);

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





        //MAKALE YÜKLEME file yollarken
        [HttpPost]
        [Route("Upload")]
        [Authorize(Roles = "Writer")]
        public IActionResult CreateArticle(IFormFile articleFile, long articleCategoryId)
        {
          var result = _articleService.CreateArticle(articleFile, articleCategoryId);

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
        [Authorize(Roles = "Writer")]
        [ValidationFilter]
        public IActionResult UpdateArticle(UpdateArticleDto updateArticleDto)
        {
            var result = _articleService.UpdateArticle(updateArticleDto);

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
        [Route("{articleId}/CoverPhoto")]
        [Authorize(Roles = "Writer")]
        [ValidationFilter]
        
        public IActionResult UpdateArticleCoverPhoto(IFormFile articleCoverPhoto, long articleId)
        {
            var result = this._articleService.UpdateArticleCoverPhoto(articleCoverPhoto, articleId);

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
        [Route("{articleId}")]
        [Authorize(Roles = "Writer")]
        public IActionResult RemoveArticle(long articleId)
        {
            var result = _articleService.RemoveArticle(articleId);

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
        [Route("CurrentUser")]
        [Authorize(Roles = "Writer")]
        public IActionResult GetArticlesByUser()
        {
            var result = this._articleService.GetArticlesByUser();

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
        [Route("{articleId}/CurrentUser")]
        [Authorize(Roles = "Writer")]
        public IActionResult GetArticleByUser(long articleId)
        {
            var result = this._articleService.GetArticleByUser(articleId);

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
        [Route("{articleId}/Comments")]
        public IActionResult GetArticleCommentsByArticle(long articleId, int currentPage, int pageSize)
        {
            var result = this._articleService.GetArticleCommentsByArticle(articleId, currentPage, pageSize);

            if (result.ResultType == ResultType.Success)
            {
                
                return Ok(new PagedDataResponse<ArticleCommentDto>(result.Data));
                
            }

            return BadRequest(result.Message);
        }







        [HttpGet]
        [Route("Comments/User/{userId}")]
        public IActionResult GetArticleCommentsByUser(long userId)
        {
            var result = this._articleService.GetArticleCommentsByUser(userId);

            if (result.ResultType == ResultType.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }





        [HttpGet]
        [Route("Comments/{articleCommentId}")]
        public IActionResult GetArticleComment(long articleCommentId)
        {
            var result = this._articleService.GetArticleComment(articleCommentId);

            if (result.ResultType == ResultType.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }





        [HttpPost]
        [Authorize]
        [ValidationFilter]
        [Route("[Action]")]
        public IActionResult Comments(AddArticleCommentDto addArticleCommentDto)
        {
            var result = this._articleService.CreateArticleComment(addArticleCommentDto);

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
        [Authorize]
        [ValidationFilter]
        [Route("[Action]")]
        public IActionResult Comments(UpdateArticleCommentDto updateArticleCommentDto)
        {
            var result = this._articleService.UpdateArticleComment(updateArticleCommentDto);

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
        [Authorize]
        [Route("[Action]/{articleCommentId}")]
        public IActionResult Comments(long articleCommentId)
        {
            var result = this._articleService.RemoveArticleComment(articleCommentId);

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

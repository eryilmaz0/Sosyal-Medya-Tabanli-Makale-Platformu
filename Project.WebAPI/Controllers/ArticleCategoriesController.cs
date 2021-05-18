using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Project.Business.Abstract;
using Project.Core.Business.BusinessResultObjects.Enums;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleCategoriesController : ControllerBase
    {
        private readonly IArticleCategoryService _articleCategoryService;


        //DI
        public ArticleCategoriesController(IArticleCategoryService articleCategoryService)
        {
            _articleCategoryService = articleCategoryService;
        }





        [HttpGet]
        public IActionResult GetArticleCategories()
        {

          var result = _articleCategoryService.GetArticleCategories();

          if (result.ResultType == ResultType.Success)
          {
            return Ok(result);
          }


          return BadRequest(result.Message);
        }



        [HttpGet]
        [Route("User/{userId}/ByUserInterest")]
        public IActionResult GetArticleCategoriesByUserInterest(long userId)

        {
            var result = this._articleCategoryService.PredictArticleCategoryByUser(userId);


            if (result.ResultType == ResultType.Success)
            {
                return Ok(result.Data);
            }


            return BadRequest(result.Message);
        }
    }
}

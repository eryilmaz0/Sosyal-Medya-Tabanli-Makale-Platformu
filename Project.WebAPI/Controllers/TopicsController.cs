using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Project.Business.Abstract;
using Project.Core.Business.BusinessResultObjects.Enums;
using Project.Core.Utilities.Response;
using Project.Entities.Dtos;
using Project.Entities.Dtos.PostDtos;
using Project.WebAPI.Filters;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;


        //DI
        public TopicsController(ITopicService topicService)
        {
            _topicService = topicService;
        }



        [HttpGet]
        public IActionResult GetAllTopics(int currentPage, int pageSize)
        {
            var result = this._topicService.GetAllTopics(currentPage,pageSize);


            if (result.ResultType == ResultType.Success)
            {
                return Ok(new PagedDataResponse<TopicDto>(result.Data));
            }


            return BadRequest(result.Message);
        }





        [HttpGet]
        [Route("{topicId}")]
        public IActionResult GetTopicById(long topicId)
        {
            var result = this._topicService.GetTopic(topicId);


            if (result.ResultType == ResultType.Success)
            {
                return Ok(result.Data);
            }


            return BadRequest(result.Message);
        }





        [HttpPost]
        [Authorize]
        [ValidationFilter]
        public IActionResult CreateTopic(AddTopicDto addTopicDto)
        {
            var result = this._topicService.CreateTopic(addTopicDto);



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
        public IActionResult UpdateTopic(UpdateTopicDto updateTopicDto)
        {
            var result = this._topicService.UpdateTopic(updateTopicDto);



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
        [Route("{topicId}")]
        [Authorize]
        public IActionResult RemoveTopic(long topicId)
        {
            var result = this._topicService.RemoveTopic(topicId);


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
        [Route("Comments/{topicCommentId}")]
        public IActionResult GetTopicCommentById(long topicCommentId)
        {
            var result = this._topicService.GetTopicCommentById(topicCommentId);


            if (result.ResultType == ResultType.Success)
            {
                return Ok(result);
            }


            return BadRequest(result.Message);
        }




        //TOPIC YORUMLARIINI ÇEKME (PAGEABLE)
        [HttpGet]
        [Route("Comments/ByArticle/{topicId}")]
        public IActionResult GetTopicCommentsByArticle(long topicId, int currentPage, int pageSize)
        {
            var result = _topicService.GetTopicCommentsByTopic(topicId, currentPage, pageSize);

            if(result.ResultType == ResultType.Success)
            {
              return Ok(new PagedDataResponse<TopicCommentDto>(result.Data));
            }

            return BadRequest(result.Message);
        }


        [HttpPost]
        [Route("Comments")]
        [Authorize]
        [ValidationFilter]
        public IActionResult CreateTopicComment(AddTopicCommentDto addTopicCommentDto)
        {
            var result = this._topicService.CreateTopicComment(addTopicCommentDto);


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
        [Route("Comments")]
        [Authorize]
        [ValidationFilter]
        public IActionResult UpdateTopicComment(UpdateTopicCommentDto updateTopicCommentDto)
        {
            var result = this._topicService.UpdateTopicComment(updateTopicCommentDto);


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
        [Route("Comments/{topicCommentId}")]
        [Authorize]
        public IActionResult RemoveTopicComment(long topicCommentId)
        {
            var result = this._topicService.RemoveTopicComment(topicCommentId);


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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Business.Abstract;
using Project.Core.Business;
using Project.Core.Business.BusinessResultObjects.Enums;
using Project.Entities.Entities;
using Project.Entities.Enums;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UploadsController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly UserManager<User> _userManager;


        //DI
        public UploadsController(IDocumentService documentService, UserManager<User> userManager)
        {
            _documentService = documentService;
            _userManager = userManager;
        }



        [HttpPost]
        [Authorize]
        public IActionResult UploadProfilePicture(IFormFile profilePicture)
        {
            var result = _documentService.UplaodFile(profilePicture, FileType.ProfilePics);

            if (result.ResultType == ResultType.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult UploadArticleCoverPhoto(IFormFile coverPhoto)
        {
            var result = _documentService.UplaodFile(coverPhoto, FileType.ArticleCoverPhotos);

            if (result.ResultType == ResultType.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
        
    }
}

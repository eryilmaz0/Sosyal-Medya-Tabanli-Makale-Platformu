using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Business.Abstract;
using Project.Core.Business.BusinessResultObjects.Enums;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserHistoriesController : ControllerBase
    {
        private readonly IUserHistoryService _userHistoryService;


        //DI

        public UserHistoriesController(IUserHistoryService userHistoryService)
        {
            _userHistoryService = userHistoryService;
        }


    }
}

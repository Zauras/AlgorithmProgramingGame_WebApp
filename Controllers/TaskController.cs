using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using AlgorithmProgramingGame_WebApp.Controllers.ApiDto;
using AlgorithmProgramingGame_WebApp.Services.DomainModels;
using AlgorithmProgramingGame_WebApp.Services.Facade;

namespace AlgorithmProgramingGame_WebApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CodeTaskController
    {
        private readonly ILogger<CodeTaskController> _logger;
        private readonly ICodeTaskService _codeTaskService;

        public CodeTaskController(ILogger<CodeTaskController> logger, ICodeTaskService codeTaskService)
        {
            _logger = logger;
            _codeTaskService = codeTaskService;
        }

        // [HttpGet("/all")]
        // public ActionResult<IEnumerable<CodeTaskApiDto>> GetCodeTasks()
        // {
        //     var x = _codeTaskService.GetCodeTasks();
        //     return _codeTaskService.GetCodeTasks().Select(CodeTaskModel.ToApiDto).ToList();
        // }
        
        public class Kebab
        {
            public string Name { get; set; }
        }
        
        [HttpGet("all")]
        public ActionResult<IEnumerable<CodeTaskApiDto>> GetCodeTasks()
        {
            var z = "Whatever";
            var x = _codeTaskService.GetCodeTasks();
            //return new Kebab { Name = "kebab"};
            return _codeTaskService.GetCodeTasks().Select(CodeTaskModel.ToApiDto).ToList();
        }



    }
}
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
        
        [HttpGet("/all")]
        public  ActionResult<IEnumerable<CodeTaskApiDto>> GetCodeTasks() =>
            _codeTaskService.GetCodeTasks().Select(CodeTaskModel.ToApiDto).ToList();
        
    }
}
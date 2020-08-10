using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using AlgorithmProgramingGame_WebApp.Controllers.ApiDto;
using AlgorithmProgramingGame_WebApp.Services.DomainModels;
using AlgorithmProgramingGame_WebApp.Services.Facade;


namespace AlgorithmProgramingGame_WebApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SolutionController : ControllerBase
    {
        private readonly ILogger<SolutionController> _logger;
        
        private readonly ISolutionService _solutionService;

        public SolutionController(ILogger<SolutionController> logger, ISolutionService solutionService)
        {
            _logger = logger;
            _solutionService = solutionService;
        }
        
        [HttpPatch("/submit")]
        public void SubmitTaskSolution([FromBody] TaskSolutionSubmissionApiDto taskSolutionSubmission) =>
            _solutionService.SubmitTaskSolution(TaskSolutionSubmissionModel.ToDomainModel(taskSolutionSubmission));

    }

}
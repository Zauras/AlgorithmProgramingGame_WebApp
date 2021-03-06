﻿using System;
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

        [HttpPost("submit")]
        public ActionResult<TaskSolutionSubmissionResponseApiDto> SubmitTaskSolution(
            [FromBody] TaskSolutionSubmissionApiDto taskSolutionSubmission)
        {
            try
            {
                taskSolutionSubmission.UserName = taskSolutionSubmission.UserName.Trim();
                if (string.IsNullOrEmpty(taskSolutionSubmission.UserName))
                {
                    return new TaskSolutionSubmissionResponseApiDto
                        { ValidationErrorMessage = "User name must be not empty string" };
                }
                
                taskSolutionSubmission.TaskSolution = taskSolutionSubmission.TaskSolution.Trim();
                if (string.IsNullOrEmpty(taskSolutionSubmission.UserName))
                {
                    return new TaskSolutionSubmissionResponseApiDto
                        { ValidationErrorMessage = "Task Solution must be not empty string" };
                }
            }
            catch (Exception)
            {
                return new TaskSolutionSubmissionResponseApiDto
                    { ValidationErrorMessage = "User Name and Task Solution must be not empty string" };
            }

            if (taskSolutionSubmission.CodeTaskId < 1)
            {
                return new TaskSolutionSubmissionResponseApiDto
                    { ValidationErrorMessage = "Invalid Tasks selection" };
            }

            return _solutionService.SubmitTaskSolution(
                    TaskSolutionSubmissionModel.ToDomainModel(taskSolutionSubmission))
                .ToApiDto();
        }

        
    }

}
﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using AlgorithmProgramingGame_WebApp.Controllers.ApiDto;
using AlgorithmProgramingGame_WebApp.Services.Facade;
 
namespace AlgorithmProgramingGame_WebApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ScoreController : ControllerBase
    {
        private readonly ILogger<ScoreController> _logger;

        private readonly IScoreService _scoreService;

        public ScoreController(ILogger<ScoreController> logger, IScoreService scoreService)
        {
            _logger = logger;
            _scoreService = scoreService;
        }

        [HttpGet("top/{countOfTopScores}")]
        public ActionResult<IEnumerable<ScoreApiDto>> GetTopScores(int countOfTopScores) => 
            _scoreService.GetTopScores(countOfTopScores).Select(score => score.ToApiDto())
                .ToArray();

    }
}
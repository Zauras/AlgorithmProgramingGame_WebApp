using System.Collections.Generic;
using AlgorithmProgramingGame_WebApp.Controllers.ApiDto;

namespace AlgorithmProgramingGame_WebApp.Services.DomainModels
{
    public class ScoreModel
    {
        public int PlaceIndex { get; set; }
        public string UserName { get; set; }
        public int ScoreCount { get; set; }
        public float SuccessRate { get; set; }
        public IEnumerable<string> TaskNames { get; set; }

        public ScoreApiDto ToApiDto() => new ScoreApiDto
        {
            PlaceIndex = PlaceIndex,
            UserName = UserName,
            ScoreCount = ScoreCount,
            SuccessRate = SuccessRate,
            TaskNames = TaskNames
        };

    }
    
}
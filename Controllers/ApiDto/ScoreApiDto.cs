using System.Collections.Generic;

namespace AlgorithmProgramingGame_WebApp.Controllers.ApiDto
{
    public class ScoreApiDto
    {
        public int PlaceIndex { get; set; }
        public string UserName { get; set; }
        public int ScoreCount { get; set; }
        public float SuccessRate { get; set; }
        public IEnumerable<string> TaskNames { get; set; }
    }
}
using System.Collections.Generic;

namespace AlgorithmProgramingGame_WebApp.Providers.DataModels
{
    public class UserScoreDto
    {
        public int PlaceIndex { get; set; }
        public string UserName { get; set; }
        
        public int SubmissionsCount { get; set; }
        
        public int ScoreCount { get; set; }
        
        public IEnumerable<int> CodeTaskIds { get; set; }
    }
}
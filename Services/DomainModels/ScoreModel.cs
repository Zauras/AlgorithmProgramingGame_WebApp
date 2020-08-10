using AlgorithmProgramingGame_WebApp.Controllers.ApiDto;

namespace AlgorithmProgramingGame_WebApp.Services.DomainModels
{
    public class ScoreModel
    {
        public int PlaceIndex { get; set; }
        public string UserName { get; set; }
        public int SuccessCount { get; set; }
        public float SuccessRate { get; set; }
        public string[] TaskNames { get; set; }
        
        public ScoreModel(int placeIndex, string userName, int successCount, float successRate, string[] taskNames)
        {
            PlaceIndex = placeIndex;
            UserName = userName;
            SuccessCount = successCount;
            SuccessRate = successRate;
            TaskNames = taskNames;
        }

        public ScoreApiDto ToApiDto() => new ScoreApiDto
        {
            PlaceIndex = PlaceIndex,
            UserName = UserName,
            SuccessCount = SuccessCount,
            SuccessRate = SuccessRate,
            TaskNames = TaskNames
        };

    }
    
}
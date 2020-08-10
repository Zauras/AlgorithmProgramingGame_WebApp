namespace AlgorithmProgramingGame_WebApp.Controllers.ApiDto
{
    public class ScoreApiDto
    {
        public int PlaceIndex { get; set; }
        public string UserName { get; set; }
        public int SuccessCount { get; set; }
        public float SuccessRate { get; set; }
        public string[] TaskNames { get; set; }
    }
}
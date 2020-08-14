namespace AlgorithmProgramingGame_WebApp.Controllers.ApiDto
{
    public class TaskSolutionSubmissionResponseApiDto
    {
        public bool IsSuccess { get; set; }
        
        public string ValidationErrorMessage { get; set; }
        
        public string ComputationErrorMessage { get; set; }
    }
}
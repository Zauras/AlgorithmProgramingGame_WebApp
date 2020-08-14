using AlgorithmProgramingGame_WebApp.Controllers.ApiDto;

namespace AlgorithmProgramingGame_WebApp.Services.DomainModels
{
    public class TaskSolutionSubmissionResponseModel 
    {
        public bool IsSuccess { get; set; }
        public string ValidationErrorMessage { get; set; }
        public string ComputationErrorMessage { get; set; }
        
        
        public TaskSolutionSubmissionResponseApiDto ToApiDto() => 
            new TaskSolutionSubmissionResponseApiDto
        {
            IsSuccess = IsSuccess,
            ValidationErrorMessage = ValidationErrorMessage, 
            ComputationErrorMessage = ComputationErrorMessage
        };

    }
}
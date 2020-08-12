using AlgorithmProgramingGame_WebApp.Services.DomainModels;

namespace AlgorithmProgramingGame_WebApp.Services.Facade
{
    public interface ISolutionService
    {
        public TaskSolutionSubmissionResponseModel SubmitTaskSolution(TaskSolutionSubmissionModel taskSolutionSubmission);
    }
}

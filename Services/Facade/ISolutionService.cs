using AlgorithmProgramingGame_WebApp.Services.DomainModels;

namespace AlgorithmProgramingGame_WebApp.Services.Facade
{
    public interface ISolutionService
    {
        public void SubmitTaskSolution(TaskSolutionSubmissionModel taskSolutionSubmission);
    }
}

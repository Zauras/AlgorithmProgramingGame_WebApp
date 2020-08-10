using AlgorithmProgramingGame_WebApp.Providers.Facade;
using AlgorithmProgramingGame_WebApp.Services.DomainModels;
using AlgorithmProgramingGame_WebApp.Services.Facade;

namespace AlgorithmProgramingGame_WebApp.Services
{
    public class SolutionService: ISolutionService
    {
        private readonly IScoreProvider _scoreProvider;

        public SolutionService(IScoreProvider scoreProvider)
        {
            _scoreProvider = scoreProvider;
        }
        
        public void SubmitTaskSolution(TaskSolutionSubmissionModel taskSolutionSubmission)
        {
            // Calculate
            _scoreProvider.AddScore(taskSolutionSubmission);
        }
    }
}
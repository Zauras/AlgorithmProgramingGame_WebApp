using System.Collections.Generic;
using AlgorithmProgramingGame_WebApp.Services.DomainModels;

namespace AlgorithmProgramingGame_WebApp.Providers.Facade
{
    public interface IScoreProvider
    {
        public IEnumerable<ScoreModel> GetTopScores(int countOfTopScores);

        public void AddScore(TaskSolutionSubmissionModel taskSolutionSubmission);
    }
}
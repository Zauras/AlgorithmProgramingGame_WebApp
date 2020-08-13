using System.Collections.Generic;
using AlgorithmProgramingGame_WebApp.Providers.DataModels;
using AlgorithmProgramingGame_WebApp.Services.DomainModels;

namespace AlgorithmProgramingGame_WebApp.Providers.Facade
{
    public interface IUserProvider
    {
        public IEnumerable<UserScoreDto> GetTopScores(int countOfTopScores);

        public bool IsNameExists(string name);

        public void AddScore(TaskSolutionSubmissionModel taskSolutionSubmission);

        public void RegisterFailedScore(string userName);

    }
}
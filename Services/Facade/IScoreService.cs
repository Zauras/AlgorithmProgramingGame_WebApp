using System.Collections.Generic;

using AlgorithmProgramingGame_WebApp.Services.DomainModels;

namespace AlgorithmProgramingGame_WebApp.Services.Facade
{
    public interface IScoreService
    {
        public IEnumerable<ScoreModel> GetTopScores(int countOfTopScores);
    }
}
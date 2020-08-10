using System.Collections.Generic;

using AlgorithmProgramingGame_WebApp.Providers.Facade;
using AlgorithmProgramingGame_WebApp.Services.DomainModels;
using AlgorithmProgramingGame_WebApp.Services.Facade;

namespace AlgorithmProgramingGame_WebApp.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IScoreProvider _scoreProvider;
    
        public ScoreService(IScoreProvider scoreProvider)
        {
            _scoreProvider = scoreProvider;
        }
        
        public IEnumerable<ScoreModel> GetTopScores(int countOfTopScores)
        {
            return _scoreProvider.GetTopScores(countOfTopScores);
        }
    }
}

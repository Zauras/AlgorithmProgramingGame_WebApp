using System.Collections.Generic;

using AlgorithmProgramingGame_WebApp.Providers.Facade;
using AlgorithmProgramingGame_WebApp.Services.DomainModels;
using AlgorithmProgramingGame_WebApp.Services.Facade;

namespace AlgorithmProgramingGame_WebApp.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IUserProvider _userProvider;
    
        public ScoreService(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }
        
        public IEnumerable<ScoreModel> GetTopScores(int countOfTopScores)
        {
            return _userProvider.GetTopScores(countOfTopScores);
        }
    }
}

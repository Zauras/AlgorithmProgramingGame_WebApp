using System.Collections.Generic;
using System.Linq;

using AlgorithmProgramingGame_WebApp.Providers.DataModels;
using AlgorithmProgramingGame_WebApp.Providers.Facade;
using AlgorithmProgramingGame_WebApp.Services.DomainModels;
using AlgorithmProgramingGame_WebApp.Services.Facade;

namespace AlgorithmProgramingGame_WebApp.Services
{
    public class ScoreService : IScoreService
    {
        private readonly ICodeTaskService _codeTaskService;
        private readonly IUserProvider _userProvider;

        public ScoreService(ICodeTaskService codeTaskService, IUserProvider userProvider)
        {
            _userProvider = userProvider;
            _codeTaskService = codeTaskService;
        }
        
        public IEnumerable<ScoreModel> GetTopScores(int countOfTopScores)
        {
            IEnumerable<UserScoreDto> userScoreDtos = _userProvider.GetTopScores(countOfTopScores);
            IEnumerable<CodeTaskModel> codeTasks = _codeTaskService.GetCodeTasks();

            return userScoreDtos.Select(userScoreDto => new ScoreModel
            {
                PlaceIndex = userScoreDto.PlaceIndex,
                UserName = userScoreDto.UserName,
                ScoreCount = userScoreDto.ScoreCount,
                SuccessRate = userScoreDto.ScoreCount / (float) userScoreDto.SubmissionsCount,
                TaskNames = codeTasks.Where(task => userScoreDto.CodeTaskIds
                                .Contains(task.CodeTaskId))
                                .Select(task => task.Name)
            });
        }
    }
}
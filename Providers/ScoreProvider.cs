using System.Collections.Generic;
using AlgorithmProgramingGame_WebApp.Providers.Facade;
using AlgorithmProgramingGame_WebApp.Services.DomainModels;

namespace AlgorithmProgramingGame_WebApp.Providers
{
    public class ScoreProvider : IScoreProvider
    {
        private static List<ScoreModel> _scores = new List<ScoreModel>()
        {
            new ScoreModel(1, "Margaret Thatcher", 6, 0.82f, new[] {"Fibonacci Sequence"}),
            new ScoreModel(2, "Willy Turner", 7, 0.65f, new[] {"Fibonacci Sequence"}),
            new ScoreModel(3, "John Cena", 5, 0.45f, new[] {"Fibonacci Sequence"})
        };
            
        public IEnumerable<ScoreModel> GetTopScores(int countOfTopScores)
        {
            return _scores;
        }

        public void AddScore(TaskSolutionSubmissionModel taskSolutionSubmission)
        {
            _scores.Add(
                new ScoreModel(
                    _scores.Count + 1,
                    taskSolutionSubmission.UserName,
                    1,
                    1, 
                    new[] {"Fibonacci Sequence"}
                    )
                );
        }
    }
}
using System.Collections.Generic;
using AlgorithmProgramingGame_WebApp.Providers.DataModels;
using AlgorithmProgramingGame_WebApp.Providers.Facade;
using AlgorithmProgramingGame_WebApp.Services.DomainModels;
using MongoDB.Driver;

namespace AlgorithmProgramingGame_WebApp.Providers
{
    public class ScoreProvider : IScoreProvider
    {
        private readonly IMongoCollection<Task> _tasks;
        
        private static List<ScoreModel> _scores = new List<ScoreModel>()
        {
            new ScoreModel(1, "Margaret Thatcher", 6, 0.82f, new[] {"Fibonacci Sequence"}),
            new ScoreModel(2, "Willy Turner", 7, 0.65f, new[] {"Fibonacci Sequence"}),
            new ScoreModel(3, "John Cena", 5, 0.45f, new[] {"Fibonacci Sequence"})
        };
        
        public ScoreProvider(ICodeSolutionsDatabaseSettings settings)
        {
            var dbClient = new MongoClient(settings.ConnectionString);
            var database = dbClient.GetDatabase(settings.DatabaseName);

            _tasks = database.GetCollection<Task>(settings.TaskCollectionName);
        }
        
        public List<Task> GetAll() =>
            _tasks.Find(task => true).ToList();
        
        public Task Create(Task task)
        {
            _tasks.InsertOne(task);
            return task;
        }
        
        public void Update(string id, Task taskIn) =>
            _tasks.ReplaceOne(task => task.Id == id, taskIn);

        public void Remove(Task taskIn) =>
            _tasks.DeleteOne(task => task.Id == taskIn.Id);

        public void Remove(string id) => 
            _tasks.DeleteOne(task => task.Id == id);
        
        
        
        
            
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
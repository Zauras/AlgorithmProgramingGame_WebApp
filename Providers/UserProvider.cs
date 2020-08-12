using System.Collections.Generic;
using MongoDB.Driver;

using AlgorithmProgramingGame_WebApp.Providers.DataModels;
using AlgorithmProgramingGame_WebApp.Providers.Facade;
using AlgorithmProgramingGame_WebApp.Services.DomainModels;

namespace AlgorithmProgramingGame_WebApp.Providers
{
    public class UserProvider : IUserProvider
    {
        private static List<ScoreModel> _scores = new List<ScoreModel>()
        {
            new ScoreModel(1, "Margaret Thatcher", 6, 0.82f, new[] {"Fibonacci Sequence"}),
            new ScoreModel(2, "Willy Turner", 7, 0.65f, new[] {"Fibonacci Sequence"}),
            new ScoreModel(3, "John Cena", 5, 0.45f, new[] {"Fibonacci Sequence"})
        };

        private readonly IMongoCollection<UserEntity> _users;

        public UserProvider(ICodeSolutionsDatabaseSettings settings)
        {
            var dbClient = new MongoClient(settings.ConnectionString);
            var database = dbClient.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<UserEntity>(settings.UsersCollectionName);
        }

        public IEnumerable<ScoreModel> GetTopScores(int countOfTopScores)
        {
            //return _users;
            return _scores;
        }

        public bool IsNameExists(string name) =>
            _users.Find(user => user.Name == name).Limit(1).CountDocuments() > 0;


        public void AddScore(TaskSolutionSubmissionModel taskSolutionSubmission)
        {
            if (IsNameExists(taskSolutionSubmission.UserName))
            {
                InsertScore(
                    taskSolutionSubmission.CodeTaskId, 
                    taskSolutionSubmission.TaskSolution,
                    taskSolutionSubmission.UserName
                );
            }
            else
            {
                Create(new UserEntity
                {
                    Name = taskSolutionSubmission.UserName,
                    SubmissionsCount = 1,
                    Scores = new[]
                    {
                        new ScoreEntity
                        {
                            CodeTaskId = taskSolutionSubmission.CodeTaskId,
                            SolutionCode = taskSolutionSubmission.TaskSolution
                        }
                    }
                });
            }

            //
            // _scores.Add(
            //     new ScoreModel(
            //         _scores.Count + 1,
            //         taskSolutionSubmission.UserName,
            //         1,
            //         1, 
            //         new[] {"Fibonacci Sequence"}
            //         )
            //     );
        }

        public void IncrementSubmissionsCount(string userName, int amountOfIncrement = 1)
        {
            var update = new UpdateDefinitionBuilder<UserEntity>().Inc(e => e.SubmissionsCount, 1);
            _users.FindOneAndUpdate(e => e.Name == userName, update);
        }

        private void InsertScore(int codeTaskId, string taskSolution, string userName)
        {
            var s = new ScoreEntity
            {
                CodeTaskId = codeTaskId,
                SolutionCode = taskSolution
            };

            // _users.Find(e => e.Name == userName).Single().Scores
            //     .Append(s);
            
            var update = new UpdateDefinitionBuilder<UserEntity>().Push(e => e.Scores, s);
            _users.FindOneAndUpdate(e => e.Name == userName, update);
            
            IncrementSubmissionsCount(userName);
            
            // var filter =
            //     Builders<UserEntity>.Filter.Eq(e => e.Name, taskSolutionSubmission.UserName);
            // var update = Builders<ScoreEntity[]>.Update.Push<ScoreEntity>();
            // _users.FindOneAndUpdate(filter, update);
        }


        private void Create(UserEntity user) =>
            _users.InsertOne(user);

    }
}
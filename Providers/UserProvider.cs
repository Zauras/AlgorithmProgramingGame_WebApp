using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

using AlgorithmProgramingGame_WebApp.Providers.DataModels;
using AlgorithmProgramingGame_WebApp.Providers.Facade;
using AlgorithmProgramingGame_WebApp.Services.DomainModels;

namespace AlgorithmProgramingGame_WebApp.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IMongoCollection<UserEntity> _users;

        public UserProvider(ICodeSolutionsDatabaseSettings settings)
        {
            var dbClient = new MongoClient(settings.ConnectionString);
            var database = dbClient.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<UserEntity>(settings.UsersCollectionName);
        }

        public IEnumerable<UserScoreDto> GetTopScores(int countOfTopScores) =>
            _users.Aggregate()
                .Project(user => new UserScoreDto
                {
                    UserName = user.Name,
                    SubmissionsCount = user.SubmissionsCount,
                    ScoreCount = user.Scores.Length,
                    CodeTaskIds = user.Scores.Select(score => score.CodeTaskId)
                })
                .SortByDescending(user => user.ScoreCount)
                .Limit(countOfTopScores)
                .ToEnumerable()
                .Select((userScoreDto, index) =>
                {
                    userScoreDto.PlaceIndex = index + 1;
                    // Work around since MongoDb Driver does not support LINQ Distinct()
                    userScoreDto.CodeTaskIds = userScoreDto.CodeTaskIds.Distinct().ToArray();
                    return userScoreDto;
            });
        

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
                    Scores = new []
                    {
                        new ScoreEntity
                        {
                            CodeTaskId = taskSolutionSubmission.CodeTaskId,
                            SolutionCode = taskSolutionSubmission.TaskSolution
                        }
                    }
                });
            }
        }

        public void RegisterFailedScore(string userName)
        {
            if (IsNameExists(userName))
            {
                IncrementSubmissionsCount(userName);
            }
            else
            {
                Create(new UserEntity
                {
                    Name = userName,
                    SubmissionsCount = 1,
                    Scores = new ScoreEntity[0]
                });
            }
        }

        private void IncrementSubmissionsCount(string userName, int amountOfIncrement = 1)
        {
            var updateQuery = new UpdateDefinitionBuilder<UserEntity>()
                .Inc(user => user.SubmissionsCount, amountOfIncrement);
            
            _users.FindOneAndUpdate(e => e.Name == userName, updateQuery);
        }

        private void InsertScore(int codeTaskId, string taskSolution, string userName)
        {
            var scoreEntity = new ScoreEntity
            {
                CodeTaskId = codeTaskId,
                SolutionCode = taskSolution
            };

            var updateQuery = new UpdateDefinitionBuilder<UserEntity>()
                .Push(user => user.Scores, scoreEntity);
            
            _users.FindOneAndUpdate(user => user.Name == userName, updateQuery);
            
            IncrementSubmissionsCount(userName);
        }


        private void Create(UserEntity user) =>
            _users.InsertOne(user);

    }
}
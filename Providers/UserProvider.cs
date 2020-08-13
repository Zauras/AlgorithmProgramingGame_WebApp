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

        public IEnumerable<UserScoreDto> GetTopScores(int countOfTopScores)
        {


            var x = _users.Aggregate()
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
            
                // .Limit(countOfTopScores)
                // .ToList();
                // .Select(user => new UserScoreDto
                // {
                //     UserName = user.Name,
                //     SubmissionsCount = user.SubmissionsCount,
                //     ScoreCount = user.Scores.Length,
                //     CodeTaskIds = user.Scores.Select(score => score.CodeTaskId)
                // });

            // .Project(user => new UserScoreDto
            // {
            //     UserName = user.Name,
            //     SubmissionsCount = user.SubmissionsCount,
            //     ScoreCount = user.Scores.Length,
            //     CodeTaskIds = user.Scores.Select(score => score.CodeTaskId)
            // }).ToList()
            // .Select(userScoreDto =>
            // {
            //     // Work around since MongoDb Driver does not support LINQ Distinct()
            //     userScoreDto.CodeTaskIds = userScoreDto.CodeTaskIds.Distinct().ToArray();
            //     return userScoreDto;
            // }).ToList();
            return x;

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
            
            var update = new UpdateDefinitionBuilder<UserEntity>().Inc(e => e.SubmissionsCount, amountOfIncrement);
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
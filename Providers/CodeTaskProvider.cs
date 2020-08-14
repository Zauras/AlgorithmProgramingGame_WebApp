using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

using AlgorithmProgramingGame_WebApp.Providers.DataModels;
using AlgorithmProgramingGame_WebApp.Providers.Facade;
using AlgorithmProgramingGame_WebApp.Services.DomainModels;

namespace AlgorithmProgramingGame_WebApp.Providers
{
    public class CodeTaskProvider : ICodeTaskProvider
    {
        private readonly IMongoCollection<CodeTaskEntity> _tasks;
        
        public CodeTaskProvider(ICodeSolutionsDatabaseSettings settings)
        {
            var dbClient = new MongoClient(settings.ConnectionString);
            var database = dbClient.GetDatabase(settings.DatabaseName);

            _tasks = database.GetCollection<CodeTaskEntity>(settings.CodeTaskCollectionName);
        }

        public IEnumerable<CodeTaskModel> GetAll()
        {
            var taskEntities = _tasks.Find(task => true).ToList();
            return taskEntities.Select(CodeTaskModel.ToDomainModel);
        }
        
    }
}
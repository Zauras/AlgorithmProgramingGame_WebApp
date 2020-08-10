using System.Collections.Generic;
using System.Linq;
using AlgorithmProgramingGame_WebApp.Providers.DataModels;
using AlgorithmProgramingGame_WebApp.Providers.Facade;
using AlgorithmProgramingGame_WebApp.Services.DomainModels;
using MongoDB.Driver;

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
        
        // public CodeTaskModel Create(CodeTaskEntity task)
        // {
        //     _tasks.InsertOne(task);
        //     return task;
        // }
        //
        // public void Update(string id, CodeTask taskIn) =>
        //     _tasks.ReplaceOne(task => task.Id == id, taskIn);
        //
        // public void Remove(CodeTask taskIn) =>
        //     _tasks.DeleteOne(task => task.Id == taskIn.Id);
        //
        // public void Remove(string id) => 
        //     _tasks.DeleteOne(task => task.Id == id);
        
    }
}
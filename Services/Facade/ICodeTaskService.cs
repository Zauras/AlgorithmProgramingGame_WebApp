using System.Collections.Generic;

using AlgorithmProgramingGame_WebApp.Services.DomainModels;

namespace AlgorithmProgramingGame_WebApp.Services.Facade
{
    public interface ICodeTaskService
    {
        public IEnumerable<CodeTaskModel> GetCodeTasks();
    }
}
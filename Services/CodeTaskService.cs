using System.Collections.Generic;

using AlgorithmProgramingGame_WebApp.Providers.Facade;
using AlgorithmProgramingGame_WebApp.Services.DomainModels;
using AlgorithmProgramingGame_WebApp.Services.Facade;

namespace AlgorithmProgramingGame_WebApp.Services
{
    public class CodeTaskService : ICodeTaskService
    {
        private readonly ICodeTaskProvider _codeTaskProvider;
    
        public CodeTaskService(ICodeTaskProvider codeTaskProvider)
        {
            _codeTaskProvider = codeTaskProvider;
        }
        
        public IEnumerable<CodeTaskModel> GetCodeTasks() =>
            _codeTaskProvider.GetAll();
        
    }
}

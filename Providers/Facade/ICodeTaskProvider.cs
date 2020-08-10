using System.Collections.Generic;
using AlgorithmProgramingGame_WebApp.Services.DomainModels;

namespace AlgorithmProgramingGame_WebApp.Providers.Facade
{
    public interface ICodeTaskProvider
    {
        public IEnumerable<CodeTaskModel> GetAll();
    }
}
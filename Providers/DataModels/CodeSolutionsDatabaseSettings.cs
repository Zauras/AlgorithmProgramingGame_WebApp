namespace AlgorithmProgramingGame_WebApp.Providers.DataModels
{
    public interface ICodeSolutionsDatabaseSettings
    {
        string TaskCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
    
    public class CodeSolutionsDatabaseSettings : ICodeSolutionsDatabaseSettings
    {
        public string TaskCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
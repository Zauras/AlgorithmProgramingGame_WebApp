namespace AlgorithmProgramingGame_WebApp.Providers.DataModels
{
    public interface ICodeSolutionsDatabaseSettings
    {
        string CodeTaskCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
    
    public class CodeSolutionsDatabaseSettings : ICodeSolutionsDatabaseSettings
    {
        public string CodeTaskCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    
    // mongod --dbpath <data_directory_path>
    // mongo
    // use CodeSolutionsDb
    // db.createCollection('CodeTasks')
    // db.CodeTasks.insert({ 'Name':'Fibonacci Sequence', 'Description':'The Fibonacci sequence is a set of numbers that starts with a one or a zero, followed by a one, and proceeds based on the rule that each number (called a Fibonacci number) is equal to the sum of the preceding two numbers. If the Fibonacci sequence is denoted F (n), where n is the first term in the sequence, the following equation obtains for n = 0, where the first two terms are defined as 0 and 1 by convention: F (0) = 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 ...'})
    // db.CodeTasks.find({}).pretty()
   
}
using AlgorithmProgramingGame_WebApp.Controllers.ApiDto;

namespace AlgorithmProgramingGame_WebApp.Services.DomainModels
{
    public class TaskSolutionSubmissionModel
    {
        public string UserName { get; set; }

        public int TaskId { get; set; }

        public string CodeString { get; set; }
        
        
        public TaskSolutionSubmissionModel(string userName, int taskId, string codeString)
        {
            UserName = userName;
            TaskId = taskId;
            CodeString = codeString;
        }
        
        public static TaskSolutionSubmissionModel ToDomainModel(TaskSolutionSubmissionApiDto dto) =>
            new TaskSolutionSubmissionModel(dto.UserName, dto.TaskId, dto.CodeString);

    }
}
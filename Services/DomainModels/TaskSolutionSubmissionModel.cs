using AlgorithmProgramingGame_WebApp.Controllers.ApiDto;

namespace AlgorithmProgramingGame_WebApp.Services.DomainModels
{
    public class TaskSolutionSubmissionModel
    {
        public string UserName { get; set; }

        public int CodeTaskId { get; set; }

        public string TaskSolution { get; set; }
        
        
        public TaskSolutionSubmissionModel(string userName, int codeTaskId, string taskSolution)
        {
            UserName = userName;
            CodeTaskId = codeTaskId;
            TaskSolution = taskSolution;
        }
        
        public static TaskSolutionSubmissionModel ToDomainModel(TaskSolutionSubmissionApiDto dto) =>
            new TaskSolutionSubmissionModel(dto.UserName, dto.CodeTaskId, dto.TaskSolution);

    }
}
using AlgorithmProgramingGame_WebApp.Controllers.ApiDto;
using AlgorithmProgramingGame_WebApp.Providers.DataModels;

namespace AlgorithmProgramingGame_WebApp.Services.DomainModels
{
    public class CodeTaskModel
    {
        public int CodeTaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CodeTaskModel(int codeTaskId, string name, string description)
        {
            CodeTaskId = codeTaskId;
            Name = name;
            Description = description;
        }

        public static CodeTaskModel ToDomainModel(CodeTaskEntity entity) =>
            new CodeTaskModel(entity.CodeTaskId, entity.Name, entity.Description);

        public static CodeTaskApiDto ToApiDto(CodeTaskModel model) =>
            new CodeTaskApiDto
            {
                CodeTaskId = model.CodeTaskId,
                Name = model.Name,
                Description = model.Description
            };

    }
}
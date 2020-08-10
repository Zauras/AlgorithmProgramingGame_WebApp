using AlgorithmProgramingGame_WebApp.Controllers.ApiDto;
using AlgorithmProgramingGame_WebApp.Providers.DataModels;

namespace AlgorithmProgramingGame_WebApp.Services.DomainModels
{
    public class CodeTaskModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public CodeTaskModel(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public static CodeTaskModel ToDomainModel(CodeTaskEntity entity) =>
            new CodeTaskModel(entity.Name, entity.Description);

        public static CodeTaskApiDto ToApiDto(CodeTaskModel model) =>
            new CodeTaskApiDto
            {
                Name = model.Name,
                Description = model.Description
            };

    }
}
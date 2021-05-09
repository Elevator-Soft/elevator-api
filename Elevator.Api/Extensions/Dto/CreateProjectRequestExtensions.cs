using Elevator.Api.Dto;
using Models;

namespace Elevator.Api.Extensions.Dto
{
    public static class CreateProjectRequestExtensions
    {
        public static Project ToServiceProject(this CreateProjectRequestDto createProjectRequestDto) => new Project
        {
            Name = createProjectRequestDto.Name,
            GitToken = createProjectRequestDto.GitToken,
            ProjectUri = createProjectRequestDto.GitUrl
        };
    }
}

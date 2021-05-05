using Elevator.Api.Dto;
using Models;

namespace Elevator.Api.Extensions.Dto
{
    public static class CreateBuildConfigRequestExtensions
    {
        public static BuildConfig ToServiceModel(this CreateBuildConfigRequestDto createBuildConfigRequestDto) =>
            new BuildConfig
            {
                Name = createBuildConfigRequestDto.Name,
                ProjectId = createBuildConfigRequestDto.ProjectId
            };
    }
}

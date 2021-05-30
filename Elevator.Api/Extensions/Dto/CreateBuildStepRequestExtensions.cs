using Elevator.Api.Dto;
using Models;

namespace Elevator.Api.Extensions.Dto
{
    public static class CreateBuildStepRequestExtensions
    {
        public static BuildStep ToServiceModel(this CreateBuildStepRequestDto createBuildStepRequestDto) => new()
        {
            Name = createBuildStepRequestDto.Name,
            BuildConfigId = createBuildStepRequestDto.BuildConfigId,
            BuildStepScript = createBuildStepRequestDto.BuildStepScript
        };
    }
}

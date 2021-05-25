using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Elevator.Api.Authentication;
using Elevator.Api.Dto;
using Elevator.Api.Services.Interfaces;
using Elevator.Api.Utils.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectAccesses = Elevator.Api.Dto.ProjectAccesses;

namespace Elevator.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController: ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UserController> logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [HttpGet("me")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<HttpOperationResult<UserDto>> GetCurrentUserAsync()
        {
            if (!await userService.ExistsAsync(CurrentUser.Id))
            {
                return HttpOperationResult<UserDto>.Ok(new()
                {
                    Id = CurrentUser.Id,
                    Email = CurrentUser.Id,
                    Name = CurrentUser.Name,
                    IsRegistered = false,
                    ProjectAccesses = new()
                    {
                        Admin = new List<string>(),
                        User = new List<string>()
                    }
                });
            }

            var currentUser = await userService.GetByIdAsync(CurrentUser.Id);
            return HttpOperationResult<UserDto>.Ok(ModelsMapper.ConvertUserServiceModelToDto(currentUser));
        }

        [HttpPost("me/register")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<HttpOperationResult<UserDto>> RegisterCurrentUserAsync()
        {
            var currentUser = new User()
            {
                Id = CurrentUser.Id,
                Email = CurrentUser.Id,
                Name = CurrentUser.Name,
                ProjectAccesses = new()
                {
                    Admin = new List<string>(),
                    User = new List<string>()
                }
            };

            await userService.RegisterUser(currentUser);

            return HttpOperationResult<UserDto>.Ok(ModelsMapper.ConvertUserServiceModelToDto(currentUser));
        }
    }
}
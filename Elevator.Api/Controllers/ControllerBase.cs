using System;
using Elevator.Api.Authentication;
using Models;
using Microsoft.AspNetCore.Mvc;
using Elevator.Api.Claims;
using Elevator.Api.Extensions;

namespace Elevator.Api.Controllers
{
    public abstract class ControllerBase: Controller
    {
        protected AuthenticatedUser CurrentUser => User.GetServerUser();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Elevator.Api.Exceptions
{
    public class ClaimNotFoundException: ApiException
    {
        public ClaimNotFoundException(string claimType) : base(HttpStatusCode.InternalServerError, $"Claim with type '{claimType}' not found")
        { }
    }
}

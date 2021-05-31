using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Agent.Manager.Client.Providers
{
    public abstract class BaseProvider
    {
        protected readonly string ApiUrl;
        protected readonly HttpClient HttpClient;

        protected BaseProvider(string apiUrl)
        {
            ApiUrl = apiUrl ?? throw new ArgumentNullException(nameof(apiUrl));
            HttpClient = new HttpClient();
        }
    }
}

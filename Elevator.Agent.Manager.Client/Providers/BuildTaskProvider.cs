using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Common;
using Elevator.Agent.Manager.Client.Models;
using Models;
using Newtonsoft.Json;

namespace Elevator.Agent.Manager.Client.Providers
{
    public sealed class BuildTaskProvider: BaseProvider
    {
        public BuildTaskProvider(string apiUrl) : base(apiUrl)
        { }

        public async Task<HttpOperationResult<Build>> PushTaskAsync(BuildTaskDto task)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(new Uri(ApiUrl), "/buildTasks"));
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");

            var response = await HttpClient.SendAsync(request);

            if ((int) response.StatusCode < 200 || (int) response.StatusCode > 299)
                return HttpOperationResult<Build>.Failed(HttpStatusCode.InternalServerError, "Something gone wrong. Check agent-manager's logs");

            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<HttpOperationResult<Build>>(body);
        }
    }
}

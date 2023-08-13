using RestSharp;
using System.Threading.Tasks;

namespace BedeBackEndTestProject.BaseActions
{
    public class Actions
    {
        private static RestClient restClient;
        public static string url;

        public static RestClient NewRestClient()
        {
            restClient = new();
            return restClient;
        }

        public RestRequest AddQueryParameterToGetRequest(string parameterName, string parameterValue, RestRequest restRequest)
        {
            restRequest.AddParameter(parameterName, parameterValue, ParameterType.QueryString);

            return restRequest;
        }

        public async Task<RestResponse> GetRequest(string url)
        {

            var request = new RestRequest(url, Method.Get);

            var response = await restClient.ExecuteAsync(request);

            return response;
        }
    }
}
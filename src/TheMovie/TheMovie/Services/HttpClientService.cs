using System;
using System.Net.Http;
using System.Net.Http.Headers;
using TheMovie.Common.API;

namespace TheMovie.Services
{
    public class HttpClientService : IHttpClientService
    {
        public HttpClient HeaderTheMovieApi()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(TheMovieApi.API_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}

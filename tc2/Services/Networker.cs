using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace tc2
{
    class Networker : IService, INetworker
    {
        public class LoggingHandler : DelegatingHandler
        {
            public LoggingHandler(HttpMessageHandler innerHandler)
                : base(innerHandler)
            {
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                Console.WriteLine("Request:");
                Console.WriteLine(request.ToString());
                if (request.Content != null)
                {
                    Console.WriteLine(await request.Content.ReadAsStringAsync());
                }
                Console.WriteLine();

                HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

                Console.WriteLine("Response:");
                Console.WriteLine(response.ToString());
                if (response.Content != null)
                {
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                }
                Console.WriteLine();

                return response;
            }
        }
        public HttpClient HttpClient { get; }

        public Networker()
        {
            //this.HttpClient = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            this.HttpClient = new HttpClient();
            this.HttpClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
        }
        public NetworkResult Get(string link) => new NetworkResult(this.HttpClient.GetAsync(link).Result.Content);
        public NetworkResult Post(string link, HttpContent content) => new NetworkResult(this.HttpClient.PostAsync(link, content).Result.Content);

        public NetworkResult LongPoll(string link, int timeOut)
        {
            Task<HttpResponseMessage> t = HttpClient.GetAsync(link);
            bool wait = t.Wait(timeOut * 1000);
            if (wait) return new(t.Result.Content);
            else return null;
        }
    }
}

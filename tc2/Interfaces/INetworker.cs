using System.Net.Http;

namespace tc2
{
    interface INetworker
    {
        NetworkResult Get(string link);
        NetworkResult Post(string link, HttpContent content);
        NetworkResult LongPoll(string link, int timeOut);
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> SendAsync(HttpMethod method, string url, HttpContent content);
        Task<HttpResponseMessage> PutAsync(string url, string token, HttpContent content);
        Task<HttpResponseMessage> PutAsync(string url, HttpContent content);
    }
}

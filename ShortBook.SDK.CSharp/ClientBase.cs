using System.Collections;
using System.Collections.Generic;
using RestSharp;

namespace ShortBook.SDK.CSharp
{
    public abstract class ClientBase
    {
        private readonly RestClient _client;

        protected ClientBase()
        {
            Parameters = new RequestParameters();
#if DEBUG
            _client = new RestClient("http://localhost:5000/api");
#else
            client = new RestClient("http://www.shortbook.me/api");
#endif
        }

        protected RequestParameters Parameters { get; }

        protected RestResponse Post(string resource)
        {
            var request = new RestRequest(resource);
            foreach (var parameter in Parameters)
            {
                request.AddQueryParameter(parameter.Name, parameter.Value);
            }
            
            return _client.PostAsync(request).Result;
        }
        
        protected RestResponse Post(string resource, object body)
        {
            var request = new RestRequest(resource);
            request.AddJsonBody(body);
            return _client.PostAsync(request).Result;
        }

        protected class RequestParameters : IEnumerable<RequestParameter>
        {
            private readonly List<RequestParameter> _parameters;

            public RequestParameters()
            {
                _parameters = new List<RequestParameter>();
            }

            public void Clear()
            {
                _parameters.Clear();
            }

            public RequestParameters Add(string name, string value)
            {
                _parameters.Add(new RequestParameter(name, value));
                return this;
            }

            public IEnumerator<RequestParameter> GetEnumerator()
            {
                return _parameters.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        protected class RequestParameter
        {
            public RequestParameter(string name, string value)
            {
                Name = name;
                Value = value;
            }

            public string Name { get; set; }

            public string Value { get; set; }
        }
    }
}
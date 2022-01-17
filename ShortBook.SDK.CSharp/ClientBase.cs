using System.Collections;
using System.Collections.Generic;
using RestSharp;

namespace ShortBook.SDK.CSharp
{
    public abstract class ClientBase
    {
        private readonly IRestClient _client;

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

        protected IRestResponse Post(string resource)
        {
            var request = new RestRequest(resource, Method.POST, DataFormat.Json);
            foreach (var parameter in Parameters)
            {
                request.AddParameter(parameter.Name, parameter.Value);
            }
            
            return _client.Execute(request);
        }
        
        protected IRestResponse Post(string resource, object body)
        {
            var request = new RestRequest(resource, Method.POST, DataFormat.Json);
            request.AddJsonBody(body);
            return _client.Execute(request);
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

            public RequestParameters Add(string name, object value)
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
            public RequestParameter(string name, object value)
            {
                Name = name;
                Value = value;
            }

            public string Name { get; set; }

            public object Value { get; set; }
        }
    }
}
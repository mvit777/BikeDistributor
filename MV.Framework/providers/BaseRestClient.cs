using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MV.Framework.providers
{
    public class BaseRestClient
    {
        private string _baseUrl;
        private IRestClient _client;
        /// <summary>
        /// TODO: ADD OVERRIED WITH AUTH WHEN CAPABILITY (OR MAYBE LET IT FOR DERIVEDED CLASSES)
        /// https://restsharp.dev/usage/
        /// </summary>
        /// <param name="baseUrl"></param>
        public BaseRestClient(string baseUrl)
        {
            _baseUrl = baseUrl;
            _client = new RestClient(_baseUrl);
        }

        

        public T Execute<T>(RestRequest request) where T : new()
        {
            var response = _client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var MyException = new Exception(message, response.ErrorException);
                throw MyException;
            }
            return response.Data;
        }
    }
}

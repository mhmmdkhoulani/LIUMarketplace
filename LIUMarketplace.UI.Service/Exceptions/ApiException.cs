using LIUMarketplace.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.UI.Service.Exceptions
{
    public class ApiException : Exception
    {
        public AuthResponse AuthResponse { get; set; }

        public ApiException(AuthResponse authResponse)
        {
            AuthResponse = authResponse;
        }

        public HttpStatusCode StatusCode { get; set; }

        public ApiException(AuthResponse authResponse, HttpStatusCode statusCode) : this(authResponse)
        {
            AuthResponse = authResponse;
            StatusCode = statusCode;
        }

        
    }
}

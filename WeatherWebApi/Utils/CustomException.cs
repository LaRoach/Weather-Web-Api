using System;

namespace WeatherWebApi.Utils
{
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }

        public CustomException() { }

        public CustomException(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public CustomException(string message, Exception inner, int statusCode)
            : base(message, inner)
        {
            StatusCode = statusCode;
        }
    }
}
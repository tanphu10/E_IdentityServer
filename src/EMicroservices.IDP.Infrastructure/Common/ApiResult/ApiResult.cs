﻿namespace EMicroservices.IDP.Infrastructure.Common.ApiResult
{
    public class ApiResult<T>
    {
        public ApiResult(bool isSucceeded, string message = null)
        {
            Message = message;
            IsSucceeded = isSucceeded;
        }

        public ApiResult(bool isSucceeded, T data, string message = null)
        {
            Data = data;
            Message = message;
            IsSucceeded = isSucceeded;
        }

        public bool IsSucceeded { get; set; }
        public string Message { get; set; }
        public T Data { get; }
    }
}
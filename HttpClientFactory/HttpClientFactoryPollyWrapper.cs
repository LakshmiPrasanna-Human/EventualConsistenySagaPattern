/*
    Description:    Reusable methods to support Pollys and Jitter retry policy. It also has Circuit Braker implementation.
    Author:         Lakshmi Prasanna

*/

using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace HttpClientFactory
{
    public class HttpClientFactoryPollyWrapper
    {
       
        public static IAsyncPolicy<HttpResponseMessage> GetHierarchyServiceRetryPolicy()
        {
           
            HttpStatusCode[] httpStatusCodesWorthRetrying = {
               HttpStatusCode.RequestTimeout, // 408
               HttpStatusCode.InternalServerError, // 500
               HttpStatusCode.BadGateway, // 502
               HttpStatusCode.ServiceUnavailable, // 503
               HttpStatusCode.GatewayTimeout, // 504
          
            };
            //With Jitter applied
            Random jitterer = new Random();
            var retryPolicy = Policy.Handle<HttpRequestException>()
              .Or<OperationCanceledException>()
              .OrResult<HttpResponseMessage>(r => httpStatusCodesWorthRetrying.Contains(r.StatusCode))
              .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                                                   + TimeSpan.FromMilliseconds(jitterer.Next(0, 100)));
            return retryPolicy;
        }

        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(120));
        }

        /*static IAsyncPolicy<HttpResponseMessage>  DoFallbackAction()
        {
            //some fallback action
            return 
        }*/

    }
}

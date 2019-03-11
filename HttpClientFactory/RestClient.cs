/*
    Description:    RestClient which supports resuable class with methods for Get, Post, Put and Delete. 
                    It supports any input and output data.
    Author:         Lakshmi Prasanna

*/


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace HttpClientFactory
{
    public class RestClient<T, R> where T : class where R : class
    {
        //Do not Delete commented Code in this file as it is meant for future use.
        private readonly string _baseAddress;
        private readonly string _accessToken;
        // private readonly int _httpClientTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["HTTPClientTimeOut"] ?? "60");
        private readonly int _httpClientTimeOut = 60;

        public RestClient() { }
        
        public async Task<T> GetSingleItemRequest(HttpClient client, string apiUrl)
        {
            T result = null;

            var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T>(x.Result);
                });

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;
                    var Errorresult = JsonConvert.DeserializeObject<ResultEntity>(x.Result);
                    throw new Exception(Errorresult.ErrorDetails.ErrorMessage);
                });
            }

            return result;
        }


        public async Task<T[]> GetMultipleItemsRequest(HttpClient client, string apiUrl)
        {
            T[] result = null;

                  
            var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T[]>(x.Result);
             });

            }
   
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;
                    var resultentity = JsonConvert.DeserializeObject<ResultEntity>(x.Result);
                    throw new Exception(resultentity.ErrorDetails.ErrorMessage);
                });
            }

            return result;
        } 
        

        public async Task<R> PostRequest(HttpClient client, string apiUrl, T postObject)
        {
            R result = null;

           
            var response = await client.PostAsync(apiUrl, postObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<R>(x.Result);

                });

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;
                    var Errorresult = JsonConvert.DeserializeObject<ResultEntity>(x.Result);
                    throw new Exception(Errorresult.ErrorDetails.ErrorMessage);
                });
            }

            return result;
        }

       

        public async Task<R> PutRequest(HttpClient client, string apiUrl, T putObject)
        {
            R result = null;
          
            var response = await client.PutAsync(apiUrl, putObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);

            // response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<R>(x.Result);

                });

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;
                    var Errorresult = JsonConvert.DeserializeObject<ResultEntity>(x.Result);
                    throw new Exception(Errorresult.ErrorDetails.ErrorMessage);
                });
            }
            return result;
        }


        public async Task<T> DeleteRequest(HttpClient client, string apiUrl)
        {
            T result = null;
         
            var response = await client.DeleteAsync(apiUrl).ConfigureAwait(false);

            //response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T>(x.Result);

                });

            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;
                    var Errorresult = JsonConvert.DeserializeObject<ResultEntity>(x.Result);
                    throw new Exception(Errorresult.ErrorDetails.ErrorMessage);
                });
            }
            return result;
        }
    }



}

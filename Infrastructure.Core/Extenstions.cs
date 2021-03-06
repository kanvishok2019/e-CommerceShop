﻿using System;
using System.Collections;

namespace Infrastructure.Core
{
    public static class Extensions
    {
        public static bool IsSimpleType(this object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var type = value.GetType();

            return
                type.IsValueType ||
                type.IsPrimitive ||
                ((IList) new Type[]
                {
                    typeof(String),
                    typeof(Decimal),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan),
                    typeof(Guid)
                }).Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }

        //public static Task<HttpResponseMessage> PostAsJsonAsync<T>(
        //    this HttpClient httpClient, string url, T data)
        //{
        //    var dataAsString = JsonConvert.SerializeObject(data);
        //    var content = new StringContent(dataAsString);
        //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    return httpClient.PostAsync(url, content);
        //}

        //public static Task<HttpResponseMessage> PutAsJsonAsync<T>(
        //    this HttpClient httpClient, string url, T data)
        //{
        //    var dataAsString = JsonConvert.SerializeObject(data);
        //    var content = new StringContent(dataAsString);
        //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    return httpClient.PutAsync(url, content);
        //}

        //public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        //{
        //    var dataAsString = await content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<T>(dataAsString);
        //}
    }
}

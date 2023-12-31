﻿using CaseProject.AppConfig;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CaseProject.Helper
{
    public static class HttpHelper
    {
        private static string _url = AppConfigurationService.GetApiMainUrl();

        public static async Task<TResult> HttpPost<TResult, TInput>(string url, TInput model, Dictionary<string, string> headerParams, int? timeOutSecond = null)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    if (headerParams != null && headerParams.Count > decimal.Zero)
                    {
                        foreach (var item in headerParams)
                            httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }

                    if (timeOutSecond != null && timeOutSecond > 0)
                        httpClient.Timeout = TimeSpan.FromSeconds((int)timeOutSecond);

                    var _postDataJson = JsonConvert.SerializeObject(model);

                    var _postDataContent = new StringContent(_postDataJson, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(_url + url, _postDataContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(response.ReasonPhrase);
                    }

                    string jsonResult = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<TResult>(jsonResult);

                    return result;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

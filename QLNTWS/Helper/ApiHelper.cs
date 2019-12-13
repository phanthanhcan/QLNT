using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using QLNTWS.Models;
using System.Web;
using System.Net;
// <package id = "System.Net.Http.Formatting.Extension" version="5.2.3.0" targetFramework="net47" />
//  <package id = "System.Net.Http" version="4.3.4" targetFramework="net47" />

namespace QLNTWS.Helper
{
    public class ApiHelper<T>
    {
        public static async Task<dynamic> RunPostAsync(string url, object input = null)
        {
            dynamic output = null;
            using (HttpClient client = new HttpClient())
            {
                //string apiURL = "http://localhost/QLBHAPI/api/";
                string apiURL = ConfigurationManager.AppSettings["apiURL"]; // dpc tu file config
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync(url, input);
                if (response.IsSuccessStatusCode)
                {
                    output = await response.Content.ReadAsAsync<T>();
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string jsonStr = await response.Content.ReadAsStringAsync();
                    var t = JsonConvert.DeserializeObject<BadRequestModel>(jsonStr);
                    string errMsg = null;
                    if (t.ModelState == null)
                    {
                        errMsg = t.Message;
                    }
                    else
                    {
                        foreach (KeyValuePair<string, string[]> item in t.ModelState)
                        {
                            errMsg += $"{string.Join(" ,", item.Value)}<br/>";
                        }
                    }
                    throw new Exception(errMsg);
                }
                else
                {
                    throw new Exception("Lỗi kết nối với service");
                }
            }
            return output;
        }

        public static async Task<dynamic> RunGetAsync(string url)
        {
            dynamic output = null;
            using (HttpClient client = new HttpClient())
            {
                //string apiURL = "http://localhost:49972/api/";
                string apiURL = ConfigurationManager.AppSettings["apiURL"];
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    output = await response.Content.ReadAsAsync<T>();
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string jsonStr = await response.Content.ReadAsStringAsync();
                    var t = JsonConvert.DeserializeObject<BadRequestModel>(jsonStr);
                    string errMsg = null;
                    if (t.ModelState == null)
                    {
                        errMsg = t.Message;
                    }
                    else
                    {
                        foreach (KeyValuePair<string, string[]> item in t.ModelState)
                        {
                            errMsg += $"{string.Join(" ,", item.Value)}<br/>";
                        }
                    }
                    throw new Exception(errMsg);
                }
                else
                {
                    throw new Exception("Lỗi kết nối với service");
                }
            }
            return output;
        }
    }
}


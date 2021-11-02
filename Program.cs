using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace C_2119
{
    class Program
    {
        static async Task<HttpResponseMessage> func(){
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"https://3au52778nk.execute-api.ap-northeast-1.amazonaws.com/default/jphacks2021_unity_ddb");
                // Console.Write(result);  
                return result;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var parameters = @"{""foo"":""boo""}";

            var content = new StringContent(parameters, Encoding.UTF8, @"application/json");

            var res = func();

            Console.Write(res.Result.StatusCode);
            Console.Write(res.Result.Content.ReadAsStringAsync().Result);

            // using (var client = new HttpClient())
            // {
            //     Task result = await client.GetAsync($"https://3au52778nk.execute-api.ap-northeast-1.amazonaws.com/default/jphacks2021_unity_ddb");
            //     // Console.Write(result);  
            // }

            
        }
    }
}

/*
var parameters = @"{""foo"":""boo""}"

var content = new StringContent(parameters, Encoding.UTF8, @"application/json");

using (var client = new HttpClient())
{
    var result = await client.GetAsync($"https://3au52778nk.execute-api.ap-northeast-1.amazonaws.com/default/jphacks2021_unity_ddb", content);
}

Console.Write(result);*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.UI;

public class HttpRequest : MonoBehaviour
{
    public GameObject prefabObj;
    public string url;
    public class Walls
    {
        [JsonProperty("data")]
        public List<Wall> eachData { get; set; }
    }
    public class Wall
    {
        [JsonProperty("fileName")]
        public string fileName { get; set; }
        [JsonProperty("data")]
        public List<List<float>> data { get; set; }
    }

    public static Walls publicData;
    // Start is called before the first frame update
    public async Task<HttpResponseMessage> func()
    {
        Debug.Log("In func");
        using (var client = new HttpClient())
        {
            Debug.Log("got HTTP Client");
            var result = await client.GetAsync(url);
            Debug.Log("got HTTP response");
            // Console.Write(result);  
            Debug.Log(result.StatusCode);
            Debug.Log(result.Headers);
            Debug.Log(result.Content.ReadAsStringAsync().Result);
            Debug.Log(result.Content.ReadAsStringAsync().Result.GetType());
            string data = result.Content.ReadAsStringAsync().Result;
            Walls WallFromJson = new Walls();
            WallFromJson = JsonConvert.DeserializeObject<Walls>(data);
            Debug.Log(WallFromJson.eachData[0].fileName);
            List<Wall> DataArray = WallFromJson.eachData;
            for(int i = 0; i < DataArray.Count; i++)
            {
                Debug.Log("creating button");
                int row = i % 3;
                int column = i / 3;
                GameObject obj = Instantiate(prefabObj);
                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
                obj.GetComponent<RectTransform>().localScale = new Vector3(0.03f, 0.03f, 0.02f);
                float x = -4.41f + row * 4.41f;
                float y = 1.82f - column * 2f;
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
                obj.name = DataArray[i].fileName;
                obj.GetComponentInChildren<Text>().text = DataArray[i].fileName;
            }
            GameObject.Find("reload_btn").GetComponentInChildren<Text>().text = "Reload";
            publicData = WallFromJson;
            return result;
        }
    }
    void Start()
    {
        Debug.Log("Hello World");
        var parameters = @"{""foo"":""boo""}";
        var content = new StringContent(parameters, Encoding.UTF8, @"application/json");
        var res = func();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
//using Newtonsoft.Jsonが必要

public class CreateWall : MonoBehaviour
{
    //[JsonObject]はお試しでjsonのデータ欲しくて書いてるだけなので本来はいらない
    [JsonObject("wall")]
    public class Wall
    {
        [JsonProperty("x_w")]
        public float x_w { get; set; }
        [JsonProperty("y_w")]
        public float y_w { get; set; }
        [JsonProperty("width_w")]
        public float width_w { get; set; }
        [JsonProperty("length_w")]
        public float length_w { get; set; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //お試しにjson形式のデータが欲しかったから作ってるだけで本来は以下のコードはいらない
        List<Wall> walls = new List<Wall>();
        walls.Add(new Wall(){x_w=2.5f, y_w=3.5f, width_w=0.2f, length_w=5.2f});
        walls.Add(new Wall(){x_w=5.5f, y_w=3.5f, width_w=0.2f, length_w=5.2f});
        walls.Add(new Wall(){x_w=4.0f, y_w=1.0f, width_w=3.2f, length_w=0.2f});
        walls.Add(new Wall(){x_w=4.0f, y_w=6.0f, width_w=3.2f, length_w=0.2f});
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(walls);

        //ここから先が実際に使うコード。jsonって書いてあるところに使いたいjson形式のデータを入れる。
        List<Wall> Walls= new List<Wall>();
        Walls = JsonConvert.DeserializeObject<List<Wall>>(json);

        //unityの座標軸の関係でyの値をxに代入している。
        GameObject resource_w = (GameObject)Resources.Load("Wall");
        for(int r = 0; r <=Walls.Count ; r++){
            GameObject wall1 = Instantiate(resource_w,new Vector3(Walls[r].x_w,0,Walls[r].y_w),Quaternion.identity);
            wall1.transform.localScale = new Vector3(Walls[r].width_w, 2.0f, Walls[r].length_w);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

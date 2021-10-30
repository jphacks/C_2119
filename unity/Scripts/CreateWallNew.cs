using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallNew : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<List<float>> Rooms = ClickExample.Rooms;
        GameObject resource_w = (GameObject)Resources.Load("Wall");
        Debug.Log(Rooms.Count);
        for (int i = 0; i < Rooms.Count; i++)
        {
            GameObject wall1 = Instantiate(resource_w, new Vector3(Rooms[i][0] + Rooms[i][2]/2, 0, Rooms[i][1]), Quaternion.identity);
            wall1.transform.localScale = new Vector3(Rooms[i][2], 6.0f, 0.1f);
            GameObject wall2 = Instantiate(resource_w, new Vector3(Rooms[i][0] + Rooms[i][2] / 2, 0, Rooms[i][1]+Rooms[i][3]), Quaternion.identity);
            wall2.transform.localScale = new Vector3(Rooms[i][2], 6.0f, 0.1f);
            GameObject wall3 = Instantiate(resource_w, new Vector3(Rooms[i][0], 0, Rooms[i][1] + Rooms[i][3] / 2), Quaternion.identity);
            wall3.transform.localScale = new Vector3(0.1f, 6.0f, Rooms[i][3]);
            GameObject wall4 = Instantiate(resource_w, new Vector3(Rooms[i][0] + Rooms[i][2], 0, Rooms[i][1] + Rooms[i][3] / 2), Quaternion.identity);
            wall4.transform.localScale = new Vector3(0.1f, 6.0f, Rooms[i][3]);
            GameObject.Find("XRRig").transform.position = new Vector3(Rooms[i][0] + Rooms[i][2] / 2, 0, Rooms[i][1] + Rooms[i][3] / 2);
            GameObject.Find("Camera").transform.position = new Vector3(Rooms[i][0] + Rooms[i][2] / 2, 80, Rooms[i][1] + Rooms[i][3] / 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}

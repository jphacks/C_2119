using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class putbed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("space")){
            
            GameObject chairs = Instantiate (gameObject, new Vector3(GameObject.Find("Main Camera").transform.position.x,0.2f,GameObject.Find("Main Camera").transform.position.z), Quaternion.Euler(-90,-90,0));
            chairs.transform.rotation = Quaternion.Euler(-90, GameObject.Find("Main Camera").transform.eulerAngles.y + 180, 0);
        }
        


    }
}
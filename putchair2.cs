using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class putchair2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("tab")){
            Quaternion quaternion = GameObject.Find("Main Camera").transform.rotation;
            float y = quaternion.eulerAngles.y; 
            GameObject chairs = Instantiate (gameObject, new Vector3(GameObject.Find("Main Camera").transform.position.x,0.25f,GameObject.Find("Main Camera").transform.position.z), Quaternion.Euler(-90,0,0));
            chairs.transform.LookAt(GameObject.Find("Main Camera").transform);
            chairs.transform.rotation = Quaternion.Euler(-90, -y, 0);
            
        }


    }
}

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
            
            GameObject chairs = Instantiate (gameObject, new Vector3(GameObject.Find("Main Camera").transform.position.x,0.25f,GameObject.Find("Main Camera").transform.position.z), Quaternion.Euler(-90,-90,0));
            chairs.transform.rotation = Quaternion.Euler(-90, GameObject.Find("Main Camera").transform.eulerAngles.y, 0);
        }
        


    }
}

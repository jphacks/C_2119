using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class putragmat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("return")){
            
            GameObject chairs = Instantiate (gameObject, new Vector3(GameObject.Find("Main Camera").transform.position.x,0.001f,GameObject.Find("Main Camera").transform.position.z), Quaternion.Euler(-90,0,0));
        }
        


    }
}

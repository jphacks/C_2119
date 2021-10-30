using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefabObj;
    void Start()
    {
        GameObject obj = Instantiate(prefabObj, new Vector3(2.0f, LineRendererSettings.height, 2.0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

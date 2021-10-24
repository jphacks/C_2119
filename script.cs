using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localScaleで大きさを変える
        //矢印キーで大きさを変える
        gameObject.transform.localScale = new Vector3(
            gameObject.transform.localScale.x + Input.GetAxis("Horizontal"),
            gameObject.transform.localScale.y + Input.GetAxis("Vertical"),
            gameObject.transform.localScale.z);

        //transform.positionで位置を変える
        //スペースキーで位置を変える
        if(Input.GetKey("space")){
            gameObject.transform.position = new Vector3(
                gameObject.transform.position.x + 1,
                gameObject.transform.position.y + 1,
                gameObject.transform.position.z + 1);
        }

        //instantiateで複製可能。この時位置も指定出来る。ただ複製後に、複製元のを変更したらその変更も反映される。複製、名前つける、付けた名前で大きさ変更、が良さそう？？
        //タブキーで複製
        if(Input.GetKey("tab")){
            Instantiate (gameObject, new Vector3(0.0f,2.0f,0.0f), Quaternion.identity);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ClickExample : MonoBehaviour
{
	public Button btn;
    public static List<List<float>> Rooms;

    void Start()
	{
		btn = gameObject.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		HttpRequest.Walls WallData = HttpRequest.publicData;
        Debug.Log(WallData);
        if (btn != null)
        {
            if (btn.name == "reload_btn")
            {
                Debug.Log("reload");
            }
            else
            {
                List<HttpRequest.Wall> DataArray = WallData.eachData;
                for (int i = 0; i < DataArray.Count; i++)
                {
                    if (btn.name == DataArray[i].fileName)
                    {
                        Rooms = DataArray[i].data;
                        SceneManager.LoadScene("RoomScene");
                    }
                }
            }
        }
        Debug.Log(btn.name);
	}
}
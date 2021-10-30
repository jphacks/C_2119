using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LineRendererSettings : MonoBehaviour
{
    [SerializeField] LineRenderer rend;
    Vector3[] points;
    public LayerMask layerMask;
    public GameObject panel;
    public Image img;
    public Button btn;

    public static float height = 0;
    public static List<List<float>> Rooms;

    public bool AlignLineRenderer(LineRenderer rend)
    {
        bool hitBtn = false;
        Ray ray;
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            points[1] = transform.forward + new Vector3(0, 0, hit.distance);
            rend.startColor = Color.red;
            rend.endColor = Color.red;
            rend.startWidth = (float)0.2;
            rend.endWidth = (float)0.2;
            btn = hit.collider.gameObject.GetComponent<Button>();
            hitBtn = true;
        }
        else
        {
            points[1] = transform.forward + new Vector3(0, 0, 40);
            rend.startColor = Color.green;
            rend.endColor = Color.green;
            rend.startWidth = (float)0.4;
            rend.endWidth = (float)0.4;
            hitBtn = false;
        }
        rend.SetPositions(points);
        rend.material.color = rend.startColor;
        return hitBtn;
    }

    public void ColorChangeOnClick()
    {
        HttpRequest.Walls WallData = HttpRequest.publicData;
        if (btn != null)
        {
            if(btn.name == "reload_btn")
            {
                img.color = Color.red;
            }
            else if(btn.name == "blue_btn")
            {
                height = 10;
                SceneManager.LoadScene("RoomScene");
            }
            else if (btn.name == "green_btn")
            {
                height = 20;
                SceneManager.LoadScene("RoomScene");
            }
            else
            {
                List<HttpRequest.Wall> DataArray = WallData.eachData;
                for(int i = 0; i < DataArray.Count; i++)
                {
                    if(btn.name == DataArray[i].fileName)
                    {
                        Rooms = DataArray[i].data;
                    }
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        img = panel.GetComponent<Image>();
        rend = gameObject.GetComponent<LineRenderer>();
        points = new Vector3[2];
        points[0] = Vector3.zero;
        points[1] = transform.position + new Vector3(0, 0, 20);
        rend.SetPositions(points);
        rend.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(AlignLineRenderer(rend) && Input.GetAxis("Submit") > 0)
        {
            btn.onClick.Invoke();
        }
    }
}

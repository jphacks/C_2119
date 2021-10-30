using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LineRenderSettingsRoom : MonoBehaviour
{
    [SerializeField] LineRenderer rend;
    Vector3[] points;
    public LayerMask layerMask;

    public static float height = 0;

    public void AlignLineRenderer(LineRenderer rend)
    {
        Ray ray;
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            points[1] = transform.forward + new Vector3(0, 0, hit.distance);
            rend.startColor = Color.red;
            rend.endColor = Color.red;
            //rend.startWidth = (float)0.2;
            //rend.endWidth = (float)0.2;
        }
        else
        {
            points[1] = transform.forward + new Vector3(0, 0, 40);
            rend.startColor = Color.green;
            rend.endColor = Color.green;
            //rend.startWidth = (float)0.4;
            //rend.endWidth = (float)0.4;
        }
        rend.SetPositions(points);
        rend.material.color = rend.startColor;
    }


    // Start is called before the first frame update
    void Start()
    {
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
        AlignLineRenderer(rend);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private bool doMoveMent = true;


    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float scrollspeed = 5f;

    public float minx = 0f;
    public float maxx = 35f;


    private void Update()
    {
        //해당 키 누르면 이동 및 마우스로 겉테두리 누르면 이동 
        if (Input.GetKey("a"))
        {

            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d"))
        {

            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);

        }

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minx, maxx);

        transform.position = pos;
    }
}

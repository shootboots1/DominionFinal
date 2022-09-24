using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    private Vector3 resetCamera;
    private Vector3 Origin;
    private Vector3 Difference;
    private bool Drag = false;

    public Camera cam;

    public float zoom;
    public float maxZoom, minZoom;

    public float zoomForce = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        resetCamera = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        zoom = Input.GetAxisRaw("Mouse ScrollWheel");
        if(cam.orthographicSize >= maxZoom && cam.orthographicSize <= minZoom)
        {
            cam.orthographicSize -= zoom * zoomForce;
        }
        if(cam.orthographicSize < maxZoom)
        {
            cam.orthographicSize = maxZoom + 0.1f;
        }
        if(cam.orthographicSize > minZoom)
        {
            cam.orthographicSize = minZoom - 0.1f;
        }

        if (Input.GetMouseButton(1))
        {
            Difference = (cam.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;

            if(Drag == false)
            {
                Drag = true;
                Origin = cam.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            Drag = false;
        }

        if(Drag == true)
        {
            cam.transform.position = Origin - Difference;
        }

        if (Input.GetMouseButton(2))
        {
            cam.transform.position = resetCamera;
        }
    }
}

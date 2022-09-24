using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V2cameraBounds : MonoBehaviour
{
    private Camera cam;
    private BoxCollider2D cameraBox;
    public BoxCollider2D boundary;

    private float leftPivot;
    private float rightPivot;
    private float topPivot;
    private float botPivot;

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }
    void Update()
    {
        CalculateCameraPivot();
        FollowPlayer();
    }

    void CalculateCameraPivot()
    {
        botPivot = boundary.bounds.min.y + cameraBox.size.y / 2;
        topPivot = boundary.bounds.max.y - cameraBox.size.y / 2;
        leftPivot = boundary.bounds.min.x + cameraBox.size.x / 2;
        rightPivot = boundary.bounds.max.x - cameraBox.size.x / 2;

    }

    void FollowPlayer()
    {
        transform.position = new Vector3(Mathf.Clamp(cam.transform.position.x, leftPivot, rightPivot),
                                         Mathf.Clamp(cam.transform.position.y, botPivot, topPivot),
                                         transform.position.z);
    }
}

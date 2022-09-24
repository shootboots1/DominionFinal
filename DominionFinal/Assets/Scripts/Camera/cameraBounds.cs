using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBounds : MonoBehaviour
{
    public Camera linkedCamera;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float vertExtent = linkedCamera.orthographicSize;
        float horizExtent = vertExtent * Screen.width / Screen.height;

        Vector3 linkedCameraPos = linkedCamera.transform.position;
        Bounds areaBounds = boxCollider.bounds;

        linkedCamera.transform.position = new Vector3(
            Mathf.Clamp(linkedCameraPos.x, areaBounds.min.x + horizExtent, areaBounds.max.x - horizExtent),
            Mathf.Clamp(linkedCameraPos.y, areaBounds.min.y + vertExtent, areaBounds.max.y - vertExtent),
            linkedCameraPos.z);
    }
}

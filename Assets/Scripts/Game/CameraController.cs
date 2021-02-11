using Mirror;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float currentZoom = 10f;
    private float zoomSpeed = 4f;
    private float minZoom = 5f;
    private float maxZoom = 15f;

    // Update is called once per frame
    void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }
}

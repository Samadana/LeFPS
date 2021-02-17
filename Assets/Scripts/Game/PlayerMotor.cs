using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Transform chest;

    private Vector3 velocity;
    private Vector3 rotation;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;


    [SerializeField]
    private float cameraRotationLimit = 85f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRigidBodyRotation();
    }

    private void LateUpdate()
    {
        PerformCameraAndChestRotation();
    }

    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    private void PerformRigidBodyRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
    }

    private void PerformCameraAndChestRotation()
    {
        // On calcule la rotation de la caméra
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        // On applique la rotation de la caméra
        cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        chest.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
}
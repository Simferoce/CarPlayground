using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private float defaultVelocity = 25f;
    [SerializeField] private float minimumGasPedal = 0.7f;
    [SerializeField] private float minimumSpeedAccepted = 15f;
    [SerializeField] private float spawnHeight = 3f;
    [SerializeField] private CinemachineCamera virtualCamera;
    [SerializeField] private CameraPosition cameraPosition;

    private float horizontal;
    private float vertical;
    private RaceController raceController;

    private void Awake()
    {
        raceController = GameObject.FindFirstObjectByType<RaceController>();
    }

    private void Start()
    {
        Repositionate();
    }

    public void OnHorizontal(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<float>();
    }

    public void OnVertical(InputAction.CallbackContext context)
    {
        vertical = context.ReadValue<float>();
    }

    private void Update()
    {
        if (car.Velocity.magnitude < minimumSpeedAccepted)
        {
            Repositionate();
        }

        car.SetSteering(horizontal);
        car.SetGasPedal(Mathf.Max(minimumGasPedal, vertical));
        Debug.Log(car.Velocity.magnitude);
    }

    public void Repositionate()
    {
        Vector3 center = raceController.GetClosestPoint(car.transform.position, out Vector3 direction);
        if (Physics.Raycast(center + Vector3.up * 10, Vector3.down, out RaycastHit hit))
            center = hit.point;

        center += Vector3.up * spawnHeight;
        Vector3 previousPosition = cameraPosition.transform.position;
        car.ResetAt(center, direction);
        car.SetVelocity(car.Rigidbody.transform.forward * defaultVelocity);
        cameraPosition.Update();
        Vector3 newLookDirection = car.transform.position - cameraPosition.transform.position;
        virtualCamera.ForceCameraPosition(cameraPosition.transform.position, Quaternion.LookRotation(newLookDirection, Vector3.up));
    }
}


using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private Wheel frontLeft;
    [SerializeField] private Wheel frontRight;
    [SerializeField] private Wheel backLeft;
    [SerializeField] private Wheel backRight;
    [SerializeField] private Steering steering;
    [SerializeField] private float groundedDrag = 0.5f;

    public Rigidbody Rigidbody { get => rigidbody; set => rigidbody = value; }
    public Wheel FrontLeft { get => frontLeft; set => frontLeft = value; }
    public Wheel FrontRight { get => frontRight; set => frontRight = value; }
    public Wheel BackLeft { get => backLeft; set => backLeft = value; }
    public Wheel BackRight { get => backRight; set => backRight = value; }
    public float GasPedal { get; private set; }
    public Vector3 Velocity { get; private set; }
    public bool IsGrounded => wheels.Any(x => x.IsGrounded);

    private List<Wheel> wheels = new List<Wheel>();
    private Rigidbody rigidbody;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody>();

        wheels.Add(frontLeft);
        wheels.Add(frontRight);
        wheels.Add(backLeft);
        wheels.Add(backRight);

        steering.Initialize(this);
        foreach (Wheel wheel in wheels)
            wheel.Initialize(this);
    }

    public void SetVelocity(Vector3 velocity)
    {
        rigidbody.linearVelocity = velocity;
    }

    public void SetSteering(float amount)
    {
        steering.Steer(amount);
    }

    public void SetGasPedal(float amount)
    {
        GasPedal = amount;
    }

    public void ResetAt(Vector3 point, Vector3 direction)
    {
        this.transform.forward = direction.normalized;
        this.transform.position = point;
        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        foreach (Wheel wheel in wheels)
            wheel.Tick();

        Velocity = rigidbody.linearVelocity;
        rigidbody.linearDamping = IsGrounded ? groundedDrag : 0;
    }
}

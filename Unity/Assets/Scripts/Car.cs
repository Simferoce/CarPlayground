using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Car : MonoBehaviour
{
    [SerializeField]
    private Wheel frontLeft;

    [SerializeField]
    private Wheel frontRight;

    [SerializeField]
    private Wheel backLeft;

    [SerializeField]
    private Wheel backRight;

    [SerializeField]
    private float steeringAngle = 45;

    [SerializeField]
    private float steeringPerSeconds = 90;

    [SerializeField]
    private float startingVelocity = 25f;

    public Rigidbody Rigidbody { get => rigidbody; set => rigidbody = value; }

    private List<Wheel> wheels = new List<Wheel>();
    private Vector2 input = Vector2.zero;
    private Rigidbody rigidbody;
    private Vector3 steeringDirection;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        rigidbody.linearVelocity = rigidbody.transform.forward * startingVelocity;

        wheels.Add(frontLeft);
        wheels.Add(frontRight);
        wheels.Add(backLeft);
        wheels.Add(backRight);

        foreach (Wheel wheel in wheels)
            wheel.Initialize(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        Debug.Log(rigidbody.linearVelocity.magnitude);
    }

    private void FixedUpdate()
    {
        steeringDirection = rigidbody.transform.forward;
        Quaternion steeringOffset = Quaternion.Euler(0, steeringAngle * input.x, 0f);
        steeringDirection = steeringOffset * steeringDirection;
        Quaternion steeringRotation = Quaternion.LookRotation(steeringDirection, rigidbody.transform.up);
        frontLeft.transform.rotation = Quaternion.RotateTowards(frontLeft.transform.rotation, steeringRotation, steeringPerSeconds * Time.deltaTime);
        frontRight.transform.rotation = Quaternion.RotateTowards(frontRight.transform.rotation, steeringRotation, steeringPerSeconds * Time.deltaTime); ;

        foreach (Wheel wheel in wheels)
            wheel.Tick(input);
    }
}

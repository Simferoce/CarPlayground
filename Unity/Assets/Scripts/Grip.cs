using System;
using UnityEngine;

[Serializable]
public class Grip
{
    [SerializeField] private bool enabled;
    [SerializeField] private float grip = 0.5f;

    private Wheel wheel;
    private float sidewayVelocity;
    private Vector3 sidewayDirection;
    private float desiredVelocity;
    private float desiredAcceleration;

    public void Initialize(Wheel wheel)
    {
        this.wheel = wheel;
    }

    public void Tick(RaycastHit hit)
    {
        if (!enabled)
            return;

        Vector3 velocity = wheel.Car.Rigidbody.GetPointVelocity(wheel.transform.position);

        sidewayDirection = wheel.transform.right;
        sidewayVelocity = Vector3.Dot(velocity, sidewayDirection);
        if (sidewayVelocity == 0)
            return;

        desiredVelocity = -sidewayVelocity * grip;
        desiredAcceleration = desiredVelocity / Time.fixedDeltaTime;
        wheel.Car.Rigidbody.AddForceAtPosition(sidewayDirection * desiredAcceleration, wheel.transform.position, ForceMode.Acceleration);
    }
}


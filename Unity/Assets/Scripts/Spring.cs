using System;
using UnityEngine;

[Serializable]
public class Spring
{
    [SerializeField] private bool enabled;
    [SerializeField] private float rest;
    [SerializeField] private float strenght;
    [SerializeField] private float damping;

    private Vector3 up;
    private Wheel wheel;
    private float velocity;
    private float force;
    private float offset;

    public void Initialize(Wheel wheel)
    {
        this.wheel = wheel;
    }

    public void Tick(RaycastHit hit)
    {
        if (!enabled)
            return;

        up = wheel.transform.up;
        Vector3 worldVelocityAtPoint = wheel.Car.Rigidbody.GetPointVelocity(wheel.transform.position);
        velocity = Vector3.Dot(up, worldVelocityAtPoint);
        offset = rest - hit.distance;
        force = (offset * strenght) - (velocity * damping);
        wheel.Car.Rigidbody.AddForceAtPosition(up * force, wheel.transform.position);
    }
}


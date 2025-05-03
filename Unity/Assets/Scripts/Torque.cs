using System;
using UnityEngine;

[Serializable]
public class Torque
{
    [SerializeField] private bool enabled;
    [SerializeField] private AnimationCurve torqueOverVelocity;
    [SerializeField] private float torqueScale = 1f;
    [SerializeField] private float maximumTorqueContributionVelocity;

    private Wheel wheel;
    private Vector3 direction;
    private float forwardSpeed;
    private float normalizedSpeed;
    private float torque;

    public void Initialize(Wheel wheel)
    {
        this.wheel = wheel;
    }

    public void Tick(RaycastHit hit, float input)
    {
        if (!enabled)
            return;

        if (input == 0)
            return;

        direction = wheel.transform.forward;
        forwardSpeed = Vector3.Dot(wheel.Car.transform.forward, wheel.Car.Rigidbody.linearVelocity);
        normalizedSpeed = input * Mathf.Clamp01(Mathf.Abs(forwardSpeed) / maximumTorqueContributionVelocity);
        torque = Mathf.Sign(input) * torqueOverVelocity.Evaluate(Mathf.Abs(normalizedSpeed)) * torqueScale;
        wheel.Car.Rigidbody.AddForceAtPosition(direction * torque, wheel.transform.position);
    }
}


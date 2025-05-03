using System;
using UnityEngine;

[Serializable]
public class Steering
{
    [SerializeField] private float steeringAngle = 45;
    [SerializeField] private float steeringPerSeconds = 90;

    private Car car;
    private Vector3 steeringDirection;

    public void Initialize(Car car)
    {
        this.car = car;
    }

    public void Steer(float amount)
    {
        steeringDirection = car.Rigidbody.transform.forward;
        Quaternion steeringOffset = Quaternion.Euler(0, steeringAngle * amount, 0f);
        steeringDirection = steeringOffset * steeringDirection;
        Quaternion steeringRotation = Quaternion.LookRotation(steeringDirection, car.Rigidbody.transform.up);
        car.FrontLeft.transform.rotation = Quaternion.RotateTowards(car.FrontLeft.transform.rotation, steeringRotation, steeringPerSeconds * Time.deltaTime);
        car.FrontRight.transform.rotation = Quaternion.RotateTowards(car.FrontRight.transform.rotation, steeringRotation, steeringPerSeconds * Time.deltaTime);
    }
}


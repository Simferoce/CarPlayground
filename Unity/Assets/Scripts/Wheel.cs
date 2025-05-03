using UnityEngine;


public class Wheel : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private float radius;
    [SerializeField] private Spring spring;
    [SerializeField] private Grip grip;
    [SerializeField] private Torque torque;

    public Car Car { get => car; set => car = value; }

    private Car car;
    private bool grounded = false;

    public void Initialize(Car car)
    {
        this.car = car;
        spring.Initialize(this);
        grip.Initialize(this);
        torque.Initialize(this);
    }

    public void Tick(Vector2 input)
    {
        grounded = Physics.Raycast(this.transform.position, -this.transform.up, out RaycastHit hit, radius, ground.value);

        if (grounded)
        {
            spring.Tick(hit);
            grip.Tick(hit);
            torque.Tick(hit, input.y);
        }
    }
}


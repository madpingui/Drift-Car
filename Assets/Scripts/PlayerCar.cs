using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour {

    [SerializeField] float turnSpeed = 5;
    [SerializeField] float acceleration = 8;

    Quaternion targetRotation;
    Rigidbody _rigidBody;

    Vector3 lastPosition;
    float _sideSlipAmount = 0;

    public LayerMask layerMask;

    public float SideSlipAmount
    {
        get
        {
            return _sideSlipAmount;
        }
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        SetRotationPoint();
        SetSideSlip();
    }

    private void SetSideSlip()
    {
        Vector3 direction = transform.position - lastPosition;
        Vector3 movement = transform.InverseTransformDirection(direction);
        lastPosition = transform.position;

        _sideSlipAmount = movement.x;
    }

    private void SetRotationPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if(plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(0, rotationAngle, 0);
        }
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            float speed = _rigidBody.velocity.magnitude / 1000;


            float accelerationInput = acceleration * (Input.GetMouseButton(0) ? 1 : Input.GetMouseButton(1) ? -1 : 0) * Time.fixedDeltaTime;
            _rigidBody.AddRelativeForce(Vector3.forward * accelerationInput);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Mathf.Clamp(speed, -1, 1) * Time.fixedDeltaTime);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 1.0f, layerMask);
    }
}

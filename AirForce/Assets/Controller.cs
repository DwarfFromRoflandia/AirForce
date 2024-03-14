using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    private Rigidbody _rb;

    private float horizontalInput;
    private float verticalInput;

    private float forwardSpeed;
    private float forwardSpeedMultiplayer;

    public float SpeedMultiplayer;
    public float horizontalSpeed;
    public float verticalSpeed;

    public float _smoothness;

    private float maxHorizontalRotation;
    private float maxVerticalRotation;

    private float rotationSmoothness;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        forwardSpeed = 0.5f;
        forwardSpeedMultiplayer = 100f;

        SpeedMultiplayer = 1000f;
        horizontalSpeed = 0.2f;
        verticalSpeed = 0.2f;
        _smoothness = 5f;

        maxHorizontalRotation = 0.1f;
        maxVerticalRotation = 0.06f;
        rotationSmoothness = 5f;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) || Input.touches.Length != 0)
        {
            horizontalInput = _joystick.Horizontal;
            verticalInput = _joystick.Vertical;
        }
        else
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }

        HandlePlaneRotation();
    }

    private void FixedUpdate()
    {
        HandlePlaneMovement();
    }

    private void HandlePlaneMovement()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, forwardSpeed * forwardSpeedMultiplayer * Time.deltaTime);

        float xVelocity = horizontalInput * SpeedMultiplayer * horizontalSpeed * Time.deltaTime;
        float yVelocity = -verticalInput * SpeedMultiplayer * verticalSpeed * Time.deltaTime;

        _rb.velocity = Vector3.Lerp(_rb.velocity, new Vector3(xVelocity, yVelocity, _rb.velocity.z), Time.deltaTime * _smoothness);
    }

    private void HandlePlaneRotation()
    {
        float horizontalRotation = horizontalInput * maxHorizontalRotation;
        float verticalRotation = verticalInput * maxVerticalRotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(verticalRotation, transform.rotation.y, horizontalRotation, transform.rotation.w), Time.deltaTime * rotationSmoothness);
    }
}

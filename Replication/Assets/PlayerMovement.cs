using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float acceleration = 20, maxVelocity = 10, rotationSpeed = 10;
    [SerializeField] ForceMode force = ForceMode.Force;
    Rigidbody pRigidbody;
    void Start()
    {
        pRigidbody = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (Input.GetButton("Vertical"))
        {
            pRigidbody.AddRelativeForce(Vector3.forward * Input.GetAxisRaw("Vertical") * acceleration * Time.deltaTime, force);
        }
        if (Input.GetButton("Horizontal"))
        {
            pRigidbody.AddRelativeForce(Vector3.right * Input.GetAxisRaw("Horizontal") * acceleration * Time.deltaTime, force);
        }
        else if (pRigidbody.velocity != Vector3.zero && !Input.GetButton("Vertical"))
        {
            pRigidbody.velocity -= pRigidbody.velocity * Time.deltaTime;
        }
        if (pRigidbody.velocity.sqrMagnitude > maxVelocity * maxVelocity)
        {
            pRigidbody.velocity = pRigidbody.velocity.normalized * maxVelocity;
        }
        transform.Rotate(new Vector3(Input.GetAxisRaw("Mouse Y") * -1, Input.GetAxisRaw("Mouse X"), 0) * rotationSpeed);
    }
}
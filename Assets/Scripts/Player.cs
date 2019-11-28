using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    Rigidbody rb;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        transform.GetChild(0).Rotate(-Input.GetAxis("Mouse Y"), 0, 0);
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        rb.velocity = transform.rotation * new Vector3(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y, Input.GetAxisRaw("Vertical") * speed);
    }
}

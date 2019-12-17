using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jump;
    public float jumpRecharge;
    public float grappleSpeed;
    float timeSincejump;
    Rigidbody rb;
    int isJumping;
    Vector3 grapPos;
    bool isGrappling;
    RaycastHit hit;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        if(Input.GetMouseButtonUp(1)){
            if(isGrappling){
                isGrappling = false;
            }else{
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 50f)){
                    grapPos = hit.point;
                    isGrappling = true;
                }
            }
        }
    }

    void FixedUpdate(){
        timeSincejump += Time.deltaTime;
        transform.GetChild(0).Rotate(-Input.GetAxis("Mouse Y"), 0, 0);
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        if(isGrappling){
            rb.velocity = (transform.rotation * new Vector3(Input.GetAxisRaw("Horizontal") * speed, Mathf.Clamp(rb.velocity.y - (rb.velocity.y * isJumping) + (isJumping * jump), -Mathf.Infinity, 0f), Input.GetAxisRaw("Vertical") * speed) + (grapPos - transform.position).normalized * grappleSpeed);
        }else{
            rb.velocity = transform.rotation * new Vector3(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y - (rb.velocity.y * isJumping) + (isJumping * jump), Input.GetAxisRaw("Vertical") * speed);
        }
        isJumping = 0;
    }

    void OnTriggerStay(){
        if(Input.GetAxis("Jump") > 0 && jumpRecharge <= timeSincejump){
            isJumping = 1;
            timeSincejump = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jump;
    public float jumpRecharge;
    public float rotationSpeed;
    float timeSincejump;
    Rigidbody rb;
    int isJumping;
    RaycastHit hit;
    GameObject pickUp;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        timeSincejump += Time.deltaTime;
        if(Input.GetMouseButtonUp(0)){
            if(pickUp == null){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 4)){
                    if(hit.collider.gameObject.tag == "pickUp"){
                        pickUp = hit.collider.gameObject;
                        pickUp.GetComponent<Rigidbody>().useGravity = false;
                        pickUp.layer = 2;
                    }
                }
            }else{
                pickUp.layer = 0;
                pickUp.GetComponent<Rigidbody>().useGravity = true;
                pickUp = null;
            }
        }
        transform.GetChild(0).Rotate(-Input.GetAxis("Mouse Y") * rotationSpeed, 0, 0);
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);
        if(pickUp != null){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && Vector3.SqrMagnitude(hit.point - ray.origin) < 4){
                pickUp.transform.position = new Vector3(hit.point.x, hit.point.y + pickUp.GetComponent<Pickup>().holdHeightOnGround, hit.point.z);
                pickUp.transform.rotation = Quaternion.identity;
            }else{
                pickUp.transform.position = transform.position + transform.GetChild(0).rotation * Vector3.forward * pickUp.GetComponent<Pickup>().holdDistance + Vector3.up * pickUp.GetComponent<Pickup>().holdUp;
            }
            pickUp.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 4))
            {
                if(hit.collider.gameObject.tag == "Plant")
                {
                    hit.collider.gameObject.GetComponent<Plant>().clicked = true;
                }
            }
        }
    }

    void FixedUpdate(){
        rb.velocity = transform.rotation * new Vector3(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y - (rb.velocity.y * isJumping) + (isJumping * jump), Input.GetAxisRaw("Vertical") * speed);
        isJumping = 0;
    }

    void OnTriggerStay(){
        if(Input.GetAxis("Jump") > 0 && jumpRecharge <= timeSincejump){
            isJumping = 1;
            timeSincejump = 0;
        }
    }
}

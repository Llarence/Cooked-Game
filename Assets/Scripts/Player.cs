﻿using System.Collections;
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
                if (Physics.Raycast(ray, out hit, 100)){
                    if(hit.collider.gameObject.tag == "pickUp"){
                        if(hit.collider.gameObject.GetComponent<Pickup>().grabDistance >= hit.distance){
                            pickUp = hit.collider.gameObject;
                            pickUp.GetComponent<Rigidbody>().useGravity = false;
                            pickUp.layer = 2;
                        }
                    }else if(hit.collider.transform.parent != null){
                        if(hit.collider.transform.parent.gameObject.tag == "pickUp"){
                            if(hit.collider.transform.parent.gameObject.GetComponent<Pickup>().grabDistance >= hit.distance){
                                pickUp = hit.collider.transform.parent.gameObject;
                                pickUp.GetComponent<Rigidbody>().useGravity = false;
                                foreach(Transform child in pickUp.transform){
                                    child.gameObject.layer = 2;
                                }
                                pickUp.layer = 2;
                            }
                        }
                    }
                }
            }else{
                pickUp.layer = 0;
                if(pickUp.transform.childCount > 0){
                    foreach(Transform child in pickUp.transform){
                        child.gameObject.layer = 0;
                    }
                }
                pickUp.GetComponent<Rigidbody>().useGravity = true;
                pickUp = null;
            }
        }
        transform.GetChild(0).Rotate(-Input.GetAxis("Mouse Y") * rotationSpeed, 0, 0);
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);
        if(pickUp != null){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && Vector3.SqrMagnitude(hit.point - ray.origin) < pickUp.GetComponent<Pickup>().holdDistanceGround * pickUp.GetComponent<Pickup>().holdDistanceGround){
                pickUp.transform.position = new Vector3(hit.point.x, hit.point.y + pickUp.GetComponent<Pickup>().holdHeightOnGround, hit.point.z);
                pickUp.transform.rotation = Quaternion.identity;
            }else{
                pickUp.transform.position = transform.GetChild(0).position + transform.GetChild(0).rotation * Vector3.forward * pickUp.GetComponent<Pickup>().holdDistance + Vector3.up * pickUp.GetComponent<Pickup>().holdUp;
            }
            pickUp.GetComponent<Rigidbody>().velocity = Vector3.zero;
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

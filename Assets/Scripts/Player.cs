using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jump;
    public float jumpRecharge;
    public float rotationSpeed;
    public int Gold;
    float timeSincejump;

    public bool InventoryOn;
    public bool MarketOn;

    Rigidbody rb;
    int isJumping;
    RaycastHit hit;
    GameObject pickUp;
    public GameObject openPickup;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        openPickup = pickUp;
        InventoryOn = GameObject.Find("Manager").GetComponent<Inventory>().On;
        MarketOn = GameObject.Find("Market").GetComponent<MarketPlace>().selected;
        timeSincejump += Time.deltaTime;
        if(Input.GetMouseButtonUp(0)){
            if(pickUp == null){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100)){
                    if(hit.collider.gameObject.GetComponent<DataHolder>() != null){
                        if(hit.collider.gameObject.GetComponent<DataHolder>().has<PickUp>()){
                            if(hit.collider.gameObject.GetComponent<DataHolder>().get<PickUp>().grabDistance >= hit.distance){
                                pickUp = hit.collider.gameObject;
                                pickUp.GetComponent<Rigidbody>().useGravity = false;
                                pickUp.layer = 2;
                            }
                        }
                    }
                    
                    if(hit.collider.transform.parent.gameObject.GetComponent<DataHolder>() != null)
                    {
                        if(hit.collider.transform.parent.gameObject.GetComponent<DataHolder>().has<PickUp>()){
                            if(hit.collider.transform.parent.gameObject.GetComponent<DataHolder>().get<PickUp>().grabDistance >= hit.distance){
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
        if (InventoryOn == false && MarketOn == false)
        {
            transform.GetChild(0).Rotate(-Input.GetAxis("Mouse Y") * rotationSpeed, 0, 0);
            transform.Rotate(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);
        }
        if(pickUp != null){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && Vector3.SqrMagnitude(hit.point - ray.origin) < pickUp.GetComponent<DataHolder>().get<PickUp>().holdDistanceGround * pickUp.GetComponent<DataHolder>().get<PickUp>().holdDistanceGround){
                pickUp.transform.position = new Vector3(hit.point.x, hit.point.y + pickUp.GetComponent<DataHolder>().get<PickUp>().holdHeightOnGround, hit.point.z);
                pickUp.transform.rotation = Quaternion.identity;
            }else{
                pickUp.transform.position = transform.GetChild(0).position + transform.GetChild(0).rotation * Vector3.forward * pickUp.GetComponent<DataHolder>().get<PickUp>().holdDistance + Vector3.up * pickUp.GetComponent<DataHolder>().get<PickUp>().holdUp;
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
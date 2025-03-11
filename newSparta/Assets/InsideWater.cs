using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class InsideWater : MonoBehaviour
{
    public float waterDrag = 5f;
    public float yForce = 3f;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            Rigidbody rb = other.GetComponent<Rigidbody>();
            playerController.moveSpeed = 3f;
    
            rb.mass = 100;
            rb.drag = waterDrag;
            rb.useGravity = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            Rigidbody rb = other.GetComponent<Rigidbody>();
            playerController.moveSpeed = 10f;
        
            rb.mass = 20;
            rb.drag = 0;
            rb.useGravity = true;
        }
    }
    IEnumerator UseYForce(Rigidbody rb)
    {
        while(rb != null && !rb.useGravity)
        {
            rb.AddForce(Vector3.up * yForce, ForceMode.Acceleration);
            yield return null;
        }
    }
}

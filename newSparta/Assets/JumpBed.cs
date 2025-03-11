using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class JumpBed : MonoBehaviour
{
    public float jumpPower;
    public Animator anim;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // 기존 Y 속도 초기화
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); // 위로 점프

                anim.SetTrigger("IsForce");

                StartCoroutine(ResetTrigger());
            }

        }
        
    }
    private IEnumerator ResetTrigger()
    {
        yield return new WaitForSeconds(1.5f);
        anim.ResetTrigger("IsForce"); // 트리거 초기화
    }
}

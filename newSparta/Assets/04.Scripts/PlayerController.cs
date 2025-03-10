using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movment")]
    public float moveSpeed;
    private Vector2 curMoveInput;
    private Rigidbody _rigdibody;

    [Header("Look")]
    public Transform cameraContainer;

    public float minXLook;
    public float MaxXLook;
    public float camCurXrot;
    private float lookSensitivity = 1;
    private Vector2 mouseDelta;
    [Header ("Jump")]
    public float jumpForce = 5;
    public LayerMask groundLayerMask;


    private void Awake()
    {
        _rigdibody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void FixedUpdate()
    {
        Move();
    }
    private void LateUpdate()
    {
        CameraLook();
    }
    void Move()
    {
        Vector3 dir = transform.forward * curMoveInput.y + transform.right * curMoveInput.x;
        dir *= moveSpeed;
        dir.y = _rigdibody.velocity.y;

        _rigdibody.velocity = dir;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMoveInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            curMoveInput = Vector2.zero;
        }
    }
    void CameraLook()
    {
        camCurXrot += mouseDelta.y * lookSensitivity;
        camCurXrot = Mathf.Clamp(camCurXrot, minXLook, MaxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXrot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
        Debug.Log("Mouse Delta: " + mouseDelta);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && isGrounded())
        {
            _rigdibody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }
    }
    bool isGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };
        for(int i = 0; i < rays.Length; i++)
        {
            if(Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

}


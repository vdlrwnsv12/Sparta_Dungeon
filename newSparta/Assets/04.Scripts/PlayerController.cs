using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public Vector2 lastMoveInput;  // 마지막 입력을 저장할 변수
    public Vector2 curMoveInput;
    private Rigidbody _rigidbody;
    public bool isOnIce;


    [Header("Look")]
    public Transform cameraContainer;

    public float minXLook;
    public float MaxXLook;
    public float camCurXrot;
    private float lookSensitivity = 1;
    private Vector2 mouseDelta;
    public bool canLook = true;
    public Action inventory;

    [Header("Jump")]
    public float jumpForce = 5;
    public LayerMask groundLayerMask;
    [Header("camera")]
    public Camera mainCam;
    public float currFov;
    public float fovVelocity;

    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currFov = mainCam.fieldOfView;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    void Move()
    {
        if (isOnIce && lastMoveInput != Vector2.zero)
        {
            curMoveInput = lastMoveInput;
        }

        float targetFov = Input.GetKey(KeyCode.LeftShift) ? 76f : 60f;
        currFov = Mathf.SmoothDamp(currFov, targetFov, ref fovVelocity, 0.1f);
        mainCam.fieldOfView = currFov;
        
        Vector3 dir = transform.forward * curMoveInput.y + transform.right * curMoveInput.x;
        dir *= Input.GetKey(KeyCode.LeftShift) ? moveSpeed*1.7f : moveSpeed;
        dir.y = _rigidbody.velocity.y;
        _rigidbody.velocity = dir;

        if (!isOnIce)
        {
            lastMoveInput = Vector2.zero;
        }else
        {
            lastMoveInput = curMoveInput;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMoveInput = context.ReadValue<Vector2>();
            
        }
        else if (context.phase == InputActionPhase.Canceled)
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
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && isGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
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

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}

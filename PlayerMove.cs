using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Set editible Values
    public float speed;
    public float jumpForce;
    public float sensititvity;
    public KeyCode Jump = KeyCode.Space;
    public KeyCode Run = KeyCode.LeftShift;
    public LayerMask groundLayers;

    //Private Values (Camera)
    private float cameraLookLimit = 85;
    private float currentCameraRotation = 0f;

    //Private Values
    private bool Jumping;


    //On-Player Values
    private Rigidbody rb;
    public Camera cam;
    private CapsuleCollider capsule;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        if (Grounded() && Input.GetKeyDown(Jump))
        {
            Debug.Log("is jumping");
            PlayerJump();
        }

        //Press the space bar to apply no locking to the Cursor
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void FixedUpdate()
    {
        Look();
        Move();
    }

    void Look()
    {
        float _MouseX = Input.GetAxisRaw("Mouse X");
        float _MouseY = Input.GetAxisRaw("Mouse Y");

        //rotate on x axis mouse
        Vector3 rotation = new Vector3(0f, _MouseX, 0f) * sensititvity;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            //rotate on y axis mouse
            float camRotation = _MouseY * sensititvity;
            currentCameraRotation -= camRotation;
            currentCameraRotation = Mathf.Clamp(currentCameraRotation, -cameraLookLimit, cameraLookLimit);


            cam.transform.localEulerAngles = new Vector3(currentCameraRotation, 0f, 0f);
        }

    }

    private void Move()
    {

        Vector3 moveForward = transform.forward * speed;
        Vector3 moveRight = transform.right * speed;

        if (Input.GetKey(KeyCode.W))
        {
            rb.MovePosition(rb.position + moveForward * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.MovePosition(rb.position + -moveForward * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.MovePosition(rb.position + -moveRight * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.MovePosition(rb.position + moveRight * Time.deltaTime);
        }


    }

    private bool Grounded()
    {
        return Physics.CheckCapsule(capsule.bounds.center, new Vector3(capsule.bounds.center.x, capsule.bounds.min.y, capsule.bounds.center.z), capsule.radius * .9f, groundLayers);
    }


    private void PlayerJump()
    {

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }


}

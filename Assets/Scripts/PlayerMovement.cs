using EazyCamera;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public float speed;
    public float grounDrag;
   
    [Header("Check Ground")]
    public float height;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Ground Check
        grounded = Physics.Raycast(transform.position, Vector3.down, height * 0.5f + 0.3f,whatIsGround);
        GetInput();
        SpeedControl();
        //Handle Drag
        if (grounded)
            rb.drag = grounDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = orientation.forward * verticalInput + orientation.right* horizontalInput;
        rb.AddForce(dir.normalized * speed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        //Limit Velocity if needed
        if(flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized* speed;
            rb.velocity = new Vector3(limitedVel.x,rb.velocity.y, limitedVel.z);    
        }
    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
}

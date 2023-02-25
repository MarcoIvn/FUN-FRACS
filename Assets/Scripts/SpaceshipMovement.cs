using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 6;
    public float turnspeed = 10;
    private Vector2 input;
    private float angle;
    private Quaternion rotation;
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    private void Update()
    {
        GetInput();
        if(Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) return;
        GetDirection();
        Rotate();
        Move();
    }

    void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical"); 
    }

    void GetDirection()
    {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }

    void Rotate()
    {
        rotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation,rotation,turnspeed*Time.deltaTime);
    }

    void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}

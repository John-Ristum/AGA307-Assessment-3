using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Transform target;
    public float speed = 10f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    public bool isGrounded;

    //For finding nearest object
    public GameObject[] AllObjects;
    public GameObject NearestOBJ;
    float distance;
    float nearestDistance = 1000000;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Checks if we are touching the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        //Get the input from the player
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        //Locking on enemy
        if (Input.GetButtonDown("R-Target"))
            LockEnemyOn();

        if (Input.GetButtonUp("R-Target"))
            LockEnemyOff();

        //Move the player
        Vector3 direction = new Vector3(x, 0f, z).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            //transform.rotation = Quaternion.Euler(0f, angle, 0f);
            transform.LookAt(new Vector3(target.position.z, transform.position.y, target.position.z));

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        //Physics
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void LockEnemyOn()
    {
        //Find nearest object
        AllObjects = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < AllObjects.Length; i++)
        {
            distance = Vector3.Distance(this.transform.position, AllObjects[i].transform.position);

            if (distance < nearestDistance)
            {
                Debug.Log("TargetOn");
                NearestOBJ = AllObjects[i];
                target = NearestOBJ.transform;
                nearestDistance = distance;
            }
        }
    }

    void LockEnemyOff()
    {
        target = NearestOBJ.transform;
        nearestDistance = 1000000;
        AllObjects = null;
        Debug.Log("TargetO");
        NearestOBJ = null;
    }
}

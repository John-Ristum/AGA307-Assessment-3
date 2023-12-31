using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMovementState { Idle, Running, LockedOn, Damage }
public enum PlayerActionState { Idle, QuickStepping, AttackLight, AttackHeavy }

public class PlayerMovement : Singleton<PlayerMovement>
{
    
    public PlayerMovementState movementState;         //Reference to player's movement state
    public PlayerActionState actionState;             //Reference to player's action state

    //Movement variables
    public CharacterController controller;
    public Transform cam;
    public Transform target;
    public float speed = 10f;
    public float runSpeed = 10f;
    public float walkSpeed = 5f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Vector3 moveDir;

    //Gravity variables
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private Vector3 velocity;
    public bool isGrounded;

    //For finding nearest enemy
    public GameObject[] AllObjects;
    public GameObject NearestOBJ;
    float distance;
    float nearestDistance = 1000000;

    public Animator anim;

    public GameObject targetEnemy;

    //temp
    public bool isQuickStep;
    float angle = 0;
    public Vector3 direction = new Vector3(1f, 0f, 1f);

    private void Start()
    {
        //Get character controller component
        controller = GetComponent<CharacterController>();
        //Sets speed to default speed
        speed = runSpeed;
        //Set player state to Idle
        movementState = PlayerMovementState.Idle;
        //Gets animator component from the model
        anim = GetComponentInChildren<Animator>();
        //Gives the player a default move direction so that QuickStepping works without having to move first
        moveDir = Quaternion.Euler(0f, 0.1f, 0f) * Vector3.forward;
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
        direction = new Vector3(x, 0f, z).normalized;

        if (direction.magnitude >= 0.1f && actionState == PlayerActionState.Idle)
        {
            if (_PLAYER.movementState == PlayerMovementState.Damage)
                return;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            if (actionState == PlayerActionState.QuickStepping)
                return;

            if (movementState != PlayerMovementState.LockedOn)
            {
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                movementState = PlayerMovementState.Running;
                anim.SetBool("Running", true);
                anim.SetBool("LockedOn", false);
            }
            else
            {
                transform.LookAt(new Vector3(target.localPosition.x, transform.position.y, target.localPosition.z));
                anim.SetBool("LockedOn", true);
                anim.SetBool("Running", false);
            }


            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else if (movementState != PlayerMovementState.LockedOn && movementState != PlayerMovementState.Damage)
        {
            movementState = PlayerMovementState.Idle;
            anim.SetBool("Running", false);
            anim.SetBool("LockedOn", false);
        }

        //Physics
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void LockEnemyOn()
    {
        movementState = PlayerMovementState.LockedOn;
        speed = walkSpeed;
        //Find nearest enemy
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
        movementState = PlayerMovementState.Idle;
        speed = runSpeed;
        
        target = null;
        nearestDistance = 1000000;
        AllObjects = null;
        Debug.Log("TargetOff");
        NearestOBJ = null;
    }

    public void Hit()
    {
        StopAllCoroutines();
        movementState = PlayerMovementState.Damage;
        transform.LookAt(new Vector3(_PLAYER.transform.localPosition.x, transform.position.y, _PLAYER.transform.localPosition.z));
        anim.SetTrigger("dmgL");
        _PLAYATK.canCombo = true;
    }
}

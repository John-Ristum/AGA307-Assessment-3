using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickStep : GameBehaviour
{
    /*[Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private CharacterController controller;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    [Header("Cooldown")]
    public float dashCoolDown;
    private float dashCdTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("QuickStep"))
            Dash();
    }

    private void Dash()
    {
        Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpwardForce;

        //controller.Move(Vector3.forward * 100 * Time.deltaTime);

        /rb.AddForce(forceToApply, ForceMode.Impulse);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private void ResetDash()
    {

    }*/

    public float dashSpeed = 20f;
    public float dashTime = 0.25f;
    float startTime;

    public GameObject playerModel;

    private void Update()
    {
        if (Input.GetButtonDown("QuickStep"))
            StartCoroutine(QuickStepCo());

        //if (Time.time >= startTime + dashTime)
            //_PLAYER.playerState = PlayerState.Idle;
    }

    IEnumerator QuickStepCo()
    {
        if (_PLAYER.actionState == PlayerActionState.QuickStepping)
            yield break;

        _PLAYER.actionState = PlayerActionState.QuickStepping;

        _PLAYER.anim.SetTrigger("QuickStep");

        Vector3 dashDir;
        if (_PLAYER.direction.magnitude >= 0.1f)
            dashDir = _PLAYER.moveDir;
        else
            dashDir = _PLAYER.moveDir * -1;


        startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            _PLAYER.controller.Move(dashDir * dashSpeed * Time.deltaTime);

            yield return null;
        }


        playerModel.transform.position = new Vector3(transform.position.x, playerModel.transform.position.y, transform.position.z);
        yield return new WaitForSeconds(0.5f);

        _PLAYER.actionState = PlayerActionState.Idle;
    }
}

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

    private void Update()
    {
        if (Input.GetButtonDown("QuickStep"))
            StartCoroutine(Dash());

        if (Time.time >= startTime + dashTime)
            _PLAYER.isQuickStep = false;
    }

    IEnumerator Dash()
    {
        _PLAYER.isQuickStep = true;
        startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            if(_PLAYER.direction.magnitude >= 0.1f)
                _PLAYER.controller.Move(_PLAYER.moveDir * dashSpeed * Time.deltaTime);
            else
                _PLAYER.controller.Move((_PLAYER.moveDir * -1) * dashSpeed * Time.deltaTime);

            yield return null;
        }
    }
}

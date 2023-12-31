using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationTestState { Movement, Attacks, Finishers, Damage}
public class AnimationTest : MonoBehaviour
{

    public AnimationTestState animState;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //State Keys
        if (Input.GetKeyDown("f1"))
            animState = AnimationTestState.Movement;
        if (Input.GetKeyDown("f2"))
            animState = AnimationTestState.Attacks;
        if (Input.GetKeyDown("f3"))
            animState = AnimationTestState.Finishers;
        if (Input.GetKeyDown("f4"))
            animState = AnimationTestState.Damage;

        //Animation Keys
        if (Input.GetKeyDown("1"))
        {
            if (animState == AnimationTestState.Movement)
                anim.SetTrigger("run");
            if (animState == AnimationTestState.Attacks)
                anim.SetTrigger("atk1");
            if (animState == AnimationTestState.Finishers)
                anim.SetTrigger("fin0");
            if (animState == AnimationTestState.Damage)
                anim.SetTrigger("dmg1");
        }
            
        if (Input.GetKeyDown("2"))
        {
            if (animState == AnimationTestState.Movement)
                anim.SetTrigger("walk");
            if (animState == AnimationTestState.Attacks)
                anim.SetTrigger("atk2");
            if (animState == AnimationTestState.Finishers)
                anim.SetTrigger("fin1");
            if (animState == AnimationTestState.Damage)
                anim.SetTrigger("dmg2");
        }
            
        if (Input.GetKeyDown("3"))
        {
            if (animState == AnimationTestState.Movement)
                anim.SetTrigger("quickstep");
            if (animState == AnimationTestState.Attacks)
                anim.SetTrigger("atk3");
            if (animState == AnimationTestState.Finishers)
                anim.SetTrigger("fin1W");
            if (animState == AnimationTestState.Damage)
                anim.SetTrigger("dmg3");
        }

        if (Input.GetKeyDown("4"))
        {
            if (animState == AnimationTestState.Attacks)
                anim.SetTrigger("atk4");
            if (animState == AnimationTestState.Finishers)
                anim.SetTrigger("fin2");
            if (animState == AnimationTestState.Damage)
                anim.SetTrigger("dmg4");
        }

        if (Input.GetKeyDown("5"))
        {
            if (animState == AnimationTestState.Finishers)
                anim.SetTrigger("fin2W");
            if (animState == AnimationTestState.Damage)
                anim.SetTrigger("dmg5");
        }

        if (Input.GetKeyDown("6"))
        {
            if (animState == AnimationTestState.Finishers)
                anim.SetTrigger("fin3");
        }

        if (Input.GetKeyDown("7"))
        {
            if (animState == AnimationTestState.Finishers)
                anim.SetTrigger("fin3W");
        }

        if (Input.GetKeyDown("8"))
        {
            if (animState == AnimationTestState.Finishers)
                anim.SetTrigger("fin4");
        }

        if (Input.GetKeyDown("9"))
        {
            if (animState == AnimationTestState.Finishers)
                anim.SetTrigger("fin4W");
        }


        if (Input.GetKeyDown("0"))
            anim.SetTrigger("idle");
    }
}

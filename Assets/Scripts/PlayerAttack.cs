using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Singleton<PlayerAttack>
{
    public int atkNumber = 1;
    public bool canCombo;

    // Start is called before the first frame update
    void Start()
    {
        canCombo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Attack();
        //if (Input.GetButtonDown("Fire2"))
        //    atkNumber = 1;
    }

    void Attack()
    {
        if (!canCombo)
            return;

        if (_PLAYER.movementState == PlayerMovementState.Damage)
            return;

        if (_PLAYER.targetEnemy != null)
            _PLAYER.transform.LookAt(new Vector3(_PLAYER.targetEnemy.transform.localPosition.x, _PLAYER.transform.position.y, _PLAYER.targetEnemy.transform.localPosition.z));

        _PLAYER.anim.SetTrigger("atk" + atkNumber);

        //canCombo = false;
        if (atkNumber > 4)
            atkNumber = 1;
    }
}

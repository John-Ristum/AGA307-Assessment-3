using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitboxUser { Player, Enemy}

public class PlayerHitbox : GameBehaviour
{
    public HitboxUser user;

    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && user == HitboxUser.Player)
        {
            if (other.gameObject.GetComponent<Enemy>() != null)
                other.gameObject.GetComponent<Enemy>().Hit(damage);
        }

        if (other.CompareTag("Player") && user == HitboxUser.Enemy)
        {
            if (other.gameObject.GetComponent<PlayerMovement>() != null)
                other.gameObject.GetComponent<PlayerMovement>().Hit();
        }
    }
}

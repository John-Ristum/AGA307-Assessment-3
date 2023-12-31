using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    protected static PlayerMovement _PLAYER { get { return PlayerMovement.INSTANCE; } }
    protected static PlayerAttack _PLAYATK { get { return PlayerAttack.INSTANCE; } }
}

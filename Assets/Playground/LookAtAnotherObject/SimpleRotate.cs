using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    public float RotationAmount = 2f;
    public int TicksPerSecond = 60;
    public bool Pause = false;

    void Start()
    {
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        WaitForSeconds Wait = new WaitForSeconds(1f / TicksPerSecond);

        while (true)
        {
            if (!Pause)
            {
                transform.Rotate(Vector3.up * RotationAmount);
            }

            yield return Wait;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private Vector3 top;
    [SerializeField] private Vector3 bot;

    private bool check = false;

    public void LiftMove()
    {
        if (transform.position.y < top.y && check == false)
        {
            transform.position += 5 * Time.deltaTime * Vector3.up;
            if (transform.position.y > top.y)
            {
                check = true;
            }
        }
        if (transform.position.y > bot.y && check == true)
        {
            transform.position += 5 * Time.deltaTime * Vector3.down;
            if (transform.position.y < bot.y)
            {
                check = false;
            }
        }
    }
    void Update()
    {
        LiftMove();
    }
}

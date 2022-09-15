using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProperties : MonoBehaviour
{
    [SerializeField] protected float _hp;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _runForce;
    [SerializeField] protected AudioSource _cameraAudioSource;

    protected int _dir = 1;
    protected Rigidbody2D _rb;

    public void TakeDamage(float damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            gameObject.SetActive(false);
        }

    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private int _impulse;

    private int _dir;
    private readonly float _delay = 1.5F;
    private readonly int _damage = 1;

    private Rigidbody2D _rb;


    public void Init(int dir)
    {
        _dir = dir;

        _rb = GetComponent<Rigidbody2D>();

        _rb.AddForce(_dir * _impulse * Vector3.right, ForceMode2D.Impulse);

        Destroy(gameObject, _delay);

    }

   
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
            collision.gameObject.GetComponent<Enemy>().Angry();
            _rb.sharedMaterial.bounciness = Random.Range(0.04F, 0.08F);
        }

        _rb.gravityScale = 1;
        gameObject.GetComponent<Collider2D>().isTrigger = true;



        
    }

    


}

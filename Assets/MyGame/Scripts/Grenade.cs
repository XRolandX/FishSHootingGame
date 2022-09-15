using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private int _throw;
    [SerializeField] private int _shockWave;
    [SerializeField] private float _explosionRadius;

    private AudioSource _audioSource;

    private int _dir;
    private Rigidbody2D _rb;

    private Vector3 distance;
    private Enemy _enemy;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        gameObject.SetActive(false);
        
    }

    public void Init(int dir)
    {
        _dir = dir;
        _rb = GetComponent<Rigidbody2D>();


        if (_dir == 1)
        {
            _rb.AddForce(Vector3.Lerp(Vector3.up, Vector3.right, 0.5F) * _throw, ForceMode2D.Force);
            transform.rotation = Quaternion.Euler(0, 0, -30);
        }
        else
        {
            _rb.AddForce(Vector3.Lerp(Vector3.up, Vector3.left, 0.5F) * _throw, ForceMode2D.Force);
            transform.rotation = Quaternion.Euler(0, 0, 30);
        }

        Invoke(nameof(Explosion), 2f);
    }


    private void Explosion()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        _audioSource.Play();

        Collider2D[] enemy = Physics2D.OverlapCircleAll(transform.position, _explosionRadius, 1 << LayerMask.NameToLayer("Enemy"));

        foreach (Collider2D en in enemy)
        {
            distance = en.transform.position - gameObject.transform.position;
            Vector3 force = distance.normalized * _shockWave;
            _enemy = en.gameObject.GetComponent<Enemy>();
            _enemy.Angry();

            if (gameObject.transform.position.x < en.transform.position.x)
            {
                en.gameObject.GetComponent<Rigidbody2D>().AddForce(force / distance.x, ForceMode2D.Impulse);
            }
            else
            {
                en.gameObject.GetComponent<Rigidbody2D>().AddForce(force / -distance.x, ForceMode2D.Impulse); // force depend with distance to grenade
            }
            _ = StartCoroutine(nameof(Wait));
        }

        Invoke(nameof(SetActiveFalse), 0.25f);

    }


    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1F);
        _enemy.TakeDamage(20F / Mathf.Abs(distance.x));
    }
    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    

}

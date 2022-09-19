using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseProperties
{
    [SerializeField] Vector3 _leftPoint;
    [SerializeField] Vector3 _rightPoint;
    [SerializeField] private Sprite _goLeftSprite;
    [SerializeField] private Sprite _goRightSprite;
    [SerializeField] private Transform _hero;
    [SerializeField] private float _forceOfHit = 5F;

    [SerializeField] private AudioSource _audioSource;
    public AudioClip Agressive;

    private bool _angry;

    [SerializeField] private float _distanceToBeAngry;
    private readonly float _distanceToEndSearchArea = 0.3F;
    public Vector3 _startPosition;
    private void Awake()
    {
        _angry = false;
        _startPosition = transform.position;
    }
    public void Angry()
    {
        if(_angry == false)
        {
            _cameraAudioSource.Stop();
            _cameraAudioSource.PlayOneShot(Agressive);
        }
        _angry = true;
    }
    public void Restart()
    {
        if(gameObject.activeSelf != false)
        {
            transform.position = _startPosition;
            _hp = 4;
        }
        else
        {
            transform.position = _startPosition;
            _hp = 4;
            gameObject.SetActive(true);

        }
        _angry = false;
        
    }

    public void PersecutionMode()
    {
        if (_hero.position.x > transform.position.x)
        {
            _dir = 1;
            GetComponent<SpriteRenderer>().sprite = _goRightSprite;
            transform.position += _runForce * Time.deltaTime * Vector3.right / 2;
        }
        else if (_hero.position.x < transform.position.x)
        {
            _dir = -1;
            GetComponent<SpriteRenderer>().sprite = _goLeftSprite;
            transform.position += _runForce * Time.deltaTime * Vector3.left / 2;
        }
    }

    public void WatchMode()
    {
        if (transform.position.x < _leftPoint.x)
        {
            GetComponent<SpriteRenderer>().sprite = _goRightSprite;
            _dir = -1;
            transform.position += _runForce * Time.deltaTime * Vector3.right;
        }

        if (_dir == 1 && Mathf.Abs(transform.position.x - _rightPoint.x) < _distanceToEndSearchArea)
        {
            GetComponent<SpriteRenderer>().sprite = _goLeftSprite;
            _dir = -1;
            return;
        }
        else if (_dir == -1 && Mathf.Abs(transform.position.x - _leftPoint.x) < _distanceToEndSearchArea)
        {
            GetComponent<SpriteRenderer>().sprite = _goRightSprite;
            _dir = 1;
            return;
        }


        if (_dir == 1 && transform.position.x < _rightPoint.x)
        {
            transform.position += _runForce * Time.deltaTime * Vector3.right;
        }
        else if (_dir == -1 && transform.position.x > _leftPoint.x)
        {
            transform.position += _runForce * Time.deltaTime * Vector3.left;
        }
    }

    private void Update()
    {
        
        if(Vector2.Distance(_hero.position, transform.position) <= _distanceToBeAngry || _angry == true)
        {

            PersecutionMode(); // if distance to the player is lower or equal the distaceToBeAngry the enemy turns on Persecution Mode

        }
        else if(Vector2.Distance(_hero.position, transform.position) >= _distanceToBeAngry)
        {

            WatchMode(); // if distance to the player is bigger than the distanceToBeAngry the enemy remains in Watch Mode

        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(_dir == -1)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.Lerp(Vector3.up, Vector3.left, 0.5F) * _forceOfHit, ForceMode2D.Impulse);

            }else if(_dir == 1)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.Lerp(Vector3.up, Vector3.right, 0.5F) * _forceOfHit, ForceMode2D.Impulse);

            }
            collision.gameObject.GetComponent<HeroMove>().TakeDamage(1f);

        }
    }

}

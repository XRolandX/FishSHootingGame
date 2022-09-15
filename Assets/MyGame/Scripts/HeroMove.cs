using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HeroMove : BaseProperties
{

    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _grenade;
    [SerializeField] private Transform _bulletPosition;
    [SerializeField] private Transform _grenadePosition;
    [SerializeField] private Transform _newLevel;
    [SerializeField] private float _jumpForce;

    [SerializeField] private AudioListener _audioListener;
    [SerializeField] private AudioClip _jump1;
    [SerializeField] private AudioClip _jump2;
    [SerializeField] private AudioClip _shot;
    [SerializeField] private AudioClip _stepOne;
    [SerializeField] private AudioClip _stepTwo;

    [SerializeField] private Button _up;
    [SerializeField] private Button _left;
    [SerializeField] private Button _right;
    [SerializeField] private Button _down;

    private Animator _animator;
    private AudioSource _audioSource;

    private bool shootCheck = false;
    private bool _groundChecker = true;
    private int _grenadeCount = 2;


    public new void TakeDamage(float damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            _animator.SetBool("isDeath", true);
            _audioSource.mute = true;
            transform.GetChild(0).gameObject.SetActive(false);

            _audioListener.enabled = false;
        }

    }
    public void Jump()
    {
        if (_groundChecker == true)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode2D.Force);
            _audioSource.PlayOneShot(_jump1);
            _animator.SetBool("isJump", true);
            _animator.SetBool("isRun", false);
            _animator.SetBool("isFly", true);
        }
            
    }
    public void GoLeft()
    {
        _animator.SetBool("isRun", true);
        _rb.AddForce(Vector3.left * _runForce, ForceMode2D.Impulse);


        if (_dir == 1)
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.right, Vector3.left);
        }
        //else if (Input.GetKeyDown(KeyCode.W) && _groundChecker == true)
        //{
        //    Jump();
        //}
        //else if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Fire();
        //}

        _dir = -1;
    }
    public void GoRight()
    {
            _animator.SetBool("isRun", true);
            _rb.AddForce(Vector3.right * _runForce, ForceMode2D.Impulse);


            if (_dir == -1)
            {
                transform.rotation = Quaternion.FromToRotation(Vector3.zero, Vector3.zero);
            }
        
        
        //else if (Input.GetKeyDown(KeyCode.W) && _groundChecker == true)
        //{
        //    Jump();
        //}
        //else if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Fire();
        //}


        _dir = 1;
    }
    
    public int GetGrenadeCount()
    {
        return _grenadeCount;
    }

    public int GetDir()
    {
        return _dir;
    }
    public float GetHP()
    {
        if(_hp == 0)
        {
            _animator.SetBool("isDeath", true);
        }
        return _hp;
    }
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.D))
        {
            GoRight();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            GoLeft();
        }
        else if (Input.GetKeyDown(KeyCode.W) && _groundChecker == true)
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Fire();
        }
        else if (Input.GetKeyUp(KeyCode.G) && _grenade.activeSelf == false && _grenadeCount != 0)
        {
            HandGrenade();
        }
        else
        {
            _animator.SetBool("isRun", false);
        }

        //if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved || Input.GetMouseButtonDown(0))
        //{
        //}
       
    }

    private void HandGrenade()
    {
        _grenadeCount--;

        _grenade.transform.position = _grenadePosition.position;
        _grenade.transform.rotation = Quaternion.identity;

        _grenade.SetActive(true);
        _grenade.GetComponent<Grenade>().Init(_dir);
        
    }
    private void Fire()
    {
        if(shootCheck == false)
        {

            shootCheck = true;

            _audioSource.PlayOneShot(_shot);

            var bullet = Instantiate(_bullet, _bulletPosition.position, Quaternion.identity);

            if (_dir == -1)
            {
                bullet.GetComponent<Bullet>().transform.rotation = Quaternion.FromToRotation(Vector3.left, Vector3.right);
            }

            bullet.GetComponent<Bullet>().Init(_dir);

            StartCoroutine(nameof(ShootDelay));
        }
    }
    
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(1f);
        shootCheck = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            _audioSource.PlayOneShot(_jump2);
            _groundChecker = true;
            _animator.SetBool("isJump", false);
            _animator.SetBool("isFly", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            _groundChecker = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Exit"))
        {
            transform.position = _newLevel.position;
        }
    }

    public void StepOne()
    {
        _audioSource.PlayOneShot(_stepOne);
    }
    public void StepTwo()
    {
        _audioSource.PlayOneShot(_stepTwo);
    }


}

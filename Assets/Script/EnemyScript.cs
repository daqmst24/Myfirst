using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private AudioSource _music;
    private float _speed = 20;
    [SerializeField]
    private AudioClip _deadMusic;
    private PlayerCar _player;
    private Animator _anim;
    private Transform _playerCar;
    private int _sayi;
    private void Start()
    {
        _music = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCar>();
        _playerCar = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        _sayi = Random.Range(500, 900);
        transform.position += Vector3.forward * _speed * Time.deltaTime;
        CarController();
        PlayerWait();
    }


    private void PlayerWait()
    {

        if (_playerCar.position.z < transform.position.z)
        {
            _speed = 10;
        }
        else
        {
            _speed = 20;
        }


    }
    public void DeadEffect()
    {

        _music.PlayOneShot(_deadMusic);
        transform.position += new Vector3(0, 0, _playerCar.transform.position.z - transform.position.z + _sayi);

    }

    private void CarController()
    {


        if (_playerCar.transform.position.z > transform.position.z + 30)
        {
            transform.position += new Vector3(0, 0, _playerCar.transform.position.z - transform.position.z + _sayi);
        }


    }

    private void BugFix()
    {
        transform.position += new Vector3(0, 0, _playerCar.transform.position.z - transform.position.z + _sayi);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DeadEffect();
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CarCollider")
        {
            BugFix();
        }
    }

}

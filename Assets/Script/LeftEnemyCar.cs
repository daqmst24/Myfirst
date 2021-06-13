using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftEnemyCar : MonoBehaviour
{
    private Animator _anim;
    public float _Speed = 20;
    private AudioSource _music;
    [SerializeField]
    private AudioClip _deadMusic;
    private Transform _playerCar;

    [SerializeField]
    private GameObject _trapGame;

    private Transform _trapTransform;

    private int sayi;


    private void Start()
    {

        _trapTransform = GameObject.FindGameObjectWithTag("Trap").GetComponent<Transform>();
        _music = GetComponent<AudioSource>();
        _playerCar = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        _anim = GetComponent<Animator>();



    }
    private void Update()
    {
        sayi = Random.Range(500, 900);
        TrapCar();
        Movement();
        CarController();
    }

  
    private void Movement()
    {
        transform.position += -Vector3.forward * _Speed * Time.deltaTime;
    }



    public void EnemyDestroy()
    {
        _trapGame.SetActive(false);
        _Speed = 20;
        _music.PlayOneShot(_deadMusic);
        transform.position += new Vector3(0, 0, _playerCar.transform.position.z - transform.position.z + sayi);
    }
    private void TrapCar()
    {


        if (_trapTransform.position.z>transform.position.z)
        {
            _Speed = 20;
        }



    }
    private void CarController()
    {

        if (_playerCar.transform.position.z > transform.position.z + 30)
        {
         
            _trapGame.SetActive(false);
            _Speed = 20;
            Invoke("SpawnCar",3f);

        }
    }
    private void SpawnCar()
    {
        transform.position += new Vector3(0, 0, _playerCar.transform.position.z - transform.position.z + sayi);
    }

    private void BugFix()
    {
        _Speed = 20;
        Invoke("SpawnCar", 1f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EnemyDestroy();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trap")
        {
            BugFix();
        }
        if (  other.tag == "TrapWait")
        {

            _trapGame.SetActive(true);
            _Speed = 0;
            
        }
        if (other.tag == "Wait")
        {
          
                _trapGame.SetActive(true);
        
            _Speed = 0;

        }
    }
}

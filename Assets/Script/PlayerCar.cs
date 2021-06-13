using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCar : MonoBehaviour
{
    private Rigidbody _rg;
    private Animator _anim;
    /*müzik ayarları*/
    private AudioSource _music;
    [SerializeField]
    private AudioSource _settingsMusic;
    [SerializeField]
    private AudioClip _speedMusic;
    [SerializeField]
    private AudioClip _scoreVolume;
    [SerializeField]
    private AudioClip _finishMusic;
    private bool _cal = false;
    private bool _idleAnim = false;
    [SerializeField]
    private GameObject _gazEffect;
    /*camera yakınlaşma */
    [SerializeField]
    private Animator _animCamera;
    /*Zaman işlemleri*/
    [SerializeField]
    private float _startTimeBtwShots;
    private float _timeBtwShots;
    /*müzik ve animasyon */
    private bool _tracking = false;
    /*farklı class tanımı*/
    private EnemyScript _rightEnemy;
    public float _Speed = 20;
    public bool _EnemyCarDead = false;
    /*camera bakış açıları*/
    [SerializeField]
    private GameObject _deadCamera;
    [SerializeField]
    private GameObject _frontCamera;
    [SerializeField]
    private GameObject _inCamera;
    [SerializeField]
    private GameObject _normalCamera;
    /*Dead işlemleri*/
    [SerializeField]
    private GameObject _pause;
    [SerializeField]
    private GameObject _cameraSettings;
    [SerializeField]
    private GameObject _deadMenu;
    /*Score sistem*/
    private int i = 0;
    [SerializeField]
    private Text _scoreText;
    private int _score;
    [SerializeField]
    private GameObject _score100;
    private bool _leftBox = false;



    void Start()
    {

       
        _rightEnemy = GameObject.FindGameObjectWithTag("EnemyCar").GetComponent<EnemyScript>();
        _anim = GetComponent<Animator>();
        _rg = GetComponent<Rigidbody>();
        _music = GetComponent<AudioSource>();
        _timeBtwShots = _startTimeBtwShots;
    }


    void Update()
    {

        SettingMinituePluse();
        CarMovement();
        ScoreSystem();

    }

    private void ScoreSystem()
    {

        if (Time.timeScale == 1 && i == 0)
        {

            _score++;
        }
        else
        {

            i++;
            if (Time.timeScale == 1)
            {
                i = 0;
            }
        }
        if (PlayerPrefs.GetInt("BestScore") < _score)
        {
            PlayerPrefs.SetInt("BestScore", _score);
        }

        _scoreText.text = "Score: " + _score.ToString("0");


    }
    private void ScorePoint()
    {

        _music.PlayOneShot(_scoreVolume);
        _score += 100;
        _score100.SetActive(true);
        Invoke("ScoreAktif", 0.5f);



    }
    private void ScoreAktif()
    {
        _score100.SetActive(false);

    }
    private void AnimMethot(int i)
    {
        if (i == 0)
        {
            _gazEffect.SetActive(false);
            _anim.SetBool("right", false);
            _anim.SetBool("left", false);
        }

        if (i == 1)
        {
            _gazEffect.SetActive(true);
            _rg.velocity = -Vector3.left * 10;
            _anim.SetBool("right", true);
            _anim.SetBool("left", false);

        }
        if (i == 2)
        {
            _gazEffect.SetActive(false);
            StartCoroutine(StartCar());

            if (!_idleAnim)
            {
                _anim.SetBool("right", false);
                _anim.SetBool("left", true);
            }

        }
    }

    IEnumerator StartCar()
    {
        _rg.velocity = Vector3.left * 10;

        yield return new WaitForSeconds(0.5f);
        transform.position += Vector3.forward * _Speed * Time.deltaTime;

    }

    private void CarMovement()
    {





        transform.position += Vector3.forward * _Speed * Time.deltaTime;
        if (Input.GetMouseButton(0)&&Time.timeScale==1)
        {


            SettingMinitueNegative();

            _cal = true;
            int i = 2;
            AnimMethot(i);
            CameraAnim(i);

        }
        else if (Input.GetMouseButtonUp(0) && Time.timeScale == 1)
        {

            _tracking = true;
            if (_tracking)
            {


                _cal = false;
                int i = 1;
                AnimMethot(i);
                CameraAnim(i);

            }
        }

    }
    private void SettingMinitueNegative()
    {
        if (_Speed < 25)
        {


            if (_timeBtwShots <= 0)
            {

                _Speed++;
                _timeBtwShots = _startTimeBtwShots;
            }
            else
            {
                _timeBtwShots -= Time.deltaTime;
            }

        }
    }
    private void SettingMinituePluse()
    {
        if (!_cal)
        {

            if (_Speed > 20)
            {


                if (_timeBtwShots <= 0)
                {

                    _Speed--;
                    _timeBtwShots = _startTimeBtwShots;
                }
                else
                {
                    _timeBtwShots -= Time.deltaTime;
                }

            }

        }
    }

    private void MusicSetings()
    {

        if (_cal)
        {

            _music.Stop();

            _music.PlayOneShot(_speedMusic);
            _music.loop = true;

            _speedMusic.LoadAudioData();


        }
        if (!_cal)
        {
            _music.Stop();
            _music.Play();
        }

        if (Time.timeScale == 0)
        {
            
            _music.Stop();
            _music.PlayOneShot(_finishMusic);

        }
    }
    private void PlayerCarDeadMenu()
    {
        _settingsMusic.Stop();
        _cameraSettings.SetActive(false);
        _deadMenu.SetActive(true);
        _pause.SetActive(false);
        _music.Stop();
        _music.PlayOneShot(_finishMusic);
        Time.timeScale = 0;
        _normalCamera.SetActive(false);
        _frontCamera.SetActive(false);
        _inCamera.SetActive(false);
        _deadCamera.SetActive(true);

    }

    private void CameraAnim(int i)
    {

        if (i == 0)
        {

            _animCamera.SetBool("speed", false);
            _animCamera.SetBool("aspeed", false);
        }
        else if (i == 1)
        {
            _animCamera.SetBool("speed", false);
            _animCamera.SetBool("aspeed", true);
        }
        else if (i == 2)
        {

            _animCamera.SetBool("speed", true);
            _animCamera.SetBool("aspeed", false);
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trap")
        {
            PlayerCarDeadMenu();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        MusicSetings();
        if (collision.collider.tag == "RightBox")
        {
            _idleAnim = false;
            _tracking = false;
            _leftBox = false;
            int i = 0;
            AnimMethot(i);
            if (!_leftBox)
            {
                CameraAnim(i);
            }

        }
        if (collision.collider.tag == "LeftBox")
        {
            _leftBox = true;
            _anim.SetBool("right", false);
            _anim.SetBool("left", false);
            _idleAnim = true;
        }

        if (collision.collider.tag == "LeftCar" || collision.collider.tag == "EnemyCar")
        {
            PlayerCarDeadMenu();
        }
        if (collision.collider.tag == "DeadCar" || collision.collider.tag == "LeftDead")
        {
            ScorePoint();
            if (collision.collider.tag == "DeadCar")
            {
                _rg.velocity = -Vector3.left * 10;
            }
        }
    }


}





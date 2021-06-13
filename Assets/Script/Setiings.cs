using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setiings : MonoBehaviour
{
    [SerializeField]
    private GameObject _resumePanel;
    [SerializeField]
    private GameObject _Effect;
    [SerializeField]
    private Text _bestScore;
    [SerializeField]
    private AudioSource _ses;

    /*Camera işlemleri*/
    [SerializeField]
    private GameObject _mainCamera;
    [SerializeField]
    private GameObject _frontCamera;  
    [SerializeField]
    private GameObject _inCamera;
    public int _cameraSayi;
    /*GamePlay*/
    [SerializeField]
    private GameObject _gamePlayPanel;
    private AudioSource _audioPlayer;

    private void Start()
    {
        _audioPlayer= GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }
    private void Update()
    {
        CameraIf();


        _bestScore.text = "BESTSCORE: " + PlayerPrefs.GetInt("BestScore");
    }
    public void ResumeTrue()
    {
        Time.timeScale = 0;
        _audioPlayer.Stop();
        GetComponent<AudioSource>().volume=0;
       
        _resumePanel.SetActive(true);
    }
    public void CameraSettings()
    {
        _cameraSayi++;

    }
    private void CameraIf()
    {
        if (_cameraSayi == 1)
        {
            _mainCamera.SetActive(false);
            _frontCamera.SetActive(true);
            _inCamera.SetActive(false);

        }
        if (_cameraSayi == 0)
        {
            _mainCamera.SetActive(true);
            _frontCamera.SetActive(false);
            _inCamera.SetActive(false);

        }
        if (_cameraSayi ==2)
        {
            _mainCamera.SetActive(false);
            _frontCamera.SetActive(false);
            _inCamera.SetActive(true);

        }
        if (_cameraSayi>=3)
        {
            _cameraSayi = 0;
        }

    }

    public void Play()
    {
        _Effect.SetActive(true);
        Invoke("PlaySecond", 1f);
    }
    private void PlaySecond()
    {

        SceneManager.LoadScene(1);

    }
    public void TryAgain()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(1);
    }

    public void ResumeFalse()
    {
        _audioPlayer.Play();
        GetComponent<AudioSource>().volume = 0.5f;
        Time.timeScale = 1;
        _resumePanel.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        _Effect.SetActive(true);
        Application.Quit();
    }
    public void GamePlay()
    {
        _ses.Stop();
        _gamePlayPanel.SetActive(true);

    }
    public void GamePlayClose()
    {
        _ses.Play();
        _gamePlayPanel.SetActive(false);

    }
}

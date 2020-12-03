using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string keyBestScore = "bestScore";

    public int score;
    public int lives;

    public bool isPause;


    public GameObject gameOver;
    public GameObject pause;

    public Text scoreText;
    public Text gameOverText;
    public Text bestScoreText;
    public Image[] hearts;

    private AudioSource audioSource;

    #region Singleton
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion
    private void Start()
    {
        Camera camera = FindObjectOfType<Camera>();
        audioSource = camera.GetComponent<AudioSource>();

        int bestScore = PlayerPrefs.GetInt(GameManager.keyBestScore);
        bestScoreText.text = "Лучший счёт " + bestScore.ToString();
    }

    private void Update()
    {
        Pause();
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        scoreText.text = score.ToString();
    }

    public void SaveBestScore()
    {
        int oldBestScore = PlayerPrefs.GetInt(keyBestScore);
        if (score > oldBestScore)
        {
            PlayerPrefs.SetInt(keyBestScore, score);
        }
      
    }

    public void LoseLife()
    {
        lives--;
        HeartsUpdate();

        if (lives == 0)
        {
            GameOver();
        }
    }

    public void AddLife()
    {
        if (lives < 3)
        {
            lives++;
            HeartsUpdate();
        }
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        gameOverText.text = "Текущий счёт " + score.ToString();
        SaveBestScore();
        Time.timeScale = 0;
        audioSource.Stop();
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Time.timeScale = 0;
            pause.SetActive(true);
            isPause = true;
            audioSource.Stop();
        }
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
        isPause = false;
        audioSource.Play();
    }

    public void HeartsUpdate()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < lives)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}

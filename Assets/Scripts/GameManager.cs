using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    [SerializeField] private GameObject GameFinishedPanel;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public static bool isGameFinished = false;
    public GameObject Camera;
    private CameraFollow camFollow;
    [SerializeField] private Slider gasSlider;
    public float gas = 50;
    private float timer;

    public bool canCountinue = false;

    public GoogleAdsManager googleAdsManager;

    [SerializeField] private GameObject gameStartPanel;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private Text startGame_HighScore;

    public static float OverallGameSpeed;
    private float scoreTimer;
    private int score;
    private int highScore;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text endGame_HighScore;
    [SerializeField] private Text endGame_Score;
    [SerializeField] private Text endGame_Coin;

    [SerializeField] private GameObject countinuePanel;
    [SerializeField] private Text conText;

    [SerializeField] private Text inGameCoinText;


    public static bool isGameStarted = false;
    //private int _brakeController = 0;

    private int currentGameCoin;
    public static int TotalCoin;
    public bool isContinue = false;

    //private float breakeValue;

    public void StartCountinuePanel()
    {
        Time.timeScale = 0;
        StartCoroutine(countinuePanelIEnumerator());
    }

    private IEnumerator countinuePanelIEnumerator()
    {
        countinuePanel.SetActive(true);
        inGamePanel.SetActive(false);
        conText.text = 5 + "";
        yield return new WaitForSecondsRealtime(1f);
        conText.text = 4 + "";
        yield return new WaitForSecondsRealtime(1f);
        conText.text = 3 + "";
        yield return new WaitForSecondsRealtime(1f);
        conText.text = 2 + "";
        yield return new WaitForSecondsRealtime(1f);
        conText.text = 1 + "";
        yield return new WaitForSecondsRealtime(1f);
        if (!isContinue)
        {
            countinuePanel.SetActive(false);
            GameFinished();
            yield break;
        }
        else
        {
            countinuePanel.SetActive(false);
            yield break;
        }

    }

    public void ContinueGame()
    {
        AudioManager.instance.Play("Click");

        googleAdsManager.UserChoseToWatchAd();
        isContinue = true;
        countinuePanel.SetActive(false);
        Time.timeScale = 1;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            obj.SetActive(false);
        }
        inGamePanel.SetActive(true);
    }

    public void GameFinished()
    {
        if (!GameManager.isGameFinished)
        {
            Time.timeScale = 1;
            if (score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
            GameManager.isGameFinished = true;
            StartCoroutine(GameFinishedIEnumerator());
        }
    }

    private void Start()
    {
        camFollow = Camera.GetComponent<CameraFollow>();
        initGame();
        //breakeValue = 0.02f;
        TotalCoin = PlayerPrefs.GetInt("Total_Coin", 0);
        AudioManager.instance.Play("Main Theme");

    }
    private void initGame()
    {
        isGameStarted = false;
        score = 0;
        currentGameCoin = 0;
        highScore = PlayerPrefs.GetInt("HighScore");
        startGame_HighScore.text = "" + highScore;
    }

    private IEnumerator GameFinishedIEnumerator()
    {

        calculateTotalCoin();
        AudioManager.instance.stopPlaying("GenelMotor");
        AudioManager.instance.stopPlaying("YavasMotor");
        AudioManager.instance.stopPlaying("Kalks");
        AudioManager.instance.stopPlaying("Police");
        yield return new WaitForSeconds(0.2f);
        inGamePanel.SetActive(false);
        yield return new WaitForSeconds(0.8f);
        endGame_HighScore.text = "" + PlayerPrefs.GetInt("HighScore");
        endGame_Score.text = "" + score;
        endGame_Coin.text = currentGameCoin + "";
        googleAdsManager.GameOver();
        GameFinishedPanel.SetActive(true);

    }

    public void RestartGame()
    {
        StartCoroutine(RestartGameIEnumerator());
    }

    IEnumerator RestartGameIEnumerator()
    {
        yield return new WaitForSeconds(0.3f);
        isGameFinished = false;
        SceneManager.LoadScene(0);
    }

    private void FixedUpdate()
    {
        gasUpdate();
        ScoreUpdateByTime();

    }
    private void Update()
    {
        if (!isGameStarted)
        {
            OverallGameSpeed = 0;
        }
        inGameCoinText.text = currentGameCoin + "";
    }

    private void gasUpdate()
    {
        if (isGameStarted)
        {
            if (gas >= 0 && !isGameFinished)
            {
                timer += Time.deltaTime;
                if (timer > 1)
                {



                    gas = gas - timer;

                    gasSlider.value = gas;
                    timer = 0;
                }
            }
            else if (gas <= 0)
            {
                GameFinished();
            }

        }
    }

    private void ScoreUpdateByTime()
    {
        if (isGameStarted && !isGameFinished)
        {
            score += 1;
            OverallGameSpeed += 0.002f;
            scoreTimer = 0;


            scoreText.text = score + "";
        }
    }

    public void AddGas()
    {
        float gasControlFloat = gas;
        if (gasControlFloat + 20 > 50)
        {
            gas = 50;
        }
        else
        {
            gas += 20;
        }

    }

    public void StartGame()
    {
        Debug.Log("Started");
        StartCoroutine( camFollow.StartCamera());
        StartCoroutine(StartGameIEnumerator());
    }

    IEnumerator StartGameIEnumerator()
    {
        gameStartPanel.SetActive(false);
        inGamePanel.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        AudioManager.instance.Play("Kalkis");
        AudioManager.instance.Play("Police");
        isGameStarted = true;
    }

    public void addCoin(int coin)
    {
        currentGameCoin += coin;
        AudioManager.instance.Play("Click");
    }

    public void calculateTotalCoin()
    {
        TotalCoin = PlayerPrefs.GetInt("Total_Coin");
        PlayerPrefs.SetInt("Total_Coin", TotalCoin + currentGameCoin);
    }
}

using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] private AudioClip deathSound;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject tapToPlayPanel;

    private AudioSource audioSource;
    private int _score;
    public bool isGameStarted { get; private set; }

    public int score
    {
        get => _score;
        set
        {
            _score = value;
            scoreText.text = _score.ToString();
            UpdateHighScore(_score);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        isGameStarted = false;
        Time.timeScale = 0f;
        tapToPlayPanel.SetActive(true);
        DisplayHighScore();
    }

    private void Update()
    {
        if (!isGameStarted && Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        isGameStarted = true;
        Time.timeScale = 1f;
        tapToPlayPanel.SetActive(false);
        score = 0;
        FindObjectOfType<BallController>()?.StartPhysics();
    }

    public void OnGameOver()
    {
        audioSource.PlayOneShot(deathSound);
        UpdateHighScore(score);
        DisplayHighScore();
        score = 0;
        FindObjectOfType<BallController>()?.ResetBall();
        Debug.Log("Game Over");
    }

    private void UpdateHighScore(int currentScore)
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
        }
    }

    private void DisplayHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScoreText != null)
        {
            highScoreText.text = highScore.ToString();
        }
    }
}
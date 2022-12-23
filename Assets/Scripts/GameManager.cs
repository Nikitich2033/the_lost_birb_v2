using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Player player;
    public Text scoreText;
    public GameObject Leaderboard;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject replayButton;
    public GameObject showLeaderboardButton;
    public GameObject hideLeaderboardButton;


    // public bool isDead = false;
    private int score;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        Pause();
    }

    public void Start()
    {
        replayButton.SetActive(false);
        Leaderboard.SetActive(false);
    }

    public void Replay()
    {
        SceneManager.LoadScene(0);
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        replayButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++){
            Destroy(pipes[i].gameObject);
        }

        showLeaderboardButton.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        replayButton.SetActive(true);

        // isDead = true;

        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void ShowLeaderboard()
    {
        Leaderboard.SetActive(true);
    }

    public void HideLeaderboard()
    {
        Leaderboard.SetActive(false);
    }
    
}
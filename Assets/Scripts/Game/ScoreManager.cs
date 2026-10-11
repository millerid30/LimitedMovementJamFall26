using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    [SerializeField] private int score;
    [SerializeField] private int startScore = 3;
    [SerializeField] private int winScore = 10;
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI gameResult;
    private bool gameEnd;

    [SerializeField] private GameObject confetti;
    [SerializeField][Range(1, 1000)] private float confettiCoef;
    void Start()
    {
        if (Instance == null) { Instance = this; }
        score = startScore;
        Points();
        gameEnd = false;
        gameResult.text = "";
        gameResult.gameObject.SetActive(false);
    }
    void Update()
    {
        if (!gameEnd && (score >= winScore || score <= 0))
        {
            StartCoroutine(GameEnd());
            GameEnd();
        }
    }
    public int GetScore()
    {
        return score;
    }
    public void Points(int amount = 0)
    {
        if (!gameEnd)
        {
            score += amount;
            score = Mathf.Clamp(score, 0, winScore);
            scoreUI.text = score.ToString();
        }
    }
    IEnumerator GameEnd()
    {
        gameEnd = true;
        gameResult.gameObject.SetActive(true);
        if (score >= winScore)
        {
            gameResult.text = "You Win!";
            SoundManager.Instance.PlaySound("Win", 0.5f);
            if (confetti.GetComponent<Confetti>() != null)
            {
                for (int i = 0; i < confettiCoef; i++)
                {
                    Instantiate(confetti, transform);
                }
            }
        }
        else
        {
            gameResult.text = "You're Fired!";
            SoundManager.Instance.PlaySound("Lose", 0.5f);
            if (confetti.GetComponent<Confetti>() != null)
            {
                for (int i = 0; i < Mathf.RoundToInt(confettiCoef / 10); i++)
                {
                    Instantiate(confetti, transform);
                }
            }
        }
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
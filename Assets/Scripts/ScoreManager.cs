using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    [SerializeField] private int score;
    [SerializeField] private TextMeshProUGUI scoreUI;
    void Start()
    {
        if (Instance == null) { Instance = this; }
        score = 0;
    }
    void Update()
    {

    }
    public int GetScore()
    {
        return score;
    }
    public void Points(int amount)
    {
        score += amount;
        scoreUI.text = score.ToString();
    }
}
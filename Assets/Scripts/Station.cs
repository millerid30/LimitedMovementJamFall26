using UnityEngine;
using UnityEngine.UI;

public class Station : MonoBehaviour
{
    [SerializeField] private StationSO station;
    [SerializeField] private bool isActive;
    [SerializeField] private float workChance;
    [SerializeField] private Image activeTask;
    [SerializeField] private Image progressBar;
    [SerializeField] private float progress;
    [SerializeField] private float maxProgress;
    [SerializeField] private bool isWorking;
    [SerializeField] private float workRate;
    [SerializeField] private float decayRate;
    [SerializeField] private float points;
    [SerializeField] private StationType type;
    private SpriteRenderer sr;
    void Start()
    {
        IsActive(false);
        progress = 0.0f;
        isWorking = false;
        sr = GetComponentInChildren<SpriteRenderer>();
        maxProgress = station.stationProgress;
        workRate = station.stationSpeed;
        decayRate = station.stationDecay;
        workChance = station.stationChance;
        points = station.stationPoints;
        type = station.stationType;
    }
    void Update()
    {
        if (!isActive)
        {
            float x = Random.Range(0f, 1f);
            if (x <= workChance)
            {
                IsActive(true);
            }
        }
        Work();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isWorking = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isWorking = false;
        }
        sr.color = Color.white;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (isActive)
            {
                sr.color = Color.red;
            }
            progress -= Time.deltaTime * decayRate;
        }
        else
        {
            sr.color = Color.white;
        }
    }
    public void Work()
    {
        if (isActive)
        {
            if (isWorking)
            {
                progress += Time.deltaTime * workRate;
            }
            else
            {
                progress -= Time.deltaTime * decayRate;
            }
            progress = Mathf.Clamp(progress, 0.0f, maxProgress);
            progressBar.fillAmount = progress / maxProgress;
            if (progress >= maxProgress)
            {
                Debug.Log("Task Complete!");
                ScoreManager.Instance.Points((int)points);
                IsActive(false);
            }
            if (progress <= 0)
            {
                Debug.Log("Task Failed!");
                if (type == StationType.Active)
                {
                    ScoreManager.Instance.Points(-(int)points);
                }
                else
                {
                    ScoreManager.Instance.Points(0);
                }
                IsActive(false);
            }
        }
    }
    public void IsActive(bool active)
    {
        isActive = active;
        activeTask.gameObject.SetActive(active);
        progressBar.gameObject.SetActive(active);
        progress = maxProgress / 2;
    }
    public bool StationActive()
    {
        return isActive;
    }
}
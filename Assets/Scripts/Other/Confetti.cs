using UnityEngine;

public class Confetti : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Color randomColor = Random.ColorHSV(0f,1f,1f,1f,0.5f,1f);
        GetComponentInChildren<SpriteRenderer>().material.color = randomColor;
        Kaboom();
    }
    void Kaboom()
    {
        Vector2 randDir = new Vector2(Random.Range(-1f,1f),Random.Range(0.1f,1f));
        float randForce = Random.Range(1f, 10f);
        rb.AddForce(randForce * randDir * 3, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * (randForce) * 150);
        Destroy(gameObject, 3);
    }
}
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private int target = 0;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float turnSpeed = 2f;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float health = 0f;
    [SerializeField] private float maxHealth = 10f;
    private bool isDead;
    private SpriteRenderer sr;
    private float offset = -90;
    private Transform targetDir;
    private Vector2 direction;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        GetTarget();
        health = maxHealth;
        isDead = false;
    }
    void Update()
    {
        if (targetDir == null || !StationList.Instance.stations[target].GetComponent<Station>().StationActive())
        {
            GetTarget();
        }
        Look();
        Move();
        ClampSpeed();
        if (health <= 0 && !isDead)
        {
            isDead = true;
            OnDeath();
        }
    }
    void GetTarget()
    {
        target = Random.Range(0, StationList.Instance.stations.Count);
        targetDir = StationList.Instance.stations[target].GetComponent<Transform>();
    }
    void Look()
    {
        direction = Vector2.zero;
        if (targetDir != null)
        {
            direction = (targetDir.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRot = Quaternion.Euler(new Vector3(0, 0, angle + offset));
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
        }
    }
    void Move()
    {
        rb.AddForce(direction * moveSpeed, ForceMode2D.Force);
    }
    void ClampSpeed()
    {
        if (Mathf.Abs(rb.linearVelocityY) > maxSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Sign(rb.linearVelocityY) * maxSpeed);
        }
    }
    public void Damage(float amount)
    {
        health -= amount;
    }
    void OnDeath()
    {
        isDead = true;
        sr.color = Color.red;
        foreach (Collider2D coll in GetComponentsInChildren<Collider2D>())
        {
            coll.enabled = false;
        }
        Vector2 randDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.AddForce(randDir.normalized * moveSpeed * maxSpeed, ForceMode2D.Impulse);
        rb.AddTorque(randDir.x * turnSpeed * maxSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, 3);
    }
}
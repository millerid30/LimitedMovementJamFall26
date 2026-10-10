using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput input;
    public float speed;
    [SerializeField] private Transform body;
    [SerializeField] private Image kickMeter;
    [SerializeField] private float kickForce = 5f;
    [SerializeField] private float kickTimer = 0f;
    [SerializeField] private float timerMulti = 5f;
    bool timerOn;
    [SerializeField] private float kickCooldown = 0.5f;
    [SerializeField] private float cooldownTimer = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        kickMeter.gameObject.SetActive(false);
        kickTimer = 0;
        cooldownTimer = 0;
    }
    void Update()
    {
        speed = rb.linearVelocity.magnitude;
        Timer();
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
    public void Kick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            timerOn = true;
            kickMeter.gameObject.SetActive(true);
        }
        if (context.canceled)
        {
            timerOn = false;
            kickMeter.gameObject.SetActive(false);
            if (cooldownTimer <= 0)
            {
                rb.AddForce(-body.up * kickForce * kickTimer, ForceMode2D.Impulse);
                cooldownTimer = kickCooldown;
            }
        }
    }
    void Timer()
    {
        if (timerOn)
        {
            kickTimer += Time.deltaTime * timerMulti;
            kickTimer = Mathf.Clamp(kickTimer, 0, kickForce);
        }
        else
        {
            kickTimer = 0;
        }
        kickMeter.fillAmount = kickTimer / kickForce;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Damage(rb.linearVelocity.magnitude);
        }
    }
}
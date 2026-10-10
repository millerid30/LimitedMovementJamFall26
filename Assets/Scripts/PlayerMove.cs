using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Transform body;
    [SerializeField] private Image kickMeter;
    [SerializeField] private float kickForce = 5f;
    [SerializeField] private float kickTimer = 0f;
    [SerializeField] private float timerMulti = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        kickMeter.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            kickMeter.gameObject.SetActive(true);
            kickMeter.fillAmount = kickTimer/kickForce;
            kickTimer += Time.deltaTime * timerMulti;
            kickTimer = Mathf.Clamp(kickTimer, 0, kickForce);
        }
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            rb.AddForce(-body.up * kickForce * kickTimer, ForceMode2D.Impulse);
            kickTimer = 0;
            kickMeter.gameObject.SetActive(false);
        }
    }
}
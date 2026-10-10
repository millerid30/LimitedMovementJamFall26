using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 worldPosition;
    private Vector2 direction;

    [SerializeField] private Transform Aim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Look();
    }
    public void Look()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldPosition - (Vector2)Aim.transform.position).normalized;
        Aim.transform.up = direction;
    }
}
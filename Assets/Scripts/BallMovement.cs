using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField]
    private GameManager manager;

    private float ballSpeed = 5;
    private Vector2 currentVelocity;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        manager.onReset += OnReset;
        manager.onStart += OnGameStart;
    }

    private void OnGameStart()
    {
        var xRandom = Random.Range(0.5f, 1) * Mathf.Sign(Random.Range(-1, 1));
        currentVelocity = new Vector3(xRandom,
            Random.Range(-0.5f, 0.5f)).normalized * ballSpeed;
    }

    private void OnDestroy()
    {
        manager.onReset -= OnReset;
        manager.onStart -= OnGameStart;
    }

    private void OnReset()
    {
        transform.position = Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var normal = collision.contacts[0].normal;
        currentVelocity = Vector2.Reflect(currentVelocity, normal);

        if (collision.collider.tag == "Paddle")
        {
            var paddleCenter = collision.collider.transform.position.y;
            var distanceFromCenter = transform.position.y - paddleCenter;
            var yVelocity = distanceFromCenter * ballSpeed;
            currentVelocity = (new Vector2(currentVelocity.x, yVelocity).normalized) * ballSpeed;
        }

        if (collision.collider.tag == "WallLeft")
        {
            manager.OnLost(Player.Player1);
        }
        else if (collision.collider.tag == "WallRight")
        {
            manager.OnLost(Player.Player2);
        }
    }

    private void FixedUpdate()
    {
        if (manager.state != GameState.Playing)
        {
            return;
        }

        rb.position += currentVelocity * Time.fixedDeltaTime;
    }
}

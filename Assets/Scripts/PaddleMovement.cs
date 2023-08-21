using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    [SerializeField]
    private GameManager manager;

    [SerializeField]
    private float speed;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager.onReset += OnReset;
    }

    private void OnReset()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    public void Move(int direction)
    {
        if (manager.state != GameState.Playing)
        {
            return;
        }

        if (direction == 0)
        {
            return;
        }

        var verticalSpeed = speed * new Vector2(0, Mathf.Sign(direction));
        rb.MovePosition(rb.position + verticalSpeed * Time.fixedDeltaTime);
    }
}

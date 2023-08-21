using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PaddleMovement))]
public class AIInput : MonoBehaviour
{
    private PaddleMovement movement;
    private Transform ballTransform;

    private Vector3 lastBallPosition;

    private void Start()
    {
        movement = GetComponent<PaddleMovement>();
        ballTransform = GameObject.FindWithTag("Ball").GetComponent<Transform>();
        lastBallPosition = ballTransform.position;
    }

    private void FixedUpdate()
    {
        var currentPosition = ballTransform.position;
        var ballDirection = currentPosition - lastBallPosition;
        lastBallPosition = currentPosition;
        // if (ballDirection.x < 0)
        // {
        //     return;
        // }

        var xDistance = currentPosition.x - transform.position.x;
        var xSpeed = ballDirection.x / Time.fixedDeltaTime; // velocity * Time.fixedDeltaTime;
        var timeToMe = Mathf.Abs(xDistance) / xSpeed;
        var ySpeed = ballDirection.y / Time.fixedDeltaTime;
        var yPosition = ySpeed * timeToMe;

        var distanceToCover = yPosition - transform.position.y;
        if (Mathf.Abs(distanceToCover) < 0.1f)
        {
            return;
        }

        movement.Move((int)Mathf.Sign(distanceToCover));
    }
}

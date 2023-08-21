using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PaddleMovement))]
public class PlayerInput : MonoBehaviour
{
    private PaddleMovement movement;

    private void Start()
    {
        movement = GetComponent<PaddleMovement>();
    }

    private void FixedUpdate()
    {
        var moveDirection = Input.GetAxisRaw("Vertical");
        if (moveDirection > 0)
        {
            movement.Move(1);
        }
        else if (moveDirection < 0)
        {
            movement.Move(-1);
        }
    }
}

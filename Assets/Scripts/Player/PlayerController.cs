using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    private Vector3 _targetPosition;
    public bool moving = false;


    private void Start()
    {
        _targetPosition = transform.position;

    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);

        if (transform.position == _targetPosition)
        {
            moving = false;
        }

        // Check player's movement direction
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput > 0)
        {
            // Flip the player's sprite horizontally
            transform.localScale = new Vector3(0.48f, 0.48f, 0.48f);
        }
        else if (horizontalInput < 0)
        {
            // Flip the player's sprite horizontally
            transform.localScale = new Vector3(-0.48f, 0.48f, 0.48f);
        }
    }

    public void MoveTo(Vector2Int position)
    {
        _targetPosition = GridController.Instance.GetWorldPosition(position);
    }
}

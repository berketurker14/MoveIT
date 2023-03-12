using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    private Vector3 _targetPosition;
    public bool moving = false;
    private Animator _animator;
    private bool _facingRight = true;

    private void Start()
    {
        _targetPosition = transform.position;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);

        if (transform.position == _targetPosition)
        {
            moving = false;
            // Player has reached the target position, play "idle" animation
            _animator.SetBool("MovingLeft", false);
            _animator.SetBool("MovingRight", false);
            _animator.Play("idle");
        }

        // Check player's movement direction
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput > 0 && !_facingRight)
        {
            // Start "runTowards" animation
            _animator.SetBool("MovingLeft", false);
            _animator.SetBool("MovingRight", true);
            _animator.Play("runTowards");
            _facingRight = true;
            if (!moving)
            {
                moving = true;
            }
        }
        else if (horizontalInput < 0 && _facingRight)
        {
            // Start "run" animation
            _animator.SetBool("MovingRight", false);
            _animator.SetBool("MovingLeft", true);
            _animator.Play("run");
            _facingRight = false;
            if (!moving)
            {
                moving = true;
            }
        }
    }

    public void MoveTo(Vector2Int position)
    {
        _targetPosition = GridController.Instance.GetWorldPosition(position);
    }
}

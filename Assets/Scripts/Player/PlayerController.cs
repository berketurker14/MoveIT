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
        if (transform.position != _targetPosition)
        {
            Move();
        }
        else
        {
            moving = false;
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
    }

    public void MoveTo(Vector2Int position)
    {
        moving = true;
        _targetPosition = GridController.Instance.GetWorldPosition(position);
    }

}

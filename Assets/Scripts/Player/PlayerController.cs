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
    }

    public void MoveTo(Vector2Int position)
    {
        _targetPosition = GridController.Instance.GetWorldPosition(position);
    }

}

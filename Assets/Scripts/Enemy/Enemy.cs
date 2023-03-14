using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2Int currentGridPos;
    public Vector2Int targetGridPos;
    public Vector2Int[] moveDirections;
    public float moveTime = 0.5f;
    private float timer;

    private float turnTimer = 0f;
    public float turnTime = 1f;

    private void Start()
    {
        // Set up move directions for the enemy
        moveDirections = new Vector2Int[] { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
        MoveTowards(GridController.Instance.currentGridPosition);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Increment the turn timer
        turnTimer += Time.deltaTime;

        // If the turn timer is greater than the move time, move the enemy
        if (turnTimer >= turnTime)
        {
            // Reset the turn timer
            turnTimer = 0f;

            transform.position = GridController.Instance.GetWorldPosition(targetGridPos);

            // Move the enemy
            MoveTowards(GridController.Instance.currentGridPosition);
        }


        // Increment the timer for the current move
        timer += Time.deltaTime;

        // Calculate the percentage of the move completed based on the timer and moveTime
        float t = Mathf.Clamp01(timer / moveTime);

        // Move towards the target grid position
        Vector3 targetPos = GridController.Instance.GetWorldPosition(targetGridPos);
        Vector3 direction = (targetPos - transform.position).normalized;
        Vector3 newPos = transform.position + direction * moveSpeed * Time.deltaTime;

        // Snap the new position to the grid
        Vector2Int newGridPos = new Vector2Int(Mathf.RoundToInt(newPos.x), Mathf.RoundToInt(newPos.y));

        // If the new grid position is different from the current grid position, move to the new grid position
        if (newGridPos != currentGridPos)
        {
            // Set the new grid position as the current grid position
            currentGridPos = targetGridPos;

            // Reset the timer for the new move
            timer = 0f;
        }

        // Interpolate between the current position and the target position based on the percentage of the move completed
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }

    public void MoveTowards(Vector2Int destinationGridPos)
    {
        Debug.Log("MoveTowards");
        if (destinationGridPos == currentGridPos)
        {
            // The player is on the same grid position as the enemy
            return;
        }
        // Get the direction to move in
        Vector2Int direction = destinationGridPos - currentGridPos;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Move in the x direction
            if (direction.x > 0)
            {
                // Move right
                targetGridPos = currentGridPos + Vector2Int.right;
            }
            else
            {
                // Move left
                targetGridPos = currentGridPos + Vector2Int.left;
            }
        }
        else
        {
            // Move in the y direction
            if (direction.y > 0)
            {
                // Move up
                targetGridPos = currentGridPos + Vector2Int.up;
            }
            else
            {
                // Move down
                targetGridPos = currentGridPos + Vector2Int.down;
            }
        }

        // Reset the timer for the new mov
        timer = 0f;
    }
}

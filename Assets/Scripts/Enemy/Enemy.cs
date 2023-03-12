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

    private void Start()
    {
        // Set up move directions for the enemy
        moveDirections = new Vector2Int[] { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
    }

    private void Update()
    {
        // Check if the enemy has reached its target grid position
        if (currentGridPos == targetGridPos)
        {
            // Choose a random direction to move in
            Vector2Int randomDir = moveDirections[Random.Range(0, moveDirections.Length)];

            // Set the target grid position to the new position in the chosen direction
            targetGridPos = currentGridPos + randomDir;

            // Reset the timer for the new move
            timer = 0f;
        }

        // Increment the timer for the current move
        timer += Time.deltaTime;

        // Calculate the percentage of the move completed based on the timer and moveTime
        float t = Mathf.Clamp01(timer / moveTime);

        // Move towards the target grid position
        Vector3 targetPos = new Vector3(targetGridPos.x, targetGridPos.y, 0f);
        Vector3 direction = (targetPos - transform.position).normalized;
        Vector3 newPos = transform.position + direction * moveSpeed * Time.deltaTime;

        // Snap the new position to the grid
        Vector2Int newGridPos = new Vector2Int(Mathf.RoundToInt(newPos.x), Mathf.RoundToInt(newPos.y));

        // If the new grid position is different from the current grid position, move to the new grid position
        if (newGridPos != currentGridPos)
        {
            // Set the new grid position as the current grid position
            currentGridPos = newGridPos;

            // Reset the timer for the new move
            timer = 0f;
        }

        // Interpolate between the current position and the target position based on the percentage of the move completed
        transform.position = Vector3.Lerp(transform.position, targetPos, t);
    }
}

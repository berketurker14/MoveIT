using UnityEngine;
using System.Collections.Generic;

public class GridController : MonoBehaviour
{
    public float gridSize = 1.0f;
    public float actionCooldown = 1.0f;

    public GameObject player;

    public Vector2Int currentGridPosition;
    private float currentCooldown = 0.0f;
    private List<Vector2Int> currentMovementSequence = new List<Vector2Int>();

    private Dictionary<Vector2Int, bool> occupiedGridPositions;


    public static GridController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        occupiedGridPositions = new Dictionary<Vector2Int, bool>();
    }

    private void Start()
    {
        currentGridPosition = GetGridPosition(player.transform.position);
    }

    public Vector2Int GetGridPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / gridSize);
        int y = Mathf.RoundToInt(position.y / gridSize);
        return new Vector2Int(x, y);
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        float x = gridPosition.x * gridSize;
        float y = gridPosition.y * gridSize;
        return new Vector3(x, y, transform.position.z);
    }

    public bool IsGridPositionAvailable(Vector2Int gridPosition)
    {
        return !occupiedGridPositions.ContainsKey(gridPosition);
    }

    public void SetGridPositionOccupied(Vector2Int gridPosition, bool isOccupied)
    {
        if (isOccupied)
        {
            occupiedGridPositions[gridPosition] = true;
        }
        else
        {
            occupiedGridPositions.Remove(gridPosition);
        }
    }

    /*     private List<Vector2Int> GetActionPositions(Dictionary<Vector2Int, int> actionOffsets)
        {
            List<Vector2Int> actionPositions = new List<Vector2Int>();

            foreach (KeyValuePair<Vector2Int, int> offset in actionOffsets)
            {
                Vector2Int actionPosition = currentGridPosition + (offset.Key * offset.Value);
                actionPositions.Add(actionPosition);
            }

            return actionPositions;
        } */
}

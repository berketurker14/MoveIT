using UnityEngine;
using System.Collections.Generic;

public class GridController : MonoBehaviour
{
    public float gridSize = 1.0f;
    public float actionCooldown = 1.0f;
    public List<MovementActionPattern> MovementActionPatterns;

    public GameObject player;

    public Vector2Int currentGridPosition;
    private float currentCooldown = 0.0f;
    private List<Vector2Int> currentMovementSequence = new List<Vector2Int>();

    public static GridController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentGridPosition = GetGridPosition(player.transform.position);
    }

    private void Update()
    {
        (bool isPatternFound, PatternType patternType) = HandleMovement();
        ActionController.Instance.HandleAction(isPatternFound, patternType);
    }

    private (bool, PatternType) HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if ((horizontalInput != 0.0f || verticalInput != 0.0f) && currentCooldown <= 0.0f && !player.GetComponent<PlayerController>().moving)
        {
            Vector2Int direction;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                direction = new Vector2Int(0, 1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                direction = new Vector2Int(0, -1);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                direction = new Vector2Int(-1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                direction = new Vector2Int(1, 0);
            }
            else
            {
                return (false, PatternType.Direct);
            }
            Vector2Int nextGridPosition = currentGridPosition + direction;

            if (IsGridPositionValid(nextGridPosition))
            {
                player.GetComponent<PlayerController>().MoveTo(nextGridPosition);
                currentGridPosition = nextGridPosition;
                //transform.position = GetWorldPosition(currentGridPosition);

                currentMovementSequence.Add(direction);

                foreach (MovementActionPattern pattern in MovementActionPatterns)
                {
                    if (currentMovementSequence.Count >= pattern.movementSequence.Count)
                    {
                        bool match = true;

                        for (int i = 0; i < pattern.movementSequence.Count; i++)
                        {
                            if (pattern.movementSequence[i] != currentMovementSequence[currentMovementSequence.Count - pattern.movementSequence.Count + i])
                            {
                                match = false;
                                break;
                            }
                        }

                        if (match)
                        {
                            /*List<Vector2Int> actionPositions = GetActionPositions(pattern.actionOffsets);

                            foreach (Vector2Int actionPosition in actionPositions)
                            {
                                if (IsGridPositionValid(actionPosition))
                                {
                                    // Perform action logic for each valid position here
                                }
                            }*/

                            currentMovementSequence.Clear();
                            currentCooldown = actionCooldown;
                            return (true, pattern.patternType);
                        }
                    }
                }
            }
        }
        return (false, PatternType.Direct);
    }

    public Vector2Int GetGridPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / gridSize);
        int y = Mathf.RoundToInt(position.y / gridSize);
        return new Vector2Int(x, y);
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        float x = gridPosition.x * gridSize + 0.5f * gridPosition.x;
        float y = gridPosition.y * gridSize + 0.5f * gridPosition.y;
        return new Vector3(x, y, transform.position.z);
    }

    public bool IsGridPositionValid(Vector2Int gridPosition)
    {
        // Perform any necessary validation checks here
        return true;
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

[System.Serializable]
public class MovementActionPattern
{
    public List<Vector2Int> movementSequence;
    public PatternType patternType;

    public MovementActionPattern(List<Vector2Int> movementSequence, PatternType patternType)
    {
        this.movementSequence = movementSequence;
        this.patternType = patternType;
    }

}

public enum PatternType
{
    Circle,
    Direct,
    L,
    ZigZag,

}

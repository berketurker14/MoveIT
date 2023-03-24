using UnityEngine;
using System.Collections.Generic;

public class MovementController : MonoBehaviour
{
    public float actionCooldown = 1.0f;

    public GameObject player;

    public Vector2Int currentGridPosition;
    private float currentCooldown = 0.0f;
    private List<Vector2Int> currentMovementSequence = new List<Vector2Int>();

    public static MovementController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            (bool isPatternFound, PatternType patternType) = HandleMovement();
            ActionController.Instance.HandleAction(isPatternFound, patternType);
        }
    }

    private (bool, PatternType) HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        if ((horizontalInput != 0.0f || verticalInput != 0.0f) && !player.GetComponent<PlayerController>().moving)
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
                direction = new Vector2Int(Mathf.RoundToInt(horizontalInput), Mathf.RoundToInt(verticalInput));
            }
            Vector2Int nextGridPosition = currentGridPosition + direction;

            if (GridController.Instance.IsGridPositionAvailable(nextGridPosition))
            {
                player.GetComponent<PlayerController>().MoveTo(nextGridPosition);
                player.GetComponent<PlayerController>().moving = true;
                currentGridPosition = nextGridPosition;
                //transform.position = GetWorldPosition(currentGridPosition);

                currentMovementSequence.Add(direction);

                foreach (MovementActionPattern pattern in MovementActionPatternList.MovementActionPatterns)
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
                                if (IsGridPositionAvailable(actionPosition))
                                {
                                    // Perform action logic for each valid position here
                                }
                            }*/
                            Debug.Log("Pattern found: " + pattern.patternType);
                            currentMovementSequence.Clear();
                            currentCooldown = actionCooldown;
                            return (true, pattern.patternType);
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Grid Position not available");
            }
        }
        return (false, PatternType.Direct);
    }
}


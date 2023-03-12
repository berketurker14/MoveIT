using UnityEngine;
using System.Collections.Generic;

public class ActionController : MonoBehaviour
{
    public List<Action> actions;
    public float actionCooldown = 1.0f;
    private float currentCooldown = 0.0f;
    private List<Vector2Int> currentActionSequence = new List<Vector2Int>();

    public static ActionController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void HandleAction(bool isPatternFound, PatternType patternType)
    {
        if (currentCooldown > 0.0f)
        {
            currentCooldown -= Time.deltaTime;
        }
        else
        {
            if (isPatternFound)
            {
                Action action = actions.Find(a => a.patternType == patternType);
                List<Vector2Int> actionPositions = GetActionPositions(action.GetActionOffsets());

                foreach (Vector2Int actionPosition in actionPositions)
                {
                    if (action.actionType == ActionType.Attack)
                    {
                        // Attack
                    }
                    else if (action.actionType == ActionType.Heal)
                    {
                        // Heal
                    }
                }

                currentActionSequence.Clear();
                currentCooldown = actionCooldown;
            }
        }
    }

    public List<Vector2Int> GetActionPositions(List<Vector2Int> actionOffsets)
    {
        List<Vector2Int> actionPositions = new List<Vector2Int>();

        foreach (Vector2Int actionOffset in actionOffsets)
        {
            actionPositions.Add(GridController.Instance.currentGridPosition + actionOffset);
        }

        return actionPositions;
    }
}
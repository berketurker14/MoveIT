using UnityEngine;
using System.Collections.Generic;

public class Action
{
    public ActionType actionType;
    public List<Vector2Int> attackOffsets;
    public RangeType rangeType;
    public PatternType patternType;

    public DirectionType directionType;

    public Action(ActionType actionType, RangeType rangeType, DirectionType directionType = DirectionType.Up)
    {
        this.actionType = actionType;
        this.rangeType = rangeType;
        this.directionType = directionType;
    }

    public List<Vector2Int> GetActionOffsets()
    {
        List<Vector2Int> actionOffsets = new List<Vector2Int>();

        switch (rangeType)
        {
            case RangeType.Single:
                actionOffsets.Add(Vector2Int.zero);
                break;
            case RangeType.Cross:
                actionOffsets.Add(Vector2Int.zero);
                actionOffsets.Add(Vector2Int.up);
                actionOffsets.Add(Vector2Int.down);
                actionOffsets.Add(Vector2Int.left);
                actionOffsets.Add(Vector2Int.right);
                break;
            case RangeType.Square:
                actionOffsets.Add(Vector2Int.zero);
                actionOffsets.Add(Vector2Int.up);
                actionOffsets.Add(Vector2Int.down);
                actionOffsets.Add(Vector2Int.left);
                actionOffsets.Add(Vector2Int.right);
                actionOffsets.Add(Vector2Int.up + Vector2Int.left);
                actionOffsets.Add(Vector2Int.up + Vector2Int.right);
                actionOffsets.Add(Vector2Int.down + Vector2Int.left);
                actionOffsets.Add(Vector2Int.down + Vector2Int.right);
                break;
            case RangeType.Diamond:
                actionOffsets.Add(Vector2Int.zero);
                actionOffsets.Add(Vector2Int.up);
                actionOffsets.Add(Vector2Int.down);
                actionOffsets.Add(Vector2Int.left);
                actionOffsets.Add(Vector2Int.right);
                actionOffsets.Add(Vector2Int.up + Vector2Int.left);
                actionOffsets.Add(Vector2Int.up + Vector2Int.right);
                actionOffsets.Add(Vector2Int.down + Vector2Int.left);
                actionOffsets.Add(Vector2Int.down + Vector2Int.right);
                actionOffsets.Add(Vector2Int.up * 2);
                actionOffsets.Add(Vector2Int.down * 2);
                actionOffsets.Add(Vector2Int.left * 2);
                actionOffsets.Add(Vector2Int.right * 2);
                break;
            case RangeType.Custom:
                actionOffsets = attackOffsets;
                break;
        }

        return actionOffsets;
    }


}

public enum RangeType
{
    Single,
    Cross,
    Square,
    Diamond,
    Custom
}

public enum ActionType
{
    Attack,
    Defend,
    Buff,
    Heal
}

public enum DirectionType { Up, Down, Left, Right }
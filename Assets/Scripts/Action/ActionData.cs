using UnityEngine;
using System.Collections.Generic;

public static class ActionData
{
    public static List<Action> actions = new List<Action>()
    {
        new Action(ActionType.Attack, RangeType.Single),
        new Action(ActionType.Attack, RangeType.Cross),
        new Action(ActionType.Attack, RangeType.Square),
        new Action(ActionType.Attack, RangeType.Diamond),
        new Action(ActionType.Heal, RangeType.Single),
        new Action(ActionType.Heal, RangeType.Cross),
        new Action(ActionType.Heal, RangeType.Square),
        new Action(ActionType.Heal, RangeType.Diamond)
    };
}
using UnityEngine;
using System.Collections.Generic;

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

public static class MovementActionPatternList
{
    public static List<MovementActionPattern> MovementActionPatterns = new List<MovementActionPattern>()
    {
        // Circle Patterns
        new MovementActionPattern(new List<Vector2Int>() { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left }, PatternType.Circle),
        new MovementActionPattern(new List<Vector2Int>() { Vector2Int.up, Vector2Int.left, Vector2Int.down, Vector2Int.right }, PatternType.Circle),
        new MovementActionPattern(new List<Vector2Int>() { Vector2Int.down, Vector2Int.right, Vector2Int.up, Vector2Int.left }, PatternType.Circle),
        new MovementActionPattern(new List<Vector2Int>() { Vector2Int.down, Vector2Int.left, Vector2Int.up, Vector2Int.right }, PatternType.Circle),
        new MovementActionPattern(new List<Vector2Int>() { Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down }, PatternType.Circle),
        new MovementActionPattern(new List<Vector2Int>() { Vector2Int.left, Vector2Int.down, Vector2Int.right, Vector2Int.up }, PatternType.Circle),
        new MovementActionPattern(new List<Vector2Int>() { Vector2Int.right, Vector2Int.up, Vector2Int.left, Vector2Int.down }, PatternType.Circle),
        new MovementActionPattern(new List<Vector2Int>() { Vector2Int.right, Vector2Int.down, Vector2Int.left, Vector2Int.up }, PatternType.Circle),
        
        // Direct Patterns
        new MovementActionPattern(new List<Vector2Int>() { Vector2Int.down, Vector2Int.down, Vector2Int.down, Vector2Int.down }, PatternType.Direct),
        new MovementActionPattern(new List<Vector2Int>() { Vector2Int.left, Vector2Int.left, Vector2Int.left, Vector2Int.left }, PatternType.Direct),
        new MovementActionPattern(new List<Vector2Int>() { Vector2Int.right, Vector2Int.right, Vector2Int.right, Vector2Int.right }, PatternType.Direct),


        // L Patterns
        new MovementActionPattern(new List<Vector2Int> { Vector2Int.right,Vector2Int.right,Vector2Int.right, Vector2Int.up }, PatternType.L),
        new MovementActionPattern(new List<Vector2Int> { Vector2Int.right,Vector2Int.right,Vector2Int.right, Vector2Int.down }, PatternType.L),
        new MovementActionPattern(new List<Vector2Int> { Vector2Int.left,Vector2Int.left,Vector2Int.left, Vector2Int.up }, PatternType.L),
        new MovementActionPattern(new List<Vector2Int> { Vector2Int.left,Vector2Int.left,Vector2Int.left, Vector2Int.down }, PatternType.L),
        new MovementActionPattern(new List<Vector2Int> { Vector2Int.up,Vector2Int.up,Vector2Int.up, Vector2Int.right }, PatternType.L),
        new MovementActionPattern(new List<Vector2Int> { Vector2Int.up,Vector2Int.up,Vector2Int.up, Vector2Int.left }, PatternType.L),
        new MovementActionPattern(new List<Vector2Int> { Vector2Int.down,Vector2Int.down,Vector2Int.down, Vector2Int.right }, PatternType.L),
        new MovementActionPattern(new List<Vector2Int> { Vector2Int.down,Vector2Int.down,Vector2Int.down, Vector2Int.left }, PatternType.L),

        // ZigZag Patterns
        new MovementActionPattern(new List<Vector2Int> { Vector2Int.right, Vector2Int.up, Vector2Int.right, Vector2Int.up }, PatternType.ZigZag),
        new MovementActionPattern(new List<Vector2Int> { Vector2Int.up, Vector2Int.right, Vector2Int.up, Vector2Int.right }, PatternType.ZigZag),
        new MovementActionPattern(new List<Vector2Int> { Vector2Int.right, Vector2Int.down, Vector2Int.right, Vector2Int.down }, PatternType.ZigZag),
        new MovementActionPattern(new List<Vector2Int> { Vector2Int.down, Vector2Int.right, Vector2Int.down, Vector2Int.right }, PatternType.ZigZag),
        new MovementActionPattern(new List<Vector2Int> { Vector2Int.left, Vector2Int.up, Vector2Int.left, Vector2Int.up }, PatternType.ZigZag),
        new MovementActionPattern(new List<Vector2Int> { Vector2Int.up, Vector2Int.left, Vector2Int.up, Vector2Int.left }, PatternType.ZigZag),
        new MovementActionPattern(new List<Vector2Int> { Vector2Int.left, Vector2Int.down, Vector2Int.left, Vector2Int.down }, PatternType.ZigZag),
        new MovementActionPattern(new List<Vector2Int> { Vector2Int.down, Vector2Int.left, Vector2Int.down, Vector2Int.left }, PatternType.ZigZag),
    };
}

public static class PatternHelper
{
    public static List<Vector2Int> GetAffectedTiles(Vector2Int playerLastPosition, List<Vector2Int> offsets)
    {
        List<Vector2Int> affectedTiles = new List<Vector2Int>();

        foreach (Vector2Int offset in offsets)
        {
            Vector2Int affectedTile = playerLastPosition + offset;
            affectedTiles.Add(affectedTile);
        }

        return affectedTiles;
    }
}

public enum PatternType
{
    Circle,
    Direct,
    L,
    ZigZag,
}

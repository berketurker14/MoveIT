using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "AddItem")]
public class ItemScriptable : ScriptableObject
{
    public Sprite item;// prefab => skills?
    //public Skillset cartcurt;?
    public Sprite effect;
    public string name, definition;
    // rarity is determined by a random value between and if it is between current
    // intervals f.e. 0-100 is 9.99 percent chance of a common item's in the
    // scriptableobject in project 
    [HideInInspector] public enum Rarity { Common, Uncommon, Rare, Epic, Legendary };
    public Rarity rarity;

}

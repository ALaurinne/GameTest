using UnityEngine;

public enum Effect
{
    Life,
    Jump
}

[CreateAssetMenu(menuName = "ScriptableObjects/UsableItem")]
public class UsableItem : Item
{
    public bool isConsumable;

    public Effect effect;
}

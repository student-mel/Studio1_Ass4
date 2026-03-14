using UnityEngine;

[CreateAssetMenu(fileName = "Spell_",  menuName = "Scriptable Objects/Spell Stats", order = 1)]
public class SpellStats : ScriptableObject
{
    public GameObject spellPrefab;
    
    [Header("Threshold Stats")]
    public int meteorThreshold = 10;
    public bool isScalingThreshold = false;
    
    [Header("Fall Stats")]    
    public float fallSpeed = 4f;
    
    [Header("Collection Stats")]
    public float scoreToAdd = 0;
    public float duration = 10f;
    
    public delegate void OnCollected();
    public OnCollected Collected;
}

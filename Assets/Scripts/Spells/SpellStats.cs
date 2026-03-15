using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Spell_",  menuName = "Scriptable Objects/Spell Stats", order = 1)]
public class SpellStats : ScriptableObject
{
    public SpellBehaviour spellPrefab;
    
    [Header("Threshold Stats")]
    public int meteorThreshold = 10;
    public bool isScalingThreshold = false;
    
    [Header("Fall Stats")]    
    public float fallSpeed = 4f;
    
    [Header("Collection Stats")]
    public float scoreToAdd = 0;
    public float duration = 10f;
    
    [Header("Effect Stats")]
    public bool isPhysicsEffect = false;
    public float effectRadius = 1.5f;

    public enum SpellEffect
    {
        Magnet,
        Something
    }
    
    public SpellEffect spellEffect;

    public void Spawn(Transform spawnPoint, SpellsManager spellMan)
    {
        SpellBehaviour spell = Instantiate(spellPrefab,  spawnPoint.position, Quaternion.identity);
        spell.ApplyStats(this, spellMan);
    }
}

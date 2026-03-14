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
    public SpellEffect effect;

    public void Spawn(Transform spawnPoint)
    {
        SpellBehaviour spell = Instantiate(spellPrefab,  spawnPoint.position, Quaternion.identity);
        spell.ApplyStats(this);
    }

    public IEnumerator EffectCoroutine(Transform player)
    {
        float t = 0f;
        while (t < duration)
        {
            effect.Effect(t, player);            
            t += Time.deltaTime;
            yield return null;
        }
    }
}

using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class SpellsManager : MonoBehaviour
{
    public SpellStats[] spells;
    private Transform player;
    

    private void Awake()
    {
        player = transform.parent;
        transform.parent = null;
    }

    public void StartEffect(SpellStats stats)
    {
        Action spell = GetSpellAction(stats);
        StartCoroutine(EffectCoroutine(stats, spell));
    }
    
    public IEnumerator EffectCoroutine(SpellStats stats, Action spellAction)
    {
        float t = 0f;

        while (t < stats.duration)
        {
            spellAction?.Invoke();
            t += Time.deltaTime;

            yield return stats.isPhysicsEffect ? new WaitForFixedUpdate() : null;
        }
    }

    public void MagnetSpell(SpellStats stats)
    {
        Collider2D[] starColls = Physics2D.OverlapCircleAll(player.position, stats.effectRadius, 1 << 6);
        foreach (Collider2D starColl in starColls)
        {
            Vector3 dir = (Vector2)player.position+Vector2.up - starColl.attachedRigidbody.position;
            starColl.attachedRigidbody?.AddForce(dir.normalized * 5f, ForceMode2D.Force);
        }
    }

    private SpellStats GetStatsFromString(string name)
    {
        SpellStats stats = spells.FirstOrDefault(item => item.name == $"Spell_{name}");
        return stats;
    }

    private Action GetSpellAction(SpellStats spell)
    {
        switch (spell.spellEffect)
        {
            case SpellStats.SpellEffect.Magnet:
                return () => MagnetSpell(spell);
            case SpellStats.SpellEffect.Something:
            default:
                return null;
        }
    }

    public bool CheckSpellSpawn(int meteorsShot, out SpellStats stats)
    {
        stats = null;
        foreach (SpellStats s in spells)
        {
            if (meteorsShot >= s.meteorThreshold)
            {
                stats = s;
                return true;
            }
        }
        return false;
    }
}

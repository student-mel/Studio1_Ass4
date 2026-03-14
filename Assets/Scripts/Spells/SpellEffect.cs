using UnityEngine;

public abstract class SpellEffect : MonoBehaviour
{
    public abstract void Effect(float t, Transform player);
}

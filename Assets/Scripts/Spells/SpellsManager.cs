using System.Collections;
using UnityEngine;

public class SpellsManager : MonoBehaviour
{
    public SpellStats[] spells;
    


    public static void MagnetSpell()
    {
        
    }

    IEnumerator MagnetAttract()
    {
        yield return new WaitForSeconds(2f);
    }
}

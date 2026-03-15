using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BgmHandler : MonoBehaviour
{
    public static BgmHandler instance;
    AudioSource audio;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    
}

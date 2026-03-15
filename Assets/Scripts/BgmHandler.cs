using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BgmHandler : MonoBehaviour
{
    AudioSource audio;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
    }
}

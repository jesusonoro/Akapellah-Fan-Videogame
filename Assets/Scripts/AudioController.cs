using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource myAudioSource;

    private GameObject playerGameObject;
    private AudioSource playerAudioSource;

    public AudioClip runningClip;
    public AudioClip bulletExplosion;

    public bool bulletExploded = false;

    void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerAudioSource = playerGameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (bulletExploded == true)
        {
            myAudioSource.clip = bulletExplosion;
            myAudioSource.Play();
            bulletExploded = false;
        }
    }
}

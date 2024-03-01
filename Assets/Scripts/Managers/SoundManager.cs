using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sounds
{
    BGM,
    EnemyHurt,
    GunShoot,
    Pickup,
    PlayerHurt,
    SwordSwipe
}

[Serializable] struct SoundSources
{
    public AudioSource BGM;
    public AudioSource EnemyHurt;
    public AudioSource GunShoot;
    public AudioSource Pickup;
    public AudioSource PlayerHurt;
    public AudioSource SwordSwipe;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] SoundSources soundSources;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public static void PlaySound(Sounds sound)
    {
        switch (sound)
        {
            case Sounds.EnemyHurt:
                Instance.soundSources.EnemyHurt.Play();
                break;

            case Sounds.GunShoot:
                Instance.soundSources.GunShoot.Play();
                break;

            case Sounds.Pickup:
                Instance.soundSources.Pickup.Play();
                break;

            case Sounds.PlayerHurt:
                Instance.soundSources.PlayerHurt.Play();
                break;

            case Sounds.SwordSwipe:
                Instance.soundSources.SwordSwipe.Play();
                break;
        }
    }

    public static void ToggleBGM(bool play)
    {
        if (play)
        {
            Instance.soundSources.BGM.Play();
        }
        else
        {
            Instance.soundSources.BGM.Stop();
        }
    }
}

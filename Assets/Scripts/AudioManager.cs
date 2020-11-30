using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    #region Singleton
    private static AudioManager _instance;

    public static AudioManager Instance => _instance;
    private void Awake()
    {
       

        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    #endregion

    public void PlaySound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodAudio : MonoBehaviour
{
    private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.volume = PlayerPrefs.GetFloat("volume", 1);
    }
}

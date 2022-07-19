using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadVolume : MonoBehaviour
{
    public GameSettingSO game_settings;
    public AudioSource audio_source;

    // Update is called once per frame
    void Update()
    {
        audio_source.volume = game_settings.volume;
    }
}

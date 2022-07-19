using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveVolume : MonoBehaviour
{
    public GameSettingSO game_settings;
    public Slider slider;
    
    public void SetVolume()
    {
        game_settings.volume = slider.value;
    }

}

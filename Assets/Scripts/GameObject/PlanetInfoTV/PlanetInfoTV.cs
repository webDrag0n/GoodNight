using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanetInfoTV : MonoBehaviour
{

    public GameStateSO game_state;
    private TMP_Text tmp_text;

    private void Start()
    {
        tmp_text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (game_state.is_TV_open)
        {
            tmp_text.enabled = true;
        }
        else
        {
            tmp_text.enabled = false;
        }
    }

    public void TextUpdate()
    {
        if (game_state.universe.Size() <= 1)
        {
            return;
        }
        string text = "<" + game_state.currentPlanet.name + ">\n"
            + "===================\n"
            + game_state.currentPlanet.description + "\n\n"
            + "Cordinate:\n" + game_state.currentPlanet.position.ToString() + "\n";
        if (game_state.currentPlanet.owned_by_race)
        {
            text = text + "They own " + game_state.currentPlanet.owned_by_race.owned_planet_amount.ToString() + " planets\n"
            + "Their alert value to us is " + game_state.currentPlanet.owned_by_race.alert_level_to_us.ToString();
        }

        tmp_text.text = text;
    }
}

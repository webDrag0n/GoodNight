using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlanetHumidity
{
    Wet,
    Medium,
    Dry
}

public enum PlanetTemperature
{
    Cold,
    Warm,
    Hot
}
public class Planet
{
    // TODO: all the planet settings go into here
    public string name;
    public string description;
    public Vector2 position;

    public PlanetHumidity humidity = PlanetHumidity.Wet;
    public PlanetTemperature temperature = PlanetTemperature.Warm;

    public Race owned_by_race;

    public void Initialize()
    {
        name = "";
        description = "";
        position = Vector2.zero;
        humidity = PlanetHumidity.Wet;
        temperature = PlanetTemperature.Hot;
        owned_by_race = null;
    }


    public void SerializeIn()
    {

    }

    public string SerializeOut()
    {
        return description;
    }
}

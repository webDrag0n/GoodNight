using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Universe", menuName = "GoodNight/Universe/Universe", order = 1)]
public class Universe : ScriptableObject
{
    public List<Planet> planet_collection = new List<Planet>();

    public void Initialize()
    {
        planet_collection.Clear();
    }

    public Planet GetRandomPlanet()
    {
        if (planet_collection.Count == 0)
        {
            return null;
        }
        int pop_out_index = Random.Range(0, planet_collection.Count);
        Planet pop_out_planet = planet_collection[pop_out_index];
        return pop_out_planet;
    }

    public void AddPlanet(Planet planet)
    {
        planet_collection.Add(planet);
    }

    public void RemovePlanet(Planet planet)
    {
        planet_collection.Remove(planet);
        planet = null;
    }

    public int Size()
    {
        return planet_collection.Count;
    }

    public void Clear()
    {
        planet_collection.Clear();
    }

    public void SerializeIn()
    {

    }

    public string SerializeOut()
    {
        return "";
    }
}

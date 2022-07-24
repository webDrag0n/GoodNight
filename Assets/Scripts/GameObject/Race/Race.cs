using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Race", menuName = "GoodNight/Race/Race", order = 1)]
public class Race : ScriptableObject
{
    [TextArea(8, 30)]
    public string import_data = "";
    public string name = "";
    public int[] avatar_code = new int[3];
    public float alert_level_to_us = 0;
    public string description = "";
    public int planet_destroyed = 0;
    public List<string> planet_destroy_record;

    public int owned_planet_amount = 1;

    public int technogy_level = 0;

    // {level 1, lv 2, lv 3, ...}
    public int[] weapon_storage = { 0, 0, 0, 0, 0 };

    public float expansion_rate = 0;
    public float evolve_rate = 0;

    // adaptability to 9 different environment prototypes in env proto collections
    public PlanetHumidity prefered_humidity;
    public PlanetTemperature prefered_temperature;

    public void Initialize()
    {
        name = "";
        alert_level_to_us = 0;
        description = "";
        planet_destroyed = 0;
        planet_destroy_record = new List<string>();
        owned_planet_amount = 1;
        technogy_level = 0;
        for (int i = 0; i < weapon_storage.Length; i++)
        {
            weapon_storage[i] = 0;
        }
        expansion_rate = 0;
    }

    private void Awake()
    {
        if (import_data != "") ParseDataFromString(import_data);
    }

    public void Copy(Race target)
    {
        // copy all attributes

    }

    public void Upgrade()
    {
        technogy_level += 1;
    }

    public void ParseDataFromString(string raw)
    {
        string[] raw_arr = raw.Split("\n"[0]);
        List<string> data_list = new List<string>();
        foreach (string line in raw_arr)
        {
            if (line.Length > 0)
            {
                data_list.Add(line);
            }
        }
        name = data_list[0];
        alert_level_to_us = UnityEngine.Random.Range(0, 0.1f);
        description = data_list[1];
        planet_destroyed = int.Parse(data_list[2]);
        owned_planet_amount = int.Parse(data_list[3]);
        technogy_level = int.Parse(data_list[4]);
        expansion_rate = 0;
        switch (data_list[5])
        {
            case "0":
                prefered_humidity = PlanetHumidity.Wet;
                break;
            case "1":
                prefered_humidity = PlanetHumidity.Medium;
                break;
            case "2":
                prefered_humidity = PlanetHumidity.Dry;
                break;
        }
        switch (data_list[6])
        {
            case "0":
                prefered_temperature = PlanetTemperature.Cold;
                break;
            case "1":
                prefered_temperature = PlanetTemperature.Warm;
                break;
            case "2":
                prefered_temperature = PlanetTemperature.Hot;
                break;
        }
        evolve_rate = float.Parse(data_list[7]);

    }



    public float CalculateKillPoint()
    {
        return technogy_level * expansion_rate * owned_planet_amount;
    }

    //public void SetRandomValues()
    //{
    //    // TODO: Finished the algorithm of generating random race values
    //    name = "Alien_" + Random.Range(0, 1000);
    //    alert_level_to_us = Random.Range(0, 1);
    //    description = "";
    //    prefered_humidity = 
    //    for (int i = 0; i < 9; i++)
    //    {
    //        adaptability_to_environments[i] = Random.Range(0, 1);
    //    }
    //}

    public void LoadRacePrototype(Race race_prototype)
    {
        name = race_prototype.name;
        description = race_prototype.description;
        alert_level_to_us = race_prototype.alert_level_to_us;
        prefered_humidity = race_prototype.prefered_humidity;
        prefered_temperature = race_prototype.prefered_temperature;
    }

    public void SerializeIn()
    {

    }

    public string SerializeOut()
    {
        return description;
    }
}

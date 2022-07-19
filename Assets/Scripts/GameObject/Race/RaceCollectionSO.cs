using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RaceCollection", menuName = "GoodNight/Race/RaceCollection", order = 2)]
public class RaceCollectionSO : ScriptableObject
{
    public List<Race> races_collection;

    public void Initialize()
    {
        races_collection.Clear();
    }

    public void DeepCopy(RaceCollectionSO target)
    {
        races_collection.Clear();
        for (int i = 0; i < target.races_collection.Count; i++)
        {
            races_collection.Add(target.races_collection[i]);
        }
    }

    public Race Get(int index)
    {
        return races_collection[index];
    }

    public void AddNewRace(Race race)
    {
        races_collection.Add(race);
    }

    public int Size()
    {
        return races_collection.Count;
    }
}

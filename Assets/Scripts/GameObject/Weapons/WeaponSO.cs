using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Tank,
    Plane,
    Rocket,
    Laser,
    DuelVectorFoil
}

[CreateAssetMenu(fileName = "Weapon", menuName = "GoodNight/Weapon/Weapon", order = 2)]
public class WeaponSO : ScriptableObject
{
    public string name = "";
    public string description = "";
    public int amount = 0;
    public void SerializeIn()
    {

    }

    public string SerializeOut()
    {
        return description;
    }
}

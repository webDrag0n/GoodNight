using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponCollection", menuName = "GoodNight/Weapon/WeaponCollection", order = 1)]
public class WeaponCollectionSO : ScriptableObject
{
    // stores all the 
    public List<WeaponSO> weapon_collection;

    public void Initialize()
    {
        weapon_collection.Clear();
    }

}

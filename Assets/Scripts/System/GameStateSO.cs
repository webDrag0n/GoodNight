using System.Collections.Generic;
using UnityEngine;

public enum GameStages
{
    AwaitPlanetLoad,
    Default,
    Wins,
    Dead,
    Paused
}

[CreateAssetMenu(fileName = "GameState", menuName = "GoodNight/GameState", order = 2)]
public class GameStateSO : ScriptableObject
{
    public GameStages game_stage = GameStages.Default;

    public Race player;

    public RaceCollectionSO runtime_races_collection;
    public WeaponCollectionSO runtime_weapon_collection;

    public Universe universe;
    public Planet currentPlanet;

    public bool no_touch = false;

    public Planet[] planet_bookmark;
    public bool is_bookmark_open = false;
    public bool is_TV_open = true;

    public int round = 0;

    public void Initialize()
    {
        game_stage = GameStages.Default;

        player.Initialize();

        runtime_races_collection.Initialize();
        runtime_weapon_collection.Initialize();

        universe.Initialize();
        currentPlanet.Initialize();

        no_touch = false;

        round = 0;
    }

    public void LoadSave(GameStateSO save)
    {

    }

}

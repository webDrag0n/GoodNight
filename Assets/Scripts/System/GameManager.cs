using Math = System.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float auto_save_time = 60;
    public float kill_point_required_to_kill_player = 0.5f;

    public int start_planet_amount;
    public int start_race_amount;

    public GameStateSO game_state;
    public GameStateSO game_checkpoint;
    public GameSettingSO game_settings;
    public GameSettingSO game_settings_default;

    public WeaponCollectionSO deafault_weapon_collection;
    public RaceCollectionSO default_races_collection;

    public PlanetRenderer planet_renderer;
    public GameObject ui_shield;

    public PlanetInfoTV planet_info_tv;

    public TMP_Text universe_size_value;

    public GameObject win_UI;
    public TMP_Text win_UI_PlanetDestroyedValue;
    public GameObject dead_UI;
    public TMP_Text dead_UI_message;
    public TMP_Text dead_UI_PlanetDestroyedValue;

    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        game_state.game_stage = GameStages.AwaitPlanetLoad;
        // copy default races collection to runtime
        InitRacePool(game_state.runtime_races_collection);
        // clear and populate universe with planets
        ClearUniverse(game_state.universe);
        InitUniverse(game_state.universe);
    }

    public void NextLoop()
    {
        // if no planets left, player wins
        if (game_state.universe.Size() <= 0)
        {
            game_state.game_stage = GameStages.Wins;
            win_UI_PlanetDestroyedValue.text = game_state.player.planet_destroyed.ToString();

        }
        else
        {

            // get a new planet from the waiting list to render
            game_state.currentPlanet = game_state.universe.GetRandomPlanet();

            // update planet info TV
            planet_info_tv.TextUpdate();

            // trigger renderer update
            planet_renderer.planet = game_state.currentPlanet;
            planet_renderer.RenderPlanet();
            universe_size_value.text = game_state.universe.Size().ToString();

            planet_renderer.animator.SetTrigger("next");
            planet_renderer.effects_animator.SetTrigger("next");
            game_state.game_stage = GameStages.Default;

            // iterate through the runtime race collection
            for (int i = 0; i < game_state.runtime_races_collection.Size(); i++)
            {
                // skip null races
                if (!game_state.runtime_races_collection.Get(i)) continue;

                // expand a new planet for some random races
                if (game_state.runtime_races_collection.Get(i).expansion_rate > Random.Range(0f, 1f))
                {
                    // init a new planet and set race settings
                    Planet planet = new Planet();
                    planet.owned_by_race = game_state.runtime_races_collection.Get(i);
                    if (planet.owned_by_race)
                    {
                        planet.owned_by_race.owned_planet_amount += 1;
                    }
                    game_state.universe.AddPlanet(planet);
                }

                // calculate the kill point of current race
                float kill_point = game_state.runtime_races_collection.Get(i).CalculateKillPoint();
                float roll_point = Random.Range(0f, kill_point);
                Debug.Log(roll_point);

                // check if this race could kill player
                if (roll_point >= kill_point_required_to_kill_player)
                {
                    // play the killing animation base on the killing weapon

                    // player killed, game over
                    game_state.game_stage = GameStages.Dead;
                    // record the race and weapon used that killed player
                    Race killer = game_state.runtime_races_collection.Get(i);
                    dead_UI_message.text = "You are killed by " + killer.name + " with Rocket.\n"
                        + "Their technology is at level " + killer.technogy_level + ", they have:\n"
                        + killer.weapon_storage[2] + " Rockets\n"
                        + killer.weapon_storage[3] + " Lasers\n"
                        + killer.weapon_storage[4] + " Dual Vector Foils.";
                    dead_UI_PlanetDestroyedValue.text = game_state.player.planet_destroyed.ToString();
                    // player became the race that killed player
                    game_state.player = killer;
                    Debug.Log("Player Killed");
                    dead_UI.SetActive(true);
                }

            }

            // player upgrade every 10 kills
            if (game_state.player.planet_destroyed % 10 == 0)
            {
                game_state.player.Upgrade();
            }

            // player's gets a random weapon every 5 rounds
            if (game_state.round % 5 == 0)
            {
                game_state.player.weapon_storage[Random.Range(2, 5)] += 1;
            }
        }

        game_state.round += 1;
    }

    private void Update()
    {
        ui_shield.SetActive(game_state.no_touch);

        // main round loop, executes when player performs an action on planet
        if (game_state.game_stage == GameStages.AwaitPlanetLoad)
        {
            NextLoop();
        }

        if (game_state.game_stage == GameStages.Wins)
        {
            win_UI.SetActive(true);
        }
        else if (game_state.game_stage == GameStages.Dead)
        {
            dead_UI.SetActive(true);
        }
    }

    public void SetGameStage(GameStages _game_stage)
    {
        game_state.game_stage = _game_stage;
    }

    public void PassCurrentPlanet()
    {
        // Disable any input before animation complete
        game_state.no_touch = true;
        
        planet_renderer.animator.SetBool("is_boom_boom", false);
        planet_renderer.effects_animator.SetBool("is_boom_boom", false);
        planet_renderer.animator.SetBool("is_bzzz_bzzz", false);
        planet_renderer.effects_animator.SetBool("is_bzzz_bzzz", false);
        planet_renderer.animator.SetBool("is_pop_pop", false);
        planet_renderer.effects_animator.SetBool("is_pop_pop", false);
        // Trigger pass planet animation
        planet_renderer.animator.SetTrigger("next");

        // Trigger pass planet animation
        planet_renderer.effects_animator.SetTrigger("next");
    }

    public void DestroyCurrentPlanetByRocket()
    {
        if (game_state.player.weapon_storage[2] >= 1)
        {
            DestroyCurrentPlanet(WeaponType.Rocket);
            game_state.player.weapon_storage[2] -= 1;
        }
    }

    public void DestroyCurrentPlanetByLaser()
    {
        if (game_state.player.weapon_storage[3] >= 1)
        {
            DestroyCurrentPlanet(WeaponType.Laser);
            game_state.player.weapon_storage[3] -= 1;
        }
    }

    public void DestroyCurrentPlanetByDualVectorFoil()
    {
        if (game_state.player.weapon_storage[4] >= 1)
        {
            DestroyCurrentPlanet(WeaponType.DuelVectorFoil);
            game_state.player.weapon_storage[4] -= 1;
        }
    }

    private void DestroyCurrentPlanet(WeaponType weapon_type)
    {
        // TODO: 
        // reset all animation bools
        planet_renderer.animator.SetBool("is_boom_boom", false);
        planet_renderer.effects_animator.SetBool("is_boom_boom", false);
        planet_renderer.animator.SetBool("is_bzzz_bzzz", false);
        planet_renderer.effects_animator.SetBool("is_bzzz_bzzz", false);
        planet_renderer.animator.SetBool("is_pop_pop", false);
        planet_renderer.effects_animator.SetBool("is_pop_pop", false);
        // Disable any input before animation complete
        game_state.no_touch = true;

        // decrease that type of weapon by one
        string weapon_name = "";
        switch (weapon_type)
        {
            case(WeaponType.Rocket):
                planet_renderer.animator.SetBool("is_boom_boom", true);
                planet_renderer.effects_animator.SetBool("is_boom_boom", true);
                weapon_name = "Rocket";
                break;
            case (WeaponType.Laser):
                planet_renderer.animator.SetBool("is_bzzz_bzzz", true);
                planet_renderer.effects_animator.SetBool("is_bzzz_bzzz", true);
                weapon_name = "Laser";
                break;
            case (WeaponType.DuelVectorFoil):
                planet_renderer.animator.SetBool("is_pop_pop", true);
                planet_renderer.effects_animator.SetBool("is_pop_pop", true);
                weapon_name = "Duel Vector Foil";
                break;
        }

        // Trigger destroy planet animation
        planet_renderer.animator.SetTrigger("next");
        planet_renderer.effects_animator.SetTrigger("next");

        game_state.player.planet_destroyed += 1;
        game_state.player.planet_destroy_record.Add(game_state.currentPlanet.name + ": " + weapon_name);
        game_state.universe.RemovePlanet(game_state.currentPlanet);
        game_state.currentPlanet = null;
    }
    public void InitUniverse(Universe universe)
    {
        // check for any vacancy in the universe and fill them with new planet
        for (int i = 0; i < default_races_collection.Size(); i++)
        {
            // init a new planet and set race settings
            Planet planet = new Planet();
            string name = "";
            planet.position = new Vector2(Random.Range(-100000, 100000), Random.Range(-100000, 100000));
            
            planet.owned_by_race = game_state.runtime_races_collection.Get(i);

            if (game_state.runtime_races_collection.Get(i))
            {
                game_state.runtime_races_collection.Get(i).owned_planet_amount = 1;

                planet.humidity = game_state.runtime_races_collection.Get(i).prefered_humidity;
                planet.temperature = game_state.runtime_races_collection.Get(i).prefered_temperature;

                game_state.universe.AddPlanet(planet);
            }
            else
            {
                switch (Random.Range(0, 2))
                {
                    case 0:
                        planet.humidity = PlanetHumidity.Dry;
                        break;
                    case 1:
                        planet.humidity = PlanetHumidity.Medium;
                        break;
                    case 2:
                        planet.humidity = PlanetHumidity.Wet;
                        break;
                }
                switch (Random.Range(0, 2))
                {
                    case 0:
                        planet.temperature = PlanetTemperature.Hot;
                        break;
                    case 1:
                        planet.temperature = PlanetTemperature.Warm;
                        break;
                    case 2:
                        planet.temperature = PlanetTemperature.Cold;
                        break;
                }
            }

            name += planet.humidity.ToString()[0] + planet.temperature.ToString()[0];
            name += planet.position.x < 0 ? "N" : "P";
            name += planet.position.y < 0 ? "N" : "P";
            name += (Math.Abs(planet.position.x) + Math.Abs(planet.position.y)).ToString();
            planet.name = name;
            planet.description = "A " + planet.humidity.ToString() + ", " + planet.temperature.ToString();
            if (planet.owned_by_race)
            {
                planet.description += " planet owned by " + planet.owned_by_race.name;
            }
        }
    }

    public void ClearUniverse(Universe universe)
    {
        universe.Clear();
    }

    public void InitRacePool(RaceCollectionSO runtime_race_collection)
    {
        // first load all preset races
        // contains preset and null races
        runtime_race_collection.DeepCopy(default_races_collection);

        for (int i = 0; i < runtime_race_collection.Size(); i++)
        {
            if (runtime_race_collection.Get(i) != null)
            {
                // all races, set init owned planet to 0
                runtime_race_collection.Get(i).owned_planet_amount = 0;
            }
        }
    }

    public void ClearArmoryCrate(WeaponSO[] armory_crate)
    {
        for (int i = 0; i < armory_crate.Length; i++) armory_crate[i] = null;
    }


    public void SaveGame()
    {
        // save game_state to game save file
    }

    public void LoadGame()
    {
        // load game save file to game_state
    }

    public void Restart()
    {
        Initialize();
    }

    public void Shutdown()
    {
        Application.Quit();
    }
}

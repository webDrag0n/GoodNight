using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRenderer : MonoBehaviour
{
    public Planet planet;

    // TODO: get all components
    public Sprite[] planet_bases;
    public Sprite[] L1_buildings;
    public Sprite[] L2_buildings;
    public Sprite[] L3_buildings;
    public Sprite[] L4_buildings;
    public Sprite[] L5_buildings;
    public SpriteRenderer[] building_points;
    public Sprite[] trees;
    public SpriteRenderer[] tree_points;

    public Sprite[] L1_military;
    public Sprite[] L2_military;
    public Sprite[] L3_military;
    public Sprite[] L4_military;
    public Sprite[] L5_military;

    public Sprite[] L1_plane;
    public Sprite[] L2_plane;
    public Sprite[] L3_plane;
    public Sprite[] L4_plane;
    public Sprite[] L5_plane;

    public SpriteRenderer[] military_points;
    public SpriteRenderer[] plane_points;
    
    public Sprite[] planet_clouds;
    public Sprite[] wet_planet_fragments;
    public Sprite[] normal_planet_fragments;
    public Sprite[] dry_planet_fragments;

    public SpriteRenderer[] fragment_points;

    public SpriteRenderer planet_renderer;
    public SpriteRenderer effect_renderer;
    public SpriteRenderer cloud_renderer;

    public Animator animator;
    public Animator effects_animator;

    private AvatarGenerator avatar_gen;

    // TODO: Load art resources required to assemble a planet here

    // Start is called before the first frame update
    void Start()
    {
        //planet_renderer = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
        avatar_gen = GetComponent<AvatarGenerator>();
    }

    /**
     *  max num is 3 since there are only 3 placing points
     */
    private void RandomPut(Sprite[] sprites, SpriteRenderer[] spriteRenderers, int num = 0)
    {
        for (int i = 0; i < num; i++)
        {
            if (sprites[i] == null) spriteRenderers[i].sprite = null;
            spriteRenderers[i].sprite = sprites[i];
        }
    }

    public void RenderPlanet()
    {
        // TODO: Instantiate art pieces and assemble the planet
        // fragments
        switch (planet.humidity)
        {
            case PlanetHumidity.Wet:
                // use wet base
                RandomPut(wet_planet_fragments, fragment_points, 8);
                break;
            case PlanetHumidity.Medium:
                // use medium base
                RandomPut(normal_planet_fragments, fragment_points, 8);
                break;
            case PlanetHumidity.Dry:
                // use dry base
                RandomPut(dry_planet_fragments, fragment_points, 8);
                break;
        }
        // base (Temperature: set color, Humidity: choose base and cloud)
        switch (planet.humidity)
        {
            case PlanetHumidity.Wet:
                // use wet base
                planet_renderer.sprite = planet_bases[0];
                break;
            case PlanetHumidity.Medium:
                // use medium base
                planet_renderer.sprite = planet_bases[1];
                break;
            case PlanetHumidity.Dry:
                // use dry base
                planet_renderer.sprite = planet_bases[2];
                break;
        }
        // clouds around
        switch (planet.humidity)
        {
            case PlanetHumidity.Wet:
                // use wet cloud
                cloud_renderer.sprite = planet_clouds[1];
                break;
            case PlanetHumidity.Medium:
                // use medium cloud
                cloud_renderer.sprite = planet_clouds[5];
                break;
            case PlanetHumidity.Dry:
                // use dry cloud
                cloud_renderer.sprite = planet_clouds[3];
                break;
        }

        if (planet.owned_by_race)
        {
            avatar_gen.RenderAvatar(
                planet.owned_by_race.avatar_code[0],
                planet.owned_by_race.avatar_code[1],
                planet.owned_by_race.avatar_code[2]
            );
            // military
            switch (planet.owned_by_race.technogy_level)
            {
                case 0:
                    // no weapons

                    break;
                case 1:
                    // use medium base
                    RandomPut(L1_military, military_points, 2);
                    RandomPut(L1_plane, plane_points, 2);
                    break;
                case 2:
                    // use dry base
                    RandomPut(L2_military, military_points, 2);
                    RandomPut(L2_plane, plane_points, 2);
                    break;
                case 3:
                    // use dry base
                    RandomPut(L3_military, military_points, 2);
                    RandomPut(L3_plane, plane_points, 2);
                    break;
                case 4:
                    // use dry base
                    RandomPut(L4_military, military_points, 2);
                    RandomPut(L4_plane, plane_points, 2);
                    break;
                case 5:
                    RandomPut(L5_military, military_points, 2);
                    RandomPut(L5_plane, plane_points, 2);
                    break;
            }

            // buildings
            // randomly place buildings into building points
            switch (planet.owned_by_race.technogy_level)
            {
                case 0:
                    // no buildings

                    break;
                case 1:
                    RandomPut(L1_buildings, building_points, 2);
                    break;
                case 2:
                    RandomPut(L2_buildings, building_points, 2);
                    break;
                case 3:
                    RandomPut(L3_buildings, building_points, 2);
                    break;
                case 4:
                    RandomPut(L4_buildings, building_points, 2);
                    break;
                case 5:
                    RandomPut(L5_buildings, building_points, 2);
                    break;
            }

        }

        // trees
        switch (planet.humidity)
        {
            case PlanetHumidity.Wet:
                // use wet trees
                for (int i = 0; i < tree_points.Length; i++)
                {
                    tree_points[i].sprite = trees[0];
                }
                break;
            case PlanetHumidity.Medium:
                // use medium trees
                for (int i = 0; i < tree_points.Length; i++)
                {
                    tree_points[i].sprite = trees[1];
                }
                break;
            case PlanetHumidity.Dry:
                // use dry trees
                for (int i = 0; i < tree_points.Length; i++)
                {
                    tree_points[i].sprite = trees[2];
                }
                break;
        }

        // env (cloud cover)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

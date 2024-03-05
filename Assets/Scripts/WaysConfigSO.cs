using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//how this asset can be created in Unity's editor. It defines the asset's menu name ("Wave Config") and the default file name when creating a new instance of this asset ("New Wave Config").

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaysConfigSO : ScriptableObject // store data and create custom asset types to be used in game
{
    [SerializeField] List<GameObject> enemy_prefabs;
    [SerializeField] Transform path_prefab; // transform type gives position , rotation and scale
    [SerializeField] float move_speed = 5f;
    [SerializeField] float time_between_enemy_spawns = 1f;
    [SerializeField] float spawn_time_variance = 0.5f; //enenmy showing period difference. any value between 0 and o.5f
    [SerializeField] float minimum_spawn_time = 0.2f;



    //get the enemy count
    public int GetEnemyCount()
    {
        return enemy_prefabs.Count;
    }


    //get the enemy prefab at given index
    public GameObject GetEnemyPrefab(int index)
    {
        return enemy_prefabs[index];
    }

    public Transform GetStartingWaypoint()
    {
        return path_prefab.GetChild(0);
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> way_points = new List<Transform>();
        foreach (Transform child in path_prefab)
        {
            way_points.Add(child);
        }
        return way_points;
    }

    public float GetMoveSpeed()
    {
        return move_speed;
    }
    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(time_between_enemy_spawns - spawn_time_variance,
                                        time_between_enemy_spawns + spawn_time_variance);
        return Mathf.Clamp(spawnTime, minimum_spawn_time, float.MaxValue);
    }

}

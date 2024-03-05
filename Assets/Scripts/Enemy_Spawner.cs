using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{

    [SerializeField] List<WaysConfigSO> wave_configs;
    [SerializeField] float timeBetweenWaves = 0f; // time between the waves appear after each other
    WaysConfigSO current_wave; // to get the count and prefab
    [SerializeField] bool is_looping; //control the condition of our do while loop (do while loop we want at least one time to run this wave)
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaysConfigSO GetCurrentWave()
    {
        return current_wave;
    }

    IEnumerator SpawnEnemyWaves()
    {

        do
        {
            //here i loop through a list of waysCOnfigOS
            //on each loop i am setting the value of curret wave , then loop through the all the elemets in that wave and then waitfor time between waves
            foreach (WaysConfigSO wave in wave_configs)
            {

                current_wave = wave;
                for (int i = 0; i < current_wave.GetEnemyCount(); i++)
                {
                    Instantiate(current_wave.GetEnemyPrefab(i),
                                current_wave.GetStartingWaypoint().position,
                                Quaternion.Euler(0, 0, 180), // to rotate the object that the enemy is shooting down ward
                                transform);
                    yield return new WaitForSeconds(current_wave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }while (is_looping);

    }
}



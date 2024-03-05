using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour

{
    Enemy_Spawner enemy_spawner;
    WaysConfigSO wave_config;
    List<Transform> way_points;
    int way_point_index = 0;


    void Awake()
    {
        enemy_spawner = FindObjectOfType<Enemy_Spawner>();
    }

    // Start is called before the first frame update

    void Start()
    {
        wave_config = enemy_spawner.GetCurrentWave();
        way_points = wave_config.GetWaypoints();
        transform.position = way_points[way_point_index].position;

    }
    

    // Update is called once per frame
    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (way_point_index < way_points.Count) // we know we are not at the last point yet. then enemy should move on to next point in a particular speed
        {
            Vector3 target_position = way_points[way_point_index].position; //hwere we go
            float delta = wave_config.GetMoveSpeed() * Time.deltaTime; // how fast we go . distance we go each
            transform.position = Vector2.MoveTowards(transform.position, target_position, delta);// this move to the next poition from currnet poition from delta speed
            if (transform.position == target_position)
            {
                way_point_index++;
            }
        }
        else
        {
            Destroy(gameObject);//when enemy go the last waypoint it should be destroyed
        }
    }

}

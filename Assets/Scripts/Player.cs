using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    Vector2 raw_input;
    [SerializeField] float move_speed  = 6f;
    [SerializeField] float padding_left = 0.5f;
    [SerializeField] float padding_right = 0.5f;
    [SerializeField] float padding_top = 5f;
    [SerializeField] float padding_bottom = 2f;


    Vector2 min_bounds;//value for bottom left of the viewport
    Vector2 max_bounds;//value for top right of the viewport

    shooter Shooter; //creating shooter object

    void Awake()
    {
        Shooter = GetComponent<shooter>(); // to get componets in unity
    }



    void Start()
    {
        init_bounds();
    }

    void Update()
    { 
        Move();
    }

    void Move()
    {
        Vector2 delta = raw_input * move_speed * Time.deltaTime;
        Vector2 new_pos = new Vector2();
        new_pos.x = Mathf.Clamp(transform.position.x + delta.x, min_bounds.x + padding_left, max_bounds.x - padding_right);
        new_pos.y = Mathf.Clamp(transform.position.y + delta.y, min_bounds.y + padding_bottom, max_bounds.y - padding_top);
        transform.position = new_pos;


    }

    // to initialize the max and min bounds
    void init_bounds()
    {
        Camera mainCamera = Camera.main;
        min_bounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        max_bounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }


    //To intercept with the moveon call which is a method in input system
    void OnMove(InputValue value)
    {
        raw_input =value.Get<Vector2>();
        Debug.Log(raw_input);
    }
    void OnFire(InputValue value)
    {
        if (Shooter != null)
        {
            Shooter.isFiring = value.isPressed;
        }

    }

}

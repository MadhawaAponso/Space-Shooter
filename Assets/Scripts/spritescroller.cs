using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spritescroller : MonoBehaviour
{
    [SerializeField] Vector2 move_speed;//to give controll over x and y in our texture

    Vector2 offset;
    Material material;

    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        // make our maririal scroll
        
        offset = move_speed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }

}

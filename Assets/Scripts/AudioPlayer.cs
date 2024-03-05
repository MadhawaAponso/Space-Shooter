using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")] // shooting section
    [SerializeField] AudioClip shooting_clip;
    [SerializeField][Range(0f, 1f)] float shooting_volume = 1f;//constrain the volume between the given range

    [Header("Damage")] // damage section
    [SerializeField] AudioClip damage_clip;
    [SerializeField][Range(0f, 1f)] float damage_volume = 1f;


    public void PlayShootingClip()
    {
        PlayClip(shooting_clip, shooting_volume);
    }

    public void PlayDamageClip()
    {
        PlayClip(damage_clip, damage_volume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }

}

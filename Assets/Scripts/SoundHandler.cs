using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    private AudioSource[] Sounds;

    private AudioSource JumpSound; 
    private AudioSource DieSound;
    private AudioSource PointSound;
    // Start is called before the first frame update
    void Start()
    {
        Sounds = GetComponents<AudioSource>();
        JumpSound = Sounds[0];
        DieSound = Sounds[1];
        PointSound = Sounds[2];
    }

    public void PlayJumpSound()
    {
      JumpSound.Play();   
    }

    public void PlayDieSound()
    {
        DieSound.Play();
    }

    public void PlayPointSound()
    {
        PointSound.Play();
    }


}

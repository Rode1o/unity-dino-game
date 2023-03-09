using System;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJumpSound()
    {
      audioSource.PlayOneShot(sound1);  
    }

    public void PlayDieSound()
    {
        audioSource.PlayOneShot(sound2);  
    }
    


}

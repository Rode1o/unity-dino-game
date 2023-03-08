using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(SoundHandler))]
public class Player : MonoBehaviour
{
    private SoundHandler Sounds;
    private CharacterController character;
    private Vector3 direction;

    public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;

    private void Start()
    {
        Sounds.GetComponent<SoundHandler>();
    }

    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            if (Input.GetButton("Jump")) {
                Sounds.PlayJumpSound();
                direction = Vector3.up * jumpForce;
            }
        }

        character.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) {
            FindObjectOfType<GameManager>().GameOver();
        }
    }

}

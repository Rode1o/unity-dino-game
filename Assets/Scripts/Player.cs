using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;
    public SoundHandler soundHandler;
    public float jetForce = 15f;
    //public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;
    public float maxUpwardSpeed = 10f;
    public float maxDownwardSpeed = -15f;
    public float groundedResetSpeed = 0f;

    private void Start()
    {
    }

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        direction = Vector3.zero;
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (character.isGrounded)
        {
            direction.y = groundedResetSpeed;
        }

        if (Input.GetButton("Jump"))
        {
            direction.y += jetForce * Time.fixedDeltaTime;
        }
        else
        {
            direction.y -= gravity * Time.fixedDeltaTime;
        }

        direction.y = Mathf.Clamp(direction.y, maxDownwardSpeed, maxUpwardSpeed);
        character.Move(direction * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) {
            soundHandler.PlayDieSound();
            Invoke("Find",0.2f); 
        }
    }

    private void Find()
    {
        FindObjectOfType<GameManager>().GameOver();
    }

}

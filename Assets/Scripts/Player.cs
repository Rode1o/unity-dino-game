using System;
using UnityEngine;

public enum GameEntityType
{
    Obstacle,
    Collectable
}

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

    private RewardManager rewardManager;
    private HealthComponent health;

    private void Start()
    {
    }

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        direction = Vector3.zero;
        rewardManager = FindObjectOfType<RewardManager>();
        if (rewardManager == null)
        {
            Debug.LogError("RewardManager no encontrado.");
        }
        health = GetComponent<HealthComponent>();
        if (health == null)
            Debug.LogError("HealthComponent no encontrado.");
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
        if (health != null)
        {
            health.OnDeath += OnPlayerDeath;
        }
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
        try
        {
            GameEntityType entityType = GetEntityTypeFromTag(other.tag);
            switch (entityType)
            {
                case GameEntityType.Obstacle:
                    health.TakeDamage(25f);
                    soundHandler.PlayDieSound();
                    break;
                case GameEntityType.Collectable:
                    rewardManager.Collect();
                    Destroy(other.gameObject);
                    break;
            }
        }
        catch (System.ArgumentException e)
        {
            Debug.LogWarning(e.Message);
            // Aquí puedes manejar el caso en el que el tag no se reconoce, si es necesario
        }
    }
    private GameEntityType GetEntityTypeFromTag(string tag)
    {
        switch (tag)
        {
            case "Obstacle":
                return GameEntityType.Obstacle;
            case "Collectable":
                return GameEntityType.Collectable;
            default:
                throw new System.ArgumentException("Tag no reconocido: " + tag);
        }
    }

     private void OnPlayerDeath()
    {
        Invoke("Find", 0.2f);
    }

    private void Find()
    {
        FindObjectOfType<GameManager>().GameOver();
    }

}

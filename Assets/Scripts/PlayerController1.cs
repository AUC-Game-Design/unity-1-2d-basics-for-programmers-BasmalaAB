using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;


public class PlayerController1 : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputAction MoveAction;
    public Rigidbody2D rigidbody2d;
    Vector2 move2;
    Vector2 move;
    public InputAction WASD_Action;
    public float speed = 3.0f;

    public int maxHealth = 5;
    int currentHealth;
   // PlayerController controller;  //Defines which character is currently active
    public int health { get { return currentHealth; } }

    // Variables related to temporary invincibility
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;

    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
        MoveAction.Enable();
        //rigidbody2d = GetComponent<Rigidbody2D>();

        WASD_Action.Enable();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        move2 = WASD_Action.ReadValue<Vector2>();

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        /*if(Keyboard.current.leftShiftKey.IsPressed() && controller.current_player == "Sugar")
        {
            controller.current_player = "Ruby";
            
        }
        else
        {
            controller.current_player = "Sugar";
            
        }*/
    }

    private void FixedUpdate()
    {
       /* if (controller.current_player == "Sugar")
        {
            Vector2 position = (Vector2)transform.position + move * speed * Time.deltaTime;
            transform.position = position;

            Vector2 position2 = (Vector2)transform.position + move2 * speed * Time.deltaTime;
            transform.position = position2;
        }*/
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            damageCooldown = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }

}

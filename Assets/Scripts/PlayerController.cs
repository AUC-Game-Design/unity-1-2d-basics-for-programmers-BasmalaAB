using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;


public class PlayerController : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;

    public int maxHealth = 5;
    int currentHealth;

    Vector2 move2;
    Vector2 move;
    public InputAction WASD_Action;
    public float speed = 3.0f;
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();

        WASD_Action.Enable();
        currentHealth = maxHealth;


    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        Debug.Log(move);
        

        move2 = WASD_Action.ReadValue<Vector2>();
        Debug.Log(move2);
        
    }

    private void FixedUpdate()
    {
        Vector2 position = (Vector2)transform.position + move * speed * Time.deltaTime;
        transform.position = position;

        Vector2 position2 = (Vector2)transform.position + move2 * speed * Time.deltaTime;
        transform.position = position2;
    }

    void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);

    }

}

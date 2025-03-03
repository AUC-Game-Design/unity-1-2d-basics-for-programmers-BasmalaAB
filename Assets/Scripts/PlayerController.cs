using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.InputSystem.XR;


public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;

    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);

    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move2;
    Vector2 move;
    public InputAction WASD_Action;
    public float speed = 3.0f;
    private string text_displayed = "Help me!!";

    public int maxHealth = 5;
    int currentHealth;
    public int health { get { return currentHealth; } }


    // Variables related to temporary invincibility
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;

    //public string current_player = "Ruby";

    public InputAction talkAction;

    void Start()
    {
        MoveAction.Enable();
        WASD_Action.Enable();
        talkAction.Enable();

        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        move2 = WASD_Action.ReadValue<Vector2>();

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
        if (!Mathf.Approximately(move2.x, 0.0f) || !Mathf.Approximately(move2.y, 0.0f))
        {
            moveDirection.Set(move2.x, move2.y);
            moveDirection.Normalize();
            move = move2;
        }

        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Pressed X");
            FindFriend();
        }

        /*if (Keyboard.current.leftShiftKey.IsPressed() && current_player == "Ruby")
        {
            current_player = "Sugar";
        }
        else
        {
            current_player = "Ruby";
            
        }*/
    }

    private void FixedUpdate()
    {
            Vector2 position = (Vector2)transform.position + move * speed * Time.deltaTime;
            transform.position = position;

            //Vector2 position2 = (Vector2)transform.position + move2 * speed * Time.deltaTime;
            //transform.position = position2;
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            damageCooldown = timeInvincible;
            animator.SetTrigger("Hit");
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(moveDirection, 300);

        animator.SetTrigger("Launch");
    }

    void FindFriend()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, moveDirection, 2.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {
            NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
            if (character != null)
            {
                if (character.identity == "Froggo")
                {
                    text_displayed = "Hey! Help me fix all those broken robots! Either one of you works";
                    UIHandler.instance.DisplayDialogue(text_displayed);
                }
                else if(character.identity == "Martha")
                {
                    text_displayed = "I'm looking for my lost book... have you seen it anywhere, Ruby?";
                    UIHandler.instance.DisplayDialogue(text_displayed);
                }
                else if (character.identity == "Teddy")
                {
                    text_displayed = "I've lost my bag of candy! Please help me find it Sugar!";
                    UIHandler.instance.DisplayDialogue(text_displayed);
                }
            }
        }
    }
}

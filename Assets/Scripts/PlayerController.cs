using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.InputSystem.XR;
//using static Unity.Cinemachine.InputAxisControllerBase<T>;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;


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
    //private string text_displayed = "Help me!!";

    public int maxHealth = 5;
    int currentHealth;
    public int health { get { return currentHealth; } }


    // Variables related to temporary invincibility
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;

    public InputAction talkAction;

    AudioSource audioSource;
    public AudioClip audioClip1; //walking
    public AudioClip audioClip2; //hit
    public AudioClip audioClip3; //projectile

    void Start()
    {
        MoveAction.Enable();
        WASD_Action.Enable();
        talkAction.Enable();

        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

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
            //walking sound? only when person is walking
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            Launch();
            audioSource.PlayOneShot(audioClip3);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Pressed X");
            FindFriend();
        }
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
            audioSource.PlayOneShot(audioClip2);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);

        if (currentHealth <= 0)
        {
           SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload scene
        }
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
            //NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
            //if (character != null)
            //{
            //    if (character.identity == "Froggo")
            //    {
            //        text_displayed = "Hey! Help me fix all those broken robots!";
            //        UIHandler.instance.DisplayDialogue(text_displayed);
            //    }
            //    else if(character.identity == "Martha")
            //    {
            //        text_displayed = "I'm looking for my lost book... have you seen it anywhere, Ruby?";
            //        UIHandler.instance.DisplayDialogue(text_displayed);
            //    }
            //    else if (character.identity == "Teddy")
            //    {
            //        text_displayed = "I've lost my bag of candy! Please help me find it Sugar!";
            //        UIHandler.instance.DisplayDialogue(text_displayed);
            //    }
            //}

            NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
            if (character != null)
            {
                string dialogueText = character.GetDialogue();
                UIHandler.instance.DisplayDialogue(dialogueText);

                character.AdvanceDialogue();
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}

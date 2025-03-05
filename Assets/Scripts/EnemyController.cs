using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 3.0f;
    public Rigidbody2D rigidbody2d;

    public bool vertical;

    public float changeTime = 3.0f;
    float timer = 0;
    int direction = -1;

    public int damage = -1;

    Animator animator;

    public bool broken = true;

    AudioSource audioSource;
    public AudioClip collectedClip; //Fixed Clip
    public AudioClip collectedClip2; //Hit

    public ParticleSystem smokeEffect;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;

        //if (timer < 0)
        //{
        //    int D_value = Random.Range(0, 2);

        //    if (D_value > 0)
        //    {
        //        vertical = true;
        //    }
        //    else
        //    {
        //        vertical = false;
        //    }

        //    direction = -direction;
        //    timer = changeTime;
        //}

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    private void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
            position.y = position.y + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        rigidbody2d.MovePosition(position);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(damage);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.PlayOneShot(collectedClip);
        smokeEffect.Stop();
    }
}

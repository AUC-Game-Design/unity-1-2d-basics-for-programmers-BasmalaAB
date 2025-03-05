using UnityEngine;

public class CandyBox : MonoBehaviour
{
    private Collider2D candyCollider;
    public bool sugarHolding = false; // Whether Sugar is holding the box
    public GameObject player2; // Sugar
    public GameObject Teddy; // Teddy (NPC)
    public float giveDistance = 3.0f; // Distance to give the candy

    private bool given = false;

    public GameManager gameManager;

    void Start()
    {
        candyCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (sugarHolding)
        {
            // Make the candy box follow Sugar
            transform.position = player2.transform.position;
        }

        // Check if Sugar is close enough to Teddy
        float distanceToTeddy = Vector2.Distance(player2.transform.position, Teddy.transform.position);
        if (sugarHolding && distanceToTeddy <= giveDistance && Input.GetKeyDown(KeyCode.C))
        {
            GiveToTeddy();
            gameManager.candybox_collected = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if Sugar collides with the candy box
        if (collision.gameObject == player2 && given == false)
        {
            transform.position = player2.transform.position;
            sugarHolding = true;
        }
    }

    void GiveToTeddy()
    {
        // Stop following Sugar
        sugarHolding = false;

        // Move candy to Teddy
        transform.position = Teddy.transform.position;

        // (Optional) Disable CandyBox so it's "given away"
        //gameObject.SetActive(false);

        // (Optional) Trigger some event, like playing an animation or dialogue
        Debug.Log("Sugar gave the candy box to Teddy!");
        given = true;
    }
}

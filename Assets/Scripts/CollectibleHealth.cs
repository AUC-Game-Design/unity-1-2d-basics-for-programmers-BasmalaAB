using UnityEngine;

public class CollectibleHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int health_increase = 1;
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null && controller.health < controller.maxHealth)
        {
            controller.ChangeHealth(health_increase);
            Destroy(gameObject);
            controller.PlaySound(collectedClip);
        }

    }
}

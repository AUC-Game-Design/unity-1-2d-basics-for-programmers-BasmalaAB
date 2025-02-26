using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damage = -1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.ChangeHealth(damage);
        }
    }
}

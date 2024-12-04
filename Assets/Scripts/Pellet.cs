using UnityEngine;

public class Pellet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
            Destroy(collision.gameObject);
    }
}

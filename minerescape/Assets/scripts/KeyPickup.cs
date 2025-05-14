using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Menghancurkan kunci setelah player menabraknya
            Destroy(gameObject);
        }
    }
}

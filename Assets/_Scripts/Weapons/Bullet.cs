using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public int damage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            return;
        }

        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}

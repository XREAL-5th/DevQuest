using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float damage = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Bullet collided with: " + collision.gameObject.name);
        if (collision.gameObject.tag != "Player")
        {
            Enemy enemy = collision.gameObject.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                Debug.Log($"Enemy collide");
                enemy.TakeDamage(damage);
                float health = enemy.GetCurrentHealth();
            }
            Destroy(gameObject);    // Destroy the bullet after it hits the enemy
        }
    }
}

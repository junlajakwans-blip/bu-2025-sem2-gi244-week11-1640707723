using System.Collections;
using UnityEngine;

public class StunPowerUp : MonoBehaviour
{
    public float stunDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(StunAllEnemies());
            Destroy(gameObject);
        }
    }

    IEnumerator StunAllEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        // Stun all enemies
        foreach (Enemy e in enemies)
        {
            e.isStunned = true;
        }

        yield return new WaitForSeconds(stunDuration);

        // stop stunning all enemies
        foreach (Enemy e in enemies)
        {
            if (e != null)
                e.isStunned = false;
        }
    }
}
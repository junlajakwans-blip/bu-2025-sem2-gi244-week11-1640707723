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

        // knockback ก่อน 
        foreach (Enemy e in enemies)
        {
            if (e != null)
            {
                e.Knockback(transform.position, 200f);
            }
        }

        // รอให้ knockback ทำงานก่อนจะ stun จริง ๆ
        yield return new WaitForSeconds(0.3f);

        // stun ศัตรูทั้งหมด
        foreach (Enemy e in enemies)
        {
            if (e != null)
            {
                e.isStunned = true;
            }
        }

        yield return new WaitForSeconds(stunDuration);

        // ปลด stun
        foreach (Enemy e in enemies)
        {
            if (e != null)
            {
                e.isStunned = false;
            }
        }
    }
}
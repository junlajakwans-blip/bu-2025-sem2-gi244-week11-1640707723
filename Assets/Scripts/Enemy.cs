using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;
    private GameObject player;
    public bool isStunned = false;
    public bool isKnockback = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        // ถ้าตกลงไปต่ำกว่า -2
        if (transform.position.y < -2f)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void FixedUpdate() 
    {
        if (isStunned)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        if (isKnockback)
        {
            return;
        }

        if (player == null) return;

        Vector3 direction = (player.transform.position - transform.position).normalized;
        rb.AddForce(direction * speed);
    }

    public void Knockback(Vector3 sourcePosition, float force)
    {
        if (rb != null)
        {
            isKnockback = true;

            rb.linearVelocity = Vector3.zero; 
            rb.AddExplosionForce(force, sourcePosition, 2f, 0.2f, ForceMode.Impulse);

            StartCoroutine(RecoverFromKnockback());
        }
    }

    private System.Collections.IEnumerator RecoverFromKnockback()
    {
        yield return new WaitForSeconds(0.5f);
        isKnockback = false;
    }
}

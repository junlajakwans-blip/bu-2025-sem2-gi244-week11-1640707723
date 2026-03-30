using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;
    private GameObject player;
    public bool isStunned = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isStunned)
        {
            rb.linearVelocity = Vector3.zero; // หยุดจริง ๆ
            return;
        }

        Vector3 direction = (player.transform.position - transform.position).normalized;
        rb.AddForce(direction * speed);

    }
}

using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float delay = 3f;
    public float blastRadius = 5f;
    public float explosionSpeed = 50f;

    void Start()
    {
        Invoke("Explode", delay);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero; // ����ǽ��ֹͣ�ƶ�
            GetComponent<Rigidbody2D>().isKinematic = true; // ʹ�ӵ���ֹ
        }
    }

    void Explode()
    {
        // �������ʵ�ֱ�ըЧ��������ͨ������������Ч��

        // �Ա�ը��Χ�ڵ����ж���ʩ����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);
        foreach (Collider2D nearbyObject in colliders)
        {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
            if (rb != null && nearbyObject.gameObject.tag == "Player")
            {
                Vector2 directionToPlayer = (nearbyObject.transform.position - transform.position).normalized;
                // ֱ�������ܱ�ըӰ��������ٶ�
                rb.velocity = directionToPlayer * explosionSpeed;
            }
        }
        Destroy(gameObject); // �����ӵ�
    }
}
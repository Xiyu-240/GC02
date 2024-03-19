using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public CameraShake cameraShake;

    public float delay = 3f;
    public float blastRadius = 5f;
    public float explosionSpeed = 50f;
    public float particleTime = 1f;

    public bool boomShake = false;

    void Start()
    {
        cameraShake = GameObject.Find("CM vcam1").GetComponent<CameraShake>();//��ȡ���
        Invoke("Explode", delay);
    }
    void Update()
    {
        RotateInMovementDirection();
    }

    void RotateInMovementDirection()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 velocity = rb.velocity;
        if (velocity != Vector2.zero) // ȷ���ٶȲ�Ϊ��
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg; // ����Ƕ�
            transform.rotation = Quaternion.Euler(0f, 0f, angle-40f);
        }
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
        cameraShake.BoomShake();//�����𶯺���

        // �Ա�ը��Χ�ڵ����ж���ʩ����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);
        foreach (Collider2D nearbyObject in colliders)
        {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
            if (rb != null && nearbyObject.gameObject.tag == "Player")
            {
                Vector2 directionToPlayer = (nearbyObject.transform.position - transform.position).normalized;
                // ֱ�������ܱ�ըӰ��������ٶ�
                rb.velocity += directionToPlayer * explosionSpeed;
            }
        }

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
        gameObject.GetComponent<AudioSource>().Play();

        Invoke("DestroyBullet", particleTime);//�����ӵ�
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
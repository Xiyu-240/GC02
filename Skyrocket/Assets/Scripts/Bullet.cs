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
        cameraShake = GameObject.Find("CM vcam1").GetComponent<CameraShake>();//获取组件
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
        if (velocity != Vector2.zero) // 确保速度不为零
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg; // 计算角度
            transform.rotation = Quaternion.Euler(0f, 0f, angle-40f);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero; // 碰到墙体停止移动
            GetComponent<Rigidbody2D>().isKinematic = true; // 使子弹静止
        }
    }

    void Explode()
    {
        cameraShake.BoomShake();//调用震动函数

        // 对爆炸范围内的所有对象施加力
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);
        foreach (Collider2D nearbyObject in colliders)
        {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
            if (rb != null && nearbyObject.gameObject.tag == "Player")
            {
                Vector2 directionToPlayer = (nearbyObject.transform.position - transform.position).normalized;
                // 直接设置受爆炸影响物体的速度
                rb.velocity += directionToPlayer * explosionSpeed;
            }
        }

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
        gameObject.GetComponent<AudioSource>().Play();

        Invoke("DestroyBullet", particleTime);//销毁子弹
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
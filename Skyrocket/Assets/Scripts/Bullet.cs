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
            GetComponent<Rigidbody2D>().velocity = Vector2.zero; // 碰到墙体停止移动
            GetComponent<Rigidbody2D>().isKinematic = true; // 使子弹静止
        }
    }

    void Explode()
    {
        // 这里可以实现爆炸效果，比如通过动画或粒子效果

        // 对爆炸范围内的所有对象施加力
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);
        foreach (Collider2D nearbyObject in colliders)
        {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
            if (rb != null && nearbyObject.gameObject.tag == "Player")
            {
                Vector2 directionToPlayer = (nearbyObject.transform.position - transform.position).normalized;
                // 直接设置受爆炸影响物体的速度
                rb.velocity = directionToPlayer * explosionSpeed;
            }
        }
        Destroy(gameObject); // 销毁子弹
    }
}
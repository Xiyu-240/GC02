using UnityEngine;
using System.Collections;

public class DeformOnCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject wall;
    private bool canDeform = true;
    public float minCollisionSpeed = 5f;
    public Vector3 deformationScale = new Vector3(1.5f, 1f, 1f);

    public ParticleSystem dustParticle;//落地烟
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canDeform && collision.gameObject.CompareTag("Wall"))
        {
            wall = collision.gameObject;
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 wallPosition = wall.transform.position;

            // 检查物体速度是否大于指定值
            if (rb.velocity.magnitude > minCollisionSpeed && contactPoint.x < wallPosition.x)
            {
                // 形变
                StartCoroutine(DeformCoroutine());
                dustParticle.Play();
            }
        }
    }

    IEnumerator DeformCoroutine()
    {
        // 禁用碰撞
        canDeform = false;

        // 形变
        transform.localScale = deformationScale;

        // 等待一段时间后恢复原状
        yield return new WaitForSeconds(0.1f);

        // 恢复原状
        transform.localScale = Vector3.one;

        // 重新启用碰撞
        yield return new WaitForSeconds(0.1f);
        canDeform = true;
    }
}

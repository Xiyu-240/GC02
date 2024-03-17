using UnityEngine;
using System.Collections;

public class DeformOnCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject wall;
    private bool canDeform = true;
    public float minCollisionSpeed = 5f;
    public Vector3 deformationScale = new Vector3(1.5f, 1f, 1f);

    public ParticleSystem dustParticle;//�����
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

            // ��������ٶ��Ƿ����ָ��ֵ
            if (rb.velocity.magnitude > minCollisionSpeed && contactPoint.x < wallPosition.x)
            {
                // �α�
                StartCoroutine(DeformCoroutine());
                dustParticle.Play();
            }
        }
    }

    IEnumerator DeformCoroutine()
    {
        // ������ײ
        canDeform = false;

        // �α�
        transform.localScale = deformationScale;

        // �ȴ�һ��ʱ���ָ�ԭ״
        yield return new WaitForSeconds(0.1f);

        // �ָ�ԭ״
        transform.localScale = Vector3.one;

        // ����������ײ
        yield return new WaitForSeconds(0.1f);
        canDeform = true;
    }
}

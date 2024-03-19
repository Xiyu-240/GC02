using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public float fallDurationThreshold = 2f; // �������׹�䳬����ʱ��ʱ��������
    private float fallTimer = 0f; // ��¼���׹���ʱ��
    public float minCollisionSpeed = 5f; // ������Ч����С��ײ�ٶ�
    private bool isFalling = false; // ����Ƿ���������
    public AudioSource asFall;
    public AudioSource ascollide;

    void Start()
    {
       
    }

    void Update()
    {
        // �������Ƿ�����׹��
        if (isFalling)
        {
            // ����������䣬���Ӽ�ʱ��
            fallTimer += Time.deltaTime;

            // ��׹��ʱ�䳬����ֵʱ��ʼ��������
            if (fallTimer >= fallDurationThreshold && !asFall.isPlaying)
            {
                asFall.Play();
            }
        }
    }

    // ����ҿ�ʼ����ʱ����
    void StartFalling()
    {
        isFalling = true;
        fallTimer = 0f; // ���ü�ʱ��
    }

    // �����ֹͣ����ʱ����
    void StopFalling()
    {
        isFalling = false;
        fallTimer = 0f; // ���ü�ʱ��
        asFall.Stop(); // ֹͣ��������
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ��ײ����ʱ���ж��Ƿ�Ϊ�����ָ������
        // ��������������ײ����ı�ǩΪ"Ground"
        if (collision.gameObject.CompareTag("Wall"))
        {
            StopFalling();
        }
        // ������ײ�ٶ�
        float collisionSpeed = collision.relativeVelocity.magnitude;

        // �����ײ�ٶȴ��ڻ������С��ײ�ٶȣ��򲥷���Ч
        if (collisionSpeed >= minCollisionSpeed && !ascollide.isPlaying)
        {
            ascollide.Play();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isFalling = false;
        }

    }
    void OnCollisionExit2D(Collision2D collision)
    {
        // �뿪����ʱ����ʼ����
        if (collision.gameObject.CompareTag("Wall"))
        {
            StartFalling();
        }
    }
}
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Ҫ�����Ŀ�꣨��ң�

    public float smoothSpeed = 0.125f; // ����ƶ���ƽ���ٶ�
    public Vector3 offset; // ��������Ŀ���ƫ����

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // ���������Ŀ��λ��
        desiredPosition.z = -10;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // ƽ���ƶ���Ŀ��λ��
        transform.position = smoothedPosition; // �������λ��
    }
}

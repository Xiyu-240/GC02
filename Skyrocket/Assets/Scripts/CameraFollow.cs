using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Ҫ�����Ŀ�꣨��ң�
    private CameraController controller;

    public float smoothSpeed = 1f; // ����ƶ���ƽ���ٶ�
    public Vector3 offset; // ��������Ŀ���ƫ����

    private void Start()
    {
        controller = GetComponent<CameraController>();
    }
    void Update()
    {
        if (controller.follow)
        {
            Vector3 desiredPosition = target.position + offset; // ���������Ŀ��λ��
            desiredPosition.z = -10;
            desiredPosition.y += 2.5f;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime); // ƽ���ƶ���Ŀ��λ��
            transform.position = smoothedPosition; // �������λ��
        }
    }
}
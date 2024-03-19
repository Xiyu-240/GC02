using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 要跟随的目标（玩家）
    private CameraController controller;

    public float smoothSpeed = 1f; // 相机移动的平滑速度
    public Vector3 offset; // 相机相对于目标的偏移量

    private void Start()
    {
        controller = GetComponent<CameraController>();
    }
    void Update()
    {
        if (controller.follow)
        {
            Vector3 desiredPosition = target.position + offset; // 计算相机的目标位置
            desiredPosition.z = -10;
            desiredPosition.y += 2.5f;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime); // 平滑移动到目标位置
            transform.position = smoothedPosition; // 更新相机位置
        }
    }
}
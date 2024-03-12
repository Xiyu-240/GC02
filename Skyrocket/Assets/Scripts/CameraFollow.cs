using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 要跟随的目标（玩家）

    public float smoothSpeed = 0.125f; // 相机移动的平滑速度
    public Vector3 offset; // 相机相对于目标的偏移量

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // 计算相机的目标位置
        desiredPosition.z = -10;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // 平滑移动到目标位置
        transform.position = smoothedPosition; // 更新相机位置
    }
}

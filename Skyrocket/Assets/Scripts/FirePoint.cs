using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    void Update()
    {
        // 将鼠标的屏幕坐标转换为世界坐标
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // 确保z坐标为0，因为是2D游戏

        // 计算FirePoint指向鼠标的向量
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;

        // 计算这个向量的角度
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 设置FirePoint的旋转
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
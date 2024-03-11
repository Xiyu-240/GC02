using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    void Update()
    {
        // ��������Ļ����ת��Ϊ��������
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // ȷ��z����Ϊ0����Ϊ��2D��Ϸ

        // ����FirePointָ����������
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;

        // ������������ĽǶ�
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ����FirePoint����ת
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
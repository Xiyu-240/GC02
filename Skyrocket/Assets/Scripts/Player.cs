using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float minSpeed = 10f;//��С����
    public float maxSpeed = 30f;//������
    public float maxChargeTime = 2f;//�������ʱ��

    private float chargeTime;
    private bool isCharging = false;

    public int maxBullets = 2;
    private int currentBullets;
    public float reloadTime = 2f; // װ��ʱ��
    private bool isReloading = false;

    private void Start()
    {
        currentBullets = maxBullets;
    }
    void Update()
    {
        if (!isReloading)
        {
            if (Input.GetMouseButtonDown(0)) // ����������
            {
                isCharging = true;
                chargeTime = 0f; // ��ʼ����
            }
            else if (isCharging && chargeTime < maxChargeTime)
            {
                chargeTime += Time.deltaTime; // ������
            }

            if (Input.GetMouseButtonUp(0) && isCharging) // �������ͷ�
            {
                Fire();
                isCharging = false;
            }

            if (currentBullets <= 0 && !isReloading)
            {
                StartCoroutine(Reload());
            }
        }
    }
    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentBullets = maxBullets;
        isReloading = false;
    }
    void Fire()
    {
        float speed = Mathf.Lerp(minSpeed, maxSpeed, chargeTime / maxChargeTime);
        Vector2 shootingDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // ֱ�������ӵ��ٶ�
        bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * speed;

        // ��������ӷ����ٶ�
        GetComponent<Rigidbody2D>().velocity = -shootingDirection * speed; // �ɵ��������ٶȵĴ�С
        Debug.Log(shootingDirection * speed);
        if (currentBullets > 0)
        {
            // �����ӵ��Ĵ���...
            currentBullets--;
        }
    }
}

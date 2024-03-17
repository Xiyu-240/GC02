using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float minSpeed = 10f;//最小后坐
    public float maxSpeed = 30f;//最大后坐
    public float maxChargeTime = 2f;//最大蓄力时间

    private float chargeTime;
    private bool isCharging = false;

    public int maxBullets = 2;
    private int currentBullets;
    public float reloadTime = 2f; // 装填时间
    private bool isReloading = false;

    public bool fireShake = false;
    public ParticleSystem fireParticle;

    private void Start()
    {
        currentBullets = maxBullets;
    }
    void Update()
    {
        if (!isReloading)
        {
            if (Input.GetMouseButtonDown(0)) // 鼠标左键按下
            {
                isCharging = true;
                chargeTime = 0f; // 开始蓄力
            }
            else if (isCharging && chargeTime < maxChargeTime)
            {
                chargeTime += Time.deltaTime; // 蓄力中
            }

            if (Input.GetMouseButtonUp(0) && isCharging) // 鼠标左键释放
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
        fireParticle.Play();//开火烟尘
        fireShake = true;//开火震动

        float speed = Mathf.Lerp(minSpeed, maxSpeed, chargeTime / maxChargeTime);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector2 shootingDirection = (mousePos - transform.position).normalized;
        Debug.Log("1:" + shootingDirection.magnitude + "and" + speed);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        // 直接设置子弹速度
        bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * speed;
        Debug.Log("2:" +shootingDirection + "and" + speed);
        // 给玩家添加反向速度
        GetComponent<Rigidbody2D>().velocity = -shootingDirection * speed; // 可调整反向速度的大小
        Debug.Log("3:" +shootingDirection +"and"+speed);
        if (currentBullets > 0)
        {
            // 发射子弹的代码...
            currentBullets--;
        }
    }
}


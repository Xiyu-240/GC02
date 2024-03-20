using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;

    public GameObject bullet;
    public Sprite sprite0;
    public Sprite sprite1;
    public Sprite sprite2;

    public GameObject chargeBar;
    public Transform firePoint;
    public float minSpeed = 10f;//最小后坐
    public float maxSpeed = 30f;//最大后坐
    public float maxChargeTime = 2f;//最大蓄力时间

    public SpriteRenderer playerSprite;
    public SpriteRenderer gunSprite;

    private float chargeTime;
    private bool isCharging = false;

    public int maxBullets = 2;
    private int currentBullets;
    public float reloadTime = 2f; // 装填时间
    private bool isReloading = false;

    public bool fireShake = false;
    public ParticleSystem fireParticle;
    public ParticleSystem chargeParticle;
    public ParticleSystem chargeParticle_;

    public AudioSource asCharge;
    public AudioSource asChargeEnd;
    private void Start()
    {
        currentBullets = maxBullets;
    }
    void Update()
    {
        if (currentBullets == 2)
        {
            bullet.GetComponent<Image>().sprite = sprite2;
        }
        if (currentBullets == 1)
        {
            bullet.GetComponent<Image>().sprite = sprite1;
        }
        if (currentBullets == 0)
        {
            bullet.GetComponent<Image>().sprite = sprite0;
        }

        chargeBar.GetComponent<Image>().fillAmount = chargeTime * 5;

        if (!isReloading)
        {
            if (Input.GetMouseButtonDown(0)) // 鼠标左键按下
            {
                chargeParticle.Play();
                chargeParticle_.Play();
                isCharging = true;
                asCharge.Play();
                Time.timeScale = 0.1f;
            }
            else if (isCharging && chargeTime < maxChargeTime)
            {
                chargeTime += Time.deltaTime; // 蓄力中
            }

            if (Input.GetMouseButtonUp(0) && isCharging) // 鼠标左键释放
            {
                chargeParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                chargeParticle_.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                Fire();
                isCharging = false;
                chargeTime = 0f;
                asCharge.Stop();
                asChargeEnd.Play();
                Time.timeScale = 1f;
            }

            if (currentBullets <= 0 && !isReloading)
            {
                StartCoroutine(Reload());
            }
        }

        // 将鼠标的屏幕坐标转换为世界坐标
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 因为是2D游戏，所以设置z坐标为0
        mouseWorldPosition.z = 0;

        // 检查鼠标位置是否在物体的左边
        if (mouseWorldPosition.x < transform.position.x)
        {
            // 如果在左边，翻转Sprite（沿着x轴）
            playerSprite.flipX = false;
            gunSprite.flipY = true;
        }
        else
        {
            // 如果在右边或正上方，确保Sprite正常朝向（不翻转）
            playerSprite.flipX = true;
            gunSprite.flipY = false;
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

        float speed = Mathf.Lerp(minSpeed, maxSpeed, chargeTime * 10 / maxChargeTime);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector2 shootingDirection = (mousePos - transform.position).normalized;
        //Debug.Log("1:" + shootingDirection.magnitude + "and" + speed);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        // 直接设置子弹速度
        bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * speed;
        //Debug.Log("2:" +shootingDirection + "and" + speed);
        // 给玩家添加反向速度
        GetComponent<Rigidbody2D>().velocity -= shootingDirection * speed; // 可调整反向速度的大小
        //Debug.Log("3:" +shootingDirection +"and"+speed);
        if (currentBullets > 0)
        {
            // 发射子弹的代码...
            currentBullets--;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;

    public GameObject rocket0;
    public GameObject rocket1;
    public Transform firePoint;
    public float minSpeed = 10f;//��С����
    public float maxSpeed = 30f;//������
    public float maxChargeTime = 2f;//�������ʱ��

    public SpriteRenderer playerSprite;
    public SpriteRenderer gunSprite;

    private float chargeTime;
    private bool isCharging = false;

    public int maxBullets = 2;
    private int currentBullets;
    public float reloadTime = 2f; // װ��ʱ��
    private bool isReloading = false;

    public bool fireShake = false;
    public ParticleSystem fireParticle;

    private void Start()
    {
        currentBullets = maxBullets;
    }
    void Update()
    {
        if (currentBullets == 2)
        {
            rocket0.SetActive(true);
            rocket1.SetActive(true);
        }
        if (currentBullets == 1)
        {
            rocket0.SetActive(false);
            rocket1.SetActive(true);
        }
        if (currentBullets == 0)
        {
            rocket0.SetActive(false);
            rocket1.SetActive(false);
        }


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

        // ��������Ļ����ת��Ϊ��������
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // ��Ϊ��2D��Ϸ����������z����Ϊ0
        mouseWorldPosition.z = 0;

        // ������λ���Ƿ�����������
        if (mouseWorldPosition.x < transform.position.x)
        {
            // �������ߣ���תSprite������x�ᣩ
            playerSprite.flipX = false;
            gunSprite.flipY = true;
        }
        else
        {
            // ������ұ߻����Ϸ���ȷ��Sprite�������򣨲���ת��
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
        fireParticle.Play();//�����̳�
        fireShake = true;//������

        float speed = Mathf.Lerp(minSpeed, maxSpeed, chargeTime / maxChargeTime);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector2 shootingDirection = (mousePos - transform.position).normalized;
        Debug.Log("1:" + shootingDirection.magnitude + "and" + speed);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        // ֱ�������ӵ��ٶ�
        bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * speed;
        Debug.Log("2:" +shootingDirection + "and" + speed);
        // �������ӷ����ٶ�
        GetComponent<Rigidbody2D>().velocity -= shootingDirection * speed; // �ɵ��������ٶȵĴ�С
        Debug.Log("3:" +shootingDirection +"and"+speed);
        if (currentBullets > 0)
        {
            // �����ӵ��Ĵ���...
            currentBullets--;
        }
    }
}


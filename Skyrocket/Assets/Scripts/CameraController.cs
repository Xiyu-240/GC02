using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool isShowing = true;
    public bool follow = false;

    public Transform targetTransform;
    public Transform targetTransform_;
    private CinemachineVirtualCamera vCamera;

    public float scaleSpeed = 5f;
    public float fallSpeed = 1f;

    public float duration = 2.0f; // 渐变持续时间
    private float elapsedTime = 0f; // 已经过的时间

    private void Start()
    {
        vCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void Update()
    {
        if (isShowing && vCamera.m_Lens.OrthographicSize <= 50)
        {
            vCamera.m_Lens.OrthographicSize += Time.deltaTime * scaleSpeed;
            transform.position = Vector3.Lerp(transform.position, targetTransform_.position, Time.deltaTime*fallSpeed);
            fallSpeed += Time.deltaTime * 0.075f;
        }
        else
        {
            isShowing = false;

            if (!isShowing && vCamera.m_Lens.OrthographicSize >= 10)
            {
                if(elapsedTime<=duration)
                {
                    elapsedTime += Time.deltaTime;
                }
                // 计算插值时间，范围在0到1之间
                float t = elapsedTime / duration;

                vCamera.m_Lens.OrthographicSize -= Time.deltaTime * scaleSpeed * 3f;
                Vector3 target = new Vector3(targetTransform.position.x, targetTransform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, target, t);
            }
            else
            {
                follow = true;
            }
        }
    }
}

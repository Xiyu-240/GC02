using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public float fallDurationThreshold = 2f; // 玩家向下坠落超过此时间时播放音乐
    private float fallTimer = 0f; // 记录玩家坠落的时间
    public float minCollisionSpeed = 5f; // 触发音效的最小碰撞速度
    private bool isFalling = false; // 玩家是否正在下落
    public AudioSource asFall;
    public AudioSource ascollide;

    void Start()
    {
       
    }

    void Update()
    {
        // 检查玩家是否向下坠落
        if (isFalling)
        {
            // 玩家正在下落，增加计时器
            fallTimer += Time.deltaTime;

            // 当坠落时间超过阈值时开始播放音乐
            if (fallTimer >= fallDurationThreshold && !asFall.isPlaying)
            {
                asFall.Play();
            }
        }
    }

    // 当玩家开始下落时调用
    void StartFalling()
    {
        isFalling = true;
        fallTimer = 0f; // 重置计时器
    }

    // 当玩家停止下落时调用
    void StopFalling()
    {
        isFalling = false;
        fallTimer = 0f; // 重置计时器
        asFall.Stop(); // 停止播放音乐
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 碰撞发生时，判断是否为地面或指定物体
        // 这里假设地面或可碰撞物体的标签为"Ground"
        if (collision.gameObject.CompareTag("Wall"))
        {
            StopFalling();
        }
        // 计算碰撞速度
        float collisionSpeed = collision.relativeVelocity.magnitude;

        // 如果碰撞速度大于或等于最小碰撞速度，则播放音效
        if (collisionSpeed >= minCollisionSpeed && !ascollide.isPlaying)
        {
            ascollide.Play();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isFalling = false;
        }

    }
    void OnCollisionExit2D(Collision2D collision)
    {
        // 离开地面时，开始下落
        if (collision.gameObject.CompareTag("Wall"))
        {
            StartFalling();
        }
    }
}
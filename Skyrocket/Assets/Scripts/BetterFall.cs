using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterFall : MonoBehaviour
{
    private Player _player;
    private Rigidbody2D _rb;

    public float _gravity = 1.0f;
    public float _gravityPlus = 1.5f;
    public float maxFallSpeed = -8f;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //����ʱ��������
        if (_rb.velocity.y >= 0)
        {
            _rb.gravityScale = _gravity;
        }
        else
        {
            // ��������ٶ�
            if (_rb.velocity.y <= maxFallSpeed)
            {
                // ������嵱ǰ�ٶ��Ѿ��ﵽ�򳬹���������ٶȣ���ֹͣ�����ٶȣ�������������Ϊ��
                _rb.velocity = new Vector2(_rb.velocity.x, maxFallSpeed);
                _rb.gravityScale = 0f;
            }
            else if (_rb.velocity.y > maxFallSpeed + 2f)
            {
                _rb.gravityScale = _gravityPlus;
            }
        }
        //Debug.Log(_rb.velocity.y);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDisappear : MonoBehaviour
{
    bool yes=true;
    public float disTime=4;

    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMesh;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // ���Ի�ȡTextMeshPro�����������Ҫ���������л�ȡ
        textMesh = GetComponentInChildren<TextMeshPro>();

    }

    // Update is called once per frame
    void Update()
    {
        if ((GameObject.Find("Player").transform.position - gameObject.transform.position).magnitude <= 6&&yes)
        {
            StartCoroutine(FadeOutCoroutine());
            yes = false;
        }
    }
    IEnumerator FadeOutCoroutine()
    {
        float currentTime = 0.0f;

        // ��ʼ��ɫ
        Color spriteInitialColor = spriteRenderer ? spriteRenderer.color : Color.white;
        Color textInitialColor = textMesh ? textMesh.color : Color.white;

        while (currentTime < disTime)
        {
            float alpha = Mathf.Lerp(1.0f, 0.0f, currentTime / disTime);

            // �����µ���ɫ
            if (spriteRenderer) spriteRenderer.color = new Color(spriteInitialColor.r, spriteInitialColor.g, spriteInitialColor.b, alpha);
            if (textMesh) textMesh.color = new Color(textInitialColor.r, textInitialColor.g, textInitialColor.b, alpha);

            currentTime += Time.deltaTime;
            yield return null;
        }

        // ȷ��������ȫ͸��
        if (spriteRenderer) spriteRenderer.color = new Color(spriteInitialColor.r, spriteInitialColor.g, spriteInitialColor.b, 0);
        if (textMesh) textMesh.color = new Color(textInitialColor.r, textInitialColor.g, textInitialColor.b, 0);
    }
}



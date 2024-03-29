using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WobbleUI : MonoBehaviour
{
    private float x;
    private float scale;
    private Vector2 defaultScale;
    private float currentAmplitude;

    [SerializeField]
    private float amplitude;

    [SerializeField]
    private float settleSpeed;

    [SerializeField]
    private float wobbleFrequency;

    [SerializeField]
    private bool wobbleOnStart = false;

    [HideInInspector]
    public bool isWobbling = false;

    private RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        defaultScale = rt.localScale;
        if (wobbleOnStart)
        {
            DoTheWobble();
        }
    }

    private void Update()
    {
        if (currentAmplitude > 0)
        {
            scale = currentAmplitude * Mathf.Sin(x * wobbleFrequency);
            x += 7f * Time.deltaTime;
            currentAmplitude -= (settleSpeed * 70f) * Time.deltaTime;
        }
        else
        {
            scale = 0;
            currentAmplitude = 0;
            isWobbling = false;
        }

        rt.localScale = defaultScale + new Vector2(scale, scale);

    }

    public void DoTheWobble()
    {
        currentAmplitude = amplitude;
        isWobbling = true;
    }
}

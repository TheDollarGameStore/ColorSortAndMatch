using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveAndFade : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Vector2 direction;

    [SerializeField]
    private float lifeSpan;

    private TextMeshProUGUI tmp;

    private RectTransform rt;

    private float alpha;

    private float fadeSpeed;


    void Start()
    {
        alpha = 1f;
        fadeSpeed = 1f / lifeSpan;
        rt = GetComponent<RectTransform>();
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        rt.localPosition += (Vector3)direction * Time.deltaTime;
        alpha -= fadeSpeed * Time.deltaTime;
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, alpha);

        if (alpha <= 0f)
        {
            Destroy(gameObject);
        }
    }
}

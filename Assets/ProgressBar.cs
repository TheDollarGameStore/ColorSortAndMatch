using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI levelText;

    [SerializeField]
    private TextMeshProUGUI nextLevelText;

    [SerializeField]
    private Image progressBar;

    private float progressBarFill;

    public static ProgressBar instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        levelText.text = GameManager.instance.level.ToString();
        nextLevelText.text = (GameManager.instance.level + 1).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, progressBarFill, 10f * Time.deltaTime);
    }

    public void UpdateProgressBar()
    {
        progressBarFill = (float)GameManager.instance.score / (float)GameManager.instance.requiredScore;
    }
}

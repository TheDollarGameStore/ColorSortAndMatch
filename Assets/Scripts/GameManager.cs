using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject tubePrefab;

    public int amountOfTubes;
    public int startingSpheres;

    public Claw claw;

    private List<Tube> tubes;

    [HideInInspector]
    public bool canMove;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [HideInInspector]
    public int score;
    private int displayScore;
    public int combo;

    [SerializeField]
    private TextMeshProUGUI comboText;

    private WobbleUI wobbler;

    private List<Constants.SphereColor> nextFill;

    [SerializeField]
    private List<Image> nextFillIndicators;

    [SerializeField]
    private List<Sprite> nextFillIndicatorsSprites;

    [SerializeField]
    private TextMeshProUGUI turnsLeftText;

    private int turnsLeft;

    [SerializeField]
    private int fillEveryXMoves;

    [SerializeField]
    private RectTransform skipTurnButtonTransform;

    [SerializeField]
    private GameObject addScorePrefab;

    public Transform ballSpawnTransform;

    [SerializeField]
    private GameObject menuBg;

    [SerializeField]
    private GameObject gameOverText;

    [SerializeField]
    private GameObject victoryText;

    [SerializeField]
    private GameObject continueButton;

    [SerializeField]
    private GameObject retryButton;

    [HideInInspector]
    public int level;

    [HideInInspector]
    public int requiredScore;

    [SerializeField]
    private TextMeshProUGUI goalScore;

    [SerializeField]
    private GameObject menuBox;

    [SerializeField]
    private GameObject happyFace;

    [SerializeField]
    private GameObject sadFace;


    public Pipe pipe;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);


        level = PlayerPrefs.GetInt("Level", 1);
        requiredScore = 5000 + (level * 500);
        goalScore.text = requiredScore.ToString();
    }

    private void Start()
    {
        turnsLeft = fillEveryXMoves;
        turnsLeftText.text = turnsLeft.ToString();
        wobbler = comboText.GetComponent<WobbleUI>();
        combo = 0;
        score = 0;
        canMove = true;
        tubes = new List<Tube>();
        float spaceBetween = 2f;
        float startX = -((amountOfTubes - 1) * spaceBetween) / 2f ;

        for (int i = 0; i < amountOfTubes; i++)
        {
            Tube newTube = Instantiate(tubePrefab, new Vector3(startX + (i * spaceBetween), 0f, 0f), Quaternion.identity).GetComponent<Tube>();
            newTube.AddRandomBalls(startingSpheres);
            tubes.Add(newTube);
        }

        PrepNextFill();
    }

    public void CountDown()
    {
        turnsLeft -= 1;
        if (turnsLeft == -1)
        {
            FillUp();
            turnsLeft = fillEveryXMoves;
        }
        turnsLeftText.text = turnsLeft.ToString();
    }

    public void Skip()
    {
        FillUp();
        AddScore(100 * (turnsLeft + 1));
        GameObject newScore = Instantiate(addScorePrefab, Vector3.zero, Quaternion.identity);
        newScore.transform.SetParent(Camera.main.transform.Find("Canvas").transform, false);
        newScore.GetComponent<RectTransform>().localPosition = skipTurnButtonTransform.localPosition + (Vector3)(Vector2.up * 300f) + (Vector3)(Vector2.left * 125f);
        newScore.GetComponent<TextMeshProUGUI>().text = "+" + (100 * (turnsLeft + 1)).ToString();
        turnsLeft = fillEveryXMoves;
        turnsLeftText.text = turnsLeft.ToString();
    }

    private void PrepNextFill()
    {
        nextFill = new List<Constants.SphereColor>();

        for (int i = 0; i < 3; i++)
        {
            nextFill.Add((Constants.SphereColor)Random.Range(0, 4));
            nextFillIndicators[i].sprite = nextFillIndicatorsSprites[(int)nextFill[i]];
        }
    }

    public void FillUp()
    {
        canMove = false;
        Invoke("ResetCanMove", 0.31f);
        CheckGameOver();

        List<Tube> pickedTubes = new List<Tube>(tubes);

        while (pickedTubes.Count > 3)
        {
            pickedTubes.RemoveAt(Random.Range(0, pickedTubes.Count));
        }

        pipe.hide = false;

        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(pickedTubes[i].AddColorBall(nextFill[i], i * 0.1f));
        }

        Invoke("HidePipe", 0.4f);

        PrepNextFill();
    }

    private void HidePipe()
    {
        pipe.hide = true;
    }


    private void ResetCanMove()
    {
        canMove = true;
    }

    private void CheckGameOver()
    {
        for (int i = 0; i < tubes.Count; i++)
        {
            if (tubes[i].spheres.Count > 5)
            {
                GameOver();
                break;
            }
        }
    }

    public void GameOver()
    {
        menuBg.SetActive(true);
        gameOverText.SetActive(true);
        menuBox.SetActive(true);
        retryButton.SetActive(true);
        sadFace.SetActive(true);
        canMove = false;
    }

    private void FixedUpdate()
    {
        if (displayScore != score)
        {
            if (score - displayScore > 200)
            {
                displayScore += 100;
            }
            else
            {
                displayScore += 20;
            }
        }
        scoreText.text = displayScore.ToString();
    }

    public void AddScore()
    {
        score += 100 * combo;
        ProgressBar.instance.UpdateProgressBar();
        CheckVictory();
    }

    public void AddScore(int amount)
    {
        score += amount;
        ProgressBar.instance.UpdateProgressBar();
        CheckVictory();
    }

    public void UpdateComboCounter()
    {
        if (combo > 1)
        {
            comboText.text = "X" + combo.ToString();
            wobbler.DoTheWobble();
        }
        else
        {
            comboText.text = "";
        }
    }

    private void CheckVictory()
    {
        if (score >= requiredScore)
        {
            canMove = false;
            menuBg.SetActive(true);
            victoryText.SetActive(true);
            menuBox.SetActive(true);
            continueButton.SetActive(true);
            happyFace.SetActive(true);
            PlayerPrefs.SetInt("Level", level + 1);
        }
    }
}

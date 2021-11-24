using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    private int score;
    private int displayScore;
    public int combo;

    [SerializeField]
    private TextMeshProUGUI comboText;

    private WobbleUI wobbler;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
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

    private void PrepNextFill()
    {

    }

    public void FillUp()
    {
        List<Tube> nonFullTubes = new List<Tube>();

        for (int j = 0; j < tubes.Count; j++)
        {
            if (tubes[j].spheres.Count < 5)
            {
                nonFullTubes.Add(tubes[j]);
            }
        }

        if (nonFullTubes.Count <= 3)
        {
            for (int i = 0; i < nonFullTubes.Count; i++)
            {
                nonFullTubes[i].AddRandomBalls(1);
            }

            GameOver();
        }
        else
        {
            while (nonFullTubes.Count > 3)
            {
                nonFullTubes.RemoveAt(Random.Range(0, nonFullTubes.Count));
            }

            for (int i = 0; i < nonFullTubes.Count; i++)
            {
                nonFullTubes[i].AddRandomBalls(1);
            }

            PrepNextFill();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
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
}

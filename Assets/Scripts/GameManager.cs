using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public void FillUp(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            List<Tube> nonFullTubes = new List<Tube>();

            for (int j = 0; j < tubes.Count; j++)
            {
                if (tubes[j].spheres.Count < 5)
                {
                    nonFullTubes.Add(tubes[j]);
                }
            }

            if (nonFullTubes.Count != 0)
            {
                nonFullTubes[Random.Range(0, nonFullTubes.Count - 1)].AddRandomBalls(1);
            }
            else
            {
                //GameOver
                Debug.Log("Game Over");
            }
            
        }
    }
}

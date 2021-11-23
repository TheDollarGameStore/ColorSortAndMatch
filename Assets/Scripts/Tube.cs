using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [HideInInspector]
    public List<Sphere> spheres = new List<Sphere>(); //Listed from bottom up

    public List<GameObject> spherePrefabs;

    // Update is called once per frame
    void UpdateSphereLocations()
    {
        for (int i = 0; i < spheres.Count; i++)
        {
            spheres[i].goalPos = transform.position + (Vector3.up * 0.2f) + (Vector3.up * i * 1f);
        }
    }

    public void AddRandomBalls(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Sphere newSphere = Instantiate(spherePrefabs[Random.Range(0, spherePrefabs.Count)], transform.position + (Vector3.up * 8f), Quaternion.identity).GetComponent<Sphere>();
            spheres.Add(newSphere);
            UpdateSphereLocations();
            CheckMatches();
        }
    }

    public void AddBall(Sphere ball)
    {
        spheres.Add(ball);

        UpdateSphereLocations();

        CheckMatches();
    }

    public Sphere TakeBall()
    {
        Sphere takeSphere = spheres[spheres.Count - 1];
        spheres.RemoveAt(spheres.Count - 1);
        return takeSphere;
    }

    private void CheckMatches()
    {
        Constants.SphereColor color = spheres[spheres.Count - 1].color;

        int totalMatches = 0;

        for (int i = spheres.Count - 1; i >= 0; i--)
        {
            if (spheres[i].color == color)
            {
                totalMatches++;
            }
            else
            {
                break;
            }
        }

        if (totalMatches == 3)
        {
            //Pop
            GameManager.instance.canMove = false;
            int removeAtIndex = spheres.Count - totalMatches;
            for (int i = 0; i < totalMatches; i++)
            {
                spheres[removeAtIndex].Invoke("Pop", 0.35f);
                spheres.RemoveAt(removeAtIndex);
            }
        }
    }
}

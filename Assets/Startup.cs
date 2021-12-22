using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string destination = "";
        int level = PlayerPrefs.GetInt("Level", 1);

        if (level % 30 <= 10)
        {
            destination = "Gameplay1";
        }
        else if (level % 30 <= 20)
        {
            destination = "Gameplay2";
        }
        else
        {
            destination = "Gameplay3";
        }
        SceneManager.LoadScene(destination);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationButton : MonoBehaviour
{
    // Start is called before the first frame update
    private WobbleUI wobbler;

    [SerializeField]
    private string destination;

    void Start()
    {
        wobbler = GetComponent<WobbleUI>();
    }

    // Update is called once per frame
    public void Click()
    {
        if (GameObject.FindGameObjectWithTag("Transition") == null)
        {
            wobbler.DoTheWobble();

            if (destination == "Gameplay")
            {
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
            }
            Transitioner.instance.FadeIn(destination);
        }
    }
}

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
            Transitioner.instance.FadeIn(destination);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    // Start is called before the first frame update
    public bool fadeIn;
    public string targetRoom;

    private float size;

    private int rotateDir;

    void Start()
    {

        rotateDir = Random.Range(0, 2) == 0 ? -1 : 1;

        if (fadeIn)
        {
            size = 0f;
        }
        else
        {
            size = 1f;
        }

        transform.localScale = Vector3.one * size;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            if (size < 1f)
            {
                size += 1f * Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene(targetRoom);
            }
        }
        else
        {
            if (size > 0f)
            {
                size -= 1f * Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        transform.localScale = Vector3.one * size;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitioner : MonoBehaviour
{
    public GameObject transition;

    public static Transitioner instance = null;

    private AudioSource source;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
        FadeOut();
    }

    // Start is called before the first frame update
    public void FadeIn(string targetRoom)
    {
        if (GameObject.FindGameObjectWithTag("Transition") == null)
        {
            if (source != null)
            {
                source.Play();
            }

            PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
            Transition transCp = Instantiate(transition, transform, false).GetComponent<Transition>();
            transCp.fadeIn = true;
            transCp.targetRoom = targetRoom;
        }
    }

    public void FadeOut()
    {
        if (GameObject.FindGameObjectWithTag("Transition") == null)
        {
            Transition transCp = Instantiate(transition, transform, false).GetComponent<Transition>();
            transCp.fadeIn = false;
        }
    }


}

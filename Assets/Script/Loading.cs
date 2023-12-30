using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public string levelToLoad;
    public GameObject background;
    public GameObject text;
    public GameObject progressBar;
    private int loadProgress = 0;

    void Start()
    {
        background.SetActive(false);
        text.SetActive(false);
        progressBar.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            StartCoroutine(DisplayLoadingScreen(levelToLoad));
        }
    }

    IEnumerator DisplayLoadingScreen(string level)
    {
        background.SetActive(true);
        text.SetActive(true);
        progressBar.SetActive(true);

        progressBar.transform.localScale = new Vector3(loadProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
        text.GetComponent<Text>().text = "Loading Progress: " + loadProgress + "%";

        AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(level);

        while (!async.isDone)
        {
            loadProgress = (int)(async.progress * 100);
            text.GetComponent<Text>().text = "Loading Progress: " + loadProgress + "%";
            progressBar.transform.localScale = new Vector3(async.progress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
            yield return null;
        }
    }
}

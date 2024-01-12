using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    // Start is called before the first frame update
    public int  senceBuildIndex;


    private void OnTriggerEnter2D(Collider2D other)
    {
        changeScene(other);
    }
    public void changeScene(Collider2D other){

        if(other.CompareTag("Player"))
        {
            StartCoroutine(wait());
            SceneManager.LoadScene(senceBuildIndex);
        }
    }
    private IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);

    }

}

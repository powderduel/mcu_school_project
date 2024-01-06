using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDelete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Self());
    }
    IEnumerator Self(){
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }


}

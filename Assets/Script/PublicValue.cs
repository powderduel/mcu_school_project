using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicValue : MonoBehaviour
{
    // Start is called before the first frame update
    public int Hp;
    public GameObject HpBar;
    void Start()
    {
        Hp = 6;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<HpBar.transform.childCount;i++){
            if(Hp>i){
                HpBar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else{
                HpBar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    
}

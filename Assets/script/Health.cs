using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health :MonoBehaviour
{
    
    [SerializeField]
    private Image[] health;
    private int PlayerHealth = Status.PlayerHealth;
    private int attack = Status.attack;
    // Start is called before the first frame update
    private void Start()
    {
        UpdateHealth();
    }

    private void FixedUpdate()
    {
        UpdateHealth();
        UpdateStatus();
        
    }

    private  void UpdateHealth(){
        for(int i = 0; i < health.Length; i++){
            if(i < PlayerHealth){
                health[i].enabled = true;
            }else{
                health[i].enabled = false;
            }
        }
    }
    private void UpdateStatus(){
        Status.PlayerHealth = PlayerHealth;
    }
    public void TakeDamage(){
        PlayerHealth --;
        Status.PlayerHealth = PlayerHealth;
    }
}

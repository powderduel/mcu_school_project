using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadArea : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        player.GetComponent<Health>().TakeDamage();
    }
}

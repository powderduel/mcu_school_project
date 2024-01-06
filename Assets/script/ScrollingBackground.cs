using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed;
    [SerializeField]
    private Renderer bgRend;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        bgRend.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0f);
    }
}

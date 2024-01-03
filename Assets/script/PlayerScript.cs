using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerScript : MonoBehaviour
{
    // 定義一個表示速度的屬性，設置爲 public 可以在 unity 可視化窗口中調整值
    public Vector2 speed = new Vector2(50, 50);
    // Start is called before the first frame update
    // 2 - Store the movement and the component
    private Vector2 movement;// 速度
    private Rigidbody2D rigidbodyComponent; // 緩存 剛體對象
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        // 獲取用戶輸入
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        // 依據輸入設置 當前速度
        movement = new Vector2(
          speed.x * inputX,
          speed.y * inputY);
    }
    void FixedUpdate()
    {
        // 獲取組件並
        if (rigidbodyComponent == null)
        {
            rigidbodyComponent = GetComponent<Rigidbody2D>();
        }
        // 設置速度移動剛體
        rigidbodyComponent.velocity = movement;
    }
}
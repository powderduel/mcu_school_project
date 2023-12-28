using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//YEE_DINO
public class Ground_AI : MonoBehaviour
{
    public float moveSpeed = 3f;  // 敵人移動速度
    public float detectionRange = 3f; // 檢測玩家的範圍
    public Transform player; // 玩家的Transform組件
    public Animator animator;
    SpriteRenderer sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        // 檢查玩家是否在檢測範Q圍內
        if (PlayerInRange())
        {
            animator.SetBool("Run", true);
            // 如果玩家在檢測範圍內，則移動敵人朝向玩家
            MoveTowardsPlayer();
        }
        else
        {
            animator.SetBool("Run", false);
        }
        sprite.flipX = player.position.x > transform.position.x ? true : false;

    }

    bool PlayerInRange()
    {
        // 計算敵人與玩家之間的X軸距離
        float distanceToPlayerX = Mathf.Abs(player.position.x - transform.position.x);

        // 如果X軸距離小於檢測範圍，返回true，否則返回false
        return distanceToPlayerX < detectionRange;
    }

    void MoveTowardsPlayer()
    {
        // 計算X軸上的移動向量
        float moveDirectionX = (player.position.x - transform.position.x > 0) ? 1f : -1f;

        // 移動敵人
        transform.Translate(new Vector2(moveDirectionX, 0f) * moveSpeed * Time.deltaTime);
    }
}

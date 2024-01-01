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
    public int damageAmount = 10; // 對玩家造成的傷害量
    public float cooldownTime = 2f; // 冷卻時間
    private float nextAttackTime = 0f; // 下一次能造成傷害的時間
    public float backwardJumpForce = 5f;
    public float upwardForce = 5f;
    public int EnemyHP = 10;
    SpriteRenderer sprite;
    private Rigidbody2D rb;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // 檢查玩家是否在檢測範Q圍內
        if (PlayerInRange() && Time.time >= nextAttackTime)
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
    /*void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player detected!");
        // 檢查碰撞的對象是否是玩家，並檢查是否已經過了冷卻時間
        if (other.CompareTag("Player") && Time.time >= nextAttackTime)
        {//
            // 對玩家造成傷害
            DealDamage(other.gameObject);
            BackwardJump();
            // 設定下一次能造成傷害的時間
            nextAttackTime = Time.time + cooldownTime;
        }
    }*/
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name);
        Collider2D detectedObject = other.collider;

        if (detectedObject.CompareTag("Player") && Time.time >= nextAttackTime)
        {
            Debug.Log("Player collided with an enemy!");
            DealDamage(other.gameObject);
            BackwardJump();
            EnemyHP -= 2;
            // 設定下一次能造成傷害的時間
            nextAttackTime = Time.time + cooldownTime;
        }
    }
    bool PlayerInRange()
    {
        // 計算敵人與玩家之間的X軸距離
        float distanceToPlayerX = Mathf.Abs(player.position.x - transform.position.x);

        return distanceToPlayerX < detectionRange;
    }

    void MoveTowardsPlayer()
    {
        // 計算X軸上的移動向量
        float moveDirectionX = (player.position.x - transform.position.x > 0) ? 1f : -1f;

        // 移動敵人
        transform.Translate(new Vector2(moveDirectionX, 0f) * moveSpeed * Time.deltaTime);
    }
    
    void DealDamage(GameObject player)
    {
        // 敵人對玩家造成的傷害處理

        Debug.Log("Enemy damaged player!");
        // player.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
    }
    void BackwardJump()
    {
        Debug.Log("Enemy damaged player!");
        // 获取玩家的位置
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        // 计算反方向跳的方向
        Vector2 jumpDirection = (transform.position - playerPosition).normalized;

        // 施加反方向跳的力度
        rb.velocity = new Vector2(jumpDirection.x * backwardJumpForce, jumpDirection.y * backwardJumpForce);
        rb.velocity += new Vector2(0, upwardForce);
        // 防止穿透地图，使用射线检测地面
        
    }
    void Dead() 
    {

        animator.SetBool("Dead", true);
        Destroy(this.gameObject,2f);
        
    }

    void been_attacked(int Player_Damage)
    {
        BackwardJump();

        EnemyHP -= Player_Damage;

        if (EnemyHP <= 0)
        {
            Dead();
        }
    }
}

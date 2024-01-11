using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public EnemySOBase soBase;

    [Header("Stats")]
    [SerializeField] float health;
    [SerializeField] float moveSpeed;
    [SerializeField] float scoreValue;

    [Header("Visuals")]
    Animator animator;

    [Header("Movement")]
    Rigidbody2D rb;
    Transform playerPos;
    Vector2 targetDir;

    [Header("Etc")]
    GameObject player;
    PlayerHealth playerHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerPos = player.transform;
    }

    private void OnEnable()
    {
        health = soBase.health;
        moveSpeed = soBase.moveSpeed;
        scoreValue = soBase.scoreValue;

        targetDir = (playerPos.position - transform.position).normalized;
        SetAnimation(targetDir);
    }

    void Update()
    {
        MoveTowardsPlayer();

        if(Vector2.Distance(transform.position, playerPos.position) < 1)
        {
            playerHealth.TakeDamage();
            gameObject.SetActive(false);
        }
    }

    private void MoveTowardsPlayer()
    {
        rb.velocity = targetDir * moveSpeed;
    }

    public void TakeDamage(float damageValue, float knockbackForce)
    {
        health -= damageValue;

        if (health <= 0)
        {
            DestroyEnemy();
        }
        else
        {
            Vector2 knockbackDirection = -rb.velocity.normalized;
            rb.AddForce(knockbackDirection * (knockbackForce * 1000));
        }
    }

    public void DestroyEnemy()
    {
        EventManager.TriggerEnemyDestroyed(scoreValue);
        gameObject.SetActive(false);
    }

    private void SetAnimation(Vector2 moveDir)
    {
        int animationHash = 0;

        switch (moveDir)
        {
            case Vector2 v when v.Equals(Vector2.up):
                animationHash = Animator.StringToHash(soBase.animMoveUp);
                break;

            case Vector2 v when v.Equals(Vector2.right):
                animationHash = Animator.StringToHash(soBase.animMoveRight);
                break;

            case Vector2 v when v.Equals(Vector2.down):
                animationHash = Animator.StringToHash(soBase.animMoveDown);
                break;

            case Vector2 v when v.Equals(Vector2.left):
                animationHash = Animator.StringToHash(soBase.animMoveLeft);
                break;

            default:
                Debug.LogWarning("Enemy animation switch statemnt hit DEFAULT");
                break;
        }

        animator.Play(animationHash);
    }
}

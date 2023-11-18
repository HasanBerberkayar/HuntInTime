using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Creature
{
    public float attackDistance;

    public float rightBorder;
    public float leftBorder;

    public Transform target;
    public bool isSeen = false;
    public bool isAttacked = false;

    public float skyLimit;

    public LayerMask ignoreLayer1;
    public LayerMask ignoreLayer2;

    public LayerMask ignoreLayer;
    Vector3 rayDirection;
    // Start is called before the first frame update
    void Start()
    {
        ignoreLayer = ignoreLayer1 | ignoreLayer2;
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        deathAnimation = 1.8f;
        deathCount = 1;
        rb2D = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        Die();
        if(transform.rotation.y == 0)
        {
            rayDirection = Quaternion.Euler(0, 0, -45) * transform.right;
        }       
        else
        {
            rayDirection = Quaternion.Euler(0, 0, 45) * transform.right;           
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (0.6f * rayDirection), rayDirection, attackDistance, ~ignoreLayer);

        Debug.DrawLine(transform.position + (0.6f * rayDirection), transform.position + rayDirection * attackDistance , Color.red, ~ignoreLayer);

        if (hit.collider != null && hit.collider == target.GetComponent<Collider2D>())
        {
            Attack(true);
            isAttacked = true;
        }
        else if (isAttacked)
        {
            
            if (transform.position.y < skyLimit)
            {

                rb2D.velocity = Vector2.up * moveSpeed;
            }
            else
            {
                rb2D.velocity = Vector2.up * 0;
                isAttacked = false;
            }
        }
        else if (isSeen)
        {
            
            FollowEnemy();
        }
        else
        {
            
            if (transform.position.y < skyLimit)
            {
                rb2D.velocity = Vector2.up * moveSpeed;
            }
            else
            {
                rb2D.velocity = Vector2.up * 0;
            }

            if (transform.position.x <= leftBorder)
            {
                direction = 1;
                Move(direction);
            }
            else if (transform.position.x >= rightBorder)
            {
                direction = -1;
                Move(direction);
            }
            else if (direction == 1)
            {
                Move(direction);
            }
            else
            {
                Move(direction);
            }

        }
    }

    public void FollowEnemy()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, target.position.z);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, (moveSpeed * 1.4f) * Time.deltaTime);

        if (targetPosition.x < transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isSeen = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isSeen = false;
        }
    }
}



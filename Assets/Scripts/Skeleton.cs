using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Creature
{
    public float sightDistance;
    public float attackDistance;

    public float rightBorder;
    public float leftBorder;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
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

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, attackDistance);
        RaycastHit2D see = Physics2D.Raycast(transform.position, transform.right, sightDistance);

        if (hit.collider != null)
        {
            Attack(true);
        }
        else if (see.collider != null)
        {
            FollowEnemy();
        }
        else
        {
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
}

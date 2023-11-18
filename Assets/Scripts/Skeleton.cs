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

    public LayerMask ignoreLayer1;
    public LayerMask ignoreLayer2;

    public LayerMask ignoreLayer;

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

        RaycastHit2D hit = Physics2D.Raycast(transform.position + (0.6f * transform.right), transform.right, attackDistance, ~ignoreLayer);
        RaycastHit2D see = Physics2D.Raycast(transform.position + (0.6f * transform.right), transform.right, sightDistance, ~ignoreLayer);

        // Ray'ý görselleþtir
        Debug.DrawLine(transform.position + (0.6f * transform.right), transform.position + transform.right * attackDistance, Color.red);

        if (hit.collider != null && hit.collider == target.GetComponent<Collider2D>())
        {
            Debug.Log(hit.transform);
            Attack(true);
        }
        else if (see.collider != null && see.collider == target.GetComponent<Collider2D>())
        {
            FollowEnemy();
        }
        else
        {           
            if (transform.position.x <= leftBorder)
            {
                Debug.Log("a");
                direction = 1;
                Move(direction);
            }
            else if (transform.position.x >= rightBorder)
            {
                Debug.Log("b");
                direction = -1;
                Move(direction);
            }
            else if (direction == 1)
            {
                Debug.Log("c");
                Move(direction);
            }
            else
            {
                Debug.Log("d");
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

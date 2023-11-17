using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Creature
{
    float dashTimer;
    public float dashCoolDown;

    public void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
        // anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        dashTimer = dashCoolDown;
    }

    public void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        dashTimer += Time.deltaTime;
        Dash();
        attackTimer += Time.deltaTime;
        Die();
        if (horizontalInput > 0)
        {
            direction = 1;
            Move(horizontalInput);
        }
        else if (horizontalInput < 0)
        {
            direction = -1;
            Move(horizontalInput);
        }
        if (rb2D.velocity.x == 0)
        {
            //anim.SetBool("isRunning", false);
        }
        Attack(Input.GetKeyDown(KeyCode.Mouse0));
        Jump(Input.GetKeyDown(KeyCode.Space));

    }

    public void Dash()
    {
        if (Input.GetKeyDown((KeyCode.LeftShift)) && dashCoolDown <= dashTimer)
        {
            moveSpeed = 20;
            StartCoroutine(Waiter(0.2f, "Dash"));
            Move(direction);
        }
    }

    IEnumerator Waiter(float second, string functionName)
    {
        yield return new WaitForSeconds(second);

        if (functionName == "Dash")
        {
            dashTimer = 0;
            moveSpeed = 5;
        }
    }

}
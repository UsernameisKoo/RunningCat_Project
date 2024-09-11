using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //public float maxSpeed;
    public float jumpPower;
    public float speed = 3.0f;
    public PlayerMove player;
    public GameManager gameManager;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }
    void Update()
    {
        //Jump
        if (Input.GetButtonUp("Jump"))//!anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            //anim.SetBool("isJumping", true);
        }
        rigid.velocity = new Vector2(speed, rigid.velocity.y); //rigid.velocity.y

    }


    /*//Stop Speed
    if (Input.GetButtonUp("Horizontal"))
    {
        rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

    }*/



    //Animation
    /*if ((anim.GetBool("isJumping"))) Mathf.Abs(rigid.velocity.x) < 0.7*//*rigid.velocity.normalized.x == 0
    {
        anim.SetBool("isWalking", false);
    }
    else
    {
        anim.SetBool("isWalking", true);
    }
}*/
    void FixedUpdate()
    {
        // 

        //Landing Platform

        //transform.Translate(Vector3.right * speed * Time.deltaTime);
        /*  Debug.DrawRay(rigid.position, Vector3.down*4,   new Color(0, 1, 0));

          RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, 10,  LayerMask.GetMask("Platform"));

          if (rayHit.collider != null)
          {
             // if (rayHit.distance < 0.5f)
              {
                 // anim.SetBool("isJumping", false);
              }
          }
      }*/

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            OnDamaged(collision.transform.position);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            if (collision.transform.position.y <= 0) 
            {
                Debug.Log("Player Collision Detected");
                gameManager.HeartDown();
                gameManager.HeartDown();
                gameManager.HeartDown();
                Invoke("Gameover", 0.5f);
            }
        }

        if (collision.gameObject.CompareTag("finish"))
        {
            Debug.Log("Finish Collision Detected");
            gameManager.heart = 3;
            //gameManager.Me.SetActive(false);
            //gameManager.Stages[gameManager.stageIndex].SetActive(false);
            Time.timeScale = 0;
            gameManager.StageClear();
            Debug.Log("Game Clear");
        }
    }



    void OnDamaged(Vector2 targetPos)
    {
        gameManager.HeartDown();
       //Change Layer(Immortal Active)
        gameObject.layer = 11;

        
        //View Alpha 
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        
        
        Invoke("OffDamaged", 2); 
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
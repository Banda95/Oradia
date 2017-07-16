using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public int startingLifes = 3;

    public float baseSpeed = 5f;
    public float jumpForce = 180f;
    public float reJumpForce = 50;

    public Transform groundCheck;
    public LayerMask walkable;

    public Text comboText;

    private AudioSource audioSource;

    private float groundRadius = 0.5f;
    private bool grounded = false;

    private Animator animator;
    private SpriteRenderer sprite;
    private Rigidbody2D rb2D;
    private ScoreSystem scoreSystem;
    private Lives lives;

    private int dir = 1;

    private bool isWalking = false;

    private int currentLifes;
    private int bonusMultiplier = 1;

    // Use this for initialization
    protected void Start()
    {
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        scoreSystem = FindObjectOfType<ScoreSystem>();
        lives = FindObjectOfType<Lives>();
        for (int i = 0; i < startingLifes; i++)
        {
            lives.PlusOneHeart();
        }
        currentLifes = startingLifes;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, walkable);
       
        float x = Input.acceleration.x;
        //float x = 0.35f;
        if (x > 0.15 || x < -0.15)
        {
            isWalking = true;
            MoveX(x);
            if(!audioSource.isPlaying && grounded && GameData.music)
            {
                audioSource.Play();
            }        
        }
        else
        {
            isWalking = false;
            audioSource.Stop();
        }

        UpdateAnimator();
    }

    void Update()
    {
        if (Input.GetMouseButton(0)  && grounded)
        {
            rb2D.AddForce(new Vector2(0, jumpForce));

            audioSource.Stop();
        }
        if(grounded)
        {
            bonusMultiplier = 1;
            UpdateBonusMultiplier();
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y);
        }     
    }

    private void MoveX(float x)
    {
        if (x < 0)
        {
            if (dir != -1)
            {
                dir = -1;
                sprite.flipX = true;
            }
        }
        else
        {
            if(dir != 1)
            {
                dir = 1;
                sprite.flipX = false;
            }       
        }

        rb2D.velocity = new Vector2(dir * baseSpeed * (1 + System.Math.Abs(x)), rb2D.velocity.y);
    }

    private void UpdateAnimator()
    {
        animator.SetBool("IsWalking", isWalking);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Vector2 contactPoint = coll.contacts[0].point;
            Vector2 center = coll.collider.bounds.center;
            BaseEnemy en = coll.gameObject.GetComponent<BaseEnemy>();

            if (contactPoint.y > center.y)
            {
                //I'm above him              
                scoreSystem.AddPoints((uint)(en.GetPoints() * bonusMultiplier));

                en.Die();
                rb2D.AddForce(new Vector2(0, reJumpForce));
                UpdateBonusMultiplier();

                bonusMultiplier++;
                
            }
            else
            {
                en.Die();

                MinusHealth();                              
            }
        }
        else if(coll.gameObject.tag == "Bonus")
        {
            lives.PlusOneHeart();
            currentLifes++;

            Destroy(coll.gameObject);
        }
        else if(coll.gameObject.tag == "Meteor")
        {
            MinusHealth();
            Destroy(coll.gameObject);
        }
    }

    private void MinusHealth()
    {
        if (currentLifes > 0)
        {
            lives.MinusOneHeart();
            currentLifes--;
        }

        if (currentLifes == 0)
        {
            //Endgame.
            GameData.last_score = scoreSystem.currentPoints;
            GameData.last_time = scoreSystem.seconds;           
                    
           
            FindObjectOfType<SceneLoader>().Load("EndGameMenu");
        }
    }

    private void UpdateBonusMultiplier()
    {
        if(bonusMultiplier == 1)
        {
            comboText.text = "";
        }
        else
        {
            comboText.gameObject.SetActive(true);
            comboText.text = "X" + bonusMultiplier;
            StartCoroutine(IncreaseFontSize());
        }
    }

    private IEnumerator IncreaseFontSize()
    {
        comboText.fontSize = 30;
        for (int i = 0; i < 8; i++)
        {
            yield return new WaitForSeconds(0.02f);
            comboText.fontSize += 10;
        }
    }

}

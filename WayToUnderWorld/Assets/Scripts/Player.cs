using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    static public Player player;
    public Rigidbody2D rb;

    Animator animator;
    [Header("Move")]
    [SerializeField] public float speedMove;
    [SerializeField] public float maxSpeedMove;
    [SerializeField] float horizontal;

    private void Awake()
    {
        player = this;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        speedMove = maxSpeedMove;
        AudioManager.Instance.PlayMusic("BackGround");
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        horizontal *= speedMove;
        rb.velocity = new Vector2(horizontal, (rb.velocity.y + Gamemanager.gamemanager.playerFly)*Time.deltaTime);

        if (horizontal != 0)
        {
            animator.SetBool("Run", true);
            if (horizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else if (horizontal == 0)
        {
            animator.SetBool("Run", false);
        }
    }

    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Barrier")
        {
            Gamemanager.gamemanager.pointGameOver = 0;
        }
    }
}

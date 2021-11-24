using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float movePower = 5f;
    private float realMovePower = 5f; // ���� �̵� �ӵ�
    public float jumpPower = 5f;
    private float realJumpPower = 5f; // ���� ���� ��
    public GameManager gameManager;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    PolygonCollider2D polygonCollider;

    Vector3 movement;
    bool isJumping = false;

    private float item_jump_cooltime;


    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioCoin;
    public AudioClip audioDie;
    public AudioClip audioFinish;

    AudioSource audioSource;

    //----------------------------------------[Overrid Function]

    // Initialzation
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                break;
            case "COIN":
                audioSource.clip = audioCoin;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                break;
        }
    }


    // Graphic & Input Updates
    void Update()
    {
        
        //RandomInt = UnityEngine.Random.Range(1, 4);

        // Moving
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            animator.SetBool("isMoving", true);

            spriteRenderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.SetBool("isMoving", true);

            spriteRenderer.flipX = true;
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            isJumping = true;
            animator.SetBool("isJumping", true);    // Jumping Flog
            animator.SetTrigger("doJumping");   // Jumping Animation

            audioSource.clip = audioJump;
            audioSource.Play();

        }

        if (jumpPower == 20.0f)
        {
            item_jump_cooltime += Time.deltaTime;
            if (item_jump_cooltime > 10)
            {
                jumpPower = 15.0f;
            }
            animator.SetTrigger("doItem");
        }
    }

    // Attach Event
    void OnTriggerEnter2D(Collider2D collision)
    {

        //Debug.Log("Attach : " + collision.gameObject.layer);

        if (collision.gameObject.layer == 8 && rigid.velocity.y < 0)
            animator.SetBool("isJumping", false);   // Landing

        // Enemy hit
        if (collision.gameObject.tag == "Enemy" && !collision.isTrigger && rigid.velocity.y < -5f)  // -6f : ���� Ŀ�� ���� ������ ������
        {

            audioSource.clip = audioAttack;
            audioSource.Play();

            EnemyMove enemy = collision.gameObject.GetComponent<EnemyMove>();
            enemy.Attack();

            // ����
            Vector2 killVelocity = new Vector2(0, 13f);
            rigid.AddForce(killVelocity, ForceMode2D.Impulse);

            EnemyMove enemyMove = GameObject.Find("Enemy").GetComponent<EnemyMove>();

            if (enemyMove.enemyhealth > 0)
            {
                gameManager.HealthUp();
            }

            

        }

        if (collision.gameObject.tag == "coin")
        {
            audioSource.clip = audioCoin;
            audioSource.Play();

            // Point
            bool isYellow = collision.gameObject.name.Contains("YellowJewelry");
            bool isMint = collision.gameObject.name.Contains("MintJewelry");
            bool isRed = collision.gameObject.name.Contains("RedJewelry");
            bool isPurple = collision.gameObject.name.Contains("PurpleJewelry");
            bool isRainbow = collision.gameObject.name.Contains("RainbowJewelry");

            ScoreManager score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

            if (isYellow)
            {
                score.stagePoint += 50;
            }
            else if (isMint)
            {
                score.stagePoint += 70;
            }
            else if (isRed)
            {
                score.stagePoint += 90;
            }
            else if (isPurple)
            {
                score.stagePoint += 100;
            }
            else if (isRainbow)
            {
                score.stagePoint += 130;
            }

            // Deactive Item
            collision.gameObject.SetActive(false);
        }else if (collision.gameObject.tag == "portal")
        {
            Invoke("clear", 0.5f);
        }
        if (collision.gameObject.tag == "healthItem")
        {
            audioSource.clip = audioItem;
            audioSource.Play();

            gameManager.HealthUp();
        }
        if (collision.gameObject.tag == "invincibleItem")
        {
            audioSource.clip = audioItem;
            audioSource.Play();

            StartCoroutine("GetInvincible");
        }
        if (collision.gameObject.tag == "random")
        {
            audioSource.clip = audioItem;
            audioSource.Play();

            Random();
        }

    }

    // ��Ż ���� ȭ�� ����
    void clear()
    {
        Time.timeScale = 0;
        // Next Stage
        gameManager.UIImg.SetActive(true);
        gameManager.UINextBtn.SetActive(true);
        gameManager.ClearStory.SetActive(true);
        gameManager.Clear.SetActive(true);

        audioSource.clip = audioFinish;
        audioSource.Play();
    }

    void Random()
    {
        int ran = UnityEngine.Random.Range(1, 4);
        if (ran == 1) // RandomInt�� 1�̶��
        {
            gameManager.HealthUp();
            Debug.Log("ȿ�� : ��������");
        }
        else if (ran == 2)
        {
            StartCoroutine("GetInvincible");
            Debug.Log("ȿ�� : ����");
        }
        else if (ran == 3)
        {
            audioSource.clip = audioJump;
            audioSource.Play();
            jumpPower = 20.0f;
            if (jumpPower == 20.0f)
            {
                item_jump_cooltime += Time.deltaTime;
                if (item_jump_cooltime > 10)
                {
                    jumpPower = 15.0f;
                }
                animator.SetTrigger("doItem");
            }
            Debug.Log("ȿ�� : ���� ����");
        }
    }

    IEnumerator GetInvincible()//���������� ����
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        animator.SetTrigger("doItem");
        Physics2D.IgnoreLayerCollision(13, 14, true);
        yield return new WaitForSeconds(10f);
        Physics2D.IgnoreLayerCollision(13, 14, false);
        Invoke("OffDamaged", 5);
    }

    // Detach Event
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Detach : " + other.gameObject.layer);
    }

    // Physics engine Updates
    void FixedUpdate()
    {
        Move();
        Jump();
    }

    //-----------------------------------------------------[Movement Function]

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;

            spriteRenderer.flipX = false;
        }

        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;

            spriteRenderer.flipX = true;
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }


    void Jump()
    {
        if (!isJumping)
            return;

        // Prevent Velocity amplification.
        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            onDamaged(collision.transform.position);
        }
        if(collision.gameObject.tag == "spikes")
        {
            onDamaged(collision.transform.position);
        }
        if (collision.gameObject.CompareTag("item_jump"))
        {
            audioSource.clip = audioJump;
            audioSource.Play();
            jumpPower = 20.0f;
        }
    }

    void onDamaged(Vector2 targetPos)
    {
        audioSource.clip = audioDamaged;
        audioSource.Play();

        // Health Down
        gameManager.HealthDown();

        // Change Layer (Immortal Active)
        gameObject.layer = 12;

        // View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        // Animation
        animator.SetTrigger("doDamaged");

        Invoke("OffDamaged", 2);    // �Լ� ȣ��
    }

    void OffDamaged()
    {
        gameObject.layer = 13;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void OnDie()
    {
        audioSource.clip = audioDie;
        audioSource.Play();

        // Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        // Collider Disable
        polygonCollider.enabled = false;

        // �״� �ִϸ��̼� �ֱ�
        animator.SetTrigger("doDie");
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }

}
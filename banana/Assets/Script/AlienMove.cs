using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlienMove : MonoBehaviour
{
    public float moveSpeed = 5.0f; //�÷��̾� �̵� �ӵ�
    private float realMoveSpeed = 5.0f; // ���� �̵� �ӵ�
    public float jumpPower = 5.0f; //�÷��̾� ���� ��
    private float realJumpPower = 5.0f; // ���� ���� ��

    public Rigidbody2D rigid;

    float horizontal; //����, ������ ���Ⱚ�� �޴� ����
    public bool isground;   // ���� ���� bool��
    //public Transform groundCheck;   // player����ġ
    //public float groundRadius = 0.2f;   // ������ ����
    //public LayerMask whatIsGround; // � layer�� ��������
    public Animator animator; // �ִϸ����� �߰�

    //Renderer rend;//��������Ʈ ������   
    //Color c;//���� ����
    //public int health;//���� �����
    //public Image[] hearts;//����� �̹���
    //public Sprite healthSprite;//����� ��������Ʈ 

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //rend = GetComponent<Renderer>();
        //c = rend.material.color;
        //health = 3;
    }

    private void FixedUpdate()
    {
        //isground = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        // if(isground == true){
        //      animator.SetBool("jump", false);
        // }

        Move(); //�÷��̾� �̵�
        Jump(); //����   
        //healthCheck();//ü��Ȯ��
    }

    // ������ ��..!
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isground = true;//ground�� �����ϸ� isground�� true��
        }
        if (collision.gameObject.tag == "Enemy")
        {
            onDamaged(collision.transform.position);
        }

        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    StartCoroutine("GetInvulnerable");
        //}
    }

    void onDamaged(Vector2 targetPos)
    {
        // Change Layer (Immortal Active)
        gameObject.layer = 11;

        // View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        Invoke("OffDamaged", 3);    // �Լ� ȣ��
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }


    //IEnumerator GetInvulnerable()
    //{
    //    health--;
    //    Physics2D.IgnoreLayerCollision(13, 14, true);
    //    c.a = 0.5f;
    //    rend.material.color = c;
    //    yield return new WaitForSeconds(3f);
    //    Physics2D.IgnoreLayerCollision(13, 14, false);
    //    c.a = 1f;
    //    rend.material.color = c;
    //}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "portal")
        {
            SceneManager.LoadScene("AlienMove");
        }
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("jumpGround"))
    //    {
    //        realJumpPower = 2 * jumpPower;
    //    }
    //    else if (collision.gameObject.CompareTag("stickGround"))
    //    {
    //        realJumpPower = jumpPower / 2;
    //    }
    //    else
    //    {
    //        realJumpPower = jumpPower;
    //    }

    //    if (collision.gameObject.CompareTag("fastGround"))
    //    {
    //        realMoveSpeed = 2 * moveSpeed;
    //    }
    //    else if (collision.gameObject.CompareTag("slowGround"))
    //    {
    //        realMoveSpeed = moveSpeed / 2;
    //    }
    //    else
    //    {
    //        realMoveSpeed = moveSpeed;
    //    }
    //}


    void Jump()
    {
        if (Input.GetButton("Jump")) //���� Ű�� ������ ��//ground�̸鼭 �����̽��� ������ 
        {
            if (isground == true) //���� ������ ���� ��
            {
                animator.SetBool("jump", true);  // �����ϸ� �ִϸ����� ���� jump true
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); //�������� ���� �ش�.
                isground = false;
            }
            else return; //���� ���� ���� �������� �ʰ� �ٷ� return.
        }
    }

    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
            animator.SetBool("walk", true);  // �ִϸ����� ���� walk�� true
            if (horizontal >= 0) this.transform.eulerAngles = new Vector3(0, 0, 0);

            else this.transform.eulerAngles = new Vector3(0, 180, 0);

        }
        else
        {
            animator.SetBool("walk", false);    // �¿�� �ȿ����̸� �ִϸ����� ���� walk false.
        }

        Vector3 dir = math.abs(horizontal) * Vector3.right; //������ �ڷ����� ���߱� ���� ������ ���ο� Vector3 ����

        this.transform.Translate(dir * moveSpeed * Time.deltaTime); //������Ʈ �̵� �Լ�
    }

    //void healthCheck()
    //{
    //    for (int i = 0; i < hearts.Length; i++)
    //    {
    //        hearts[i].sprite = healthSprite;
    //        if (i < health)
    //        {
    //            hearts[i].enabled = true;
    //        }
    //        else
    //        {
    //            hearts[i].enabled = false;
    //        }
    //        if (health <= 0)
    //        {
    //            SceneManager.LoadScene("Main");//ü�� 0�̸� scene�ٽúҷ����� 
    //        }
    //    }
    //}

}
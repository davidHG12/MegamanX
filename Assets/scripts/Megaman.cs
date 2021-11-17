using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megaman : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float jumpSpeed = 20;
    [SerializeField] GameObject vfx_death;
    [SerializeField] AudioClip sfx_death;
    public bool pause;
    Animator myAnimator;
    int soundcompare;
    Rigidbody2D myBody;
    BoxCollider2D myCollider;
    int saltoExtra = 25;
    bool isSaltoExtra = false;
    public float mov;

    public bool Dash;
    public float Dash_time;
    public float Dash_speed;

    public bool movRight;

    public GameObject[] bullets;
    public bool shooting;
    private float shoot_time;
    public float time;
    public GameObject point;
    [SerializeField] AudioClip sfx_jump;
    [SerializeField] AudioClip sfx_shoot;
    [SerializeField] AudioClip sfx_dash;
    bool soundstop = false;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!pause)
        {
            Movement();
            Jump();
            Falling();
            Resetear();
            Shoot();
        }
    }
    void FixedUpdate()
    {
        Dash_Press();
    }

    void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            AudioSource.PlayClipAtPoint(sfx_shoot, Camera.main.transform.position);
            shoot_time = 0.01f;
            GameObject obj = Instantiate(bullets[0], point.transform.position, transform.rotation) as GameObject;
            if(!shooting)
            {
                shooting = true;
            }
        }
        if(shooting)
        {
            shoot_time += 1 * Time.deltaTime;
            myAnimator.SetLayerWeight(0,0);
            myAnimator.SetLayerWeight(1,1);
        }
        else
        {
            myAnimator.SetLayerWeight(0,1);
            myAnimator.SetLayerWeight(1,0);
        }
        if(shoot_time >= time)
        {
            shooting = false;
            shoot_time = 0;
        }
    }
    void Movement()
    {
        // float mov = Input.GetAxis("Horizontal");
        mov = Input.GetAxis("Horizontal");
        if(mov > 0)
        {
            movRight = true;
        }
        else if(mov < 0)
        {
            movRight = false;
        }
        if (mov != 0 && !Dash)
        {
            transform.localScale = new Vector2(Mathf.Sign(mov), 1);
            myAnimator.SetBool("isRunning", true);
            transform.Translate(new Vector2(mov * speed * Time.deltaTime, 0));
        }
        else
            myAnimator.SetBool("isRunning", false);
    }

    void Resetear()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector2(0, 5);
        }
    }

    void Jump()
    {

        if (isGrounded() && !myAnimator.GetBool("isJumping"))
        {
            myAnimator.SetBool("isFalling", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioSource.PlayClipAtPoint(sfx_jump, Camera.main.transform.position);
                myAnimator.SetTrigger("Jump");
                myAnimator.SetBool("isJumping", true);
                myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && saltoExtra >= 1 && !isGrounded() && !isSaltoExtra)

        {
            AudioSource.PlayClipAtPoint(sfx_jump, Camera.main.transform.position);
            myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);

            myAnimator.SetTrigger("Jump");

            saltoExtra--;
            myAnimator.SetBool("isJumping", true);
            isSaltoExtra = true;

        }

        if (isGrounded() && isSaltoExtra)
            isSaltoExtra = false;
            myAnimator.SetBool("isJumping", false);
    }

    void Falling()
    {
        if (myBody.velocity.y < 0 && !myAnimator.GetBool("isJumping"))
        {
            myAnimator.SetBool("isFalling", true);
        }
    }

    bool isGrounded()
    {
        RaycastHit2D myRayCast = Physics2D.Raycast(myCollider.bounds.center, Vector2.down, myCollider.bounds.extents.y + 0.2f, LayerMask.GetMask("Ground"));
        return myRayCast.collider != null;
    }

    public void afterJumpEvent()
    {
        myAnimator.SetBool("isFalling", true);
        myAnimator.SetBool("isJumping", false);
    }
    void afterDash()
    {
        myAnimator.SetBool("isDashing",false);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine("Die");
        }
    }
    IEnumerator Die()
    {
        pause = true;
        myAnimator.SetBool("Death", true);
        myBody.isKinematic = true;
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayClipAtPoint(sfx_death, Camera.main.transform.position);
        Instantiate(vfx_death, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void Dash_Press()
    {
        if (Input.GetKey(KeyCode.X))
        {
            if(!soundstop)
            {
                AudioSource.PlayClipAtPoint(sfx_dash, Camera.main.transform.position);
                soundstop = true;
            }
            Dash_time += 1 * Time.deltaTime;
            if(Dash_time < 0.35f)
            {
                
                Dash = true;
                myAnimator.SetBool("isDashing", true);
                if(movRight)
                    transform.Translate(Vector3.right * Dash_speed * Time.fixedDeltaTime);
                if(!movRight)
                    transform.Translate(Vector3.left * Dash_speed * Time.fixedDeltaTime);
            }
            else
            {
                Dash = false;
                myAnimator.SetBool("isDashing", false);
            }
        }
        else
        {
            soundstop = false;
            Dash = false;
            myAnimator.SetBool("isDashing", false);
            Dash_time = 0;
        }
    }
}

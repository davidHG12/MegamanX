using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megaman : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] BoxCollider2D pies;
    [SerializeField] float jumpSpeed;
    [SerializeField] GameObject An_Bullet;
    [SerializeField] float dashCooldown;
    [SerializeField] float dashTime;
    [SerializeField] float dashSpeed;
    [SerializeField] float nextFire;

    /*
    [SerializeField] float dashSpeed;
    private float dashTime;
    [SerializeField] float startDashTime;
 
    */

    Animator myAnimator;
    Rigidbody2D myBody;
    BoxCollider2D myCollider;


    private float tamX;
    private float tamY;

    float minX, maxX, minY, maxY, fireInterval;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();


        tamX = (GetComponent<SpriteRenderer>()).bounds.size.x;
        tamY = (GetComponent<SpriteRenderer>()).bounds.size.y;

        Vector2 esquinaSupDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        maxX = esquinaSupDer.x - tamX / 2;
        maxY = esquinaSupDer.y - tamY / 2;

        Vector2 esquinaInfIzq = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        minX = esquinaInfIzq.x + tamX / 2;
        minY = esquinaInfIzq.y ;

    }

    // Update is called once per frame
    void Update()
    {
        dashCooldown -= Time.deltaTime;
        Mover();
        Saltar();
        falling();
        Fire();
        Dash();
    }

    
    void Fire()
    {
        if (Input.GetKey(KeyCode.X))
        {
            myAnimator.SetLayerWeight(1, 1);
            if (Input.GetKeyDown(KeyCode.X))
            {
                float mov = Input.GetAxis("Horizontal");
                Instantiate(An_Bullet, transform.position + new Vector3(1, tamX / 4, 1), transform.rotation);
                fireInterval = Time.time + nextFire;
            }

        }
        else
            myAnimator.SetLayerWeight(1, 0);
    }
    void Mover()
    {
        float mov = Input.GetAxis("Horizontal");
        if (mov != 0)
        {

            transform.localScale = new Vector2(Mathf.Sign(mov), 1);
            myAnimator.SetBool("running", true);
            transform.Translate(new Vector2(mov * speed * Time.deltaTime, 0));

        }
        else
        {

            myAnimator.SetBool("running", false);

        }

    }

    void Saltar()
    {
        
       if(isGrounded() && !myAnimator.GetBool("jumping"))
        {
            myAnimator.SetBool("jumping", false);
            myAnimator.SetBool("falling", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myAnimator.SetTrigger("takeof");
                myAnimator.SetBool("jumping", true);
                myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            }
        }
        
    }

    void falling()
    {
        if (myBody.velocity.y < 0 && !myAnimator.GetBool("jumping"))
        {
            myAnimator.SetBool("falling", true);
        }
    }


    bool isGrounded()
    {
        //return pies.IsTouchingLayers(LayerMask.GetMask("Ground"));
        RaycastHit2D myRaycast = Physics2D.Raycast(myCollider.bounds.center, Vector2.down, myCollider.bounds.extents.y + 0.2f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(myCollider.bounds.center, new Vector2(0, (myCollider.bounds.extents.y + 0.2f)*-1), Color.red);
        return myRaycast.collider != null; 
    }


    void AfterTakeOffEvent()
    {
        myAnimator.SetBool("jumping", false);
        myAnimator.SetBool("falling", true);
    }

    void AfterTakeOff2Event()
    {
        myAnimator.SetBool("dash", false);
        myAnimator.SetBool("dashing", true);
    }


    void Dash()
    {
        float movd = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.Z))
        {

            myAnimator.SetBool("dashing", true);

            if (dashCooldown <= 0)
            {
                StopDash();
            }
            else
            {

                if (movd != 0)
                {

                    transform.localScale = new Vector2(Mathf.Sign(movd), 1);
                    transform.Translate(new Vector2(movd * dashSpeed, 0));
                    //myBody.AddForce(new Vector2(dashSpeed, 0), ForceMode2D.Impulse);


                }
                else
                {
                   
                    myAnimator.SetBool("dashing", false);

                }

                dashCooldown = 3;
            }
        }
    }

    private void StopDash()
    {
        myBody.velocity = Vector2.zero;
        dashCooldown = dashTime;
        myAnimator.SetBool("dashing", false);
    }
}

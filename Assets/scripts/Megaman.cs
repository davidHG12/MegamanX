using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megaman : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] BoxCollider2D pies;
    [SerializeField] float jumpSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashCooldown;
    [SerializeField] float dashTime;
    bool isDashing;
    bool canDash = true;
    float direction = 1;
    float horizontal;
    float normalGravity;
    int saltoExtra = 25;
    bool isSaltoExtra = false;


    /*
    [SerializeField] float dashSpeed;
    private float dashTime;
    [SerializeField] float startDashTime;


 
    */

    IEnumerator dashCoroutine;
    Animator myAnimator;
    Rigidbody2D myBody;
    BoxCollider2D myCollider;
   

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        normalGravity = myBody.gravityScale;

}

    // Update is called once per frame
    void Update()
    {
        //dashCooldown -= Time.deltaTime;
        if (horizontal != 0)
        {
            direction = horizontal;
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        Mover();
        Saltar();
        falling();
        Fire();


        if (Input.GetKey(KeyCode.Z) && canDash == true)
        {
            if (dashCoroutine != null)
            {
                StopCoroutine(dashCoroutine);
            }
            dashCoroutine = Dash(dashTime, dashCooldown);
            StartCoroutine(dashCoroutine);
        }

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            myBody.AddForce(new Vector2(direction * dashSpeed,0), ForceMode2D.Impulse);
        }
    }

    void Fire()
    {
        if (Input.GetKey(KeyCode.X))
        {
            myAnimator.SetLayerWeight(1, 1);
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
        if (isGrounded() && !myAnimator.GetBool("jumping"))

        {

            myAnimator.SetBool("jumping", false);

            myAnimator.SetBool("falling", false);

            if (Input.GetKeyDown(KeyCode.Space))

            {

                Debug.Log("salte 1");

                myAnimator.SetTrigger("takeof");

                myAnimator.SetBool("jumping", true);

                myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);

            }

        }

        if (Input.GetKeyDown(KeyCode.Space) && myAnimator.GetBool("jumping") && saltoExtra >= 1 && !isGrounded() && !isSaltoExtra)

        {

            Debug.Log("salte 2");

            myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);

            myAnimator.SetTrigger("takeof");

            saltoExtra--;

            isSaltoExtra = true;

        }

        if(isGrounded() && isSaltoExtra)
        isSaltoExtra = false;

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

    IEnumerator Dash(float dashDuration, float dashCooldown)
    {
        myAnimator.SetBool("dashing", true);
        Vector2 originalVelocity = myBody.velocity;
        isDashing = true;
        canDash = false;
        myBody.gravityScale = 0;
        myBody.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashDuration);
        myAnimator.SetBool("dashing", false);
        isDashing = false;
        myBody.gravityScale = normalGravity;
        myBody.velocity = originalVelocity;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    /*IEnumerator Dash (float Direction)
    {
        isDashing = true;
        myBody.velocity = new Vector2(myBody.velocity.x, 0f);
        myBody.AddForce(new Vector2(dashDistance * Direction, 0f), ForceMode2D.Impulse);
        float gravity = myBody.gravityScale;
        myBody.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        myBody.gravityScale = gravity;
    }*/

    /*void Dash()
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
                    //myBody.velocity = new Vector2(myBody.velocity.x, 0f);
                    //myBody.AddForce(new Vector2(dashDistance * Direction, 0f), ForceMode2D.Impulse);
                    //myBody.AddForce(new Vector2(dashSpeed, 0), ForceMode2D.Impulse);
                    //myBody.velocity = new Vector2(dashSpeed, 0);


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
        //myBody.velocity = Vector2.zero;
        dashCooldown = dashTime;
        myAnimator.SetBool("dashing", false);
    }*/
}

using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float mov;
    public int direction;
    public Megaman megaman;
    public bool right;


    void Awake()
    {
        megaman = FindObjectOfType<Megaman>();
        
        // if(mov == 0)
        // {
        //     Update
        // }
    }
    void Start()
    {
        mov = megaman.mov;
        right = megaman.movRight;
        if(mov > 0)
            direction = 1;
        else if(mov < 0)
            direction = -1;
        if(mov == 0)
        {
            if(right)
            {
                direction = 1;
            }
            if(!right)
            {
                direction = -1;
            }
        }
        StartCoroutine(Destroy());
    }
    void Update()
    {
        // if (right > 0)
        //     transform.Translate(Vector3.right * speed * Time.deltaTime);
        // else if (right < 0)
        //     transform.Translate(Vector3.left * speed * Time.deltaTime);Ç
        transform.Translate(new Vector3(direction, 0, 0) * speed * Time.deltaTime);
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //NO FUNCIONANDO AÚN
        Debug.Log("LAYER: "+ col.gameObject.layer);
        if(col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
    // private void OnTriggerEnter2D(Collider2D trr)
    // {
    //     // Debug.Log("FUNCIONO?"+ trr.name);
    //     // if(trr.gameObject.tag == "Enemy")
    //     // {
    //     //     Destroy(trr.gameObject);
    //     //     Destroy(this.gameObject);
    //     // }
    // }
}

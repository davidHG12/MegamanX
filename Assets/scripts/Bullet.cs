using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speedB;
    [SerializeField] bool col=false;
    [SerializeField] GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        //transform.localScale = new Vector2(Mathf.Sign(mov), 1);
        
        transform.Translate(new Vector2(speedB * Time.deltaTime, 0));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(speedB * Time.deltaTime, 0));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Bullet"))

            col = true;
            
            Destroy(this.gameObject);
    }
}

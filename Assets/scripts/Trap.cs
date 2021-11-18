using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    [SerializeField] AudioClip sfx_shootT;
    public float actionDis;
    public GameObject balaI;
    public GameObject balaD;
    [SerializeField] float cadencia;
    [SerializeField] Transform piontI;
    [SerializeField] Transform piontD;
    void Start()
    {
        player = GameObject.Find("Megaman").transform;
    }

    // Update is called once per frame
    void Update()
    {

        //Similar a la torreta pero pues con deteccion de proximidad
        #region
        cadencia = cadencia + Time.deltaTime;
        if (Vector2.Distance(transform.position, player.position) < actionDis)
        {
            
            if (cadencia >= 2)
            {
                Instantiate(balaI, piontI.position, Quaternion.identity);
                Instantiate(balaD, piontD.position, Quaternion.identity);
                cadencia = 0;
            }
        }
        #endregion
    }
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Bullet"))
        {
            //AudioSource.PlayClipAtPoint(sfx_death, Camera.main.transform.position);
            Destroy(c.gameObject);
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform player;
    [SerializeField] AudioClip sfx_shootT;
    public float actionDis;
    public GameObject bala;
    [SerializeField] float cadencia;
    [SerializeField] Transform piont;
    void Start()
    {
        player = GameObject.Find("Megaman").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Basicamente lo que hace disparar a la torreta 
        #region
        cadencia = cadencia + Time.deltaTime;
        if (cadencia >= 2)
        {
            Instantiate(bala, piont.position, Quaternion.identity);
            cadencia = 0;
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

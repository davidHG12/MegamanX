using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] GameObject player;
    void Update()
    {
        // if(Vector2.Distance(player.transform.position, transform.position) <= range)
        // Debug.Log("Entro");
        // if (Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("Player")) != null)
        // Debug.Log("Entro");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color= new Color(1f, 0, 0, 0.35f);
        Gizmos.DrawSphere(transform.position, range);
        //Gizmos.DrawLine(transform.position, player.transform.position);
    }
}

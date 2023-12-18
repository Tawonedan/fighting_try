using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitTrigger : MonoBehaviour
{
    // player nempel lalu hit
    public bool isDamaging;
    public float damage = 10;

    // private void OnTriggerStay(Collider col)
    // {
    //     if(col.tag == "Player")
    //        col.SendMessage((isDamaging)?"HealDamage":"TakeDamage",Time.deltaTime * damage);
           
    // }

    private void OnTriggerStay(Collider col)
    {
    if (col.tag == "Player")
    {
        if (isDamaging)
        {
            col.SendMessage("HealDamage", Time.deltaTime * damage);
        }
        else
        {
            col.SendMessage("TakeDamage", Time.deltaTime * damage);
            }
        }
    }

    
}

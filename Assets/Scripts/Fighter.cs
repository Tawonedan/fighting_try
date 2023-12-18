using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public Collider[] attackHitboxes;
    // Update is called once per frame

    public AudioSource audioPlayer;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
           LaunchAttack(attackHitboxes[0]);
        if(Input.GetKeyDown(KeyCode.H))
           LaunchAttack(attackHitboxes[1]);
    }

    private void LaunchAttack (Collider col)
    {
        Collider[] cols = Physics.OverlapBox(col.bounds.center,col.bounds.extents,col.transform.rotation,LayerMask.GetMask("Hitbox"));
        foreach(Collider c in cols)
        {
            if(c.transform.parent.parent == transform)
               continue;

            float damage = 0;
            switch(c.name)
            {
                case "Head":
                damage = 30;
                break;
                case "Torso":
                damage = 10;
                break;
                default:
                Debug.Log("Unable to identify body part");
                break;
            }


            c.SendMessageUpwards("TakeDamage",damage);
            audioPlayer.Play(); 


        }
    }
}

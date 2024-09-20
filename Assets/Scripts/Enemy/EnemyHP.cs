using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] public int EnemyHealth = 10;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            //Replace with a script playerstats
            collision.GetComponent<PlayerHP>();
        }
    }
}

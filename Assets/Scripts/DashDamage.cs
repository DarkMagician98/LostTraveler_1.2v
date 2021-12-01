using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && transform.parent.gameObject.GetComponent<BossScript>().IsDashing())
        {
            collision.GetComponent<PlayerMovement>().StunPlayer(4.0f);
        }
    }
}

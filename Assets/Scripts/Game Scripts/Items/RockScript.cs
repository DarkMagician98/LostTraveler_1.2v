using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
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
        if(collision.tag == "Wall")
        {
            Destroy(this.gameObject);
        }

        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().DamagePlayer(8);
            Destroy(this.gameObject);
        }
    }
}

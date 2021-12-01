using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool P_IsHurt { private set; get; }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Trap")
        {
            P_IsHurt = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Trap")
        {
            P_IsHurt = false;
        }
    }
}

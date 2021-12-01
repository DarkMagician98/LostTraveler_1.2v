using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject s_WinScreen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            FindObjectOfType<PlayerMovement>().ActivateWin();
            s_WinScreen.SetActive(true);
        }
    }
}

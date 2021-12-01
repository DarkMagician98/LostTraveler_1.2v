using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingScript : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioManager p_AudioManager;
    void Start()
    {
        p_AudioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            p_AudioManager.queueSound("mouseClick");
            GetComponent<Animator>().SetTrigger("isPicked");
        }

    }

    public void DestroyStone()
    {
        Destroy(this.gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStoneScript : MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Wall") {
            p_AudioManager.queueSound("stoneBreak");
            GetComponent<Animator>().SetTrigger("destroyStone");
        
        }
    }

    public void DestroyStone()
    {
        Destroy(this.gameObject);
    }
}

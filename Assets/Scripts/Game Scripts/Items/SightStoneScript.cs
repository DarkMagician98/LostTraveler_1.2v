using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightStoneScript : MonoBehaviour
{
    private AudioManager p_AudioManager;
    private PlayerMovement p_Target;
    // Start is called before the first frame update
    void Start()
    {
        p_Target = FindObjectOfType<PlayerMovement>();
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
            p_Target.IncrementClairvoyance();
            p_AudioManager.queueSound("mouseClick");
            GetComponent<Animator>().SetTrigger("isPicked");
        }
    }

    public void DestroyStone()
    {
        Destroy(this.gameObject);
    }
}

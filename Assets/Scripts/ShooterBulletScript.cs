using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBulletScript : MonoBehaviour
{
    private bool p_isGlitched;

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
        if(collision.tag == "Wall" || collision.tag == "Player")
        {
            if(collision.tag == "Player" && !p_isGlitched)
            {
                p_isGlitched = true;
                PlayerMovement p_Target = FindObjectOfType<PlayerMovement>();
                if (!p_Target.IsClairvoyancing())
                {
                    p_Target.glitchFlashlight();
                }
            }

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Animator>().SetTrigger("destroyBullet");
        }
    }

    private void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

}

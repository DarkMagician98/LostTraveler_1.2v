using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{

    [SerializeField] Material s_UnlitMaterial;
    [SerializeField] Color s_UnlitColor;

    //public EnemyTypes s_type;

    private Animator p_TrapAnimator;
    private Color p_DefaultColor;
    private Material p_DefaultMaterial;
    private GameObject p_Target;
    private bool p_DamagePlayer;
    private float p_PlayerOnTrapTime;
    private AudioManager p_AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerMovement.TakeDamage(10);
        // if( PlayerMovement.takeDamageEvent
        //PlayerMovement.takeDamageEvent += check;
        p_AudioManager = FindObjectOfType<AudioManager>();
        p_PlayerOnTrapTime = 0;
        p_DefaultMaterial = transform.GetComponent<SpriteRenderer>().material;
        p_DefaultColor = transform.GetComponent<SpriteRenderer>().color;
        p_Target = FindObjectOfType<PlayerMovement>().gameObject;
        p_TrapAnimator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerFeet")
        {
            p_AudioManager.queueSound("activateTrap");
            p_TrapAnimator.SetBool("isActivate", true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            p_PlayerOnTrapTime += Time.deltaTime;
           // Debug.Log(p_PlayerOnTrapTime);

            if(p_PlayerOnTrapTime >= .60f)
            {
                p_DamagePlayer = true;
                p_PlayerOnTrapTime = 0;
            }

            if (p_DamagePlayer)
            {
                other.GetComponent<PlayerMovement>().DamagePlayer(GetComponent<EnemyAttribute>().s_Damage);
                p_DamagePlayer = false;
            }
        }
    }


    public void DamagePlayer()
    {
        p_DamagePlayer = true;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "PlayerFeet")
        {
            p_TrapAnimator.SetBool("isActivate", false);
        }
    }

}

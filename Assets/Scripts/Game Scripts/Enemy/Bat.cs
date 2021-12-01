using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Bat : MonoBehaviour
{
    [SerializeField] float s_Speed;
    [SerializeField] float s_AttackRange;
    [SerializeField] float s_ChaseRange;
    [SerializeField] float s_FlyingAreaLimit;
    [SerializeField] Material s_UnlitMaterial;
    [SerializeField] Color s_UnlitColor;
    [SerializeField] GameObject s_HeartObj;
  //  [SerializeField] GameObject s_HeartParent;

    private GameObject p_HeartHolder;
    private GameObject p_Target;
    private bool p_AttackTarget, p_ChaseTarget;
    private bool p_SeeLight;
    private Vector2 p_NormalPosition;
    private bool p_ContinueChasing;
    private Color p_DefaultColor;
    private Material p_DefaultMaterial;
    private Camera cam;
    private AudioManager p_AudioManager;
    private float p_FlapTime;
    private Vector3 p_DefaultPosition;


    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        p_DefaultPosition = transform.position; 
        p_AudioManager = FindObjectOfType<AudioManager>();
        p_DefaultMaterial = transform.GetComponent<SpriteRenderer>().material;
        p_DefaultColor = transform.GetComponent<SpriteRenderer>().color;
        p_Target = FindObjectOfType<PlayerMovement>().gameObject;
        cam = FindObjectOfType<Camera>();
    }
    
     /*
       Check if target is close.
       Copy current position
       Begin Attack
       While player is within attackrange, continue attack.
       If the player is outside of range, take final position of player and use that as an attack position, then go back to normal position. 
       If not, if the player got damage by the attack, get back to normal position. 
     */

    // Update is called once per frame
    void Update()
    {
     //   Vector2 heartOffset = new Vector2(.2f, .50f);
     //   p_HeartHolder.transform.position = cam.WorldToScreenPoint(new Vector2(transform.position.x - heartOffset.x, transform.position.y + heartOffset.y));

        //Debug.Log(p_SeeLight);
        float step = s_Speed * Time.deltaTime;

       // Debug.Log(distanceToPlayer);
        /*
         Given distance = 10;
         If player is within that distance, check distance difference.
         Use that difference to change the volume, should be playerDistance/maxDistance; Set to one when at the closest, zero at the furthest.
         If player is still within max distance, keep playing sound;    
       */
        
        p_ChaseTarget = false;
        p_AttackTarget = false;
        
        if(Vector2.Distance(p_Target.transform.position,transform.position) <= s_ChaseRange)
        {
            p_ChaseTarget = true;
        }

        if (Vector2.Distance(p_Target.transform.position, transform.position) <= s_AttackRange)
        {
            p_AttackTarget = true;
        }


        if(Vector2.Distance(p_DefaultPosition,transform.position) < s_FlyingAreaLimit && Vector2.Distance(p_Target.transform.position, p_DefaultPosition) < s_FlyingAreaLimit && p_Target.GetComponent<PlayerMovement>().IsFlashlightOn()) 
        { 

            p_FlapTime += Time.deltaTime;

            if (p_ChaseTarget || p_ContinueChasing )
            {
                float distanceToPlayer = Vector2.Distance(transform.position, p_Target.transform.position);
                float panStereo = Mathf.Sign((p_Target.transform.position - transform.position).x);

                if (p_Target.GetComponent<PlayerMovement>().IsFlashlightOn()) { p_SeeLight = true; }

                if (p_SeeLight)
                {
                    if (p_FlapTime >= .5f)
                    {
                        //  Debug.Log("Flapping");
                        p_AudioManager.queueSound("flap",panStereo,(s_ChaseRange-distanceToPlayer)/s_ChaseRange);
                        //Debug.Log(distanceToPlayer / s_ChaseRange);
                        p_FlapTime = 0;
                    }
                    p_ContinueChasing = true;
                    transform.position = Vector2.MoveTowards(transform.position, p_Target.transform.position, step);
                }

                if (p_AttackTarget)
                {
                    //Do Attack Here.
                }
            }

        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, p_DefaultPosition, step);
        }
           

       // DEBUG(p_)
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, s_AttackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, s_ChaseRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, s_FlyingAreaLimit);
    }
}

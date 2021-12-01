using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLover : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float s_AttackRate;
    
    private bool p_HitPlayer;
    private float p_BeginAttackTime;
    private GameObject p_Target;
    private float p_Offset;

    [SerializeField] float s_Speed;
    [SerializeField] float s_AttackRange;
    [SerializeField] float s_ChaseRange;
    [SerializeField] float s_FlyingAreaLimit;
    [SerializeField] Material s_UnlitMaterial;
    [SerializeField] Color s_UnlitColor;
    [SerializeField] GameObject s_HeartObj;
    //  [SerializeField] GameObject s_HeartParent;

    private GameObject p_HeartHolder;
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

    void Start()
    {
        p_Offset = .65f;
        p_BeginAttackTime = 0;
        p_Target = FindObjectOfType<PlayerMovement>().gameObject;

        p_DefaultPosition = transform.position;
        p_AudioManager = FindObjectOfType<AudioManager>();
        p_DefaultMaterial = transform.GetComponent<SpriteRenderer>().material;
        p_DefaultColor = transform.GetComponent<SpriteRenderer>().color;
        p_Target = FindObjectOfType<PlayerMovement>().gameObject;
        cam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {


        if(p_HitPlayer && Mathf.Abs(p_BeginAttackTime - Time.time) >= p_Offset)
        {
            p_Target.GetComponent<PlayerMovement>().DamagePlayer(5);
            p_BeginAttackTime = Time.time;
        }

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

        if (Vector2.Distance(p_Target.transform.position, transform.position) <= s_ChaseRange)
        {
            p_ChaseTarget = true;
        }

        if (Vector2.Distance(p_Target.transform.position, transform.position) <= s_AttackRange)
        {
            p_AttackTarget = true;
        }


        if (p_Target.GetComponent<PlayerMovement>().IsFlashlightOn() && Vector2.Distance(p_DefaultPosition, transform.position) < s_FlyingAreaLimit && Vector2.Distance(p_Target.transform.position, p_DefaultPosition) < s_FlyingAreaLimit)
        {

            Vector2 llRotation = (p_Target.transform.position - transform.position).normalized;
            float rotationInDegrees = Mathf.Atan2(llRotation.y, llRotation.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(rotationInDegrees, Vector3.forward);

            p_FlapTime += Time.deltaTime;

            if (p_ChaseTarget || p_ContinueChasing)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, p_Target.transform.position);
                float panStereo = Mathf.Sign((p_Target.transform.position - transform.position).x);

                if (p_Target.GetComponent<PlayerMovement>().IsFlashlightOn()) { p_SeeLight = true; }

                if (p_SeeLight)
                {
                    if (p_FlapTime >= .5f)
                    {
                        //  Debug.Log("Flapping");
                        p_AudioManager.queueSound("flap", panStereo, (s_ChaseRange - distanceToPlayer) / s_ChaseRange);
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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            p_Target.GetComponent<PlayerMovement>().DamagePlayer(6);
            p_BeginAttackTime = Time.time;
            p_HitPlayer = true;
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            p_HitPlayer = false;
        }
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

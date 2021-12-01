using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BossScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float s_TimeToSurvive;
    [SerializeField] private float s_AttackDistance;
    [SerializeField] private Animator bossAnimator;



    [Header("MOVEMENT")]
    [SerializeField] private float s_Speed;
    [SerializeField] private float s_ChaseSpeed;
    [SerializeField] private float s_ChaseRadius;

    [Header("THROWING STONES")]
    [SerializeField] private GameObject s_StoneObj;
    [SerializeField] private GameObject s_Rock;
    [SerializeField] private GameObject s_RockParent;
    [SerializeField] private float s_ThrowTimeMax;
    [SerializeField] private int s_MaxStoneToThrow;
    [SerializeField] private float s_RockSpeed;


    [Header("DASH")]

    [SerializeField] private float s_DashTime;
    [SerializeField] private float s_Distance;
    [SerializeField] private float s_DashSpeed;

    [Header("CAMERA")]

    [SerializeField] private GameObject s_CameraController;


    private float p_SurviveTime;
    private float p_ThrowTime;
    private ArrayList p_StoneLists;
    private ArrayList p_StoneDirectionLists;
    private bool p_CanThrow;
    private GameObject p_Target;
    private bool p_CanChase;
    private GameObject p_StoneObj;
    private bool p_BossActivated;
    private bool p_StartAttack;
    private int p_CurrentStoneThrown;
    private bool p_ThrowStone;

    private Rigidbody2D p_Rb;

    enum Direction { up, down, left, right};
    Direction lastDirection = Direction.down;
    private bool p_CanDash;
    private bool p_StillRolling;
    private bool p_DoneThrowing;
    private bool p_BossAttacking;

    private AudioManager p_AudioManager;

    void Start()
    {
        p_AudioManager = FindObjectOfType<AudioManager>();
        p_StillRolling = false;
        p_Rb = GetComponent<Rigidbody2D>();
        p_Target = FindObjectOfType<PlayerMovement>().gameObject;
        p_StoneLists = new ArrayList();
        p_StoneDirectionLists = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {

        if (p_BossActivated)
        {
            if (p_StartAttack && p_CanChase)
            {
              //  Debug.Log("Attack initiated!");
                StartCoroutine(BossAttack());
                p_StartAttack = false;
            }
            p_SurviveTime = Time.time;

            if (Vector2.Distance(transform.position, p_Target.transform.position) < s_ChaseRadius)
            {
                p_CanChase = true;
            }
            else
            {
                p_CanChase = false;
            }
            bossAnimator.SetBool("moving", p_CanChase);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Playing...");
            for(int i = 0; i < 360; i += 90)
            {
                Vector2 rockDirection = new Vector2(Mathf.Cos(i * Mathf.Deg2Rad), Mathf.Sin(i * Mathf.Deg2Rad));
                GameObject rockObject = Instantiate(s_Rock);
                transform.SetParent(s_RockParent.transform);
                rockObject.transform.position = transform.position + new Vector3(rockDirection.x,rockDirection.y,transform.position.z) * 2.0f;

            }
        }

    }

    IEnumerator ThrowRock()
    {
        
        while (true)
        {
            if (p_CurrentStoneThrown <= s_MaxStoneToThrow)
            {
                p_AudioManager.queueSound("whoosh");
                p_CurrentStoneThrown++;
                p_CanThrow = true;
                GameObject stoneObject = Instantiate(s_StoneObj);
                stoneObject.transform.SetParent(transform);
                stoneObject.transform.position = transform.position;
                stoneObject.GetComponent<Rigidbody2D>().velocity = (p_Target.transform.position - transform.position).normalized * s_RockSpeed * Time.deltaTime;
                yield return new WaitForSeconds(.5f);
            }
            else
            {
              //  Debug.Log("Done throwing bruh");
                p_CurrentStoneThrown = 0;
                break;
            }
        }

        p_DoneThrowing = true;
    }

    IEnumerator DashBoss()
    {
        p_AudioManager.queueSound("groan");
        int dashCount = 0;
        // p_ReadyToRegenerate = false;
            p_StillRolling = true;
            float startTime = Time.time;
            Vector2 direction = (p_Target.transform.position - transform.position);
            s_DashTime = s_Distance / s_DashSpeed;

            while (Time.time < startTime + s_DashTime)
            {
                p_Rb.MovePosition(p_Rb.position + (direction * s_DashSpeed * Time.deltaTime));
                yield return null;
            }

           // Debug.Log("Done rolling bruh");
            p_StillRolling = false;
    }

   
    IEnumerator BossAttack()
    {
        p_BossAttacking = true;
        while (true)
        {
            yield return new WaitForSeconds(3.0f);
            StartCoroutine(DashBoss());
            yield return new WaitUntil(() => p_StillRolling == false);
            StartCoroutine(DashBoss());
            yield return new WaitUntil(() => p_StillRolling == false);
            StartCoroutine(ThrowRock());
            yield return new WaitUntil(()=>p_DoneThrowing == true);
            p_DoneThrowing = false;
            p_AudioManager.queueSound("throwStones");
            s_CameraController.GetComponent<CameraShake>().shakeDuration = .75f;
            yield return new WaitForSeconds(1.0f);
            FindObjectOfType<TilePositions>().DropStones();
            yield return new WaitForSeconds(2.0f);
            StartCoroutine(DashBoss());
            yield return new WaitUntil(() => p_StillRolling == false);
            StartCoroutine(ThrowRock());
            yield return new WaitUntil(() => p_DoneThrowing == true);
            p_DoneThrowing = false;
        }
    }

    public bool IsDashing() => p_StillRolling;
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, s_ChaseRadius);
        Gizmos.DrawWireSphere(transform.position, s_AttackDistance);
    }

    void FixedUpdate()
    {
        if (p_CanChase)
        {
             
            

            Vector2 moveDirection = new Vector2(p_Target.transform.position.x - transform.position.x, p_Target.transform.position.y - transform.position.y);

            // Get absolute value of x and y
            float absX = Mathf.Abs(moveDirection.x);
            float absY = Mathf.Abs(moveDirection.y);

            if(absX > absY)
            {
                if(absX > 0)
                {
                    lastDirection = Direction.right;
                  //  transform.GetChild(3).gameObject.SetActive(true);
                    bossAnimator.SetInteger("lastDirection", 4);
                } else
                {
                    lastDirection = Direction.left;
                  //  transform.GetChild(2).gameObject.SetActive(true);
                    bossAnimator.SetInteger("lastDirection", 3);
                }
            }
            else
            {
                if(absY > 0)
                {
                    lastDirection = Direction.up;
                  //  transform.GetChild(0).gameObject.SetActive(true);
                    bossAnimator.SetInteger("lastDirection", 1);
                } else
                {
                    lastDirection = Direction.down;
                  //  transform.GetChild(1).gameObject.SetActive(true);
                    bossAnimator.SetInteger("lastDirection", 2);
                }
            }
            bossAnimator.SetFloat("xDir", moveDirection.x);
            bossAnimator.SetFloat("yDir", moveDirection.y);

            if(!p_StillRolling)
            {
                p_Rb.MovePosition(Vector2.MoveTowards(transform.position, p_Target.transform.position, s_ChaseSpeed * Time.deltaTime));
               // transform.position = Vector2.MoveTowards(transform.position, p_Target.transform.position, s_ChaseSpeed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Wall" && p_StillRolling)
        {
            p_AudioManager.queueSound("bang");
            s_CameraController.GetComponent<CameraShake>().shakeDuration = .5f;
        }

        if(collision.collider.tag == "FallingStone")
        {
           p_AudioManager.queueSound("bang");
            s_CameraController.GetComponent<CameraShake>().shakeDuration = .5f;
            Destroy(collision.collider.gameObject);
        }

    }

    public bool IsBossAttacking() => p_BossAttacking;

    public void ActivateBoss()
    {
        p_BossActivated = true;
        p_StartAttack = true;
    }

    public void DeactivateBoss()
    {
        p_BossActivated = false;
        p_StartAttack = true;
    }




}

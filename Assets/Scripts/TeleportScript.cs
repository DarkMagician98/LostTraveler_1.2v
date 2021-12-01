using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TeleportScript : MonoBehaviour
{

    [SerializeField] GameObject s_TeleportTo;
    [SerializeField] Animator s_PanelAnimator;


    private GameObject p_Target;
    private bool p_CanTeleport;
    private bool p_PlayerDestination;
    private Vector2 p_TargetDirection;
    private AudioManager p_AudioManager;


    // Start is called before the first frame update
    void Start()
    {
        p_CanTeleport = true;
        // StartCoroutine(ResetTeleport());
        p_AudioManager = FindObjectOfType<AudioManager>();
        p_Target = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (p_PlayerDestination)
        {
            p_Target.transform.DOMove(new Vector2(transform.position.x, transform.position.y) + p_TargetDirection * 1.2f, .5f);
            StartCoroutine(StartScene());
            StartCoroutine(ResetTeleport());
            p_PlayerDestination = false;
        }
    }

    public void setDestination(bool isDestination)
    {
        p_PlayerDestination = isDestination;
    }
    public void setCanTeleport(bool readyToTeleport)
    {
        p_CanTeleport = readyToTeleport;
    }

    public Vector2 getTargetDirection()
    {
       return p_Target.GetComponent<PlayerMovement>().GetPlayerDirection();
    }

    public void setTargetDirection(Vector2 direction)
    {
        p_TargetDirection = direction;
    }

    IEnumerator ResetTeleport()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            p_CanTeleport = true;
            break;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && p_CanTeleport)
        {
            StartCoroutine(LoadTransitionScene());
            p_CanTeleport = false;
        }
    }

    IEnumerator LoadTransitionScene()
    {
        while (true)
        {
            s_PanelAnimator.SetTrigger("end");
            s_TeleportTo.GetComponent<TeleportScript>().setTargetDirection(p_Target.GetComponent<PlayerMovement>().GetPlayerDirection());
            p_AudioManager.queueSound("bell");
            yield return new WaitForSeconds(1.0f);
            s_TeleportTo.GetComponent<TeleportScript>().setDestination(true);
            s_TeleportTo.GetComponent<TeleportScript>().setCanTeleport(false);
            p_Target.transform.position = s_TeleportTo.transform.position;
           // s_PanelAnimator.SetTrigger("start");
            break;
        }
    }

    IEnumerator StartScene()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            s_PanelAnimator.SetTrigger("start");
            break;
        }
    }

}

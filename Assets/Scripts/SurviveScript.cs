using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SurviveScript : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI surviveText;
    [SerializeField] GameObject s_DoorToExit;
    [SerializeField] private int s_MaxTime;
    [SerializeField] DialogueManager s_DialogueManager;
    [SerializeField] DialogueScript s_DialogueScript;

    private int p_StartTime, p_CurrentTime;
    private bool p_IsStartTime;
    private GameObject p_Boss;
    private bool p_IsEnded;

    // Start is called before the first frame update
    void Start()
    {
        p_CurrentTime = s_MaxTime;
        p_Boss = FindObjectOfType<BossScript>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (p_IsStartTime && !p_IsEnded)
        {
            Debug.Log("Started time");
            p_CurrentTime = (s_MaxTime - (int)Mathf.Abs(p_StartTime - Time.time));
            surviveText.text = p_CurrentTime.ToString();
        }

        if(p_CurrentTime <= 0)
        {
            FindObjectOfType<PlayerMovement>().ActivateWin();
            p_IsEnded = true;
        }
        //Debug.Log(p_Boss.GetComponent<BossScript>().IsBossAttacking());
       // Debug.Log((int)Time.unscaledTime + "|" + (int)Time.time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!p_IsStartTime && collision.tag == "Player" && p_Boss.GetComponent<BossScript>().IsBossAttacking())
        {
            s_DialogueManager.activateDialogue();
            s_DialogueManager.addDialogue(s_DialogueScript);
            s_DoorToExit.SetActive(true);
            Debug.Log("Hit!!!");
            p_StartTime = (int)Time.time;
            p_IsStartTime = true;
        }
    }
}

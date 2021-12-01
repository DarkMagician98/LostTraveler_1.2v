using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    private enum TriggerType {ACTIVATE,DEACTIVATE,DESTROY, TOGGLE}

    

    [SerializeField] private TriggerType s_TriggerType;
    [SerializeField] private GameObject s_Target;
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
        if(collision.tag == "Player")
        {
            if(s_TriggerType == TriggerType.ACTIVATE)
            {
                s_Target.SetActive(true);
            }
            else if(s_TriggerType == TriggerType.DEACTIVATE)
            {
                s_Target.SetActive(false);
            }
            else if(s_TriggerType == TriggerType.DESTROY)
            {
                Destroy(s_Target);
            }
            else if(s_TriggerType == TriggerType.TOGGLE)
            {
                s_Target.SetActive(!s_Target.activeSelf);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (s_TriggerType == TriggerType.TOGGLE)
            {
                s_Target.SetActive(!s_Target.activeSelf);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject p_Target;
    private string p_PlayerSteppedTeleporter;
    void Start()
    {
        p_Target = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {


/*
        foreach (GameObject child in transform)
        {
            if(child.GetComponent<TeleportScript>().GetTeleportToName() == p_PlayerSteppedTeleporter)
            {
                p_Target.transform.position = child.transform.position;
                break;
            }
        }*/



    }
}

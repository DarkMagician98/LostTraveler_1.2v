using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class BackAndForthScript : MonoBehaviour
{
   
    
    // Start is called before the first frame update
    private Vector3 p_DefaultPosition;
    [SerializeField] private float s_TravelDistance;
    [SerializeField] private float s_Speed;
    [SerializeField] private float s_MaxAcceleration;
    [SerializeField] private Ease easeType;
    [SerializeField] private bool isVertical;
    [SerializeField] private float s_Duration = 3.0f;
 

    private float p_DefaultSpeed;
    private float p_CurrentAcceleration;
    private bool p_HasReachMax;
    private bool p_HasReachMin;

    void Start()
    {
        p_DefaultPosition = transform.position;
        p_DefaultSpeed = s_Speed;
        if (isVertical)
        {
            transform.DOMoveY(transform.position.y + s_TravelDistance, s_Duration).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            transform.DOMoveX(transform.position.x + s_TravelDistance, s_Duration).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
        }
    }

    // Update is called once per frame
    void Update()
    {
       // transform.do
        //if(Input.GetKeyDown())
        // s_MaxAcceleration += .1f;
        /*      if(Vector2.Distance(p_DefaultPosition,transform.position) >= s_TravelDistance-1 && p_HasReachMax)
               {
                   Debug.Log("Forth");
                   s_Speed = p_DefaultSpeed;
                   p_CurrentAcceleration = 0;
                   p_HasReachMin = true;
                   p_HasReachMax = false;
               }
              else if(Vector2.Distance(p_DefaultPosition, transform.position) <= 0 && p_HasReachMin)
               {
                   Debug.Log("Back");
                   s_Speed = p_DefaultSpeed;
                   p_CurrentAcceleration = 0;
                   p_HasReachMax = true;
                   p_HasReachMin = false;
               }

               p_CurrentAcceleration += .05f;
               p_CurrentAcceleration = Mathf.Clamp(p_CurrentAcceleration, 0, s_MaxAcceleration);
               s_Speed += p_CurrentAcceleration;*/
        //transform.position = new Vector3(p_DefaultPosition.x + (Mathf.PingPong(s_Speed * Time.deltaTime, s_TravelDistance)),p_DefaultPosition.y,p_DefaultPosition.z);

       // transform.domo
        //Debug.Log(Vector2.Distance(p_DefaultPosition, transform.position));
    }
}

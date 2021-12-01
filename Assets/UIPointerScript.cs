using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPointerScript : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{

    private AudioManager p_AudioManager;
    // Start is called before the first frame update
    void Start()
    {
        p_AudioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hover");
        p_AudioManager.queueSound("click");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        p_AudioManager.queueSound("mouseClick");
    }
}

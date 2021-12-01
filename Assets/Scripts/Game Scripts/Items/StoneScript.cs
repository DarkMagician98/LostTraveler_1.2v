using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    private bool p_DestroyObject;
    private Vector3 p_RandomDirection;
    private bool p_DestroyStarted;
    private float p_DestroyCountdownTime;

    private AudioManager p_AudioManager;



    // Start is called before the first frame update
    void Start()
    {
        p_AudioManager = FindObjectOfType<AudioManager>();
        Random.InitState(Random.Range(0, 9999));
        p_RandomDirection = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 1);
    }

    private void OnEnable()
    {
        p_DestroyCountdownTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
            if(Mathf.Abs(p_DestroyCountdownTime - Time.time) >= 7.0f)
            {
                Destroy(this.gameObject);
            }
       
    }

  
    public void PlayFall()
    {
        p_AudioManager.queueSound("fall");
    }

    public void PlayDrop()
    {
        p_AudioManager.queueSound("bang");
    }
    public Vector3 RandomDirection() => p_RandomDirection;
    public bool IsStoneDestroyed() => p_DestroyObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall" || collision.tag == "Player")
        {
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class KeyScript : MonoBehaviour
{
    private enum KeyTypes { DEFAULT_KEY, TIMED_KEY}


    Light2D p_KeyLight;
    [SerializeField] KeyTypes s_KeyType;
    [SerializeField] float duration;
    [SerializeField] GameObject[] DestroyObjects;




    float t;
    [SerializeField] float p_DefaultRadius;

    private AudioManager p_AudioManager;
  
    void Start()
    {
        p_AudioManager = FindObjectOfType<AudioManager>();
     //   p_DefaultT = .10f;
      // p_KeyLight=transform.GetChild(0).gameObject.GetComponent<Light2D>();
      // p_DefaultRadius = p_KeyLight.pointLightOuterRadius;
      // p_KeyLight.ou
       
        
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(Mathf.PingPong(p_DefaultT, duration));
        //p_KeyLight.pointLightOuterRadius = p_DefaultRadius + Mathf.PingPong(Time.time/2.0f, p_DefaultRadius);

        //p_KeyLight.falloffIntensity = Mathf.PingPong(.10f, duration);
        //float t = Mathf.PingPong(Time.time, duration) / duration;
       // lt.color = Color.Lerp(color0, color1, t);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {

            p_AudioManager.queueSound("mouseClick");
            FindObjectOfType<CheckpointManagerScript>().IncrementKeyCount();
            foreach(GameObject child in DestroyObjects)
            {
                Destroy(child.gameObject);
            }
            Destroy(this.gameObject);
           
        }
    }

    IEnumerator changeLightIntensity()
    {
        while (true)
        {
          //  p_KeyLight.falloffIntensity
        }
    }
}

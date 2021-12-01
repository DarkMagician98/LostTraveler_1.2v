using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    private static CheckpointManagerScript instance;

    //Some things to save.
    private int keyCount;
    private bool runTutorial;
    private GameObject p_Target;

    //Things to tweak when scene is enabled.

    [SerializeField] GameObject s_DoorsHolderObject;
    private bool p_IsDoorsHolderActive;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        p_Target = FindObjectOfType<PlayerMovement>().gameObject;
        runTutorial = true;
    }

    void Start()
    {
       

      //  Debug.Log("Run Tut: " + runTutorial);
        //InvokeRepeating("printCount", Time.time, 1.0f);
    }

    public void IncrementKeyCount()
    {
        keyCount++;
    }

    public bool RunTutorial() => runTutorial;

    public void SetTutorial(bool status)
    {
        runTutorial = status;
    }

    void Update()
    {
        Debug.Log("Key count: " + keyCount);

        if (s_DoorsHolderObject != null)
        {

            if (s_DoorsHolderObject.gameObject.activeSelf && !p_IsDoorsHolderActive)
            {
                //s_DoorsHolderObject.transform.GetChild(0).gameObject.SetActive(false);
                if (keyCount - 1 >= 0)
                {
                    Debug.Log("Checkpoint in");
                    for (int i = keyCount-1; i >= 0; i--)
                    {
                        s_DoorsHolderObject.transform.GetChild(i).gameObject.SetActive(false);
                    }

                    if(keyCount >= 3)
                    {
                        FindObjectOfType<BossScript>().ActivateBoss();
                    }
                }

                p_IsDoorsHolderActive = true;
            }
            else if (!s_DoorsHolderObject.gameObject.activeSelf)
            {
                p_IsDoorsHolderActive = false;
            }
        }
        else
        {
            Debug.Log("Searching for door script...");
            s_DoorsHolderObject = FindObjectOfType<DoorScript>().gameObject;
            p_IsDoorsHolderActive = false;
        }
    }
}

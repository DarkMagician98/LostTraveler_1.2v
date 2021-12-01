using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceAreaUIScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToDarkDungeon()
    {
        this.gameObject.SetActive(false);
        FindObjectOfType<SceneTransition>().ChangeScene("DarkDungeon");
    }
    
    public void GoToIceDungeon()
    {
        FindObjectOfType<SceneTransition>().ChangeScene("IceDungeon");
    }

    public void GoToSoundDungeon()
    {
        FindObjectOfType<SceneTransition>().ChangeScene("SoundDungeon");
    }





}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntranceScript : MonoBehaviour
{
    private enum DungeonType{DD,ID,SD}
    [SerializeField] private DungeonType s_DungeonType;
    [SerializeField] private GameObject s_DungeonListObj;

    private SceneTransition p_SceneTransitionObject;
    // Start is called before the first frame update
    
    void Start()
    {
        p_SceneTransitionObject = FindObjectOfType<SceneTransition>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        if (s_DungeonType == DungeonType.DD)
        {
            p_SceneTransitionObject.ChangeScene("DarkDungeon");
            //  SceneManager.LoadScene("DarkDungeon");
        }
        else if (s_DungeonType == DungeonType.ID)
        {
            p_SceneTransitionObject.ChangeScene("IceDungeon");
        }
        else if (s_DungeonType == DungeonType.SD)
        {
            p_SceneTransitionObject.ChangeScene("SoundDungeon");
        }
        transform.parent.transform.gameObject.SetActive(false);
    }

    public void CheckClick()
    {
        Debug.Log("Check click");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            s_DungeonListObj.SetActive(true);
        }
       /* if(collision.tag == "Player")
        {
            if (s_DungeonType == DungeonType.DD)
            {

                p_SceneTransitionObject.ChangeScene("DarkDungeon");
              //  SceneManager.LoadScene("DarkDungeon");
            }
            else if (s_DungeonType == DungeonType.ID)
            {
                p_SceneTransitionObject.ChangeScene("IceDungeon");
            }
            else if (s_DungeonType == DungeonType.SD)
            {
                p_SceneTransitionObject.ChangeScene("SoundDungeon");
            }
        }*/
    }
}

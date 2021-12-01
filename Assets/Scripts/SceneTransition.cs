using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField] Animator s_PanelAnimator;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      /*  if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(LoadTransitionScene());
        }*/
    }

    public void ChangeScene(string name)
    {
        StartCoroutine(LoadTransitionScene(name));
    }

    IEnumerator LoadTransitionScene(string name)
    {
        while (true)
        {
            s_PanelAnimator.SetTrigger("end");
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(name);
            break;
        }
    }
    IEnumerator LoadTransitionScene()
    {
        while (true)
        {
            s_PanelAnimator.SetTrigger("end");
            yield return new WaitForSeconds(1.5f);
            s_PanelAnimator.SetTrigger("start");
           // SceneManager.LoadScene(name);
            break;
        }
    }
}

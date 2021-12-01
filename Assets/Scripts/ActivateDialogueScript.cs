using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialogueScript : MonoBehaviour
{
    // Start is called before the first frame update
    private string key;
    [SerializeField] DialogueScript s_Dialogue;
    [SerializeField] bool s_DestroyDialogue;
    private bool isToBeDestroyed;
    private DialogueManager dialogueManager;


    void Start()
    {
 /*       key = dialogue.key;

        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetString(key, "false");
        }
        else
        {
            if (PlayerPrefs.GetString(key) == "true")
            {
                Destroy(this.gameObject);
            }
        }*/

        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
                StartCoroutine(activateNewDialogue());
        }
    }

    IEnumerator activateNewDialogue()
    { 
        yield return new WaitForSeconds(.10f);
        dialogueManager.activateDialogue();
        if (s_DestroyDialogue)
        {
            dialogueManager.addDialogue(s_Dialogue);
            Destroy(this.gameObject);
        }
        else
        {
            dialogueManager.addDialogue(s_Dialogue);
        }
    }

    private void OnDisable()
    {

    }
}

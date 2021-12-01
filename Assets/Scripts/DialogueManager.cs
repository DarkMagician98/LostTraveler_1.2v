using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DialogueManager : MonoBehaviour
{

    // [SerializeField] private Text dialogueUI;
    [SerializeField] private TMPro.TextMeshProUGUI dialogueUI;
    [SerializeField] private TMPro.TextMeshProUGUI nameUI;
    [SerializeField] private DialogueScript[] sentenceList;
    [SerializeField] private GameObject activateDialogueObj;

    private Queue<DialogueScript> dialogueList;

    public static DialogueManager instance;
    private string temp;
    private bool isDone;
    private int currentIndex;

    private AudioManager p_AudioManager;
    // private Queue<DialogueScript> dialogueList;
    //   public static bool skipTutorial = false;

    private void Awake()
    {
        p_AudioManager = FindObjectOfType<AudioManager>();
        currentIndex =  0;
        dialogueList = new Queue<DialogueScript>();
        /*
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }   
       DontDestroyOnLoad(gameObject);*/

        //   if (!skipTutorial)
        //  {
        //   storeDialogue();
        //  skipTutorial = true;
        //    }
        //  StartCoroutine(readCurrentDialogue());
    }



    public void storeDialogue()
    {
        dialogueList.Clear();
        foreach (DialogueScript dialogue in sentenceList)
        {
            dialogueList.Enqueue(dialogue);
        }
        activateDialogue();
        // dialogueList.Enqueue(dialogues);
        //dialogues = dialogues;
    }

    public void addDialogue(DialogueScript dialogue)
    {
        dialogueList.Enqueue(dialogue);
    }

    private void readCurrentDialogue()
    {
        if (dialogueList.Count > 0)
        {
            Time.timeScale = 0;
            DialogueScript dialogues = dialogueList.Peek();
            if (dialogueUI != null)
            {
                dialogueUI.text = dialogues.sentences[Mathf.Min(dialogues.sentences.Length - 1, currentIndex)];
            }

            if (dialogues.sentences.Length <= currentIndex)
            {

                deactivateDialogue();
                Time.timeScale = 1;
                currentIndex = 0;
                dialogueList.Dequeue();
                /*  if(dialogues.key == "goodjob")
                  {
                      SceneManager.LoadScene("LevelScene");
                  }*/
            }
        }

    }

    public bool hasDialogue()
    {
        return dialogueList.Count > 0;
    }

    public void activateDialogue()
    {

        //  if (activateDialogueObj != null)
        // {
        activateDialogueObj.SetActive(true);
        //   }    
    }

    public void deactivateDialogue()
    {
        // if (activateDialogueObj != null)
        //  {
        activateDialogueObj.SetActive(false);
        //   }
    }


    public void incrementCurrentIndex()
    {
        p_AudioManager.queueSound("mouseClick");
        currentIndex++;
    }

    private void OnDisable()
    {
        if (Time.timeScale < 1)
        {
            Time.timeScale = 1;
        }
        //PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        readCurrentDialogue();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            incrementCurrentIndex();
        }
    }


    /*    IEnumerator readCurrentDialogue()
        {
            if (dialogueList.Count > 0)
            {
                DialogueScript currentDialogue = dialogueList.Peek();
                foreach(string sentence in currentDialogue.sentences)
                {
                    StartCoroutine(showCurrentDialogue(sentence));
                    yield return new WaitWhile(() => isDone = true);
                    isDone = false;
                }
            }

        }

        IEnumerator showCurrentDialogue(string dialogue)
        {

            foreach(char a in dialogue)
            {
                dialogueUI.text += a;
                yield return new WaitForSeconds(.1f);
            }


            dialogueUI.text = "";
            isDone = true;
            temp = "";
            yield return new WaitForSeconds(2.0f);
        }*/






}

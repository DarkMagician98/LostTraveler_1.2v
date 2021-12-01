using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWallScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject s_SettingsUI;
    private AudioManager p_AudioManager;
    void Start()
    {
        p_AudioManager = FindObjectOfType<AudioManager>();
        p_AudioManager.queueSound("menuMusic");
    }

    private void OnDisable()
    {
        Debug.Log("disabled");
        p_AudioManager.Stop("menuMusic");
    }

    // Update is called once per frame
    void Update()
    {
        if (s_SettingsUI.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}

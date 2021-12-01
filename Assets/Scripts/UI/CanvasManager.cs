using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject s_HpBarUI;
    [SerializeField] GameObject s_GameoverScreen;
    [SerializeField] GameObject s_TrapOverScreen;
    [SerializeField] GameObject s_winScreen;
    [SerializeField] GameObject s_BossPatienceValue;
    [SerializeField] GameObject s_Settings;
    [SerializeField] GameObject s_Controls;




    bool p_IsBossAngry;
    private float p_showGameoverScreenTime;
    private AudioManager p_AudioManager;
    private bool isDelayed;
    private bool p_BossActivated;
    private Slider p_HpSlider;

    void Start()
    {
        p_HpSlider = s_HpBarUI.GetComponent<Slider>();
        p_AudioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !FindObjectOfType<SettingsCanvas>().transform.GetChild(0).gameObject.activeSelf)
        {
            Time.timeScale = 0;
            s_Settings.SetActive(!s_Settings.activeSelf);
        }

        if(s_BossPatienceValue.GetComponent<Slider>().value == s_BossPatienceValue.GetComponent<Slider>().maxValue && !p_BossActivated)
        {
            Debug.Log("Activate boss!");
            FindObjectOfType<BossScript>().ActivateBoss();
            p_BossActivated = true;
        }
       // p_IsBossAngry = s_BossPatienceValue.GetComponent<Slider>().maxValue == s_BossPatienceValue.GetComponent<Slider>().value;
        if (p_HpSlider.value <= 0 && !s_GameoverScreen.activeSelf)
        {
            if (!isDelayed)
            {
                isDelayed = true;
              //  p_AudioManager.queueSound("death");
              //  FindObjectOfType<PlayerMovement>().GetComponent<Animator>().SetBool("isDead", true);
            }

            p_showGameoverScreenTime += Time.deltaTime;

            if (p_showGameoverScreenTime >= .5f)
            {
               
                if (p_HpSlider.value <= 0)
                {
                    Time.timeScale = 0;
                    s_GameoverScreen.SetActive(true);
                }
              
             
            }
        }

        if (FindObjectOfType<PlayerMovement>().IsWinGame())
        {
            Time.timeScale = 0;
           s_winScreen.SetActive(true);
        }

        if ((p_HpSlider.value <= 0 || FindObjectOfType<PlayerMovement>().IsWinGame()) && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("DarkDungeon");
        }

    }

    public void ResumeGame()
    {
        Debug.Log("Resuming game");
        s_Settings.SetActive(!s_Settings.activeSelf);
        Time.timeScale = 1;
       // s_Settings.gameObject.SetActive(true);
    }

    public void LeaveGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Home");
    }

    public void OverrideOpenSettings()
    {
        FindObjectOfType<SettingsCanvas>().OpenSetting();
    }

    public void OpenControls()
    {
        s_Controls.gameObject.SetActive(true);
    }

    public void CloseControls()
    {
        s_Controls.gameObject.SetActive(false);
    }
}

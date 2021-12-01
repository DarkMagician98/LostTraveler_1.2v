using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject s_SettingsObj;
    [SerializeField] AudioMixer s_AudioMixer;
    [SerializeField] Slider s_MusicVolumeSlider;
    [SerializeField] Slider s_SoundVolumeSlider;
    private static SettingsCanvas instance;
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if(SceneManager.GetActiveScene().name == "EntranceArea")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                s_SettingsObj.SetActive(!s_SettingsObj.activeSelf);
            }
        }
    }

    public void UpdateMusicValueOnChange()
    {
        s_AudioMixer.SetFloat("Music", s_MusicVolumeSlider.value);
    }

    public void UpdateSoundValueOnChange()
    {
        s_AudioMixer.SetFloat("Sound", s_SoundVolumeSlider.value);
    }

    public void OpenSetting()
    {
        s_SettingsObj.SetActive(true);
    }

    public void LeaveSettings()
    {
        s_SettingsObj.SetActive(false);
    }
}

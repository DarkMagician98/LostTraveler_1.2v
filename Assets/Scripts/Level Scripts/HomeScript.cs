using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToEntrance()
    {
        SceneManager.LoadScene("DarkDungeon");
    }

    public void GoToSettings()
    {

    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}

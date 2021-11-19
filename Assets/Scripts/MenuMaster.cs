using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuMaster : MonoBehaviour
{
    public GameObject mainMenu, loadMenu;
    public List<Text> slotTexts;
    public string saveName;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        for(int i=0;i<slotTexts.Count;i++)
        {
            string path = Application.dataPath + "/Save" + (i + 1).ToString() + ".json";
            if (File.Exists(path))
            {
                slotTexts[i].text = "Save " + (i + 1).ToString();
            }
        }
    }

    public void Play(int level)
    {
        SceneManager.LoadSceneAsync(level);
    }

    public void OpenLoadMenu()
    {
        mainMenu.SetActive(false);
        loadMenu.SetActive(true);
    }

    public void OpenMainMenu()
    {
        loadMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void LoadSave(string save)
    {
        string path = Application.dataPath + save + ".json";
        
        saveName = save;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

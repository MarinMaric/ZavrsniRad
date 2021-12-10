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
    public List<GameObject> deleteButtons;
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
            else
            {
                deleteButtons[i].SetActive(false);
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

    public void DeleteSave(int index)
    {
        string path = Application.dataPath + "/Save" + (index+1) + ".json";
        File.Delete(path);
        deleteButtons[index].SetActive(false);
        RefreshMenu();
    }

    void RefreshMenu()
    {
        foreach (var t in slotTexts)
        {
            var saveName = t.transform.parent.name;
            string path = Application.dataPath + "/" + saveName  + ".json";

            if (!File.Exists(path))
            {
                t.text = "EMPTY";
            }
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}

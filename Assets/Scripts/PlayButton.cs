using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void Play(int level)
    {
        Destroy(FindObjectOfType<MenuMaster>().gameObject);
        SceneManager.LoadSceneAsync(level);
    }
}

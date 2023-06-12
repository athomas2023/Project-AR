using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene_Transition : MonoBehaviour
{
    public string Scene_Name;

    public void Change_scene()
    {
        SceneManager.LoadScene(Scene_Name);
    }
}

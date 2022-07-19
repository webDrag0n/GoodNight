using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOperations : MonoBehaviour
{
    public void LoadScene(string scene_name)
    {
        SceneManager.LoadSceneAsync(scene_name);
    }
}

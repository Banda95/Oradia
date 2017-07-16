using UnityEngine;
using UnityEngine.SceneManagement;
class SceneLoader : MonoBehaviour
{
    public static string LevelName;

    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetLevelName(string name)
    {
        LevelName = name;
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator transition;
    [SerializeField] float transitionTime;

    /// <summary>
    /// Starts a coroutine to load the next level in the build index (Check build settings to know build index)
    /// </summary>
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    /// <summary>
    /// Starts a coroutine to load any level included in build given its name. (Case Sensitive)
    /// </summary>
    /// <param name="levelName">Name of the level that has to be loaded</param>
    public void LoadLevelName(string levelName)
    {
        StartCoroutine(LoadLevelByName(levelName));
    }

    /// <summary>
    /// Coroutine to load the next level. Waits for the level transition to start before loading next level. 
    /// </summary>
    /// <param name="levelIndex">The build index of the next level</param>
    /// <returns></returns>
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    /// <summary>
    /// Coroutine to load a level by name. Waits for the level transition to start before loading level by name
    /// </summary>
    /// <param name="levelName">Name of the level to be loaded</param>
    /// <returns></returns>
    IEnumerator LoadLevelByName(string levelName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }
}

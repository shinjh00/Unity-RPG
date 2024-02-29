using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    [SerializeField] Image fade;

    private BaseScene curScene;

    public BaseScene GetCurScene()
    {
        if (curScene == null)
            curScene = FindObjectOfType<BaseScene>();

        return curScene;
    }

    public T GetCurScene<T>() where T : BaseScene
    {
        if (curScene == null)
            curScene = FindObjectOfType<BaseScene>();

        return curScene as T;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingRoutine(sceneName));
    }

    IEnumerator LoadingRoutine(string sceneName)
    {
        // Fade Out
        float time = 0f;
        while (time < 0.5f)
        {
            time += Time.unscaledDeltaTime;
            fade.color = new Color(0, 0, 0, time * 2);
            yield return null;
        }

        Time.timeScale = 0f;
        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
        while (oper.isDone == false)
        {
            Debug.Log(oper.progress);
            yield return null;
        }

        BaseScene curScene = GetCurScene();
        yield return curScene.LoadingRoutine();
        Time.timeScale = 1f;

        // Fade In
        time = 0.5f;
        while (time > 0f)
        {
            time -= Time.unscaledDeltaTime;
            fade.color = new Color(0, 0, 0, time * 2);
            yield return null;
        }
    }
}

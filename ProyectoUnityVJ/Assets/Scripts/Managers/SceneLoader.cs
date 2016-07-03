using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public SCENES_NUMBER sceneToLoad;
    public Text textLoading;

    void Start()
    {
        StartCoroutine(Load((int)sceneToLoad));
    }

    IEnumerator Load(int scene)
    {
        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            if (ao.progress == 0.9f)
            {
                ao.allowSceneActivation = true;
            }
            textLoading.text = "LOADING\n" + (int)(progress * 100) + "%";
            yield return null;
        }
    }
}
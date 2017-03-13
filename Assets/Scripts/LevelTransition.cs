using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MovementEffects;
using System.Collections.Generic;

public class LevelTransition : MonoBehaviour {
    public string levelToLoad;
    public Text[] loadingText;
    public Text textToDisplay;

    //Load the scene
    private IEnumerator<float> LoadNextLevel(string levelToLoad)
    {
        Timing.WaitForSeconds(1.0f);
        AsyncOperation async = SceneManager.LoadSceneAsync(levelToLoad);
        while (!async.isDone)
        {
            //if we're basically done, press a key to move to the next scene
            if (async.progress >= 0.9f)
            {
                async.allowSceneActivation = true;
            }
            else
            {
                if (loadingText.Length > 0)
                {
                    Timing.RunCoroutine(CycleText(loadingText));
                }
            }
            yield return 0f;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag.Equals("Player"))
        {
            Timing.RunCoroutine(LoadNextLevel(levelToLoad));
        }
    }

    private IEnumerator<float> CycleText(Text[] loadingText)
    {
        foreach(Text t in loadingText)
        {
            textToDisplay = t;
            Timing.WaitForSeconds(1.0f);
        }
        yield return 0f;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour {

    public Animator anim;
    static private ScreenManager _instance;
    private int level;

    static public ScreenManager GetInstance()
    {
        if (_instance == null)
        {
            return new ScreenManager();
        }
        else return _instance;
    }

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);	
	}

    public void TriggerLoadingFadeOut(int screenIndex)
    {
        anim.SetTrigger("LoadingFadeOut");
        level = screenIndex;
    }

    void TriggerLoadingFadeIn()
    {
        anim.SetTrigger("LoadingFadeIn");
    }

    void TriggerIdle()
    {
        anim.SetTrigger("Idle");
    }

    public void TriggerBattleFadeOut()
    {
        anim.SetTrigger("BattleFadeOut");
        level = 2;
    }

    void TriggerBattleFadeIn()
    {
        anim.SetTrigger("BattleFadeIn");
    }

    void OnLoadComplete(string trigger)
    {
        StartCoroutine(LoadAsynchronously(level, trigger));
    }

    IEnumerator LoadAsynchronously(int sceneIndex, string trigger)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);
            yield return null;
        }
        anim.SetTrigger(trigger);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    //显示进度的文本
    public Text progress;
    //进度条的数值
    private float progressValue;
    //进度条
    public Slider slider;
    [Tooltip("下个场景的名字")]
    public string nextSceneName = "Main";

    private AsyncOperation async = null;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LoadScene");
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(nextSceneName);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            //if (async.progress < 0.9f)
            //    progressValue = async.progress;
            //else
            //    progressValue = 1.0f;
            progressValue += Time.deltaTime/3;

            slider.value = progressValue;
            progress.text = "Loading... "+(int)(slider.value * 100) + " %";

            if (progressValue >= 0.98)
            {
                progress.text = "按任意键继续";
                if (Input.anyKeyDown)
                {
                    async.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}

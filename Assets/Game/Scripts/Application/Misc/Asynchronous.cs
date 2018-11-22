using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Asynchronous : View
{
    public Slider loadingSlider;
    public Text loadingText;
    private float loadingSpeed = 1;
    private float targetValue;
    private AsyncOperation operation;
    bool IsStart;
    int index=3;

    public override string Name
    {
        get
        {
            return Consts.V_Asynchronous;
        }
    }
    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_AsynchronousLoading);
    }
    void Start()
    {
        loadingSlider.value = 0.0f;
        StartCoroutine(AsyncLoading());
        IsStart = true;
    }
    IEnumerator AsyncLoading()
    {
        operation = SceneManager.LoadSceneAsync(index);

        //阻止当加载完成自动切换
        
        operation.allowSceneActivation = false;
        yield return operation;
    }
    void Update()
    {
        if (!IsStart)
        {
            return;
        }
        targetValue = operation.progress;
        if (operation.progress >= 0.9f)
        {
            //operation.progress的值最大为0.9
            targetValue = 1.0f;
        }
        if (targetValue != loadingSlider.value)
        {
            //插值运算
            loadingSlider.value = Mathf.Lerp(loadingSlider.value, targetValue, Time.deltaTime * loadingSpeed);

            if (Mathf.Abs(loadingSlider.value - targetValue) < 0.01f)
            {
                loadingSlider.value = targetValue;
            }
        }
        loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";
        if ((int)(loadingSlider.value * 100) == 100)
        {
            //允许异步加载完毕后自动切换场景
            operation.allowSceneActivation = true;
            //发布事件
            IsStart = false;
            Game.Instance.Com();
        }
    }

     
    public override void HandleEvent(string eventName, object data)
    {
        if (eventName==Consts.E_AsynchronousLoading)
        {
            Debug.Log("收到消息");
            SceneArgs args = data as SceneArgs;
        }
    }
}


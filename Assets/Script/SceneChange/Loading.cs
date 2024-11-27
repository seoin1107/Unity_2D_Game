using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Londing : MonoBehaviour
{
    public static int nScene;
    public UnityEngine.UI.Slider mySlider;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadingScene());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void LoadScene(int n)
    {
        nScene = n;
        SceneManager.LoadScene(1);
    }

    IEnumerator LoadingScene()
    {
        WaitForSeconds ws = new WaitForSeconds(1.0f); //new로 만드는건 전부 인스턴스 so 가비지가 생김
        mySlider.value = 0;
        yield return GameTime.Wait(1f);
        AsyncOperation ao = SceneManager.LoadSceneAsync(nScene); //얼마나 로딩됐는지 알 수 있는 함수
        ao.allowSceneActivation = false; //로딩이 끝난다음 대기하도록 하는 함수, true면 바로 이동

        while (mySlider.value < mySlider.maxValue)
        {
            //mySlider.value = ao.progress; //progress 최댓값이 0.9 so Slider최댓값도 낮춰야함
            yield return StartCoroutine(UpdateSlider(ao.progress));
        }
        yield return GameTime.Wait(1f);
        ao.allowSceneActivation = true;
    }

    IEnumerator UpdateSlider(float v)
    {
        while (mySlider.value < v)
        {
            mySlider.value += Time.deltaTime;
            yield return null;
        }
        mySlider.value = v;
    }
}

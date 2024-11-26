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
        WaitForSeconds ws = new WaitForSeconds(1.0f); //new�� ����°� ���� �ν��Ͻ� so �������� ����
        mySlider.value = 0;
        yield return GameTime.Wait(1f);
        AsyncOperation ao = SceneManager.LoadSceneAsync(nScene); //�󸶳� �ε��ƴ��� �� �� �ִ� �Լ�
        ao.allowSceneActivation = false; //�ε��� �������� ����ϵ��� �ϴ� �Լ�, true�� �ٷ� �̵�

        while (mySlider.value < mySlider.maxValue)
        {
            //mySlider.value = ao.progress; //progress �ִ��� 0.9 so Slider�ִ񰪵� �������
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class serviceList : MonoBehaviour
{
    public HorizontalLayoutGroup hlg;
    public service service;

    void OnEnable()
    {
        hlg.spacing = -159f;
    }

    public IEnumerator expand(float current, float target, float duration)
    {
        service.setting();
        yield return new WaitForSecondsRealtime(0.3f);
        float start = 0;

        AudioManager.instance.spread();
        while (start < duration)
        {
            hlg.spacing = Mathf.Lerp(current, target, start / duration);
            start += Time.deltaTime;
            yield return null;
        }

        hlg.spacing = target;
    }

    public IEnumerator collect(float current, float target, float duration)
    {
        service.back();
        yield return new WaitForSecondsRealtime(0.3f);
        float start = 0;

        while (start < duration)
        {
            hlg.spacing = Mathf.Lerp(current, target, start / duration);
            start += Time.deltaTime;
            yield return null;
        }

        hlg.spacing = target;
    }

    public void back()
    {
        service.back();
    }
}

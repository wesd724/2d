using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeDeckList : MonoBehaviour
{
    public HorizontalLayoutGroup hlg;
    public shopDeck shopdeck;

    void OnEnable()
    {
        hlg.spacing = -100f;
    }

    public IEnumerator expand(float current, float target, float duration)
    {
        shopdeck.setting();
        yield return new WaitForSecondsRealtime(0.15f);
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
        shopdeck.back();
        yield return new WaitForSecondsRealtime(0.15f);
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
        shopdeck.back();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {

    public Image fadingImage;
    public bool isFading = false;
    public bool finishedFading = false;
    public float timeToFadeOut = 2f;
    private float a = 0;

    public delegate void OnComplete();

    private void Start()
    {
        fadingImage.color = new Color(fadingImage.color.r, fadingImage.color.b, fadingImage.color.g, a);
    }

    private void Update()
    {
        if (isFading)
        {
            a += Time.deltaTime/timeToFadeOut;
            if (a > 1)
            {
                a = 1;
                finishedFading = true;
            };

            fadingImage.color = new Color(fadingImage.color.r, fadingImage.color.b, fadingImage.color.g, a);
        }
    }

    public void FadeOutNow(OnComplete onComplete)
    {
        StartCoroutine(StartFadingCoroutine(onComplete));
    }

    IEnumerator StartFadingCoroutine(OnComplete onComplete)
    {
        a = 0f;

        while (a <= 1)
        {
            a += Time.deltaTime / timeToFadeOut;
            fadingImage.color = new Color(fadingImage.color.r, fadingImage.color.b, fadingImage.color.g, a);

            yield return null;
        }

        onComplete.Invoke();
    }

}

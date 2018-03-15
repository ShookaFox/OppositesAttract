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

    private void Start()
    {
        
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

}

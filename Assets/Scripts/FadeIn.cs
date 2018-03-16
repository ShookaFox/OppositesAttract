using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    public Image fadingImage;
    public bool isFading = true;
    public bool finishedFading = false;
    public float timeToFadeOut = 2f;
    private float a = 1;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (isFading)
        {
            a -= Time.deltaTime/timeToFadeOut;
            if (a < 0)
            {
                a = 0;
                finishedFading = true;
            };

            fadingImage.color = new Color(fadingImage.color.r, fadingImage.color.b, fadingImage.color.g, a);
        }
    }

}

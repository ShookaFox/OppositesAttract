using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour {

    public Image fadingImage1;
    public Image fadingImage2;

    public GameObject slide2;

    private void Start()
    {
        AudioManager.instance.playMusic();
        StartCoroutine(DoTheThing());
    }

    public IEnumerator DoTheThing()
    {
        float a = 1f;
        while (a > 0)
        {
            a -= Time.deltaTime / 2f;
            if (a < 0)
            {
                a = 0;
            };

            fadingImage1.color = new Color(fadingImage1.color.r, fadingImage1.color.b, fadingImage1.color.g, a);

            yield return null;
        }
        float t = 2f;
        while (t > 0)
        {
            t -= Time.deltaTime;
            yield return null;
        }
        while (a < 1)
        {
            a += Time.deltaTime / 2f;
            if (a > 1)
            {
                a = 1;
            };

            fadingImage1.color = new Color(fadingImage1.color.r, fadingImage1.color.b, fadingImage1.color.g, a);

            yield return null;
        }
        a = 1f;
        fadingImage2.color = new Color(fadingImage2.color.r, fadingImage2.color.b, fadingImage2.color.g, a);
        slide2.SetActive(true);
        while (a > 0)
        {
            a -= Time.deltaTime / 2f;
            if (a < 0)
            {
                a = 0;
            };

            fadingImage2.color = new Color(fadingImage2.color.r, fadingImage2.color.b, fadingImage2.color.g, a);

            yield return null;
        }
        t = 2f;
        while (t > 0)
        {
            t -= Time.deltaTime;
            yield return null;
        }
        while (a < 1)
        {
            a += Time.deltaTime / 2f;
            if (a > 1)
            {
                a = 1;
            };

            fadingImage2.color = new Color(fadingImage2.color.r, fadingImage2.color.b, fadingImage2.color.g, a);

            yield return null;
        }
        SceneManager.LoadScene(1);
    }
}

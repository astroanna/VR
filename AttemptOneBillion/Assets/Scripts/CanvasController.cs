using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CanvasController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        foreach (Transform child in this.transform.GetChild(0))
        {
            if (child.GetComponent<Image>())
            {
                child.GetComponent<Image>().canvasRenderer.SetAlpha(0.5f);
            }
            if (child.GetComponent<Text>())
            {
                child.GetComponent<Text>().canvasRenderer.SetAlpha(1.0f);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            toggleFade(this.transform.GetChild(0));
        }
    }

    void toggleFade (Transform c)
    {
        foreach (Transform child in c)
        {
            if (child.GetComponent<Image>())
            {
                Image image = child.GetComponent<Image>();
                if (image.canvasRenderer.GetAlpha() == 0.5f)
                {
                    image.CrossFadeAlpha(0.0f, 0.5f, false);
                }
                else
                {
                    image.CrossFadeAlpha(0.5f, 0.5f, false);
                }
            }
            if (child.GetComponent<Text>())
            {
                Text text = child.GetComponent<Text>();
                if (text.canvasRenderer.GetAlpha() == 1.0f)
                {
                    text.CrossFadeAlpha(0.0f, 0.5f, false);
                }
                else
                {
                    text.CrossFadeAlpha(1.0f, 0.5f, false);
                }
            }
        }
    }

}

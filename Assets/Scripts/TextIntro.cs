using System;
using UnityEngine;
using UnityEngine.UI;

public class TextIntro : MonoBehaviour
{
    public float Duration;
    public GameObject TextParent;
    public String nextString = "";

    private float _myTime;

    public void Update()
    {
        _myTime += Time.deltaTime;
        if (_myTime> Duration)
        {
            if (nextString != "")
            {
                foreach (Transform child in TextParent.transform)
                {
                    if (child.gameObject.name.Equals(nextString))
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }
            Destroy(gameObject);
        }

        Color myColor = GetComponent<Text>().color;
        
        float ratio = _myTime/ (Duration / 2f) - (_myTime> Duration / 2f ? 1 : 0);

        if (_myTime< Duration / 2f)
        {
            // We need to be fading in
            myColor.a = Mathf.Lerp(0f, 1f, ratio);
        }
        else
        {
            myColor.a = Mathf.Lerp(1f, 0f, ratio);
        }

        GetComponent<Text>().color = myColor;
    }
}
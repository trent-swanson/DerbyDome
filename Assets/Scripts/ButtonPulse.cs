using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonPulse : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    [Space]
    public Vector2 startSize;
    public Vector2 fullSize;
    [Space]
    public float animationSpeedScaleExpand;
    public float animationSpeedScaleContract;

    private float timer = 0.0f;
    private bool lerping = true;


    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == button1)
        {
            Resize(button2);
            animateButton(button1);
        }
        else
        {
            Resize(button1);
            animateButton(button2);
        }
    }

    void animateButton(GameObject buttonToAnimate)
    {
        RectTransform rectangle = buttonToAnimate.GetComponent<RectTransform>();
        if (lerping)
        {
            //Timer that counts to the time in which the color must change directions
            timer += Time.unscaledDeltaTime * animationSpeedScaleExpand; //* scale

            //Changes the colors so that the lerp changes direction
            if (timer >= 1.0f)
            {
                //Resets the timer to count back to the desired time
                timer = 1.0f;
                lerping = false;
            }
        }

        else
        {
            timer -= Time.unscaledDeltaTime * animationSpeedScaleContract;
            if (timer <= 0.0f)
            {
                //Resets the timer to count back to the desired time
                timer = 0.0f;
                lerping = true;
            }
        }
        rectangle.sizeDelta = Vector2.Lerp(startSize, fullSize, timer);
    }

    void Resize(GameObject button)
    {
        RectTransform rectangle = button.GetComponent<RectTransform>();
        rectangle.sizeDelta = startSize;
    }
}

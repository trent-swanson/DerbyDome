//================================================================================
//ButtonPulseMain
//
//Purpose: Animates all menu buttons so that they pulsate on a loop from small
//to big while the player remains in the menu
//
//Creator: Joel Goodchild
//================================================================================

using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPulseMain : MonoBehaviour
{
    //Any button that needs to pulsate can be put into this gameobject to be animated
    public GameObject button1;
    //Any button that needs to pulsate can be put into this gameobject to be animated
    public GameObject button2;
    //Any button that needs to pulsate can be put into this gameobject to be animated
    public GameObject button3;
    //Any button that needs to pulsate can be put into this gameobject to be animated
    public GameObject button4;

    [Space]

    //Allows to determine the starting, or minimum size of the buttons
    public Vector2 startSize;
    //Allows to determine the fianl, or maximum size of the buttons
    public Vector2 fullSize;

    [Space]

    //Determines how fast the button expands to its maximum size
    public float animationSpeedScaleExpand;
    //Determines how fast the button contracts back to its minimum size
    public float animationSpeedScaleContract;

    //Times how long the animation plays for and reverses the animation once it completes the loop
    private float timer = 0.0f;
    //Bool to flip the direction of the animation, from expanding to contracting
    private bool lerping = true;


    void Update()
    {
        //If button1 is the selected button, it gets animated and the others are resized to the minimum size of
        //at the beginning of the animation
        if (EventSystem.current.currentSelectedGameObject == button1)
        {
            Resize(button2);
            Resize(button3);
            Resize(button4);
            animateButton(button1);
        }

        //If button2 is the selected button, it gets animated and the others are resized to the minimum size of
        //at the beginning of the animation
        else if (EventSystem.current.currentSelectedGameObject == button2)
        {
            Resize(button1);
            Resize(button3);
            Resize(button4);
            animateButton(button2);
        }

        //If button3 is the selected button, it gets animated and the others are resized to the minimum size of
        //at the beginning of the animation
        else if (EventSystem.current.currentSelectedGameObject == button3)
        {
            Resize(button1);
            Resize(button2);
            Resize(button4);
            animateButton(button3);
        }

        //If button3 is the selected button, it gets animated and the others are resized to the minimum size of
        //at the beginning of the animation
        else
        {
            Resize(button1);
            Resize(button2);
            Resize(button3);
            animateButton(button4);
        }
    }

    void animateButton(GameObject buttonToAnimate)
    {
        //Gets the transform of the button to allow it to be altered
        RectTransform rectangle = buttonToAnimate.GetComponent<RectTransform>();

        //If lerping is true, the button is expanding
        if (lerping)
        {
            //Timer that determines how long the animation has been playing for
            //and determines when to switch its direction
            timer += Time.unscaledDeltaTime * animationSpeedScaleExpand;

            //If the animation is complete, as identified by a timer value of 1
            //the animation flips directions and begins to contract
            if (timer >= 1.0f)
            {
                //Resets the timer to count back to the desired time
                timer = 1.0f;
                //Once the timer reaches 1.0f, lerping is set to false to reverse the animation to contract
                lerping = false;
            }
        }

        //If lerping is false, the button is contracting
        else
        {
            //Timer that determines how long the animation has been playing for
            //and determines when to switch its direction
            timer -= Time.unscaledDeltaTime * animationSpeedScaleContract;

            //If the animation is complete, as identified by a timer value of 0
            //the animation flips directions and begins to expand
            if (timer <= 0.0f)
            {
                //Resets the timer to count back to the desired time
                timer = 0.0f;
                lerping = true;
            }
        }
        //Sets the scale for the button
        rectangle.sizeDelta = Vector2.Lerp(startSize, fullSize, timer);
    }

    //Enables the button to be resized back to its default size when it becomes deselected
    void Resize(GameObject button)
    {
        //Gets the transform of the button to allow it to be altered
        RectTransform rectangle = button.GetComponent<RectTransform>();
        //Returns it back to its starting size
        rectangle.sizeDelta = startSize;
    }
}

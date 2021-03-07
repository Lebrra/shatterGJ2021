using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Knob : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool hover = false;

    public int value = 1;
    int startValue;

    public bool clicked = false;
    Vector2 clickedPos;

    float initialPos;
    Vector2 dragWidth;

    private void Start()
    {
        initialPos = transform.position.x;
        if (transform.position.x < Screen.width / 2F) dragWidth = new Vector2(0, initialPos * 2F);
        else dragWidth = new Vector2(initialPos - (Screen.width - initialPos), Screen.width);

        Debug.Log(dragWidth);

        TurnKnob(value);
    }

    void Update()
    {
        if (hover && !clicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clicked = true;
                clickedPos = Input.mousePosition;
                startValue = Mathf.RoundToInt(value);
            }
        }

        if(clicked)
        {
            //rotate and fun things
            //Debug.Log("difference: " + (Input.mousePosition.x - clickedPos.x));
            int currentValue = currentDragValue(startValue);
            TurnKnob(currentValue);


            if (Input.GetMouseButtonUp(0))
            {
                //Debug.Log("initial pos: " + clickedPos);
                //Debug.Log("final pos: " + Input.mousePosition.x);

                //do things with final knob value
                //Debug.Log("knob value set to " + currentValue);
                value = currentValue;

                clicked = false;
            }
        }
    }

    void TurnKnob(int turnValue)
    {
        int rotationValue;
        switch (turnValue)
        {
            case 0:
                rotationValue = 60;
                break;
            case 1:
                rotationValue = 0;
                break;
            case 2:
                rotationValue = -60;
                break;
            default: return;
        }

        Quaternion thing = new Quaternion();
        thing.eulerAngles = new Vector3(0, 0, rotationValue);
        transform.rotation = thing;
    }

    public int currentDragValue(int prevValue)
    {
        float leftBound, rightBound;

        switch (prevValue)
        {
            case 0:         //knob started to the left
                leftBound = dragWidth.x + (dragWidth.y - dragWidth.x) / 2F + ((dragWidth.y - dragWidth.x) * 0.1F);
                rightBound = leftBound + (dragWidth.y - dragWidth.x) * 0.2F;

                break;

            case 1:         //knob started in the middle
                leftBound = initialPos - ((dragWidth.y - dragWidth.x) * 0.1F);
                rightBound = initialPos + ((dragWidth.y - dragWidth.x) * 0.1F);

                break;

            case 2:         //knob started to the right\
                rightBound = dragWidth.x + (dragWidth.y - dragWidth.x) / 2F - ((dragWidth.y - dragWidth.x) * 0.1F);
                leftBound = rightBound - (dragWidth.y - dragWidth.x) * 0.2F;

                break;

            default: return -1;
        }

        float mousePos = Input.mousePosition.x;

        //Debug.Log("left bound: " + leftBound);
        //Debug.Log("right bound: " + rightBound);

        if (mousePos < leftBound) return 0;
        else if (mousePos > rightBound) return 2;
        else return 1;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
    }
}

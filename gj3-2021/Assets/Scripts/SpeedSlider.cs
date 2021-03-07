using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpeedSlider : MonoBehaviour, IPointerUpHandler
{
    public bool interactable = true;

    public CharacterMovement character;

    public Sprite[] handleSprites;
    public Image handle;

    Slider slider;
    int prevValue;

    public GameObject speedGadge;
    bool speedMoving = false;
    float finalDirection;
    float adder;

    void Start()
    {
        slider = GetComponent<Slider>();

        slider.value = (float)AudioManager.inst?.currSong;
        updateSprite(slider.value);
        ChangeSpeed((int)slider.value);

        Quaternion thing = new Quaternion();
        thing.eulerAngles = new Vector3(0, 0, getDirection((int)slider.value));
        speedGadge.transform.rotation = thing;

        prevValue = (int)slider.value;
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0)) Debug.Log(Input.mousePosition);

        if (speedMoving) 
        {
            if (finalDirection < adder) adder -= Time.deltaTime * 200F;
            else adder += Time.deltaTime * 200F;

            Quaternion thing = new Quaternion();
            thing.eulerAngles = new Vector3(0, 0, adder);
            speedGadge.transform.rotation = thing;
            //speedGadge.Rotate(new Vector3(0, 0, adder));
        }
    }

    public void updateSprite(float value)
    {
        handle.sprite = handleSprites[(int)value];
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (interactable)
        {
            slider.interactable = interactable = false;
            speedMoving = true;
            StartCoroutine(EnableSlider());
        }
    }

    IEnumerator EnableSlider()
    {
        ChangeSong((int)slider.value);
        adder = getDirection(prevValue);
        finalDirection = getDirection((int)slider.value);
        yield return new WaitForSeconds(1.1F);

        speedMoving = false;

        Quaternion thing = new Quaternion();
        thing.eulerAngles = new Vector3(0, 0, finalDirection);
        speedGadge.transform.rotation = thing;

        prevValue = (int)slider.value;
        slider.interactable = interactable = true;
    }

    float getDirection(int index)
    {
        //adder = speedGadge.transform.rotation.eulerAngles.z;
        switch (index)  //up = 329, left = 400, right = 255
        {
            case 0:
                return 400F;
            case 1:
                return 329F;
            case 2:
                return 255F;
            default: return -1;
        }

        //switch (index)  //up = 329, left = 400, right = 255
        //{
        //    case 0:
        //        finalDirection = 40F;
        //        subtract = false;
        //        break;
        //    case 1:
        //        finalDirection = 329F;
        //        if (prevValue == 0) subtract = true;
        //        else subtract = false;
        //        break;
        //    case 2:
        //        finalDirection = 255F;
        //        subtract = true;
        //        break;
        //    default: return;
        //}
    }

    void ChangeSong(int index)
    {
        switch (index)
        {
            case 0:
                AudioManager.inst?.PlaySlowSong();
                Debug.Log("playing slow song");
               
                break;
            case 1:
                AudioManager.inst?.PlayNormalSong();
                Debug.Log("playing normal song");
                
                break;
            case 2:
                AudioManager.inst?.PlayFastSong();
                Debug.Log("playing fast song");
                
                break;
            default: return;
        }

        ChangeSpeed(index);
    }

    void ChangeSpeed(int index)
    {
        switch (index)
        {
            case 0:
                character.moveSpeed = 0.7F;
                character.GetComponent<Animator>().speed = 1.05F;
                break;

            case 1:
                character.moveSpeed = 1.2F;
                character.GetComponent<Animator>().speed = 1.2F;
                break;

            case 2:
                character.moveSpeed = 2F;
                character.GetComponent<Animator>().speed = 1.8F;
                break;

            default: return;
        }
    }
}

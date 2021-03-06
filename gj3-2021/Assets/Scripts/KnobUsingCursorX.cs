using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class KnobUsingCursorX : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 cursorStart;
    private Vector3 eulerAnglesStart;
    private float directionMultiplier;
    [SerializeField] private float[] snaps = new float[] { -60, 0, 60 };

    public void OnBeginDrag(PointerEventData eventData)
    {
        cursorStart = Input.mousePosition;
        eulerAnglesStart = transform.eulerAngles;
        bool beganDragAboveKnobCenter = (cursorStart.y - transform.position.y) > 0;
        directionMultiplier = beganDragAboveKnobCenter ? -1 : 1;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 delta = Input.mousePosition - cursorStart;
        transform.eulerAngles = eulerAnglesStart + new Vector3(0, 0, directionMultiplier * delta.x);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int nearestIndex = 0;
        float minimumAngle = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, snaps[0]));

        for(int i = 1; i < snaps.Length; i++)
        {
            float angle = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, snaps[i]));
            if(angle < minimumAngle)
            {
                nearestIndex = i;
                minimumAngle = angle;
            }
        }

        StartCoroutine(RotateToAngle(snaps[nearestIndex]));
    }

    private IEnumerator RotateToAngle(float target)
    {
        float deltaAngle = Mathf.DeltaAngle(transform.eulerAngles.z, target);
        while(Mathf.Abs(deltaAngle) > 2)
        {
            transform.eulerAngles += new Vector3(0, 0, 2 * Mathf.Sign(deltaAngle));
            yield return null;
            deltaAngle = Mathf.DeltaAngle(transform.eulerAngles.z, target);
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, target);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class KnobUsingCursorAngle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private float offsetAngle;
    [SerializeField] private float[] snaps = new float[] { -60, 0, 60 };

    public float CursorAngle()
    {
        Vector3 knobCenterToCursor = Input.mousePosition - transform.position;
        return Mathf.Atan2(knobCenterToCursor.y, knobCenterToCursor.x) * Mathf.Rad2Deg;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offsetAngle = CursorAngle() - transform.eulerAngles.z;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, CursorAngle() - offsetAngle);
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

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}

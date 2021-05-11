using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(WorldChanger))]
public class KnobUsingCursorAngle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private float offsetAngle;
    private Image image;
    private WorldChanger worldChanger;
    [SerializeField] private Material phaseMaterial = null;
    [SerializeField] private string materialPropertyName = "Phase";
    [SerializeField] private Animator lineAnimator = null;
    [SerializeField] private Color disabledKnobColor = new Color32(150, 150, 150, 255);
    [SerializeField] private int currentWorld = 2;
    [SerializeField] private float[] snapAngles = new float[] { 60, 0, -60 };
    [SerializeField] private Color[] worldColors = new Color[] { Color.yellow, Color.cyan, Color.magenta };

    void Start()
    {
        image = GetComponent<Image>();
        worldChanger = GetComponent<WorldChanger>();
        Initialize();
    }

    private void Initialize()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, snapAngles[currentWorld]);
        lineAnimator.SetTrigger(currentWorld.ToString());
        phaseMaterial.SetColor(materialPropertyName, worldColors[currentWorld]);
        worldChanger.setWorld(currentWorld);
    }

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
        int nearestIndex = snapAngles.MinIndex(angle => Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, angle)));
        StartCoroutine(RotateToAngle(snapAngles[nearestIndex]));
        if(nearestIndex != currentWorld)
        {
            currentWorld = nearestIndex;
            StartCoroutine(PhaseShift(currentWorld));
        }
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

    private IEnumerator PhaseShift(int world)
    {
        enabled = false;
        image.color = disabledKnobColor;
        AudioManager.inst.PlayStatic(world);
        lineAnimator.SetTrigger(world.ToString());
        Color start = phaseMaterial.GetColor(materialPropertyName);
        Color end = worldColors[world];

        yield return LerpPhaseColor(start, end, 0, 0.5f);
        worldChanger.setWorld(world);
        image.color = Color.white;
        enabled = true;

        yield return LerpPhaseColor(start, end, 0.5f, 1f);
    }

    private IEnumerator LerpPhaseColor(Color start, Color end, float tStart, float tEnd)
    {
        for(float t = tStart; t < tEnd; t += Time.deltaTime)
        {
            phaseMaterial.SetColor(materialPropertyName, Color.Lerp(start, end, t));
            yield return null;
        }

        phaseMaterial.SetColor(materialPropertyName, Color.Lerp(start, end, tEnd));
    }
}

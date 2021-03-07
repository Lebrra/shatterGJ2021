using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSound : MonoBehaviour
{
    public static PlayerSound inst;

    [SerializeField] private AudioClip[] hits = null;
    [SerializeField] private AudioClip[] shocks = null;
    [SerializeField] private AudioClip[] falls = null;
    private AudioSource audioSource;

    private void Awake()
    {
        if (inst != null)
        {
            Destroy(this);
        }
        else
            inst = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHit()
    {
        audioSource.PlayOneShot(hits[Random.Range(0, hits.Length)]);
    }

    public void PlayShock()
    {
        audioSource.PlayOneShot(shocks[Random.Range(0, shocks.Length)]);
    }

    public void PlayFall()
    {
        audioSource.PlayOneShot(falls[Random.Range(0, falls.Length)]);
    }
}

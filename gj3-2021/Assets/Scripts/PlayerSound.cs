using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] hits = null;
    [SerializeField] private AudioClip[] shocks = null;
    [SerializeField] private AudioClip[] falls = null;
    private AudioSource audioSource;

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
        audioSource.PlayOneShot(shocks[Random.Range(0, falls.Length)]);
    }
}

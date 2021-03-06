using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> songList;
    private AudioSource aSource;
    private float startVol;

    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        startVol = aSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(FadeSongOut(1f, 0, 0));
    }

    public IEnumerator FadeSongOut(float duration, float targetVolume, int song)
    {
        float currentTime = 0;
        float start = aSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            aSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        StartCoroutine(FadeSongIn(1f, start, song));
        yield break;
    }

    public IEnumerator FadeSongIn(float duration, float targetVolume, int song)
    {
        float currentTime = 0;
        float start = aSource.volume;
        aSource.clip = songList[song];

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            aSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public void PlaySlowSong()
    {

    }
}

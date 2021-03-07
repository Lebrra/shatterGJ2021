using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager inst;

    public List<AudioClip> songList;
    public List<AudioClip> staticList;
    public AudioSource songSource;
    public AudioSource sfxSource;
    private float startVol;
    public int currSong;

    private void Awake()
    {
        if (inst != null)
        {
            Destroy(this);
        }
        else
            inst = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        inst = this;
        //aSource = GetComponent<AudioSource>();
        startVol = songSource.volume;
        songSource.clip = songList[1];
        songSource.Play();
    }

    public IEnumerator FadeSongOut(float duration, float targetVolume, int song)
    {
        float currentTime = 0;
        float start = songSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            songSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        StartCoroutine(FadeSongIn(1f, start, song));
        yield break;
    }

    public IEnumerator FadeSongIn(float duration, float targetVolume, int song)
    {
        float currentTime = 0;
        float start = songSource.volume;
        songSource.clip = songList[song];
        songSource.Play();

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            songSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public void PlaySlowSong()
    {
        StartCoroutine(FadeSongOut(1f, 0, 0));
        currSong = 0;
    }

    public void PlayNormalSong()
    {
        StartCoroutine(FadeSongOut(1f, 0, 1));
        currSong = 1;
    }

    public void PlayFastSong()
    {
        StartCoroutine(FadeSongOut(1f, 0, 2));
        currSong = 2;
    }

    public void PlayStatic(int num)
    {
        switch (num)
        {
            case 0:
                sfxSource.clip = staticList[0];
                sfxSource.Play();
                return;
            case 1:
                sfxSource.clip = staticList[1];
                sfxSource.Play();
                return;
            case 2:
                sfxSource.clip = staticList[2];
                sfxSource.Play();
                return;
            default:
                return;
        }
    }
}

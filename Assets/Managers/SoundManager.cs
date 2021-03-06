using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public class Clip
{
    public string Name;
    public AudioClip clip;
}
public class SoundManager : Singleton<SoundManager>
{
    public AudioSource audioSource;
    public List<Clip> clips;
    public Slider BGMBar;
    public Slider SFXBar;
    public bool On = true;
    public bool SFXOn = true;

    float SEvolume = 1;
    protected SoundManager() { }
    protected virtual void Awake()
    {
        if (!isNotSingle)
        {
            SoundManager[] objects = FindObjectsOfType<SoundManager>();
            foreach (SoundManager obj in objects)
            {
                if (obj.gameObject != this.gameObject)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    public void Playbgm(string name)
    //사용법 Sound.Instance.ChangeClip("이름",루프 할껀지안할껀지(bool))
    {
        Clip find = clips.Find((o) => { return o.Name == name; });
        if (find != null)
        {
            audioSource.Stop();
            audioSource.clip = find.clip;
            audioSource.loop = true;
            audioSource.Play();

        }
    }

    public void PlaySound(string _clip)
    {
        Clip find = clips.Find((o) => { return o.Name == _clip; });
        if (find != null)
        {
            GameObject audio_object = new GameObject();
            AudioSource object_source = audio_object.AddComponent<AudioSource>();
            object_source.volume = SEvolume;
            object_source.clip = find.clip;
            object_source.loop = false;
            object_source.Play();

            Destroy(audio_object, find.clip.length);
        }
    }
    public void PlaySound(AudioClip _clip)
    {
        GameObject audio_object = new GameObject();
        AudioSource object_source = audio_object.AddComponent<AudioSource>();
        object_source.volume = SEvolume;
        object_source.clip = _clip;
        object_source.loop = false;
        object_source.Play();

        Destroy(audio_object, _clip.length);
    }

    public void MusicChange()
    {
        SetMusicVolume(BGMBar.value);
    }

    public void SoundChange()
    {
        SetSEVolume(BGMBar.value);
    }

    public void SetMusicVolume(float volume)
    {
        audioSource.volume = volume;
    }
    public void SetSEVolume(float volume)
    {
        SEvolume = volume;
    }
}

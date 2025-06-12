using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoSingleton<AudioManager>
{
    private Dictionary<string , AudioClip> clips = new Dictionary<string , AudioClip>();
    [SerializeField] private List<AudioClip> clipList;
    public AudioMixer audioMixer;
    [SerializeField] private GameObject sfxSourcePrefab;
    [SerializeField] private GameObject HitSourcePrefab;
    private List<AudioSource> sfxPool = new List<AudioSource>();
    private List<AudioSource> HitPool = new List<AudioSource>();
    
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    protected override void Awake()
    {
        base.Awake();
        clipList.ForEach(Clip => clips.Add(Clip.name,Clip));
    }

    public void PlayBGM(string clipName)
    {
        AudioClip clip = clips.GetValueOrDefault(clipName);
        bgmSource.clip = clip;
        
        bgmSource.Play();
    }

    public void PlaySFX(string clipName,float volume = 1f)
    {
        AudioClip clip = clips.GetValueOrDefault(clipName);
        
        AudioSource source = GetAvailableSFXSource();
        source.spatialBlend = 0f; 
        source.clip = clip;
        source.PlayOneShot(clip,volume);
    }


    public void PlayHitSFX(string clipName, float volume = 1f)
    {
        AudioClip clip = clips.GetValueOrDefault(clipName);
        if (clip == null) return;
        
        foreach (var src in HitPool)
        {
            if (src.isPlaying && src.clip == clip)
                return; 
        }

        AudioSource source = GetAvailableHitSource();

        source.spatialBlend = 0f;
        source.clip = clip;
        source.PlayOneShot(clip, volume);
    }
    
    private AudioSource GetAvailableSFXSource()
    {
        foreach (var src in sfxPool)
        {
            if (!src.isPlaying)
                return src;
        }
        
        var obj = Instantiate(sfxSourcePrefab, transform);
        var newSrc = obj.GetComponent<AudioSource>();
        sfxPool.Add(newSrc);
        return newSrc;
    }
    
    private AudioSource GetAvailableHitSource()
    {
        foreach (var src in HitPool)
        {
            if (!src.isPlaying)
                return src;
        }
        
        var obj = Instantiate(HitSourcePrefab, transform);
        var newSrc = obj.GetComponent<AudioSource>();
        HitPool.Add(newSrc);
        return newSrc;
    }
        

    public void StopBGM()
    {
        bgmSource.Stop();
    }
}

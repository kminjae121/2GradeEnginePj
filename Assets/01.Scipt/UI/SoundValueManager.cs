using UnityEngine;
using UnityEngine.Audio;

public class SoundValueManager : MonoSingleton<SoundValueManager>
{
    [SerializeField] private AudioMixer _thisGroup;
    
    public void BackGround(float volume)
    {
        SetVolume("BGM", volume);
    }

    public void VFX(float volume)
    {
        SetVolume("SFX", volume);
        SetVolume("Hit", volume);
    }

    public void Master(float volume)
    {
        SetVolume("Master", volume);
    }

    private void SetVolume(string exposedParam, float volume)
    {
        _thisGroup.SetFloat(exposedParam, volume);
    }
}

using UnityEngine;

public class PlayParticleVfx : MonoBehaviour, IPlayableVfx
{
    [SerializeField] private bool isOnPostion;
    [SerializeField] private ParticleSystem particle;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(VfxName) == false)
            gameObject.name = VfxName;
    }

    [field: SerializeField] public string VfxName { get; private set; }


    public void PlayVfx(Vector3 position, Quaternion rotation)
    {
        if (isOnPostion == false)
            transform.SetPositionAndRotation(position, rotation);

        particle.Play(true);
    }

    public void StopVfx()
    {
        particle.Stop(true);
    }
}
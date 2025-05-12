using UnityEngine;

namespace _01.Scipt.Effect
{
    public interface IPlayableVFX
    {
        public string VfxName { get; }
        public void PlayVfx(Vector3 position, Quaternion rotation);
        public void StopVfx();
    }
}
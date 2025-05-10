
using System.Collections.Generic;
using System.Linq;
using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

public class EntityVFX : MonoBehaviour, IEntityComponet
{
    private Dictionary<string, IPlayableVfx> _playableDictionary;
    private Entity _entity;

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _playableDictionary = new Dictionary<string, IPlayableVfx>();
        GetComponentsInChildren<IPlayableVfx>().ToList()
            .ForEach(playable => _playableDictionary.Add(playable.VfxName, playable));
    }

    public void PlayVfx(string vfxName, Vector3 position, Quaternion rotation)
    {
        IPlayableVfx vfx = _playableDictionary.GetValueOrDefault(vfxName);
        Debug.Assert(vfx != default(IPlayableVfx), $"{vfxName} is not exist in dictionary");

        vfx.PlayVfx(position, rotation);
    }

    public void StopVfx(string vfxName)
    {
        IPlayableVfx vfx = _playableDictionary.GetValueOrDefault(vfxName);
        Debug.Assert(vfx != default(IPlayableVfx), $"{vfxName} is not exist in dictionary");
        vfx.StopVfx();
    }
}

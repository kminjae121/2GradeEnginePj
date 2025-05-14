using Member.Kmj._01.Scipt.Entity.AttackCompo;
using UnityEngine;

namespace Blade.Entities
{
    [CreateAssetMenu(fileName = "EntityFinder", menuName = "SO/EntityFinder", order = 0)]
    public class EntityFinderSO : ScriptableObject
    {
        public Entity Target { get; private set; }

        public void SetTarget(Entity target)
        {
            Target = target;
        }
    }
}
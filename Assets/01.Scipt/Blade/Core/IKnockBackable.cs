

using System.Numerics;

namespace Blade.Core
{
    public interface IKnockBackable
    {
        public void KnockBack(Vector3 force, float duration);        
    }
}
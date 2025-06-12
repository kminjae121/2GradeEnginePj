using Unity.Behavior;

namespace Blade.Enemies
{
    [BlackboardEnum]
    public enum EnemyState
    {
        IDLE = 1, PATROL = 2, CHASE = 3, ATTACK = 4, HIT = 5, DEAD = 6, AIRBORN = 7, STUN = 8, JUMPANDSTUN = 9,STOP = 10
        ,OnAIR = 11, Skill = 12,
    }
}
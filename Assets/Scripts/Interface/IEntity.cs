using Assets.Scripts;
using UnityEngine;

public interface IEntity : IDamageable, IUpdatable
{
    int MaxHealth { get; }
    int CurrentHealth { get; }
    int Damage { get; }
    float MoveSpeed { get; }
    bool IsAlive { get; }
}

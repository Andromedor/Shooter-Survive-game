using UnityEngine;

public interface IEntity
{
    int MaxHealth { get; set; }
    int CurrentHealth { get; set; }
    int Damage { get; set; }
    float MoveSpeed { get; set; }
}

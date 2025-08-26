using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public static class TargetRegistry 
{
    private static readonly List<IEntity> _enemies = new();
    public static IEntity Player { get; private set; }


    public static void RegisterPlayer(IEntity player) => Player = player;
    public static void UnregisterPlayer(IEntity player) { if (Player == player) Player = null; }


    public static void RegisterEnemy(IEntity enemy)
    {
        if (!_enemies.Contains(enemy)) _enemies.Add(enemy);
    }


    public static void UnregisterEnemy(IEntity enemy)
    {
        _enemies.Remove(enemy);
    }


    public static IReadOnlyList<IEntity> GetEnemies() => _enemies;

}


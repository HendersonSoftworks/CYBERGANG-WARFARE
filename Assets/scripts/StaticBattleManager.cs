using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To be used if doing action-oriented combat
/// </summary>
public static class StaticBattleManager
{
    public static PlayerArmy playerArmy;
    public static EnemyArmy enemyArmy;

    // Player units
    public static int playerPunks;
    public static int playerMercs;
    public static int playerHackers;
    public static int playerCyborgs;

    // Enemy Units
    public static int enemyPunks;
    public static int enemyMercs;
    public static int enemyHackers;
    public static int enemyCyborgs;
}

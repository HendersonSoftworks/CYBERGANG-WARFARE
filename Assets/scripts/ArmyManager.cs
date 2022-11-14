using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for PlayerArmy and EnemyArmy.
/// </summary>
public class ArmyManager : MonoBehaviour
{
    public int totalTroops;
    public int punks;
    public int mercs;
    public int hackers;
    public int cyborgs;

    /// <summary>
    /// Ensures that troop counts do not go below 0.
    /// </summary>
    public void EnsureCorrectTroopNumbers()
    {
        totalTroops = (punks + mercs + hackers + cyborgs);

        if (punks <= 0) { punks = 0; }
        if (mercs <= 0) { mercs = 0; }
        if (hackers <= 0) { hackers = 0; }
        if (cyborgs <= 0) { cyborgs = 0; }
    }

    /// <summary>
    /// Sets troop counts to 0.
    /// </summary>
    public void DestroyArmy()
    {
        punks = 0;
        mercs = 0;
        hackers = 0;
        cyborgs = 0;
    }

    protected virtual void EnterBattleMode(GameObject enemyObject)
    {
        Debug.Log(gameObject.name + " has entered a battle with " + enemyObject.name);

        // Destroy losing party
        if (enemyObject.GetComponent<EnemyArmy>().totalTroops > totalTroops)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(enemyObject);
        }
    }
}

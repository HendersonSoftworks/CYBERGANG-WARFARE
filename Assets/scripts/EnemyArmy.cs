using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmy : ArmyManager
{
    [SerializeField] private PlayerArmy playerArmy;
    [SerializeField] private int rewardAmount;

    void Update()
    {
        EnsureCorrectTroopNumbers();
    }

    public override void DestroyArmy()
    {
        base.DestroyArmy();
        playerArmy.credits += rewardAmount; // would be nice to have a fancy calc here that took troop num and type into account 
        Destroy(gameObject);
    }
}

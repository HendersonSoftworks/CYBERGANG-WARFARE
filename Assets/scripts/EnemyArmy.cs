using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmy : ArmyManager
{
    [SerializeField] private PlayerArmy playerArmy;
    
    public int rewardAmount;
    public int enemyStrength;

    private void Start()
    {
        enemyStrength = punks * 2 + mercs * 4 + hackers * 4 + cyborgs * 8;
    }

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

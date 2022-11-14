using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyManager : MonoBehaviour
{
    public int totalTroops;
    public int punks;
    public int mercs;
    public int hackers;
    public int cyborgs;

    public List<troop> troopList;

    public class troop : ScriptableObject
    {
        public enum troopTypes { punk, merc, hacker, cyborg}
        public troopTypes troopType;

        public troop(troopTypes troop)
        {
            troopType = troop;
        }
    }

    private void Start()
    {
        Debug.LogError("REWRITE THIS SYSTEM AS IT IS TRASH!");

        // Convert punk counts to troops
        for (int i = 0; i < punks; i++)
        {
            troop punk = new troop(troop.troopTypes.punk);
            troopList.Add(punk);
            Debug.Log("Added troop of type: " + punk.troopType);
        }
        // Convert merc counts to troops
        for (int i = 0; i < mercs; i++)
        {
            troop merc = new troop(troop.troopTypes.merc);
            troopList.Add(merc);
            Debug.Log("Added troop of type: " + merc.troopType);
        }

        // Randomise positions in Troop List
        StaticUtils.Shuffle(troopList);

        // This seems to corrupt the data...? 
        for (int i = 0; i < troopList.Count; i++)
        {
            Debug.Log(troopList[i].troopType + i);
        }
    }

    void Update()
    {
        totalTroops = (punks + mercs + hackers + cyborgs);

        if (punks <= 0){ punks = 0; }
        if (mercs <= 0) { mercs = 0; }
        if (hackers <= 0) { hackers = 0; }
        if (cyborgs <= 0) { cyborgs = 0; }
    }
}

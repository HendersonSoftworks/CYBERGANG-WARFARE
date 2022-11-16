using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] PlayerArmy playerArmy;

    [SerializeField] private GameObject dialoguePanel;
    public Text purchaseText;

    [SerializeField] Collider currentCol; 
 
    private void Update()
    {
        if (dialoguePanel.activeSelf)
        {
            ManagePurchases(currentCol);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.tag);

        // Reveal dialogue and handle purchasing units
        if (other.tag == "punk_vendor" | other.tag == "hacker_vendor" | other.tag == "merc_vendor" | other.tag == "cyborg_vendor")
        {
            dialoguePanel.SetActive(true);
        }
        // Handles purchase text
        switch (other.name)
        {
            case "punk_vendor":
                purchaseText.text = "E.Purchase punk 15CR";
                break;
            case "female_hacker_vendor":
                purchaseText.text = "E.Purchase hacker 40CR";
                break;
            case "merc_vendor":
                purchaseText.text = "E.Purchase merc 25CR";
                break;
            case "female_cyborg_vendor":
                purchaseText.text = "E.Purchase cyborg 50CR";
                break;
            default:
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "punk_vendor" | other.tag == "hacker_vendor" | other.tag == "merc_vendor" | other.tag == "cyborg_vendor")
        {
            currentCol = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "punk_vendor" | other.tag == "hacker_vendor" | other.tag == "merc_vendor" | other.tag == "cyborg_vendor")
        {
            dialoguePanel.SetActive(false);
        }
    }

    private void ManagePurchases(Collider collider)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(collider.tag);

            switch (collider.tag)
            {
                case "punk_vendor":
                    if (playerArmy.credits >= 15)
                    {
                        playerArmy.credits -= 15;
                        playerArmy.punks += 1;
                    }
                    break;
                case "hacker_vendor":
                    if (playerArmy.credits >= 40)
                    {
                        playerArmy.credits -= 40;
                        playerArmy.hackers += 1;
                    }
                    break;
                case "merc_vendor":
                    if (playerArmy.credits >= 25)
                    {
                        playerArmy.credits -= 25;
                        playerArmy.mercs += 1;
                    }
                    break;
                case "cyborg_vendor":
                    if (playerArmy.credits >= 50)
                    {
                        playerArmy.credits -= 50;
                        playerArmy.cyborgs += 1;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}

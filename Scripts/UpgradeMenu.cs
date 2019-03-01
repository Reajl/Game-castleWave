using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour {

    [SerializeField]
    private Text healthText;

    [SerializeField]
    private Text speedText;

    [SerializeField]
    private float healthMultiplier = 1.3f;

    [SerializeField]
    private float movementSpeedMultiplier = 1.3f;

    [SerializeField]
    private int upgradeCost = 50;

    private PlayerStats stats;

    void OnEnable ()
    {
        stats = PlayerStats.instance;
        UpdateValues();
    }

    void UpdateValues ()
    {
        healthText.text = "Armor: " + stats.maxHealth.ToString();
        speedText.text = "Speed: " + stats.movementSpeed.ToString();
    }

    public void UpgradeHealth ()
    {   if (GameMaster.Money < upgradeCost)
        {
            return;
        }
        stats.maxHealth = (int)(stats.maxHealth * healthMultiplier);
        GameMaster.Money -= upgradeCost;

        UpdateValues();
    }

    public void UpgradeSpeed()
    {   if (GameMaster.Money < upgradeCost)
        {
            return;
        }
        stats.movementSpeed = Mathf.Round (stats.movementSpeed * movementSpeedMultiplier);
        GameMaster.Money -= upgradeCost;

        UpdateValues();
    }

}

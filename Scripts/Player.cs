using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
[RequireComponent(typeof(Platformer2DUserControl))]
public class Player : MonoBehaviour {

	


	public int fallBoundary = -20;

	[SerializeField]
	private StatusIndicator statusIndicator;
    private PlayerStats stats;
	void Start()
	{
        stats = PlayerStats.instance;

        stats.curHealth = stats.maxHealth;


		if (statusIndicator == null)
		{
			Debug.LogError("No status indicator referenced on Player");
		}
		else
		{
			statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		}
        GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;
        GameMaster.gm.onToggleGamePaused += OnGamePausedToggle;
        InvokeRepeating("RegenHealth", 1f / stats.healthRegenRate, 1f / stats.healthRegenRate);
	}
    void RegenHealth()
    {
        stats.curHealth += 1;
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }
	void Update () {
		if (transform.position.y <= fallBoundary)
			DamagePlayer (9999999);
	}
    void OnUpgradeMenuToggle(bool active)
    {
        GetComponent<Platformer2DUserControl>().enabled = !active;
        Weapon _weapon = GetComponentInChildren<Weapon>();
        if (_weapon != null)
            _weapon.enabled = !active;
    }
    void OnGamePausedToggle(bool active)
    {
        GetComponent<Platformer2DUserControl>().enabled = !active;
        Weapon _weapon = GetComponentInChildren<Weapon>();
        if (_weapon != null)
            _weapon.enabled = !active;
    }
    void OnDestroy()
    {
        GameMaster.gm.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
        GameMaster.gm.onToggleGamePaused -= OnGamePausedToggle;
    }
	public void DamagePlayer (int damage) {
		stats.curHealth -= damage;
		if (stats.curHealth <= 0)
		{
			GameMaster.KillPlayer(this);
		}

		statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
	}

}

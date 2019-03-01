using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
[RequireComponent(typeof(Platformer2DUserControl))]
public class Enemy : MonoBehaviour {

	[System.Serializable]
	public class EnemyStats {
		public int maxHealth = 100;

		private int _curHealth;
		public int curHealth
		{
			get { return _curHealth; }
			set { _curHealth = Mathf.Clamp (value, 0, maxHealth); }
		}

		public int damage = 40;

		public void Init()
		{
			curHealth = maxHealth;
		}
	}

	public EnemyStats stats = new EnemyStats();
    public Transform deathParticles;
    public int moneyDrop = 10;


	void Start()
	{
		stats.Init ();
        GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;
        GameMaster.gm.onToggleGamePaused += OnGamePausedToggle;
	}
    void OnUpgradeMenuToggle(bool active)
    {
        GetComponent<EnemyAI>().enabled = !active;
    }
    void OnGamePausedToggle(bool active)
    {
        GetComponent<EnemyAI>().enabled = !active;
    }

	public void DamageEnemy (int damage) {
		stats.curHealth -= damage;
		if (stats.curHealth <= 0)

			GameMaster.KillEnemy (this);
	}

	void OnCollisionEnter2D(Collision2D _colInfo)
	{
		Player _player = _colInfo.collider.GetComponent<Player>();
		if (_player != null)
		{
			_player.DamagePlayer(stats.damage);
			DamageEnemy(9999999);
		}
	}
    void OnDestroy()
    {
        GameMaster.gm.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
        GameMaster.gm.onToggleGamePaused -= OnGamePausedToggle;
    }
}

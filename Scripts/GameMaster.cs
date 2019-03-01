using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;
    [SerializeField]

    public static int _remainingLives=3;
    public static int RemainingLives
    {
        get { return _remainingLives; }
    }

    [SerializeField]
    public static int Money=100;

    void Awake () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster>();
		}
	}

   
	public Transform playerPrefab;
	public Transform spawnPoint;
	public float spawnDelay = 2;
	

 
   
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject upgradeMenu;
    [SerializeField]
    private WaveSpawner waveSpawner;
    [SerializeField]
    private  GameObject gamePaused;

    public delegate void UpgradeMenuCallback(bool active);
    public UpgradeMenuCallback onToggleUpgradeMenu;

    public delegate void GamePausedCallback(bool active);
    public GamePausedCallback onToggleGamePaused;

	public IEnumerator _RespawnPlayer () {

		yield return new WaitForSeconds (spawnDelay);

		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);

	}


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ToggleUpgradeMenu();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleGamePaused();
        }
    }

  

    public void  ToggleGamePaused()
    {
        gamePaused.SetActive(!gamePaused.activeSelf);
        waveSpawner.enabled = !gamePaused.activeSelf;
        onToggleGamePaused.Invoke(gamePaused.activeSelf);
    }

    private void ToggleUpgradeMenu()
    {
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
        waveSpawner.enabled = !upgradeMenu.activeSelf;
        onToggleUpgradeMenu.Invoke(upgradeMenu.activeSelf);
    }
    public void EndGame ()
    {
   
      
        gameOverUI.SetActive(true);
    }

    public static void KillPlayer (Player player) {
        Destroy (player.gameObject);
        _remainingLives -= 1;
        if (_remainingLives <= 0)
        {
            gm.EndGame();
        } else
        {
            gm.StartCoroutine(gm._RespawnPlayer());
        }
    }

	public static void KillEnemy (Enemy enemy) {
		gm._KillEnemy(enemy);
      
	}
	public void _KillEnemy(Enemy _enemy)
    {   Money += _enemy.moneyDrop;
        GameObject _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity).gameObject;
        Destroy(_clone, 5f);
    
		Destroy(_enemy.gameObject);
	}
}

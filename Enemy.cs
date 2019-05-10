using UnityEngine;
using System.Collections;

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

        public int damage = 20;

        public void Init()
        {
            curHealth = maxHealth;
        }

    }
	
	public EnemyStats enemyStats = new EnemyStats();

    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {
        enemyStats.Init();

        if (statusIndicator != null)
        {
          //  Debug.LogError("WE HAVE AN IDICATOR");
            statusIndicator.SetHealth(enemyStats.curHealth, enemyStats.maxHealth);
        }
    }
	
	public void DamageEnemy (int damage) {
		enemyStats.curHealth -= damage;
		if (enemyStats.curHealth <= 0) {
			GameMaster.KillEnemy(this);
		}

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(enemyStats.curHealth, enemyStats.maxHealth);
        }

    }

    void Update()
    {
        if (transform.position.y < -4)
            Destroy(gameObject.transform.parent.gameObject);
    }
}

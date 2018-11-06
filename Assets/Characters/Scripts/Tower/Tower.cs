using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour {

    [System.Serializable]
	public class TowerStats
    {
        public int maxHealth = 100;

        private int _currHealth;
        public int currHealth
        {
            get { return _currHealth; }
            set { _currHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            _currHealth = maxHealth;
        }
    }

    public TowerStats stats = new TowerStats();

    public void DamageTower (int damage)
    {
        stats.currHealth -= damage;
        if (stats.currHealth <= 0)
        {
            GameManager.killPlayer(this);
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour {

    [System.Serializable]
    public class TowerStats
    {
        public int life_tower = 1000;
        public int defense = 0;

        private int _currHealth;
        public int currHealth
        {
            get { return _currHealth; }
            set { _currHealth = Mathf.Clamp(value, 0, life_tower); }
        }

        public void Init()
        {
            currHealth = life_tower;
        }
    }

    [Header("Unity Stuff")]
    public Image healthBar;

    public TowerStats stats = new TowerStats();

    private void Start()
    {
        stats.Init();
    }

    public void TakeDamage (int damage)
    {
        stats.currHealth -= (damage - stats.defense);
        healthBar.fillAmount = (float) stats.currHealth / stats.life_tower;
        if (stats.currHealth <= 0)
        {
            GameManager.killPlayer(this);
        }
    }
}

using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

	public int maxHealth;

	int health;
	public int GetHealth() {
		return health;
	}

	public delegate void CharacterDied();
	public event CharacterDied OnCharacterDeath;

	void Start() {
		health = maxHealth;
	}

	public void TakeDamage(int damage) {
		health -= damage;

		if (health < 0) {
			health = 0;
		}

		Debug.Log(gameObject.name + "'s health: " + health);

		if (health == 0) {
			if(OnCharacterDeath != null) {
				OnCharacterDeath();
			}
		}
	}

	public void Heal(int amount) {
		health += amount;

		if (health > maxHealth) {
			health = maxHealth;
		}

		Debug.Log(gameObject.name + "'s health: " + health);
	}
}

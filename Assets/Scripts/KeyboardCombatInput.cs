using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CombatController))]
public class KeyboardCombatInput : MonoBehaviour {

	CombatController combatController;

	public KeyCode defaultAttackKey;

	KeyCode attackKey;

	void Awake() {
		combatController = GetComponent<CombatController>();
	}

	void Start() {
		attackKey = defaultAttackKey;
	}

	void Update() {
		if (Input.GetKeyDown(attackKey)) {
			combatController.Attack();
		}
	}
}

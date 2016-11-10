using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(HealthController))]
public class CombatController : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	MovementController movementController;
	HealthController healthController;

	[SerializeField]
	LayerMask attackableLayerMask;

	public int strength;

	[SerializeField]
	Color baseColor;

	bool isBlocking;
	public bool GetIsBlocking() {
		return isBlocking;
	}

	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		movementController = GetComponent<MovementController>();
		healthController = GetComponent<HealthController>();
	}

	void Start() {
		baseColor = spriteRenderer.color;

		SetBlocking(false);
	}

	public void SetBlocking(bool value) {
		isBlocking = value;
		if (value) {
			spriteRenderer.color = Color.black;
		} 
		else {
			spriteRenderer.color = baseColor;
		}
	}

	public void Attack() {
		// Perform animation
		Vector3 facingDir = movementController.IsFacingRight() ? Vector3.right : Vector3.left;
		Vector3 raycastStart = transform.position + (facingDir * (((spriteRenderer.bounds.size.x/2) + 0.1f)));

		Debug.DrawLine(raycastStart, (raycastStart + (facingDir * 0.5f)), Color.red, 0.5f, false);
		// Debug.DrawLine(transform.position, (transform.position + (facingDir * ((spriteRenderer.bounds.size.x/2)+0.5f))), Color.red, 0.5f, false);

		RaycastHit2D characterHit = Physics2D.Raycast(raycastStart, facingDir, 0.5f, attackableLayerMask);
		// RaycastHit2D characterHit = Physics2D.Raycast(transform.position, facingDir, (spriteRenderer.bounds.size.y/2)+0.5f, attackableLayerMask);
		if (characterHit.collider != null) {
			CombatController otherCharacterCombatController = characterHit.collider.gameObject.GetComponent<CombatController>();

			if (otherCharacterCombatController != null) {
				otherCharacterCombatController.TakeDamage(strength);
			}
		}
	}

	public void TakeDamage(int damage) {
		int damageTaken = damage;

		if (isBlocking) {
			damageTaken = 0;
		}

		healthController.TakeDamage(damageTaken);
	}
}

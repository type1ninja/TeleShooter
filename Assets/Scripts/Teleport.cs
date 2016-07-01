using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	CharacterController charControl;
	Transform charHead;
	CharacterMove charMove;

	float distSensitivity = 5.0f;

	float teleportDist = 0f;
	float maxDist = 100f;
	float minDist = -100f;

	float maxTeleCooldown = .75f;
	float currentTeleCooldown;
	bool hasTeleported = false;

	// Use this for initialization
	void Start () {
		charControl = GetComponent<CharacterController> ();
		charHead = transform.Find ("CharacterHead");
		charMove = GetComponent<CharacterMove> ();

		currentTeleCooldown = maxTeleCooldown;
	}

	void Update () {
		teleportDist += Input.GetAxis ("Mouse ScrollWheel") * distSensitivity;
		if (teleportDist > maxDist) {
			teleportDist = maxDist;
		} 
		if (teleportDist < minDist) {
			teleportDist = minDist;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		currentTeleCooldown -= Time.fixedDeltaTime;
		if (currentTeleCooldown < 0) {
			currentTeleCooldown = maxTeleCooldown;
			hasTeleported = false;
		}

		if (Input.GetButton ("SecondaryFire") && !hasTeleported) {
			charMove.StopMotion();
			charControl.Move (charHead.forward * teleportDist);

			hasTeleported = true;
		}
	}
}

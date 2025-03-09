using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	// References to the text UI elements
	public Text NumTotal;
	public Text WinText;

	// Reference to the rigidbody component of the player
	private Rigidbody rb;
	
    // Player speed
    public float Speed;

	// Count of pick up objects collected so far
	private float scoreCount;

	// When the player loads into the scene...
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		scoreCount = 0;
		SetCountText();
		WinText.text = "";
	}

	// Every physics tick...
	void FixedUpdate()
	{
		// Get the player's input
		// (Could be optimized further. If we use the new Input System Unity package instead,
		// we could run this only when a new input is detected with OnMove())
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		// Create a Vector3 for movement using the player's inputs
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		// Apply the movement
		rb.AddForce(movement * Speed);
	}

	// When we trigger another object's box collider...
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Pick Up"))
		{
			other.gameObject.SetActive(false);
			scoreCount = scoreCount + 1;
			SetCountText();
		}
	}
	
	// Update the score counter in the UI
	void SetCountText()
	{
		NumTotal.text = "Count: " + scoreCount.ToString();
		// If all pick ups were collected (update this number to match the # of pick ups)
		if (scoreCount >= 12) 
		{
			WinText.text = "You Win!";
		}
	}
}
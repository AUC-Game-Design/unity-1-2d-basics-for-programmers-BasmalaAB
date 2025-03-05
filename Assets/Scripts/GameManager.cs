using UnityEngine;
using Unity.Cinemachine;  // Use this namespace

public class GameManager : MonoBehaviour
{
    public GameObject player1; // Assign Ruby
    public GameObject player2; // Assign Sugar
    private GameObject activePlayer;

    private PlayerController controller1; // Store PlayerController component
    private PlayerController controller2; // Store PlayerController component

    public CinemachineCamera cinemachineCam; // Assign your Cinemachine Virtual Camera
    void Start()
    {
        activePlayer = player1; // Start with player1 active
        controller1 = player1.GetComponent<PlayerController>();
        controller2 = player2.GetComponent<PlayerController>();

        // Ensure player1 starts with control, player2 starts disabled
        controller1.enabled = true;
        controller2.enabled = false;

        if (cinemachineCam != null)
        {
            cinemachineCam.Follow = player1.transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // Press Q to switch
        {
            SwitchPlayer();
        }
    }

    void SwitchPlayer()
    {
        if (activePlayer == player1)
        {
            controller1.enabled = false;  // Disable Ruby
            controller2.enabled = true;   // Enable Sugar
            activePlayer = player2;
        }
        else
        {
            controller1.enabled = true;   // Enable Ruby
            controller2.enabled = false;  // Disable Sugar
            activePlayer = player1;
        }

        // Update Cinemachine camera to follow the new active player
        if (cinemachineCam != null)
        {
            cinemachineCam.LookAt = activePlayer.transform; // Optional: set LookAt target
            cinemachineCam.Follow = activePlayer.transform;
        }
    }
}

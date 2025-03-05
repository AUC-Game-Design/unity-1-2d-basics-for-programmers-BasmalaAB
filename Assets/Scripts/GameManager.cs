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

    public string[,] player_Speaking;
    /// <summary>
    /// //////////////////////////////////////////////////////////////////
    /// </summary>
    public GameObject candy_box;
    public bool candybox_collected = false;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    EnemyController c_enemy1;
    EnemyController c_enemy2;
    EnemyController c_enemy3;

    public GameObject gate1;
    public GameObject gate2;

    private Vector3 gate1StartPos;
    private Vector3 gate2StartPos;
    private bool gatesShouldMove = false;

    public int froggoDialogueIndex = 0;
    public int teddyDialogueIndex = 0;
    private bool missionCompleted1 = false;
    private bool missionCompleted2 = false;

    /// <summary>
    /// /////////////////////////////////////////////////////////////////////
    /// </summary>


    void Start()
    {
        activePlayer = player1; // Start with player1 active
        controller1 = player1.GetComponent<PlayerController>();
        controller2 = player2.GetComponent<PlayerController>();

        //////////////////////////////////////////////////////////////////////
        c_enemy1 = enemy1.GetComponent<EnemyController>();
        c_enemy2 = enemy2.GetComponent<EnemyController>();
        c_enemy3 = enemy3.GetComponent<EnemyController>();

        gate1StartPos = gate1.transform.position;
        gate2StartPos = gate2.transform.position;
        //////////////////////////////////////////////////////////////////////

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

        if (c_enemy1.broken == false && c_enemy2.broken == false && c_enemy3.broken == false)
        {
            gatesShouldMove = true; // Start moving gates
        }

        if (gatesShouldMove)
        {
            // Define target positions (2 units from the original positions)
            Vector3 targetPos1 = gate1StartPos + new Vector3(2.0f, 0, 0);
            Vector3 targetPos2 = gate2StartPos + new Vector3(-2.0f, 0, 0);

            // Gradually move towards target positions
            gate1.transform.position = Vector3.Lerp(gate1.transform.position, targetPos1, Time.deltaTime * 2);
            gate2.transform.position = Vector3.Lerp(gate2.transform.position, targetPos2, Time.deltaTime * 2);

            // Stop moving when close enough to target positions
            if (Vector3.Distance(gate1.transform.position, targetPos1) < 0.01f &&
                Vector3.Distance(gate2.transform.position, targetPos2) < 0.01f)
            {
                gatesShouldMove = false; // Stop updating positions
            }
        }

        if (activePlayer == player1) //ruby can't see the box!
        {
            candy_box.SetActive(false);
        }
        else if (activePlayer == player2) //only sugar can see the box
        {
            candy_box.SetActive(true);
        }

        if (!missionCompleted1 && AllEnemiesDefeated())
        {
            missionCompleted1 = true;
            froggoDialogueIndex = 3;  // Update dialogue index to the "mission complete" line
        }

        if (!missionCompleted2 && candybox_collected)
        {
            missionCompleted2 = true;
            teddyDialogueIndex = 3;
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

    bool AllEnemiesDefeated()
    {
        if (c_enemy1.broken == false && c_enemy2.broken == false && c_enemy3.broken == false)
        {
            return true;
        }
        return false;
    }
}

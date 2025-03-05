using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public string identity = "Froggo";  // Assign per NPC
    private string[,] dialogue;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();  // Find the GameManager instance
        InitializeDialogue();
    }

    private void InitializeDialogue()
    {
        dialogue = new string[2, 4]
        {
            // Froggo's dialogue (row 0)
            { "Hmph. What do you want now?",
              "Ugh, those useless tin cans broke again… *AGAIN!* Go fix ‘em! \n (Hint: To shoot, press E. To switch between Ruby and Sugar, press Q.)",
              "Still tinkering with those robots? Hurry up! \n (Hint: To shoot, press E. To switch between Ruby and Sugar, press Q.)",
              "Took ya long enough. Now scram, I need my nap!" },

            // Teddy's dialogue (row 1)
            { "Oh no… oh dear… where *are* they?!",
              "*My muffins!* They’re gone! I was baking for tonight’s party, and now they’re missing! \n Please, Sugar, will you help me find them?",
              "Still no muffins? Oh dear… \n(Hint: Ruby’s not from Candy Town, but Sugar is! Maybe he can find them?) \n(Hint 2: Press C to give Teddy his muffins!)",
              "You found them! Thank you so much, Sugar! And you too, Ruby! \n Tonight’s party is gonna be *sweet!*" }
        };
    }

    public string GetDialogue()
    {
        if (identity == "Froggo")
            return dialogue[0, gameManager.froggoDialogueIndex];

        if (identity == "Teddy")
            return dialogue[1, gameManager.teddyDialogueIndex];

        return "Hmm?";
    }

    public void AdvanceDialogue()
    {
        if (identity == "Froggo")
        {
            if (gameManager.froggoDialogueIndex < 2)
                gameManager.froggoDialogueIndex++;
        }
        else if (identity == "Teddy")
        {
            if (gameManager.teddyDialogueIndex < 2)
                gameManager.teddyDialogueIndex++;
        }
    }
}

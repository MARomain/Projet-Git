using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : Singleton<GameManager> {

    public PlayerManager[] Players;
    public GameObject[] playerPrefab;
    public int m_TotalRounds = 5;            // The number of rounds a single player has to win to win the game.
    public float m_StartDelay = 3f;             // The delay between the start of RoundStarting and RoundPlaying phases.
    public float m_EndDelay = 3f;               // The delay between the end of RoundPlaying and RoundEnding phases.

    public Text m_MessageText;                  // Reference to the overlay Text to display winning text, etc.
    private int m_RoundNumber;                  // Which round the game is currently on.
    private WaitForSeconds m_StartWait;         // Used to have a delay whilst the round starts.
    private WaitForSeconds m_EndWait;           // Used to have a delay whilst the round or game ends.
    private PlayerManager m_RoundWinner;          // Reference to the winner of the current round.  Used to make an announcement of who won.
    private PlayerManager m_GameWinner;           // Reference to the winner of the game.  Used to make an announcement of who won.

    public Text timer;
    private bool timerActivated;
    public float roundTime = 30;
    private float timeLeft;


    void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        Human warrior = new Warrior();

        Human mage = new Mage();

        PlayerManager p1 = new PlayerManager();
        p1.Setup();

       
        SpawnPlayers();

        StartCoroutine(GameLoop());
    }

    private void Update()
    {
        if (timerActivated)
        {
            if (timeLeft > 00)
            {
                timeLeft -= Time.deltaTime;
                timer.text = timeLeft.ToString();
            }
        }

    }

    private void SpawnPlayers()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].m_Instance =
                Instantiate(playerPrefab[i % playerPrefab.Length], Players[i].m_SpawnPoint.position, Players[i].m_SpawnPoint.rotation) as GameObject;
            Players[i].m_PlayerNumber = i + 1; //ça ça vient du script originel
            Players[i].m_Instance.GetComponent<Human>().playerNumber = i + 1; // cette ligne elle sert pour le score
            Players[i].Setup();
        }
    }

    // This is called from start and will run each phase of the game one after another.
    private IEnumerator GameLoop()
    {
        // Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
        yield return StartCoroutine(RoundStarting());

        // Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
        yield return StartCoroutine(RoundPlaying());

        // Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
        yield return StartCoroutine(RoundEnding());

        // This code is not run until 'RoundEnding' has finished.  At which point, check if a game winner has been found.
        if (m_GameWinner != null)
        {
            // If there is a game winner, restart the level.
            SceneManager.LoadScene(0);
        }
        else
        {
            // If there isn't a winner yet, restart this coroutine so the loop continues.
            // Note that this coroutine doesn't yield.  This means that the current version of the GameLoop will end.
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator RoundStarting()
    {
        Debug.Log("roundtstaring");
        // As soon as the round starts reset the tanks and make sure they can't move.
        ResetAllPlayers();
        DisableControl();
        timeLeft = roundTime;


        //Increment the round number and display text showing the players what round it is.
        m_RoundNumber++;
        m_MessageText.text = "ROUND " + m_RoundNumber;

        // Wait for the specified length of time until yielding control back to the game loop.
        yield return m_StartWait;
    }


    private IEnumerator RoundPlaying()
    {
        Debug.Log("roundplaying");
        // As soon as the round begins playing let the players control the tanks.
        EnableControl();
        timerActivated = true;

        // Clear the text from the screen.
        m_MessageText.text = string.Empty;

        // While there is not one tank left...
        while (!OnePlayerLeft() && timeLeft > 0)
        {
            // ... return on the next frame.
            yield return null;
        }


    }


    private IEnumerator RoundEnding()
    {
        Debug.Log("roundending");
        // Stop tanks from moving.
        DisableControl();
        timerActivated = false;

        // Clear the winner from the previous round.
        m_RoundWinner = null;

        // See if there is a winner now the round is over.
        m_RoundWinner = GetRoundWinner();

        // Now the winner's score has been incremented, see if someone has one the game.
        if(m_RoundNumber == m_TotalRounds)
        m_GameWinner = GetGameWinner();

        // Get a message based on the scores and whether or not there is a game winner and display it.
        string message = EndMessage();
        m_MessageText.text = message;

        // Wait for the specified length of time until yielding control back to the game loop.
        yield return m_EndWait;
    }


    // This is used to check if there is one or fewer tanks remaining and thus the round should end.
    private bool OnePlayerLeft()
    {
        // Start the count of tanks left at zero.
        int numPlayersLeft = 0;

        // Go through all the tanks...
        for (int i = 0; i < Players.Length; i++)
        {
            // ... and if they are active, increment the counter.
            if (Players[i].m_Instance.activeSelf)
                numPlayersLeft++;
        }

        // If there are one or fewer tanks remaining return true, otherwise return false.
        return numPlayersLeft <= 1;
    }


    private PlayerManager GetRoundWinner()
    { 
    {
        if (Score.Instance.player1Score > Score.Instance.player2Score)
            return Players[0];

        else if (Score.Instance.player2Score > Score.Instance.player1Score)
            return Players[1];
    }

        // If none of the tanks are active it is a draw so return null.
        return null;
    }


    // This function is to find out if there is a winner of the game.
    private PlayerManager GetGameWinner()
    {
        if (Score.Instance.player1Score > Score.Instance.player2Score)
            return Players[0];

        else if(Score.Instance.player2Score > Score.Instance.player1Score)
            return Players[1]; 

        // If no tanks have enough rounds to win, return null.
        return null;
    }


    // Returns a string message to display at the end of each round.
    private string EndMessage()
    {
        // By default when a round ends there are no winners so the default end message is a draw.
        string message = "EGALITE";

        // If there is a winner then change the message to reflect that.
        if (m_RoundWinner != null)
            message = m_RoundWinner.m_ColoredPlayerText + " EST EN TETE";

        // Add some line breaks after the initial message.
        message += "\n\n\n\n";

        // If there is a game winner, change the entire message to reflect that.
        if (m_GameWinner != null)
            message = m_GameWinner.m_ColoredPlayerText + " GAGNE LA PARTIE";

        return message;
    }


    // This function is used to turn all the tanks back on and reset their positions and properties.
    private void ResetAllPlayers()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].Reset();
}
    }

    private void EnableControl()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].EnableControl();
        }
    }

    private void DisableControl()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].DisableControl();
        }
    }

}

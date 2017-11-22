using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : Singleton<GameManager> {

    public PlayerManager[] Players;
    public GameObject[] playerPrefab;
    public int totalRounds = 5;
    public float startDelay = 3f;
    public float endDelay = 3f;

    public Text messageText;
    private int roundNumber;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;
    private PlayerManager roundWinner;
    private PlayerManager gameWinner;

    public Text timer;
    private bool timerActivated;
    public float roundTime = 30;
    private float timeLeft;


    void Start()
    {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        //Human warrior = new Warrior();
        //Human mage = new Mage();

        SpawnPlayers();

        //PlayerManager p1 = new PlayerManager();
        //p1.Setup();

        StartCoroutine(GameLoop());
    }

    private void Update()
    {
        if (timerActivated)
        {
            if (timeLeft > 00)
            {
                timeLeft -= Time.deltaTime;
                timer.text = Mathf.Round(timeLeft).ToString();
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

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());

        yield return StartCoroutine(RoundPlaying());

        yield return StartCoroutine(RoundEnding());

        if (gameWinner != null)
        {
            SceneManager.LoadScene(0);
        }
        else if (roundNumber == totalRounds)    // si le nombre de round est le nombre max on stop le jeux 
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator RoundStarting()
    {
        Debug.Log("roundtstaring");
        ResetAllPlayers();
        DisableControl();
        timeLeft = roundTime;


        //Increment the round number and display text showing the players what round it is.
        roundNumber++;
        messageText.text = "ROUND " + roundNumber;

        // Wait for the specified length of time until yielding control back to the game loop.
        yield return startWait;
    }


    private IEnumerator RoundPlaying()
    {
        // As soon as the round begins playing let the players control the tanks.
        EnableControl();
        timerActivated = true;

        // Clear the text from the screen.
        messageText.text = string.Empty;

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
        roundWinner = null;

        // See if there is a winner now the round is over.
        roundWinner = GetRoundWinner();

        // Now the winner's score has been incremented, see if someone has one the game.
        if(roundNumber == totalRounds)
        gameWinner = GetGameWinner();

        // Get a message based on the scores and whether or not there is a game winner and display it.
        string message = EndMessage();
        messageText.text = message;

        // Wait for the specified length of time until yielding control back to the game loop.
        yield return endWait;
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
        if (roundWinner != null)
            message = roundWinner.m_ColoredPlayerText + " EST EN TETE";

        // Add some line breaks after the initial message.
        message += "\n\n\n\n";

        // If there is a game winner, change the entire message to reflect that.
        if (gameWinner != null)
        {
            Debug.Log("game winner");
            message = gameWinner.m_ColoredPlayerText + " GAGNE LA PARTIE";
        }
        else if (roundNumber == totalRounds)
        {
            Debug.Log("égalité");
            message = "EGALITE";
        }

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

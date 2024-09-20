using UnityEngine;

public class PlayerSaveManager : MonoBehaviour
{
    // Reference to the player transform
    public Transform player;

    public Playtime playtimeManager;

    public float? besttime = null;

    // Key for saving the player's position in PlayerPrefs
    private const string PositionXKey = "PlayerPositionX";
    private const string PositionYKey = "PlayerPositionY";
    private const string PositionZKey = "PlayerPositionZ";
    private const string PlaytimeKey = "Playtime";
    private const string BestTimeKey = "BestTime";

    void Start()
    {
        LoadInfo(); // Load position when the game starts
        GameOverTrigger.OnGameOver += () => { besttime = playtimeManager.playtime; playtimeManager.playtime = 0; SaveInfo(); };
    }

    // Method to save player's position
    public void SaveInfo()
    {
        Vector3 position = player.position;

        // Save each component of the position
        PlayerPrefs.SetFloat(PositionXKey, position.x);
        PlayerPrefs.SetFloat(PositionYKey, position.y);
        PlayerPrefs.SetFloat(PositionZKey, position.z);

        PlayerPrefs.SetFloat(PlaytimeKey, playtimeManager.playtime);

        if (besttime != null)
        {
            PlayerPrefs.SetFloat(BestTimeKey, (float)besttime);
        }

        // Ensure the data is saved immediately
        PlayerPrefs.Save();

        Debug.Log("Player position saved: " + position);
    }

    // Method to load player's position
    public void LoadInfo()
    {
        Vector3 loadedPosition;
        // Check if the saved data exists
        if (PlayerPrefs.HasKey(PositionXKey))
        {
            float x = PlayerPrefs.GetFloat(PositionXKey);
            float y = PlayerPrefs.GetFloat(PositionYKey);
            float z = PlayerPrefs.GetFloat(PositionZKey);

            loadedPosition = new Vector3(x, y, z);

            Debug.Log("Player position loaded: " + loadedPosition);
        }
        else
        {
            loadedPosition = new(127.5f, 12f, 90.5f);
            Debug.Log("No saved position found, starting with default position.");
        }
        player.position = loadedPosition;

        if (PlayerPrefs.HasKey(PlaytimeKey))
        {
            playtimeManager.playtime = PlayerPrefs.GetFloat(PlaytimeKey);
        }
        else
        {
            playtimeManager.playtime = 0f;
        }

        if (PlayerPrefs.HasKey(BestTimeKey))
        {
            besttime = PlayerPrefs.GetFloat(BestTimeKey);
        }
        else
        {
            besttime = null;
        }
    }
}

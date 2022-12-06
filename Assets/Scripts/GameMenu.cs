using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public static bool SoundsEnabled { get; private set; }
    public static float SoundsVolume { get; private set; }

    private const string preferencesFilename = "preferences.txt";

    private static GameObject MenuContainer;
    private static TMPro.TextMeshProUGUI Message;
    private static TMPro.TextMeshProUGUI Stats;
    private static TMPro.TextMeshProUGUI MenuButtonText;

    private AudioSource backgroundMusic;

    void Start()
    {
        MenuContainer = GameObject.Find(nameof(MenuContainer));
        Message = GameObject
        .Find(nameof(Message))
        .GetComponent<TMPro.TextMeshProUGUI>();
        Stats = GameObject
        .Find(nameof(Stats))
        .GetComponent<TMPro.TextMeshProUGUI>();
        MenuButtonText = GameObject
        .Find(nameof(MenuButtonText))
        .GetComponent<TMPro.TextMeshProUGUI>();

        backgroundMusic = GetComponent<AudioSource>();
        SoundsEnabled = GameObject
            .Find("SoundsToggle")
            .GetComponent<UnityEngine.UI.Toggle>().isOn;
        SoundsVolume = GameObject
            .Find("SoundsVolumeSlider")
            .GetComponent<UnityEngine.UI.Slider>().value;

        if (MenuContainer.activeInHierarchy)
        {
            GameMenu.Show(Message.text, MenuButtonText.text);
        }
    }

    void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            
            if (MenuContainer.activeInHierarchy)
            {
                GameMenu.Hide();
            
                return;
            }

            GameMenu.Show();
        }
    }

    #region Event Handlers
    public void MenuButtonClick()
    {
        GameMenu.Hide();
    }

    public void MusicToggled(bool enabled)
    {
        if (enabled) {
            backgroundMusic.Play();

            return;
        }

        backgroundMusic.Stop();
    }

    public void MusicVolumeChanged(float value) {
        backgroundMusic.volume = value;
    }

    public void SoundsToggled(bool enabled) {
        GameMenu.SoundsEnabled = enabled;
    }

    public void SoundsVolumeChanged(float value) {
        GameMenu.SoundsVolume = value;
    }
    #endregion

    public static void Show(
        string messageText = "Game paused",
        string buttonText = "Continue"
    )
    {
        MenuContainer.SetActive(true);
        Message.text = messageText;
        MenuButtonText.text = buttonText;
        Stats.text = $"Time in game: {GameStat.GameTime:F1}\n";
        Stats.text += $"First checkpoint: ";
        switch (GameStat.FirstCheckpointTime)
        {
            case -1:
                Stats.text += "in progress\n";
                break;
            case 0:
                Stats.text += "failed\n";
                break;
            default:
                Stats.text += $"passed at {GameStat.FirstCheckpointTime:F1}\n";
                break;
        }
        Stats.text += $"Second checkpoint: ";
        switch (GameStat.SecondCheckpointTime)
        {
            case -1:
                Stats.text += "in progress\n";
                break;
            case 0:
                Stats.text += "failed\n";
                break;
            default:
                Stats.text += $"passed at {GameStat.SecondCheckpointTime:F1}\n";
                break;
        }
        Stats.text += $"Final checkpoint: ";
        switch (GameStat.FinalCheckpointTime)
        {
            case -1:
                Stats.text += "in progress\n";
                break;
            case 0:
                Stats.text += "failed\n";
                break;
            default:
                Stats.text += $"passed at {GameStat.FinalCheckpointTime:F1}\n";
                break;
        }
        Stats.text += $"Score: {GameStat.GameScore}";
        Time.timeScale = 0;
    }

    public static void Hide()
    {
        MenuContainer.SetActive(false);
        Time.timeScale = 1;
    }
}
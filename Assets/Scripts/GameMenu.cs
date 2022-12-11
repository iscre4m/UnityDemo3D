using UnityEngine;
using System;
using System.Text;

public class GameMenu : MonoBehaviour
{
    public static bool SoundsEnabled { get; private set; }
    public static float SoundsVolume { get; private set; }

    private const string preferencesFilename = "Assets/Files/preferences.txt";

    private static GameObject MenuContainer;
    private static TMPro.TextMeshProUGUI Message;
    private static TMPro.TextMeshProUGUI Stats;
    private static TMPro.TextMeshProUGUI MenuButtonText;

    private AudioSource backgroundMusic;

    private bool musicEnabled;
    private float musicVolume;

    void Start()
    {
        MenuContainer = GameObject
            .Find(nameof(MenuContainer));
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

        var MusicToggle = GameObject
            .Find("MusicToggle")
            .GetComponent<UnityEngine.UI.Toggle>();
        var MusicVolumeSlider = GameObject
            .Find("MusicVolumeSlider")
            .GetComponent<UnityEngine.UI.Slider>();
        var SoundsToggle = GameObject
            .Find("SoundsToggle")
            .GetComponent<UnityEngine.UI.Toggle>();
        var SoundsVolumeSlider = GameObject
            .Find("SoundsVolumeSlider")
            .GetComponent<UnityEngine.UI.Slider>();

        if (LoadPreferences())
        {
            MusicToggle.isOn = musicEnabled;
            MusicVolumeSlider.value = musicVolume;
            SoundsToggle.isOn = SoundsEnabled;
            SoundsVolumeSlider.value = SoundsVolume;
        }
        else
        {
            musicEnabled = MusicToggle.isOn;
            musicVolume = MusicVolumeSlider.value;
            SoundsEnabled = SoundsToggle.isOn;
            SoundsVolume = SoundsVolumeSlider.value;
        }

        UpdateMusicState();

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

    private void OnDestroy()
    {
        SavePreferences();
    }

    #region Event Handlers
    public void MenuButtonClick()
    {
        GameMenu.Hide();
    }

    public void MusicToggled(bool value)
    {
        musicEnabled = value;
        UpdateMusicState();
    }

    public void MusicVolumeChanged(float value) {
        musicVolume = value;
        UpdateMusicState();
    }

    public void SoundsToggled(bool enabled) {
        GameMenu.SoundsEnabled = enabled;
    }

    public void SoundsVolumeChanged(float value) {
        GameMenu.SoundsVolume = value;
    }
    #endregion

    private void UpdateMusicState()
    {
        backgroundMusic.volume = musicVolume;

        if (musicEnabled)
        {
            if (!backgroundMusic.isPlaying)
            {
                backgroundMusic.Play();
            }
            
            return;
        }

        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }
    }

    private static string CheckpointTimeToMessage(float time)
    {
        switch (time)
        {
            case -1:
                return "in progress";
            case 0:
                return "failed";
            default:
                return $"passed at {time:00.0}";
        }
    }

    public static void Show(
        string messageText = "Game paused",
        string buttonText = "Continue"
    )
    {
        MenuContainer.SetActive(true);
        Message.text = messageText;
        MenuButtonText.text = buttonText;
        StringBuilder statsBuilder = new();
        statsBuilder.Append($"Time in game: {GameStat.GameTime:00.0}\n");
        string number;
        for (int i = 0; i < 3; ++i)
        {
            number = i == 0 ? "First" : i == 1 ? "Second" : "Third";
            statsBuilder.Append($"{number} checkpoint: ");
            statsBuilder.Append(
                CheckpointTimeToMessage(
                    i == 0
                    ? GameStat.FirstCheckpointTime
                    : i == 1
                      ? GameStat.SecondCheckpointTime
                      : GameStat.FinalCheckpointTime
                )
            );
            if (i < 2)
            {
                statsBuilder.Append("\n");
            }
        }
        Stats.text = statsBuilder.ToString();
        Time.timeScale = 0;
    }

    public static void Hide()
    {
        MenuContainer.SetActive(false);
        Time.timeScale = 1;
    }

    private void SavePreferences()
    {
        System.IO.File.WriteAllText(preferencesFilename,
            $"{musicEnabled};{musicVolume};" +
            $"{GameMenu.SoundsEnabled};{GameMenu.SoundsVolume}"
        );
    }
    private bool LoadPreferences()
    {
        if (System.IO.File.Exists(preferencesFilename))
        {
            try
            {
                string[] data = System.IO.File
                                .ReadAllText(preferencesFilename)
                                .Split(";");
                musicEnabled = Convert.ToBoolean(data[0]);
                musicVolume = Convert.ToSingle(data[1]);
                SoundsEnabled = Convert.ToBoolean(data[2]);
                SoundsVolume = Convert.ToSingle(data[3]);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            
            return true;
        }

        return false;
    }
}
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    private static GameObject MenuContainer;
    private static TMPro.TextMeshProUGUI Message;
    private static TMPro.TextMeshProUGUI Stats;
    private static TMPro.TextMeshProUGUI MenuButtonText;

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

    public void MenuButtonClick()
    {
        GameMenu.Hide();
    }

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
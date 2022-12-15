using UnityEngine;

public class EndGameMenu : MonoBehaviour
{
    private static GameObject EndGameMenuContainer;
    private static TMPro.TextMeshProUGUI ResultMessage;

    void Start()
    {
        EndGameMenuContainer = GameObject
            .Find(nameof(EndGameMenuContainer));
        ResultMessage = GameObject
            .Find(nameof(ResultMessage))
            .GetComponent<TMPro.TextMeshProUGUI>();
        
        if (EndGameMenuContainer.activeInHierarchy)
        {
            EndGameMenuContainer.SetActive(false);
        }
    }

    public static void Show()
    {
        Time.timeScale = 0;
        ResultMessage.text = $"You scored {GameStat.GameScore}\n";
        ResultMessage.text += "1st checkpoint: ";
        ResultMessage.text += GameStat.FirstCheckpointTime <= 0
            ? "failed"
            : $"{GameStat.FirstCheckpointTime}";
        ResultMessage.text += "\n2nd checkpoint: ";
        ResultMessage.text += GameStat.SecondCheckpointTime <= 0
            ? "failed"
            : $"{GameStat.SecondCheckpointTime}";
        ResultMessage.text += "\n3rd checkpoint: ";
        ResultMessage.text += GameStat.FinalCheckpointTime <= 0
            ? "failed"
            : $"{GameStat.FinalCheckpointTime}\n";
        int recordStatus = GameStat.CheckForRecord();
        switch (recordStatus)
        {
            case 0:
                ResultMessage.text += "You've achieved 1st place";
                break;
            case 1:
                ResultMessage.text += "You've achieved 2nd place";
                break;
            case 2:
                ResultMessage.text += "You've achieved 3rd place";
                break;
            default:
                break;
        }
        EndGameMenuContainer.SetActive(true);
    }
}

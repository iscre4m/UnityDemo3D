using UnityEngine;

public class FinalCheckpoint : MonoBehaviour
{
    private const float START_TIME = 21;

    public static bool IsActivated;

    [SerializeField]
    private UnityEngine.UI.Image Countdown;

    private float countdownTime;
    private byte scoreValue = 30;
    private bool checkpointExpired;

    void Start()
    {
        countdownTime = START_TIME - GameMenu.Difficulty;
    }

    void Update()
    {
        if (FinalCheckpoint.IsActivated)
        {
            countdownTime -= Time.deltaTime;

            if (countdownTime < 0)
            {
                GameStat.SetFinalCheckpointStatus(false);
                checkpointExpired = true;

                return;
            }

            GameStat.FinalCheckpointFill =
            Countdown.fillAmount = countdownTime /
            (START_TIME - GameMenu.Difficulty);
            Countdown.color = new Color(
                1 - GameStat.FinalCheckpointFill,
                GameStat.FinalCheckpointFill,
                .1f
            );
        }
        else
        {
            countdownTime = START_TIME - GameMenu.Difficulty;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!checkpointExpired)
        {
            GameStat.SetFinalCheckpointStatus(true);
        }

        GameStat.GameScore += checkpointExpired ? (byte)(scoreValue / 2) : scoreValue;
    }
}

using UnityEngine;

public class FinalCheckpoint : MonoBehaviour
{
    private const float START_TIME = 20;

    public static bool IsActivated;

    [SerializeField]
    private UnityEngine.UI.Image countdown;

    private float countdownTime;
    private byte scoreValue = 30;
    private bool checkpointExpired;

    void Start()
    {
        FinalCheckpoint.IsActivated = false;
        countdownTime = START_TIME;
        checkpointExpired = false;
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
            countdown.fillAmount = countdownTime / START_TIME;
            countdown.color = new Color(
                1 - GameStat.FinalCheckpointFill,
                GameStat.FinalCheckpointFill,
                .1f
            );
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

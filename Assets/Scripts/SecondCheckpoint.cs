using UnityEngine;

public class SecondCheckpoint : MonoBehaviour
{
    private const float START_TIME = 11;
    private byte SCORE_VALUE = 15;
    
    public static bool IsActivated;

    [SerializeField]
    private UnityEngine.UI.Image Countdown;
    
    private float countdownTime;
    private GameObject SecondGate;

    void Start()
    {
        SecondCheckpoint.IsActivated = false;
        countdownTime = START_TIME - GameMenu.Difficulty;
        SecondGate = GameObject.Find(nameof(SecondGate));
    }

    void Update()
    {
        if (SecondCheckpoint.IsActivated)
        {
            countdownTime -= Time.deltaTime;

            if (countdownTime < 0)
            {
                GameStat.SetSecondCheckpointStatus(false);
                gameObject.SetActive(false);

                return;
            }

            GameStat.SecondCheckpointFill =
            Countdown.fillAmount = countdownTime /
            (START_TIME - GameMenu.Difficulty);
            Countdown.color = new Color(
                1 - GameStat.SecondCheckpointFill,
                GameStat.SecondCheckpointFill,
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
        SecondGate.SetActive(false);
        GameStat.SetSecondCheckpointStatus(true);
        GameStat.GameScore += SCORE_VALUE;
    }
}

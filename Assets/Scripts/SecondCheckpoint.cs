using UnityEngine;

public class SecondCheckpoint : MonoBehaviour
{
    private const float START_TIME = 10;
    private byte SCORE_VALUE = 15;
    
    public static bool IsActivated;

    [SerializeField]
    private UnityEngine.UI.Image Countdown;
    
    private float countdownTime;
    private GameObject SecondGate;

    void Start()
    {
        SecondCheckpoint.IsActivated = false;
        countdownTime = START_TIME;
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
            Countdown.fillAmount = countdownTime / START_TIME;
            Countdown.color = new Color(
                1 - GameStat.SecondCheckpointFill,
                GameStat.SecondCheckpointFill,
                .1f
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SecondGate.SetActive(false);
        GameStat.SetSecondCheckpointStatus(true);
        GameStat.GameScore += SCORE_VALUE;
    }
}

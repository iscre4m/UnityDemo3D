using UnityEngine;

public class FirstCheckpoint : MonoBehaviour
{
    private const float START_TIME = 5;
    private byte SCORE_VALUE = 5;

    [SerializeField]
    private UnityEngine.UI.Image Countdown;
    
    private float countdownTime;
    private GameObject FirstGate;

    void Start()
    {
        countdownTime = START_TIME;
        FirstGate = GameObject.Find(nameof(FirstGate));
    }

    void Update()
    {
        countdownTime -= Time.deltaTime;

        if (countdownTime < 0)
        {
            GameStat.SetFirstCheckpointStatus(false);
            gameObject.SetActive(false);

            return;
        }

        GameStat.FirstCheckpointFill =
        Countdown.fillAmount = countdownTime / START_TIME;
        Countdown.color = new Color(
            1 - GameStat.FirstCheckpointFill,
            GameStat.FirstCheckpointFill,
            .1f
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        FirstGate.SetActive(false);
        GameStat.SetFirstCheckpointStatus(true);
        GameStat.GameScore += SCORE_VALUE;
    }
}

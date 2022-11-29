using UnityEngine;

public class SecondCheckpoint : MonoBehaviour
{
    public static bool IsActivated;

    [SerializeField]
    private UnityEngine.UI.Image countdown;
    private const float START_TIME = 25;
    private float countdownTime;
    private GameObject secondGate;

    void Start()
    {
        SecondCheckpoint.IsActivated = false;
        countdownTime = START_TIME;
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
            countdown.fillAmount = countdownTime / START_TIME;
            countdown.color = new Color(
                1 - GameStat.SecondCheckpointFill,
                GameStat.SecondCheckpointFill,
                .1f
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameStat.SetSecondCheckpointStatus(true);
    }
}

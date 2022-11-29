using UnityEngine;

public class SecondCheckpoint : MonoBehaviour
{
    private const float START_TIME = 10;
    
    public static bool IsActivated;

    [SerializeField]
    private UnityEngine.UI.Image countdown;
    
    private float countdownTime;
    private GameObject secondGate;
    private byte scoreValue = 15;

    void Start()
    {
        SecondCheckpoint.IsActivated = false;
        countdownTime = START_TIME;
        secondGate = GameObject.Find("SecondGate");
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
        secondGate.SetActive(false);
        GameStat.SetSecondCheckpointStatus(true);
        GameStat.GameScore += scoreValue;
    }
}

using UnityEngine;

public class FirstCheckpoint : MonoBehaviour
{
    private const float START_TIME = 5;

    [SerializeField]
    private UnityEngine.UI.Image countdown;
    
    private float countdownTime;
    private GameObject firstGate;
    private byte scoreValue = 5;

    void Start()
    {
        countdownTime = START_TIME;
        firstGate = GameObject.Find("FirstGate");
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
        countdown.fillAmount = countdownTime / START_TIME;
        countdown.color = new Color(
            1 - GameStat.FirstCheckpointFill,
            GameStat.FirstCheckpointFill,
            .1f
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        firstGate.SetActive(false);
        GameStat.SetFirstCheckpointStatus(true);
        GameStat.GameScore += scoreValue;
    }
}

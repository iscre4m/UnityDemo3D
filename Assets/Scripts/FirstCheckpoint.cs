using UnityEngine;

public class FirstCheckpoint : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image countdown;
    private const float START_TIME = 5;
    private float countdownTime;
    private GameObject firstGate;

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
    }
}

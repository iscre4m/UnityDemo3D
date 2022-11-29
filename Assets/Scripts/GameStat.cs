using UnityEngine;

public class GameStat : MonoBehaviour
{
    #region Clock & GameTime
    [SerializeField]
    private static TMPro.TextMeshProUGUI Clock;

    private static float _gameTime;

    public static float GameTime
    {
        get => _gameTime;
        set
        {
            _gameTime = value;
            UpdateTime();
        }
    }
    #endregion

    #region FirstCheckpoint
    private static UnityEngine.UI.Image FirstCheckpointImage;
    private static float _firstCheckpointFill;

    public static float FirstCheckpointFill
    {
        get => _firstCheckpointFill;
        set
        {
            _firstCheckpointFill = value;
            UpdateFirstCheckpoint();
        }
    }
    #endregion

    #region SecondCheckpoint
    private static UnityEngine.UI.Image SecondCheckpointImage;
    private static float _secondCheckpointFill;

    public static float SecondCheckpointFill
    {
        get => _secondCheckpointFill;
        set
        {
            _secondCheckpointFill = value;
            UpdateSecondCheckpoint();
        }
    }
    #endregion

    void Start()
    {
        GameStat.Clock = GameObject.Find("Clock")
        .GetComponentInChildren<TMPro.TextMeshProUGUI>();
        GameStat.FirstCheckpointImage = 
        GameObject
            .Find(nameof(FirstCheckpointImage))
            .GetComponent<UnityEngine.UI.Image>();
        GameStat.SecondCheckpointImage = 
        GameObject
            .Find(nameof(SecondCheckpointImage))
            .GetComponent<UnityEngine.UI.Image>();
    }

    void LateUpdate()
    {
        GameTime += Time.deltaTime;
    }

    private static void UpdateTime()
    {
        Clock.text = $"{(int)_gameTime / 60:00}:{_gameTime % 60:00.0}";
    }

    private static void UpdateFirstCheckpoint()
    {
        if (FirstCheckpointFill >= 0 && FirstCheckpointFill <= 1)
        {
            FirstCheckpointImage.fillAmount = FirstCheckpointFill;
            FirstCheckpointImage.color = new Color(
                1 - FirstCheckpointFill,
                FirstCheckpointFill,
                .1f
            );
        }
    }

    private static void UpdateSecondCheckpoint()
    {
        if (SecondCheckpointFill >= 0 && SecondCheckpointFill <= 1)
        {
            SecondCheckpointImage.fillAmount = SecondCheckpointFill;
            SecondCheckpointImage.color = new Color(
                1 - SecondCheckpointFill,
                SecondCheckpointFill,
                .1f
            );
        }
    }

    public static void SetFirstCheckpointStatus(bool status)
    {
        FirstCheckpointFill = 1;
        FirstCheckpointImage.color = status ? Color.green : Color.red;
    }

    public static void SetSecondCheckpointStatus(bool status)
    {
        SecondCheckpointFill = 1;
        SecondCheckpointImage.color = status ? Color.green : Color.red;
    }
}

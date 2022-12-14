using System;
using System.Text;
using UnityEngine;

public class GameStat : MonoBehaviour
{
    #region Stats
    [SerializeField]
    private static TMPro.TextMeshProUGUI Clock;
    [SerializeField]
    private static TMPro.TextMeshProUGUI Score;

    private static float _gameTime;
    private static byte _gameScore;

    public static float GameTime
    {
        get => _gameTime;
        set
        {
            _gameTime = value;
            UpdateTime();
        }
    }

    public static byte GameScore
    {
        get => _gameScore;
        set
        {
            _gameScore = value;
            UpdateScore();
        }
    }
    #endregion

    #region FirstCheckpoint
    private static UnityEngine.UI.Image FirstCheckpointImage;
    private static float _firstCheckpointFill;
    public static float FirstCheckpointTime = -1;

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
    public static float SecondCheckpointTime = -1;

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

    #region FinalCheckpoint
    private static UnityEngine.UI.Image FinalCheckpointImage;
    private static float _finalCheckpointFill;
    public static float FinalCheckpointTime = -1;

    public static float FinalCheckpointFill
    {
        get => _finalCheckpointFill;
        set
        {
            _finalCheckpointFill = value;
            UpdateFinalCheckpoint();
        }
    }
    #endregion

    private const string recordsDataFilename = "Assets/Files/records_data.json";

    public static RecordsData Records;

    void Start()
    {
        GameStat.Clock = GameObject
            .Find("Clock")
            .GetComponentInChildren<TMPro.TextMeshProUGUI>();
        GameStat.Score = GameObject
            .Find("Score")
            .GetComponentInChildren<TMPro.TextMeshProUGUI>();
        GameStat.FirstCheckpointImage = GameObject
            .Find(nameof(FirstCheckpointImage))
            .GetComponent<UnityEngine.UI.Image>();
        GameStat.SecondCheckpointImage = GameObject
            .Find(nameof(SecondCheckpointImage))
            .GetComponent<UnityEngine.UI.Image>();
        GameStat.FinalCheckpointImage = GameObject
            .Find(nameof(FinalCheckpointImage))
            .GetComponent<UnityEngine.UI.Image>();

        Records = new RecordsData();
        LoadStats();
        GameMenu.SetRecords();
    }

    void LateUpdate()
    {
        GameTime += Time.deltaTime;
    }

    private static void UpdateTime()
    {
        Clock.text = $"{(int)_gameTime / 60:00}:{_gameTime % 60:00.0}";
    }

    private static void UpdateScore()
    {
        Score.text = $"{_gameScore}";
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

    private static void UpdateFinalCheckpoint()
    {
        if (FinalCheckpointFill >= 0 && FinalCheckpointFill <= 1)
        {
            FinalCheckpointImage.fillAmount = FinalCheckpointFill;
            FinalCheckpointImage.color = new Color(
                1 - FinalCheckpointFill,
                FinalCheckpointFill,
                .1f
            );
        }
    }

    public static void SetFirstCheckpointStatus(bool status)
    {
        FirstCheckpointTime = status ? _gameTime : 0;
        FirstCheckpointFill = 1;
        FirstCheckpointImage.color = status ? Color.green : Color.red;
    }

    public static void SetSecondCheckpointStatus(bool status)
    {
        SecondCheckpointTime = status ? _gameTime : 0;
        SecondCheckpointFill = 1;
        SecondCheckpointImage.color = status ? Color.green : Color.red;
    }

    public static void SetFinalCheckpointStatus(bool status)
    {
        FinalCheckpointTime = _gameTime;
        FinalCheckpointFill = 1;
        FinalCheckpointImage.color = status ? Color.green : Color.yellow;
    }

    public class RecordsData
    {
        public short[] Scores = { 0, 0, 0 };
        public string[] CheckpointTimes = {
            "0;0;0",
            "0;0;0",
            "0;0;0"
        };

        public override string ToString()
        {
            StringBuilder result = new();
            string[] checkpointTimes;
            for (int i = 0; i < 3; ++i)
            {
                result.Append(i == 0 ? "1st" : i == 1 ? "2nd" : "3rd");
                result.Append("\n");
                result.Append($"Score: {Records.Scores[i]}\n");
                checkpointTimes = Records.CheckpointTimes[i].Split(';');
                result.Append("1st checkpoint: ");
                result.Append(
                    checkpointTimes[0] == "0"
                    ? "failed"
                    : checkpointTimes[0]
                );
                result.Append($"\n2nd checkpoint: ");
                result.Append(
                    checkpointTimes[1] == "0"
                    ? "failed"
                    : checkpointTimes[1]
                );
                result.Append($"\n3rd checkpoint: ");
                result.Append(
                    checkpointTimes[2] == "0"
                    ? "failed"
                    : checkpointTimes[2]
                );
                if (i < 2)
                {
                    result.Append("\n\n");
                }
            }

            return result.ToString();
        }
    }

    public static void SaveStats()
    {
        for (int i = 0; i < 3; ++i)
        {
            if (_gameScore > Records.Scores[i]
            || _gameScore == Records.Scores[i]
            && GameStat.FinalCheckpointTime < Convert.ToSingle(
                Records.CheckpointTimes[i].Split(';')[2]))
            {
                switch (i)
                {
                    case 0:
                        for (int j = 0; j < 2; ++j)
                        {
                            Records.Scores[2 - j] =
                            Records.Scores[1 - j];
                            Records.CheckpointTimes[2 - j] =
                            Records.CheckpointTimes[1 - j];
                        }
                        break;
                    case 1:
                        Records.Scores[2] = Records.Scores[1];
                        Records.CheckpointTimes[2] =
                        Records.CheckpointTimes[1];
                        break;
                    default:
                        break;
                }

                Records.Scores[i] = _gameScore;
                Records.CheckpointTimes[i] =
                $"{GameStat.FirstCheckpointTime};" +
                $"{GameStat.SecondCheckpointTime};" +
                $"{GameStat.FinalCheckpointTime}";
                break;
            }
        }

        System.IO.File.WriteAllText(
            recordsDataFilename,
            JsonUtility.ToJson(Records, true)
        );
    }

    private static void LoadStats()
    {
        if (System.IO.File.Exists(recordsDataFilename))
        {
            Records = JsonUtility.FromJson<RecordsData>(
                System.IO.File.ReadAllText(recordsDataFilename)
            );
        }
    }
}

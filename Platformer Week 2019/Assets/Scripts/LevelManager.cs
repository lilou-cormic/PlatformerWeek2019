using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int CurrentLevelIndex = 0;

    private static LevelDef[] Levels;

    [SerializeField]
    private GameObject PlatformPrefab = null;

    [SerializeField]
    private GameObject GoalPrefab = null;

    [SerializeField]
    private GameObject KeyPrefab = null;

    [SerializeField]
    private GameObject DoorPrefab = null;

    [SerializeField]
    private GameObject LightPickupPrefab = null;

    [SerializeField]
    private AudioClip LoseSound = null;

    [SerializeField]
    private TMPro.TextMeshProUGUI LevelText = null;

    [SerializeField]
    private TMPro.TextMeshProUGUI DescriptionText = null;

    private float _minY = -10;

    private void Start()
    {
        if (Levels == null)
            Levels = Resources.LoadAll<LevelDef>("Levels").OrderBy(x => x.Number).ToArray();

        LoadLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Retry();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Skip();
        }

        if (Player.GetY() < _minY)
        {
            Lose();
        }
    }

    private void Lose()
    {
        SoundPlayer.Play(LoseSound);

        Retry();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Skip()
    {
        Win();
    }

    private void LoadLevel()
    {
        Key.Keys = 0;
        Player.SetLightActive(true);

        LevelDef level = Levels[CurrentLevelIndex];

        if (LevelText != null)
            LevelText.text = $"{level.Number}/{Levels.Length}";

        if (DescriptionText != null)
            DescriptionText.text = level.DescriptionText;

        string layout = level.LevelLayout;

        int row = 0;

        foreach (var line in layout.Split(new string[] { System.Environment.NewLine }, System.StringSplitOptions.None))
        {
            int col = 0;

            foreach (var letter in line)
            {
                switch (letter)
                {
                    case 'P':
                        Player.SetPosition(new Vector3(col, -row, 0));
                        break;

                    case '-':
                        Instantiate(PlatformPrefab, new Vector3(col, -row, 0), Quaternion.identity, transform);
                        break;

                    case 'G':
                        Instantiate(GoalPrefab, new Vector3(col, -row, 0), Quaternion.identity, transform);
                        break;

                    case 'K':
                        Instantiate(KeyPrefab, new Vector3(col, -row, 0), Quaternion.identity, transform);
                        break;

                    case '|':
                        Instantiate(DoorPrefab, new Vector3(col, -row, 0), Quaternion.identity, transform);
                        break;

                    case 'O':
                        Instantiate(LightPickupPrefab, new Vector3(col, -row, 0), Quaternion.identity, transform);
                        Player.SetLightActive(false);
                        break;

                    default:
                        break;
                }

                col++;
            }

            row++;
        }

        _minY = -row - 2;
    }

    public static void Win()
    {
        if (CurrentLevelIndex == Levels.Length - 1)
        {
            CurrentLevelIndex = 0;
            SceneManager.LoadScene("Win");
            return;
        }

        CurrentLevelIndex++;
        SceneManager.LoadScene("Main");
    }
}

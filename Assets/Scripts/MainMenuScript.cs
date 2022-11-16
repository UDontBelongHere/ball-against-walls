using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    static readonly string[] difficlutyNames = { "Easy", "Medium", "Hard" };
    static readonly float[] difficlutySpeed = { 1f, 3f, 6f};
    [SerializeField] TextMeshProUGUI difText;
    [SerializeField] TextMeshProUGUI recordText;
    [SerializeField] TextMeshProUGUI attemptsText;
    [SerializeField] Slider difSlider;

    [SerializeField] GameObject startViewObjects;
    [SerializeField] GameObject restartViewObjects;

    float[] records = new float[3];
    int attempts = 0;
    int currentDifficult = 0;

    void Start()
    {
        LoadData();

        GameController.instance.onGameEnd += ChangeRecordData;

        attemptsText.text = "Ваши попытки\n" + attempts.ToString();
        //recordText.text = "Your record\n" + string.Format("{0:0.##}", records[currentDifficult]);
    }

    void LoadData()
    {
        StoredData data = SaveSystem.LoadGameData();
        data.storedRecords.CopyTo(records, 0);
        attempts = data.storedAttempts;
    }

    void SaveData()
    {
        StoredData data = new StoredData(records, attempts);
        SaveSystem.SaveGameData(data);
    }

    public void ChangeDifficult()
    {
        currentDifficult = (int)difSlider.value;
        difText.text = difficlutyNames[currentDifficult];
        //recordText.text = "Your record\n" + string.Format("{0:0.##}", records[currentDifficult]);
    }

    void ChangeRecordData()
    {
        ChangeToRestartView();
        gameObject.GetComponent<Canvas>().enabled = true;

        float time = GameController.instance.gameTime;
        attempts += 1;
        attemptsText.text = "Ваши попытки\n" + attempts.ToString();
        records[currentDifficult] = Mathf.Max(records[currentDifficult], time);
        //recordText.text = "Your record\n" + string.Format("{0:0.##}", records[currentDifficult]);
        recordText.text = "Ваше время\n" + string.Format("{0:0.##}", time);

        SaveData();
    }

    void ChangeToRestartView() //yep, not so good
    {
        startViewObjects.SetActive(false); 
        restartViewObjects.SetActive(true);
    }

    private void OnDestroy()
    {
        GameController.instance.onGameEnd -= ChangeRecordData;
    }

    public void StartGame()
    {
        float speed = difficlutySpeed[currentDifficult];
        gameObject.GetComponent<Canvas>().enabled = false; 
        GameController.instance.onGameStart(speed);
    }
    
    public void DifficultyButtonPress()
    {
        currentDifficult = (currentDifficult + 1) % 3;
        difText.text = difficlutyNames[currentDifficult];
    }
}

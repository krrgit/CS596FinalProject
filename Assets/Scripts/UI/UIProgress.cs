using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIProgress : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private Health playerHealth;
    [Header("UI Elements")]
    [SerializeField] private Image dayArrow;
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TMP_Text healthText;
    [Header("Screens")]
    [SerializeField] private GameObject finishedLevelUI;
    [SerializeField] private GameObject lostLevelUI;

    private Vector3 arrowRotation;
    private int currentDay;
    private int totalDays;

    private void OnEnable()
    {
        waveSpawner.NewDayEvent += UpdateUI;
        levelManager.finishedLevelEvent += DisplayFinishedLevelUI;
        levelManager.lostLevelEvent += DislayLostLevelUI;
        playerHealth.OnDamageEvent += UpdateHealth;
    }
    
    private void OnDisable()
    {
        waveSpawner.NewDayEvent -= UpdateUI;
        levelManager.finishedLevelEvent -= DisplayFinishedLevelUI;
        levelManager.lostLevelEvent -= DislayLostLevelUI;
        playerHealth.OnDamageEvent -= UpdateHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        finishedLevelUI.SetActive(false);
        lostLevelUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDayArrow();
        UpdateLevelProgress();

    }

    void UpdateUI(int _currentDay, int _totalDays)
    {
        dayText.text = _currentDay.ToString();
        
        currentDay = _currentDay-1;
        totalDays = _totalDays-1;
    }

    void UpdateDayArrow()
    {
        arrowRotation.z = (waveSpawner.DayPercent * -360f) - 90f;
        dayArrow.transform.localRotation = Quaternion.Euler(arrowRotation);
    }

    void UpdateLevelProgress()
    {
        progressSlider.value = ((float)currentDay / totalDays) + (waveSpawner.DayPercent/ totalDays);
    }

    void UpdateHealth(float health)
    {
        healthText.text = ((int)health).ToString() + "/" + playerHealth.startHealth;
    }

    void DisplayFinishedLevelUI()
    {
        finishedLevelUI.SetActive(true);   
    }

    void DislayLostLevelUI()
    {
        lostLevelUI.SetActive(true);
    }
}

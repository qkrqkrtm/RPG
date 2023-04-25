using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stats_UI : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private Slider sliderHP;
    [SerializeField]
    private Text textHP;
    [SerializeField]
    private Slider sliderMP;
    [SerializeField]
    private Text textMP;
    [SerializeField]
    private Slider sliderEXP;
    [SerializeField]
    private Text textEXP;
    [SerializeField]
    private Slider sliderSP;
    [SerializeField]
    private Text textSP;

    [SerializeField]
    private Text LV;

    bool isUI_Updating;

    private void Awake()
    {
        //playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        sliderHP.value = Utils.Percent(PlayerStats.currentHP, PlayerStats.maxHP);
        sliderMP.value = Utils.Percent(PlayerStats.currentMP, PlayerStats.maxMP);
        sliderEXP.value = Utils.Percent(PlayerStats.currentLevelExp, PlayerStats.requiredExpPerLevel[PlayerStats.currentLevel - 1]);
        sliderSP.value = Utils.Percent(PlayerStats.currentStaminaPoint, PlayerStats.maxStaminaPoint);


        if(!isUI_Updating)
            StartCoroutine(UpdateUI_PerSecond());
    }

    float EXP_Percent()
    {
        if (PlayerStats.currentLevelExp == 0) return 0;
        else
        {
            return (PlayerStats.currentLevelExp * 100) / PlayerStats.requiredExpPerLevel[PlayerStats.currentLevel - 1];
        }
    }

    private double Round(float number, int point =1 )
    {

        return Math.Round(number, point);
    }

    IEnumerator UpdateUI_PerSecond()
    {
        textHP.text = $"{Round(PlayerStats.currentHP)} / {Round(PlayerStats.maxHP)}";
        textMP.text = $"{Round(PlayerStats.currentMP)} / {Round(PlayerStats.maxMP)}";
        textEXP.text = $"{PlayerStats.currentLevelExp} / {PlayerStats.requiredExpPerLevel[PlayerStats.currentLevel - 1]} ({PlayerStats.totalExp}  {EXP_Percent()}%)";
        textSP.text = $"{Round(PlayerStats.currentStaminaPoint)} / {Round(PlayerStats.maxStaminaPoint)}";

        LV.text = (PlayerStats.currentLevel).ToString();
        isUI_Updating = true;
        yield return new WaitForSeconds(1f);
        isUI_Updating = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats_Page : MonoBehaviour
{
    [SerializeField]
    Text statText1;
    [SerializeField]
    Text statText2;
    
    private void Awake()
    {
        //statText1 = GetComponent<Text>();
        //statText2 = GetComponent<Text>();
       
    }

    private void Update()
    {
        statText1.text = $"HP:{Math.Round(PlayerStats.maxHP)}\n" +
            $"MP:{Math.Round(PlayerStats.maxMP)}\n" +
            $"SP:{Math.Round(PlayerStats.maxStaminaPoint) }\n" +
            $"HP����:{Math.Round(PlayerStats.HP_RegenPerSecond, 5)}\n" +
            $"MP����:{Math.Round(PlayerStats.MP_RegenPerSecond, 5)}\n" +
            $"SP����:{Math.Round(PlayerStats.SP_RegenPerSecond, 5)}";

        statText2.text = $"���ݷ�:{PlayerStats.attack }(+{PlayerStats.weaponAttack})\n" +
            $"����:{PlayerStats.defence}(+{PlayerStats.armorDefence})";
    }

    public void ON_OFF()
    {
        if (gameObject.activeSelf) gameObject.SetActive(false);
        else gameObject.SetActive(true);
    }
    private double Round(float number, int point = 5)
    {

        return Math.Round(number, point); 
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostEnergyUI : MonoBehaviour
{

    [SerializeField]
    Slider slider;

    NewMovement player;

    float energy; //에너지

    void Start()
    {
        slider = GameObject.FindGameObjectWithTag("boostenergy").GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<NewMovement>();
    }

    
    void Update()
    {
        energy = player.boostenergy / player.boostenergy_full; // 연료값 최대밸류 1로 맞춰주기 위한 것
        slider.value = energy;// 슬라이더에 반영

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterUI : MonoBehaviour
{
    [SerializeField] private Monster monster;

    [SerializeField] private Canvas canvas;
    [SerializeField] private Slider hpBar;

    [SerializeField] private GameObject damageText;
    [SerializeField] private Transform damagePos;
    
    private void Awake()
    {
        canvas.worldCamera = Camera.main;
        monster.updateHpUI += UpdateHpUI;
        monster.inactiveHpUI += InactiveHpUI;
        monster.updateDamageUI += UpdateDamageUI;
    }

    private void Update()
    {
        hpBar.transform.rotation = Camera.main.transform.rotation;
    }

    private void UpdateDamageUI(float damage)
    {
        DamageUI damageUI = Instantiate(damageText, damagePos).GetComponent<DamageUI>();
        damageUI.damage = damage;
    }

    private void UpdateHpUI(float curHp, float maxHp)
    {
        hpBar.value = curHp / maxHp;
    }

    private void InactiveHpUI()
    {
        hpBar.gameObject.SetActive(false);
    }
}

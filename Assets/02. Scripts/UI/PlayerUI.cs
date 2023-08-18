using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider expBar;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text expText;
    [SerializeField] private TMP_Text levelText;

    private void Awake()
    {
        player.updateHpUI += UpdateHpUI;
        player.updateExpUI += UpdateExpUI;
        player.updateLevelUI += UpdateLevelUI;
    }

    private void UpdateHpUI(float curHp, float maxHp)
    {
        hpBar.value = curHp / maxHp;
        hpText.text = $"{curHp} / {maxHp}";
    }

    private void UpdateExpUI(float curExp, float levelUpExp)
    {
        expBar.value = (float)curExp / (float)levelUpExp;
        expText.text = $"{(float)curExp / (float)levelUpExp * 100}%";
    }

    private void UpdateLevelUI(float curLevel)
    {
        levelText.text = $"Lv. {curLevel.ToString()}";
    }
}

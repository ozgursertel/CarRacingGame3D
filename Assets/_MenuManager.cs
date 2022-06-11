using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject bodyShopPanel;
    [SerializeField] private GameObject paintShopPanel;
    [SerializeField] private GameObject startGamePanel,optionsPanel;
    [SerializeField] private Text coinText,shopCoinText,paintShopCoinText;
    

    public List<Sprite> voiceIcons;
    public Button voiceButton;
    public Text voiceOnOffText;

    private void Update()
    {
        coinText.text = ""+GameManager.TotalCoin;
        shopCoinText.text = "" + GameManager.TotalCoin;
        paintShopCoinText.text = "" + GameManager.TotalCoin;
        checkVoiceButtonStatus();
    }

    private void checkVoiceButtonStatus()
    {
        if (AudioManager.instance.isVoiceOn)
        {
            voiceButton.GetComponent<Image>().sprite = voiceIcons[0];
            voiceOnOffText.GetComponent<Text>().text = "ON";

        }
        if (!AudioManager.instance.isVoiceOn)
        {
            voiceButton.GetComponent<Image>().sprite = voiceIcons[1];
            voiceOnOffText.GetComponent<Text>().text = "OFF";
        }
    }

    public void clickedVoiceButton()
    {
        AudioManager.instance.Play("Click");
        if (AudioManager.instance.isVoiceOn)
        {
            AudioManager.instance.isVoiceOn = false;
            PlayerPrefs.SetInt("isVoiceOn", 0);
        }
        else
        {
            AudioManager.instance.isVoiceOn = true;
            PlayerPrefs.SetInt("isVoiceOn", 1);
        }
        AudioManager.instance.setVoice();
    }

    public void bodyShopPanelOpen()
    {
        AudioManager.instance.Play("Click");
        startGamePanel.SetActive(false);
        bodyShopPanel.SetActive(true);
    }

    public void paintShopPanelOpen()
    {
        AudioManager.instance.Play("Click");
        startGamePanel.SetActive(false);
        paintShopPanel.SetActive(true);
    }

    public void bodyShopPanelClose()
    {
        AudioManager.instance.Play("Click");
        startGamePanel.SetActive(true);
        bodyShopPanel.SetActive(false);
    }

    public void paintShopPanelClose()
    {
        AudioManager.instance.Play("Click");
        startGamePanel.SetActive(true);
        paintShopPanel.SetActive(false);
    }

    public void EarnCoinButton()
    {
        GoogleAdsManager.Instance.UserChoseToWatchAd();
        if (GoogleAdsManager.Instance.isRewardOkay)
        {
            GameManager.TotalCoin += 10;
            PlayerPrefs.SetInt("Total_Coin", GameManager.TotalCoin);
        }
    }

    public void optionsPanelOpen()
    {
        AudioManager.instance.Play("Click");
        startGamePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void optionsPanelClose()
    {
        AudioManager.instance.Play("Click");
        startGamePanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void RateUsButton()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.GloriaSoftware.FastLineInfinite2&showAllReviews=true");
    }


}

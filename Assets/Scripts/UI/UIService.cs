using System;
using TMPro;
using UnityEngine;

public class UIService : GenericMonoSingleton<UIService>
{
    [SerializeField] private TextMeshProUGUI coinBalance;
    [SerializeField] private TextMeshProUGUI gemsBalance;
    [SerializeField] private Transform popupsParent;
    [SerializeField] private OpenChestNowPopup openChestNowPopup;

    public void UpdateCoinsBalance(int newBalance){
        coinBalance.text = $"{newBalance}";
    }

    public void UpdateGemsBalance(int newBalance){
        gemsBalance.text = $"{newBalance}";
    }

    public void ShowOpenChestNowPopup(Action<bool> callback){
        var openChestNowPopupView = Instantiate<OpenChestNowPopup>(openChestNowPopup, popupsParent);
        openChestNowPopupView.SetCallback(callback);
    }
}

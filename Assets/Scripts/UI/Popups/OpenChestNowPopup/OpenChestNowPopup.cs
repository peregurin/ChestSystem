using System;
using UnityEngine;
using UnityEngine.UI;

namespace Popup.OpenChestNow{
    public class OpenChestNowPopup : MonoBehaviour
{
    [SerializeField] private Button openNowButton;
    [SerializeField] private Button closePopupButton;

    private Action<bool> buttonCallback;

    private void Start(){
        openNowButton.onClick.AddListener(OnOpenNowButtonClicked);
        closePopupButton.onClick.AddListener(OnClosePopupButtonClicked);
    }

    public void SetCallback(Action<bool> callback){
        buttonCallback = callback;
    }

    private void OnOpenNowButtonClicked(){
        buttonCallback?.Invoke(true);
        Destroy(gameObject);
    }

    private void OnClosePopupButtonClicked(){
        buttonCallback?.Invoke(false);
        Destroy(gameObject);
    }
}
}
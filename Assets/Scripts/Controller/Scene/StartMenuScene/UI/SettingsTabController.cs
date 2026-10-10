using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsTabController : MonoBehaviour
{
    //[field: SerializeField] private Button BackButton { get; set; }

    public void Init(Action close)
    {
        /*
        gameObject.SetActive(true);

        BackButton.onClick.RemoveAllListeners();
        BackButton.onClick.AddListener(() => close());
        
        foreach (var setting in Settings)
        {
            setting.Init(this);
        }
        
        Settings[0].Show();
        */
    }

    public void HideTabs()
    {
        /*
        foreach (var setting in Settings)
        {
            setting.Hide();
        }
        */
    }
}

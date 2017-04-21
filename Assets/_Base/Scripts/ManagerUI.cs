using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    [Header("Components"), SerializeField]
    private PanelBase panelMenu;
    [SerializeField]
    private PanelBase panelHUD;
    [SerializeField]
    private PanelBase panelLoading;

    void Awake()
    {
        Director.Instance.managerUI = this;
    }


    #region Panel management
    public void UpdateUI()
    {
        switch (Director.Instance.currentScene)
        {
            case Structs.GameScene.Menu:
                panelMenu.Show();
                panelHUD.Hide();
                panelLoading.Hide();
                break;

            case Structs.GameScene.Ingame:
                panelMenu.Hide();
                panelHUD.Show();
                panelLoading.Hide();
                break;

            case Structs.GameScene.LoadingGame:
                panelMenu.Hide();
                panelHUD.Hide();
                panelLoading.Show();
                break;

            default:
                panelMenu.Hide();
                panelHUD.Hide();
                panelLoading.Hide();
                break;
        }
    }
    #endregion
}

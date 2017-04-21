using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    [Header("Components"), SerializeField]
    private PanelBase panelMenu;
    [SerializeField]
    private PanelBase panelHUD;

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
                break;

            case Structs.GameScene.Ingame:
                panelMenu.Hide();
                panelHUD.Show();
                break;

            default:
                panelMenu.Hide();
                panelHUD.Hide();
                break;
        }
    }
    #endregion
}

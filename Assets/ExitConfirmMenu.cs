using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitConfirmMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text exitGame;
    [SerializeField] private TMP_Text backToMenu;
    [SerializeField] private PauseMenu pauseMenu;

    [SerializeField] private string exitGameText = "Quit";
    [SerializeField] private string returnToMenuText = "No";

    private enum MenuEntry
    {
        Exit,
        ReturnToMenu
    }

    private MenuEntry[] menuEntries = { MenuEntry.Exit, MenuEntry.ReturnToMenu };
    private int selectedMenuIdx = 0;

    void Start()
    {
        UpdateMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            UpdateMenu();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectMenu();
        }
    }

    public void ActivateMenu()
    {
        gameObject.SetActive(true);
        selectedMenuIdx = 0;
        UpdateMenu();
    }

    private void UpdateMenu()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            selectedMenuIdx--;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            selectedMenuIdx++;
        }

        selectedMenuIdx = Math.Clamp(selectedMenuIdx, 0, menuEntries.Length - 1);

        if (menuEntries[selectedMenuIdx] == MenuEntry.Exit)
        {
            exitGame.text = "> " + exitGameText;
            backToMenu.text = returnToMenuText;
        }

        if (menuEntries[selectedMenuIdx] == MenuEntry.ReturnToMenu)
        {
            backToMenu.text = "> " + returnToMenuText;
            exitGame.text = exitGameText;
        }
    }

    private void SelectMenu()
    {
        if (menuEntries[selectedMenuIdx] == MenuEntry.Exit)
        {
            Application.Quit();
        }

        if (menuEntries[selectedMenuIdx] == MenuEntry.ReturnToMenu)
        {
            pauseMenu.ActivateMenu();
            gameObject.SetActive(false);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text exitGame;
    [SerializeField] private TMP_Text returnToGame;
    [SerializeField] private PauseController pauseController;
    [SerializeField] private ExitConfirmMenu exitConfirmMenu;

    [SerializeField] private string exitGameText = "Exit Game";
    [SerializeField] private string returnToGameText = "Return";

    private enum MenuEntry
    {
        Exit,
        ReturnToGame
    }

    private MenuEntry[] menuEntries = { MenuEntry.ReturnToGame, MenuEntry.Exit };
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
            returnToGame.text = returnToGameText;
        }

        if (menuEntries[selectedMenuIdx] == MenuEntry.ReturnToGame)
        {
            returnToGame.text = "> " + returnToGameText;
            exitGame.text = exitGameText;
        }
    }

    private void SelectMenu()
    {
        if (menuEntries[selectedMenuIdx] == MenuEntry.Exit)
        {
            exitConfirmMenu.ActivateMenu();
            gameObject.SetActive(false);
        }

        if (menuEntries[selectedMenuIdx] == MenuEntry.ReturnToGame)
        {
            pauseController.Resume();
        }
    }
}
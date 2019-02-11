using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
  int level;
  enum Screen
  {
    Welcome,
    MainMenu,
    Password,
    Success,
    EasterEgg
  };
  Screen currentScreen;

  // Start is called before the first frame update
  void Start()
  {
    ShowMainMenu();
  }

  void ShowWelcomeScreen()
  {
    // Create a welcome screen w/login
  }

  void ShowMainMenu()
  {
    Terminal.ClearScreen();
    Terminal.WriteLine("What would you like to hack into?");
    Terminal.WriteLine("Press 1 for the local library");
    Terminal.WriteLine("Press 2 for the local police station");
    Terminal.WriteLine("Enter your selection:");
  }

  void OnUserInput(string input)
  {
    if (input.ToLower() == "menu")
    {
      level = 0;
      currentScreen = Screen.MainMenu;
      ShowMainMenu();
    }
    else if (currentScreen == Screen.MainMenu)
    {
      RunMainMenu(input);
    }
  }

  void RunMainMenu(string input)
  {
    switch (input)
    {
      case "1":
        level = 1;
        StartGame();
        break;
      case "2":
        level = 2;
        StartGame();
        break;
      case "007":
        level = 3;
        currentScreen = Screen.EasterEgg;
        Terminal.WriteLine("Please select a level Mr Bond!");
        break;
      default:
        Terminal.WriteLine("Please select a valid level");
        break;
    }
  }

  void StartGame()
  {
    currentScreen = Screen.Password;
    Terminal.WriteLine("You have chosen level: " + level);
    Terminal.WriteLine("Please enter your password: ");
  }
}

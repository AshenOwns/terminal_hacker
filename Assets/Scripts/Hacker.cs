using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Hacker : MonoBehaviour
{

  // GAME CONFIG ---------------------
  string[] passSchool = { "Chair", "Book", "Teacher", "Table", "Lesson", "Playground", "Headteacher", "Lunchbox", "Assistant", "Uniform" };
  string[] passGovernment = { "President", "Meeting", "Country", "Leadership", "Federal", "Authority", "Office", "Legislature" };
  string[] passGaming = { "Sony", "Nintendo", "Controller", "Microsoft", "Zelda", "Pokemon", "Mario", "Sonic", "Online", "Player", "Cooperative", "Family" };


  // GAME STATE ---------------------
  int level;
  string password;
  string userName = "unidentified user";
  enum Screen
  {
    WelcomeScreen,
    MainMenu,
    Password,
    Success,
    Restart
  };
  Screen currentScreen;

  // Start is called before the first frame update
  void Start()
  {
    ShowWelcomeScreen();
  }

  void ShowWelcomeScreen()
  {
    currentScreen = Screen.WelcomeScreen;
    Terminal.ClearScreen();
    Terminal.WriteLine(@"  _______  ___ ___   ___ _  _  ___
 |_  / _ \| _ \_ _| |_ _| \| |/ __|
  / / (_) |   /| |   | || .` | (__
 /___\___/|_|_\___| |___|_|\_|\___|
                                   ");
    Terminal.WriteLine("\n\nAuthentication required to proceed.\n");
    Terminal.WriteLine("What is your name?");
  }

  void DisplayRestartScreen()
  {
    currentScreen = Screen.Restart;
    Terminal.ClearScreen();
    Terminal.WriteLine("You thought you could get away with that, didn't you?\n\n");
    Terminal.WriteLine("Try again, type \"restart\"");
  }

  void DisplayMainMenu()
  {
    currentScreen = Screen.MainMenu;
    Terminal.ClearScreen();
    Terminal.WriteLine("Welcome, " + userName + "\n");
    Terminal.WriteLine("What would you like to access today?");
    Terminal.WriteLine("1) for the school database [easy]");
    Terminal.WriteLine("2) for the gaming database [medium]");
    Terminal.WriteLine("3) for the government database [hard]");
    Terminal.WriteLine("Enter your selection:");
  }

  void GetUserName(string input)
  {
    Regex regex = new Regex(@"^[a-zA-Z]+$");
    Match isMatch = regex.Match(input);

    if (isMatch.Success)
    {
      userName = input;
      DisplayMainMenu();
    }
    else
    {
      Terminal.WriteLine("ACCESS DENIED: Username not recognized (letters only!).");
    }
  }

  void OnUserInput(string input)
  {
    if (input.ToLower() == "menu")
    {
      if (currentScreen == Screen.WelcomeScreen)
      {
        DisplayRestartScreen();
      }
      else
      {
        level = 0;
        password = "";
        currentScreen = Screen.MainMenu;
        DisplayMainMenu();
      }
    }
    else if (input.ToLower() == "quit")
    {
      Application.Quit();
    }
    else if (currentScreen == Screen.Restart)
    {
      if (input.ToLower() == "restart")
      {
        ShowWelcomeScreen();
      }
    }
    else if (currentScreen == Screen.WelcomeScreen)
    {
      GetUserName(input);
    }
    else if (currentScreen == Screen.MainMenu)
    {
      RunMainMenu(input);
    }
    else if (currentScreen == Screen.Password)
    {
      ComparePassword(input);
    }
  }

  void RunMainMenu(string input)
  {
    bool isValidLevel = (input == "1" || input == "2" || input == "3");

    if (isValidLevel)
    {
      level = int.Parse(input);
      DisplayPasswordScreen();
    }
    else
    {
      Terminal.WriteLine("Please choose a valid database");
    }
  }

  void DisplayPasswordScreen()
  {
    currentScreen = Screen.Password;
    Terminal.ClearScreen();
    RequestPassword();
    Terminal.WriteLine("Please enter the password: ");
  }

  void RequestPassword()
  {
    switch (level)
    {
      case 1:
        int i = Random.Range(0, passSchool.Length);
        password = passSchool[i].ToLower();
        Terminal.WriteLine("Uh oh! It looks like a child typed the password.\n");
        Terminal.WriteLine("Hint: " + password.Anagram());
        break;
      case 2:
        int y = Random.Range(0, passGaming.Length);
        password = passGaming[y].ToLower();
        Terminal.WriteLine("It's dangerous to hack alone! Guess this.");
        Terminal.WriteLine("Hint: " + password.Anagram());
        break;
      case 3:
        int x = Random.Range(0, passGovernment.Length);
        password = passGovernment[x].ToLower();
        Terminal.WriteLine("Oh no! It looks like someone \"encrypted\" the password.\n");
        Terminal.WriteLine("Hint: " + password.Anagram());
        break;
      default:
        Debug.LogError("Invalid level number");
        break;
    }
  }

  void ComparePassword(string input)
  {
    if (input.ToLower() == password.ToLower())
    {
      ShowSuccessScreen();
    }
    else
    {
      DisplayPasswordScreen();
    }
  }

  void ShowSuccessScreen()
  {
    currentScreen = Screen.Success;
    Terminal.ClearScreen();
    ShowLevelReward();
  }

  void ShowLevelReward()
  {
    const string menuHint = "\n\nTo play again, type \"menu\"";

    switch (level)
    {
      case 1:
        Terminal.WriteLine("You cracked the password! You could say it was... Child's play!\n");
        Terminal.WriteLine(@"");
        Terminal.WriteLine(menuHint);
        break;
      case 2:
        Terminal.WriteLine("You're a professional at this, aren't you?\n");
        Terminal.WriteLine(@"  __        ___
 / o\      /o o\
|   <      |   |
 \__/      |,,,|");
        Terminal.WriteLine(menuHint);
        break;
      case 3:
        Terminal.WriteLine("You cracked the password! But uh, let's stop before we end up in a lot of trouble...\n");
        Terminal.WriteLine(@"   (o o)   (o o)
  (  V  ) (  V  )
 /--m-m- /--m-m-");
        Terminal.WriteLine(menuHint);
        break;
      default:
        break;
    }
  }
}

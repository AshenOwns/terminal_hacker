using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Hacker : MonoBehaviour
{

  // GAME CONFIG ---------------------
  string[] passSchool = { "Chair", "Book", "Teacher", "Table", "Lesson", "Playground", "Headteacher", "Lunchbox", "Assistant", "Uniform" };
  string[] passGovernment = { "President", "Meeting", "Country", "Leadership", "Federal", "Authority", "Office", "Legislature" };

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
    EasterEgg
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

  void ShowMainMenu()
  {
    currentScreen = Screen.MainMenu;
    Terminal.ClearScreen();
    Terminal.WriteLine("Welcome, " + userName + "\n");
    Terminal.WriteLine("What would you like to access today?");
    Terminal.WriteLine("Press 1 for the school database");
    Terminal.WriteLine("Press 2 for the government");
    Terminal.WriteLine("Enter your selection:");
  }

  void GetUserName(string input)
  {
    Regex regex = new Regex(@"^[a-zA-Z]+$");
    Match match = regex.Match(input);

    if (match.Success)
    {
      userName = input;
      ShowMainMenu();
    }
    else
    {
      Terminal.WriteLine("ACCESS DENIED: Username not recognised (letters only!).");
    }
  }

  void OnUserInput(string input)
  {
    if (input.ToLower() == "menu")
    {
      level = 0;
      password = "";
      currentScreen = Screen.MainMenu;
      ShowMainMenu();
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
    bool isValidLevel = (input == "1" || input == "2");

    if (isValidLevel)
    {
      level = int.Parse(input);
      StartGame(input);
    }
    else if (input == "007")
    {
      Terminal.WriteLine("Please select a level Mr Bond!");
    }
    else
    {
      Terminal.WriteLine("Please choose a valid database");
    }
  }

  void StartGame(string input)
  {
    currentScreen = Screen.Password;
    Terminal.ClearScreen();
    switch (level)
    {
      case 1:
        int i = Random.Range(0, passSchool.Length);
        password = passSchool[i].ToLower();
        Terminal.WriteLine("Uh oh! It looks like a child typed the password.\n");
        Terminal.WriteLine("Hint: " + password.Anagram());
        break;
      case 2:
        int x = Random.Range(0, passGovernment.Length);
        password = passGovernment[x].ToLower();
        Terminal.WriteLine("Oh no! It looks like someone \"encrypted\" the password.\n");
        Terminal.WriteLine("Hint: " + password.Anagram()); ;
        break;
      default:
        Debug.LogError("Invalid level number");
        break;
    }
    Terminal.WriteLine("Please enter the password: ");
  }

  void ComparePassword(string input)
  {
    if (input.ToLower() == password.ToLower())
    {
      ShowSuccessScreen();
    }
    else
    {
      Terminal.WriteLine("Incorrect");
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
    switch (level)
    {
      case 1:
        Terminal.WriteLine("You cracked the password! You could say it was... Childs play!\n");
        Terminal.WriteLine(@"");
        Terminal.WriteLine("\n\nTo play again, type \"menu\"");
        break;
      case 2:
        Terminal.WriteLine("You cracked the password! But uh, let's stop before we end up in a lot of trouble...\n");
        Terminal.WriteLine(@"   (o o)   (o o)
  (  V  ) (  V  )
 /--m-m- /--m-m-");
        Terminal.WriteLine("\n\nTo play again, type \"menu\"");
        break;
      default:
        break;
    }
  }
}

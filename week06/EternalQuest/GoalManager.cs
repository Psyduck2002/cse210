
using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;
    private List<string> _purchasedRewards;

    private string[] _rewardNames =
    {
        "Treat yourself to ice cream",
        "30 extra minutes of free time",
        "Buy your favorite snack",
        "Watch a movie",
        "Take a guilt-free gaming break"
    };

    private int[] _rewardPrices =
    {
        100, 200, 150, 300, 500
    };

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
        _purchasedRewards = new List<string>();
    }

    public void Start()
    {
        bool running = true;

        while (running)
        {
            DisplayPlayerInfo();

            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Reward Shop");
            Console.WriteLine("  7. View Purchased Rewards");
            Console.WriteLine("  8. Quit");

            int choice = GetNumber("Select a choice from the menu: ");

            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    CreateGoal();
                    break;

                case 2:
                    ListGoalDetails();
                    break;

                case 3:
                    SaveGoals();
                    break;

                case 4:
                    LoadGoals();
                    break;

                case 5:
                    RecordEvent();
                    break;

                case 6:
                    RewardShop();
                    break;

                case 7:
                    ViewPurchasedRewards();
                    break;

                case 8:
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        Console.WriteLine("Thanks for playing Eternal Quest!");
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine();
        Console.WriteLine($"You have {_score} points.");
    }

    public void ListGoalNames()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetName()}");
        }
    }

    public void ListGoalDetails()
    {
        Console.WriteLine("The goals are:");

        if (_goals.Count == 0)
        {
            Console.WriteLine("You have no goals yet.");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine(
                $"{i + 1}. {_goals[i].GetDetailsString()}"
            );
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("The types of goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");

        int type = GetNumber("Which type of goal would you like to create? ");

        if (type < 1 || type > 3)
        {
            Console.WriteLine("Invalid goal type.");
            return;
        }

        Console.Write("What is the name of your goal? ");
        string name = (Console.ReadLine() ?? "").Replace("|", "-");

        Console.Write("What is a short description of it? ");
        string description = (Console.ReadLine() ?? "").Replace("|", "-");

        int points = GetNumber("What is the amount of points associated with this goal? ");

        if (points <= 0)
        {
            Console.WriteLine("Points must be greater than zero.");
            return;
        }

        if (type == 1)
        {
            SimpleGoal goal = new SimpleGoal(name, description, points);
            _goals.Add(goal);
        }
        else if (type == 2)
        {
            EternalGoal goal = new EternalGoal(name, description, points);
            _goals.Add(goal);
        }
        else if (type == 3)
        {
            int target = GetNumber("How many times does this goal need to be accomplished? ");
            int bonus = GetNumber("What is the bonus for accomplishing it that many times? ");

            if (target <= 0 || bonus < 0)
            {
                Console.WriteLine("Invalid target or bonus.");
                return;
            }

            ChecklistGoal goal = new ChecklistGoal(
                name, description, points, target, bonus
            );

            _goals.Add(goal);
        }

        Console.WriteLine("Your goal has been created!");
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You don't have any goals to record.");
            return;
        }

        Console.WriteLine("The goals are:");
        ListGoalNames();

        int choice = GetNumber("Which goal did you accomplish? ");

        if (choice < 1 || choice > _goals.Count)
        {
            Console.WriteLine("Invalid goal selection.");
            return;
        }

        Goal goal = _goals[choice - 1];

        if (goal.IsComplete())
        {
            Console.WriteLine("This goal is already completed!");
            return;
        }

        int pointsEarned = goal.RecordEvent();

        _score += pointsEarned;

        Console.WriteLine();
        Console.WriteLine($"Congratulations! You have earned {pointsEarned} points!");
        Console.WriteLine($"You now have {_score} points.");
    }

    public void SaveGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine() ?? "";

        try
        {
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                outputFile.WriteLine($"Score|{_score}");

                foreach (Goal goal in _goals)
                {
                    outputFile.WriteLine(goal.GetStringRepresentation());
                }

                foreach (string reward in _purchasedRewards)
                {
                    outputFile.WriteLine($"Reward|{reward}");
                }
            }

            Console.WriteLine("Goals saved successfully!");
        }
        catch (Exception)
        {
            Console.WriteLine("There was an error saving the file.");
        }
    }

    public void LoadGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine() ?? "";

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);

            List<Goal> loadedGoals = new List<Goal>();
            List<string> loadedRewards = new List<string>();
            int loadedScore = 0;

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');

                if (parts[0] == "Score")
                {
                    loadedScore = int.Parse(parts[1]);
                }
                else if (parts[0] == "SimpleGoal")
                {
                    string name = parts[1];
                    string description = parts[2];
                    int points = int.Parse(parts[3]);
                    bool isComplete = bool.Parse(parts[4]);

                    SimpleGoal goal = new SimpleGoal(
                        name, description, points, isComplete
                    );

                    loadedGoals.Add(goal);
                }
                else if (parts[0] == "EternalGoal")
                {
                    string name = parts[1];
                    string description = parts[2];
                    int points = int.Parse(parts[3]);

                    EternalGoal goal = new EternalGoal(
                        name, description, points
                    );

                    loadedGoals.Add(goal);
                }
                else if (parts[0] == "ChecklistGoal")
                {
                    string name = parts[1];
                    string description = parts[2];
                    int points = int.Parse(parts[3]);
                    int target = int.Parse(parts[4]);
                    int bonus = int.Parse(parts[5]);
                    int amountCompleted = int.Parse(parts[6]);

                    ChecklistGoal goal = new ChecklistGoal(
                        name, description, points,
                        target, bonus, amountCompleted
                    );

                    loadedGoals.Add(goal);
                }
                else if (parts[0] == "Reward")
                {
                    loadedRewards.Add(parts[1]);
                }
            }

            _goals = loadedGoals;
            _purchasedRewards = loadedRewards;
            _score = loadedScore;

            Console.WriteLine("Goals loaded successfully!");
        }
        catch (Exception)
        {
            Console.WriteLine("There was an error loading the file.");
        }
    }

    // REWARD SHOP - EXCEEDING REQUIREMENTS

    public void RewardShop()
    {
        bool shopping = true;

        while (shopping)
        {
            Console.WriteLine();
            Console.WriteLine("===== REWARD SHOP =====");
            Console.WriteLine($"Your Points: {_score}");
            Console.WriteLine();

            for (int i = 0; i < _rewardNames.Length; i++)
            {
                Console.WriteLine(
                    $"{i + 1}. {_rewardNames[i]} - {_rewardPrices[i]} points"
                );
            }

            Console.WriteLine("0. Leave Shop");

            int choice = GetNumber("What would you like to purchase? ");

            if (choice == 0)
            {
                shopping = false;
            }
            else if (choice >= 1 && choice <= _rewardNames.Length)
            {
                int index = choice - 1;
                int price = _rewardPrices[index];
                string reward = _rewardNames[index];

                if (_score >= price)
                {
                    _score -= price;
                    _purchasedRewards.Add(reward);

                    Console.WriteLine();
                    Console.WriteLine($"You purchased: {reward}!");
                    Console.WriteLine($"You have {_score} points remaining.");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("You don't have enough points!");
                    Console.WriteLine($"You need {price - _score} more points.");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }
    }

    public void ViewPurchasedRewards()
    {
        Console.WriteLine("===== PURCHASED REWARDS =====");

        if (_purchasedRewards.Count == 0)
        {
            Console.WriteLine("You haven't purchased any rewards yet.");
            return;
        }

        for (int i = 0; i < _purchasedRewards.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_purchasedRewards[i]}");
        }
    }

    private int GetNumber(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out int number))
            {
                return number;
            }

            Console.WriteLine("Please enter a valid number.");
        }
    }
}

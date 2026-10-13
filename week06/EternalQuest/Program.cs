
using System;

class Program
{
    static void Main(string[] args)
    {
        // EXCEEDING REQUIREMENTS:
        // I added a Reward Shop where users can spend their earned
        // points on real-life rewards, such as getting ice cream,
        // having extra free time, or taking a gaming break.
        //
        // The program checks if the user has enough points before
        // allowing them to purchase a reward.
        //
        // The program also keeps track of purchased rewards
        // and saves them along with the goals and score.

        GoalManager manager = new GoalManager();
        manager.Start();
    }
}

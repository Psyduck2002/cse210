
using System;

public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points,
        int target, int bonus)
        : base(name, description, points)
    {
        _amountCompleted = 0;
        _target = target;
        _bonus = bonus;
    }

    public ChecklistGoal(string name, string description, int points,
        int target, int bonus, int amountCompleted)
        : base(name, description, points)
    {
        _amountCompleted = amountCompleted;
        _target = target;
        _bonus = bonus;
    }

    public override int RecordEvent()
    {
        if (IsComplete())
        {
            return 0;
        }

        _amountCompleted++;

        int earnedPoints = _points;

        if (_amountCompleted == _target)
        {
            earnedPoints += _bonus;
        }

        return earnedPoints;
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetDetailsString()
    {
        string details = base.GetDetailsString();

        return $"{details} -- Completed {_amountCompleted}/{_target} times";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{_shortName}|{_description}|{_points}|{_target}|{_bonus}|{_amountCompleted}";
    }
}

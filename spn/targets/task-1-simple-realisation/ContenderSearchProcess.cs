﻿using spn_models.targets.common.models;
using spn_models.targets.common.models.interfaces;
using spn_models.targets.common.util;

namespace spn.targets.task_1_simple_realisation;

public class ContenderSearchProcess
{
    private readonly IHall _hall;
    private readonly IPrincess _princess;
    private readonly IContendersFactory _contendersFactory;
    private readonly int _startContendersNumber;
    private readonly List<RatedContender> _visitedContenders;

    public ContenderSearchProcess(IHall hall, IPrincess princess, IContendersFactory contendersFactory,
        int startContendersNumber)
    {
        _hall = hall;
        _princess = princess;
        _contendersFactory = contendersFactory;
        _startContendersNumber = startContendersNumber;
        _visitedContenders = new List<RatedContender>();
    }

    public void SearchLove()
    {
        InitHall();

        RatedContender? love = null;

        while (!_hall.IsNoContendersInHall())
        {
            RatedContender currentContender = _hall.ReturnNextContender();
            var response = _princess.CheckContender(currentContender);
            _visitedContenders.Add(currentContender);

            if (response.Contender != null)
            {
                love = response.Contender;
                break;
            }
        }

        PrintResults(_princess.CheckChoice(love));
    }

    private void PrintResults(int choiceResult)
    {
        using StreamWriter file = new("result.txt");
        for (int i = 0; i < _visitedContenders.Count; i++)
        {
            file.WriteLineAsync($"{i + 1}: {_visitedContenders[i].GetDetails()}");
        }

        file.WriteLineAsync("--------------------");
        file.WriteLineAsync($"Choice result: {choiceResult}");
    }

    private void InitHall()
    {
        _contendersFactory
            .CreateRatedContenders(ContenderFactory.SimpleGeneration, _startContendersNumber)
            .ForEach((contender) => _hall.AddNewContender(contender));
    }
}
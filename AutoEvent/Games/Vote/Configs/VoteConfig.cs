﻿using System.Collections.Generic;
using System.ComponentModel;
using AutoEvent.API;
using AutoEvent.Interfaces;

namespace AutoEvent.Games.Vote;

public class VoteConfig : EventConfig
{
    [Description("The loadouts that players can get.")]
    public List<Loadout> PlayerLoadouts { get; set; } = new List<Loadout>()
    {
        new Loadout()
        {
            Items = new List<ItemType>() { ItemType.Coin}
        }
    };
}


﻿using System.Collections.Generic;
using System.ComponentModel;
using AutoEvent.API;
using AutoEvent.API.Enums;
using AutoEvent.Interfaces;
using PlayerRoles;

namespace AutoEvent.Games.Deathmatch;
public class Config : EventConfig
{
    [Description("How many total kills a team needs to win. Determined per-person at the start of the round. [Default: 3]")]
    public int KillsPerPerson { get; set; } = 3;

    [Description("A list of loadouts for team Chaos Insurgency")]
    public List<Loadout> ChaosLoadouts = new List<Loadout>()
    {
        new Loadout()
        {
            Roles = new Dictionary<RoleTypeId, int>(){ { RoleTypeId.ChaosRifleman, 100 } },
            Items = new List<ItemType>() { ItemType.ArmorCombat, ItemType.Medkit, ItemType.Painkillers },
            InfiniteAmmo = AmmoMode.InfiniteAmmo,
            Effects = new List<Effect>()
            {
                new Effect()
                {
                    EffectType = StatusEffect.MovementBoost,
                    Intensity = 10,
                    Duration = 0,
                },
                new Effect()
                {
                    EffectType = StatusEffect.Scp1853,
                    Intensity = 1,
                    Duration = 0,
                }
            }
        }
    };
    
    [Description("A list of loadouts for team NTF")]
    public List<Loadout> NTFLoadouts = new List<Loadout>()
    {
        new Loadout()
        {
            Roles = new Dictionary<RoleTypeId, int>(){ { RoleTypeId.NtfSpecialist, 100 } },
            Items = new List<ItemType>() { ItemType.ArmorCombat, ItemType.Medkit, ItemType.Painkillers },
            InfiniteAmmo = AmmoMode.InfiniteAmmo,
            Effects = new List<Effect>()
            {
                new Effect()
                {
                    EffectType = StatusEffect.MovementBoost,
                    Intensity = 10,
                    Duration = 0,
                },
                new Effect()
                {
                    EffectType = StatusEffect.Scp1853,
                    Intensity = 1,
                    Duration = 0,
                }
            }
        }
    };

    [Description("The weapons a player can get once the round starts.")]
    public List<ItemType> AvailableWeapons = new List<ItemType>()
    {
        ItemType.GunAK,
        ItemType.GunA7,
        ItemType.GunCrossvec,
        ItemType.GunShotgun,
        ItemType.GunE11SR
    };
}
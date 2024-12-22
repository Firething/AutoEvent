﻿using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Features;
using PlayerRoles;
using UnityEngine;
using YamlDotNet.Serialization;

namespace AutoEvent.API;

[Description("A loadout that a user can get during the event")]
public class Loadout
{
    [YamlMember(DefaultValuesHandling = DefaultValuesHandling.OmitDefaults | DefaultValuesHandling.OmitNull | DefaultValuesHandling.OmitEmptyCollections)]
    //[DefaultValue(1)]
    [Description("The chance of a user getting this class. Chance cannot be <= 0, it will be set to 1.")]
    public int Chance { get; set; } = 1;

    [YamlMember(DefaultValuesHandling = DefaultValuesHandling.OmitDefaults | DefaultValuesHandling.OmitNull | DefaultValuesHandling.OmitEmptyCollections)]
    // [DefaultValue(default(Dictionary<RoleTypeId, int>))]
    [Description("A list of roles, and the chance of getting the role.")]
    public Dictionary<RoleTypeId, int> Roles { get; set; } = default(Dictionary<RoleTypeId, int>);
    
    [YamlMember(DefaultValuesHandling = DefaultValuesHandling.OmitDefaults | DefaultValuesHandling.OmitNull | DefaultValuesHandling.OmitEmptyCollections)]
    // [DefaultValue(0)]
    [Description("The health that this class has. 0 is default role health. -1 is godmode.")]
    public int Health { get; set; } = 0;

    [YamlMember(DefaultValuesHandling = DefaultValuesHandling.OmitDefaults | DefaultValuesHandling.OmitNull |
                                        DefaultValuesHandling.OmitEmptyCollections)]
    // [DefaultValue(0)]
    [Description("How much artificial health the class has. 0 is default artificial health.")]
    public ArtificialHealth ArtificialHealth { get; set; } = new ArtificialHealth()
    {
        AbsorptionPercent = 100,
        Permanent = true,
        InitialAmount = 0,
        MaxAmount = 0,
        RegenerationAmount = 0
    };
    
    [YamlMember(DefaultValuesHandling = DefaultValuesHandling.OmitDefaults | DefaultValuesHandling.OmitNull | DefaultValuesHandling.OmitEmptyCollections)]
    // [DefaultValue(0)]
    [Description("The stamina the player should get. 0 will ignore.")]
    public float Stamina { get; set; } = 0f;
    
    [YamlMember(DefaultValuesHandling = DefaultValuesHandling.OmitDefaults | DefaultValuesHandling.OmitNull | DefaultValuesHandling.OmitEmptyCollections)]
    // [DefaultValue(default(List<ItemType>))]
    [Description("The items that this class spawns with.")]
    public List<ItemType> Items { get; set; } = default(List<ItemType>);

    [YamlMember(DefaultValuesHandling = DefaultValuesHandling.OmitDefaults | DefaultValuesHandling.OmitNull | DefaultValuesHandling.OmitEmptyCollections)]
    // [DefaultValue(default(List<Effect>))]
    [Description("A list of effects the player will spawn with.")]
    public List<Effect> Effects { get; set; }  = default(List<Effect>);
    
    [YamlMember(DefaultValuesHandling = DefaultValuesHandling.Preserve)]
    //[DefaultValue(null)]
    [Description("The size of this class. One is normal.")]
    public Vector3 Size { get; set; } = Vector3.one;
    
    [YamlMember(DefaultValuesHandling = DefaultValuesHandling.OmitDefaults | DefaultValuesHandling.OmitNull | DefaultValuesHandling.OmitEmptyCollections)]
    // [DefaultValue(AmmoMode.None)]
    [Description("Should the player have infinite ammo.")]
    public AmmoMode InfiniteAmmo { get; set; } = AmmoMode.None;
}

public enum AmmoMode
{
    [Description("Ammo will be normal.")]
    None = 0,
    [Description("Player will be able to reload regardless of if there is ammo in their inventory.")]
    InfiniteAmmo = 2
}
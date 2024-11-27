﻿// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         AutoEvent
//    Project:          AutoEvent
//    FileName:         ReloadConfigs.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/13/2023 4:29 PM
//    Created Date:     09/13/2023 4:29 PM
// -----------------------------------------

using System;
using System.Collections.Generic;
using AutoEvent.Interfaces;
using CommandSystem;
using PluginAPI.Core;
#if EXILED
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
#endif

namespace AutoEvent.Commands.Debug;
public class Disable : ICommand, IPermission
{
    public string Command => nameof(Disable);
    public string[] Aliases => new string[] { };
    public string Description => "Disables Debug Mode";
    public string Permission { get; set; } = "ev.debug";
    public bool SanitizeResponse => false;
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.CheckPermission(((IPermission)this).Permission, out bool IsConsoleCommandSender))
        {
            response = "<color=red>You do not have permission to use this command!</color>";
            return false;
        }

        DebugLogger.Debug = false;
        response = "Debug mode has been disabled.";
        return true;
    }
}
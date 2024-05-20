﻿using System.ComponentModel;

namespace AutoEvent.Games.Jail;

public class LockdownSettings
{
    public LockdownSettings(){ }

    public LockdownSettings(float autoReleaseDelayInSeconds = 20f, float lockdownDurationInSeconds = 15f,
        float lockdownCooldownDurationInSeconds = 15f, bool lockdownLocksGatesAsWell = true)
    {
        AutoReleaseDelayInSeconds = autoReleaseDelayInSeconds;
        LockdownDurationInSeconds = lockdownDurationInSeconds;
        LockdownCooldownDurationInSeconds = lockdownCooldownDurationInSeconds;
        LockdownLocksGatesAsWell = lockdownLocksGatesAsWell;
    }
    [Description("How long before prisoners are released automatically. -1 to disable.")]
    public float AutoReleaseDelayInSeconds { get; set; } = 20f;

    [Description("How long the \"Jail Lockdown Button\" will lock the prisoner doors. O5 and containment engineer will double the duration. Bypass mode will permanently lockdown.")]
    public float LockdownDurationInSeconds { get; set; } = 15f;
    
    [Description("How long the cooldown is for the jail lockdown. Bypass mode will skip this cooldown.")]
    public float LockdownCooldownDurationInSeconds { get; set; } = 15f;
    
    [Description("Should lockdowns lock the gates as well. Anyone with a keycard that has checkpoint access can still go through gates.")]
    public bool LockdownLocksGatesAsWell { get; set; } = true;
}
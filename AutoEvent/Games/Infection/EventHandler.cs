﻿using AutoEvent.Events.EventArgs;
using MEC;
using PlayerRoles;
using PlayerStatsSystem;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using System.Linq;

namespace AutoEvent.Games.Infection
{
    public class EventHandler
    {
        Plugin _plugin;
        public EventHandler(Plugin plugin)
        {
            _plugin = plugin;
        }

        public void OnPlayerDamage(PlayerDamageArgs ev)
        {
            if (ev.DamageType == DeathTranslations.Falldown.Id)
            {
                ev.IsAllowed = false;
            }

            if (ev.Attacker != null)
            {
                if (ev.Attacker.Role == RoleTypeId.Scp0492)
                {
                    ev.Target.GiveLoadout(_plugin.Config.ZombieLoadouts);
                    ev.Attacker.ReceiveHitMarker(1f);
                    Extensions.PlayPlayerAudio(ev.Target, _plugin.Config.ZombieScreams.RandomItem(), 15);
                }
                /* // Christmas Update
                else if (ev.Attacker.Role == RoleTypeId.ZombieFlamingo)
                {
                    ev.Target.GiveLoadout(_plugin.Config.ZombieFlamingoLoadouts);
                    ev.Attacker.ReceiveHitMarker(1f);
                    Extensions.PlayPlayerAudio(ev.Target, _plugin.Config.ZombieScreams.RandomItem(), 15);
                }
                */
            }
        }

        [PluginEvent(ServerEventType.PlayerJoined)]
        public void OnJoin(PlayerJoinedEvent ev)
        {
            if (Player.GetPlayers().Count(r => r.Role == RoleTypeId.Scp0492) > 0)
            {
                ev.Player.GiveLoadout(_plugin.Config.ZombieLoadouts);
                ev.Player.Position = RandomPosition.GetSpawnPosition(_plugin.MapInfo.Map);
                Extensions.PlayPlayerAudio(ev.Player, _plugin.Config.ZombieScreams.RandomItem(), 15);
            }
            else
            {
                ev.Player.GiveLoadout(_plugin.Config.PlayerLoadouts);
                ev.Player.Position = RandomPosition.GetSpawnPosition(_plugin.MapInfo.Map);
            }

            /* // Christmas Update
            if (_plugin.IsFlamingoVariant == true)
            {
                if (Player.GetPlayers().Count(r => r.Role == RoleTypeId.ZombieFlamingo) > 0)
                {
                    ev.Player.GiveLoadout(_plugin.Config.ZombieFlamingoLoadouts);
                    ev.Player.Position = RandomPosition.GetSpawnPosition(_plugin.MapInfo.Map);
                    Extensions.PlayPlayerAudio(ev.Player, _plugin.Config.ZombieScreams.RandomItem(), 15);
                }
                else
                {
                    ev.Player.GiveLoadout(_plugin.Config.FlamingoLoadouts);
                    ev.Player.Position = RandomPosition.GetSpawnPosition(_plugin.MapInfo.Map);
                }
            }
            */
        }

        [PluginEvent(ServerEventType.PlayerDeath)]
        public void OnDeath(PlayerDeathEvent ev)
        {
            Timing.CallDelayed(2f, () =>
            {
                /* // Christmas Update
                if (_plugin.IsFlamingoVariant == true)
                {
                    ev.Player.GiveLoadout(_plugin.Config.ZombieFlamingoLoadouts);
                }
                */
                ev.Player.GiveLoadout(_plugin.Config.ZombieLoadouts);
                ev.Player.Position = RandomPosition.GetSpawnPosition(_plugin.MapInfo.Map);
                Extensions.PlayPlayerAudio(ev.Player, _plugin.Config.ZombieScreams.RandomItem(), 15);
            });
        }

        public void OnTeamRespawn(TeamRespawnArgs ev) => ev.IsAllowed = false;
        public void OnSpawnRagdoll(SpawnRagdollArgs ev) => ev.IsAllowed = false;
        public void OnPlaceBullet(PlaceBulletArgs ev) => ev.IsAllowed = false;
        public void OnPlaceBlood(PlaceBloodArgs ev) => ev.IsAllowed = false;
        public void OnDropItem(DropItemArgs ev) => ev.IsAllowed = false;
        public void OnDropAmmo(DropAmmoArgs ev) => ev.IsAllowed = false;
    }
}

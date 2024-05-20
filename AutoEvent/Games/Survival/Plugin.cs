﻿using MER.Lite.Objects;
using MEC;
using PluginAPI.Core;
using PluginAPI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AutoEvent.Events.Handlers;
using AutoEvent.Interfaces;
using Event = AutoEvent.Interfaces.Event;

namespace AutoEvent.Games.Survival
{
    public class Plugin : Event, IEventSound, IEventMap, IInternalEvent
    {
        public override string Name { get; set; } = "Zombie Survival";
        public override string Description { get; set; } = "Humans surviving from zombies";
        public override string Author { get; set; } = "KoT0XleB";
        public override string CommandName { get; set; } = "zombie2";
        public override Version Version { get; set; } = new Version(1, 0, 0);
        [EventConfig]
        public Config Config { get; set; }
        [EventTranslation]
        public Translation Translation { get; set; }
        public MapInfo MapInfo { get; set; } = new MapInfo()
        { 
            MapName = "Survival", 
            Position = new Vector3(15f, 1030f, -43.68f)
        };
        public SoundInfo SoundInfo { get; set; } = new SoundInfo()
        { 
            SoundName = "Survival.ogg",
            Volume = 10,
            Loop = false
        };
        protected override float PostRoundDelay { get; set; } = 10f;
        private EventHandler _eventHandler { get; set; }
        internal Player FirstZombie { get; private set; }
        private GameObject _teleport;
        private GameObject _teleport1;
        private TimeSpan _remainingTime;

        protected override void RegisterEvents()
        {
            _eventHandler = new EventHandler(this);
            EventManager.RegisterEvents(_eventHandler);
            Servers.TeamRespawn += _eventHandler.OnTeamRespawn;
            Servers.SpawnRagdoll += _eventHandler.OnSpawnRagdoll;
            Servers.PlaceBullet += _eventHandler.OnPlaceBullet;
            Servers.PlaceBlood += _eventHandler.OnPlaceBlood;
            Players.DropItem += _eventHandler.OnDropItem;
            Players.DropAmmo += _eventHandler.OnDropAmmo;
            Players.PlayerDamage += _eventHandler.OnPlayerDamage;
        }

        protected override void UnregisterEvents()
        {
            EventManager.UnregisterEvents(_eventHandler);
            Servers.TeamRespawn -= _eventHandler.OnTeamRespawn;
            Servers.SpawnRagdoll -= _eventHandler.OnSpawnRagdoll;
            Servers.PlaceBullet -= _eventHandler.OnPlaceBullet;
            Servers.PlaceBlood -= _eventHandler.OnPlaceBlood;
            Players.DropItem -= _eventHandler.OnDropItem;
            Players.DropAmmo -= _eventHandler.OnDropAmmo;
            Players.PlayerDamage -= _eventHandler.OnPlayerDamage;
            _eventHandler = null;
        }

        protected override void OnStart()
        {
            _remainingTime = new TimeSpan(0, 5, 0);
            Server.FriendlyFire = false;
            foreach (Player player in Player.GetPlayers())
            {
                player.GiveLoadout(Config.PlayerLoadouts);
                player.Position = RandomClass.GetSpawnPosition(MapInfo.Map);
            }
        }

        protected override IEnumerator<float> BroadcastStartCountdown()
        {
            for (float _time = 20; _time > 0; _time--)
            {
                Extensions.Broadcast(Translation.SurvivalBeforeInfection.Replace("{name}", Name).Replace("{time}", $"{_time}"), 1);
                yield return Timing.WaitForSeconds(1f);
            }
        }

        protected override void CountdownFinished()
        {
            Extensions.PlayAudio("Zombie2.ogg", 7, true);

            List<Player> players = Config.Zombies.GetPlayers(true);
            foreach (Player x in players)
            {
                DebugLogger.LogDebug($"Making player {x.Nickname} a zombie.");
                x.GiveLoadout(Config.ZombieLoadouts);
                if (Player.GetPlayers().Count(r => r.IsSCP) == 1)
                {
                    if (FirstZombie is not null)
                        continue;
                    FirstZombie = x;
                }
            }

            _teleport = MapInfo.Map.AttachedBlocks.First(x => x.name == "Teleport");
            _teleport1 = MapInfo.Map.AttachedBlocks.First(x => x.name == "Teleport1");
        }

        protected override bool IsRoundDone()
        {
            // At least 1 human player &&
            // At least 1 scp player &&
            // round time under 5 minutes (+ countdown)
            bool a = Player.GetPlayers().Any(ply => ply.HasLoadout(Config.PlayerLoadouts));
            bool b = Player.GetPlayers().Any(ply => ply.HasLoadout(Config.ZombieLoadouts));
            bool c = EventTime.TotalSeconds < Config.RoundDurationInSeconds;
            return !(a && b && c);
        }

        protected override void ProcessFrame()
        {
            var text = Translation.SurvivalAfterInfection;
            
            text = text.Replace("{name}", Name);
            text = text.Replace("{humanCount}", Player.GetPlayers().Count(r => r.IsHuman).ToString());
            text = text.Replace("{time}", $"{_remainingTime.Minutes:00}:{_remainingTime.Seconds:00}");

            foreach (var player in Player.GetPlayers())
            {
                player.ClearBroadcasts();
                player.SendBroadcast(text, 1);

                if (Vector3.Distance(player.Position, _teleport.transform.position) < 1)
                {
                    player.Position = _teleport1.transform.position;
                }
            }

            _remainingTime -= TimeSpan.FromSeconds(FrameDelayInSeconds);
        }

        protected override void OnFinished()
        {
            string text = string.Empty;
            string musicName = "HumanWin.ogg";

            if (Player.GetPlayers().Count(r => r.IsHuman) == 0)
            {
                text = Translation.SurvivalZombieWin;
                musicName = "ZombieWin.ogg";
            }
            else if (Player.GetPlayers().Count(r => r.IsSCP) == 0)
            {
                text = Translation.SurvivalHumanWin;
            }
            else
            {
                text = Translation.SurvivalHumanWinTime;
            }

            Extensions.PlayAudio(musicName, 7, false);
            Extensions.Broadcast(text, 10);
        }
    }
}

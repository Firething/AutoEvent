﻿using Exiled.API.Interfaces;

namespace AutoEvent.Games.Lobby
{
#if EXILED
    public class LobbyTranslation : ITranslation 
#else
    public class LobbyTranslation
#endif
    {
        public string LobbyCommandName { get; set; } = "lobby";
        public string LobbyName { get; set; } = "Lobby";
        public string LobbyDescription { get; set; } = "A lobby in which one quick player chooses a mini-game.";
        public string LobbyCycle { get; set; } = "Vote: Press [Alt] Pros or [Alt]x2 Cons\n{trueCount} of {allCount} players for {newName}\n{time} seconds left!";
        public string LobbyGlobalMessage { get; set; } = "Lobby\n{message}\n{count} players in the lobby";
        public string LobbyGetReady { get; set; } = "Get ready to run to the center and choose a mini game";
        public string LobbyRun { get; set; } = "RUN";
        public string LobbyChoosing { get; set; } = "Waiting for the {nickName} to choose mini-game";
        public string LobbyFinishMessage { get; set; } = "The lobby is finished.\nThe player {nickName} chose the {newName} mini-game.\nTotal {count} players in the lobby";
    }
}
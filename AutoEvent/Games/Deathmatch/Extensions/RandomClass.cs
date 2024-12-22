﻿using System.Linq;
using MapEditorReborn.API.Features.Objects;
using UnityEngine;

namespace AutoEvent.Games.Deathmatch
{
    internal class RandomClass
    {
        public static Vector3 GetRandomPosition(SchematicObject GameMap)
        {
            if (GameMap is null)
            {
                DebugLogger.LogDebug("Map is null");
                return Vector3.zero;
            }

            if (GameMap.AttachedBlocks is null)
            {
                DebugLogger.LogDebug("Attached Blocks is null");
                return Vector3.zero;
            }

            var spawnpoint = GameMap.AttachedBlocks.Where(x => x.name == "Spawnpoint").ToList().RandomItem();
            if (spawnpoint is null)
            {
                DebugLogger.LogDebug("Spawnpoint is null");
                return Vector3.zero;
            }

            return spawnpoint.transform.position;
        }
    }
}

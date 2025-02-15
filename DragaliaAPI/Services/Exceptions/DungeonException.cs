﻿using DragaliaAPI.Models;

namespace DragaliaAPI.Services.Exceptions;

public class DungeonException : DragaliaException
{
    public DungeonException(string dungeonKeyId)
        : base(ResultCode.DUNGEON_AREA_NOT_FOUND, $"Failed to lookup dungeon: {dungeonKeyId}") { }
}

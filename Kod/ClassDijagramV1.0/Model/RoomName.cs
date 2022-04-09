// File:    RoomType.cs
// Author:  Milana
// Created: 08 April 2022 13:17:18
// Purpose: Definition of Enum RoomType

using System;
using System.Runtime.Serialization;

namespace Model
{
    public enum RoomName
    {
        operatingRoom,
        examRoom,
        recoveryRoom,
        intensiveCareRoom,
        meetingRoom
    }
}
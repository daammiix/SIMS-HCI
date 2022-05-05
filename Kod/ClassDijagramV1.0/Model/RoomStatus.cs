// File:    RoomStatus.cs
// Author:  Milana
// Created: 08 April 2022 13:17:19
// Purpose: Definition of Enum RoomStatus

using System;
using System.Collections.Generic;

namespace Model
{
    public static class RoomStatus
    {
        public const String active = "Aktivna";
        public const String renovating = "Renoviranje";
        public const String changePurpose = "Promena namene sale";

        public static List<String> getAllStatuses()
        {
            return new List<String>()
            {
                active,
                renovating,
                changePurpose,
            };
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace BattleshipsLibrary
{
    //Ship position

    [ProtoContract]
    public class GridPosition
    {
        //Coordinates
        [ProtoMember(1)]
        public int X { get; set; }
        [ProtoMember(2)]
        public int Y { get; set; }

        //Features
        [ProtoMember(3)]
        public bool IsHit { get; set; }
        [ProtoMember(4)]
        public bool IsMissed { get; set; }
        [ProtoMember(5)]
        public bool IsSelected { get; set; }

        public GridPosition()
        {
            
        }

        public GridPosition(int x, int y)
        {
            IsHit = false;
            IsMissed = false;
            X = x;
            Y = y;
        }


        //Check if the positions are equal
        //Only X and Y coordinates are checked, not other variables
        public override int GetHashCode()
        {
            return Convert.ToInt32($"{X}{Y}");
        }

        public override bool Equals(object other)
        {
            if (other is GridPosition)
            {
                if (((GridPosition) other).X == X && ((GridPosition) other).Y == Y) return true;
            }
            return false;
        }
    }
}

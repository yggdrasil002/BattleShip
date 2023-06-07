using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace BattleshipsLibrary
{
    //Position update, who's next

    [ProtoContract]
    public class GamePositionUpdateInfo
    {
        [ProtoMember(1)]
        public GridPosition GridPosition { get; set; }
        [ProtoMember(2)]        
        public UpdateType UpdateType { get; set; }
        [ProtoMember(3)]
        public bool IsOnTurn { get; set; }

        public GamePositionUpdateInfo()
        {
            
        }

        public GamePositionUpdateInfo(GridPosition gridPosition, UpdateType updateType, bool isOnTurn)
        {
            GridPosition = gridPosition;
            UpdateType = updateType;
            IsOnTurn = isOnTurn;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace BattleshipsLibrary
{
    //Request for start of game, opponent IP and Port

    [ProtoContract]
    public class GameStartRequest
    {
        [ProtoMember(1)]
        public List<GridPosition> ShipsPositions { get; set; }
        [ProtoMember(2)]
        public string EnemyIp { get; set; }
        [ProtoMember(3)]
        public int EnemyPort { get; set; }

        public GameStartRequest()
        {
            
        }

        public GameStartRequest(List<GridPosition> shipsPositions, string enemyIp, int enemyPort)
        {
            ShipsPositions = shipsPositions;
            EnemyIp = enemyIp;
            EnemyPort = enemyPort;
        }
    }
}

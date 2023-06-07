using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsLibrary
{
    public class Enums
    {
        //Return enum descriptions, instead of name
        
        public static string GetDescription(Enum enumName)
        {
            Type enumNameType = enumName.GetType();
            MemberInfo[] memberInfo = enumNameType.GetMember(enumName.ToString());
            if ((memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs.Count() > 0))
                {
                    return ((DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return enumName.ToString();
        }
    }

    //Type lodi
    public enum ShipType
    {
        [Description("Nic nevybráno!")]
        None = 0,

        [Description("Torpédoborec")]
        Destroyer = 1,

        [Description("Ponorka")]
        Submarine = 2,

        [Description("Křižník")]
        Cruiser = 3,

        [Description("Bitevní loď")]
        Battleship = 4,

        [Description("Letadlová loď")]
        Carrier = 5
    }

    //Tile texture
    public enum TileType
    {
        Water,
        ShipCenterHorizontal,
        ShipCenterVertical,
        ShipEndLeft,
        ShipEndRight,
        ShipEndUp,
        ShipEndDown,
        ShipSolo
    }

    //Type of ship laying
    public enum PlacementType
    {
        Solo,
        Horizontal,
        Vertical,
        Invalid,
        Occupied
    }

    //Updating the player or enemy grid (crosses, circles)
    public enum UpdateType
    {
        PlayerGrid,
        EnemyGrid
    }

    //The server's response to the connection request
    public enum ResponseType
    {
        Accepted,
        Rejected
    }
}

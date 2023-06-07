using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
using RemoteProcedureCalls;

namespace BattleshipsLibrary
{
    [ProtoContract]
    public class ConnectResponse
    {
        [ProtoMember(1)]
        public ResponseType ResponseType { get; set; }
        [ProtoMember(2)]
        public string Response { get; set; }
        [ProtoMember(3)]
        public string ServerName { get; set; }
        [ProtoMember(4)]
        public List<Client> ConnectedClients { get; set; }

        public ConnectResponse()
        {
            
        }

        //Connection request accepted, online clients
        public ConnectResponse(ResponseType responseType, string serverName, List<Client> connectedClients)
        {
            ResponseType = responseType;
            ServerName = serverName;
            Response = "";
            ConnectedClients = connectedClients;
        }

        //Connection request refused, reason
        public ConnectResponse(ResponseType responseType, string serverName, string response)
        {
            ResponseType = responseType;
            ServerName = serverName;
            Response = response;
        }
    }
}

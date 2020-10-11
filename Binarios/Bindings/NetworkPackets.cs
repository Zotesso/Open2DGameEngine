using System;
using System.Collections.Generic;
using System.Text;

namespace Bindings
{
    public enum ServerPackets { 
        SJoinGame = 1,
        SPlayerMove,
        SPlayerData
    }

    public enum ClientPackets { 
        CLogin = 1,
        CRegister,
        CPlayerMove,
    }
}

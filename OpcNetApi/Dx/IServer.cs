using System;
using Opc.Da;

namespace Opc.Dx
{
    public interface IServer : IDisposable
    {
        SourceServer[] GetSourceServers();

        GeneralResponse AddSourceServers(SourceServer[] servers);

        GeneralResponse ModifySourceServers(SourceServer[] servers);

        GeneralResponse DeleteSourceServers(ItemIdentifier[] servers);

        GeneralResponse CopyDefaultSourceServerAttributes(bool configToStatus, ItemIdentifier[] servers);

        DXConnection[] QueryDXConnections(string browsePath, DXConnection[] connectionMasks, bool recursive, out ResultID[] errors);

        GeneralResponse AddDXConnections(DXConnection[] connections);

        GeneralResponse ModifyDXConnections(DXConnection[] connections);

        GeneralResponse UpdateDXConnections(string browsePath, DXConnection[] connectionMasks, bool recursive, DXConnection connectionDefinition, out ResultID[] errors);

        GeneralResponse DeleteDXConnections(string browsePath, DXConnection[] connectionMasks, bool recursive, out ResultID[] errors);

        GeneralResponse CopyDXConnectionDefaultAttributes(bool configToStatus, string browsePath, DXConnection[] connectionMasks, bool recursive, out ResultID[] errors);

        string ResetConfiguration(string configurationVersion);
    }
}
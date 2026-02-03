using System;
using System.Runtime.Serialization;
using Opc.Da;

namespace Opc.Dx
{
    [Serializable]
    public class Server : Opc.Server, IServer, Opc.IServer, IDisposable, ISerializable
    {
        public Server(Factory factory, URL url) : base(factory, url)
        {
        }

        public string Version => m_version;

        public SourceServerCollection SourceServers => m_sourceServers;

        public DXConnectionQueryCollection Queries => m_connectionQueries;

        public SourceServer AddSourceServer(SourceServer server)
        {
            GeneralResponse generalResponse = AddSourceServers(new SourceServer[]
            {
                server
            });
            if (generalResponse == null || generalResponse.Count != 1)
            {
                throw new InvalidResponseException();
            }

            if (generalResponse[0].ResultID.Failed())
            {
                throw new ResultIDException(generalResponse[0].ResultID);
            }

            return new SourceServer(server)
            {
                ItemName = generalResponse[0].ItemName,
                ItemPath = generalResponse[0].ItemPath,
                Version = generalResponse[0].Version
            };
        }

        public SourceServer ModifySourceServer(SourceServer server)
        {
            GeneralResponse generalResponse = ModifySourceServers(new SourceServer[]
            {
                server
            });
            if (generalResponse == null || generalResponse.Count != 1)
            {
                throw new InvalidResponseException();
            }

            if (generalResponse[0].ResultID.Failed())
            {
                throw new ResultIDException(generalResponse[0].ResultID);
            }

            return new SourceServer(server)
            {
                ItemName = generalResponse[0].ItemName,
                ItemPath = generalResponse[0].ItemPath,
                Version = generalResponse[0].Version
            };
        }

        public void DeleteSourceServer(SourceServer server)
        {
            GeneralResponse generalResponse = DeleteSourceServers(new ItemIdentifier[]
            {
                server
            });
            if (generalResponse == null || generalResponse.Count != 1)
            {
                throw new InvalidResponseException();
            }

            if (generalResponse[0].ResultID.Failed())
            {
                throw new ResultIDException(generalResponse[0].ResultID);
            }
        }

        public DXConnection AddDXConnection(DXConnection connection)
        {
            GeneralResponse generalResponse = AddDXConnections(new DXConnection[]
            {
                connection
            });
            if (generalResponse == null || generalResponse.Count != 1)
            {
                throw new InvalidResponseException();
            }

            if (generalResponse[0].ResultID.Failed())
            {
                throw new ResultIDException(generalResponse[0].ResultID);
            }

            return new DXConnection(connection)
            {
                ItemName = generalResponse[0].ItemName,
                ItemPath = generalResponse[0].ItemPath,
                Version = generalResponse[0].Version
            };
        }

        public DXConnection ModifyDXConnection(DXConnection connection)
        {
            GeneralResponse generalResponse = ModifyDXConnections(new DXConnection[]
            {
                connection
            });
            if (generalResponse == null || generalResponse.Count != 1)
            {
                throw new InvalidResponseException();
            }

            if (generalResponse[0].ResultID.Failed())
            {
                throw new ResultIDException(generalResponse[0].ResultID);
            }

            return new DXConnection(connection)
            {
                ItemName = generalResponse[0].ItemName,
                ItemPath = generalResponse[0].ItemPath,
                Version = generalResponse[0].Version
            };
        }

        public void DeleteDXConnections(DXConnection connection)
        {
            GeneralResponse generalResponse = DeleteDXConnections(null, new DXConnection[]
            {
                connection
            }, true, out var array);
            if (array != null && array.Length > 0 && array[0].Failed())
            {
                throw new ResultIDException(array[0]);
            }

            if (generalResponse == null || generalResponse.Count != 1)
            {
                throw new InvalidResponseException();
            }

            if (generalResponse[0].ResultID.Failed())
            {
                throw new ResultIDException(generalResponse[0].ResultID);
            }
        }

        protected Server(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            DXConnectionQuery[] array = (DXConnectionQuery[])info.GetValue("Queries", typeof(DXConnectionQuery[]));
            if (array != null)
            {
                foreach (DXConnectionQuery value in array)
                {
                    m_connectionQueries.Add(value);
                }
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            DXConnectionQuery[] array = null;
            if (m_connectionQueries.Count > 0)
            {
                array = new DXConnectionQuery[m_connectionQueries.Count];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = m_connectionQueries[i];
                }
            }

            info.AddValue("Queries", array);
        }

        public SourceServer[] GetSourceServers()
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            SourceServer[] sourceServers = ((IServer)m_server).GetSourceServers();
            m_sourceServers.Initialize(sourceServers);
            return sourceServers;
        }

        public GeneralResponse AddSourceServers(SourceServer[] servers)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            GeneralResponse generalResponse = ((IServer)m_server).AddSourceServers(servers);
            if (generalResponse != null)
            {
                GetSourceServers();
                m_version = generalResponse.Version;
            }

            return generalResponse;
        }

        public GeneralResponse ModifySourceServers(SourceServer[] servers)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            GeneralResponse generalResponse = ((IServer)m_server).ModifySourceServers(servers);
            if (generalResponse != null)
            {
                GetSourceServers();
                m_version = generalResponse.Version;
            }

            return generalResponse;
        }

        public GeneralResponse DeleteSourceServers(ItemIdentifier[] servers)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            GeneralResponse generalResponse = ((IServer)m_server).DeleteSourceServers(servers);
            if (generalResponse != null)
            {
                GetSourceServers();
                m_version = generalResponse.Version;
            }

            return generalResponse;
        }

        public GeneralResponse CopyDefaultSourceServerAttributes(bool configToStatus, ItemIdentifier[] servers)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            GeneralResponse generalResponse = ((IServer)m_server).CopyDefaultSourceServerAttributes(configToStatus, servers);
            if (generalResponse != null)
            {
                if (!configToStatus)
                {
                    GetSourceServers();
                }

                m_version = generalResponse.Version;
            }

            return generalResponse;
        }

        public DXConnection[] QueryDXConnections(string browsePath, DXConnection[] connectionMasks, bool recursive, out ResultID[] errors)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)m_server).QueryDXConnections(browsePath, connectionMasks, recursive, out errors);
        }

        public GeneralResponse AddDXConnections(DXConnection[] connections)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            GeneralResponse generalResponse = ((IServer)m_server).AddDXConnections(connections);
            if (generalResponse != null)
            {
                m_version = generalResponse.Version;
            }

            return generalResponse;
        }

        public GeneralResponse ModifyDXConnections(DXConnection[] connections)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            GeneralResponse generalResponse = ((IServer)m_server).ModifyDXConnections(connections);
            if (generalResponse != null)
            {
                m_version = generalResponse.Version;
            }

            return generalResponse;
        }

        public GeneralResponse UpdateDXConnections(string browsePath, DXConnection[] connectionMasks, bool recursive, DXConnection connectionDefinition, out ResultID[] errors)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            GeneralResponse generalResponse = ((IServer)m_server).UpdateDXConnections(browsePath, connectionMasks, recursive, connectionDefinition, out errors);
            if (generalResponse != null)
            {
                m_version = generalResponse.Version;
            }

            return generalResponse;
        }

        public GeneralResponse DeleteDXConnections(string browsePath, DXConnection[] connectionMasks, bool recursive, out ResultID[] errors)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            GeneralResponse generalResponse = ((IServer)m_server).DeleteDXConnections(browsePath, connectionMasks, recursive, out errors);
            if (generalResponse != null)
            {
                m_version = generalResponse.Version;
            }

            return generalResponse;
        }

        public GeneralResponse CopyDXConnectionDefaultAttributes(bool configToStatus, string browsePath, DXConnection[] connectionMasks, bool recursive, out ResultID[] errors)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            GeneralResponse generalResponse = ((IServer)m_server).CopyDXConnectionDefaultAttributes(configToStatus, browsePath, connectionMasks, recursive, out errors);
            if (generalResponse != null)
            {
                m_version = generalResponse.Version;
            }

            return generalResponse;
        }

        public string ResetConfiguration(string configurationVersion)
        {
            if (m_server == null)
            {
                throw new NotConnectedException();
            }

            m_version = ((IServer)m_server).ResetConfiguration((configurationVersion == null) ? m_version : configurationVersion);
            return m_version;
        }

        private string m_version;

        private SourceServerCollection m_sourceServers = new SourceServerCollection();

        private DXConnectionQueryCollection m_connectionQueries = new DXConnectionQueryCollection();

        private class Names
        {
            internal const string QUERIES = "Queries";
        }
    }
}
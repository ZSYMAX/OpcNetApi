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

        public string Version
        {
            get { return this.m_version; }
        }

        public SourceServerCollection SourceServers
        {
            get { return this.m_sourceServers; }
        }

        public DXConnectionQueryCollection Queries
        {
            get { return this.m_connectionQueries; }
        }

        public SourceServer AddSourceServer(SourceServer server)
        {
            GeneralResponse generalResponse = this.AddSourceServers(new SourceServer[]
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
            GeneralResponse generalResponse = this.ModifySourceServers(new SourceServer[]
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
            GeneralResponse generalResponse = this.DeleteSourceServers(new ItemIdentifier[]
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
            GeneralResponse generalResponse = this.AddDXConnections(new DXConnection[]
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
            GeneralResponse generalResponse = this.ModifyDXConnections(new DXConnection[]
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
            ResultID[] array = null;
            GeneralResponse generalResponse = this.DeleteDXConnections(null, new DXConnection[]
            {
                connection
            }, true, out array);
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
                    this.m_connectionQueries.Add(value);
                }
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            DXConnectionQuery[] array = null;
            if (this.m_connectionQueries.Count > 0)
            {
                array = new DXConnectionQuery[this.m_connectionQueries.Count];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = this.m_connectionQueries[i];
                }
            }

            info.AddValue("Queries", array);
        }

        public SourceServer[] GetSourceServers()
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            SourceServer[] sourceServers = ((IServer)this.m_server).GetSourceServers();
            this.m_sourceServers.Initialize(sourceServers);
            return sourceServers;
        }

        public GeneralResponse AddSourceServers(SourceServer[] servers)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            GeneralResponse generalResponse = ((IServer)this.m_server).AddSourceServers(servers);
            if (generalResponse != null)
            {
                this.GetSourceServers();
                this.m_version = generalResponse.Version;
            }

            return generalResponse;
        }

        public GeneralResponse ModifySourceServers(SourceServer[] servers)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            GeneralResponse generalResponse = ((IServer)this.m_server).ModifySourceServers(servers);
            if (generalResponse != null)
            {
                this.GetSourceServers();
                this.m_version = generalResponse.Version;
            }

            return generalResponse;
        }

        public GeneralResponse DeleteSourceServers(ItemIdentifier[] servers)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            GeneralResponse generalResponse = ((IServer)this.m_server).DeleteSourceServers(servers);
            if (generalResponse != null)
            {
                this.GetSourceServers();
                this.m_version = generalResponse.Version;
            }

            return generalResponse;
        }

        public GeneralResponse CopyDefaultSourceServerAttributes(bool configToStatus, ItemIdentifier[] servers)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            GeneralResponse generalResponse = ((IServer)this.m_server).CopyDefaultSourceServerAttributes(configToStatus, servers);
            if (generalResponse != null)
            {
                if (!configToStatus)
                {
                    this.GetSourceServers();
                }

                this.m_version = generalResponse.Version;
            }

            return generalResponse;
        }

        public DXConnection[] QueryDXConnections(string browsePath, DXConnection[] connectionMasks, bool recursive, out ResultID[] errors)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            return ((IServer)this.m_server).QueryDXConnections(browsePath, connectionMasks, recursive, out errors);
        }

        public GeneralResponse AddDXConnections(DXConnection[] connections)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            GeneralResponse generalResponse = ((IServer)this.m_server).AddDXConnections(connections);
            if (generalResponse != null)
            {
                this.m_version = generalResponse.Version;
            }

            return generalResponse;
        }

        public GeneralResponse ModifyDXConnections(DXConnection[] connections)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            GeneralResponse generalResponse = ((IServer)this.m_server).ModifyDXConnections(connections);
            if (generalResponse != null)
            {
                this.m_version = generalResponse.Version;
            }

            return generalResponse;
        }

        public GeneralResponse UpdateDXConnections(string browsePath, DXConnection[] connectionMasks, bool recursive, DXConnection connectionDefinition, out ResultID[] errors)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            GeneralResponse generalResponse = ((IServer)this.m_server).UpdateDXConnections(browsePath, connectionMasks, recursive, connectionDefinition, out errors);
            if (generalResponse != null)
            {
                this.m_version = generalResponse.Version;
            }

            return generalResponse;
        }

        public GeneralResponse DeleteDXConnections(string browsePath, DXConnection[] connectionMasks, bool recursive, out ResultID[] errors)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            GeneralResponse generalResponse = ((IServer)this.m_server).DeleteDXConnections(browsePath, connectionMasks, recursive, out errors);
            if (generalResponse != null)
            {
                this.m_version = generalResponse.Version;
            }

            return generalResponse;
        }

        public GeneralResponse CopyDXConnectionDefaultAttributes(bool configToStatus, string browsePath, DXConnection[] connectionMasks, bool recursive, out ResultID[] errors)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            GeneralResponse generalResponse = ((IServer)this.m_server).CopyDXConnectionDefaultAttributes(configToStatus, browsePath, connectionMasks, recursive, out errors);
            if (generalResponse != null)
            {
                this.m_version = generalResponse.Version;
            }

            return generalResponse;
        }

        public string ResetConfiguration(string configurationVersion)
        {
            if (this.m_server == null)
            {
                throw new NotConnectedException();
            }

            this.m_version = ((IServer)this.m_server).ResetConfiguration((configurationVersion == null) ? this.m_version : configurationVersion);
            return this.m_version;
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
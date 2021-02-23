using Opc;
using Opc.Da;
using OpcCom;
using System;
using System.Collections.Generic;

namespace SampleApp
{
    class Program
    {
        private static Opc.Da.Server Server;

        private static ServerEnumerator m_discovery = new ServerEnumerator();

        private static SubscriptionState mMonitoringGroup = new SubscriptionState();
        private static Subscription mMonitoringSubscription = null;

        static void Main(string[] args)
        {
            var ServerName = "Kepware.KEPServerEX.V6";
            Init();
            items.Add(new Item()
            {
                ClientHandle = Guid.NewGuid().ToString(),
                ItemPath = null,
                ItemName = "模拟器示例.函数.Ramp1"
            });

            items.Add(new Item()
            {
                ClientHandle = Guid.NewGuid().ToString(),
                ItemPath = null,
                ItemName = "模拟器示例.函数.Ramp2"
            });

            items.Add(new Item()
            {
                ClientHandle = Guid.NewGuid().ToString(),
                ItemPath = null,
                ItemName = "模拟器示例.函数.Ramp3"
            });

            items.Add(new Item()
            {
                ClientHandle = Guid.NewGuid().ToString(),
                ItemPath = null,
                ItemName = "模拟器示例.函数.Ramp4"
            });

            Opc.Server[] servers = m_discovery.GetAvailableServers(Specification.COM_DA_20, "localhost", null);

            if (servers != null)
            {
                foreach (Opc.Da.Server server in servers)
                {
                    if (string.Compare(server.Name, ServerName, true) == 0)
                    {
                        Server = server;
                        break;
                    }
                }
            }
            if (Server != null)
            {
                Server.Connect();
            }

            // Add Group
            mMonitoringSubscription = (Subscription)Server.CreateSubscription(mMonitoringGroup); // Create Group

            ItemResult[] tItemResult = mMonitoringSubscription.AddItems(items.ToArray());
            ItemValueResult[] itemValues = mMonitoringSubscription.Read(mMonitoringSubscription.Items);

            OnServerDataChanged(mMonitoringSubscription, null, itemValues);
            // Register callback event
            mMonitoringSubscription.DataChanged += OnServerDataChanged;

            Console.ReadLine();
        }

        private static void Init()
        {
            mMonitoringGroup.Name = "Monitoring";                          // Group Name
            mMonitoringGroup.ServerHandle = null;                          // The handle assigned by the server to the group.
            mMonitoringGroup.ClientHandle = Guid.NewGuid().ToString();     // The handle assigned by the client to the group.
            mMonitoringGroup.Active = true;                                // Activate the group.
            mMonitoringGroup.UpdateRate = 100;                             // The refresh rate is 1 second. -> 1000
            mMonitoringGroup.Deadband = 0;                                 // When the dead zone value is set to 0, the server will notify the group of any data changes in the group.
            mMonitoringGroup.Locale = null;                                // No regional values are set.
        }

        private static readonly List<Item> items = new List<Item>();

        private static void OnServerDataChanged(object subscriptionHandle, object requestHandle, ItemValueResult[] values)
        {
            foreach (ItemValueResult item in values)
            {
                Console.WriteLine($"Name:{item.ItemName} ,Value:{item.Value}");
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using GTAServer;

namespace AdminTools
{
    public static class Accounts
    {
        public static bool IsAuthenticated(this Client client)
        {
            List<long> authenticatedUsers = Lists._authenticatedUsers;
            bool result;
            lock (authenticatedUsers)
            {
                result = Lists._authenticatedUsers.Contains
                    (client.NetConnection.RemoteUniqueIdentifier);
            }
            return result;
        }
        public static Account GetAccount(this Client client, bool checkauthentication = true)
        {
            if (!checkauthentication || client.IsAuthenticated())
            {
                List<Account> accounts = Lists._accounts.Accounts;
                lock (accounts)
                {
                    return Lists._accounts.Accounts.FirstOrDefault((Account acc) => acc.Name == client.Name);
                }
            }
            return null;
        }
        public static bool IsIPBanned(this Client client)
        {
            bool result = false;
            List<Ban> bannedIps = Lists._banned.BannedIps;
            lock (bannedIps)
            {
                result = Lists._banned.BannedIps.Any(delegate(Ban b)
                {
                    if (b.Address == client.NetConnection.RemoteEndPoint.Address.ToString())
                    {
                        client.GetAccount(true).Ban = b;
                        return true;
                    }
                    return false;
                });
            }
            return result;
        }
        public static bool IsBanned(this Client client)
        {
            return client.GetBan() != null;
        }
        public static Ban GetBan(this Client client)
        {
            Account account = client.GetAccount(true);
            if (account != null)
            {
                return account.Ban;
            }
            return null;
        }
        public static void Ban(this Client client, string Reason, Client IssuedBy = null)
        {
            Ban ban = new Ban
            {
                Address = client.NetConnection.RemoteEndPoint.Address.ToString(),
                BannedBy = ((IssuedBy == null) ? "Server" : IssuedBy.Name),
                Reason = Reason,
                TimeIssued = DateTime.Now,
                Name = client.Name
            };
            List<Ban> bannedIps = Lists._banned.BannedIps;
            lock (bannedIps)
            {
                Lists._banned.BannedIps.Add(ban);
            }
            client.GetAccount(true).Ban = ban;
        }
        public static Client GetClient(this Account account)
        {
            Client client = null;
            List<Client> clients = Program.ServerInstance.Clients;
            lock (clients)
            {
                Program.ServerInstance.Clients.Any(delegate(Client c)
                {
                    if (c.Name == account.Name)
                    {
                        client = c;
                        return true;
                    }
                    return false;
                });
            }
            return client;
        }
    }
}

using System;
using System.Collections.Generic;

namespace AdminTools
{
    public class Lists
    {
        internal static UserList _accounts = new UserList();

        internal static Banlist _banned = new Banlist();

        internal static List<long> _authenticatedUsers = new List<long>();
    }
}

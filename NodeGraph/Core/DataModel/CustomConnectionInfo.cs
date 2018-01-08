using System;

namespace NodeGraph.DataModel
{
    public struct CustomConnectionInfo
    {
        public CustomConnection connection;
        public Type type;

        public CustomConnectionInfo(Type t, CustomConnection n)
        {
            connection = n;
            type = t;
        }

        public Connection CreateInstance()
        {
            if(type == null || string.IsNullOrEmpty(type.FullName))
            {
                return Connection.CreateInstance<Connection>();
            }
            else
            {
                 return Connection.CreateInstance ( type) as Connection;
            }
        }
    }
}
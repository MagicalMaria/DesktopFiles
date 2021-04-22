using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPatternHandsOn
{
    class DBConn
    {
        private static DBConn instance;
        public static int instanceCount { get; set; }

        private DBConn()
        {
            instanceCount++;
        }

        public static DBConn getInstance()
        {
            if (instance == null)
                instance = new DBConn();
            return instance;
        }

    }
    class SingletonPattern
    {
        static void Main(string[] args)
        {
            DBConn dBConn1 = DBConn.getInstance();
            DBConn dBConn2 = DBConn.getInstance();
            DBConn dBConn3 = DBConn.getInstance();
            Console.WriteLine(DBConn.instanceCount);
            Console.ReadKey();
        }
    }
}

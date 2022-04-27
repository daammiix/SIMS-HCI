using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Util
{
    public class IdCounter
    {
        private static IdCounter _instance = null;

        private int _counter = 0;

        public static IdCounter getInstance()
        {
            if (_instance == null)
            {
                _instance = new IdCounter();
            }
            return _instance;
        }

        private IdCounter()
        {

        }

        public int getId()
        {
            return ++_counter;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeSpies
{
    public sealed class RuntimeSpySequence
    {
        private int _sequence = -1;

        public int _getSequence()
        {
            _sequence++;
            return _sequence;
            
        }

        public void _Reset()
        {
            _sequence = -1;
        }

        private RuntimeSpySequence()
        {
        }

        public static int GetSequence()
        {
            return Nested.instance._getSequence();
        }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly RuntimeSpySequence instance = new RuntimeSpySequence();
        }

        public static void Reset()
        {
            Nested.instance._Reset();
        }
    }
}

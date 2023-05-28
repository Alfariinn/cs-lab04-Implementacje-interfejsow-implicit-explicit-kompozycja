using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ver1;
using static ver1.IDevice;

namespace Zad3
{
    public  class Printer : BaseDevice, IPrinter
    {
        public int PrintCounter { get; private set; } = 0;
        public new int Counter { get; private set; } = 0;


        new public void PowerOn()
        {
            if (state == State.on)
                return;
            state = State.on;
            Counter++;
        }

        new public void PowerOff()
        {
            if (state == State.off)
                return;
            state = State.off;
        }
        new public State GetState() => state;

        public void Print(in IDocument document)
        {
            if (GetState() == State.off)
                return;
            PrintCounter++;

            Console.WriteLine(DateTime.Now + @" Print: " + document.GetFileName());

        }
    }
}

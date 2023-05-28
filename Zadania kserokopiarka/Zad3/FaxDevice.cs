using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ver1;
using static ver1.IDevice;
using static ver1.IDocument;

namespace Zad3
{
    public class FaxDevice : BaseDevice, IFax
    {
        new public int Counter { get; private set; } = 0;
        public int FaxCounter { get; private set; } = 0;

       new public void PowerOn()
        {
            if (state == State.on)
                return;
            state = State.on;
            Counter++;
        }

        new public void  PowerOff()
        {
            if (state == State.off)
                return;
            state = State.off;
        }
        public new State GetState() => state;

        public void Fax(in IDocument document)
        {
            if (GetState() == State.off)
                return;
            FaxCounter++;

            Console.WriteLine(DateTime.Now + @" Fax: " + document.GetFileName());

        }
    }
}

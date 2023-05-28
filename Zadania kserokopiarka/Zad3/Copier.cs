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
    public class Copier : BaseDevice
    {
        private readonly Printer printer = new Printer();
        private readonly Scanner scanner = new Scanner();
        public int PrintCounter { get => printer.PrintCounter; }
        public int ScanCounter { get =>scanner.ScanCounter;  }
        public new int Counter { get; private set ; }


        public void PowerOn()
        {
            if (state == State.on)
                return;
            state = State.on;
            Counter++;
        }

        public void PowerOff()
        {
            if (state == State.off)
                return;
            state = State.off;
        }

        public void ScannerOff()
        {
            if (scanner.GetState() == State.off)
                return;
            scanner.PowerOff();
        }
        public void ScannerOn()
        {
            if (scanner.GetState() == State.on)
                return;
            scanner.PowerOn();
        }

        public void PrinterOff()
        {
            if (printer.GetState() == State.off)
                return;
            printer.PowerOff();
        }
        public void PrinterOn()
        {
            if (printer.GetState() == State.on)
                return;
            printer.PowerOn();
        }
        

        public State GetState() => state;

        public void Print(in IDocument document)
        {
            if (GetState() == State.off)
                return;
            printer.Print(document);
        }
        public void Scan(out IDocument document, FormatType formatType = FormatType.JPG) 
        {
            if (GetState() == State.off)
            { document = null;
                return; 
            }
            scanner.Scan(out document, formatType);
        }

        public void ScanAndPrint()
        {
            if (GetState() == State.off) 
            {
                return;
            }
            if (scanner.GetState() == State.off || printer.GetState() == State.off)
            {
                Console.WriteLine("Scanner and printer must be turned on");
                return;
            }
                Scan(out IDocument document, FormatType.JPG);
            Print(document);
        }

    }
}

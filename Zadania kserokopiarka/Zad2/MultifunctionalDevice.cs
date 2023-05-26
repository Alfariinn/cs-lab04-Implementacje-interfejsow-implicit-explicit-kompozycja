using ver1;
using static ver1.IDevice;
using static ver1.IDocument;
namespace Zad2
{
    public class MultifunctionalDevice : BaseDevice, IFax, IPrinter, IScanner
    {
        public int FaxCounter { get; private set; } = 0;
        public  int PrintCounter { get; private set; } = 0;
        public   int ScanCounter { get; private set; } = 0;
        public new int Counter { get; private set; } = 0;

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
        public State GetState() => state;

        public void Print(in IDocument document)
        {
            if (GetState() == State.off)
                return;
            PrintCounter++;

            Console.WriteLine(DateTime.Now + @" Print: " + document.GetFileName());

        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = FormatType.JPG)
        {
            if (GetState() == State.off)
            {
                document = null;
                return;
            }
            ScanCounter++;
            switch (formatType)
            {
                case IDocument.FormatType.PDF:
                    document = new PDFDocument("ImageScan" + ScanCounter + '.' + formatType.ToString().ToLowerInvariant());
                    break;
                case IDocument.FormatType.JPG:
                    document = new ImageDocument("ImageScan" + ScanCounter + '.' + formatType.ToString().ToLowerInvariant());
                    break;
                case IDocument.FormatType.TXT:
                    document = new TextDocument("ImageScan" + ScanCounter + '.' + formatType.ToString().ToLowerInvariant());
                    break;
                default:
                    throw new FormatException("Document format not supported");
            }

            Console.WriteLine(DateTime.Now + @" Scan: " + document.GetFileName());
        }

        public void ScanAndPrint()
        {
            if (GetState() == State.off)
                return;
            Scan(out IDocument document, FormatType.JPG);
            Print(document);
        }




        public void Fax(in IDocument document)
        {
            if (GetState() == State.off)
                return;
            FaxCounter++;

            Console.WriteLine(DateTime.Now + @" Fax: " + document.GetFileName());

        }
    }
}

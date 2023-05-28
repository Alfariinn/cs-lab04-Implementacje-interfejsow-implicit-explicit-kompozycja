using ver1;
using Zad3;

class Program
{
    static void Main()
    {
        Console.WriteLine("Copier\n");
        var xerox = new Copier();
        xerox.PowerOn();
        xerox.PrinterOn();
        xerox.ScannerOn();
        IDocument doc1 = new PDFDocument("aaa.pdf");
        xerox.Print(in doc1);

        IDocument doc2;
        xerox.Scan(out doc2, IDocument.FormatType.PDF);

        xerox.ScanAndPrint();
        System.Console.WriteLine(xerox.Counter);
        System.Console.WriteLine(xerox.PrintCounter);
        System.Console.WriteLine(xerox.ScanCounter);
        Console.WriteLine("====================================================================\nMultidimensionalDevice\n");

        var xxerox = new MultidimensionalDevice();
        xxerox.PowerOn();
        xxerox.PrinterOn();
        xxerox.ScannerOn();
        xxerox.Print(in doc1);

        xxerox.Scan(out doc2, IDocument.FormatType.PDF);

        IDocument doc3 = new PDFDocument("aaa.pdf");
        xxerox.Fax(in doc3);

        xxerox.ScanAndPrint();
        System.Console.WriteLine(xxerox.Counter);
        System.Console.WriteLine(xxerox.PrintCounter);
        System.Console.WriteLine(xxerox.ScanCounter);
    }
}
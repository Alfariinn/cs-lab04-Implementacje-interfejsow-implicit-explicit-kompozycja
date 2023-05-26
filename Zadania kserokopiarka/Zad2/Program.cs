using ver1;
using Zad2;

class Program
{
    static void Main()
    {
        var xerox = new MultifunctionalDevice();
        xerox.PowerOn();
        IDocument doc1 = new PDFDocument("aaa.pdf");
        xerox.Print(in doc1);

        IDocument doc2;
        xerox.Scan(out doc2, IDocument.FormatType.PDF);

        IDocument doc3 = new PDFDocument("aaa.pdf");
        xerox.Fax(in doc3);

        xerox.ScanAndPrint();
        System.Console.WriteLine(xerox.Counter);
        System.Console.WriteLine(xerox.PrintCounter);
        System.Console.WriteLine(xerox.ScanCounter);
    }
}
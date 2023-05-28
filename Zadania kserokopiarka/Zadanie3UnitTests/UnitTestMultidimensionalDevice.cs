using Microsoft.VisualStudio.TestTools.UnitTesting;
using ver1;
using System;
using System.IO;
using Zad3;

namespace Zad3UnitTests
{

    [TestClass]
    public class UnitTestMultidimensionalDevice
    {
        [TestMethod]
        public void MultidimensionalDevice_GetState_StateOff()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOff();

            Assert.AreEqual(IDevice.State.off, MultidimensionalDevice.GetState());
        }

        [TestMethod]
        public void MultidimensionalDevice_GetState_StateOn()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOn();

            Assert.AreEqual(IDevice.State.on, MultidimensionalDevice.GetState());
        }


        // weryfikacja, czy po wywołaniu metody `Print` i włączonej kopiarce w napisie pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Print_DeviceOn()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOn();
            MultidimensionalDevice.PrinterOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                MultidimensionalDevice.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Print_DeviceOff()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOff();
            MultidimensionalDevice.PrinterOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                MultidimensionalDevice.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Scan_DeviceOff()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOn();
            MultidimensionalDevice.ScannerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                MultidimensionalDevice.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Scan_DeviceOn()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOn();
            MultidimensionalDevice.ScannerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                MultidimensionalDevice.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywołanie metody `Scan` z parametrem określającym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void MultidimensionalDevice_Scan_FormatTypeDocument()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOn();
            MultidimensionalDevice.ScannerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                MultidimensionalDevice.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                MultidimensionalDevice.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                MultidimensionalDevice.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie pojawiają się słowa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_ScanAndPrint_DeviceOn()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOn();
            MultidimensionalDevice.ScannerOn();
            MultidimensionalDevice.PrinterOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                MultidimensionalDevice.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // ani słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_ScanAndPrint_DeviceOff()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOff();
            MultidimensionalDevice.PrinterOff();
            MultidimensionalDevice.ScannerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                MultidimensionalDevice.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultidimensionalDevice_PrintCounter()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOn();
            MultidimensionalDevice.PrinterOn();
            MultidimensionalDevice.ScannerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            MultidimensionalDevice.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            MultidimensionalDevice.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            MultidimensionalDevice.Print(in doc3);

            MultidimensionalDevice.PrinterOff();
            MultidimensionalDevice.Print(in doc3);
            MultidimensionalDevice.Scan(out doc1);
            MultidimensionalDevice.PrinterOn();

            MultidimensionalDevice.ScanAndPrint();
            MultidimensionalDevice.ScanAndPrint();

            // 5 wydruków, gdy urządzenie włączone
            Assert.AreEqual(5, MultidimensionalDevice.PrintCounter);
        }

        [TestMethod]
        public void MultidimensionalDevice_ScanCounter()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOn();
            MultidimensionalDevice.PrinterOn();
            MultidimensionalDevice.ScannerOn();

            IDocument doc1;
            MultidimensionalDevice.Scan(out doc1);
            IDocument doc2;
            MultidimensionalDevice.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            MultidimensionalDevice.Print(in doc3);

            MultidimensionalDevice.ScannerOff();
            MultidimensionalDevice.Print(in doc3);
            MultidimensionalDevice.Scan(out doc1);
            MultidimensionalDevice.ScannerOn();

            MultidimensionalDevice.ScanAndPrint();
            MultidimensionalDevice.ScanAndPrint();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(4, MultidimensionalDevice.ScanCounter);
        }

        [TestMethod]
        public void MultidimensionalDevice_PowerOnCounter()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOn();
            MultidimensionalDevice.PowerOn();
            MultidimensionalDevice.PowerOn();

            IDocument doc1;
            MultidimensionalDevice.Scan(out doc1);
            IDocument doc2;
            MultidimensionalDevice.Scan(out doc2);

            MultidimensionalDevice.PowerOff();
            MultidimensionalDevice.PowerOff();
            MultidimensionalDevice.PowerOff();
            MultidimensionalDevice.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            MultidimensionalDevice.Print(in doc3);

            MultidimensionalDevice.PowerOff();
            MultidimensionalDevice.Print(in doc3);
            MultidimensionalDevice.Scan(out doc1);
            MultidimensionalDevice.PowerOn();

            MultidimensionalDevice.ScanAndPrint();
            MultidimensionalDevice.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, MultidimensionalDevice.Counter);
        }
        [TestMethod]
        public void MultidimensionalDevice_Fax_DeviceOff()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOn();
            MultidimensionalDevice.FaxOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                MultidimensionalDevice.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
        [TestMethod]
        public void MultidimensionalDevice_FaxCounter()
        {
            var MultidimensionalDevice = new MultidimensionalDevice();
            MultidimensionalDevice.PowerOn();
            MultidimensionalDevice.FaxOn();
          

            IDocument doc1 = new ImageDocument("aaa.jpg");
            MultidimensionalDevice.Fax(in doc1);
            IDocument doc2 = new ImageDocument("aaa.jpg");
            MultidimensionalDevice.Fax(in doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            MultidimensionalDevice.Print(in doc3);

            MultidimensionalDevice.FaxOff();
            MultidimensionalDevice.Print(in doc3);
            MultidimensionalDevice.Fax(in doc1);
            MultidimensionalDevice.FaxOn();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(2, MultidimensionalDevice.FaxCounter);
        }

    }
}

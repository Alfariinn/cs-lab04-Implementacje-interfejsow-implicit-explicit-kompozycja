using Microsoft.VisualStudio.TestTools.UnitTesting;
using ver1;
using System;
using System.IO;
using Zad2;

namespace ver1UnitTests
{

    public class ConsoleRedirectionToStringWriter : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleRedirectionToStringWriter()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }


    [TestClass]
    public class UnitTestMultifunctionalDevice
    {
        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOff()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOff();

            Assert.AreEqual(IDevice.State.off, MultifunctionalDevice.GetState()); 
        }

        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOn()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOn();

            Assert.AreEqual(IDevice.State.on, MultifunctionalDevice.GetState());
        }


        // weryfikacja, czy po wywołaniu metody `Print` i włączonej kopiarce w napisie pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOn()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using( var consoleOutput = new ConsoleRedirectionToStringWriter() )
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                MultifunctionalDevice.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);   
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOff()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                MultifunctionalDevice.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOff()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                MultifunctionalDevice.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOn()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                MultifunctionalDevice.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywołanie metody `Scan` z parametrem określającym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void MultifunctionalDevice_Scan_FormatTypeDocument()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                MultifunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                MultifunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                MultifunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie pojawiają się słowa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOn()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                MultifunctionalDevice.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // ani słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOff()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                MultifunctionalDevice.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_PrintCounter()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            MultifunctionalDevice.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            MultifunctionalDevice.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            MultifunctionalDevice.Print(in doc3);

            MultifunctionalDevice.PowerOff();
            MultifunctionalDevice.Print(in doc3);
            MultifunctionalDevice.Scan(out doc1);
            MultifunctionalDevice.PowerOn();

            MultifunctionalDevice.ScanAndPrint();
            MultifunctionalDevice.ScanAndPrint();

            // 5 wydruków, gdy urządzenie włączone
            Assert.AreEqual(5, MultifunctionalDevice.PrintCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanCounter()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOn();

            IDocument doc1;
            MultifunctionalDevice.Scan(out doc1);
            IDocument doc2;
            MultifunctionalDevice.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            MultifunctionalDevice.Print(in doc3);

            MultifunctionalDevice.PowerOff();
            MultifunctionalDevice.Print(in doc3);
            MultifunctionalDevice.Scan(out doc1);
            MultifunctionalDevice.PowerOn();

            MultifunctionalDevice.ScanAndPrint();
            MultifunctionalDevice.ScanAndPrint();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(4, MultifunctionalDevice.ScanCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_PowerOnCounter()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOn();
            MultifunctionalDevice.PowerOn();
            MultifunctionalDevice.PowerOn();

            IDocument doc1;
            MultifunctionalDevice.Scan(out doc1);
            IDocument doc2;
            MultifunctionalDevice.Scan(out doc2);

            MultifunctionalDevice.PowerOff();
            MultifunctionalDevice.PowerOff();
            MultifunctionalDevice.PowerOff();
            MultifunctionalDevice.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            MultifunctionalDevice.Print(in doc3);

            MultifunctionalDevice.PowerOff();
            MultifunctionalDevice.Print(in doc3);
            MultifunctionalDevice.Scan(out doc1);
            MultifunctionalDevice.PowerOn();

            MultifunctionalDevice.ScanAndPrint();
            MultifunctionalDevice.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, MultifunctionalDevice.Counter);
        }
        [TestMethod]
        public void MultifunctionalDevice_Fax_DeviceOff()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                MultifunctionalDevice.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
        [TestMethod]
        public void MultifunctionalDevice_FaxCounter()
        {
            var MultifunctionalDevice = new MultifunctionalDevice();
            MultifunctionalDevice.PowerOn();

            IDocument doc1 = new ImageDocument("aaa.jpg");
            MultifunctionalDevice.Fax(in doc1);
            IDocument doc2 = new ImageDocument("aaa.jpg");
            MultifunctionalDevice.Fax(in doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            MultifunctionalDevice.Print(in doc3);

            MultifunctionalDevice.PowerOff();
            MultifunctionalDevice.Print(in doc3);
            MultifunctionalDevice.Fax(in doc1);
            MultifunctionalDevice.PowerOn();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(2, MultifunctionalDevice.FaxCounter);
        }

    }
}

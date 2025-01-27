﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ver1;
using static ver1.IDevice;
using static ver1.IDocument;

namespace Zad1
{
    public sealed class Copier : BaseDevice, IPrinter, IScanner
    {
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;
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

        public void Scan(out IDocument document, FormatType formatType =FormatType.JPG)
        {
            if (GetState() == State.off)
            {
                document = null;
                return; 
            }
            ScanCounter++;
            switch (formatType) 
            {
                case FormatType.PDF:
                    document = new PDFDocument("ImageScan" + ScanCounter + '.' + formatType.ToString().ToLowerInvariant());
                    break;
                case FormatType.JPG:
                    document = new ImageDocument("ImageScan" + ScanCounter + '.' + formatType.ToString().ToLowerInvariant());
                    break;
                case FormatType.TXT:
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
    }






}

using Pechkin;
using Pechkin.Synchronized;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Web;

namespace Rendezvous.Utilities
{
    public static class Generator
    {
        public static string CreatePassword(){
            return System.Web.Security.Membership.GeneratePassword(12, 1);
        }

        public static byte[] CreatePDF(string Ref)
        {
            // create global configuration object
            GlobalConfig gc = new GlobalConfig();

            // set it up using fluent notation
            gc.SetMargins(new Margins(60, 60, 0, 10))
              .SetDocumentTitle("Test document")
              .SetPaperSize(PaperKind.A4);

            // create converter
            IPechkin pechkin = new SynchronizedPechkin(gc);

            // subscribe to events
            //pechkin.Begin += OnBegin;
            //pechkin.Error += OnError;
            //pechkin.Warning += OnWarning;
            //pechkin.PhaseChanged += OnPhase;
            //pechkin.ProgressChanged += OnProgress;
            //pechkin.Finished += OnFinished;

            // create document configuration object
            ObjectConfig oc = new ObjectConfig();

            // and set it up using fluent notation too
            oc.SetCreateExternalLinks(false)
              .SetFallbackEncoding(Encoding.ASCII)
              .SetLoadImages(true).SetRenderDelay(1000)
              .SetRunJavascript(true).SetPrintBackground(true)
              .SetCreateExternalLinks(true)
              .SetPageUri("http://localhost:1838/PDFConvert.aspx?ref=" + Ref);

            // convert document
            byte[] pdfBuf = pechkin.Convert(oc);

            return pdfBuf;
        }

        public static byte[] CreatePDFFromURL(string url)
        {
            // create global configuration object
            GlobalConfig gc = new GlobalConfig();

            // set it up using fluent notation
            gc.SetMargins(new Margins(60, 60, 0, 40))
              .SetDocumentTitle("Test document")
              .SetPaperSize(PaperKind.A4);

            // create converter
            IPechkin pechkin = new SynchronizedPechkin(gc);

            // subscribe to events
            //pechkin.Begin += OnBegin;
            //pechkin.Error += OnError;
            //pechkin.Warning += OnWarning;
            //pechkin.PhaseChanged += OnPhase;
            //pechkin.ProgressChanged += OnProgress;
            //pechkin.Finished += OnFinished;

            // create document configuration object
            ObjectConfig oc = new ObjectConfig();

            // and set it up using fluent notation too
            oc.SetCreateExternalLinks(false)
              .SetFallbackEncoding(Encoding.ASCII)
              .SetLoadImages(true).SetRenderDelay(1000)
              .SetRunJavascript(true).SetPrintBackground(true)
              .SetCreateExternalLinks(true)
              .SetPageUri(url);

            // convert document
            byte[] pdfBuf = pechkin.Convert(oc);

            return pdfBuf;
        }

        public static byte[] CreatePDFFromURL(string url, int[] margin)
        {
            // create global configuration object
            GlobalConfig gc = new GlobalConfig();

            // set it up using fluent notation
            gc.SetMargins(new Margins(margin[0], margin[1], margin[2], margin[3]))
              .SetDocumentTitle("Test document")
              .SetPaperSize(PaperKind.A4);

            // create converter
            IPechkin pechkin = new SynchronizedPechkin(gc);

            // subscribe to events
            //pechkin.Begin += OnBegin;
            //pechkin.Error += OnError;
            //pechkin.Warning += OnWarning;
            //pechkin.PhaseChanged += OnPhase;
            //pechkin.ProgressChanged += OnProgress;
            //pechkin.Finished += OnFinished;

            // create document configuration object
            ObjectConfig oc = new ObjectConfig();

            // and set it up using fluent notation too
            oc.SetCreateExternalLinks(false)
              .SetFallbackEncoding(Encoding.ASCII)
              .SetLoadImages(true).SetRenderDelay(1000)
              .SetRunJavascript(true).SetPrintBackground(true)
              .SetCreateExternalLinks(true)
              .SetPageUri(url);

            // convert document
            byte[] pdfBuf = pechkin.Convert(oc);

            return pdfBuf;
        }
    }
}
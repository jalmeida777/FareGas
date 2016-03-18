using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WebForms;

public partial class ImprimirFacturaPercival : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            hfIdComprobante.Value = Request.QueryString["i_IdComprobante"];
            ReportViewer1.LocalReport.Refresh();
        }
    }

    private int m_currentPageIndex;
    private IList<Stream> m_streams;

    private Stream CreateStream(string name,
      string fileNameExtension, Encoding encoding,
      string mimeType, bool willSeek)
    {
        Stream stream = new MemoryStream();
        m_streams.Add(stream);
        return stream;
    }

    private void PrintPage(object sender, PrintPageEventArgs ev)
    {
        Metafile pageImage = new
           Metafile(m_streams[m_currentPageIndex]);

        // Adjust rectangular area with printer margins.
        Rectangle adjustedRect = new Rectangle(
            ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
            ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
            800,
            650);

        // Draw a white background for the report
        ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

        // Draw the report content
        ev.Graphics.DrawImage(pageImage, adjustedRect);

        // Prepare for the next page. Make sure we haven't hit the end.
        m_currentPageIndex++;
        ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
    }

    private void Export(LocalReport report)
    {
        string deviceInfo =
          @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.66142in</PageWidth>
                <PageHeight>9.29921in</PageHeight>
                <MarginTop>0.00in</MarginTop>
                <MarginLeft>0.00in</MarginLeft>
                <MarginRight>0.00in</MarginRight>
                <MarginBottom>0.00in</MarginBottom>
            </DeviceInfo>";
        Warning[] warnings;
        m_streams = new List<Stream>();
        report.Render("Image", deviceInfo, CreateStream,
           out warnings);
        foreach (Stream stream in m_streams)
            stream.Position = 0;
    }

    private void Print()
    {
        if (m_streams == null || m_streams.Count == 0)
            throw new Exception("Error: no stream to print.");
        PrintDocument printDoc = new PrintDocument();
        if (!printDoc.PrinterSettings.IsValid)
        {
            throw new Exception("Error: cannot find the default printer.");
        }
        else
        {
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            printDoc.PrinterSettings.FromPage = 0;
            printDoc.PrinterSettings.ToPage = 0;
            m_currentPageIndex = 0;
            printDoc.Print();
        }
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        int i_IdMenu = int.Parse(Request.QueryString["IdMenu"]);
        Response.Redirect("~/CrearComprobante.aspx?i_IdComprobante=" + hfIdComprobante.Value + "&IdMenu=" + i_IdMenu);
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        Export(ReportViewer1.LocalReport);
        Print();
    }
}
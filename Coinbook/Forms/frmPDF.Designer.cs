namespace Coinbook
{
  partial class frmPDF
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings pdfViewerPrinterSettings1 = new Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPDF));
      this.pdfViewer = new Syncfusion.Windows.Forms.PdfViewer.PdfViewerControl();
      this.SuspendLayout();
      // 
      // pdfViewer
      // 
      this.pdfViewer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pdfViewer.EnableNotificationBar = true;
      this.pdfViewer.IsBookmarkEnabled = true;
      this.pdfViewer.Location = new System.Drawing.Point(0, 0);
      this.pdfViewer.Name = "pdfViewer";
      this.pdfViewer.PageBorderThickness = 1;
      pdfViewerPrinterSettings1.PrintLocation = ((System.Drawing.PointF)(resources.GetObject("pdfViewerPrinterSettings1.PrintLocation")));
      this.pdfViewer.PrinterSettings = pdfViewerPrinterSettings1;
      this.pdfViewer.ScrollDisplacementValue = 0;
      this.pdfViewer.ShowHorizontalScrollBar = true;
      this.pdfViewer.ShowToolBar = true;
      this.pdfViewer.ShowVerticalScrollBar = true;
      this.pdfViewer.Size = new System.Drawing.Size(865, 831);
      this.pdfViewer.TabIndex = 0;
      this.pdfViewer.Text = "pdfViewerControl1";
      this.pdfViewer.ZoomMode = Syncfusion.Windows.Forms.PdfViewer.ZoomMode.Default;
      // 
      // frmPDF
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(865, 831);
      this.Controls.Add(this.pdfViewer);
      this.Name = "frmPDF";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "frmPDF";
      this.ResumeLayout(false);

    }

    #endregion

    private Syncfusion.Windows.Forms.PdfViewer.PdfViewerControl pdfViewer;
  }
}
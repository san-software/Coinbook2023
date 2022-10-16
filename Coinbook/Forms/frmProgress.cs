using System.Drawing;
using System.Windows.Forms;

namespace SAN.Control
{
    public partial class frmProgress : Form
    {
        public frmProgress()
        {
            InitializeComponent();
        }

        #region Progressbar 1
        public ContentAlignment Bar1TextAlignment {set => bar1.TextAlignment = value;}
        public ProgressBarEx.ProgressbarStyleEx Bar1Style { set => bar1.Style = value;}
        public int Bar1Value { set => bar1.Value = value; }
        public int Bar1MaxValue { set => bar1.MaxValue = value; }
        public int Bar1MinValue { set => bar1.MinValue = value; }
        public string Bar1Text { set => bar1.Text = value; }
        public Color Bar1BorderColor { set => bar1.BorderColor = value; }
        public Color Bar1BarColor { set => bar1.BarColor = value; }
        public Color Bar1ForeColor { set => bar1.ForeColor = value; }
        public byte Bar1BlockDistance { set => bar1.BlockDistance = value; }
        public byte Bar1BlockWidth { set => bar1.BlockWidth = value; }
        public ProgressBarEx.GradientMode Bar1GradientStyle { set => bar1.GradientStyle = value; }
        public bool Bar1TextShadow { set => bar1.TextShadow = value; }
        public byte Bar1TextShadowAlpha { set => bar1.TextShadowAlpha = value; }
        #endregion

        #region Progressbar 2
        public ContentAlignment Bar2TextAlignment { set => bar2.TextAlignment = value; }
        public ProgressBarEx.ProgressbarStyleEx Bar2Style { set => bar2.Style = value; }
        public int Bar2Value { set => bar2.Value = value; }
        public int Bar2MaxValue { set => bar2.MaxValue = value; }
        public int Bar2MinValue { set => bar2.MinValue = value; }
        public string Bar2Text { set => bar2.Text = value; }
        public Color Bar2BorderColor { set => bar2.BorderColor = value; }
        public Color Bar2BarColor { set => bar2.BarColor = value; }
        public Color Bar2ForeColor { set => bar2.ForeColor = value; }
        public byte Bar2BlockDistance { set => bar2.BlockDistance = value; }
        public byte Bar2BlockWidth { set => bar2.BlockWidth = value; }
        public ProgressBarEx.GradientMode Bar2GradientStyle { set => bar2.GradientStyle = value; }
        public bool Bar2TextShadow { set => bar2.TextShadow = value; }
        public byte Bar2TextShadowAlpha { set => bar2.TextShadowAlpha = value; }
        #endregion
    }
}

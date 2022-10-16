using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace SAN.UI.DataGridView
{
	/// <summary>
	/// Summary description for ImageHelper.
	/// </summary>
	public class ImageHelper
	{
		public ImageHelper()
		{
		
		}
		/// <summary>
		/// Konvertiert ein Bild in ein Graustufen-Bild 
		/// </summary>
		/// <param name="image">Das so konvertierende Bild</param>
		/// <returns>Gibt eine Referenz auf ein Bitmap-Objekt zurück, das das in
		/// Graustufen konvertierte Bild enthält</returns>
		public static Bitmap CreateGrayscaledBitmap(Image image)
		{
			// Neues Bitmap-Objekt mit den Ausmaßen der Quelle erzeugen
			Bitmap bitmap = new Bitmap(image.Width, image.Height);

			// ColorMatrix für die Transformation der Farben erzeugen
			ColorMatrix colorMatrix = new ColorMatrix(new float[][] {
					new float[] {0.3F, 0.3F, 0.3F, 0, 0},
					new float[] {0.59F, 0.59F, 0.59F, 0, 0},
					new float[] {0.11F, 0.11F, 0.11F, 0, 0},
					new float[] {0, 0, 0, 1, 0},
					new float[] {0, 0, 0, 0, 1}
					});

			// Grafik auf dem Bitmap-Objekt ausgeben und dabei ein neues ImageAttributes-Objekt mit der ColorMatrix übergeben 
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.SetColorMatrix(colorMatrix);
			using (Graphics g = Graphics.FromImage(bitmap))
			{
				g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
			}

			return bitmap;
		}

		public static Image Logo
		{
			get;
			set;
		}

	}
}






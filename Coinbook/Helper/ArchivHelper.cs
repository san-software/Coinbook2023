using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Helper
{
    public static class ArchivHelper
    {
        /// <summary>
        /// Diese Funktion dekomprimiert eine ZIP-Datei.
        /// </summary>
        /// <param name="FileName">Die Datei die dekomprimiert werden soll.</param>
        /// <param name="OutputDir">Das Verzeichnis in dem die Dateien dekomprimiert werden sollen.</param>
        public static void DecompressFile(string FileName, string OutputDir = "", string outputFile = "", string password = "")
        {
            FileStream ZFS = new FileStream(FileName, FileMode.Open);
            ICSharpCode.SharpZipLib.Zip.ZipInputStream ZIN = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(ZFS);

            ICSharpCode.SharpZipLib.Zip.ZipEntry ZipEntry = default(ICSharpCode.SharpZipLib.Zip.ZipEntry);

            byte[] Buffer = new byte[4097];
            int ByteLen = 0;
            FileStream FS = null;

            string InZipDirName = null;
            string InZipFileName = null;
            string TargetFileName = null;

            if (password != string.Empty)
                ZIN.Password = password;

            do
            {
                ZipEntry = ZIN.GetNextEntry();
                if (ZipEntry == null) break;

                InZipDirName = Path.GetDirectoryName(ZipEntry.Name);
                InZipFileName = Path.GetFileName(ZipEntry.Name);

                if (outputFile == InZipFileName || outputFile == string.Empty)
                {
                    if (!Directory.Exists(Path.Combine(OutputDir, InZipDirName)))
                        Directory.CreateDirectory(Path.Combine(CoinbookHelper.DataPath, InZipDirName));

                    if (InZipDirName == String.Empty)
                        TargetFileName = Path.Combine(OutputDir, InZipFileName);
                    else
                        TargetFileName = Path.Combine(Path.Combine(CoinbookHelper.DataPath, InZipDirName), InZipFileName);

                    if (InZipFileName != String.Empty)
                    {
                        FS = new FileStream(TargetFileName, FileMode.Create);
                        do
                        {
                            ByteLen = ZIN.Read(Buffer, 0, Buffer.Length);
                            FS.Write(Buffer, 0, ByteLen);
                        }
                        while (!(ByteLen <= 0));
                        FS.Close();
                    }

                    //break;
                }
            }
            while (true);

            ZIN.Close();
            ZFS.Close();
        }

        public static string Zipfile(string modul, string jahr)
        {
            string zipfile = modul.Replace(" ", "_") + "-" + jahr + ".zip";
            zipfile = zipfile.Replace(" ", "_");
            zipfile = zipfile.Replace("ä", "ae");
            zipfile = zipfile.Replace("Ö", "Oe");

            return zipfile;
        }
    }
}

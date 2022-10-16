using Coinbook.Enumerations;
using Coinbook.Helper;
using SAN.UIControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbook.Import
{
    public class Class1 :IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void xxx(string katalogPath)
        {
            Directory.CreateDirectory(katalogPath);

            ModulUpdates();

            DatabaseHelper.LiteDatabase.Drop("tblAera");
            DatabaseHelper.LiteDatabase.Drop("tblDetails");
            DatabaseHelper.LiteDatabase.Drop("tblDetailTexteDE");
            DatabaseHelper.LiteDatabase.Drop("tblDetailTexteEN");
            DatabaseHelper.LiteDatabase.Drop("tblGebiet");
            DatabaseHelper.LiteDatabase.Drop("tblKatalog");
            DatabaseHelper.LiteDatabase.Drop("tblPraegeanstalt");

            if (DatabaseHelper.LiteDatabase.CollectionExists("tblBestand"))
            {
                var bestand = DatabaseHelper.LiteDatabase.ReadBestand();
                DatabaseHelper.LiteDatabase.BulkInsertBestand(bestand);
                DatabaseHelper.LiteDatabase.Drop("tblBestand");

                var sammlung = DatabaseHelper.LiteDatabase.ReadSammlung();
                DatabaseHelper.LiteDatabase.BulkInsertSammlung(sammlung);
                DatabaseHelper.LiteDatabase.Drop("tblSammlung");

                var eigeneBilder = DatabaseHelper.LiteDatabase.ReadEigeneBilder();
                DatabaseHelper.LiteDatabase.BulkInsertEigeneBilder(eigeneBilder);
                DatabaseHelper.LiteDatabase.Drop("tblEigeneBilder");

                var eigeneKatNr = DatabaseHelper.LiteDatabase.ReadKatalogNummern();
                DatabaseHelper.LiteDatabase.BulkInsertEigeneKatNr(eigeneKatNr);
                DatabaseHelper.LiteDatabase.Drop("tblEigeneKatNr");

                var preise = DatabaseHelper.LiteDatabase.ReadEigenePreise();
                DatabaseHelper.LiteDatabase.BulkInsertPreise(preise);
                DatabaseHelper.LiteDatabase.Drop("tblPreise");

                var auktionen = DatabaseHelper.LiteDatabase.ReadAuktionen();
                DatabaseHelper.LiteDatabase.BulkInsertAuktionen(auktionen);
                DatabaseHelper.LiteDatabase.Drop("tblAuktionen");
            }
        }

        public void ModulUpdates()
        {
            MessageBoxNonmodal messageBox = new MessageBoxNonmodal("Coinbook ModulVerwaltung wird geladen", "Coinbook", 10);
            DatabaseHelper.LiteDatabase.ClearDownloads();

            CoinbookHelper.StartProgram("Coinbook.Modulverwaltung", enmPrograms.ModulImport.ToString());

            messageBox.Close();
        }

        }

}

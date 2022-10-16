using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OleDB;
using System.Threading;
using System.IO;
using Coinbook.Enumerations;

namespace Coinbook.Konvert
{
  public partial class frmKonvert : Form
  {
    private readonly OleDBZugriff database = new OleDBZugriff();
		const int counter = 3;
		bool weiter = true;
        string[] version;

     public frmKonvert()
    {
      InitializeComponent();

      database.Tabelle = "tblcb_db";

      string text = "Die Datenbank von Coinbook muß jetzt auf ein neues Format konvertiert werden." 
        + "Diese Konvertierung kann (je nach Anzahl Ihrer Sammlungsgebiete) relativ viel Zeit benötigen" + Environment.NewLine + Environment.NewLine
        + "Bitte haben Sie Geduld und schalten Sie während der Konvertierung den PC nicht aus!";

      txtAnzeige.Text = text;
    }

        private void bgwVersion26_DoWork(object sender, DoWorkEventArgs e)
        {
            switch ((int)e.Argument)
            {
                case 1:
                    konvertVersion26();
                    break;

                case 2:
                    konvertVersion45();
                    break;
            }
        }   

        private void konvertVersion26()
        { 

            int progress = 0;

            String cmd;
            DataTable dt;

            //Ergänze tblcb_db
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Ergänze tblcb_db");

            cmd = "Alter Table tblcb_db add column AusserKurs Text(20)";
            if (!database.FieldExists("tblcb_db", "AusserKurs"))
                database.Execute(cmd);

            cmd = "Alter Table tblcb_db add column InKurs Text(20)";
            if (!database.FieldExists("tblcb_db", "InKurs"))
                database.Execute(cmd);

            cmd = "Alter Table tblcb_db add column geprägt Text(20)";
            if (!database.FieldExists("tblcb_db", "geprägt"))
                database.Execute(cmd);

            cmd = "Alter Table tblcb_db add column Aversbeschreibung Memo";
            if (!database.FieldExists("tblcb_db", "Aversbeschreibung"))
                database.Execute(cmd);

            cmd = "Alter Table tblcb_db add column Besonderheit Memo";
            if (!database.FieldExists("tblcb_db", "Besonderheit"))
                database.Execute(cmd);

            cmd = "Alter Table tblcb_db add column Reversbeschreibung Memo";
            if (!database.FieldExists("tblcb_db", "Reversbeschreibung"))
                database.Execute(cmd);

            cmd = "Alter Table tblcb_db add column Kommentar Memo";
            if (!database.FieldExists("tblcb_db", "Kommentar"))
                database.Execute(cmd);

            cmd = "Alter Table tblcb_db add column Motiv Text(255)";
            if (!database.FieldExists("tblcb_db", "Motiv"))
                database.Execute(cmd);

            cmd = "Alter Table tblcb_db add column Rand Text(255)";
            if (!database.FieldExists("tblcb_db", "Rand"))
                database.Execute(cmd);

            cmd = "Alter Table tblcb_db add column Ausgabeanlass Text(255)";
            if (!database.FieldExists("tblcb_db", "Ausgabeanlass"))
                database.Execute(cmd);

            cmd = "Alter Table tblcb_db add column ÄhnlicheMotive Text(255)";
            if (!database.FieldExists("tblcb_db", "ÄhnlicheMotive"))
                database.Execute(cmd);

            cmd = "Alter Table tblcb_db add column Material Text(255)";
            if (!database.FieldExists("tblcb_db", "Material"))
                database.Execute(cmd);

            cmd = "Alter Table tblcb_db add column Legierung Text(255)";
            if (!database.FieldExists("tblcb_db", "Legierung"))
                database.Execute(cmd);

            cmd = "Alter Table tblcb_db add column AversEntwurf Text(255)";
            if (!database.FieldExists("tblcb_db", "AversEntwurf"))
                database.Execute(cmd);

            cmd = "Alter Table tblcb_db add column ReversEntwurf Text(255)";
            if (!database.FieldExists("tblcb_db", "ReversEntwurf"))
                database.Execute(cmd);

            cmd = "Alter Table tblcb_db add column Picture Text(50)";
            if (!database.FieldExists("tblcb_db", "Picture"))
                database.Execute(cmd);

            //Erzeuge tblTexte
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Ergänze tblTexte");
            cmd = "Create Table tblTexte (Guid Text(36), typ Text(30), sprache Text(3), [Text] Text(255), Text2 Text(255), Nation long)";
            database.Execute(cmd);

            //Erzeuge tblModule
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Ergänze tblModule");
            cmd = "Create Table tblModule (id long, typ Text(30), sprache Text(3), [Text] Text(100), Nation long)";
            database.Execute(cmd);

            //Konvertiere Ausser Kurs
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Ausser Kurs");
            dt = database.GetDataTable("SELECT Distinct tblAusserKurs.ID, tblAusserKurs.AusserKurs FROM tblCB_DB INNER JOIN tblAusserKurs ON tblCB_DB.ausserKursSeit_ID = tblAusserKurs.ID WHERE tblAusserKurs.AusserKurs<>''");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                cmd = "Update tblcb_db set AusserKurs = '" + dt.Rows[i]["AusserKurs"].ToString() + "' where AusserKursSeit_id=" + dt.Rows[i]["id"].ToString();
                database.Execute(cmd);
            }

            //Konvertiere In Kurs
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere In Kurs");
            dt = database.GetDataTable("SELECT Distinct tblInKurs.ID, tblInKurs.InKurs FROM tblInKurs INNER JOIN tblCB_DB ON tblInKurs.ID = tblCB_DB.inKursSeit_ID WHERE tblInKurs.InKurs<>''");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                cmd = "Update tblcb_db set InKurs = '" + dt.Rows[i]["InKurs"].ToString() + "' where InKursSeit_id=" + dt.Rows[i]["id"].ToString();
                database.Execute(cmd);
            }

            //Konvertiere Bilder
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Bilder");
            dt = database.GetDataTable("SELECT Distinct tblDateiName.ID, tblDateiName.DateiName FROM tblDateiName INNER JOIN tblCB_DB ON tblDateiName.ID = tblCB_DB.DateiName_ID");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                cmd = "Update tblcb_db set Picture = '" + dt.Rows[i]["DateiName"].ToString() + "' where Dateiname_ID=" + dt.Rows[i]["id"].ToString();
                database.Execute(cmd);
            }

            //Konvertiere Prägedatum
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Prägedatum");
            dt = database.GetDataTable("SELECT Distinct tblGeprVB.ID, tblGeprVB.GeprV_B FROM tblGeprVB INNER JOIN tblCB_DB ON tblGeprVB.ID = tblCB_DB.gepraegtVonBis_ID WHERE tblGeprVB.GeprV_B<>''");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                cmd = "Update tblcb_db set Geprägt = '" + dt.Rows[i]["GeprV_B"].ToString() + "' where gepraegtVonBis_ID=" + dt.Rows[i]["id"].ToString();
                database.Execute(cmd);
            }

            //Konvertiere Aversbeschreibung
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Aversbeschreibung");
            saveTexte("tblAversbeschreibung", "Aversbeschreibung_ID", "Aversbeschreibung");

            dt = database.GetDataTable("Select * from tblTexte where typ='Aversbeschreibung' and sprache='DE'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                cmd = "update tblCB_DB set Aversbeschreibung = '" + dt.Rows[i]["text"].ToString().Replace("'", "''") + "' where repid='" + dt.Rows[i]["guid"].ToString() + "'";
                database.Execute(cmd);
            }

            //Konvertiere Reversbeschreibung
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Reversbeschreibung");
            saveTexte("tblReversbeschreibung", "Reversbeschreibung_ID", "Reversbeschreibung");

            dt = database.GetDataTable("Select * from tblTexte where typ='Reversbeschreibung' and sprache='DE'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                cmd = "update tblCB_DB set Reversbeschreibung = '" + dt.Rows[i]["text"].ToString().Replace("'", "''") + "' where repid='" + dt.Rows[i]["guid"].ToString() + "'";
                database.Execute(cmd);
            }

            //Konvertiere Besonderheiten
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Besonderheiten");
            saveTexte("tblBesonderheiten", "Besonderheit_ID", "Besonderheit");

            dt = database.GetDataTable("Select * from tblTexte where typ='Besonderheit' and sprache='DE'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                cmd = "update tblCB_DB set Besonderheit = '" + dt.Rows[i]["text"].ToString().Replace("'", "''") + "' where repid='" + dt.Rows[i]["guid"].ToString() + "'";
                database.Execute(cmd);
            }

            //Konvertiere Kommentare
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Kommentare");
            saveTexte("tblKommentare", "Kommentare_ID", "Kommentar");

            dt = database.GetDataTable("Select * from tblTexte where typ='Kommentar' and sprache='DE'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                cmd = "update tblCB_DB set Kommentar = '" + dt.Rows[i]["text"].ToString().Replace("'", "''") + "' where repid='" + dt.Rows[i]["guid"].ToString() + "'";
                database.Execute(cmd);
            }

            //Konvertiere Motive
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Motive");
            saveTexte("tblMotiv", "Motiv_ID", "Motiv");

            dt = database.GetDataTable("Select * from tblTexte where typ='Motiv' and sprache='DE'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                cmd = "update tblCB_DB set Motiv = '" + dt.Rows[i]["text"].ToString().Replace("'", "''") + "' where repid='" + dt.Rows[i]["guid"].ToString() + "'";
                database.Execute(cmd);
            }

            //Konvertiere Rand
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Rand");
            saveTexte("tblRand", "Rand_ID", "Rand");

            dt = database.GetDataTable("Select * from tblTexte where typ='Rand' and sprache='DE'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                cmd = "update tblCB_DB set Rand = '" + dt.Rows[i]["text"].ToString().Replace("'", "''") + "' where repid='" + dt.Rows[i]["guid"].ToString() + "'";
                database.Execute(cmd);
            }

            //Konvertiere Ausgabeanlass
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Ausgabeanlass");
            saveTexte("tblAusgabeanlass", "Ausgabeanlass_ID", "Ausgabeanlass");

            dt = database.GetDataTable("Select * from tblTexte where typ='Ausgabeanlass' and sprache='DE'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                cmd = "update tblCB_DB set Ausgabeanlass = '" + dt.Rows[i]["text"].ToString().Replace("'", "''") + "' where repid='" + dt.Rows[i]["guid"].ToString() + "'";
                database.Execute(cmd);
            }

            //Konvertiere Ähnliche Motive
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Ähnliche Motive");
            saveTexte("tblAehnlicheMotive", "AehnlicheMotive_ID", "ÄhnlicheMotive");

            dt = database.GetDataTable("Select * from tblTexte where typ='ÄhnlicheMotive' and sprache='DE'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                cmd = "update tblCB_DB set ÄhnlicheMotive = '" + dt.Rows[i]["text"].ToString().Replace("'", "''") + "' where repid='" + dt.Rows[i]["guid"].ToString() + "'";
                database.Execute(cmd);
            }

            //Konvertiere Material
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Material");
            saveTexte("tblMaterial", "Material_ID", "Material");

            dt = database.GetDataTable("Select * from tblTexte where typ='Material' and sprache='DE'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                cmd = "update tblCB_DB set Material = '" + dt.Rows[i]["text"].ToString().Replace("'", "''") + "' where repid='" + dt.Rows[i]["guid"].ToString() + "'";
                database.Execute(cmd);
            }

            //Konvertiere Legierung 
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Legierung");
            dt = database.GetDataTable("SELECT Distinct tblLegierung.ID, tblLegierung.DE_DE FROM tblLegierung INNER JOIN tblCB_DB ON tblLegierung.ID = tblCB_DB.Legierung_ID");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                cmd = "update tblCB_DB set Legierung = '" + dt.Rows[i]["Bezeichnung"].ToString().Replace("'", "''") + "' where Legierung_ID=" + dt.Rows[i]["id"].ToString();
                database.Execute(cmd);
            }

            //Konvertiere Aversentwurf 
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Aversentwurf");
            dt = database.GetDataTable("SELECT Distinct tblAversentwurf.ID, tblAversentwurf.DE_DE FROM tblAversentwurf INNER JOIN tblCB_DB ON tblAversentwurf.ID = tblCB_DB.Aversentwurf_ID");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                cmd = "update tblCB_DB set Aversentwurf = '" + dt.Rows[i]["Bezeichnung"].ToString().Replace("'", "''") + "' where Aversentwurf_id=" + dt.Rows[i]["id"].ToString();
                database.Execute(cmd);
            }

            //Konvertiere Reversentwurf 
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Reversentwurf");
            dt = database.GetDataTable("SELECT Distinct tblReversentwurf.ID, tblReversentwurf.DE_DE FROM tblReversentwurf INNER JOIN tblCB_DB ON tblReversentwurf.ID = tblCB_DB.Reversentwurf_ID");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                cmd = "update tblCB_DB set Reversentwurf = '" + dt.Rows[i]["Bezeichnung"].ToString().Replace("'", "''") + "' where Reversentwurf_id=" + dt.Rows[i]["id"].ToString();
                database.Execute(cmd);
            }

            cmd = "Delete * from tblTexte where [Text]=''";
            database.Execute(cmd);

            //Konvertiere Create Indizes
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Create Indizes");
            cmd = "Create Index Guid on tblTexte (Guid)";
            database.Execute(cmd);

            cmd = "Create Index typ on tblTexte (typ)";
            database.Execute(cmd);

            cmd = "Create Index sprache on tblTexte (sprache)";
            database.Execute(cmd);

            cmd = "Create Index [Text] on tblTexte ([text])";
            database.Execute(cmd);

            //Konvertiere Auflage
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Auflage");

            cmd = "Alter Table tblcb_db add column temp Text(15)";
            database.Execute(cmd);

            cmd = "Update tblcb_db set temp = format(AuflagePP,'###,###,###')";
            database.Execute(cmd);

            cmd = "Update tblcb_db set temp = 'n/a' where auflagepp = 0 and Erh_PP_Preis = 0";
            database.Execute(cmd);

            cmd = "Alter Table tblcb_db Alter column AuflagePP Text(15)";
            database.Execute(cmd);

            cmd = "Update tblcb_db set AuflagePP = temp";
            database.Execute(cmd);

            cmd = "Update tblcb_db set temp = format(AuflageSTH,'###,###,###')";
            database.Execute(cmd);

            cmd = "Update tblcb_db set temp = 'n/a' where auflagesth = 0 and Erh_STH_Preis = 0";
            database.Execute(cmd);

            cmd = "Alter Table tblcb_db Alter column AuflageSTH Text(15)";
            database.Execute(cmd);

            cmd = "Update tblcb_db set AuflageSTH = temp";
            database.Execute(cmd);

            cmd = "Update tblcb_db set temp = format(Auflage,'###,###,###')";
            database.Execute(cmd);

            cmd = "Alter Table tblcb_db Alter column Auflage Text(15)";
            database.Execute(cmd);

            cmd = "Update tblcb_db set Auflage = temp";
            database.Execute(cmd);

            cmd = "Alter Table tblcb_db drop column temp";
            database.Execute(cmd);

            cmd = "UPDATE tblTexte INNER JOIN tblCB_DB ON tblTexte.Guid = tblCB_DB.Repid SET tblTexte.Nation = tblCB_DB.Nation_ID";
            database.Execute(cmd);

            //Konvertiere Nationen
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Nationen");
            dt = database.GetDataTable("Select * from tblNation");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                string text = dt.Rows[i]["Bezeichnung"].ToString().Replace("'", "''");
                cmd = "insert into tblModule (typ,id,sprache,[text], Nation) Values('Nation'," + dt.Rows[i]["ID"].ToString() + ",'DE','" + text + "'," + dt.Rows[i]["ID"].ToString() + ")";
                database.Execute(cmd);

                text = dt.Rows[i]["EN_GB"].ToString().Replace("'", "''");
                cmd = "insert into tblModule (typ,id,sprache,[text], Nation) Values('Nation'," + dt.Rows[i]["ID"].ToString() + ",'EN','" + text + "'," + dt.Rows[i]["ID"].ToString() + ")";
                database.Execute(cmd);
            }

            //Konvertiere Äras
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Äras");
            dt = database.GetDataTable("Select * from tblAera");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                string text = dt.Rows[i]["Bezeichnung"].ToString().Replace("'", "''");
                cmd = "insert into tblModule (typ,id,sprache,[text], Nation) Values('Ära'," + dt.Rows[i]["ID"].ToString() + ",'DE','" + text + "'," + dt.Rows[i]["NAT"].ToString() + ")";
                database.Execute(cmd);

                text = dt.Rows[i]["EN_GB"].ToString().Replace("'", "''");
                cmd = "insert into tblModule (typ,id,sprache,[text], nation) Values('Ära'," + dt.Rows[i]["ID"].ToString() + ",'EN','" + text + "'," + dt.Rows[i]["NAT"].ToString() + ")";
                database.Execute(cmd);
            }

            //Konvertiere Gebiete
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Konvertiere Gebiete");
            dt = database.GetDataTable("Select * from tblGebiet");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");
                string text = dt.Rows[i]["Bezeichnung"].ToString().Replace("'", "''");
                cmd = "insert into tblModule (typ,id,sprache,[text], Nation) Values('Gebiet'," + dt.Rows[i]["ID"].ToString() + ",'DE','" + text + "'," + dt.Rows[i]["NAT"].ToString() + ")";
                database.Execute(cmd);

                text = dt.Rows[i]["EN_GB"].ToString().Replace("'", "''");
                cmd = "insert into tblModule (typ,id,sprache,[text],Nation) Values('Gebiet'," + dt.Rows[i]["ID"].ToString() + ",'EN','" + text + "'," + dt.Rows[i]["NAT"].ToString() + ")";
                database.Execute(cmd);
            }

            //Entferne unnötige Inizes
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Entferne unnötige Inizes");

            cmd = "Drop Index AehnlicheMotive_ID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index inKursSeit_ID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index Jahrgang_ID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index Kommentare_ID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index Legierung_ID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index Material_ID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index Motiv_ID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index Rand_ID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index idxRepID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index Reversbeschreibung_ID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index SortierID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index Ausgabeanlass_ID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index ausserKursSeit_ID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index Aversbeschreibung_ID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index Aversentwurf_ID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index Besonderheiten_ID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index gepraegtVonBis_ID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index Reversentwurf_ID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index idxID on tblCB_DB";
            database.Execute(cmd);

            cmd = "Drop Index DatName_ID on tblCB_DB";
            database.Execute(cmd);


            //Entferne unnötige Spalten
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Entferne nicht mehr benötigte Spalten");
            cmd = "Alter Table tblNation Drop column LastUpdate";
            database.Execute(cmd);

            cmd = "Alter Table tblNation Drop column EN_GB";
            database.Execute(cmd);

            cmd = "Alter Table tblNation add column [Key] Text(255)";
            database.Execute(cmd);

            cmd = "Update tblNation set [Key]=Bezeichnung";
            database.Execute(cmd);

            cmd = "Alter Table tblAera Drop column EN_GB";
            database.Execute(cmd);

            cmd = "Alter Table tblGebiet Drop column EN_GB";
            database.Execute(cmd);

            cmd = "Alter Table tblCB_DB Drop column Motiv_ID";
            database.Execute(cmd);

            cmd = "Alter Table tblCB_DB Drop column Besonderheit_ID";
            database.Execute(cmd);

            cmd = "Alter Table tblCB_DB Drop column Legierung_ID";
            database.Execute(cmd);

            cmd = "Alter Table tblCB_DB Drop column Rand_ID";
            database.Execute(cmd);

            cmd = "Alter Table tblCB_DB Drop column Aversbeschreibung_ID";
            database.Execute(cmd);

            cmd = "Alter Table tblCB_DB Drop column Reversbeschreibung_ID";
            database.Execute(cmd);

            cmd = "Alter Table tblCB_DB Drop column Aversentwurf_ID";
            database.Execute(cmd);

            cmd = "Alter Table tblCB_DB Drop column Reversentwurf_ID";
            database.Execute(cmd);

            cmd = "Alter Table tblCB_DB Drop column Ausgabeanlass_ID";
            database.Execute(cmd);

            cmd = "Alter Table tblCB_DB Drop column AehnlicheMotive_ID";
            database.Execute(cmd);

            cmd = "Alter Table tblCB_DB Drop column Kommentare_ID";
            database.Execute(cmd);

            cmd = "Alter Table tblCB_DB Drop column inKursSeit_ID";
            database.Execute(cmd);

            cmd = "Alter Table tblCB_DB Drop column ausserKursSeit_ID";
            database.Execute(cmd);

            cmd = "Alter Table tblCB_DB Drop column gepraegtVonBis_ID";
            database.Execute(cmd);

            cmd = "Alter Table tblCB_DB Drop column Material_ID";
            database.Execute(cmd);

            cmd = "Alter Table tblCB_DB Drop column DateiName_ID";
            database.Execute(cmd);

            //Entferne unnötige Tabellen
            progress = +counter;
            bgwVersion26.ReportProgress(progress, "Entferne nicht mehr benötigte Tabellen");

            cmd = "Drop Table tblAehnlicheMotive";
            database.Execute(cmd);

            cmd = "Drop Table tblAusgabeanlass";
            database.Execute(cmd);

            cmd = "Drop Table tblAusserKurs";
            database.Execute(cmd);

            cmd = "Drop Table tblAversbeschreibung";
            database.Execute(cmd);

            cmd = "Drop Table tblAversentwurf";
            database.Execute(cmd);

            cmd = "Drop Table tblBesonderheiten";
            database.Execute(cmd);

            cmd = "Drop Table tblGeprVB";
            database.Execute(cmd);

            cmd = "Drop Table tblInkurs";
            database.Execute(cmd);

            cmd = "Drop Table tblJahrgang";
            database.Execute(cmd);

            cmd = "Drop Table tblKommentare";
            database.Execute(cmd);

            cmd = "Drop Table tblLegierung";
            database.Execute(cmd);

            cmd = "Drop Table tblMaterial";
            database.Execute(cmd);

            cmd = "Drop Table tblMotiv";
            database.Execute(cmd);

            cmd = "Drop Table tblNennwert";
            database.Execute(cmd);

            cmd = "Drop Table tblRand";
            database.Execute(cmd);

            cmd = "Drop Table tblReversbeschreibung";
            database.Execute(cmd);

            cmd = "Drop Table tblReversentwurf";
            database.Execute(cmd);

            cmd = "Drop Table tblHinweise";
            database.Execute(cmd);

            cmd = "Drop Table tblRegistery";
            database.Execute(cmd);

            cmd = "Drop Table tblToken";
            database.Execute(cmd);

            cmd = "Drop Table tblDateiname";
            database.Execute(cmd);
        }

        private void saveTexte(string table, string field, string typ)
        {
            string text;
            string text2;

            DataTable dt = database.GetDataTable("Select * from tblcb_db");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bgwVersion26.ReportProgress(Convert.ToInt32(Convert.ToDouble(i) / Convert.ToDouble(dt.Rows.Count) * 100), "");

                string cmd = "Select * from " + table + " where id = " + dt.Rows[i][field].ToString();
                DataRow row = database.GetDataRow(cmd);
                if (row != null)
                {
                    text = row["Bezeichnung"].ToString().Replace("'", "''");
                    if (text.Length > 250)
                    {
                        text2 = text.Substring(250);
                        text = text.Substring(0, 250);
                    }
                    else
                        text2 = "";

                    cmd = "insert into tblTexte (typ,Guid,sprache,[text],text2) Values('" + typ + "','" + dt.Rows[i]["RepID"].ToString() + "','DE','" + text + "','" + text2 + "')";
                    database.Execute(cmd);

                    text = row["EN_GB"].ToString().Replace("'", "''");
                    if (text.Length > 250)
                    {
                        text2 = text.Substring(250);
                        text = text.Substring(0, 250);
                    }
                    else
                        text2 = "";
                    cmd = "insert into tblTexte (typ,Guid,sprache,[text],text2) Values('" + typ + "','" + dt.Rows[i]["RepID"].ToString() + "','EN','" + text + "','" + text2 + "')";
                    database.Execute(cmd);
                }
            }
        }

    private void bgwVersion26_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState.ToString() != "")
            {
                lblText.Text = e.UserState.ToString();
                pgbProgress.Value = e.ProgressPercentage;
            }
            else
                pgbProgress2.Value = e.ProgressPercentage;
        }

    private void bgwVersion26_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
			weiter = true;

			lblEnde.Visible = true;
            Thread.Sleep(10000);

            OleDBConnection.CloseAllConnections();

            //frmCompact form = new frmCompact(OleDBConnection.File);
            //form.ShowDialog();

            Application.Exit();
        }

        private void frmKonvert_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            OleDBZugriff database = new OleDBZugriff();
            database.Tabelle = "Update";

            version = database.Text("Select Version from tblVersion").Split(new string[1] { "." }, StringSplitOptions.None);

            // Konvertiere auf Version 3.0.0.19 wenn aktuelle Versionsnummer kleiner ist
            database.Tabelle = "Update";

            if (Convert.ToInt32(version[0]) == 3 && Convert.ToInt32(version[1]) == 0 && Convert.ToInt32(version[2]) == 0 && Convert.ToInt32(version[3]) < 19)
            {
                database.OpenTransaction();
                database.Execute("Insert Into stblSettings (id,Wert) values('Activated','')");
                database.Execute("Update tblVersion set Version ='3.0.0.19'");

                database.Commit();
                Thread.Sleep(500);
                version[3] = "19";
            }

            // Konvertiere auf Version 3.0.0.21 wenn aktuelle Versionsnummer kleiner ist
            if (Convert.ToInt32(version[0]) == 3 && Convert.ToInt32(version[1]) == 0 && Convert.ToInt32(version[2]) == 0 && Convert.ToInt32(version[3]) < 21)
            {
                database.OpenTransaction();

                if (database.Value("Select id from stblSettings where id ='Vorname'") == "")
                    database.Execute("Insert Into stblSettings (id,Wert) values('Vorname','')");

                if (database.Value("Select id from stblSettings where id ='Nachname'") == "")
                    database.Execute("Insert Into stblSettings (id,Wert) values('Nachname','')");

                database.Execute("Update tblVersion set Version ='3.0.0.21'");
                database.Commit();
                Thread.Sleep(500);
                version[3] = "21";

                if (database.Value("Select id from stblSettings where id ='Flag'") == "")
                    database.Execute("Insert Into stblSettings (id,Wert) values('Flag','')");
            }

            // Konvertiere auf Version 3.0.0.22 wenn aktuelle Versionsnummer kleiner ist
            if (Convert.ToInt32(version[0]) == 3 && Convert.ToInt32(version[1]) == 0 && Convert.ToInt32(version[2]) == 0 && Convert.ToInt32(version[3]) < 22)
            {
                database.OpenTransaction();

                database.Execute("ALTER TABLE stblSettings ALTER COLUMN Wert Memo");

                if (database.Text("Select id from stblSettings where id ='Preise'") == String.Empty)
                    database.Execute("Insert Into stblSettings (id,Wert) values('Preise','" + enmPreise.Katalogpreise.ToString() + "')");

                database.Execute("Update tblVersion set Version ='3.0.0.22'");
                database.Commit();
                Thread.Sleep(500);
                version[3] = "22";
            }

            // Konvertiere auf Version 3.0.0.23 wenn aktuelle Versionsnummer kleiner ist
            if (Convert.ToInt32(version[0]) == 3 && Convert.ToInt32(version[1]) == 0 && Convert.ToInt32(version[2]) == 0 && Convert.ToInt32(version[3]) < 23)
            {
                database.OpenTransaction();

                if (database.Text("Select id from stblSettings where id ='Preise'") == String.Empty)
                    database.Execute("Insert Into stblSettings (id,Wert) values('Preise','" + enmPreise.Katalogpreise.ToString() + "')");

                database.Execute("Update tblVersion set Version ='3.0.0.23'");
                database.Commit();
                Thread.Sleep(500);
                version[3] = "23";
            }

            // Konvertiere auf Version 3.0.0.26 wenn aktuelle Versionsnummer kleiner ist
            if (Convert.ToInt32(version[0]) == 3 && Convert.ToInt32(version[1]) == 0 && Convert.ToInt32(version[2]) == 0 && Convert.ToInt32(version[3]) < 26)
            {
                OleDBConnection.CloseAllConnections();

                string sourceFile = OleDBConnection.File;
                string destinationFile = Path.Combine(Path.GetDirectoryName(sourceFile), Path.GetFileNameWithoutExtension(sourceFile) + ".bak");

                if (File.Exists(destinationFile))
                    File.Delete(destinationFile);

                File.Copy(sourceFile, destinationFile);

                DBConnect result = OleDBConnection.Init;

                weiter = false;
                bgwVersion26.RunWorkerAsync(1);

                do
                {
                    Thread.Sleep(60000);
                } while (!weiter);

                version[3] = "26";
            }

            // Konvertiere auf Version 3.0.0.36 wenn aktuelle Versionsnummer kleiner ist
            if (Convert.ToInt32(version[0]) == 3 && Convert.ToInt32(version[1]) == 0 && Convert.ToInt32(version[2]) == 0 && Convert.ToInt32(version[3]) < 36)
            {
                database.OpenTransaction();

                string cmd = "Alter Table tblcb_db add column Typ Text(50)";
                if (!database.FieldExists("tblcb_db", "Typ"))
                    database.Execute(cmd);

                cmd = "Alter Table tblcb_db add column Form Text(100)";
                if (!database.FieldExists("tblcb_db", "Form"))
                    database.Execute(cmd);

                cmd = "Alter Table tblcb_db add column Orientation Text(50)";
                if (!database.FieldExists("tblcb_db", "Orientation"))
                    database.Execute(cmd);

                cmd = "Alter Table tblcb_db add column Referenz Text(1000)";
                if (!database.FieldExists("tblcb_db", "Referenz"))
                    database.Execute(cmd);

                database.Execute("Update tblVersion set Version ='3.0.0.36'");
                database.Commit();
                Thread.Sleep(500);
                version[3] = "36";
            }

            // Konvertiere auf Version 3.0.0.45 wenn aktuelle Versionsnummer kleiner ist
            if (Convert.ToInt32(version[0]) == 3 && Convert.ToInt32(version[1]) == 0 && Convert.ToInt32(version[2]) == 0 && Convert.ToInt32(version[3]) < 46)
            {
                pgbProgress.Maximum = 70;
                pgbProgress.Value = 0;

                pgbProgress2.Maximum = 70;
                pgbProgress2.Value = 0;

                bgwVersion26.RunWorkerAsync(2);
            }
            else
                Close();
        }


        private void renameColumn(string tabelle, string columnNeu, string columnAlt, string typ)
        {
            try
            {
                string cmd = string.Format("Alter Table {0} add column {1} {2}", tabelle, columnNeu, typ);
                database.Execute(cmd);
                cmd = string.Format("Update {0} set {1} = {2}", tabelle, columnNeu, columnAlt);
                database.Execute(cmd);
                cmd = string.Format("Alter Table {0} Drop column {1}", tabelle, columnAlt);
                database.Execute(cmd);
            }
            catch{ }
        }

        private void renameTable(string oldName, string newName)
        {
            try
            {
                string cmd = string.Format("SELECT * INTO {0} FROM {1}", newName, oldName);
                database.Execute(cmd);

                cmd = string.Format("DROP TABLE {0}", oldName);
                database.Execute(cmd);
            }
            catch { }
        }

        private void changeColumnTyp(string tabelle, string column, string typ)
        {
            string cmd = string.Format("Alter Table {0} alter {1} {2}", tabelle, column, typ);
                database.Execute(cmd);
        }

        private void addColumn(string tabelle, string column, string typ)
        {
            try
            {
                string cmd = string.Format("Alter Table {0} Add Column {1} {2}", tabelle, column, typ);
                database.Execute(cmd);
            }
            catch { }
        }

        private void dropColumn(string tabelle,string column, string index = null)
        {
            string cmd;

            if (index != null)
            {
                cmd = string.Format("DROP INDEX {0} ON {1}", index, tabelle);
                database.Execute(cmd);
            }

            try
            {
                cmd = string.Format("Alter table {0} Drop Column {1}", tabelle, column);
                database.Execute(cmd);
            }
            catch { }
        }

        private void updateColumn(string tabelle, string oldColumn, string newColumn)
        {
            string cmd = string.Format("Update {0} set {1}={2}", tabelle, newColumn, oldColumn);
            database.Execute(cmd);
        }

        private void deleteRows(string tabelle, string where)
        {
            string cmd = string.Format("Delete * from {0} where {1}", tabelle, where);
            database.Execute(cmd);
        }

        private void konvertVersion45()
        {
            int progress = 0;
            string cmd = string.Empty;
            //database.OpenTransaction();

            bgwVersion26.ReportProgress(progress, "Tabelle umbenennen tblAuktionen");
            renameTable("stblAuktionen", "tblAuktionen");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle umbenennen tblBestand");
            renameTable("stblBestand", "tblBestand");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle umbenennen tblEigenerPreis");
            renameTable("stblEigenerPreis", "tblEigenerPreis");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle umbenennen tblPreise");
            renameTable("stblPreise", "tblPreise");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle umbenennen tblSammlung");
            renameTable("stblSammlung", "tblSammlung");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle umbenennen tblSettings");
            renameTable("stblSettings", "tblSettings");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle umbenennen tblSettings2");
            renameTable("stblSettings2", "tblSettings2");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle umbenennen tblEigeneBilder");
            renameTable("stblEigeneBilder", "tblEigeneBilder");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle umbenennen tblEigeneKatNr");
            renameTable("stblEigeneKatNr", "tblEigeneKatNr");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle umbenennen tblKatalog");
            renameTable("tblCB_DB", "tblKatalog");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle umbenennen tblPraegeanstalt");
            renameTable("tblPrägeanstalt", "tblPraegeanstalt");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblAera");
            renameColumn("tblAera", "Bezeichnung", "DE_DE", "Text(255)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblAera");
            renameColumn("tblAera", "NationID", "Nat", "integer");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblAuktionen");
            renameColumn("tblAuktionen", "[Guid]", "ID_Katalog", "Text(36)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblAuktionen");
            renameColumn("tblAuktionen", "Erhaltungsgrad", "ID_Erhaltungsgrad", "integer");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblAuktionen");
            changeColumnTyp("tblAuktionen", "Datum", "Text(10)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblCulture");
            renameColumn("tblCulture", "Bezeichnung", "DE_DE", "Text(255)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblCulture");
            renameColumn("tblCulture", "Waehrung", "Währung", "Text(255)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblCulture");
            renameColumn("tblCulture", "Kultur", "Culture", "Text(255)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblDownloads");
            changeColumnTyp("tblDownloads", "Datum", "Text(20)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblErhaltungsgrad");
            renameColumn("tblErhaltungsgrad", "Erhaltung", "Erhaltungsgrad", "Text(10)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblGebiet");
            renameColumn("tblGebiet", "Bezeichnung", "DE_DE", "Text(255)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblGebiet");
            renameColumn("tblGebiet", "NationID", "Nat", "integer");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblGebiet");
            renameColumn("tblGebiet", "AeraID", "Aera", "integer");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            renameColumn("tblKatalog", "NationID", "Nation_ID", "integer");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            renameColumn("tblKatalog", "AeraID", "Aera_ID", "integer");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            renameColumn("tblKatalog", "GebietID", "Gebiet_ID", "integer");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            renameColumn("tblKatalog", "SPreis", "Erh_S_Preis", "double");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            renameColumn("tblKatalog", "SPPreis", "Erh_SP_Preis", "double");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            renameColumn("tblKatalog", "SSPreis", "Erh_SS_Preis", "double");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            renameColumn("tblKatalog", "SSPPreis", "Erh_SSP_Preis", "double");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            renameColumn("tblKatalog", "VZPreis", "Erh_VZ_Preis", "double");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            renameColumn("tblKatalog", "VZPPreis", "Erh_VZP_Preis", "double");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            renameColumn("tblKatalog", "STNPreis", "Erh_STN_Preis", "double");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            renameColumn("tblKatalog", "STHPreis", "Erh_STH_Preis", "double");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            renameColumn("tblKatalog", "PPPreis", "Erh_PP_Preis", "double");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            renameColumn("tblKatalog", "[Guid]", "Repid", "Text(36)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            renameColumn("tblKatalog", "Waehrung", "Währung", "Text(30)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            renameColumn("tblKatalog", "Gepraegt", "Geprägt", "Text(30)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            renameColumn("tblKatalog", "AehnlicheMotive", "ÄhnlicheMotive", "Text(255)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            renameColumn("tblKatalog", "Praegeort", "Prägeort", "Text(255)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            renameColumn("tblKatalog", "Muenzzeichen", "Münzzeichen", "Text(20)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            addColumn("tblKatalog", "Bearbeitungsdatum", "Text(10)");
            dropColumn("tblKatalog", "ShowCBPic");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            addColumn("tblKatalog", "Typ1", "Text(100)");
            updateColumn("tblKatalog", "typ", "typ1");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblKatalog");
            dropColumn("tblKatalog", "typ");
            addColumn("tblKatalog", "Typ", "Text(100)");
            updateColumn("tblKatalog", "typ1", "typ");
            dropColumn("tblKatalog", "typ1");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblModule");
            renameColumn("tblModule", "NationID", "Nation", "integer");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblNation");
            renameColumn("tblNation", "Bezeichnung", "DE_DE", "Text(255)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblNation");
            renameColumn("tblNation", "InUse", "bInUse", "Yesno");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblNation");
            addColumn("tblNation", "Mapping", "Text(10)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblPraegeanstalt");
            addColumn("tblPraegeanstalt", "Land", "Text(30)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblPraegeanstalt");
            renameColumn("tblPraegeanstalt", "Muenzzeichen", "Münzzeichen", "Text(20)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblSammlung");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblSammlung");
            renameColumn("tblSammlung", "[Guid]", "ID_Katalog", "Text(36)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblSammlung");
            renameColumn("tblSammlung", "Erhaltung", "ID_ErhaltungsGrad", "integer");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblSammlung");
            renameColumn("tblSammlung", "Doublette", "BOOL_Doublette", "Yesno");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblSammlung");
            changeColumnTyp("tblSammlung", "Kaufdatum", "Text(10)");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblSammlung");
            renameColumn("tblSammlung", "Kaufpreis", "Preis", "double");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblSammlung");
            renameColumn("tblSammlung", "Fehlerhaft", "BOOL_Fehlerhaft", "Yesno");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblTexte");
            dropColumn("tblTexte", "text2");
            addColumn("tblTexte", "text2", "memo");
            updateColumn("tblTexte", "[text]", "text2");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblTexte");
            dropColumn("tblTexte", "[text]", "[Text]");
            addColumn("tblTexte", "[text]", "memo");
            updateColumn("tblTexte", "text2", "[text]");
            dropColumn("tblTexte", "text2");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblTexte");
            cmd = "SELECT * INTO tblTexte_EN FROM tblTexte";
            database.Execute(cmd);

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblTexte");
            deleteRows("tblTexte_EN", "sprache='DE'");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblTexte");
            dropColumn("tblTexte_EN", "sprache");
            renameColumn("tblTexte_EN", "NationID", "Nation", "integer");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblTexte");
            renameTable("tblTexte", "tblTexte_DE");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblTexte");
            deleteRows("tblTexte_DE", "sprache='EN'");

            progress++;
            bgwVersion26.ReportProgress(progress, "Tabelle bearbeiten tblTexte");
            dropColumn("tblTexte_DE", "sprache");
            renameColumn("tblTexte_DE", "NationID", "Nation", "integer");

            database.Execute("Update tblVersion set Version ='3.0.0.45'");

            
            Thread.Sleep(500);
        }

    }
}

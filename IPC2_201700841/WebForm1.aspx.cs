using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace IPC2_201700841
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        List<string> ficha = new List<string>();
        protected System.Web.UI.WebControls.Button btnUpload;
        protected System.Web.UI.WebControls.Label lblUploadResult;
        protected System.Web.UI.WebControls.Panel frmConfirmation;
        protected System.Web.UI.HtmlControls.HtmlInputFile oFile;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        override protected void OnInit(EventArgs e)
        {
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.CargarArchivo.Click += new System.EventHandler(this.CargarArchivo_Click);
            this.Load += new System.EventHandler(this.Page_Load);
        }
        protected void CargarArchivo_Click(object sender, EventArgs e)
        {
            
            string NombreArchivo;
            string PathArchivo;
            string UbicacionA;
            UbicacionA = Server.MapPath("./");
            NombreArchivo = oFile.PostedFile.FileName;
            NombreArchivo = Path.GetFileName(NombreArchivo);
            if (oFile.Value != "")
            {
                //lblUploadResult.Text = "Funciona";
                PathArchivo = UbicacionA + NombreArchivo;
                XmlReader reader = XmlReader.Create(PathArchivo);
                ficha.Add(reader.ToString());
                while (reader.Read()) 
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString()) //ya puedo leer
                        {
                            case "ficha":
                                TextBox1.Text += "insertar fichas";
                                break;
                            case "color":
                                TextBox1.Text += "'" + reader.ReadString() + "'" + ",";
                                break;
                            case "columna":
                                TextBox1.Text += "'" + reader.ReadString() + "'" + ",";
                                break;
                            case "fila":
                                TextBox1.Text += "'" + reader.ReadString() + "'" + ",";
                                break;
                        }
                    }
                    else 
                    {
                        lblUploadResult.Text = "Archivo cargado";
                    }
                }
            }
            else 
            {
                lblUploadResult.Text = "No ha seleccionado ningun archivo";
            }
        }

        protected void GuardarArchivo_Click(object sender, EventArgs e)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            XmlWriter xmlWriter = XmlWriter.Create(ficha.ToString(), settings); //error porque no consigo la ubicacion
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("tablero");
            foreach (var dato in ficha.ToString())//error
            {
                xmlWriter.WriteStartElement("ficha");
                xmlWriter.WriteStartElement("color");
                if (dato.ToString() == "color") 
                {
                    xmlWriter.WriteString(dato.ToString());
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteStartElement("columna");
                if (dato.ToString() == "columna") 
                {
                    xmlWriter.WriteString(dato.ToString());
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteStartElement("fila");
                if (dato.ToString() == "fila")
                {
                    xmlWriter.WriteString(dato.ToString());
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();

            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();

        }
    }
}
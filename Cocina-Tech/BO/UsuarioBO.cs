using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Data;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace Cocina_Tech.BO
{
    public class UsuarioBO
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { set; get; }
        public string Telefono { set; get; }
        public DateTime FechaNac { set; get; }
        public string Correo { set; get; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public byte[] Fotografia { set; get; }

        public bool estado { get; set; }

        public int IDTIPO { get; set; }


        public string Encriptar(string str)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        System.Drawing.Image RedimencionarImagen(System.Drawing.Image Imgoriginal, int Altoimg)
        {
            var Radio = (double)Altoimg / Imgoriginal.Height;//diferencia entre la imagenes
            var NuevoAncho = (int)(Imgoriginal.Width * Radio);
            var NuevoAlto = (int)(Imgoriginal.Height * Radio);
            var ImagenRedimencionada = new Bitmap(NuevoAncho, NuevoAlto);
            //creo archivo apartir del bitmap con las nuevas dimensiones
            var g = Graphics.FromImage(ImagenRedimencionada);
            g.DrawImage(Imgoriginal, 0, 0, NuevoAncho, NuevoAlto);
            return ImagenRedimencionada;
        }


        public byte[] Convertir_Imagen_Bytes(Image img)
        {
            string sTemp = Path.GetTempFileName();
            FileStream fs = new FileStream(sTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            img.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            fs.Position = 0;

            int imgLength = Convert.ToInt32(fs.Length);
            byte[] bytes = new byte[imgLength];
            fs.Read(bytes, 0, imgLength);
            fs.Close();
            return bytes;
        }

        public string RecuperarImagen(DataSet datos)// tiene que recibir sobre carga de la tabla de los datos
        {

            byte[] bite = (byte[])datos.Tables[0].Rows[0]["Img_portada"];
            string prueba = "data:image/jpeg;base64," + Convert.ToBase64String(bite);
            return prueba;
        }
        public string RecuperarImagen(byte[] imagen)
        {
            byte[] bite = imagen;
            string prueba = "data:image / jpeg; base64," + Convert.ToBase64String(bite);
            return prueba;
        }

    }
}
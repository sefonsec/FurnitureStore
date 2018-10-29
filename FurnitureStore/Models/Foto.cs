using System;
using System.Threading;
using System.Web.Mvc;

namespace FurnitureStore.Models
{
    public class Foto
    {
        public static int _inc;

        public int id { get; set; }

        public int produtoID { get; set; }
        public virtual Produto Produto { get; set; }

        public string nome { get; set; }
        public byte[] conteudo { get; set; }
        public string tipo { get; set; }
        public int tamanho { get; set; }

        public Foto()
        {
            id = Interlocked.Increment(ref _inc);
        }
    }

    public static class Methods
    {
        public static string ImagemBase64(this HtmlHelper helper, Byte[] value, string Type)
        {
            return string.Format("data:{0}; base64,{1}", Type, Convert.ToBase64String(value));
        }
    }
}
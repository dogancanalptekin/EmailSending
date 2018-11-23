using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailSending.Models
{
    public class EpostaModel
    {
        public string adsoyad { get; set; }
        public string email { get; set; }
        public string konu { get; set; }
        public string mesaj { get; set; }
    }
}
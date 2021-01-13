using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace eUniverzitet.Web.Models
{
    public class StudentUploadSlikaVM
    {
        public int id { get; set; }
   
        public IFormFile slikaStudentaNew { set; get; }
    }

}

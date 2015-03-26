using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MatrizTranspuesta.Models;


namespace MatrizTranspuesta.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.Title = Tittle.NameProyect;
           
            ViewBag.Success = false;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadFile()
        {
            HttpPostedFileBase file = null;

              ViewBag.Success = false;
                if (Request.Files["UploadedFile"].ContentLength > 0)
                {
                    string extension = System.IO.Path.GetExtension(Request.Files["UploadedFile"].FileName);
                    if (extension == ".txt")
                   {                       
                         file = this.HttpContext.Request.Files.Get("UploadedFile");
                       
                        
                        ViewBag.Success = true;
                    }
                    else
                        ModelState.AddModelError(string.Empty, "Verifique, el archivo formato invalido");
                }

                return View((object)Convertir(file));
        }


        private string Convertir(HttpPostedFileBase file)
        {
            System.IO.StreamReader Model = new System.IO.StreamReader(file.InputStream);
            System.IO.StreamReader Modeltemp = Model;
            string[] stringSeparators = new string[] { "," };
            const char CR = (char)13 ;
            const char LF = (char)10;
            int rowcount=0, columncount = 0;
            String linea;
            string[] columna;
            List<String> row=new List<String>();
            while (Model.Peek() != -1)
            {
            
                linea = Model.ReadLine();
                if (linea.Trim().Length > 0)
                {
                    row.Add( linea.Trim());
                }
            }        

            rowcount = row[0].Split(stringSeparators, StringSplitOptions.None).Count();
            columncount = row.Count();
            string[,] Arreglo = new string[rowcount, columncount];
            String ModelResult=string.Empty ; 
            string temp ;

            if (rowcount>columncount)
                {
                    for (int i = 0; i < columncount; i++)
                    {
                        for (int j =  0; j <= columncount +1; j++)
                        {
                            columna=row[i].Split(stringSeparators, StringSplitOptions.None);
                            temp = columna[j];
                            Arreglo[j,i]=temp;

                            if (j < columncount )
                            {
                                columna = row[j].Split(stringSeparators, StringSplitOptions.None);
                                temp = columna[i];
                                Arreglo[i, j] = temp;
                            }
                    
                            }
                        }
                }
            else if (rowcount < columncount)
            {
                for (int i = 0; i < rowcount; i++)
                {
                    for (int j = 0; j <= rowcount + 1; j++)
                    {
                        if (j < rowcount)
                        {
                            columna = row[i].Split(stringSeparators, StringSplitOptions.None);
                            temp = columna[j];
                            Arreglo[j, i] = temp;
                        }

                        columna = row[j].Split(stringSeparators, StringSplitOptions.None);
                        temp = columna[i];
                        Arreglo[i, j] = temp;

                    }
                }
            }
            else 
            {
                for (int i = 0; i < rowcount; i++)
                {
                    for (int j = 0; j <= rowcount + 1; j++)
                    {
                        if (j < rowcount)
                        {
                            columna = row[i].Split(stringSeparators, StringSplitOptions.None);
                            temp = columna[j];
                            Arreglo[j, i] = temp;

                            columna = row[j].Split(stringSeparators, StringSplitOptions.None);
                            temp = columna[i];
                            Arreglo[i, j] = temp;
                        }

                        

                    }
                }
            }

            for (int i = 0; i < rowcount; i++)
            {
                for (int j = 0; j < columncount ; j++)
                {
                    ModelResult = ModelResult + Arreglo[i, j];
                    if (j < columncount-1)
                        ModelResult = ModelResult + stringSeparators[0];
                }
                ModelResult = ModelResult + CR + LF + CR + LF;
            }
            return ModelResult.Trim();
        }

    }

 
}

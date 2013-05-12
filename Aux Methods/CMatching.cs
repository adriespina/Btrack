  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.IO;
using System.Threading;
using Amib.Threading;
using System.Windows.Forms;


namespace Billetrack
{
    public class TaskInfo
    {
        
        public string _model;
        public string _candidate;
        public int _modo;
        public TaskInfo(string model, string candidate, int modo)
        {
            _model = model;
            _candidate = candidate;
            _modo = modo;

        }
    }
   public  class CMatching
    {
        public  int MAX_N_IMAGES_IN_FOLDER = 200; 
        
        public  int THRESHOLD_QUALITY = 30;
        public  int THRESHOLD_INSIDE = 30;//50
        public int MATCHING_TIMEOUT = 15000;
        public bool DOUBLE_CHECK = true;

        int m_numberOfThreads;
      

        public int NumberOfThreads
        {
            get { return m_numberOfThreads; }
            set
            {
                int numCPU = Environment.ProcessorCount;


                if (value <= 0 || value >= numCPU)
                {
                    m_numberOfThreads = numCPU;

                }
                else
                {
                    m_numberOfThreads = value;

                }
            }
        }
    public  SmartThreadPool smartThreadPool ;

     public CMatching(int threads)
     {
         NumberOfThreads = threads;
     }
        

        public int MatchingOneToVarius(string filename, ref string[] filenameMatrixObjects, out resultMatching[] pResult)
        {
           

            string sfilename, sfilenameDescriptors;
            sfilename = filename;
            sfilenameDescriptors = filename  + CSurf.EXTENSION_DESCRIPTORS;
            resultMatching[] pResult2 = new resultMatching[filenameMatrixObjects.Length];
            IWorkItemResult[] tareas = new IWorkItemResult[filenameMatrixObjects.Length];


            //check if exist  ¿hay que liberarlo para poder guardar?
            // si la imagen modelo no tiene descriptores se los creamos
            if (!File.Exists(sfilenameDescriptors))
            {
                CSurf surf_image = new CSurf(sfilename);
                surf_image.Save(filename);
                surf_image.Clean();
            }

            int index = 0;
            smartThreadPool = new SmartThreadPool();
            smartThreadPool.MinThreads = 0;
            smartThreadPool.MaxThreads = m_numberOfThreads;
     
            foreach (string pos_imagen in filenameMatrixObjects)
            {
                tareas[index] = smartThreadPool.QueueWorkItem(new WorkItemCallback(this.Match), new TaskInfo(sfilename, pos_imagen, CSurf.MODE_CESAR));
                index++;

            }


            //OPCION1 HACERLO CON UN WAIT ANY

            //        int contador=0;
            //        while (contador<filenameMatrixObjects.Length)
            //{
            //         SmartThreadPool.WaitAny(tareas, MATCHING_TIMEOUT, false);
            //         contador++;
            //}
            //        for (int i = 0; i < pResult2.Length; i++)
            //        {
            //            pResult2[i] = (resultMatching)tareas[i].Result;
            //        }


            //OPCION 2 HACERLO CON UN WAIT ALL

            bool success = SmartThreadPool.WaitAll(tareas, MATCHING_TIMEOUT, false);
            if (success)
            {
                for (int i = 0; i < pResult2.Length; i++)
                {
                    pResult2[i] = (resultMatching)tareas[i].Result;
                }
            }
            else
            {
                smartThreadPool.Join();
                smartThreadPool.Shutdown();
                pResult = null;
                return -2;
            }
           


            //cerramos el pool y copiamos los resultados obtenidos
            smartThreadPool.Shutdown();
            pResult = pResult2;

        

            //Buscamos la mejor correspodencia y enviamos el indice 
            int indice_max_quality = -1, indice_max_matches = -2, indice_max_inside = -3, it = 0;
            float max_quality = 0, max_matches = 0, max_inside = 0;
            foreach (resultMatching rst in pResult)
            {
                if (rst.quality > THRESHOLD_QUALITY && rst.inside_KeyPoints > THRESHOLD_INSIDE)
                {

                    if (rst.quality == max_quality)
                    {
                        if (rst.common_KeyPoints > max_matches||rst.inside_KeyPoints > max_inside)
                        {
                            indice_max_quality = it;
                        }
                        
                    }
                    if (rst.quality > max_quality)
                    {
                        indice_max_quality = it;
                        max_quality = rst.quality;
                    }
                   
                    if (rst.common_KeyPoints > max_matches)
                    {
                        indice_max_matches = it;
                        max_matches = rst.common_KeyPoints;
                    }
                    if (rst.inside_KeyPoints > max_inside)
                    {
                        indice_max_inside = it;
                        max_inside = rst.inside_KeyPoints;
                    }

                }
                it++;
            }

            if (DOUBLE_CHECK)
            {
                //si todos coinciden es una match perfecto y envio el indice
                if (indice_max_quality >= 0 && (indice_max_quality == indice_max_matches) && (indice_max_quality == indice_max_inside))
                {
                    return indice_max_quality;
                }
               


            //Si no pruebo con el segundo metodo
                else
                {
                    index = 0;
                    STPStartInfo sinfo = new STPStartInfo();
                    sinfo.ThreadPriority = ThreadPriority.Highest;
                    sinfo.MinWorkerThreads = 0;
                    sinfo.MaxWorkerThreads = m_numberOfThreads;
                    smartThreadPool = new SmartThreadPool(sinfo);




                    foreach (string pos_imagen in filenameMatrixObjects)
                    {
                        tareas[index] = smartThreadPool.QueueWorkItem(new WorkItemCallback(this.Match), new TaskInfo(sfilename, pos_imagen, CSurf.MODE_SURF));
                        index++;

                    }

                    //Thread.Sleep(2000);
                    success = SmartThreadPool.WaitAll(tareas);
                    if (success)
                    {
                        for (int i = 0; i < pResult2.Length; i++)
                        {
                            pResult2[i] = (resultMatching)tareas[i].Result;
                        }
                    }
                    else
                    {
                        smartThreadPool.Join();
                        smartThreadPool.Shutdown();
                        pResult = null;
                        return -2;
                    }



                    //cerramos el pool y copiamos los resultados obtenidos
                    smartThreadPool.Shutdown();
                    pResult = pResult2;

                    indice_max_quality = -1; indice_max_quality = -1; indice_max_matches = -2; indice_max_inside = -3; it = 0;
                    max_quality = 0; max_matches = 0; max_inside = 0; it = 0; max_quality = 0;

                    //Buscamos la mejor correspodencia y enviamos el indice 
                    foreach (resultMatching rst in pResult)
                    {
                        if (rst.quality > THRESHOLD_QUALITY && rst.inside_KeyPoints > THRESHOLD_INSIDE)
                        {

                            if (rst.quality == max_quality)
                            {
                                if (rst.common_KeyPoints > max_matches || rst.inside_KeyPoints > max_inside)
                                {
                                    indice_max_quality = it;
                                }

                            }
                            if (rst.quality > max_quality)
                            {
                                indice_max_quality = it;
                                max_quality = rst.quality;
                            }
                            if (rst.common_KeyPoints > max_matches)
                            {
                                indice_max_matches = it;
                                max_matches = rst.common_KeyPoints;
                            }
                            if (rst.inside_KeyPoints > max_inside)
                            {
                                indice_max_inside = it;
                                max_inside = rst.inside_KeyPoints;
                            }

                        }
                        it++;
                    }


                    //Envio el indice de mejor calidad aunque no coincidan
                    if (max_quality >= THRESHOLD_QUALITY)
                    {
                        return indice_max_quality;
                    }
                    //si la calidad es mala no hay matching
                    else
                    {
                        return -1;
                    }

                }
            }
            else
            {
                //Envio el indice de mejor calidad aunque no coincidan
                if (max_quality >= THRESHOLD_QUALITY)
                {
                        return indice_max_quality;
                }
                //si la calidad es mala no hay matching
                else
                {
                    return -1;
                }

            }
        }

      
      
        /// <summary>
        /// Funcion para calcular el matching entre 2 imagenes
        /// </summary>
        /// <param name="State">Objeto con 3 elementos : String modelo, string candidato, Int modo</param>
        /// <returns> Devuelve las estadisticas del matching</returns>
        resultMatching Match(Object State)
        {
           TaskInfo ti=(TaskInfo)State;
            string filename=(string)ti._model;
           CSurf model = new CSurf(filename, -1, -1);
           // model.OnlyLoadImage(filename);  //BUG hace falta hcerlo???
            resultMatching results = new resultMatching();
            string candidate = ti._candidate;
            int modo = ti._modo;
            if (candidate == "matched") return results;
            CSurf surf_object;
            Point[] corners1 = new Point[4]; //Homografia de las esquinas de la imagen calculada por Cesar
            double quality = 0;
            // si la imagen candidata no tiene descriptores se los creamos
            if (!File.Exists(candidate + CSurf.EXTENSION_DESCRIPTORS))
            {
                surf_object = new CSurf(candidate);
                surf_object.Save(candidate);

            }
            else
            {
                surf_object = new CSurf(candidate, -1, -1);
               //BUG falla al entrar aqui en pool , muchos accesoss a la misma imagen?
               // surf_object.OnlyLoadImage(filename);
            }
            model.Matching(surf_object, ref corners1, out quality, out  results, modo);

            for (int i = 0; i < 4; i++)
            {
                results.dst_corners[i] = corners1[i];

            }
            ///BUG hay que borrar tb el model???
            surf_object.Clean();
            return results;
        }
        
    }
}

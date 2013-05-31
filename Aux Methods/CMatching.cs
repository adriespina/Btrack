  
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

        public CSurf _model;
        public string _candidate;
        public int _modo;
        public TaskInfo(CSurf model, string candidate, int modo)
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
        public int MATCHING_TIMEOUT = 10000;
        public bool DOUBLE_CHECK = true;
        public int THRESHOLD_FACTOR2 = 800;
        public int THRESHOLD_INSIDE_KEYPOINTS = 51;
        public int THRESHOLD_TOTAL_KEYPOINTS = 4187;
        public int THRESHOLD_INCLUDED_HOMOGRAPHY = 7;

        int m_numberOfThreads;
        private BilletrackDispatcher _padre;
      

        public int NumberOfThreads
        {
            get { return m_numberOfThreads; }
            set
            {
                try
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
                catch (Exception e)
                {
                    
                   throw new SpinPlatform.Errors.SpinException("CMatching: NumberOfThreads : " + e.Message);
                }
            }
        }
    public  SmartThreadPool smartThreadPool ;

    public CMatching(BilletrackDispatcher padre, int threads)
     {
         try
         {
             this._padre = padre;
             NumberOfThreads = threads;
         }
         catch (Exception e)
         {
             
               throw new SpinPlatform.Errors.SpinException("CMatching: Constructor CMatching : " + e.Message);
         }
     }
    public CMatching( int threads)
    {
        try
        {
            this._padre = null;
            NumberOfThreads = threads;
        }
        catch (Exception e)
        {
            
            throw new SpinPlatform.Errors.SpinException("CMatching: Constructor CMatching : " + e.Message);
        }
    }

     public resultMatching MatchingOneToOne(string filename, string candidate,int modo)
     {
         try
         {
             string sfilename, sfilenameDescriptors;
             sfilename = filename;
             sfilenameDescriptors = filename + CSurf.EXTENSION_DESCRIPTORS;
             resultMatching resultado = new resultMatching();
             CSurf surf_image, surf_object;
             Point[] corners1 = new Point[4]; //Homografia de las esquinas de la imagen calculada por Cesar
             double quality = 0;

             try
             {
                 // si la imagen modelo no tiene descriptores se los creamos
                 if (!File.Exists(sfilenameDescriptors))
                 {
                     surf_image = new CSurf(sfilename);
                     surf_image.Save(filename);
              
                 }
                 else
                 {
                     surf_image = new CSurf(sfilename, -1, -1);

                 }
             }
             catch (Exception e)
             {

                 throw new SpinPlatform.Errors.SpinException("CMatching: Calculating descriptors original image : " + e.Message);
             }

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
             surf_image.Matching(surf_object, ref corners1, out quality, out  resultado, modo);

             for (int i = 0; i < 4; i++)
             {
                 resultado.dst_corners[i] = corners1[i];

             }
             ///BUG hay que borrar tb el model???
             surf_object.Clean();
             surf_image.Clean();

             return resultado;
         }
         catch (Exception e)
         {
             
             throw new SpinPlatform.Errors.SpinException("CMatching: MatchingOneToOne : " + e.Message);
         }
     
     
     }
     public int MatchingOneToVarius(string filename, string[] filenameMatrixObjects, out resultMatching[] pResult)
     {


         try
         {
             string sfilename, sfilenameDescriptors;
             sfilename = filename;
             sfilenameDescriptors = filename + CSurf.EXTENSION_DESCRIPTORS;
             resultMatching[] pResult2 = new resultMatching[filenameMatrixObjects.Length];
             IWorkItemResult[] tareas = new IWorkItemResult[filenameMatrixObjects.Length];
             CSurf[] modelos = new CSurf[filenameMatrixObjects.Length];
             CSurf surf_image;

             //check if exist  ¿hay que liberarlo para poder guardar?
             // si la imagen modelo no tiene descriptores se los creamos

             try
             {
                 
                 if (!File.Exists(sfilenameDescriptors))
                 {
                      surf_image = new CSurf(sfilename);
                     surf_image.Save(filename);
                    
                 }
                 else surf_image = new CSurf(sfilename,-1,-1);


                 for (int i = 0; i < filenameMatrixObjects.Length; i++)
                {
                     modelos[i] = new CSurf(surf_image);
                 }
                surf_image.Dispose();
             }
             catch (Exception err)
             {

                 throw new SpinPlatform.Errors.SpinException("CMatching: Calculating descriptors original image : " + err.Message);

             }

             int index = 0;

             //Creamos el pool de hilos con un timeout de 8 segundos
             smartThreadPool = new SmartThreadPool(MATCHING_TIMEOUT);
             smartThreadPool.MinThreads = 0;
             smartThreadPool.MaxThreads = m_numberOfThreads;


             foreach (string pos_imagen in filenameMatrixObjects)
             {
                 tareas[index] = smartThreadPool.QueueWorkItem(new WorkItemCallback(this.Match), new TaskInfo(modelos[index], pos_imagen, CSurf.MODE_CESAR));
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
             smartThreadPool.Dispose();
             pResult = pResult2;
             
             //borramos los descriptores usados si no los vamos a usar mas
            
             if (!DOUBLE_CHECK)
             { 
                 
                 for (int i = 0; i < modelos.Length; i++)
                 {
                       modelos[i].Dispose();
                       pResult2[i].Dispose();
                     
                 }
                 
             }


             //Buscamos la mejor correspodencia y enviamos el indice 
             int indice_max_quality = -1, indice_max_matches = -2, indice_max_inside = -3, it = 0;
             float max_quality = 0, max_matches = 0, max_inside = 0;
             foreach (resultMatching rst in pResult)
             {
                 if (rst != null)
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
                     sinfo.IdleTimeout = MATCHING_TIMEOUT;
                     smartThreadPool = new SmartThreadPool(sinfo);




                     foreach (string pos_imagen in filenameMatrixObjects)
                     {
                         tareas[index] = smartThreadPool.QueueWorkItem(new WorkItemCallback(this.Match), new TaskInfo(modelos[index], pos_imagen, CSurf.MODE_SURF));
                         index++;

                     }

                     //Thread.Sleep(2000);
                     success = SmartThreadPool.WaitAll(tareas, MATCHING_TIMEOUT, false);
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
                     smartThreadPool.Dispose();
                     pResult = pResult2;
                     //borramos los descriptores usados
             
                     for (int i = 0; i < modelos.Length; i++)
                     {
              
                         modelos[i].Dispose();
                         pResult2[i].Dispose();
                     }

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
         catch (Exception e)
         {

             throw new SpinPlatform.Errors.SpinException("CMatching: MatchingOneToVarius : " + e.Message);
         }
     }

       //DEBUG
     public int MatchingOneToVarius_fast(string filename, string[] filenameMatrixObjects, out resultMatching[] pResult)
     {


         try
         {
             string sfilename, sfilenameDescriptors;
             sfilename = filename;
             sfilenameDescriptors = filename + CSurf.EXTENSION_DESCRIPTORS;
             resultMatching[] pResult2 = new resultMatching[filenameMatrixObjects.Length];
             IWorkItemResult[] tareas = new IWorkItemResult[filenameMatrixObjects.Length];
             CSurf[] modelos = new CSurf[filenameMatrixObjects.Length];
             CSurf surf_image;

             //check if exist  ¿hay que liberarlo para poder guardar?
             // si la imagen modelo no tiene descriptores se los creamos

             try
             {

                 if (!File.Exists(sfilenameDescriptors))
                 {
                     surf_image = new CSurf(sfilename);
                     surf_image.Save(filename);

                 }
                 else surf_image = new CSurf(sfilename, -1, -1);


                 for (int i = 0; i < filenameMatrixObjects.Length; i++)
                 {
                     modelos[i] = new CSurf(surf_image);
                 }
                 surf_image.Dispose();
             }
             catch (Exception err)
             {

                 throw new SpinPlatform.Errors.SpinException("CMatching: Calculating descriptors original image : " + err.Message);

             }

             int index = 0;

             //Creamos el pool de hilos con un timeout de 8 segundos
             smartThreadPool = new SmartThreadPool(MATCHING_TIMEOUT);
             smartThreadPool.MinThreads = 0;
             smartThreadPool.MaxThreads = m_numberOfThreads;


             foreach (string pos_imagen in filenameMatrixObjects)
             {
                 tareas[index] = smartThreadPool.QueueWorkItem(new WorkItemCallback(this.Match), new TaskInfo(modelos[index], pos_imagen, CSurf.MODE_SURF));
                 index++;

             }


             //OPCION1 HACERLO CON UN WAIT ANY

                     int contador=0;
                     while (contador < filenameMatrixObjects.Length)
                     {
                         SmartThreadPool.WaitAny(tareas, MATCHING_TIMEOUT, false);
                         for (int i = 0; i < pResult2.Length; i++)
                         {
                             pResult2[i] = (resultMatching)tareas[i].Result;
                             //Si es un matching, paro el pool y devuelvo el indice
                             if (DecissionTree(pResult2[i]))
                             {
                                 //cierro el pool y libero memoria
                                 //smartThreadPool.Cancel(true);
                                 smartThreadPool.Shutdown();
                                 pResult = pResult2;
                                 for (int j = 0; j < modelos.Length; j++)
                                 {
                                     modelos[j].Dispose();
                                     if (pResult2[j] != null) pResult2[j].Dispose();
                                 }
                                 return i;

                             };
                         }
                         contador++;
                     }
              pResult = pResult2;
               for (int i = 0; i < modelos.Length; i++)
                     {
              
                         modelos[i].Dispose();
                         if (pResult2[i]!=null) pResult2[i].Dispose();
                     }
                     //return BestMatch(pResult);
               return -1;
                    

         }
         catch (Exception e)
         {

             throw new SpinPlatform.Errors.SpinException("CMatching: MatchingOneToVariusFast : " + e.Message);
         }
     }


        /// <summary>
        /// Funcion para calcular el matching entre 2 imagenes
        /// </summary>
        /// <param name="State">Objeto con 3 elementos : String modelo, string candidato, Int modo</param>
        /// <returns> Devuelve las estadisticas del matching</returns>
        resultMatching Match(Object State)
        {
            try
            {
                TaskInfo ti = (TaskInfo)State;
                CSurf model = (CSurf)ti._model;
                
                // model.OnlyLoadImage(filename);  //BUG hace falta hcerlo???
                resultMatching results = new resultMatching();
                string candidate = ti._candidate;
                //if (this._padre != null) this._padre.AddLogDesarrollo("haciendo matching de " + filename + " con " + candidate);
                int modo = ti._modo;
                if (candidate == "matched") return results;
                CSurf surf_object;
                Point[] corners1 = new Point[4]; //Homografia de las esquinas de la imagen calculada por Cesar
                double quality = 0;
                // si la imagen candidata no tiene descriptores se los creamos

                try
                {
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
                }
                catch (Exception e)
                {
                    surf_object = null;
                    throw new SpinPlatform.Errors.SpinException("CMatching: Error creando descriptores archivo candidato : " + candidate + " : " + e.Message);
                }
                try
                {

                    if (model != null && surf_object != null) model.Matching(surf_object, ref corners1, out quality, out  results, modo);
                }
                catch (Exception e)
                {
                    throw new SpinPlatform.Errors.SpinException("CMatching: Error performing matching : " + e.Message);

                }

                //if (this._padre != null) this._padre.AddLogDesarrollo("resultado de matching de " + filename + " con " + candidate + " . Quality = " + quality.ToString("F0"));

                for (int i = 0; i < 4; i++)
                {
                    results.dst_corners[i] = corners1[i];

                }
               
                surf_object.Dispose();
              
                return results;
            }
            catch (Exception e)
            {

                if (this._padre != null) this._padre.AddLogError("Error performing matching in thread Pool :" + e.Message);
                return null;
            }
        }
        bool DecissionTree(resultMatching result)
        {

            if (result.points_factor2 >= THRESHOLD_FACTOR2)
            {
                return true;
            }
            //else if (result.npoints_included_homography >= 10&&result.common_KeyPoints>50)
            //{
            //    return true;
            //}
            // else if(result.total_KeyPoints >= THRESHOLD_TOTAL_KEYPOINTS)  
            //{
            //      return true;
            //}
            else return false;
        }
        int BestMatch(resultMatching[] results)
        {

            try
            {
                //Buscamos la mejor correspodencia y enviamos el indice 
                int indice_max_quality = -1, indice_max_matches = -2, indice_max_includedhomography = -3, it = 0;
                float max_quality = 0, max_matches = 0, max_includedhomography = 0;
                foreach (resultMatching rst in results)
                {
                    if (rst != null)
                    {
                        if (rst.npoints_included_homography > THRESHOLD_INCLUDED_HOMOGRAPHY)
                        {

                            if (rst.quality == max_quality)
                            {
                                if (rst.common_KeyPoints > max_matches || rst.npoints_included_homography > max_includedhomography)
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
                            if (rst.npoints_included_homography > max_includedhomography)
                            {
                                indice_max_includedhomography = it;
                                max_includedhomography = rst.npoints_included_homography;
                            }

                        }
                        it++;
                    }
                }


                //si todos coinciden es una match perfecto y envio el indice
                if (indice_max_quality >= 0 && (indice_max_quality == indice_max_matches) && (indice_max_quality == indice_max_includedhomography))
                {
                    return indice_max_quality;
                }
                //Si las esquinas estan mal calculadas igual los puntos dentro son 0 aunque sea un buen match
                //else if (indice_max_includedhomography >= 0 )
                //{
                //    if (results[indice_max_includedhomography].inside_KeyPoints < 5  && results[indice_max_includedhomography].npoints_included_homography > THRESHOLD_INCLUDED_HOMOGRAPHY * 3)
                //    {
                //        return indice_max_includedhomography;
                //    }
                //    else return -1;
                 
                //}
                else
                {
                    return -1;
                }

            }
            catch (Exception e)
            {
                
                throw new SpinPlatform.Errors.SpinException("CMatching: BestMatch : " + e.Message);
            }

        
        
        }
        int BestMatchOld(resultMatching[] results, bool sendmax)
        { 
         //Buscamos la mejor correspodencia y enviamos el indice 
             int indice_max_quality = -1, indice_max_matches = -2, indice_max_inside = -3, it = 0;
             float max_quality = 0, max_matches = 0, max_inside = 0;
             foreach (resultMatching rst in results)
             {
                 if (rst != null)
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
             }

            
                 //si todos coinciden es una match perfecto y envio el indice
                 if (indice_max_quality >= 0 && (indice_max_quality == indice_max_matches) && (indice_max_quality == indice_max_inside))
                 {
                     return indice_max_quality;
                 }
                 else if (sendmax)
                 {
                     //Envio el indice de mejor calidad aunque no coincidan
                     if (max_quality >= THRESHOLD_QUALITY)
                     {
                         return indice_max_quality;
                     }
                     else return -1;
                 }
                 else
                 {
                     return -1;
                 }
        
        }
        
    }
}

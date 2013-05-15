using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Emgu.CV;
using Emgu.CV.GPU;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Drawing;

namespace Billetrack
{
    /// <summary>
    /// Class with methods used to process images in Billetrack Project
    /// Cesar Fraga May 2012
    /// </summary>
   public  class CMetodosAuxiliares
    {
        public  int NROTACIONES = 5;
        public  int THRESHOLD_FOR_CROP = 65;
        public int BILLET_ROTATION_ANGLE = -54;
        public  string PATH_EXAMPLE_IMG = "sample.jpg";
        public  string PATHSAVE_ONLINE_IMG = "online.jpg";

        public PointsOfLine m_RefLine1 = new PointsOfLine(931, 574, 641, 971, true);
        public PointsOfLine m_RefLine2 = new PointsOfLine(911, 840, 620, 1234, true);
        public SearchArea m_billetSearch = new SearchArea(361, 11, 568, 129, 40, 255, 200, 8);
        public SearchArea m_SpotlightOnOff = new SearchArea(874, 115, 1093, 77, 10, 50, 0, 0);



        public Image<Gray, byte> RotateAndCropImage(Image<Gray, byte> image_processed)
        {
            try
            {
                Rectangle rect = new Rectangle();
                return RotateAndCropImageForBilletrack(image_processed, BILLET_ROTATION_ANGLE, ref rect);
            }
            catch (Exception e)
            {
                
                 throw new SpinPlatform.Errors.SpinException("CMetodosAuxiliares: RotateAndCropImage : " + e.Message);
            }
        
        
        }

        public Image<Gray, byte> RotateAndCropImageForBilletrack(Image<Gray, byte> image_processed, int angle, ref Rectangle rect)
        {
            try
            {
                Image<Gray, byte>[] rotadas = new Image<Gray, byte>[NROTACIONES];
                Image<Gray, byte>[] cortadas = new Image<Gray, byte>[NROTACIONES];
                Image<Gray, byte> binaria;
                Rectangle re = new Rectangle();
                double max = 0;
                int posmax = 0;
                int angulo = angle - 8;
                //rotate& crop  the billet in diferent angles and select the best result
                for (int i = 0; i < NROTACIONES; i++, angulo = angulo + 4)
                {
                    rotadas[i] = image_processed.Rotate(angulo, new Gray(0));
                    cortadas[i] = CropBasedInProyectionsBinary(rotadas[i], THRESHOLD_FOR_CROP, 0, ref re);
                    cortadas[i] = CropOnlyProyectionVertical(cortadas[i], THRESHOLD_FOR_CROP, ref re);
                    binaria = cortadas[i].ThresholdBinary(new Gray(THRESHOLD_FOR_CROP), new Gray(255));
                    Gray sum = binaria.GetSum();
                    if (sum.Intensity > max)
                    {
                        max = sum.Intensity;
                        posmax = i;
                        rect = re;
                    }
                }
                Image<Gray, byte> automatedCrop = cortadas[posmax].Copy();
                for (int i = 0; i < NROTACIONES; i++)
                {
                    rotadas[i].Dispose();
                    cortadas[i].Dispose();
                }



                return automatedCrop;
            }
            catch (Exception e)
            {
                throw new SpinPlatform.Errors.SpinException("CMetodosAuxiliares: RotateAndCropImageForBilletrack : " + e.Message);
            }


        }
        //public Image<Gray, byte> RotateAndCropImageForBilletrack2(Image<Gray, byte> image_processed, int angle, ref Rectangle rect)
        //{
        //    Image<Gray, byte> rotadas = new Image<Gray, byte>(image_processed.Size);
        //    Image<Gray, byte> cortadas = new Image<Gray, byte>(image_processed.Size);
        //    Image<Gray, byte> binaria;
        //     Image<Gray, float> gradiente;
        //    Rectangle re = new Rectangle();
        //    double max = 0;
        //    int posmax = 0;
        //    int angulo = angle - 8;
        //    float [,] sobel= new float[3,3];sobel[0,0]=-1;sobel[0,1]=0;sobel[0,2]=1;sobel[1,0]=-2;sobel[1,1]=0;sobel[1,2]=2;sobel[2,0]=-1;sobel[2,1]=0;sobel[2,2]=1;
        //    ConvolutionKernelF conv= new ConvolutionKernelF(sobel);
        //    double 
        //    //rotate& crop  the billet in diferent angles and select the best result
        //    for (int i = 0; i < NROTACIONES; i++, angulo = angulo + 4)
        //    {
        //        rotadas = image_processed.Rotate(angulo, new Gray(0));
        //        binaria = rotadas.ThresholdBinary(new Gray(THRESHOLD_FOR_CROP), new Gray(255));
        //        gradiente= binaria.Convolution(conv);
        //        Matrix<float> vec_row = new Matrix<float>(1, gradiente.Width);
        //        gradiente.Reduce<float>(vec_row, REDUCE_DIMENSION.SINGLE_ROW, REDUCE_TYPE.CV_REDUCE_AVG);//0 reduce to row - 1 redice to colum
        //        vec_row.MinMax(





        //        cortadas[i] = CropBasedInProyectionsBinary(rotadas[i], THRESHOLD_FOR_CROP, 0, ref re);
        //        cortadas[i] = CropOnlyProyectionVertical(cortadas[i], THRESHOLD_FOR_CROP, ref re);
        //        binaria = cortadas[i].ThresholdBinary(new Gray(THRESHOLD_FOR_CROP), new Gray(255));
        //        Gray sum = binaria.GetSum();
        //        if (sum.Intensity > max)
        //        {
        //            max = sum.Intensity;
        //            posmax = i;
        //            rect = re;
        //        }
        //    }
        //    Image<Gray, byte> automatedCrop = cortadas[posmax].Copy();
        //    for (int i = 0; i < NROTACIONES; i++)
        //    {
        //        rotadas[i].Dispose();
        //        cortadas[i].Dispose();
        //    }



        //    return automatedCrop;


        //}

        public Image<Gray, byte> CropBasedInProyectionsBinary(Image<Gray, byte> image_orig, int thresholdv, int vsafe, ref Rectangle window)
        {
            try
            {

                using (Image<Gray, byte> Binaria = image_orig.ThresholdBinary(new Gray(thresholdv), new Gray(255)))
                {
                    CropBasedInProyections(Binaria, thresholdv, thresholdv, thresholdv, thresholdv, vsafe, ref  window);
                    return image_orig.GetSubRect(window);
                }
            }
            catch (Exception e)
            {
                
                throw new SpinPlatform.Errors.SpinException("CMetodosAuxiliares: CropBasedInProyectionsBinary : " + e.Message);
            }

        }

        public int SelectBestAngle(Image<Gray, byte> image_orig,int angle)
        {
            try
            {
                int[] angles = new int[20];
                for (int i = 0; i < 20; i++)
                {
                    angles[i] = angle - 50 + i * 5;
                }

                return angle;

            }
            catch (Exception e)
            {
                
                throw new SpinPlatform.Errors.SpinException("CMetodosAuxiliares: SelectBestAngle : " + e.Message);
            }
        
        }

        public Image<Gray, byte> CropBasedInProyections(Image<Gray, byte> image_orig, int thresholdLeft, int thresholdRight, int thresholdUp, int thresholdDown, int vsafe, ref Rectangle window)
        {
            try
            {
                Matrix<float> vec_row = new Matrix<float>(1, image_orig.Width);
                image_orig.Reduce<float>(vec_row, REDUCE_DIMENSION.SINGLE_ROW, REDUCE_TYPE.CV_REDUCE_AVG);//0 reduce to row - 1 redice to colum


                // Matrix<float> dst=vec_row>10;
                Point pt = new Point();
                int x = 0, y = 0, rangey = image_orig.Height, rangex = image_orig.Width;

                if (vec_row.CheckRange(0, thresholdLeft, ref pt) == false)
                {
                    if (pt.X - vsafe > 0)
                        x = pt.X - vsafe;
                    else
                        x = pt.X;
                    rangex = image_orig.Width - x;


                }
                else
                {

                    x = 0;
                }
                CvInvoke.cvFlip(vec_row.Ptr, vec_row.Ptr, 1);//


                if (vec_row.CheckRange(0, thresholdRight, ref pt) == false)
                {
                    if (image_orig.Width - pt.X - x + 2 * vsafe > 0)
                    {

                        rangex = image_orig.Width - pt.X - x + 2 * vsafe;
                    }
                    else
                        rangex = image_orig.Width - x;


                }
                else
                {

                    rangex = image_orig.Width - x;
                }
                vec_row = new Matrix<float>(image_orig.Height, 1);
                image_orig.Reduce<float>(vec_row, REDUCE_DIMENSION.SINGLE_COL, REDUCE_TYPE.CV_REDUCE_AVG);//0 reduce to row - 1 redice to colum		
                if (vec_row.CheckRange(0, thresholdUp, ref pt) == false)
                {
                    if (pt.Y - vsafe > 0)
                        y = pt.Y - vsafe;
                    else
                        y = pt.Y;
                    rangey = image_orig.Height - y;


                }
                else
                {

                    y = 0;
                }
                CvInvoke.cvFlip(vec_row.Ptr, vec_row.Ptr, -1);//


                if (vec_row.CheckRange(0, thresholdDown, ref pt) == false)
                {
                    if (pt.Y > 2 * vsafe)
                    {
                        rangey = image_orig.Height - pt.Y - y + 2 * vsafe;
                        if (rangey <= 0)
                        {
                            rangey = image_orig.Height - y;
                        }
                    }
                    else
                    {
                        rangey = image_orig.Height - pt.Y - y;
                        if (rangey <= 0)
                        {
                            rangey = image_orig.Height - y;
                        }
                    }

                }
                else
                {

                    rangey = image_orig.Height - y;
                }

                window = new Rectangle(x, y, rangex, rangey);
                vec_row.Dispose();

                if ((0 <= x) && (0 <= rangex) && (x + rangex <= image_orig.Width) && (0 <= y) && (0 <= rangey) && (y + rangey <= image_orig.Height) && (rangey >= 50) && (rangex >= 50))
                {

                    Image<Gray, byte> imageCropedd = image_orig.GetSubRect(window);
                    return imageCropedd;

                }
                else
                {
                    window = new Rectangle(0, 0, image_orig.Width, image_orig.Height);
                    return image_orig;
                }
            }
            catch (Exception e)
            {
                
                throw new SpinPlatform.Errors.SpinException("CMetodosAuxiliares: CropBasedInProyections : " + e.Message);
            }
        }

        public Image<Gray, byte> CropOnlyProyectionVertical(Image<Gray, byte> image_orig, int thresholdv, ref Rectangle windowout)
        {
            try
            {
                Size tamno = image_orig.Size;
                Matrix<float> vec_row = new Matrix<float>(image_orig.Height, 1);
                using (Image<Gray, byte> Binaria = image_orig.ThresholdBinary(new Gray(thresholdv), new Gray(255)))
                {
                    image_orig.Reduce<float>(vec_row, REDUCE_DIMENSION.SINGLE_COL, REDUCE_TYPE.CV_REDUCE_AVG);//0 reduce to row - 1 redice to colum	

                    double maxVal = 0;
                    double minVal = 0;
                    Point pMax, pMin;
                    vec_row.MinMax(out minVal, out maxVal, out pMin, out pMax);

                    int rangey;

                    if (minVal == 0.0)
                        rangey = pMin.Y;
                    else
                        rangey = tamno.Height;
                    int x = 0;
                    int y = 0;
                    int rangex = tamno.Width;

                    Rectangle window = new Rectangle(x, y, rangex, rangey);
                    if ((0 <= x) && (0 <= rangex) && (x + rangex <= tamno.Width) && (0 <= y) && (0 <= rangey) && (y + rangey <= tamno.Height) && (rangey >= 50) && (rangex >= 50))
                    {

                        Image<Gray, byte> imageCropedd = image_orig.GetSubRect(window);

                        windowout.Height = rangey; //el resto de los valores ya fueron recortados y completados en procesamiento previo
                        return imageCropedd;


                    }
                    else
                    {

                        window = new Rectangle(0, 0, image_orig.Width, image_orig.Height);
                        return image_orig;
                    }
                }
            }
            catch (Exception e)
            {
                
                throw new SpinPlatform.Errors.SpinException("CMetodosAuxiliares: CropOnlyProyectionVertical : " + e.Message);
            }
        }

        public DenseHistogram GetMaxHistogram(Image<Gray, byte> src_Image, bool bNonZero, out int pancho_hist, out int pMaxValue, out int posmax, ref int[] bin, out int pPosPtoSaturation)
        {

            try
            {

                DenseHistogram Histo;
                int sbins;
                if (bNonZero)
                {
                    Histo = new DenseHistogram(255, new RangeF(1, 255));
                    sbins = 255;
                }
                else
                {
                    Histo = new DenseHistogram(256, new RangeF(0, 255));
                    sbins = 256;
                }

                Histo.Calculate(new Image<Gray, Byte>[] { src_Image }, true, null);


                double[] posBin = { 0.1, 0.5, 0.75, 0.95 }; //en porcentaje

                float maxVal = 0;
                float minVal = 0;
                int[] minpos, maxpos;

                Histo.MinMax(out minVal, out maxVal, out minpos, out maxpos);

                Matrix<float> Hist = new Matrix<float>(1, sbins);

                for (int i = 0; i < sbins; i++)
                {
                    Hist[0, i] = (float)Histo.MatND.ManagedArray.GetValue(i);
                }



                //  Histo.Clear();

                int divisiones_max_hist = 6;
                Point pt = new Point();
                int x1_ancho, x2_ancho, ancho_hist;

                if (Hist.CheckRange(0, maxVal / divisiones_max_hist, ref pt) == false)
                {
                    x1_ancho = pt.Y;
                }
                else
                    x1_ancho = 0;



                CvInvoke.cvFlip(Hist.Ptr, Hist.Ptr, -1);//
                if (Hist.CheckRange(0, maxVal / divisiones_max_hist, ref pt) == false)
                {
                    x2_ancho = sbins - pt.Y;
                }
                else
                    x2_ancho = 0;

                pancho_hist = x2_ancho - x1_ancho;
                pMaxValue = (int)Math.Round(maxVal);

                //Busquemos el punto que acumule el 0.2%
                int objetivo = (int)(1.0 / 100 * src_Image.Rows * src_Image.Cols);
                float acumulado = 0;
                int posPtoSaturacion = 0;
                for (int s = 0; s < sbins; s++)
                {
                    float binVal = Hist[0, s];
                    acumulado += binVal;
                    if (acumulado > objetivo)
                    {
                        posPtoSaturacion = s;
                        break;
                    }

                }
                posPtoSaturacion = sbins - posPtoSaturacion;//estamos recorriendo de mas luminosidad a menos

                pPosPtoSaturation = posPtoSaturacion;


                if (bin != null)
                {
                    CvInvoke.cvFlip(Hist.Ptr, Hist.Ptr, -1); //para calcular el ancho se ha inveertido previamente el histograma

                    int limiteSuperior;
                    int limiteInferior = 0;
                    float total = 0;
                    float[] subtotal = new float[5];
                    for (int k = 0; k < 5; k++)
                    {
                        if (k == 4)
                            limiteSuperior = sbins;
                        else
                            limiteSuperior = (int)posBin[k] * sbins;
                        subtotal[k] = 0;
                        for (int s = limiteInferior; s < limiteSuperior; s++)
                        {
                            float binVal = Hist[0, s];
                            total += binVal;
                            subtotal[k] += binVal;
                        }
                        //printf("\nLimiteInf:%d\n",limiteInferior);
                        limiteInferior = limiteSuperior;
                    }
                    if (total > 0)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            bin[k] = (int)(subtotal[k] / total * 100);
                        }

                    }
                }


                if (maxpos.Length > 0)
                {
                    if (bNonZero)

                        posmax = (int)maxpos.GetValue(0) + 1;


                    else

                        posmax = (int)maxpos.GetValue(0);
                }
                else posmax = 0;
                return Histo;
            }
            catch (Exception e)
            {
                
                      throw new SpinPlatform.Errors.SpinException("CMetodosAuxiliares: GetMaxHistogram : " + e.Message);
            }

        }

        public double GetMeanOnWindow(Image<Gray, byte> src_Image, int rowi, int coli, int nrows, int ncols)
        {
            try
            {

                double cuenta = 0;
                double acumulador = 0;
                for (int k = 0; k < nrows; k++)
                {
                    for (int j = 0; j < ncols; j++)
                    {
                        acumulador += src_Image[rowi + k, coli + j].Intensity;
                        cuenta++;
                    }
                }

                double media = -1;
                if (cuenta > 0)
                    media = acumulador / cuenta;
                return media;
            }
            catch (Exception e)
            {
                throw new SpinPlatform.Errors.SpinException("CMetodosAuxiliares: GetMeanOnWindow : " + e.Message);
            }
        }


        /// <summary>
        /// Calculate Exposure time for auto exposure mode using linear relationship between ratio of gray objetive and curent gray.
        /// </summary>
        /// <param name="grayCurrent">Current gray value</param>
        /// <param name="grayObjetive">Gray Objetive</param>
        /// <param name="currentExposure">Current exposure time</param>
        /// <returns>The exposure time calculated</returns>
        public int CalculateExposureTimeSimple(int grayCurrent, int grayObjetive, int currentExposure)
        {
            try
            {
                if (grayCurrent >= 255)
                    return currentExposure / 2;
                if (grayCurrent == 0)
                    return currentExposure * 2;
                int FinalExposure = grayObjetive * currentExposure / grayCurrent;
                return FinalExposure;
            }
            catch (Exception e)
            {
                throw new SpinPlatform.Errors.SpinException("CMetodosAuxiliares: CalculateExposureTimeSimple : " + e.Message);
            }
        }

        /// <summary>
        /// Print on the image the date and the search areas, saving the result in a file
        /// </summary>
        /// <param name="src_Image">image to be written</param>
        /// <param name="m_bShowSearchArea">if drawing the search area</param>
        public Image<Rgb, byte> StampTimeAndSaveImg(Image<Gray, byte> src_Image, bool m_bShowSearchArea)
        {
            try
            {
                //GetTime

                DateTime now = DateTime.Now;
                string stime = now.Day.ToString("00") + "/" + now.Month.ToString("00") + "/" + now.Year.ToString("0000") + "  " + now.Hour.ToString("00") + ":" + now.Minute.ToString("00") + ":" + now.Second.ToString("00");


                MCvScalar colorred = new MCvScalar(0, 0, 255);
                MCvScalar colorblue = new MCvScalar(255, 0, 0);
                MCvScalar colorgreen = new MCvScalar(0, 255, 0);

                int thickness = 2;
                int thickness_p = 1;
                MCvFont fuente, fuente_pequena;
                double scale = 2.0;
                double scale_p = 1;
                fuente = new MCvFont(FONT.CV_FONT_HERSHEY_DUPLEX, scale, scale);
                fuente_pequena = new MCvFont(FONT.CV_FONT_HERSHEY_DUPLEX, scale_p, scale_p);
                fuente.thickness = thickness;

                //To put the text at up
                Point pointText = new Point(1, 60);

                Image<Rgb, byte> image_color = new Image<Rgb, byte>(src_Image.Width, src_Image.Height);
                CvInvoke.cvCvtColor(src_Image.Ptr, image_color.Ptr, COLOR_CONVERSION.CV_GRAY2BGR);


                CvInvoke.cvPutText(image_color.Ptr, stime, pointText, ref fuente, colorblue);



                if (m_bShowSearchArea)
                {

                    //To draw reference lines
                    if (m_RefLine1.bDraw)
                        CvInvoke.cvLine(image_color.Ptr, m_RefLine1.initPoint, m_RefLine1.endPoint, colorred, thickness, LINE_TYPE.EIGHT_CONNECTED, 0);

                    if (m_RefLine2.bDraw)
                        CvInvoke.cvLine(image_color.Ptr, m_RefLine2.initPoint, m_RefLine2.endPoint, colorred, thickness, LINE_TYPE.EIGHT_CONNECTED, 0);


                    //To detect Billet
                    CvInvoke.cvLine(image_color, new Point(m_billetSearch.initialCol, m_billetSearch.initialRow),
                                     new Point(m_billetSearch.initialCol + m_billetSearch.nCols, m_billetSearch.initialRow), colorred, thickness, LINE_TYPE.EIGHT_CONNECTED, 0);
                    CvInvoke.cvLine(image_color, new Point(m_billetSearch.initialCol + m_billetSearch.nCols, m_billetSearch.initialRow),
                                     new Point(m_billetSearch.initialCol + m_billetSearch.nCols, m_billetSearch.initialRow + m_billetSearch.nRows), colorred, thickness, LINE_TYPE.EIGHT_CONNECTED, 0);
                    CvInvoke.cvLine(image_color, new Point(m_billetSearch.initialCol + m_billetSearch.nCols, m_billetSearch.initialRow + m_billetSearch.nRows),
                                     new Point(m_billetSearch.initialCol, m_billetSearch.initialRow + m_billetSearch.nRows), colorred, thickness, LINE_TYPE.EIGHT_CONNECTED, 0);
                    CvInvoke.cvLine(image_color, new Point(m_billetSearch.initialCol, m_billetSearch.initialRow + m_billetSearch.nRows),
                                     new Point(m_billetSearch.initialCol, m_billetSearch.initialRow), colorred, thickness, LINE_TYPE.EIGHT_CONNECTED, 0);

                    double Billet_Search_media = GetMeanOnWindow(src_Image, m_billetSearch.initialRow, m_billetSearch.initialCol, m_billetSearch.nRows, m_billetSearch.nCols);

                    m_billetSearch.valueCalculated = Billet_Search_media;

                    pointText.X = m_billetSearch.initialCol;
                    pointText.Y = m_billetSearch.initialRow - 10;
                    stime = Billet_Search_media.ToString("F1");
                    CvInvoke.cvPutText(image_color.Ptr, stime, pointText, ref fuente, colorblue);

                    pointText.X = m_billetSearch.initialCol + m_billetSearch.nCols;
                    pointText.Y = m_billetSearch.initialRow + m_billetSearch.nRows;
                    stime = m_billetSearch.Max.ToString("");
                    CvInvoke.cvPutText(image_color.Ptr, stime, pointText, ref fuente_pequena, colorblue);

                    pointText.X = m_billetSearch.initialCol - 50;
                    pointText.Y = m_billetSearch.initialRow + m_billetSearch.nRows;
                    stime = m_billetSearch.Min.ToString("");
                    CvInvoke.cvPutText(image_color.Ptr, stime, pointText, ref fuente_pequena, colorblue);


                    //To detect SpotlightOnOff

                    CvInvoke.cvLine(image_color, new Point(m_SpotlightOnOff.initialCol, m_SpotlightOnOff.initialRow),
                                    new Point(m_SpotlightOnOff.initialCol + m_SpotlightOnOff.nCols, m_SpotlightOnOff.initialRow), colorred, thickness, LINE_TYPE.EIGHT_CONNECTED, 0);
                    CvInvoke.cvLine(image_color, new Point(m_SpotlightOnOff.initialCol + m_SpotlightOnOff.nCols, m_SpotlightOnOff.initialRow),
                                    new Point(m_SpotlightOnOff.initialCol + m_SpotlightOnOff.nCols, m_SpotlightOnOff.initialRow + m_SpotlightOnOff.nRows), colorred, thickness, LINE_TYPE.EIGHT_CONNECTED, 0);
                    CvInvoke.cvLine(image_color, new Point(m_SpotlightOnOff.initialCol + m_SpotlightOnOff.nCols, m_SpotlightOnOff.initialRow + m_SpotlightOnOff.nRows),
                                    new Point(m_SpotlightOnOff.initialCol, m_SpotlightOnOff.initialRow + m_SpotlightOnOff.nRows), colorred, thickness, LINE_TYPE.EIGHT_CONNECTED, 0);
                    CvInvoke.cvLine(image_color, new Point(m_SpotlightOnOff.initialCol, m_SpotlightOnOff.initialRow + m_SpotlightOnOff.nRows),
                                    new Point(m_SpotlightOnOff.initialCol, m_SpotlightOnOff.initialRow), colorred, thickness, LINE_TYPE.EIGHT_CONNECTED, 0);


                    m_SpotlightOnOff.valueCalculated = GetMeanOnWindow(src_Image, m_SpotlightOnOff.initialRow, m_SpotlightOnOff.initialCol, m_SpotlightOnOff.nRows, m_SpotlightOnOff.nCols);

                    pointText.Y = m_SpotlightOnOff.initialRow;
                    pointText.X = m_SpotlightOnOff.initialCol - 10;
                    stime = m_SpotlightOnOff.valueCalculated.ToString("F1");
                    CvInvoke.cvPutText(image_color.Ptr, stime, pointText, ref fuente, colorgreen);

                    pointText.X = m_SpotlightOnOff.initialCol + m_SpotlightOnOff.nCols;
                    pointText.Y = m_SpotlightOnOff.initialRow + m_SpotlightOnOff.nRows;
                    stime = m_SpotlightOnOff.Max.ToString();
                    CvInvoke.cvPutText(image_color.Ptr, stime, pointText, ref fuente_pequena, colorgreen);

                    pointText.X = m_SpotlightOnOff.initialCol - 50;
                    pointText.Y = m_SpotlightOnOff.initialRow + m_SpotlightOnOff.nRows;
                    stime = m_SpotlightOnOff.Min.ToString();
                    CvInvoke.cvPutText(image_color.Ptr, stime, pointText, ref fuente_pequena, colorgreen);

                }
                image_color.Save(PATHSAVE_ONLINE_IMG);
                return image_color;
            }
            catch (Exception e)
            {
                throw new SpinPlatform.Errors.SpinException("CMetodosAuxiliares: StampTimeAndSaveImg : " + e.Message);
            }
        }

        public imgStats CalculateStatsImage(Image<Gray, byte> src_Image, int ExposureTime)
        {
            try
            {
                imgStats stats = new imgStats();

                MCvScalar mean = new MCvScalar(0);
                MCvScalar stddev = new MCvScalar(0);
                MCvScalar suma = new MCvScalar(0);
                int NonZero;
                double MediaNonZero;
                CvInvoke.cvAvgSdv(src_Image.Ptr, ref mean, ref stddev, new IntPtr());

                suma = src_Image.GetSum().MCvScalar;
                int[] nonz = src_Image.CountNonzero();
                NonZero = nonz[0];
                MediaNonZero = suma.v0 / NonZero;

                int ancho_hist, ancho_histNonZero, maxHistNonZero, maxHist, ptosat;
                int[] bin = new int[5];
                int maxVal, maxValNonZero;
                GetMaxHistogram(src_Image, false, out ancho_hist, out maxVal, out maxHist, ref bin, out ptosat);
                GetMaxHistogram(src_Image, true, out ancho_histNonZero, out maxValNonZero, out maxHistNonZero, ref bin, out ptosat);


                //FOCUS
                //stats->FocusIndicator=CalculateFocusIndicator(src_Image);
                stats.FocusIndicator = -1;
                stats.Param_Focus_normalizefreq = 0.376;

                stats.Media = mean.v0;
                stats.Std = stddev.v0;
                stats.Suma = (int)suma.v0;
                stats.NonZero = NonZero;
                stats.MediaNonZero = MediaNonZero;
                stats.MaxHist = maxHist;
                stats.AnchoHist = ancho_hist;
                stats.MaxHistNonZero = maxHistNonZero;
                stats.AnchoHistNonZero = ancho_histNonZero;
                stats.Width = src_Image.Size.Width;
                stats.Height = src_Image.Size.Height;
                stats.nPixels = stats.Width * stats.Height;
                stats.percNonZero = (double)NonZero / (double)stats.nPixels * 100;
                stats.percHeightHist = (double)maxVal / (double)stats.nPixels * 100;
                stats.percHeightHistNonZero = (double)maxValNonZero / (double)NonZero * 100;
                stats.ExposureTime = ExposureTime;
                return stats;
            }
            catch (Exception e)
            {
                 throw new SpinPlatform.Errors.SpinException("CMetodosAuxiliares: CalculateStatsImage : " + e.Message);
            }
        }

    }



}

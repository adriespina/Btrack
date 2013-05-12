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
using Emgu.CV.Flann;
using SpinPlatform.Errors;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Billetrack
{
    public class resultMatching
    {
        public float quality;
        public float quality_surf;
        public Point[] dst_corners; // son 4 elementos
        public int total_KeyPoints;
        public int common_KeyPoints;
        public int inside_KeyPoints;
        public int common_KeyPoints_surf;
        public int inside_KeyPoints_surf;
        public int total_other_keyPoints;
        public Point[] dst_corners_local;
        public bool bdst_corners_inside;
        public bool bdst_corners_local_inside;
        public bool bHomographyOK;
        public int npoints_Homography;
        public int npoints_Homography_inside;
        public int npoints_included_homography;
        public bool bFailToCalculate;		///< The object could no calculated because there was no image,keypoints and descriptors

        public resultMatching()
        {
            dst_corners = new Point[4];
            dst_corners_local = new Point[4];

            quality = 0;
            for (int kk = 0; kk < 4; kk++)
            {
                dst_corners[kk].X = -1;
                dst_corners[kk].Y = -1;
                dst_corners_local[kk].X = -1;
                dst_corners_local[kk].Y = -1;
            }
            total_KeyPoints = 0;
            common_KeyPoints = 0;
            inside_KeyPoints = 0;
            total_other_keyPoints = 0;

            bdst_corners_inside = false;
            bdst_corners_local_inside = false;
            bHomographyOK = false;
            npoints_Homography = 0;
            npoints_Homography_inside = 0;
            bFailToCalculate = false;
        }
        public void Init()
        {
            dst_corners = new Point[4];
            dst_corners_local = new Point[4];

            quality = 0;
            for (int kk = 0; kk < 4; kk++)
            {
                dst_corners[kk].X = -1;
                dst_corners[kk].Y = -1;
                dst_corners_local[kk].X = -1;
                dst_corners_local[kk].Y = -1;
            }
            total_KeyPoints = 0;
            common_KeyPoints = 0;
            inside_KeyPoints = 0;
            total_other_keyPoints = 0;

            bdst_corners_inside = false;
            bdst_corners_local_inside = false;
            bHomographyOK = false;
            npoints_Homography = 0;
            npoints_Homography_inside = 0;
            bFailToCalculate = false;


        }


    }

    class configurationSurf
    {
        public double hessianThreshold;
        public bool removeBlackPoints;	///< KeyPoints are claculated and later checked if they are all black
        public bool ignoreBlackPoints;	///< KeyPoints could be claculated in the past without removing black points
    }
    class InsidePoly
    {
        public const int INSIDE = 1;
        public const int OUTSIDE = 0;
        public const int VISIBLE = 1;
        public const int NO_VISIBLE = 0;
        public const double __DBL_EPSILON__ = 2.2204460492503131e-16;
        public int Min(int x, int y) { return x < y ? x : y; }
        public int Max(int x, int y) { return x > y ? x : y; }
        public double Min(double x, double y) { return x < y ? x : y; }
        public double Max(double x, double y) { return x > y ? x : y; }
        public int InsidePolygon(_Point[] polygon, int N, _Point p, int bound)
        {
            //cross points count of x
            int __count = 0;

            //neighbour bound vertices
            _Point p1, p2;

            //left vertex
            p1 = polygon[0];

            //check all rays
            for (int i = 1; i <= N; ++i)
            {
                //point is an vertex
                if (p == p1) return bound;

                //right vertex
                p2 = polygon[i % N];

                //ray is outside of our interests
                if (p.y < Min(p1.y, p2.y) || p.y > Max(p1.y, p2.y))
                {
                    //next ray left point
                    p1 = p2; continue;
                }

                //ray is crossing over by the algorithm (common part of)
                if (p.y > Min(p1.y, p2.y) && p.y < Max(p1.y, p2.y))
                {
                    //x is before of ray
                    if (p.x <= Max(p1.x, p2.x))
                    {
                        //overlies on a horizontal ray
                        if (p1.y == p2.y && p.x >= Min(p1.x, p2.x)) return bound;

                        //ray is vertical
                        if (p1.x == p2.x)
                        {
                            //overlies on a ray
                            if (p1.x == p.x) return bound;
                            //before ray
                            else ++__count;
                        }

                        //cross point on the left side
                        else
                        {
                            //cross point of x
                            double xinters = (p.y - p1.y) * (p2.x - p1.x) / (p2.y - p1.y) + p1.x;

                            //overlies on a ray
                            if (Math.Abs((p.x - xinters)) < __DBL_EPSILON__) return bound;

                            //before ray
                            if (p.x < xinters) ++__count;
                        }
                    }
                }
                //special case when ray is crossing through the vertex
                else
                {
                    //p crossing over p2
                    if (p.y == p2.y && p.x <= p2.x)
                    {
                        //next vertex
                        _Point p3 = new _Point((double)(polygon[(i + 1) % N].x), (double)(polygon[(i + 1) % N].y));

                        //p.y lies between p1.y & p3.y
                        if (p.y >= Min(p1.y, p3.y) && p.y <= Max(p1.y, p3.y))
                        {
                            ++__count;
                        }
                        else
                        {
                            __count += 2;
                        }
                    }
                }

                //next ray left point
                p1 = p2;
            }

            //EVEN
            if (__count % 2 == 0) return (OUTSIDE);
            //ODD
            else return (INSIDE);


        }
    }

    class _Point
    {

        public double x;
        public double y;

        public _Point()
        {
            x = 0;
            y = 0;
        }
        public _Point(double x1, double y1)
        {
            x = x1;
            y = y1;
        }

        public static bool Igual(_Point _Left, _Point _right)
        {
            return _Left.x == _right.x && _Left.y == _right.y;
        }


    }


    class CSurf : IDisposable
    {

        public const int MARGEN_PIXELS = 10;
        public const string EXTENSION_KEYPOINTS = ".KEYPOINTS";
        public const string EXTENSION_DESCRIPTORS = ".DESCRIPTORS";
        public const int SURF_SIZE_WINDOW_WITH = 640;
        public const int SURF_SIZE_WINDOW_HEIGHT = 480;
        public const int TYPE_CORRESPOND_ALL = 1;
        public const int TYPE_CORRESPOND_ONLY_HOMOGRAPHY = 2;
        public const int DEFAUL_HESSIANTHRESHOLD = 10;
        public const int MODE_CESAR = 0;
        public const int MODE_SURF = 1;


        //public
        public VectorOfKeyPoint m_pKeyPoints;
        public Matrix<float> m_pDescriptors;
        public List<int> m_pPairs;
        public List<int> m_pPairsInside;
        public List<int> m_pPairsBelongHomography;
        public List<float> m_pDist;
        public SURFDetector surfCPU;
        public resultMatching m_resultMatching;
        public HomographyMatrix m_MatrixHomographyMat;
      
        //private

        MCvSURFParams m_params;
        Point[] m_src_corners;
        Image<Gray, byte> m_image;     
        configurationSurf m_config;       
        bool m_bIsInitiated;
        System.Threading.Mutex  locker1, locker2, locker3, locker4;
        int m_point_included_homography;

        //metodos privados

        bool PrivateInit(double hessianThreshold = DEFAUL_HESSIANTHRESHOLD)
        {


            //m_pimage=new Image<Gray,byte>(  No la inizializo todavia
            m_pKeyPoints = new VectorOfKeyPoint();
            m_pDescriptors = new Matrix<float>(1, 1);
            surfCPU = new SURFDetector(hessianThreshold, false);
         
            m_src_corners = new Point[4];
            m_pPairs = new List<int>();
            m_pDist = new List<float>();
            m_pPairs.Clear();
            m_pDist.Clear();
            m_pPairsInside = new List<int>();
            m_pPairsBelongHomography = new List<int>();
            m_bIsInitiated = false;
            m_point_included_homography = 0;


            m_config = new configurationSurf();
            m_config.hessianThreshold = hessianThreshold;
            m_config.ignoreBlackPoints = true;
            m_config.removeBlackPoints = true;
           
            m_params = new MCvSURFParams(hessianThreshold, true);
            m_MatrixHomographyMat = new HomographyMatrix();
            m_resultMatching = new resultMatching();

            locker1 = new System.Threading.Mutex();
            locker2 = new System.Threading.Mutex();
            locker3 = new System.Threading.Mutex();
            locker4 = new System.Threading.Mutex();
           
            
           

               return true;

        }
       public void Clean()
        {
            if (m_pKeyPoints!=null) m_pKeyPoints.Dispose();
            if (m_pDescriptors != null) m_pDescriptors.Dispose();
            if (surfCPU != null) surfCPU.Dispose();
            if (m_pPairs != null) m_pPairs.Clear();
            if (m_pDist != null) m_pDist.Clear();
             m_bIsInitiated = false;
            if (m_MatrixHomographyMat != null) m_MatrixHomographyMat.Dispose();
            if (m_image!=null) m_image.Dispose();
            if (m_pPairsInside != null) m_pPairsInside.Clear();
            if (m_pPairsBelongHomography != null) m_pPairsBelongHomography.Clear();
           
        }
        void RemoveBadKeyPoints()
        {

            if (m_image.Ptr == null)
                return;
            int N = m_pKeyPoints.Size;
            Point center = new Point();
            int radius;
            int i = 0;
            using (Image<Gray, byte> mask = new Image<Gray, byte>(m_image.Width, m_image.Height))
            {
                mask.SetValue(255);
                foreach (MKeyPoint r in m_pKeyPoints.ToArray())
                {


                    center.X = (int)Math.Round(r.Point.X);
                    center.Y = (int)Math.Round(r.Point.Y);
                    radius = (int)Math.Round(r.Size * 1.2 / 9 * 2);

                    int grayvalue1 = (int)m_image[center.Y, center.X].Intensity;
                    int grayvalue2 = (int)m_image[center.Y, center.X + radius].Intensity;
                    int grayvalue3 = (int)m_image[center.Y, center.X - radius].Intensity;
                    int grayvalue4 = (int)m_image[center.Y - radius, center.X - radius].Intensity;
                    int grayvalue5 = (int)m_image[center.Y - radius, center.X + radius].Intensity;
                    int grayvalue6 = (int)m_image[center.Y - radius, center.X].Intensity;
                    int grayvalue7 = (int)m_image[center.Y + radius, center.X - radius].Intensity;
                    int grayvalue8 = (int)m_image[center.Y + radius, center.X + radius].Intensity;
                    int grayvalue9 = (int)m_image[center.Y + radius, center.X].Intensity;
                    if (grayvalue1 == 0 || grayvalue2 == 0 || grayvalue3 == 0 || grayvalue4 == 0 || grayvalue5 == 0 || grayvalue6 == 0 || grayvalue7 == 0 || grayvalue8 == 0 || grayvalue9 == 0)
                    {
                        mask[center] = new Gray(0);
                        m_pDescriptors = m_pDescriptors.RemoveRows(i, i + 1);
                        //CvInvoke.cvSeqRemove(m_pDescriptors.Ptr,i);
                        //CvInvoke.cvSeqRemove(m_pKeyPoints.Ptr,i);
                        N--;
                        i--;

                    }

                    i++;
                }
                m_pKeyPoints.FilterByPixelsMask(mask);
            }
        }

        //metodos public

        public CSurf()
        {
            PrivateInit();
        }
        public CSurf(Image<Gray, byte> image, double hessianThreshold = DEFAUL_HESSIANTHRESHOLD)
        {
            PrivateInit(hessianThreshold);
            InitFromImg(image, hessianThreshold);
        }
        public CSurf(string filename, int imagewidth, int imageheight)
        {
            PrivateInit();
            SetCorners(imagewidth, imageheight);
            m_bIsInitiated = Load(filename);
            object locker = new object();
        }
        public CSurf(string filename)  {
            PrivateInit();
            m_image = new Image<Gray, byte>(filename);
            InitFromImg(m_image);
        }
        public void InitFromImg(Image<Gray, byte> image, double hessianThreshold = DEFAUL_HESSIANTHRESHOLD)
        {

            SetCorners(image.Width, image.Height);
            m_image = image.Clone();
            m_pDescriptors.Dispose();
            m_pDescriptors = surfCPU.DetectAndCompute(image, null, m_pKeyPoints);
            if (m_config.removeBlackPoints)
                RemoveBadKeyPoints();
            m_bIsInitiated = true;

        }
        public void SetCorners(int width, int height)
        {

            m_src_corners[0].X = 0;
            m_src_corners[0].Y = 0;
            m_src_corners[1].X = width;
            m_src_corners[1].Y = 0;
            m_src_corners[2].X = width;
            m_src_corners[2].Y = height;
            m_src_corners[3].X = 0;
            m_src_corners[3].Y = height;
        }
        public bool Save(string filename)
        {

            if (!SaveDescriptors2(filename))
                return false;
            if (!SaveKeyPoints(filename))
                return false;
            //Logging("Fin de escrituta de Key\n");
            return true;
        }
        bool SaveDescriptors(string filename, bool overwrite = true)
        {

            filename = filename+ EXTENSION_DESCRIPTORS;

            if (overwrite == false && File.Exists(filename)) return false;
            //File.Create(filename);//Create the file.

            try
            {
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    //write the length of each descriptor
                    sw.WriteLine(m_pDescriptors.Cols.ToString());
                    //write the number of descriptors
                    sw.WriteLine(m_pDescriptors.Rows.ToString());
                    //write the Height of the image
                    sw.WriteLine(m_src_corners[2].Y.ToString());
                    //write the width of the image
                    sw.WriteLine(m_src_corners[2].X.ToString());
                    // write to file the descriptors
                    string tofile = "";
                    for (int i = 0; i < m_pDescriptors.Rows; i++)
                    {
                        for (int j = 0; j < m_pDescriptors.Cols; j++)
                        {
                            tofile = tofile + m_pDescriptors[i, j].ToString() + ":";

                        }
                        sw.WriteLine(tofile);
                        tofile = "";
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
               
            }

        }
        bool SaveKeyPoints(string filename, bool overwrite = true)
        {

            filename = filename  + EXTENSION_KEYPOINTS;
            if (overwrite == false && File.Exists(filename)) return false;

            try
            {
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    //write the number of descriptors
                    sw.WriteLine(m_pKeyPoints.Size.ToString());
                    //write the Height of the image
                    sw.WriteLine(m_src_corners[2].Y.ToString());
                    //write the width of the image
                    sw.WriteLine(m_src_corners[2].X.ToString());
                    // write to file the descriptors
                    foreach (MKeyPoint pt in m_pKeyPoints.ToArray())
                    {
                        sw.WriteLine(pt.Angle.ToString() + ":" + pt.ClassId.ToString() + ":" + pt.Octave.ToString() + ":" + pt.Point.X.ToString() + ":" + pt.Point.Y.ToString() + ":" + pt.Response.ToString() + ":" + pt.Size.ToString());
                    }
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        bool SaveDescriptors2(string filename, bool overwrite = true)
        {

            filename = filename + EXTENSION_DESCRIPTORS;

            if (overwrite == false && File.Exists(filename)) return false;
            //File.Create(filename);//Create the file.


            System.IO.Stream stream;
            BinaryFormatter bformatter;
            // Open the saved histogram
            try
            {
               
                locker1.WaitOne();
                      // serialize histogram
                      stream = File.Open(filename, FileMode.Create);
                      bformatter = new BinaryFormatter();
                      bformatter.Serialize(stream, m_pDescriptors);
                      stream.Close();
                      locker1.ReleaseMutex();
                 
                return true;
            }

            catch (Exception ex)
            {
                return false;

            }

        }
        bool SaveKeyPoints2(string filename, bool overwrite = true)
        {

            filename = filename + EXTENSION_KEYPOINTS;
            if (overwrite == false && File.Exists(filename)) return false;

            System.IO.Stream stream;
            BinaryFormatter bformatter;
            // Open the saved histogram
            try
            {  
                locker2.WaitOne();
                      // serialize histogram
                      stream = File.Open(filename, FileMode.Create);
                      bformatter = new BinaryFormatter();
                      bformatter.Serialize(stream, m_pKeyPoints);
                      stream.Close();
                      locker2.ReleaseMutex();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Load(string filename)
        {

            if (!LoadDescriptors2(filename))
            {
                //No hay ficheros de descriptores, los volvemos a crear
                m_image = new Image<Gray, byte>(filename);
                if (m_image.Ptr == null)
                    return false;	//No image file found
                InitFromImg(m_image);
                if (!Save(filename))
                    return false;
                return true;
            }
            if (!LoadKeyPoints(filename))
            {
                //No hay ficheros de keys, los volvemos a crear
                m_image = new Image<Gray, byte>(filename);
                if (m_image.Ptr == null)
                    return false;	//No image file found
                InitFromImg(m_image);
                if (!Save(filename))
                    return false;
                return true;
            }

            return true;
        }
        bool LoadDescriptors(string filename)
        {

            filename = filename + EXTENSION_DESCRIPTORS;
            try
            {
                using (StreamReader sw = new StreamReader(filename))
                {
                    //read the length of each descriptor
                    int cols = int.Parse(sw.ReadLine());
                    //read  the number of descriptors
                    int rows = int.Parse(sw.ReadLine());
                    //read the Height of the image
                    int height = int.Parse(sw.ReadLine());
                    //read the width of the image
                    int width = int.Parse(sw.ReadLine());

                    SetCorners(width, height);
                    // read the descriptors



                    float[,] tabla = new float[rows, cols];
                    string[] data = new string[cols];
                    string linea;
                    int i = 0;

                    while ((linea = sw.ReadLine()) != null)
                    {

                        data = linea.Split(':'); //suponiendo que están separados por comas
                        for (int j = 0; j < cols; j++)
                        {
                            tabla[i, j] = float.Parse(data[j]);
                        }
                        i++;
                        if (i >= rows) break;

                    }
                    m_pDescriptors.Dispose();
                    m_pDescriptors = new Matrix<float>(tabla);


                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        bool LoadKeyPoints(string filename)
        {
            filename = filename + EXTENSION_KEYPOINTS;
            try
            {
                using (StreamReader sw = new StreamReader(filename))
                {
                    //read the number of descriptors
                    int N = int.Parse(sw.ReadLine());
                    //read the Height of the image
                    int height = int.Parse(sw.ReadLine());
                    //read the width of the image
                    int width = int.Parse(sw.ReadLine());

                    SetCorners(width, height);

                    MKeyPoint[] ptos = new MKeyPoint[N];
                    string[] data = new string[7];
                    string linea;
                    int i = 0;
                    // write to file the descriptors

                    while ((linea = sw.ReadLine()) != null)
                    {

                        data = linea.Split(':'); //suponiendo que están separados por comas
                        ptos[i].Angle = float.Parse(data[0]);
                        ptos[i].ClassId = int.Parse(data[1]);
                        ptos[i].Octave = int.Parse(data[2]);
                        ptos[i].Point.X = float.Parse(data[3]);
                        ptos[i].Point.Y = float.Parse(data[4]);
                        ptos[i].Response = float.Parse(data[5]);
                        ptos[i].Size = float.Parse(data[6]);
                        i++;
                        if (i >= N) break;

                    }
                    m_pKeyPoints.Clear();
                    m_pKeyPoints.Push(ptos);

                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        bool LoadDescriptors2(string filename)
        {

            filename = filename +  EXTENSION_DESCRIPTORS;
            System.IO.Stream stream;
            BinaryFormatter bformatter;
           
            // Open the saved histogram
            try
            {
                if (File.Exists(filename))
                {  
                    locker3.WaitOne();
                        stream = File.Open(filename, FileMode.Open);
                        bformatter = new BinaryFormatter();
                        m_pDescriptors = (Matrix<float>)bformatter.Deserialize(stream);
                        stream.Close(); 
                 locker3.ReleaseMutex();
                  
                }
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
          
        }
        bool LoadKeyPoints2(string filename)
        {
            filename = filename +  EXTENSION_KEYPOINTS;
            System.IO.Stream stream;
            BinaryFormatter bformatter;
            try
            {
                if (File.Exists(filename))
                {
                     locker4.WaitOne();
                        stream = File.Open(filename, FileMode.Open);
                        bformatter = new BinaryFormatter();
                        m_pKeyPoints = (VectorOfKeyPoint)bformatter.Deserialize(stream);
                        stream.Close();
                        locker4.ReleaseMutex();
                    return true;
                }
                else return false;

            }
            catch (Exception)
            {

                return false;
            }

        }
        public bool OnlyLoadImage(string filename)
        {

            m_image = new Image<Gray, byte>(filename);

            if (m_image.Ptr != null)
            {
                if (m_pKeyPoints.Ptr != null && m_config.ignoreBlackPoints)
                    RemoveBadKeyPoints();
                return true;
            }
            else return false;
        }
        public int Matching(CSurf Image, ref Point[] dst_corners, out double MatchingQuality, out resultMatching pResult, int modo = MODE_CESAR)
        {
         
            pResult = new resultMatching();            
            m_resultMatching.Init();

            if (m_pDescriptors.Rows == 0 || Image.m_pDescriptors.Rows == 0)
            {
                //Nothing to do, we must return with error
                MatchingQuality = 0.0;
                for (int k = 0; k < 4; k++)
                {
                    dst_corners[k].X = -1;
                    dst_corners[k].Y = -1;
                }

                return -1;
            }

            //  FlannFindPairs(Image);
            FlannFindPairs(Image);

            if (LocatePlanarObject(Image, ref dst_corners,modo))
            {
                int tmpcuenta = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (IsInside(Image.m_src_corners, dst_corners[i]))
                        tmpcuenta++;

                }
                if (tmpcuenta != 4)
                {
                    m_resultMatching.bdst_corners_inside = false;
                    m_resultMatching.bHomographyOK = true;
                }
                else
                {

                    m_resultMatching.bdst_corners_inside = true;
                    m_resultMatching.bHomographyOK = true;
                }
            }
            else
            {

                m_resultMatching.bHomographyOK = false;
                for (int k = 0; k < 4; k++)
                {
                    dst_corners[k].X = -1;
                    dst_corners[k].Y = -1;
                }
            }
            MatchingQuality = CalcularCoeficientes(Image, dst_corners);
            pResult = m_resultMatching;
            return 1;

        }
        private double CalcularCoeficientes(CSurf Image, Point[] dst_corners)
        {
            double MatchingQuality;
            int cuenta = 0;

            int max_x = -1;
            int max_y = -1;
            int min_x = 100000;
            int min_y = 100000;
            Point center_object = new Point(), center_image = new Point();
            Point image_homagraphy_transform;

            for (int i = 0; i < (int)m_pPairs.Count; i += 2)
            {

                if (m_pPairs[i]<m_pKeyPoints.Size&&m_pPairs[i+1]<m_pKeyPoints.Size)
                {
                    MKeyPoint r1 = m_pKeyPoints[m_pPairs[i]];
                    MKeyPoint r2 = m_pKeyPoints[m_pPairs[i + 1]];


                    if (IsInside(dst_corners, new Point((int)r2.Point.X, (int)r2.Point.Y)))
                    {
                        m_pPairsInside.Add((m_pPairs[i]));
                        m_pPairsInside.Add((m_pPairs[i + 1]));
                        cuenta++;
                    }

                    center_object.X = (int)Math.Round(r1.Point.X);
                    center_object.Y = (int)Math.Round(r1.Point.Y);
                    center_image.X = (int)Math.Round(r2.Point.X);
                    center_image.Y = (int)Math.Round(r2.Point.Y);
                    image_homagraphy_transform = AplyHomography(center_object.X, center_object.Y);

                    if (Math.Abs(center_image.X - image_homagraphy_transform.X) <= MARGEN_PIXELS && Math.Abs(center_image.Y - image_homagraphy_transform.Y) <= MARGEN_PIXELS)
                    {
                        //keypoint contribute to the correct homograpy
                        m_pPairsBelongHomography.Add(m_pPairs[i]);
                        m_pPairsBelongHomography.Add(m_pPairs[i + 1]);
                        if ((int)r1.Point.X > max_x)
                            max_x = (int)r1.Point.X;
                        if ((int)r1.Point.Y > max_y)
                            max_y = (int)r1.Point.Y;
                        if ((int)r1.Point.X < min_x)
                            min_x = (int)r1.Point.Y;
                        if ((int)r1.Point.Y < min_y)
                            min_y = (int)r1.Point.Y;
                    } 
                }

            }

            double x = min_x;
            double y = min_y;
            m_resultMatching.dst_corners_local[0] = AplyHomography(x, y);

            x = max_x; y = min_y;
            m_resultMatching.dst_corners_local[1] = AplyHomography(x, y);

            x = max_x; y = max_y;
            m_resultMatching.dst_corners_local[2] = AplyHomography(x, y);

            x = min_x; y = max_y;
            m_resultMatching.dst_corners_local[3] = AplyHomography(x, y);


            int cuentaInsideHomography = 0;
            for (int i = 0; i < (int)m_pPairsBelongHomography.Count; i += 2)
            {
                MKeyPoint r1 = m_pKeyPoints[m_pPairs[i]];
                MKeyPoint r2 = m_pKeyPoints[m_pPairs[i + 1]];
                if (IsInside(m_resultMatching.dst_corners_local, new Point((int)r2.Point.X, (int)r2.Point.Y)))
                {
                    cuentaInsideHomography++;
                }

            }


            m_resultMatching.npoints_Homography = m_pPairsBelongHomography.Count / 2;
            m_resultMatching.npoints_Homography_inside = cuentaInsideHomography;
            m_resultMatching.npoints_included_homography = m_point_included_homography;
            int tmpcuenta2 = 0;
            for (int i = 0; i < 4; i++)
            {
                if (IsInside(Image.m_src_corners, m_resultMatching.dst_corners_local[i]))
                    tmpcuenta2++;
                //Logging("esquina %d: x=%d y=%d\n",i,dst_corners[i].x,dst_corners[i].y);
            }
            if (tmpcuenta2 != 4)
            {

                m_resultMatching.bdst_corners_local_inside = false;
            }
            else
            {

                m_resultMatching.bdst_corners_local_inside = true;
            }


            if (m_pPairs.Count != 0)
                MatchingQuality = ((float)(cuenta * 2.0) / (float)m_pPairs.Count())  * 100;
            else
                MatchingQuality = 0.0;
          
            m_resultMatching.common_KeyPoints = (int)m_pPairs.Count / 2;
            m_resultMatching.inside_KeyPoints = cuenta;
            m_resultMatching.quality = (float)MatchingQuality;
            m_resultMatching.total_KeyPoints = (int)m_pDescriptors.Rows;
            for (int k = 0; k < 4; k++)
            {
                m_resultMatching.dst_corners[k].X = dst_corners[k].X;
                m_resultMatching.dst_corners[k].Y = dst_corners[k].Y;
            }
            m_resultMatching.total_other_keyPoints = Image.m_pDescriptors.Rows;
            return MatchingQuality;
        }
        int FlannFindPairs(CSurf Image)
        {
            int length = (int)(m_pDescriptors.Cols);
            Matrix<float> flann_object = new Matrix<float>(m_pDescriptors.Rows, m_pDescriptors.Cols);
            Matrix<float> flann_image = new Matrix<float>(Image.m_pDescriptors.Rows, m_pDescriptors.Cols);
            // copy descriptors
            flann_object.Data = m_pDescriptors.Data;
            flann_image.Data = Image.m_pDescriptors.Data;

            Matrix<int> flann_indices = new Matrix<int>(m_pDescriptors.Rows, 2);
            Matrix<float> flann_dists = new Matrix<float>(m_pDescriptors.Rows, 2);
            Emgu.CV.Flann.Index flann_index = new Index(flann_image, 4); // using 4 randomized kdtrees
            flann_index.KnnSearch(flann_object, flann_indices, flann_dists, 2, 64);

            //We have to remove the m_pPairs because it remember previous call- ¿BUG?
            m_pPairs.Clear();
            m_pPairsInside.Clear();
            m_pPairsBelongHomography.Clear();
            m_pDist.Clear();
            int count = 0;
            for (int i = 0; i < flann_indices.Rows; ++i)
            {
                if (flann_dists[i, 0] < 0.6 * flann_dists[i, 1])
                { //theory says this is the correct, the more the less exahustive the comparation
                    //if (dists_ptr[2*i]<0.5*dists_ptr[2*i+1] ) {
                    //if (dists_ptr[2*i]<0.7*dists_ptr[2*i+1] ) {
                    //if (dists_ptr[2*i]<0.55*dists_ptr[2*i+1] ) {
                    //if (dists_ptr[2*i]<0.65*dists_ptr[2*i+1] ) {
                    //if (dists_ptr[2*i]<0.1 ) {
                    //Logging("%d\n",indices_ptr[2*i]);
                    if (flann_indices[i, 0] > Image.m_pDescriptors.Rows)
                        ;
                    else
                    {
                        if (flann_indices[i, 0]<m_pKeyPoints.Size&&i<m_pKeyPoints.Size)
                        {
                            m_pPairs.Add(i);
                            m_pPairs.Add(flann_indices[i, 0]);
                            m_pDist.Add(flann_dists[i, 0]);
                            // m_pDist.Add(flann_dists[i, 0]);  ///BUG?????????????no seria[ i,1]???                        
                            count++; 
                        }
                    }
                }
            }

            flann_object.Dispose();
            flann_image.Dispose();
            flann_indices.Dispose();
            flann_dists.Dispose();

            return count;


        }    
        bool LocatePlanarObject(CSurf Image, ref Point[] dst_corners, int modo = MODE_CESAR)
        {
            int i, n;
            n = m_pPairs.Count / 2;
            if (n < 4) return false;            
           
           
            Matrix<byte> mask = new Matrix<byte>(n, 1);
            mask.SetValue(255);

            if (modo == MODE_CESAR)
            {

                Matrix<float> pt1, pt2;
                pt1 = new Matrix<float>(n, 2);
                pt2 = new Matrix<float>(n, 2);


                for (i = 0; i < n; i++)
                {
                    pt1[i, 0] = m_pKeyPoints.ToArray()[m_pPairs[i * 2]].Point.X;
                    pt1[i, 1] = m_pKeyPoints.ToArray()[m_pPairs[i * 2]].Point.Y;
                    pt2[i, 0] = Image.m_pKeyPoints.ToArray()[m_pPairs[i * 2 + 1]].Point.X;
                    pt2[i, 1] = Image.m_pKeyPoints.ToArray()[m_pPairs[i * 2 + 1]].Point.Y;
                }

                if (!CvInvoke.cvFindHomography(pt1.Ptr, pt2.Ptr, m_MatrixHomographyMat.Ptr, HOMOGRAPHY_METHOD.RANSAC, MARGEN_PIXELS, mask.Ptr))
                    return false;
                int nonZeroCount = CvInvoke.cvCountNonZero(mask);
                m_point_included_homography = nonZeroCount;

                if (nonZeroCount < 5)
                    return false;
                for (i = 0; i < 4; i++)
                {
                    double x = m_src_corners[i].X, y = m_src_corners[i].Y;
                    dst_corners[i] = AplyHomography(x, y);
                }
                pt1.Dispose();
                pt2.Dispose();
            }

            if (modo == MODE_SURF)
            {
                Matrix<int> indices;
                indices = new Matrix<int>(n, 2);
                MKeyPoint[] ptos1 = new MKeyPoint[n];
                MKeyPoint[] ptos2 = new MKeyPoint[n];

                for (i = 0; i < n; i++)
                {

                    ptos1[i] = m_pKeyPoints.ToArray()[m_pPairs[i * 2]];
                    ptos2[i] = Image.m_pKeyPoints.ToArray()[m_pPairs[i * 2 + 1]];
                    indices[i, 0] = i;
                    indices[i, 1] = i;
                }
                VectorOfKeyPoint modelKeyPoints = new VectorOfKeyPoint();
                modelKeyPoints.Push(ptos1);
                VectorOfKeyPoint observedKeyPoints = new VectorOfKeyPoint();
                observedKeyPoints.Push(ptos2);

                m_MatrixHomographyMat = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints, observedKeyPoints, indices, mask, 10);
                modelKeyPoints.Dispose();
                observedKeyPoints.Dispose();
                indices.Dispose();
               
                int nonZeroCount = CvInvoke.cvCountNonZero(mask);
                if (nonZeroCount < 5)
                    return false;

                for (i = 0; i < 4; i++)
                {
                    double x = m_src_corners[i].X, y = m_src_corners[i].Y;
                    dst_corners[i] = AplyHomography(x, y);
                }
            }
            mask.Dispose();
            return true;
        }
        Point AplyHomography(double x, double y)
        {
            double Z = 1 / (m_MatrixHomographyMat[2, 0] * x + m_MatrixHomographyMat[2, 1] * y + m_MatrixHomographyMat[2, 2]);
            double X = (m_MatrixHomographyMat[0, 0] * x + m_MatrixHomographyMat[0, 1] * y + m_MatrixHomographyMat[0, 2]) * Z;
            double Y = (m_MatrixHomographyMat[1, 0] * x + m_MatrixHomographyMat[1, 1] * y + m_MatrixHomographyMat[1, 2]) * Z;
            return new Point((int)Math.Round(X), (int)Math.Round(Y));
        }
        bool IsInside(Point[] dst_corners, Point point)
        {
            _Point[] Polygon = new _Point[4];
            for (int k = 0; k < 4; k++)
            {
                Polygon[k] = new _Point(dst_corners[k].X, dst_corners[k].Y);
                //Polygon[k].x=dst_corners[k].X;
                //Polygon[k].y=dst_corners[k].Y;
            }
            _Point _point = new _Point();
            _point.x = point.X;
            _point.y = point.Y;

            InsidePoly ipoly = new InsidePoly();
            if (ipoly.InsidePolygon(Polygon, 4, _point, InsidePoly.INSIDE) == InsidePoly.INSIDE) return true;
            else return false;
        }
        public CSurf(CSurf otro)
    {
     
    
    }

                 #region dispose

          // Indica si ya se llamo al método Dispose. (default = false)
          private Boolean disposed;

          /// <summary>
          /// Implementación de IDisposable. No se sobreescribe.
          /// </summary>
          public void Dispose()
          {
              this.Dispose(true);
              // GC.SupressFinalize quita de la cola de finalización al objeto.
              GC.SuppressFinalize(this);
          }

          /// <summary>
          /// Limpia los recursos manejados y no manejados.
          /// </summary>
          /// <param name="disposing">
          /// Si es true, el método es llamado directamente o indirectamente
          /// desde el código del usuario.
          /// Si es false, el método es llamado por el finalizador
          /// y sólo los recursos no manejados son finalizados.
          /// </param>
          protected virtual void Dispose(bool disposing)
          {
              // Preguntamos si Dispose ya fue llamado.
              if (!this.disposed)
              {
                  if (disposing)
                  {
                      // Llamamos al Dispose de todos los RECURSOS MANEJADOS.
                      this.Clean();

                  }

                  // Acá finalizamos correctamente los RECURSOS NO MANEJADOS
                  // ...

              }
              this.disposed = true;
          }

          /// <summary>
          /// Destructor de la instancia
          /// </summary>
          ~CSurf()
          {
              this.Dispose(false);
          }

          #endregion    

       

    }
}

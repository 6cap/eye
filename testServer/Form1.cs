using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Numpy;
using OpenCvSharp;
using DlibDotNet;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Extensions;
using Dlib = DlibDotNet.Dlib;


namespace testServer
{
    public partial class Form1 : Form
    {
        object LEFT_EYE_POINTS = new List<object> {
                36,
                37,
                38,
                39,
                40,
                41
            };

        object RIGHT_EYE_POINTS = new List<object> {
                42,
                43,
                44,
                45,
                46,
                47
            };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

         }

        public void init(int original_frame,  int landmarks, int side, int calibration)
        {
            var frame = 0;    //  fram , 등  어디 서 받아 봐야하는듯 
            var origin = 0;
            var center = 0;
            var pupil = 0;
            var landmark_points = 0;
            this._analyze(original_frame, landmarks, side, calibration);
        }


           public void _middle_point(int p1 , int p2)
            {   
            int x = (p1.x + p2.x) / 2;
            int y = (p1.y + p2.y) / 2;
            return x.y;
           
            }

            public  void _isolate(int frame,int landmarks, int points)
            {

            foreach (int point in points)
            {
                int region = np.array(landmarks.part(point).x, landmarks.part(point).y);
            }

             region  = new region.Getype(np.int32);
             this.landmark_points = region;

             int height ,width = frame.shape[::2];
            
            int black_frame = np.zeros((height, width), np.uint8);
            int  mask = np.full((height, width), 255, np.uint8);
            cv2.fillPoly(mask, new List<object> {
                    region
                }, (0, 0, 0));
            var eye = cv2.bitwise_not(black_frame, frame.copy(), mask: mask);
            // Cropping on the eye
            int margin = 5;
            int min_x = np.min(region[":", 0]) - margin;
            int max_x = np.max(region[":", 0]) + margin;
            int min_y = np.min(region[":", 1]) - margin;
            int  max_y = np.max(region[":", 1]) + margin;
            this.frame = eye[min_y::max_y, min_x::max_x];
            this.origin = (min_x, min_y);
            var tuple2 = this.frame.shape[::2];
            height = tuple2.Item1;
            width = tuple2.Item2;
            this.center = (width / 2, height / 2);
        }
          

            public void _blinking_ratio(int landmarks, int points)
            {
                
               int  x= ratio;
                int left = (landmarks.part(points[0]).x, landmarks.part(points[0]).y);
                int right = (landmarks.part(points[3]).x, landmarks.part(points[3]).y);
                int top = this._middle_point(landmarks.part(points[1]), landmarks.part(points[2]));
                int bottom = this._middle_point(landmarks.part(points[5]), landmarks.part(points[4]));
                int eye_width = math.hypot(left[0] - right[0], left[1] - right[1]);
                int  eye_height = math.hypot(top[0] - bottom[0], top[1] - bottom[1]);
                try
                {
                    x = eye_width / eye_height;
                }
                catch (Exception)
                {
                  x= 0;
                }
                return x;
            }

            public void _analyze(int original_frame, int  landmarks, int side, int calibration)
            {
            int x = points;
                if (side == 0)
                {
                    x = this.LEFT_EYE_POINTS;
                }
                else if (side == 1)
                {
                     x = this.RIGHT_EYE_POINTS;
                }
                else
                {
                    return;
                }
                this.blinking = this._blinking_ratio(landmarks, points);
                this._isolate(original_frame, landmarks, points);
                if (!calibration.is_complete())
                {
                    calibration.evaluate(this.frame, side);
                }
               int threshold = calibration.threshold(side);
                this.pupil = Pupil(this.frame, threshold);
            }
        }






    }
        

           

    }

        




    }

  }



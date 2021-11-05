using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace colorCode
{
   public class UtilityClass
    { 

        private double C;
        private double H;
        private double X;
        private double modeH;
        private double R1;
        private double G1;
        private double B1;

        private double FindC(double Value, double Saturation)
        {
            return C = Value / 100 * Saturation / 100;
        }

        private double FindH(double Hue)
        {
            return H = Hue / 60;
        }
        private double FindModeH(double H)
        {
            double modeh= H % 2;
            modeH = modeh - 1;
            if (modeH<0)
            {
                return modeH = modeH * -1;
            }
            else
            {
                return modeH;
            }
        }
        private double FindX(double C, double modeh)
        {
            return X = C * (1 - modeh);
        }
        private double FindM(double V, double C)
        {
            double m = V / 100 - C;
            return m;
        }

        private double FindR1(double H, double C, double X )
        {
            if (0<= H && H<= 1)
            {
                R1 = C;
            }
            else if (1 < H && H <= 2)
            {
                R1 = X;
            }
            else if (2 < H && H <= 3)
            {
                R1 = 0;
            }
            else if (3 < H && H <= 4)
            {
                R1 = 0;
            }
            else if (4 < H && H <= 5)
            {
                R1 = X;
            }
            else if (5 < H && H <= 6)
            {
                R1 = C;
            }
            return R1;
        }
        private double FindG1(double H, double C, double X)
        {
            if (0 <= H && H <= 1)
            {
                G1 = X;
            }
            else if (1 < H && H <= 2)
            {
                G1 =C;
            }
            else if (2 < H && H <= 3)
            {
                G1 = C;
            }
            else if (3 < H && H <= 4)
            {
                G1 = X;
            }
            else if (4 < H && H <= 5)
            {
                G1 = 0;
            }
            else if (5 < H && H <= 6)
            {
                G1 = 0;
            }
            return G1;
        }
        private double FindB1(double H, double C, double X)
        {
            if (0 <= H && H <= 1)
            {
                B1 = 0;
            }
            else if (1 < H && H <= 2)
            {
                B1 = 0;
            }
            else if (2 < H && H <= 3)
            {
                B1 = X;
            }
            else if (3 < H && H <= 4)
            {
                B1 = C;
            }
            else if (4 < H && H <= 5)
            {
                B1 = C;
            }
            else if (5 < H && H <= 6)
            {
                B1 = X;
            }
            return B1;
        }
        private byte FindR(double R1, double M)
        {
           double R = (R1 + M) * 255;
            return (byte)Math.Round(R);
        }
        private byte FindG(double G1, double M)
        {
            double G = (G1 + M) * 255;
            return (byte)Math.Round(G);
        }
        private byte FindB(double B1, double M)
        { 
            double B = (B1 + M) * 255;
            return (byte)Math.Round(B);
        }
        public byte[] HSVToRGB(double H, double S, double V)
        {
            double Hue = FindH(H);
            double C = FindC(V, S);
            double modeH = FindModeH(Hue);
            double X = FindX(C, modeH);
            double M = FindM(V, C);
            double R1 = FindR1(Hue, C, X);
            double G1 = FindG1(Hue, C, X);
            double B1 = FindB1(Hue, C, X);
            byte[] RGB = new byte[3];
            RGB[0] = FindR(R1, M);
            RGB[1] = FindG(G1, M);
            RGB[2] = FindB(B1, M);
            return RGB;
        }
        private double FindR2(double R)
        {
            double R2 = R / 255;
            return R2;
        }
        private double FindG2(double G)
        {
            double G2 = G / 255;
            return G2;
        }
        private double FindB2(double B)
        {
            double B2 = B / 255;
            return B2;
        }

        private double FindMax(double R, double G, double B)
        {
            double Max = Math.Max(Math.Max(R, G), B);
            return Max;
        }
        private double FindMin(double R, double G, double B)
        {
            double Min = Math.Min(Math.Min(R, G), B);
            return Min;
        }
        private double FindDifference(double Max, double Min)
        {
            double D = Max - Min;
            return D;
        }
        private double FindHue(double R2, double G2, double B2, double Max, double D)
        {
            double Hue = 0;
            if (D==0)
            {
                return Hue = 0;
            }
            else if (Max==R2)
            {
                Hue = 60 * (((G2 - B2) / D) % 6);
            }
            else if (Max ==G2)
            {
                Hue = 60 * (((B2 - R2) / D) + 2);
            }
            else
            {
                Hue = 60 * (((R2 - G2) / D) + 4);
            }
            return Hue;
        }
        private double FindSat(double Max, double D)
        {
            double Sat = 0;
            if (Max == 0)
            {
                Sat = 0;
            }
            else
            {
                Sat = D / Max;
            }
            return Sat;
        }
        public double FindHu(double R, double G, double B)
        {
            double R2 = FindR2(R);
            double G2 = FindG2(G);
            double B2 = FindB2(B);
            double Max = FindMax(R2, G2, B2);
            double Min = FindMin(R2, G2, B2);
            double Diff = FindDifference(Max, Min);
            double Hue = FindHue(R2, G2, B2, Max, Diff);
            return Hue;
        }
        public double FindSaturation(double R, double G, double B)
        {
            double R2 = FindR2(R);
            double G2 = FindG2(G);
            double B2 = FindB2(B);
            double Max = FindMax(R2, G2, B2);
            double Min = FindMin(R2, G2, B2);
            double Diff = FindDifference(Max, Min);
            double Sat = FindSat(Max, Diff) *100;
            return Sat;
        }
        public double FindVal(double R, double G, double B)
        {
            double R2 = FindR2(R);
            double G2 = FindG2(G);
            double B2 = FindB2(B);
            double Max = FindMax(R2, G2, B2)*100;
            return Max;
        }
        public Color GetColor(GradientStopCollection gsc, double offset)
        {
            GradientStop[] stops = gsc.OrderBy(x => x.Offset).ToArray();
            if (offset <= 0)
            {
                return stops[0].Color;
            }
            if (offset >= 1)
            {
                return stops[stops.Length - 1].Color;
            }
            GradientStop left = stops[0], right = null;
            foreach (var stop in stops)
            {
                if (stop.Offset >= offset)
                {
                    right = stop;
                    break;
                }
                left = stop;
            }
            if (right != null)
            {
                offset = Math.Round((offset - left.Offset) / (right.Offset - left.Offset), 2);
            }
            else
            {
                right = left;
                offset = Math.Round(left.Offset, 2);
            }
            byte a = (byte)((right.Color.A - left.Color.A) * offset + left.Color.A);
            byte r = (byte)((right.Color.R - left.Color.R) * offset + left.Color.R);
            byte g = (byte)((right.Color.G - left.Color.G) * offset + left.Color.G);
            byte b = (byte)((right.Color.B - left.Color.B) * offset + left.Color.B);
            return Color.FromArgb(a, r, g, b);
        }
    }
}

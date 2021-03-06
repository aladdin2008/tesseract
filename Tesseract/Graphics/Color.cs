using System;
using System.ComponentModel;

namespace Tesseract.Graphics
{
	[TypeConverter(typeof(ColorConverter))]
	public class Color
	{
		public Color(double A, double R, double G, double B)
		{
			this.a = A;
			this.r = R;
			this.g = G;
			this.b = B;
		}
		
		double a;
		public double A
		{
			get { return a; }
			set { a = value; }
		}
		
		double r;
		public double R
		{
			get { return r; }
			set { r = value; }
		}
		
		double g;
		public double G
		{
			get { return g; }
			set { g = value; }
		}
		
		double b;
		public double B
		{
			get { return b; }
			set { b = value; }
		}
		
		public byte AByte
		{
			get { return (byte)(a * 255); }
		}
		
		public byte RByte
		{
			get { return (byte)(r * 255); }
		}
		
		public byte GByte
		{
			get { return (byte)(g * 255); }
		}
		
		public byte BByte
		{
			get { return (byte)(b * 255); }
		}
		
		public override string ToString()
   		{
   			return string.Format("Color[{0}:{1},{2},{3}]", a, r, g, b);
   		}

        public static Color FromBytes(byte A, byte R, byte G, byte B)
        {
            return new Color((double)A / 255, (double)R / 255, (double)G / 255, (double)B / 255);
        }

        public static Color FromBytes(byte R, byte G, byte B)
        {
            return FromBytes(255, R, G, B);
        }
		
		public static Color FromString(string s)
   		{
   			if (typeof(Colors).GetProperty(s) != null)
   				return (Color)typeof(Colors).GetProperty(s).GetValue(null, null);

            if (s.StartsWith("Color["))
                s = s.Substring(6);
            if (s.EndsWith("]"))
                s = s.Substring(0, s.Length - 1);

            string[] arr = s.Split(new char[] { ':', ',' });

            double A = 1;
            double R = 0;
            double G = 0;
            double B = 0;

            if (arr.Length == 4)
            {
                A = double.Parse(arr[0]);
                
                string[] newarr = new string[3];
                newarr[0] = arr[1];
                newarr[1] = arr[2];
                newarr[2] = arr[3];

                arr = newarr;
            }

            if (arr.Length == 3)
            {
                R = double.Parse(arr[0]);
                G = double.Parse(arr[1]);
                B = double.Parse(arr[2]);

                return new Color(A, R, G, B);
            }

   			return Colors.White;
   		}
	}
	
	class ColorConverter: TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext c, Type t)
		{
			return (t == typeof(string));
		}
		
		public override object ConvertFrom(ITypeDescriptorContext c, System.Globalization.CultureInfo i, object o)
		{
			if (o is string)
				return Color.FromString((string)o);
			
			return Colors.Black;
		}
	}
}

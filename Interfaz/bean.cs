using System;
using System.Collections.Generic;
using System.Windows.Media;

public class Bean
{
    String nombre;
    int tipo;
    float parA, parB, parC, exp;

    public string Nombre { get => nombre; set => nombre = value; }
    public int Tipo { get => tipo; set => tipo = value; }
    public float ParA { get => parA; set => parA = value; }
    public float ParB { get => parB; set => parB = value; }
    public float ParC { get => parC; set => parC = value; }
    public float Exp { get => exp; set => exp = value; }
    public Color Color { get => color; set => color = value; }

    private Color color;

    public Bean()
	{
            
	}

    public Bean(string nombre, int tipo, float parA, 
                float parB, Color color)
    {
        this.Nombre = nombre;
        this.Tipo = tipo;
        this.ParA = parA;
        this.ParB = parB;
        this.color = color;
    }

    public Bean(string nombre, int tipo, float parA,
                Color color, float exp)
    {
        this.Nombre = nombre;
        this.Tipo = tipo;
        this.ParA = parA;
        this.Exp = exp;
        this.color = color;
    }

    public Bean(string nombre, int tipo, float parA,
                float parB, float parC, Color color)
    {
        this.Nombre = nombre;
        this.Tipo = tipo;
        this.ParA = parA;
        this.ParB = parB;
        this.ParC = parC;
        this.Exp = 2;
        this.color = color;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Enums
{
    public enum WeightUnit
    {
        mg,    // Milligram
        cg,    // Centigram
        dg,    // Decigram
        g,     // Gram
        dag,   // Dekagram
        hg,    // Hectogram
        kg,    // Kilogram
        t      // Metric Ton
    }

    public enum LengthUnit
    {
        mm,    // Millimeter
        cm,    // Centimeter
        dm,    // Decimeter
        m,     // Meter
        dam,   // Dekameter
        hm,    // Hectometer
        km     // Kilometer
    }

    public enum VolumeUnit
    {
        mm3,   // Cubic millimeter
        cm3,   // Cubic centimeter
        dm3,   // Cubic decimeter (also 1 liter)
        m3,    // Cubic meter
        dam3,  // Cubic dekameter
        hm3,   // Cubic hectometer
        km3,   // Cubic kilometer
        mL,    // Milliliter (also 1 cubic centimeter)
        cL,    // Centiliter
        dL,    // Deciliter
        L,     // Liter
        daL,   // Dekaliter
        hL,    // Hectoliter
        kL     // Kiloliter
    }

    public enum AreaUnit
    {
        mm2,   // Square millimeter
        cm2,   // Square centimeter
        dm2,   // Square decimeter
        m2,    // Square meter
        dam2,  // Square dekameter
        hm2,   // Square hectometer
        km2    // Square kilometer
    }
}

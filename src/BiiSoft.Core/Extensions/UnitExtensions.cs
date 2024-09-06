using BiiSoft.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Extensions
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

    public static class UnitExtensions
    {
        // WeightUnit Conversion Factors to Kilograms
        private static readonly decimal[] weightConversionFactorsToKilograms = {
            1e-6m,  // mg to kg
            1e-5m,  // cg to kg
            1e-4m,  // dg to kg
            1e-3m,  // g to kg
            1e-2m,  // dag to kg
            1e-1m,  // hg to kg
            1,      // kg to kg
            1e-3m   // t to kg
        };

        // LengthUnit Conversion Factors to Meters
        private static readonly decimal[] lengthConversionFactorsToMeters = {
            1e-3m,  // mm to m
            1e-2m,  // cm to m
            1e-1m,  // dm to m
            1,      // m to m
            1e1m,   // dam to m
            1e2m,   // hm to m
            1e3m    // km to m
        };

        // VolumeUnit Conversion Factors to Liters
        private static readonly decimal[] volumeConversionFactorsToLiters = {
            1e-9m,  // mm3 to L
            1e-6m,  // cm3 to L
            1e-3m,  // dm3 to L
            1,      // m3 to L
            1e3m,   // dam3 to L
            1e6m,   // hm3 to L
            1e9m,   // km3 to L
            1e-3m,  // mL to L
            1e-2m,  // cL to L
            1e-1m,  // dL to L
            1,      // L to L
            10m,    // daL to L
            100m,   // hL to L
            1000m   // kL to L
        };

        // AreaUnit Conversion Factors to Square Meters
        private static readonly decimal[] areaConversionFactorsToSquareMeters = {
            1e-6m,  // mm2 to m2
            1e-4m,  // cm2 to m2
            1e-2m,  // dm2 to m2
            1,      // m2 to m2
            1e2m,   // dam2 to m2
            1e4m,   // hm2 to m2
            1e6m    // km2 to m2
        };

        // WeightUnit methods
        public static string Name(this WeightUnit unit)
        {
            return unit switch
            {
                WeightUnit.mg => "Milligram",
                WeightUnit.cg => "Centigram",
                WeightUnit.dg => "Decigram",
                WeightUnit.g => "Gram",
                WeightUnit.dag => "Dekagram",
                WeightUnit.hg => "Hectogram",
                WeightUnit.kg => "Kilogram",
                WeightUnit.t => "Metric Ton",
                _ => "Unknown Unit"
            };
        }

        public static string Symbol(this WeightUnit unit)
        {
            return unit switch
            {
                WeightUnit.mg => "mg",
                WeightUnit.cg => "cg",
                WeightUnit.dg => "dg",
                WeightUnit.g => "g",
                WeightUnit.dag => "dag",
                WeightUnit.hg => "hg",
                WeightUnit.kg => "kg",
                WeightUnit.t => "t",
                _ => "Unknown"
            };
        }

        public static decimal To(this WeightUnit fromUnit, WeightUnit toUnit, decimal value)
        {
            decimal valueInKilograms = value * weightConversionFactorsToKilograms[(int)fromUnit];
            return valueInKilograms / weightConversionFactorsToKilograms[(int)toUnit];
        }

        // LengthUnit methods
        public static string Name(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.mm => "Millimeter",
                LengthUnit.cm => "Centimeter",
                LengthUnit.dm => "Decimeter",
                LengthUnit.m => "Meter",
                LengthUnit.dam => "Dekameter",
                LengthUnit.hm => "Hectometer",
                LengthUnit.km => "Kilometer",
                _ => "Unknown Unit"
            };
        }

        public static string Symbol(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.mm => "mm",
                LengthUnit.cm => "cm",
                LengthUnit.dm => "dm",
                LengthUnit.m => "m",
                LengthUnit.dam => "dam",
                LengthUnit.hm => "hm",
                LengthUnit.km => "km",
                _ => "Unknown"
            };
        }

        public static decimal To(this LengthUnit fromUnit, LengthUnit toUnit, decimal value)
        {
            decimal valueInMeters = value * lengthConversionFactorsToMeters[(int)fromUnit];
            return valueInMeters / lengthConversionFactorsToMeters[(int)toUnit];
        }

        // VolumeUnit methods
        public static string Name(this VolumeUnit unit)
        {
            return unit switch
            {
                VolumeUnit.mm3 => "Cubic Millimeter",
                VolumeUnit.cm3 => "Cubic Centimeter",
                VolumeUnit.dm3 => "Cubic Decimeter",
                VolumeUnit.m3 => "Cubic Meter",
                VolumeUnit.dam3 => "Cubic Dekameter",
                VolumeUnit.hm3 => "Cubic Hectometer",
                VolumeUnit.km3 => "Cubic Kilometer",
                VolumeUnit.mL => "Milliliter",
                VolumeUnit.cL => "Centiliter",
                VolumeUnit.dL => "Deciliter",
                VolumeUnit.L => "Liter",
                VolumeUnit.daL => "Dekaliter",
                VolumeUnit.hL => "Hectoliter",
                VolumeUnit.kL => "Kiloliter",
                _ => "Unknown Unit"
            };
        }

        public static string Symbol(this VolumeUnit unit)
        {
            return unit switch
            {
                VolumeUnit.mm3 => "mm³",
                VolumeUnit.cm3 => "cm³",
                VolumeUnit.dm3 => "dm³",
                VolumeUnit.m3 => "m³",
                VolumeUnit.dam3 => "dam³",
                VolumeUnit.hm3 => "hm³",
                VolumeUnit.km3 => "km³",
                VolumeUnit.mL => "mL",
                VolumeUnit.cL => "cL",
                VolumeUnit.dL => "dL",
                VolumeUnit.L => "L",
                VolumeUnit.daL => "daL",
                VolumeUnit.hL => "hL",
                VolumeUnit.kL => "kL",
                _ => "Unknown"
            };
        }

        public static decimal To(this VolumeUnit fromUnit, VolumeUnit toUnit, decimal value)
        {
            decimal valueInLiters = value * volumeConversionFactorsToLiters[(int)fromUnit];
            return valueInLiters / volumeConversionFactorsToLiters[(int)toUnit];
        }

        // AreaUnit methods
        public static string Name(this AreaUnit unit)
        {
            return unit switch
            {
                AreaUnit.mm2 => "Square Millimeter",
                AreaUnit.cm2 => "Square Centimeter",
                AreaUnit.dm2 => "Square Decimeter",
                AreaUnit.m2 => "Square Meter",
                AreaUnit.dam2 => "Square Dekameter",
                AreaUnit.hm2 => "Square Hectometer",
                AreaUnit.km2 => "Square Kilometer",
                _ => "Unknown Unit"
            };
        }

        public static string Symbol(this AreaUnit unit)
        {
            return unit switch
            {
                AreaUnit.mm2 => "mm²",
                AreaUnit.cm2 => "cm²",
                AreaUnit.dm2 => "dm²",
                AreaUnit.m2 => "m²",
                AreaUnit.dam2 => "dam²",
                AreaUnit.hm2 => "hm²",
                AreaUnit.km2 => "km²",
                _ => "Unknown"
            };
        }

        public static decimal To(this AreaUnit fromUnit, AreaUnit toUnit, decimal value)
        {
            decimal valueInSquareMeters = value * areaConversionFactorsToSquareMeters[(int)fromUnit];
            return valueInSquareMeters / areaConversionFactorsToSquareMeters[(int)toUnit];
        }
    }
}

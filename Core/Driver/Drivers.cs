using System.Drawing;
using Core.SpatialCore;

namespace Core.DriverCore
{
    /// <summary>
    /// Driver class. Includes all driver objects used throughout the project.
    /// </summary>
    public class Drivers : DriverCore
    {
        public Drivers() { }

        public DriverCore tweeter   { get { return _tweeter;   } set { _tweeter   = value; } }
        public DriverCore midrange1 { get { return _midrange1; } set { _midrange1 = value; } }
        public DriverCore midrange2 { get { return _midrange2; } set { _midrange2 = value; } }
        public DriverCore woofer1 { get { return _woofer1; } set { _woofer1 = value; } }
        public DriverCore woofer2 { get { return _woofer2; } set { _woofer2 = value; } }

        private DriverCore _tweeter = new DriverCore()
        {
            type = DriverType.Tweeter,

            tagSPLMag = "tweeterInterpSPLMag",
            tagSPLPhase = "tweeterInterpSPLPhase",
            tagImpMag = "tweeterInterpIMPMag",
            tagImpPhase = "tweeterInterpIMPPhase",

            titleSPLMag = "T SPL",
            titleSPLPhase = "T Phase",
            titleIMPMag = "T Imp",
            titleIMPPhase = "T Phase",

            tagSPLMagRaw = "tweeterSPLMagRaw",
            tagSPLPhaseRaw = "tweeterSPLPhaseRaw",
            tagImpMagRaw = "tweeterIMPMagRaw",
            tagImpPhaseRaw = "tweeterIMPPhaseRaw",

            titleSPLMagRaw = "T SPLRaw",
            titleSPLPhaseRaw = "T PhaseRaw",
            titleIMPMagRaw = "T ImpRaw",
            titleIMPPhaseRaw = "T PhaseRaw",

            tagEqualizedSPLMag = "tweeterEqualizedSPLMag",
            tagEqualizedSPLPhase = "tweeterEqualizedSPLPhase",
            tagEqualizedImpMag = "tweeterEqualizedIMPMag",
            tagEqualizedImpPhase = "tweeterEqualizedIMPPhase",
            titleEqualizedSPLMag = "T SPL EQ",
            titleEqualizedSPLPhase = "T Phase EQ",
            titleEqualizedIMPMag = "T Imp EQ",
            titleEqualizedIMPPhase = "T Phase EQ",

            tagCompensatedImpMag = "tweeterCompensatedIMPMag",
            tagCompensatedImpPhase = "tweeterCompensatedIMPPhase",
            titleCompensatedIMPMag = "Comp Z",
            titleCompensatedIMPPhase = "Comp Phase",

            tagCircuitOnlyImpMag = "tweeterCircuitOnlyIMPMag",
            tagCircuitOnlyImpPhase = "tweeterCircuitOnlyIMPPhase",
            titleCircuitOnlyIMPMag = "Circ Z",
            titleCircuitOnlyIMPPhase = "Circ Phase",

            tagCircuitTransferFunctionMag = "tweeterCircuitTransferFunctionMag",
            tagCircuitTransferFunctionPhase = "tweeterCircuitTransferFunctionPhase",
            titleCircuitTransferFunctionMag = "Filter",
            titleCircuitTransferFunctionPhase = "Filter Phase",
            titleCircuitTransferFunctionMagSys = "Tweeter Filter",

            colorSPLMagRaw = Color.Gray,
            colorSPLPhaseRaw = Color.Gray,
            colorImpMagRaw = Color.Gray,
            colorImpPhaseRaw = Color.Gray,

            colorSPLMag = Color.Blue,
            colorSPLPhase = Color.LightBlue,

            colorImpMag = Color.Blue,
            colorImpPhase = Color.LightBlue,
            colorCompensatedImpMag = Color.DarkGreen,
            colorCompensatedImpPhase = Color.LightGreen,

            colorEqualizedSPLMag = Color.Red,
            colorEqualizedSPLPhase = Color.Pink,

            colorEqualizedImpMag = Color.Red,
            colorEqualizedImpPhase = Color.Pink,
            colorEqualizedImpMagSys = Color.Red,
            colorEqualizedImpPhaseSys = Color.Pink,

            colorSPLMagSys = Color.Red,
            colorSPLPhaseSys = Color.Pink,
            colorEqualizedSPLMagSys = Color.Red,
            colorEqualizedSPLPhaseSys = Color.Pink,

            colorCircuitTransferFunctionMag = Color.Green,
            colorCircuitTransferFunctionPhase = Color.LightGreen,
            colorCircuitTransferFunctionMagSys = Color.Red,
            colorCircuitTransferFunctionPhaseSys = Color.Pink,

            colorCircuitOnlyImpMag = Color.Blue,
            colorCircuitOnlyImpPhase = Color.BlueViolet,

            lineWidthSPLInterpMag = 1.6F,
            lineWidthSPLInterpMagSys = 1.6F,
            lineWidthSPLEqMag = 2F,
            lineWidthSPLEqMagSys = 2F,
            lineWidthSPLSumMag = 2F,
            lineWidthIMPInterpMag = 2F,
            lineWidthIMPEqMag = 2F,
            lineWidthIMPCompMag = 2F,

            lineWidthSPLInterpPhase = 1.6F,
            lineWidthSPLInterpPhaseSys = 1.6F,
            lineWidthSPLEqPhase = 1.6F,
            lineWidthSPLEqPhaseSys = 1.6F,
            lineWidthSPLSumPhase = 1.6F,
            lineWidthIMPInterpPhase = 1.6F,
            lineWidthIMPEqPhase = 1.6F,
            lineWidthIMPCompPhase = 1.6F,

            center = new Spatial(), // Default initially to {0,0,0}
            diameter = 0.0,
            isActive = false,
            distance = 0.0,

            splAdjust = 0.0,

            titleSPLReference = "T Ref",
            tagSPLReference = "Tweeter Reference",
            colorSPLReference = Color.Red,
            lineWidthSPLReference = 1.6F,
};

        private DriverCore _midrange1 = new DriverCore()
        {
            type = DriverType.Midrange,

            tagSPLMag = "midrange1InterpSPLMag",
            tagSPLPhase = "midrange1InterpSPLPhase",
            tagImpMag = "midrange1InterpIMPMag",
            tagImpPhase = "midrange1InterpIMPPhase",

            titleSPLMag = "M1 SPL",
            titleSPLPhase = "M1 Phase",
            titleIMPMag = "M1 Imp",
            titleIMPPhase = "M1 Phase",

            tagSPLMagRaw = "midrange1SPLMagRaw",
            tagSPLPhaseRaw = "midrange1SPLPhaseRaw",
            tagImpMagRaw = "midrange1IMPMagRaw",
            tagImpPhaseRaw = "midrange1IMPPhaseRaw",

            titleSPLMagRaw = "M1 SPLRaw",
            titleSPLPhaseRaw = "M1 PhaseRaw",
            titleIMPMagRaw = "M1 ImpRaw",
            titleIMPPhaseRaw = "M1 PhaseRaw",

            tagEqualizedSPLMag = "midrangeEqualizedSPLMag",
            tagEqualizedSPLPhase = "midrangeEqualizedSPLPhase",
            tagEqualizedImpMag = "midrangeEqualizedIMPMag",
            tagEqualizedImpPhase = "midrangeEqualizedIMPPhase",
            titleEqualizedSPLMag = "M SPL EQ",
            titleEqualizedSPLPhase = "M Phase EQ",
            titleEqualizedIMPMag = "M Imp EQ",
            titleEqualizedIMPPhase = "M Phase EQ",

            tagCompensatedImpMag = "midrangeCompensatedIMPMag",
            tagCompensatedImpPhase = "midrangeCompensatedIMPPhase",
            titleCompensatedIMPMag = "Comp Z",
            titleCompensatedIMPPhase = "Comp Phase",

            tagCircuitOnlyImpMag = "midrangeCircuitOnlyIMPMag",
            tagCircuitOnlyImpPhase = "midrangeCircuitOnlyIMPPhase",
            titleCircuitOnlyIMPMag = "Circ Z",
            titleCircuitOnlyIMPPhase = "Circ Phase",

            tagCircuitTransferFunctionMag = "midrangeCircuitTransferFunctionMag",
            tagCircuitTransferFunctionPhase = "midrangeCircuitTransferFunctionPhase",
            titleCircuitTransferFunctionMag = "Filter",
            titleCircuitTransferFunctionPhase = "M Filter Phase",
            titleCircuitTransferFunctionMagSys = "Midrange1 Filter",

            colorSPLMagRaw = Color.Gray,
            colorSPLPhaseRaw = Color.Gray,
            colorImpMagRaw = Color.Gray,
            colorImpPhaseRaw = Color.Gray,

            colorSPLMag = Color.Blue,
            colorSPLPhase = Color.LightBlue,

            colorImpMag = Color.Blue,
            colorImpPhase = Color.LightBlue,
            colorCompensatedImpMag = Color.DarkGreen,
            colorCompensatedImpPhase = Color.LightGreen,

            colorEqualizedSPLMag = Color.Red,
            colorEqualizedSPLPhase = Color.Pink,

            colorEqualizedImpMag = Color.Red,
            colorEqualizedImpPhase = Color.Pink,
            colorEqualizedImpMagSys = Color.Green,
            colorEqualizedImpPhaseSys = Color.LightGreen,

            colorSPLMagSys = Color.Green,
            colorSPLPhaseSys = Color.LawnGreen,
            colorEqualizedSPLMagSys = Color.Green,
            colorEqualizedSPLPhaseSys = Color.LightGreen,

            colorCircuitTransferFunctionMag = Color.Green,
            colorCircuitTransferFunctionPhase = Color.LightGreen,
            colorCircuitTransferFunctionMagSys = Color.Green,
            colorCircuitTransferFunctionPhaseSys = Color.LightGreen,

            colorCircuitOnlyImpMag = Color.Blue,
            colorCircuitOnlyImpPhase = Color.BlueViolet,

            lineWidthSPLInterpMag = 1.6F,
            lineWidthSPLInterpMagSys = 1.6F,
            lineWidthSPLEqMag = 2F,
            lineWidthSPLEqMagSys = 2F,
            lineWidthSPLSumMag = 2F,
            lineWidthIMPInterpMag = 2F,
            lineWidthIMPEqMag = 2F,
            lineWidthIMPCompMag = 2F,

            lineWidthSPLInterpPhase = 1.6F,
            lineWidthSPLInterpPhaseSys = 1.6F,
            lineWidthSPLEqPhase = 1.6F,
            lineWidthSPLEqPhaseSys = 1.6F,
            lineWidthSPLSumPhase = 1.6F,
            lineWidthIMPInterpPhase = 1.6F,
            lineWidthIMPEqPhase = 1.6F,
            lineWidthIMPCompPhase = 1.6F,

            center = new Spatial(), // Default initially to {0,0,0}
            diameter = 0.0,
            isActive = false,
            distance = 0.0,

            splAdjust = 0.0,

            titleSPLReference = "M1 Ref",
            tagSPLReference = "Midrange1 Reference",
            colorSPLReference = Color.Green,
            lineWidthSPLReference = 1.6F,
        };

        private DriverCore _midrange2 = new DriverCore()
        {
            type = DriverType.Midrange,

            tagSPLMag = "midrange2InterpSPLMag",
            tagSPLPhase = "midrange2InterpSPLPhase",
            tagImpMag = "midrange2InterpIMPMag",
            tagImpPhase = "midrange2InterpIMPPhase",

            titleSPLMag = "M2 SPL",
            titleSPLPhase = "M2 Phase",
            titleIMPMag = "M2 Imp",
            titleIMPPhase = "M2 Phase",

            tagSPLMagRaw = "midrange2SPLMagRaw",
            tagSPLPhaseRaw = "midrange2SPLPhaseRaw",
            tagImpMagRaw = "midrange2IMPMagRaw",
            tagImpPhaseRaw = "midrange2IMPPhaseRaw",

            titleSPLMagRaw = "M2 SPLRaw",
            titleSPLPhaseRaw = "M2 PhaseRaw",
            titleIMPMagRaw = "M2 ImpRaw",
            titleIMPPhaseRaw = "M2 PhaseRaw",

            tagEqualizedSPLMag = "midrange2EqualizedSPLMag",
            tagEqualizedSPLPhase = "midrange2EqualizedSPLPhase",
            tagEqualizedImpMag = "midrange2EqualizedIMPMag",
            tagEqualizedImpPhase = "midrange2EqualizedIMPPhase",
            titleEqualizedSPLMag = "M2 SPL EQ",
            titleEqualizedSPLPhase = "M2 Phase EQ",
            titleEqualizedIMPMag = "M2 Imp EQ",
            titleEqualizedIMPPhase = "M2 Phase EQ",

            tagCompensatedImpMag = "midrange2CompensatedIMPMag",
            tagCompensatedImpPhase = "midrange2CompensatedIMPPhase",
            titleCompensatedIMPMag = "Comp Z",
            titleCompensatedIMPPhase = "Comp Phase",

            tagCircuitOnlyImpMag = "midrange2CircuitOnlyIMPMag",
            tagCircuitOnlyImpPhase = "midrange2CircuitOnlyIMPPhase",
            titleCircuitOnlyIMPMag = "Circ Z",
            titleCircuitOnlyIMPPhase = "Circ Phase",

            tagCircuitTransferFunctionMag = "midrange2CircuitTransferFunctionMag",
            tagCircuitTransferFunctionPhase = "midrange2CircuitTransferFunctionPhase",
            titleCircuitTransferFunctionMag = "M2 Filter",
            titleCircuitTransferFunctionPhase = "M2 Filter Phase",
            titleCircuitTransferFunctionMagSys = "Midrange2 Filter",

            colorSPLMagRaw = Color.Gray,
            colorSPLPhaseRaw = Color.Gray,
            colorImpMagRaw = Color.Gray,
            colorImpPhaseRaw = Color.Gray,

            colorSPLMag = Color.DarkOliveGreen,
            colorSPLPhase = Color.LawnGreen,

            colorImpMag = Color.DarkOliveGreen,
            colorImpPhase = Color.LawnGreen,
            colorCompensatedImpMag = Color.DarkGreen,
            colorCompensatedImpPhase = Color.LightGreen,

            colorEqualizedSPLMag = Color.Maroon,
            colorEqualizedSPLPhase = Color.Magenta,

            colorEqualizedImpMag = Color.Maroon,
            colorEqualizedImpPhase = Color.Magenta,
            colorEqualizedImpMagSys = Color.Green,
            colorEqualizedImpPhaseSys = Color.LightGreen,

            colorSPLMagSys = Color.Green,
            colorSPLPhaseSys = Color.LawnGreen,
            colorEqualizedSPLMagSys = Color.Green,
            colorEqualizedSPLPhaseSys = Color.LightGreen,

            colorCircuitTransferFunctionMag = Color.Blue,
            colorCircuitTransferFunctionPhase = Color.BlueViolet,
            colorCircuitTransferFunctionMagSys = Color.Green,
            colorCircuitTransferFunctionPhaseSys = Color.LightGreen,

            colorCircuitOnlyImpMag = Color.Blue,
            colorCircuitOnlyImpPhase = Color.BlueViolet,

            lineWidthSPLInterpMag = 1.6F,
            lineWidthSPLInterpMagSys = 1.6F,
            lineWidthSPLEqMag = 2F,
            lineWidthSPLEqMagSys = 2F,
            lineWidthSPLSumMag = 2F,
            lineWidthIMPInterpMag = 2F,
            lineWidthIMPEqMag = 2F,
            lineWidthIMPCompMag = 2F,

            lineWidthSPLInterpPhase = 1.6F,
            lineWidthSPLInterpPhaseSys = 1.6F,
            lineWidthSPLEqPhase = 1.6F,
            lineWidthSPLEqPhaseSys = 1.6F,
            lineWidthSPLSumPhase = 1.6F,
            lineWidthIMPInterpPhase = 1.6F,
            lineWidthIMPEqPhase = 1.6F,
            lineWidthIMPCompPhase = 1.6F,

            center = new Spatial(), // Default initially to {0,0,0}
            diameter = 0.0,
            isActive = false,
            distance = 0.0,

            splAdjust = 0.0,

            titleSPLReference = "M2 Ref",
            tagSPLReference = "Midrange2 Reference",
            colorSPLReference = Color.LightGreen,
            lineWidthSPLReference = 1.6F,
        };

        private DriverCore _woofer1 = new DriverCore()
        {
            type = DriverType.Woofer,

            tagSPLMag = "woofer1InterpSPLMag",
            tagSPLPhase = "woofer1InterpSPLPhase",
            tagImpMag = "woofer1InterpIMPMag",
            tagImpPhase = "woofer1InterpIMPPhase",

            titleSPLMag = "W SPL",
            titleSPLPhase = "W Phase",
            titleIMPMag = "W Imp",
            titleIMPPhase = "W Phase",

            tagSPLMagRaw = "woofer1SPLMagRaw",
            tagSPLPhaseRaw = "woofer1SPLPhaseRaw",
            tagImpMagRaw = "woofer1IMPMagRaw",
            tagImpPhaseRaw = "woofer1IMPPhaseRaw",

            titleSPLMagRaw = "W SPLRaw",
            titleSPLPhaseRaw = "W PhaseRaw",
            titleIMPMagRaw = "W ImpRaw",
            titleIMPPhaseRaw = "W PhaseRaw",

            tagEqualizedSPLMag = "wooferEqualizedSPLMag",
            tagEqualizedSPLPhase = "wooferEqualizedSPLPhase",
            tagEqualizedImpMag = "wooferEqualizedIMPMag",
            tagEqualizedImpPhase = "wooferEqualizedIMPPhase",
            titleEqualizedSPLMag = "W SPL EQ",
            titleEqualizedSPLPhase = "W Phase EQ",
            titleEqualizedIMPMag = "W Imp EQ",
            titleEqualizedIMPPhase = "W Phase EQ",

            tagCompensatedImpMag = "wooferCompensatedIMPMag",
            tagCompensatedImpPhase = "wooferCompensatedIMPPhase",
            titleCompensatedIMPMag = "Comp Z",
            titleCompensatedIMPPhase = "Comp Phase",

            tagCircuitOnlyImpMag = "wooferCircuitOnlyIMPMag",
            tagCircuitOnlyImpPhase = "wooferCircuitOnlyIMPPhase",
            titleCircuitOnlyIMPMag = "Circ Z",
            titleCircuitOnlyIMPPhase = "Circ Phase",

            tagCircuitTransferFunctionMag = "wooferCircuitTransferFunctionMag",
            tagCircuitTransferFunctionPhase = "wooferCircuitTransferFunctionPhase",
            titleCircuitTransferFunctionMag = "Filter",
            titleCircuitTransferFunctionPhase = "W Filter Phase",
            titleCircuitTransferFunctionMagSys = "Woofer1 Filter",

            colorSPLMagRaw = Color.Gray,
            colorSPLPhaseRaw = Color.Gray,
            colorImpMagRaw = Color.Gray,
            colorImpPhaseRaw = Color.Gray,

            colorSPLMag = Color.Blue,
            colorSPLPhase = Color.LightBlue,
            colorImpMag = Color.Blue,
            colorImpPhase = Color.LightBlue,

            colorEqualizedSPLMag = Color.Red,
            colorEqualizedSPLPhase = Color.Pink,
            colorEqualizedImpMag = Color.Red,
            colorEqualizedImpPhase = Color.Pink,

            colorCompensatedImpMag = Color.Green,
            colorCompensatedImpPhase = Color.LightGreen,

            colorSPLMagSys = Color.Blue,
            colorSPLPhaseSys = Color.LightBlue,
            colorEqualizedSPLMagSys = Color.Blue,
            colorEqualizedSPLPhaseSys = Color.Cyan,

            colorCircuitTransferFunctionMag = Color.Green,
            colorCircuitTransferFunctionPhase = Color.LightGreen,
            colorCircuitTransferFunctionMagSys = Color.Blue,
            colorCircuitTransferFunctionPhaseSys = Color.LightBlue,

            colorEqualizedImpMagSys = Color.Blue,
            colorEqualizedImpPhaseSys = Color.LightBlue,

            colorCircuitOnlyImpMag = Color.Blue,
            colorCircuitOnlyImpPhase = Color.BlueViolet,

            lineWidthSPLInterpMag = 1.6F,
            lineWidthSPLInterpMagSys = 1.6F,
            lineWidthSPLEqMag = 2F,
            lineWidthSPLEqMagSys = 2F,
            lineWidthSPLSumMag = 2F,
            lineWidthIMPInterpMag = 2F,
            lineWidthIMPEqMag = 2F,
            lineWidthIMPCompMag = 2F,

            lineWidthSPLInterpPhase = 1.6F,
            lineWidthSPLInterpPhaseSys = 2F,
            lineWidthSPLEqPhase = 1.6F,
            lineWidthSPLEqPhaseSys = 1.6F,
            lineWidthSPLSumPhase = 1.6F,
            lineWidthIMPInterpPhase = 1.6F,
            lineWidthIMPEqPhase = 1.6F,
            lineWidthIMPCompPhase = 1.6F,

            center = new Spatial(), // Default initially to {0,0,0}
            diameter = 0.0,
            isActive = false,
            distance = 0.0,

            splAdjust = 0.0,

            titleSPLReference = "W1 Ref",
            tagSPLReference = "Woofer1 Reference",
            colorSPLReference = Color.Blue,
            lineWidthSPLReference = 1.6F,
        };

        private DriverCore _woofer2 = new DriverCore()
        {
            type = DriverType.Woofer,

            tagSPLMag = "woofer2InterpSPLMag",
            tagSPLPhase = "woofer2InterpSPLPhase",
            tagImpMag = "woofer2InterpIMPMag",
            tagImpPhase = "woofer2InterpIMPPhase",

            titleSPLMag = "W2 SPL",
            titleSPLPhase = "W2 Phase",
            titleIMPMag = "W2 Imp",
            titleIMPPhase = "W2 Phase",

            tagSPLMagRaw = "woofer2SPLMagRaw",
            tagSPLPhaseRaw = "woofer2SPLPhaseRaw",
            tagImpMagRaw = "woofer2IMPMagRaw",
            tagImpPhaseRaw = "woofer2IMPPhaseRaw",

            titleSPLMagRaw = "W2 SPLRaw",
            titleSPLPhaseRaw = "W2 PhaseRaw",
            titleIMPMagRaw = "W2 ImpRaw",
            titleIMPPhaseRaw = "W2 PhaseRaw",

            tagEqualizedSPLMag = "woofer2EqualizedSPLMag",
            tagEqualizedSPLPhase = "woofer2EqualizedSPLPhase",
            tagEqualizedImpMag = "woofer2EqualizedIMPMag",
            tagEqualizedImpPhase = "woofer2EqualizedIMPPhase",
            titleEqualizedSPLMag = "W2 SPL EQ",
            titleEqualizedSPLPhase = "W2 Phase EQ",
            titleEqualizedIMPMag = "W2 Imp EQ",
            titleEqualizedIMPPhase = "W2 Phase EQ",

            tagCompensatedImpMag = "woofer2CompensatedIMPMag",
            tagCompensatedImpPhase = "woofer2CompensatedIMPPhase",
            titleCompensatedIMPMag = "Comp Z",
            titleCompensatedIMPPhase = "Comp Phase",

            tagCircuitOnlyImpMag = "woofer2CircuitOnlyIMPMag",
            tagCircuitOnlyImpPhase = "woofer2CircuitOnlyIMPPhase",
            titleCircuitOnlyIMPMag = "Circ Z",
            titleCircuitOnlyIMPPhase = "Circ Phase",

            tagCircuitTransferFunctionMag = "woofer2CircuitTransferFunctionMag",
            tagCircuitTransferFunctionPhase = "woofer2CircuitTransferFunctionPhase",
            titleCircuitTransferFunctionMag = "W2 Filter",
            titleCircuitTransferFunctionPhase = "W2 Filter Phase",
            titleCircuitTransferFunctionMagSys = "Woofer2 Filter",

            colorSPLMagRaw = Color.Gray,
            colorSPLPhaseRaw = Color.Gray,
            colorImpMagRaw = Color.Gray,
            colorImpPhaseRaw = Color.Gray,

            colorSPLMag = Color.DarkOliveGreen,
            colorSPLPhase = Color.LawnGreen,
            colorImpMag = Color.DarkOliveGreen,
            colorImpPhase = Color.LawnGreen,

            colorEqualizedSPLMag = Color.Maroon,
            colorEqualizedSPLPhase = Color.Magenta,
            colorEqualizedImpMag = Color.Maroon,
            colorEqualizedImpPhase = Color.Magenta,

            colorCompensatedImpMag = Color.DarkGreen,
            colorCompensatedImpPhase = Color.LightGreen,

            colorSPLMagSys = Color.Blue,
            colorSPLPhaseSys = Color.LightBlue,
            colorEqualizedSPLMagSys = Color.Blue,
            colorEqualizedSPLPhaseSys = Color.Cyan,

            colorCircuitTransferFunctionMag = Color.Blue,
            colorCircuitTransferFunctionPhase = Color.BlueViolet,
            colorCircuitTransferFunctionMagSys = Color.Blue,
            colorCircuitTransferFunctionPhaseSys = Color.LightBlue,

            colorEqualizedImpMagSys = Color.Blue,
            colorEqualizedImpPhaseSys = Color.LightBlue,

            colorCircuitOnlyImpMag = Color.Blue,
            colorCircuitOnlyImpPhase = Color.BlueViolet,

            center = new Spatial(), // Default initially to {0,0,0}
            diameter = 0.0,
            isActive = false,
            distance = 0.0,

            splAdjust = 0.0,

            titleSPLReference = "W2 Ref",
            tagSPLReference = "Woofer2 Reference",
            colorSPLReference = Color.LightBlue,
            lineWidthSPLReference = 1.6F,
        };
         
    }
}

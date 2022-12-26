using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.LedEditorEffect
{
    public enum PatternDDU
    {
        LEFT6,
        LEFT5,
        LEFT4,
        LEFT3,
        LEFT2,
        LEFT1,
        RPM1,
        RPM2,
        RPM3,
        RPM4,
        RPM5,
        RPM6,
        RPM7,
        RPM8,
        RPM9,
        RPM10,
        RPM11,
        RPM12,
        RPM13,
        RPM14,
        RPM15,
        RPM16,
        RPM17,
        RIGHT1,
        RIGHT2,
        RIGHT3,
        RIGHT4,
        RIGHT5,
        RIGHT6
    }

    public enum PatternSW1
    {
        SWTOPLEFT,
        SWBOTLEFT,
        SWBOTRIGHT,
        SWTOPRIGHT,
    }


    public enum AnimationType
    {
        Analog,
        LRThreeStep,
        Ferrari488,
        CenterFourStep,
        CenterFill,
        MclarenF1,
        LMP1,
        LMP2,
        AMGGT3,
        Porsche,
        Indycar,
        Linear,
        FormulaRenault,
        MX5,
        Vee,
        AudiR8,
        LamboGT3,
        PorscheGT3R,
        Supercar,
        F4,
        ToyotaGR86
    }

    public enum Threshold
    {
        Shiftlight = 1000,
        Analog1 = 740,
        Analog2 = 760,
        Analog3 = 780,
        Analog4 = 800,
        Analog5 = 820,
        Analog6 = 840,
        Analog7 = 860,
        Analog8 = 880,
        Analog9 = 900,
        Analog10 = 920,
        Analog11 = 940,
        Analog12 = 960,
        Analog13 = 980,
        F1MCL1 = 875,
        F1MCL2 = 900,
        F1MCL3 = 925,
        F1MCL4 = 940,
        F1MCL5 = 955,
        F1MCL6 = 970,
        F1MCL7 = 985,
        TwoStep1 = 900,
        TwoStep2 = 965,
        FourStep1 = 850,
        FourStep2 = 920,
        FourStep3 = 960,
        FourStep4 = 985,
        FiveStep1 = 880,
        FiveStep2 = 910,
        FiveStep3 = 935,
        FiveStep4 = 960,
        FiveStep5 = 980,
        NineStep1 = 810,
        NineStep2 = 850,
        NineStep3 = 890,
        NineStep4 = 910,
        NineStep5 = 930,
        NineStep6 = 950,
        NineStep7 = 965,
        NineStep8 = 970,
        NineStep9 = 985,
        LRFill1 = 810,
        LRFill2 = 830,
        LRFill3 = 850,
        LRFill4 = 870,
        LRFill5 = 890,
        LRFill6 = 905,
        LRFill7 = 920,
        LRFill8 = 935,
        LRFill9 = 950,
        LRFill10 = 965,
        LRFill11 = 970,
        LRFill12 = 985,
        FullRange1 = 730,
        FullRange2 = 750,
        FullRange3 = 770,
        FullRange4 = 790,
        FullRange5 = 810,
        FullRange6 = 830,
        FullRange7 = 850,
        FullRange8 = 870,
        FullRange9 = 885,
        FullRange10 = 900,
        FullRange11 = 915,
        FullRange12 = 930,
        FullRange13 = 945,
        FullRange14 = 960,
        FullRange15 = 970,
        FullRange16 = 980,
        FullRange17 = 990,
        Slip1 = 8,
        Slip2 = 45,
        Slip3 = 80,
        Slip4 = 99

    }


}
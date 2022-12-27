using Newtonsoft.Json;
using GameReaderCommon;
using SimHub.Plugins;
using SimHub.Plugins.DataPlugins.RGBDriver;
using SimHub.Plugins.DataPlugins.RGBDriver.Settings;
using SimHub.Plugins.DataPlugins.RGBDriverCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using IRacingReader;
using System.Windows.Controls;

namespace User.LedEditorEffect
{
    /// <summary>
    /// Class respresentating an effect, the entire class is serialiazed/deserialized using json.
    /// Class are "discovered" based on LedsContainerBase inheritance and ContainerMetadata attribute.
    /// </summary>
    [SimHub.Plugins.DataPlugins.RGBDriver.LedsContainers.Attributes.ContainerMetadata(80, "DahlDesignLED", "DahlDesign LED control for SW1 and DDU", "Effects")]
    public class DahlLedEffect : SimHub.Plugins.DataPlugins.RGBDriver.LedsContainers.Base.LedsContainerBase
    {

        /// <summary>
        /// Set default effect settings here, this is called after adding a new effect, this won't be called after deserialization.
        /// </summary>
        public override void LoadDefaultSettings()
        {

        }

        /// <summary>
        /// Example of property not serialized in settings.
        /// </summary>
        [JsonIgnore]
        public int SamplePropertyNotSerialized { get; set; } = 10;

        /// <summary>
        /// Builds the led result
        /// </summary>
        /// <param name="data">Game data (controlled by the test data editor)</param>
        /// <param name="result">Result provided by the engine</param>
        /// <param name="includedFrom">Specifies the list of profiles from which this effect is included from (useless most of the time)</param>
        /// <returns></returns>
        /// 

        //Animation classes
        public class AnimationDDU
        {

            public List<PatternDDU> AniPattern;
            public List<Color> AniColor;
            public int FrameDuration;

            public AnimationDDU(List<PatternDDU> aniPattern, List<Color> aniColor, int frameDuration)
            {
                AniPattern = aniPattern;
                AniColor = aniColor;
                FrameDuration = frameDuration;
            }
        }

        public class AnimationSW1
        {

            public List<PatternSW1> AniPattern;
            public List<Color> AniColor;
            public int FrameDuration;

            public AnimationSW1(List<PatternSW1> aniPattern, List<Color> aniColor, int frameDuration)
            {
                AniPattern = aniPattern;
                AniColor = aniColor;
                FrameDuration = frameDuration;
            }
        }

        //----------------------------------
        //--------Animation patterns--------
        //----------------------------------
        List<PatternDDU> LeftToRight = new List<PatternDDU> { PatternDDU.RPM1, PatternDDU.RPM2, PatternDDU.RPM3, PatternDDU.RPM4, PatternDDU.RPM5, PatternDDU.RPM6, PatternDDU.RPM7, PatternDDU.RPM8, PatternDDU.RPM9, PatternDDU.RPM10, PatternDDU.RPM11, PatternDDU.RPM12, PatternDDU.RPM13, PatternDDU.RPM14, PatternDDU.RPM15, PatternDDU.RPM16, PatternDDU.RPM17 };
        List<PatternDDU> AllDDULEDs = new List<PatternDDU> {PatternDDU.LEFT1, PatternDDU.LEFT2, PatternDDU.LEFT3, PatternDDU.LEFT4, PatternDDU.LEFT5, PatternDDU.LEFT6, PatternDDU.RPM1, PatternDDU.RPM2, PatternDDU.RPM3, PatternDDU.RPM4, PatternDDU.RPM5, PatternDDU.RPM6, PatternDDU.RPM7, PatternDDU.RPM8, PatternDDU.RPM9, PatternDDU.RPM10, PatternDDU.RPM11, PatternDDU.RPM12, PatternDDU.RPM13, PatternDDU.RPM14, PatternDDU.RPM15, PatternDDU.RPM16, PatternDDU.RPM17, PatternDDU.RIGHT1, PatternDDU.RIGHT2, PatternDDU.RIGHT3, PatternDDU.RIGHT4, PatternDDU.RIGHT5, PatternDDU.RIGHT6 };
        List<PatternDDU> LeftAndRight = new List<PatternDDU> { PatternDDU.LEFT1, PatternDDU.LEFT2, PatternDDU.LEFT3, PatternDDU.LEFT4, PatternDDU.LEFT5, PatternDDU.LEFT6, PatternDDU.RIGHT1, PatternDDU.RIGHT2, PatternDDU.RIGHT3, PatternDDU.RIGHT4, PatternDDU.RIGHT5, PatternDDU.RIGHT6 };
        List<PatternDDU> MclarenF1 = new List<PatternDDU> { PatternDDU.RPM6, PatternDDU.RPM7, PatternDDU.RPM8, PatternDDU.RPM9, PatternDDU.RPM10, PatternDDU.RPM11, PatternDDU.RPM12, PatternDDU.RPM13, PatternDDU.RPM14, PatternDDU.RPM15, PatternDDU.RPM16, PatternDDU.RPM17};
        List<PatternDDU> AMGGT3 = new List<PatternDDU> { PatternDDU.RPM1, PatternDDU.RPM17, PatternDDU.RPM2, PatternDDU.RPM16, PatternDDU.RPM5, PatternDDU.RPM13, PatternDDU.RPM6, PatternDDU.RPM12, PatternDDU.RPM7, PatternDDU.RPM8, PatternDDU.RPM9, PatternDDU.RPM10, PatternDDU.RPM11};
        List<PatternDDU> AMGGT3ABS = new List<PatternDDU> {PatternDDU.RPM7, PatternDDU.RPM8, PatternDDU.RPM9, PatternDDU.RPM10, PatternDDU.RPM11 };
        List<PatternDDU> AMGGT3Braking = new List<PatternDDU> { PatternDDU.RPM1, PatternDDU.RPM2, PatternDDU.RPM16, PatternDDU.RPM17 };
        List<PatternDDU> TCandABS = new List<PatternDDU> { PatternDDU.LEFT3, PatternDDU.LEFT4, PatternDDU.RIGHT3, PatternDDU.RIGHT4};
        List<PatternDDU> EmptyDDU = new List<PatternDDU> {};
        List<PatternDDU> Left = new List<PatternDDU> { PatternDDU.LEFT1, PatternDDU.LEFT2, PatternDDU.LEFT3, PatternDDU.LEFT4, PatternDDU.LEFT5, PatternDDU.LEFT6 };
        List<PatternDDU> Right = new List<PatternDDU> { PatternDDU.RIGHT1, PatternDDU.RIGHT2, PatternDDU.RIGHT3, PatternDDU.RIGHT4, PatternDDU.RIGHT5, PatternDDU.RIGHT6 };
        List<PatternDDU> TwoTopLeftRight = new List<PatternDDU> { PatternDDU.RIGHT1, PatternDDU.RIGHT2, PatternDDU.LEFT1, PatternDDU.LEFT2 };
        List<PatternDDU> ThreeTopLeftRight = new List<PatternDDU> { PatternDDU.RIGHT1, PatternDDU.RIGHT2,PatternDDU.RIGHT3, PatternDDU.LEFT1, PatternDDU.LEFT2, PatternDDU.LEFT3 };
        List<PatternDDU> FormulaRenault = new List<PatternDDU> { PatternDDU.RPM1, PatternDDU.RPM17, PatternDDU.RPM2, PatternDDU.RPM16, PatternDDU.RPM5, PatternDDU.RPM6, PatternDDU.RPM7, PatternDDU.RPM8, PatternDDU.RPM9, PatternDDU.RPM10, PatternDDU.RPM11, PatternDDU.RPM12, PatternDDU.RPM13 };
        List<PatternDDU> PowerLaunch = new List<PatternDDU> { PatternDDU.LEFT6, PatternDDU.LEFT5, PatternDDU.LEFT1, PatternDDU.RPM1, PatternDDU.RPM8, PatternDDU.RPM9,PatternDDU.RPM10 ,PatternDDU.RPM17, PatternDDU.RIGHT1, PatternDDU.RIGHT5, PatternDDU.RIGHT6 };
        List<PatternDDU> AudiR8 = new List<PatternDDU> { PatternDDU.RPM1, PatternDDU.RPM2, PatternDDU.RPM5, PatternDDU.RPM6, PatternDDU.RPM7, PatternDDU.RPM8, PatternDDU.RPM9, PatternDDU.RPM10, PatternDDU.RPM11, PatternDDU.RPM12, PatternDDU.RPM13, PatternDDU.RPM16, PatternDDU.RPM17 };
        List<PatternDDU> Supercar = new List<PatternDDU> { PatternDDU.RPM3, PatternDDU.RPM15, PatternDDU.RPM4, PatternDDU.RPM14,  PatternDDU.RPM5, PatternDDU.RPM13, PatternDDU.RPM6, PatternDDU.RPM12, PatternDDU.RPM7, PatternDDU.RPM11, PatternDDU.RPM8, PatternDDU.RPM9, PatternDDU.RPM10, PatternDDU.RPM1, PatternDDU.RPM17};
        List<PatternDDU> F4 = new List<PatternDDU> { PatternDDU.RPM4, PatternDDU.RPM5, PatternDDU.RPM6, PatternDDU.RPM7, PatternDDU.RPM8, PatternDDU.RPM9, PatternDDU.RPM10, PatternDDU.RPM11, PatternDDU.RPM12, PatternDDU.RPM13};
        List<PatternDDU> ToyotaGR = new List<PatternDDU> { PatternDDU.RPM1, PatternDDU.RPM2, PatternDDU.RPM17, PatternDDU.RPM16, PatternDDU.RPM3, PatternDDU.RPM4, PatternDDU.RPM15, PatternDDU.RPM14, PatternDDU.RPM5, PatternDDU.RPM6, PatternDDU.RPM13, PatternDDU.RPM12, PatternDDU.RPM7, PatternDDU.RPM8, PatternDDU.RPM11, PatternDDU.RPM10 };

        List<PatternSW1> AllSW1LEDs = new List<PatternSW1> { PatternSW1.SWTOPRIGHT, PatternSW1.SWBOTRIGHT, PatternSW1.SWBOTLEFT, PatternSW1.SWTOPLEFT };
        List<PatternSW1> EmptySW1 = new List<PatternSW1> { };
        List<PatternSW1> SW1Tops = new List<PatternSW1> { PatternSW1.SWTOPRIGHT, PatternSW1.SWTOPLEFT };
        List<PatternSW1> SW1Bots = new List<PatternSW1> { PatternSW1.SWBOTRIGHT, PatternSW1.SWBOTLEFT };

        //----------------------------------
        //--------Animation colors----------
        //----------------------------------
        List<Color> analogColors = new List<Color> { Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Red, Color.Red, Color.Red, Color.Red };
        List<Color> Ferrari488Colors = new List<Color> { Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Yellow, Color.Yellow, Color.Yellow, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red };
        List<Color> AllRed = new List<Color> { Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red};
        List<Color> AllGreen = new List<Color> { Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green};
        List<Color> AllBlack = new List<Color> { Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black};
        List<Color> AllWhite = new List<Color> { Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White};
        List<Color> AllYellow = new List<Color> { Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow };
        List<Color> AllOrange = new List<Color> { Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange };
        List<Color> AllBlue = new List<Color> { Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue, Color.Blue };
        List<Color> AllPurple = new List<Color> { Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, Color.Purple, };
        List<Color> AllDarkTurquoise = new List<Color> { Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise };
        List<Color> AllIOrangeRed = new List<Color> { Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed };
        List<Color> AMGGT3Colors = new List<Color> { Color.Green, Color.Green, Color.Green, Color.Green, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red };
        List<Color> MclarenF1Colors = new List<Color> { Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.OrangeRed, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise, Color.DarkTurquoise };
        List<Color> PorscheColors = new List<Color> { Color.Green, Color.Green, Color.Green, Color.Yellow, Color.Yellow, Color.Yellow, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Yellow, Color.Yellow, Color.Yellow, Color.Green, Color.Green, Color.Green };
        List<Color> IndyColors = new List<Color> { Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Red, Color.Red, Color.Red, Color.Red };
        List<Color> MX5 = new List<Color> { Color.Green, Color.Green, Color.Green, Color.Green, Color.Yellow, Color.Yellow, Color.Yellow, Color.Red, Color.Red, Color.Red, Color.Yellow, Color.Yellow, Color.Yellow, Color.Green, Color.Green, Color.Green, Color.Green };
        List<Color> AllEmpty = new List<Color> { Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty, Color.Empty };
        List<Color> DallaraLMP2Colors = new List<Color> { Color.Green, Color.Green, Color.Green, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red, Color.Blue, Color.Blue, Color.Blue };
        List<Color> AudiR8Colors = new List<Color> { Color.Green, Color.Green, Color.LawnGreen, Color.LawnGreen, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.OrangeRed, Color.OrangeRed, Color.Red, Color.Red };
        List<Color> LamboColors = new List<Color> { Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Orange, Color.Orange, Color.Orange, Color.Red, Color.Red, Color.Red, Color.Red, Color.Red };
        List<Color> F4Colors = new List<Color> { Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Green, Color.Red, Color.Red, Color.Red};
        List<Color> ToyotaGRColors = new List<Color> { Color.Green, Color.Green, Color.Green, Color.Green, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Orange, Color.Orange, Color.Orange, Color.Orange, Color.Red, Color.Red, Color.Red, Color.Red };

        //----------------------------------
        //--------Animation thresholds------
        //----------------------------------
        List <Threshold> analogThresholds = new List<Threshold> { Threshold.Analog1, Threshold.Analog2, Threshold.Analog3, Threshold.Analog4, Threshold.Analog5, Threshold.Analog6, Threshold.Analog7, Threshold.Analog8, Threshold.Analog9, Threshold.Analog10, Threshold.Analog11, Threshold.Analog12, Threshold.Analog13, Threshold.Shiftlight };
        List<Threshold> MclarenF1Thresholds = new List<Threshold> { Threshold.F1MCL1, Threshold.F1MCL2, Threshold.F1MCL3, Threshold.F1MCL4, Threshold.F1MCL5, Threshold.F1MCL6, Threshold.F1MCL7, Threshold.Shiftlight, Threshold.Shiftlight, Threshold.Shiftlight, Threshold.Shiftlight, Threshold.Shiftlight };
        List<Threshold> Ferrari488Thresholds = new List<Threshold> { Threshold.LRFill1, Threshold.LRFill2, Threshold.LRFill3, Threshold.LRFill4, Threshold.LRFill5, Threshold.LRFill6, Threshold.LRFill7, Threshold.LRFill8, Threshold.LRFill9, Threshold.LRFill10, Threshold.LRFill11, Threshold.LRFill12, Threshold.Shiftlight, Threshold.Shiftlight, Threshold.Shiftlight, Threshold.Shiftlight, Threshold.Shiftlight };
        List<Threshold> AMGGT3Thresholds = new List<Threshold> { Threshold.FiveStep1, Threshold.FiveStep1, Threshold.FiveStep2, Threshold.FiveStep2, Threshold.FiveStep3, Threshold.FiveStep3, Threshold.FiveStep4, Threshold.FiveStep4, Threshold.FiveStep5, Threshold.FiveStep5, Threshold.FiveStep5, Threshold.FiveStep5, Threshold.FiveStep5 };
        List<Threshold> PorscheThresholds = new List<Threshold> { Threshold.NineStep2, Threshold.NineStep3, Threshold.NineStep4, Threshold.NineStep5, Threshold.NineStep6, Threshold.NineStep7, Threshold.NineStep8, Threshold.NineStep9, Threshold.NineStep9, Threshold.NineStep9, Threshold.NineStep8, Threshold.NineStep7, Threshold.NineStep6, Threshold.NineStep5, Threshold.NineStep4, Threshold.NineStep3, Threshold.NineStep2 };
        List<Threshold> FullRange = new List<Threshold> { Threshold.FullRange1, Threshold.FullRange2, Threshold.FullRange3, Threshold.FullRange4, Threshold.FullRange5, Threshold.FullRange6, Threshold.FullRange7, Threshold.FullRange8, Threshold.FullRange9, Threshold.FullRange10, Threshold.FullRange11, Threshold.FullRange12, Threshold.FullRange13, Threshold.FullRange14, Threshold.FullRange15, Threshold.FullRange16, Threshold.FullRange17 };
        List<Threshold> NineSteps = new List<Threshold> { Threshold.NineStep1, Threshold.NineStep2, Threshold.NineStep3, Threshold.NineStep4, Threshold.NineStep5, Threshold.NineStep6, Threshold.NineStep7, Threshold.NineStep8, Threshold.NineStep9 };
        List<Threshold> FormulaRenaultThresholds = new List<Threshold> { Threshold.Shiftlight, Threshold.Shiftlight, Threshold.Shiftlight, Threshold.Shiftlight, Threshold.NineStep1, Threshold.NineStep2, Threshold.NineStep3, Threshold.NineStep4, Threshold.NineStep5, Threshold.NineStep6, Threshold.NineStep7, Threshold.NineStep8, Threshold.NineStep9};
        List<Threshold> MX5Thresholds = new List<Threshold> { Threshold.NineStep3, Threshold.NineStep4, Threshold.NineStep5, Threshold.NineStep6, Threshold.NineStep7, Threshold.NineStep8, Threshold.NineStep9, Threshold.Shiftlight, Threshold.Shiftlight, Threshold.Shiftlight, Threshold.NineStep9, Threshold.NineStep8, Threshold.NineStep7, Threshold.NineStep6, Threshold.NineStep5, Threshold.NineStep4, Threshold.NineStep3 };
        List<Threshold> FiveSteps = new List<Threshold> { Threshold.FiveStep1, Threshold.FiveStep2, Threshold.FiveStep3, Threshold.FiveStep4, Threshold.FiveStep5 };
        List<Threshold> DallaraLMP2 = new List<Threshold> { Threshold.FiveStep1, Threshold.FiveStep1, Threshold.FiveStep1, Threshold.FiveStep2, Threshold.FiveStep2, Threshold.FiveStep2, Threshold.FiveStep3, Threshold.FiveStep3, Threshold.FiveStep3, Threshold.FiveStep4, Threshold.FiveStep4, Threshold.FiveStep4, Threshold.FiveStep5, Threshold.FiveStep5, Threshold.FiveStep5, Threshold.Shiftlight, Threshold.Shiftlight};
        List<Threshold> AudiR8Thresholds = new List<Threshold> { Threshold.Analog2, Threshold.Analog3, Threshold.Analog4, Threshold.Analog5, Threshold.Analog6, Threshold.Analog7, Threshold.Analog8, Threshold.Analog9, Threshold.Analog10, Threshold.Analog11, Threshold.Analog12, Threshold.Analog13, Threshold.Shiftlight };
        List<Threshold> SupercarThresholds = new List<Threshold> { Threshold.NineStep5, Threshold.NineStep5, Threshold.NineStep6, Threshold.NineStep6, Threshold.NineStep7, Threshold.NineStep7, Threshold.NineStep8, Threshold.NineStep8, Threshold.NineStep8, Threshold.NineStep8, Threshold.Shiftlight, Threshold.Shiftlight, Threshold.Shiftlight, Threshold.Shiftlight, Threshold.Shiftlight};
        List<Threshold> SlipThresholds = new List<Threshold> {Threshold.Slip4, Threshold. Slip4, Threshold.Slip4, Threshold.Slip3, Threshold.Slip2, Threshold.Slip1 };
        List<Threshold> F4Thresholds = new List<Threshold> {Threshold.Analog4, Threshold.Analog5, Threshold.Analog6, Threshold.Analog7, Threshold.Analog8, Threshold.Analog9, Threshold.Analog10, Threshold.Analog11, Threshold.Analog12, Threshold.Analog13};
        List<Threshold> ToyotaGRThresholds = new List<Threshold> { Threshold.FourStep1, Threshold.FourStep1, Threshold.FourStep1, Threshold.FourStep1, Threshold.FourStep2, Threshold.FourStep2, Threshold.FourStep2, Threshold.FourStep2, Threshold.FourStep3, Threshold.FourStep3, Threshold.FourStep3, Threshold.FourStep3, Threshold.FourStep4, Threshold.FourStep4, Threshold.FourStep4, Threshold.FourStep4 };

        AnimationType[] excemptList = { AnimationType.LMP2, AnimationType.AudiR8, AnimationType.LamboGT3 };
        

        //---------------------------------
        //--------"SHOW" animations--------
        //---------------------------------

        //IGNITION, 8 frames

        //Frame Patterns
        List <PatternDDU> Ign1 = new List<PatternDDU> { PatternDDU.LEFT6, PatternDDU.RIGHT6 };
        List<PatternDDU> Ign2 = new List<PatternDDU> { PatternDDU.LEFT5, PatternDDU.RIGHT5 };
        List<PatternDDU> Ign3 = new List<PatternDDU> { PatternDDU.LEFT4, PatternDDU.RIGHT4 };
        List<PatternDDU> Ign4 = new List<PatternDDU> { PatternDDU.LEFT3, PatternDDU.RIGHT3 };
        List<PatternDDU> Ign5 = new List<PatternDDU> { PatternDDU.LEFT2, PatternDDU.RIGHT2 };
        List<PatternDDU> Ign6 = new List<PatternDDU> { PatternDDU.LEFT1, PatternDDU.RIGHT1 };

        List<PatternSW1> IgnSW1 = new List<PatternSW1> { PatternSW1.SWTOPLEFT };

        //Frames 7-9 are with LeftToRight pattern
        //Color patterns are all with AllGreen
        //Frame duration equal for all frames, 200 ms

        //Similar animation for SW1
        List<Color> SW1Pit1 = new List<Color> { Color.Yellow, Color.Red, Color.Yellow, Color.Red };
        List<Color> SW1Pit2 = new List<Color> { Color.Red, Color.Yellow, Color.Red , Color.Yellow };

        List<Color> BiteAdjust1 = new List<Color> { Color.ForestGreen, Color.Black, Color.ForestGreen, Color.Black };
        List<Color> BiteAdjust2 = new List<Color> { Color.Black, Color.ForestGreen, Color.Black, Color.ForestGreen };

        List<Color> BiteAdjust3 = new List<Color> { Color.Aquamarine, Color.Black, Color.Aquamarine, Color.Black };
        List<Color> BiteAdjust4 = new List<Color> { Color.Black, Color.Aquamarine, Color.Black, Color.Aquamarine };

        List<Color> BiteAdjust5 = new List<Color> { Color.Fuchsia, Color.Black, Color.Fuchsia, Color.Black };
        List<Color> BiteAdjust6 = new List<Color> { Color.Black, Color.Fuchsia, Color.Black, Color.Fuchsia };

        int ignDuration = 100;
        int radioDuration = 70;
        public int radioCounter = 0;
        public int ignCounter = 0;
        public int ignCounterSW1 = 0;

        //PIT 

        List<PatternDDU> pit1 = new List<PatternDDU> { PatternDDU.LEFT3, PatternDDU.LEFT4, PatternDDU.RPM5, PatternDDU.RPM6, PatternDDU.RPM12, PatternDDU.RPM13, PatternDDU.RIGHT3, PatternDDU.RIGHT4};
        List<PatternDDU> pit2 = new List<PatternDDU> { PatternDDU.LEFT2, PatternDDU.LEFT3, PatternDDU.LEFT4, PatternDDU.LEFT5, PatternDDU.RPM4, PatternDDU.RPM5, PatternDDU.RPM6, PatternDDU.RPM7, PatternDDU.RPM11, PatternDDU.RPM12, PatternDDU.RPM13, PatternDDU.RPM14, PatternDDU.RIGHT2, PatternDDU.RIGHT3, PatternDDU.RIGHT4, PatternDDU.RIGHT5};

        List<Color> pitColor1 = new List<Color> { Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow, Color.Yellow};
        List<Color> pitColor2 = new List<Color> { Color.Yellow, Color.Red, Color.Red, Color.Yellow, Color.Yellow, Color.Red, Color.Red, Color.Yellow, Color.Yellow, Color.Red, Color.Red, Color.Yellow, Color.Yellow, Color.Red, Color.Red, Color.Yellow};

        int pitAniDuration = 400;
        public int pitCounter = 0;
        public int pitSW1Counter = 0;
        public int biteCounter1 = 0;
        public int biteCounter2 = 0;
        public int biteCounter3 = 0;

        //PIT LIM
        List<PatternDDU> pitLim1 = new List<PatternDDU> { PatternDDU.LEFT3, PatternDDU.LEFT4, PatternDDU.RIGHT3, PatternDDU.RIGHT4};
        List<PatternDDU> pitLim2 = new List<PatternDDU> { PatternDDU.LEFT2,PatternDDU.LEFT5, PatternDDU.RIGHT2, PatternDDU.RIGHT5};

        int pitLimAniDuration = 400;
        public int pitLimCounter = 0;
        public int pitLimOutCounter = 0;

        //PIT ENTRY
        List<PatternDDU> pitEntry1 = new List<PatternDDU> { PatternDDU.LEFT3, PatternDDU.LEFT4, PatternDDU.RIGHT3, PatternDDU.RIGHT4, PatternDDU.RPM1, PatternDDU.RPM17, PatternDDU.RPM9 };
        List<PatternDDU> pitEntry2 = new List<PatternDDU> { PatternDDU.LEFT3, PatternDDU.LEFT4, PatternDDU.RIGHT3, PatternDDU.RIGHT4, PatternDDU.RPM2, PatternDDU.RPM16, PatternDDU.RPM9 };
        List<PatternDDU> pitEntry3 = new List<PatternDDU> { PatternDDU.LEFT2, PatternDDU.LEFT5, PatternDDU.RIGHT2, PatternDDU.RIGHT5, PatternDDU.RPM3, PatternDDU.RPM15, PatternDDU.RPM9 };
        List<PatternDDU> pitEntry4 = new List<PatternDDU> { PatternDDU.LEFT2, PatternDDU.LEFT5, PatternDDU.RIGHT2, PatternDDU.RIGHT5, PatternDDU.RPM4, PatternDDU.RPM14, PatternDDU.RPM9 };
        List<PatternDDU> pitEntry5 = new List<PatternDDU> { PatternDDU.LEFT3, PatternDDU.LEFT4, PatternDDU.RIGHT3, PatternDDU.RIGHT4, PatternDDU.RPM5, PatternDDU.RPM13, PatternDDU.RPM9 };
        List<PatternDDU> pitEntry6 = new List<PatternDDU> { PatternDDU.LEFT3, PatternDDU.LEFT4, PatternDDU.RIGHT3, PatternDDU.RIGHT4, PatternDDU.RPM6, PatternDDU.RPM12, PatternDDU.RPM9 };
        List<PatternDDU> pitEntry7 = new List<PatternDDU> { PatternDDU.LEFT2, PatternDDU.LEFT5, PatternDDU.RIGHT2, PatternDDU.RIGHT5, PatternDDU.RPM7, PatternDDU.RPM11, PatternDDU.RPM9 };
        List<PatternDDU> pitEntry8 = new List<PatternDDU> { PatternDDU.LEFT2, PatternDDU.LEFT5, PatternDDU.RIGHT2, PatternDDU.RIGHT5, PatternDDU.RPM8, PatternDDU.RPM10, PatternDDU.RPM9 };

        int pitEntryAniDuration = 200;
        public int pitEntryCounter = 0;

        //SPOTTER
        List<PatternDDU> spotLeft1 = new List<PatternDDU> { PatternDDU.LEFT1 };
        List<PatternDDU> spotLeft2 = new List<PatternDDU> { PatternDDU.LEFT1, PatternDDU.LEFT2 };
        List<PatternDDU> spotLeft3 = new List<PatternDDU> { PatternDDU.LEFT2, PatternDDU.LEFT3 };
        List<PatternDDU> spotLeft4 = new List<PatternDDU> { PatternDDU.LEFT3, PatternDDU.LEFT4 };
        List<PatternDDU> spotLeft5 = new List<PatternDDU> { PatternDDU.LEFT4, PatternDDU.LEFT5 };
        List<PatternDDU> spotLeft6 = new List<PatternDDU> { PatternDDU.LEFT5, PatternDDU.LEFT6 };
        List<PatternDDU> spotLeft7 = new List<PatternDDU> { PatternDDU.LEFT6 };

        List<PatternDDU> spotRight1 = new List<PatternDDU> { PatternDDU.RIGHT1 };
        List<PatternDDU> spotRight2 = new List<PatternDDU> { PatternDDU.RIGHT1, PatternDDU.RIGHT2 };
        List<PatternDDU> spotRight3 = new List<PatternDDU> { PatternDDU.RIGHT2, PatternDDU.RIGHT3 };
        List<PatternDDU> spotRight4 = new List<PatternDDU> { PatternDDU.RIGHT3, PatternDDU.RIGHT4 };
        List<PatternDDU> spotRight5 = new List<PatternDDU> { PatternDDU.RIGHT4, PatternDDU.RIGHT5 };
        List<PatternDDU> spotRight6 = new List<PatternDDU> { PatternDDU.RIGHT5, PatternDDU.RIGHT6 };
        List<PatternDDU> spotRight7 = new List<PatternDDU> { PatternDDU.RIGHT6 };

        int slipBlinkDuration = 150;

        //iRacing Data and global parameters
        DataSampleEx irData;
        double referenceVoltage = 0;
        
        int spotLevels = 7;
        double carLength = 4.6;

        public string referenceString { get; set; }
        public double referenceDouble { get; set; }
        public TimeSpan triggerTime { get; set; } = new TimeSpan { };

        //------------------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------DATA FLOW STARTS HERE-----------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------------------------------

        public override LedResult SetResultBase(LedsGameData data, LedResult result, List<Profile> includedFrom)
        {


            //------------------------------------------------------------------------------------------------------------------------------------------------------
            //----------------------------------------------------------------ANIMATION LISTS-----------------------------------------------------------------------
            //------------------------------------------------------------------------------------------------------------------------------------------------------

            //--------------------------DDU--------------------------------------------

            List<AnimationDDU> Ignition = new List<AnimationDDU> { };
            Ignition.Add(new AnimationDDU(Ign1, AllGreen, ignDuration));
            Ignition.Add(new AnimationDDU(Ign2, AllGreen, ignDuration));
            Ignition.Add(new AnimationDDU(Ign3, AllGreen, ignDuration));
            Ignition.Add(new AnimationDDU(Ign4, AllGreen, ignDuration));
            Ignition.Add(new AnimationDDU(Ign5, AllGreen, ignDuration));
            Ignition.Add(new AnimationDDU(Ign6, AllGreen, ignDuration));
            Ignition.Add(new AnimationDDU(LeftToRight, AllGreen, ignDuration));
            Ignition.Add(new AnimationDDU(LeftToRight, AllBlack, ignDuration));
            Ignition.Add(new AnimationDDU(LeftToRight, AllGreen, ignDuration));

            List<AnimationDDU> Radio = new List<AnimationDDU> { };
            Radio.Add(new AnimationDDU(Ign1, AllPurple, radioDuration));
            Radio.Add(new AnimationDDU(Ign2, AllPurple, radioDuration));
            Radio.Add(new AnimationDDU(Ign3, AllPurple, radioDuration));
            Radio.Add(new AnimationDDU(Ign4, AllPurple, radioDuration));
            Radio.Add(new AnimationDDU(Ign5, AllPurple, radioDuration));
            Radio.Add(new AnimationDDU(Ign6, AllPurple, radioDuration));

            List<AnimationDDU> PitNoLim = new List<AnimationDDU> { };
            PitNoLim.Add(new AnimationDDU(pit1, pitColor1, pitAniDuration));
            PitNoLim.Add(new AnimationDDU(pit2, pitColor2, pitAniDuration));

            List<AnimationDDU> PitLim = new List<AnimationDDU> { };
            PitLim.Add(new AnimationDDU(pitLim1, AllBlue, pitLimAniDuration));
            PitLim.Add(new AnimationDDU(pitLim2, AllBlue, pitLimAniDuration));

            List<AnimationDDU> PitLimOut = new List<AnimationDDU> { };
            PitLimOut.Add(new AnimationDDU(pitLim1, AllRed, pitLimAniDuration));
            PitLimOut.Add(new AnimationDDU(pitLim2, AllRed, pitLimAniDuration));

            List<AnimationDDU> PitEntry = new List<AnimationDDU> { };
            PitEntry.Add(new AnimationDDU(pitEntry1, AllBlue, pitEntryAniDuration));
            PitEntry.Add(new AnimationDDU(pitEntry2, AllBlue, pitEntryAniDuration));
            PitEntry.Add(new AnimationDDU(pitEntry3, AllBlue, pitEntryAniDuration));
            PitEntry.Add(new AnimationDDU(pitEntry4, AllBlue, pitEntryAniDuration));
            PitEntry.Add(new AnimationDDU(pitEntry5, AllBlue, pitEntryAniDuration));
            PitEntry.Add(new AnimationDDU(pitEntry6, AllBlue, pitEntryAniDuration));
            PitEntry.Add(new AnimationDDU(pitEntry7, AllBlue, pitEntryAniDuration));
            PitEntry.Add(new AnimationDDU(pitEntry8, AllBlue, pitEntryAniDuration));

            List<AnimationDDU> SpotLeft = new List<AnimationDDU> { };
            SpotLeft.Add(new AnimationDDU(spotLeft1, AllGreen, 0));
            SpotLeft.Add(new AnimationDDU(spotLeft2, AllGreen, 0));
            SpotLeft.Add(new AnimationDDU(spotLeft3, AllGreen, 0));
            SpotLeft.Add(new AnimationDDU(spotLeft4, AllGreen, 0));
            SpotLeft.Add(new AnimationDDU(spotLeft5, AllGreen, 0));
            SpotLeft.Add(new AnimationDDU(spotLeft6, AllGreen, 0));
            SpotLeft.Add(new AnimationDDU(spotLeft7, AllGreen, 0));

            List<AnimationDDU> SpotRight = new List<AnimationDDU> { };
            SpotRight.Add(new AnimationDDU(spotRight1, AllGreen, 0));
            SpotRight.Add(new AnimationDDU(spotRight2, AllGreen, 0));
            SpotRight.Add(new AnimationDDU(spotRight3, AllGreen, 0));
            SpotRight.Add(new AnimationDDU(spotRight4, AllGreen, 0));
            SpotRight.Add(new AnimationDDU(spotRight5, AllGreen, 0));
            SpotRight.Add(new AnimationDDU(spotRight6, AllGreen, 0));
            SpotRight.Add(new AnimationDDU(spotRight7, AllGreen, 0));

            //--------------------------SW1--------------------------------------------

            List<AnimationSW1> IgnitionSW1 = new List<AnimationSW1> { };
            IgnitionSW1.Add(new AnimationSW1(IgnSW1, AllGreen, pitAniDuration / 2));
            IgnitionSW1.Add(new AnimationSW1(IgnSW1, AllBlack, pitAniDuration / 2));
            IgnitionSW1.Add(new AnimationSW1(IgnSW1, AllGreen, pitAniDuration / 2));
            IgnitionSW1.Add(new AnimationSW1(IgnSW1, AllBlack, pitAniDuration / 2));
            IgnitionSW1.Add(new AnimationSW1(IgnSW1, AllGreen, pitAniDuration / 2));

            List<AnimationSW1> PitNoLimSW1 = new List<AnimationSW1> { };
            PitNoLimSW1.Add(new AnimationSW1(AllSW1LEDs, SW1Pit1, pitAniDuration));
            PitNoLimSW1.Add(new AnimationSW1(AllSW1LEDs, SW1Pit2, pitAniDuration));

            List<AnimationSW1> BiteSetting1 = new List<AnimationSW1> { };
            BiteSetting1.Add(new AnimationSW1(AllSW1LEDs, BiteAdjust1, pitAniDuration));
            BiteSetting1.Add(new AnimationSW1(AllSW1LEDs, BiteAdjust2, pitAniDuration));

            List<AnimationSW1> BiteSetting2 = new List<AnimationSW1> { };
            BiteSetting2.Add(new AnimationSW1(AllSW1LEDs, BiteAdjust3, pitAniDuration));
            BiteSetting2.Add(new AnimationSW1(AllSW1LEDs, BiteAdjust4, pitAniDuration));

            List<AnimationSW1> BiteSetting3 = new List<AnimationSW1> { };
            BiteSetting3.Add(new AnimationSW1(AllSW1LEDs, BiteAdjust5, pitAniDuration));
            BiteSetting3.Add(new AnimationSW1(AllSW1LEDs, BiteAdjust6, pitAniDuration));

            //------------------------------------------------------------------------------------------------------------------------------------------------------
            //----------------------------------------------------------------OBTAINING/CLEARING LEDS---------------------------------------------------------------
            //------------------------------------------------------------------------------------------------------------------------------------------------------
            PluginManager pluginManager = PluginManager.GetInstance();
            var NewData = pluginManager.LastData.NewData;

            int DDUstartLED = Convert.ToInt32(pluginManager.GetPropertyValue("DahlDesign.DDUstartLED"));
            bool DDUEnabled = Convert.ToBoolean(pluginManager.GetPropertyValue("DahlDesign.DDUEnabled"));
            int SW1startLED = Convert.ToInt32(pluginManager.GetPropertyValue("DahlDesign.SW1startLED"));
            bool SW1Enabled = Convert.ToBoolean(pluginManager.GetPropertyValue("DahlDesign.SW1Enabled"));

            double slipLF = Convert.ToDouble(pluginManager.GetPropertyValue("DahlDesign.SlipLF"));
            double slipRF = Convert.ToDouble(pluginManager.GetPropertyValue("DahlDesign.SlipRF"));
            double slipLR = Convert.ToDouble(pluginManager.GetPropertyValue("DahlDesign.SlipLR"));
            double slipRR = Convert.ToDouble(pluginManager.GetPropertyValue("DahlDesign.SlipRR"));

            if (slipLF == 100)
            {
                slipRF = 100;
            }
            else if (slipRF == 100)
            {
                slipLF = 100;
            }

            int clearLeds = 0;
            if ((SW1startLED > DDUstartLED && SW1Enabled )|| (SW1Enabled && !DDUEnabled))
            {
                clearLeds = SW1startLED + 2;
            }
            else if (DDUstartLED > SW1startLED && DDUEnabled || (DDUEnabled && !SW1Enabled))
            {
                clearLeds = DDUstartLED + 27;
            }
            result.Clear(clearLeds);

            //------------------------------------------------------------------------------------------------------------------------------------------------------
            //----------------------------------------------------------------OBTAINING DATA-------------------------------------------------------------------------
            //------------------------------------------------------------------------------------------------------------------------------------------------------

            //----------------------------------------------------------
            //-------------------------IRACING--------------------------
            //----------------------------------------------------------


            if (data.GameRunning && data.GameName == "IRacing")
            {
                bool idle = Convert.ToBoolean(pluginManager.GetPropertyValue("DahlDesign.Idle"));

                if (pluginManager.GameManager.GameData.GameRunning && !idle)
                {

                    //DahlDesign data
                    AnimationType animationType = (AnimationType)Convert.ToInt32(pluginManager.GetPropertyValue("DahlDesign.AnimationType"));
                    double revLim = Convert.ToInt32(pluginManager.GetPropertyValue("DahlDesign.TrueRevLimiter"));
                    double shiftLight = Convert.ToDouble(pluginManager.GetPropertyValue("DahlDesign.ShiftLightRPM"));
                    double pitBoxPosition = Convert.ToDouble(pluginManager.GetPropertyValue("DahlDesign.PitBoxPosition"));
                    bool pitSpeeding = Convert.ToBoolean(pluginManager.GetPropertyValue("DahlDesign.PitSpeeding"));
                    bool pitEntry = Convert.ToBoolean(pluginManager.GetPropertyValue("DahlDesign.PitEntry"));
                    string radioName = Convert.ToString(pluginManager.GetPropertyValue("DahlDesign.RadioName"));
                    bool launchScreen = Convert.ToBoolean(pluginManager.GetPropertyValue("DahlDesign.LaunchScreen"));
                    bool bitePointAdjust = Convert.ToBoolean(pluginManager.GetPropertyValue("DahlDesign.BitePointAdjust"));
                    bool radio = Convert.ToBoolean(pluginManager.GetPropertyValue("DahlDesign.Radio"));
                    bool p2p = Convert.ToBoolean(pluginManager.GetPropertyValue("DahlDesign.P2PStatus"));
                    bool highPower = Convert.ToBoolean(pluginManager.GetPropertyValue("DahlDesign.HighPower"));
                    double launchThrottle = Convert.ToDouble(pluginManager.GetPropertyValue("DahlDesign.LaunchThrottle"));
                    int biteSetting = Convert.ToInt32(pluginManager.GetPropertyValue("DahlDesign.DDCbiteSetting"));
                    bool neutralButton = Convert.ToBoolean(pluginManager.GetPropertyValue("DahlDesign.DDCneutralActive"));
                    int biteSettingSW1 = Convert.ToInt32(pluginManager.GetPropertyValue("DahlDesign.SW1BiteSetting"));
                    bool neutralButtonSW1 = Convert.ToBoolean(pluginManager.GetPropertyValue("DahlDesign.SW1NeutralActive"));
                    int idleRPM = Convert.ToInt32(pluginManager.GetPropertyValue("DahlDesign.IdleRPM"));

                    double carPositionLeft = Convert.ToDouble(pluginManager.GetPropertyValue("DahlDesign.LeftCarGap")) + carLength;
                    double carPositionRight = Convert.ToDouble(pluginManager.GetPropertyValue("DahlDesign.RightCarGap")) + carLength;

                    bool TCActive = Convert.ToBoolean(pluginManager.GetPropertyValue("DahlDesign.TCActive"));
                    string DRSState = Convert.ToString(pluginManager.GetPropertyValue("DahlDesign.DRSState"));
                    int DRSStatus = 0;
                    switch (DRSState)
                    {
                        case "None":
                            DRSStatus = 0;
                            break;

                        case "Acquired":
                            DRSStatus = 1;
                            break;

                        case "Ready":
                            DRSStatus = 2;
                            break;

                        case "Open":
                            DRSStatus = 4;
                            break;
                    }

                    //LedEditor data
                    bool ABSActive = data.AbsActive;
                    double RPM = data.Rpms;
                    string gear = data.Gear;
                    bool pitLimiterOn = data.PitLimiterOn;
                    bool isInPit = data.IsInPitLane;
                    double speed = data.SpeedKmh;
                    bool spotLeft = data.SpotterCarLeft;
                    bool spotRight = data.SpotterCarRight;


                    //NewData data
                    double brake;
                    double trackLength;
                    double throttle;

                    if (NewData != null)
                    {
                        brake = NewData.Brake;
                        trackLength = NewData.TrackLength;
                        throttle = NewData.Throttle;
                    }
                    else
                    {
                        brake = 0;
                        trackLength = 0;
                        throttle = 0;
                    }


                    //Raw data
                    if (NewData?.GetRawDataObject() is DataSampleEx)
                    {
                        irData = NewData.GetRawDataObject() as DataSampleEx;
                    }

                    irData.Telemetry.TryGetValue("Voltage", out object rawVoltage);   //Ignition voltage check
                    double voltage = Convert.ToDouble(rawVoltage);

                    irData.Telemetry.TryGetValue("SessionState", out object rawSessionState);  //Session State
                    int sessionState = Convert.ToInt32(rawSessionState);

                    if (referenceVoltage != voltage)
                    {
                        if (referenceVoltage == 0)
                        {
                            ignCounter++;
                            ignCounterSW1++;
                        }

                        referenceVoltage = voltage;
                    }

                    int engineWarinings = (int)irData.Telemetry.EngineWarnings;
                    bool waterTemp = Convert.ToBoolean(engineWarinings & 1);
                    bool fuelPressure = Convert.ToBoolean(engineWarinings & 2);
                    bool oilPressure = Convert.ToBoolean(engineWarinings & 4);
                    bool stalling = Convert.ToBoolean(engineWarinings & 8);
                    bool revLimActive = Convert.ToBoolean(engineWarinings & 32);

                    //Other data

                    TimeSpan globalClock = TimeSpan.FromTicks(DateTime.Now.Ticks);

                    //Calculation variables

                    int DDUstart = DDUstartLED - 1;
                    int SW1start = SW1startLED - 1;
                    DateTime referenceTime = DateTime.Now;
                    int opponentPassingLeft = -1;
                    int opponentPassingRight = -1;
                    bool isExcempt = (excemptList.Contains(animationType));

                    //----------------------------------------------------
                    //------------Spotter calculations--------------------
                    //----------------------------------------------------

                    if (spotLeft)
                    {
                        opponentPassingLeft = 0;
                        double passingAdd = 1;

                        for (int i = 0; i < spotLevels; i++)
                        {
                            if (carPositionLeft > passingAdd * (carLength * 2 / spotLevels))
                            {
                                passingAdd++;
                            }
                            else
                            {
                                opponentPassingLeft = spotLevels - i - 1;
                                break;
                            }
                        }
                    }


                    if (spotRight)
                    {
                        opponentPassingRight = 0;
                        double passingAdd = 1;

                        for (int i = 0; i < spotLevels; i++)
                        {
                            if (carPositionRight > passingAdd * (carLength * 2 / spotLevels))
                            {
                                passingAdd++;
                            }
                            else
                            {
                                opponentPassingRight = spotLevels - i - 1;
                                break;
                            }
                        }
                    }


                    //Standard methods

                    void fillerRevDDU(List<PatternDDU> patterns, List<Color> colors, List<Threshold> thresholds, int? ledShift = null, int? endReduction = null, bool? revLimiter = null, Color? revColor = null, int? revBlinkDuration = null, bool? shiftLightTrig = null, Color? shiftTrigColor = null)
                    {
                        List<PatternDDU> Patterns = new List<PatternDDU> { };
                        Patterns = patterns;

                        List<Color> Colors = new List<Color> { };
                        Colors = colors;

                        List<Threshold> Thresholds = new List<Threshold> { };
                        Thresholds = thresholds;

                        int LEDshift = ledShift ?? 0;
                        int EndReduction = endReduction ?? 0;
                        bool RevLim = revLimiter ?? false;
                        Color RevColor = revColor ?? Color.Black;
                        int RevBlinkDuration = revBlinkDuration ?? 0;
                        bool ShiftLightTrig = shiftLightTrig ?? false;
                        Color ShiftTrigColor = shiftTrigColor ?? Color.Black;


                        for (int i = 0; i < Patterns.Count - LEDshift - EndReduction; i++)
                        {
                            if (RevLim && RPM > revLim)
                            {
                                if (RevBlinkDuration > 0)
                                {
                                    if (referenceTime.TimeOfDay.TotalMilliseconds % RevBlinkDuration * 2 > RevBlinkDuration)
                                    {
                                        result[DDUstart + (int)Patterns[i + LEDshift]] = RevColor;
                                    }
                                }
                                else
                                {
                                    result[DDUstart + (int)Patterns[i + LEDshift]] = RevColor;
                                }


                            }

                            else if (ShiftLightTrig && RPM > shiftLight)
                            {
                                result[DDUstart + (int)Patterns[i + LEDshift]] = ShiftTrigColor;
                            }

                            else
                            {
                                double threshold = (double)Thresholds[i] / 1000;
                                if (RPM > (threshold * shiftLight))
                                {
                                    result[DDUstart + (int)Patterns[i + LEDshift]] = Colors[i];
                                }
                            }
                        }
                    }
                    void fillerTriggerDDU(List<PatternDDU> patterns, List<Color> colors, bool condition, List<PatternDDU> clearPattern, bool? clearAll = null, int? blinkDuration = null, int? ledShift = null, int? endReduction = null)
                    {
                        List<PatternDDU> Patterns = new List<PatternDDU> { };
                        Patterns = patterns;

                        List<Color> Colors = new List<Color> { };
                        Colors = colors;

                        List<PatternDDU> ClearPattern = new List<PatternDDU> { };
                        ClearPattern = clearPattern;

                        bool Condition = condition;
                        int BlinkDuration = blinkDuration ?? 0;
                        int LEDshift = ledShift ?? 0;
                        int EndReduction = endReduction ?? 0;
                        bool ClearAll = clearAll ?? false;


                        if (ClearAll && Condition)
                        {
                            for (int i = 0; i < ClearPattern.Count; i++)
                            {
                                result[DDUstart + (int)ClearPattern[i]] = Color.Black;
                            }
                        }

                        if (BlinkDuration > 0 && (referenceTime.TimeOfDay.TotalMilliseconds % BlinkDuration * 2 > BlinkDuration) && Condition)
                        {
                            for (int i = 0; i < Patterns.Count - LEDshift - EndReduction; i++)
                            {
                                result[DDUstart + (int)Patterns[i + LEDshift]] = Colors[i];
                            }
                        }
                        else if (Condition && BlinkDuration == 0)
                        {
                            for (int i = 0; i < Patterns.Count - LEDshift - EndReduction; i++)
                            {
                                result[DDUstart + (int)Patterns[i + LEDshift]] = Colors[i];
                            }
                        }
                    }
                    void fillerAnimationDDU(List<AnimationDDU> animations, ref int frameCounter, bool endCondition, List<PatternDDU> clearPattern, bool? clearAll = null)
                    {
                        List<AnimationDDU> Animations = new List<AnimationDDU> { };
                        bool EndCondition = endCondition;

                        List<PatternDDU> ClearPattern = new List<PatternDDU> { };
                        ClearPattern = clearPattern;

                        Animations = animations;

                        double timer = frameCounter * 1000 / 60;
                        double frameTimer = 0;
                        bool ClearAll = clearAll ?? false;

                        if (EndCondition)
                        {
                            frameCounter = 0;
                        }

                        if (frameCounter > 0)
                        {
                            if (ClearAll)
                            {
                                for (int i = 0; i < ClearPattern.Count; i++)
                                {
                                    result[DDUstart + (int)ClearPattern[i]] = Color.Black;
                                }
                            }

                            for (int i = 0; i < Animations.Count; i++)
                            {
                                frameTimer = frameTimer + Animations[i].FrameDuration;

                                if (frameTimer > timer)
                                {
                                    for (int u = 0; u < Animations[i].AniPattern.Count; u++)
                                    {
                                        result[DDUstart + (int)Animations[i].AniPattern[u]] = Animations[i].AniColor[u];
                                    }

                                    break;
                                }
                                else if (timer > frameTimer && i == Animations.Count - 1)
                                {
                                    frameCounter = 0;
                                    break;
                                }
                            }

                            if (frameCounter != 0)
                            {
                                frameCounter++;
                            }

                        }
                    }
                    void fillerLoopDDU(List<AnimationDDU> animations, ref int frameCounter, bool condition, List<PatternDDU> clearPattern, bool? clearAll = null)
                    {
                        List<AnimationDDU> Animations = new List<AnimationDDU> { };
                        bool Condition = condition;

                        List<PatternDDU> ClearPattern = new List<PatternDDU> { };
                        ClearPattern = clearPattern;

                        Animations = animations;

                        double timer = frameCounter * 1000 / 60;
                        double frameTimer = 0;
                        bool ClearAll = clearAll ?? false;

                        if (condition)
                        {
                            frameCounter++;
                        }
                        else
                        {
                            frameCounter = 0;
                        }

                        if (frameCounter > 0)
                        {
                            if (ClearAll)
                            {
                                for (int i = 0; i < ClearPattern.Count; i++)
                                {
                                    result[DDUstart + (int)ClearPattern[i]] = Color.Black;
                                }
                            }

                            for (int i = 0; i < Animations.Count; i++)
                            {
                                frameTimer = frameTimer + Animations[i].FrameDuration;

                                if (frameTimer >= timer)
                                {
                                    for (int u = 0; u < Animations[i].AniPattern.Count; u++)
                                    {
                                        result[DDUstart + (int)Animations[i].AniPattern[u]] = Animations[i].AniColor[u];
                                    }

                                    break;
                                }
                                else if (timer >= frameTimer && i == Animations.Count - 1)
                                {
                                    frameCounter = 0;
                                    break;
                                }
                            }
                        }
                    }

                    void fillerRevSW1(List<PatternSW1> patterns, List<Color> colors, List<Threshold> thresholds, int? ledShift = null, int? endReduction = null, bool? revLimiter = null, Color? revColor = null, int? revBlinkDuration = null, bool? shiftLightTrig = null, Color? shiftTrigColor = null)
                    {
                        List<PatternSW1> Patterns = new List<PatternSW1> { };
                        Patterns = patterns;

                        List<Color> Colors = new List<Color> { };
                        Colors = colors;

                        List<Threshold> Thresholds = new List<Threshold> { };
                        Thresholds = thresholds;

                        int LEDshift = ledShift ?? 0;
                        int EndReduction = endReduction ?? 0;
                        bool RevLim = revLimiter ?? false;
                        Color RevColor = revColor ?? Color.Black;
                        int RevBlinkDuration = revBlinkDuration ?? 0;
                        bool ShiftLightTrig = shiftLightTrig ?? false;
                        Color ShiftTrigColor = shiftTrigColor ?? Color.Black;


                        for (int i = 0; i < Patterns.Count - LEDshift - EndReduction; i++)
                        {
                            if (RevLim && RPM > revLim)
                            {
                                if (RevBlinkDuration > 0)
                                {
                                    if (referenceTime.TimeOfDay.TotalMilliseconds % RevBlinkDuration * 2 > RevBlinkDuration)
                                    {
                                        result[SW1start + (int)Patterns[i + LEDshift]] = RevColor;
                                    }
                                }
                                else
                                {
                                    result[SW1start + (int)Patterns[i + LEDshift]] = RevColor;
                                }


                            }

                            else if (ShiftLightTrig && RPM > shiftLight)
                            {
                                result[SW1start + (int)Patterns[i + LEDshift]] = ShiftTrigColor;
                            }

                            else
                            {
                                double threshold = (double)Thresholds[i] / 1000;
                                if (RPM > (threshold * shiftLight))
                                {
                                    result[SW1start + (int)Patterns[i + LEDshift]] = Colors[i];
                                }
                            }
                        }
                    }
                    void fillerTriggerSW1(List<PatternSW1> patterns, List<Color> colors, bool condition, List<PatternSW1> clearPattern, bool? clearAll = null, int? blinkDuration = null, int? ledShift = null, int? endReduction = null)
                    {
                        List<PatternSW1> Patterns = new List<PatternSW1> { };
                        Patterns = patterns;

                        List<Color> Colors = new List<Color> { };
                        Colors = colors;

                        List<PatternSW1> ClearPattern = new List<PatternSW1> { };
                        ClearPattern = clearPattern;

                        bool Condition = condition;
                        int BlinkDuration = blinkDuration ?? 0;
                        int LEDshift = ledShift ?? 0;
                        int EndReduction = endReduction ?? 0;
                        bool ClearAll = clearAll ?? false;


                        if (ClearAll && Condition)
                        {
                            for (int i = 0; i < ClearPattern.Count; i++)
                            {
                                result[SW1start + (int)ClearPattern[i]] = Color.Black;
                            }
                        }

                        if (BlinkDuration > 0 && (referenceTime.TimeOfDay.TotalMilliseconds % BlinkDuration * 2 > BlinkDuration) && Condition)
                        {
                            for (int i = 0; i < Patterns.Count - LEDshift - EndReduction; i++)
                            {
                                result[SW1start + (int)Patterns[i + LEDshift]] = Colors[i];
                            }
                        }
                        else if (Condition && BlinkDuration == 0)
                        {
                            for (int i = 0; i < Patterns.Count - LEDshift - EndReduction; i++)
                            {
                                result[SW1start + (int)Patterns[i + LEDshift]] = Colors[i];
                            }
                        }
                    }
                    void fillerAnimationSW1(List<AnimationSW1> animations, ref int frameCounter, bool endCondition, List<PatternSW1> clearPattern, bool? clearAll = null)
                    {
                        List<AnimationSW1> Animations = new List<AnimationSW1> { };
                        bool EndCondition = endCondition;

                        List<PatternSW1> ClearPattern = new List<PatternSW1> { };
                        ClearPattern = clearPattern;

                        Animations = animations;

                        double timer = frameCounter * 1000 / 60;
                        double frameTimer = 0;
                        bool ClearAll = clearAll ?? false;

                        if (EndCondition)
                        {
                            frameCounter = 0;
                        }

                        if (frameCounter > 0)
                        {
                            if (ClearAll)
                            {
                                for (int i = 0; i < ClearPattern.Count; i++)
                                {
                                    result[SW1start + (int)ClearPattern[i]] = Color.Black;
                                }
                            }

                            for (int i = 0; i < Animations.Count; i++)
                            {
                                frameTimer = frameTimer + Animations[i].FrameDuration;

                                if (frameTimer > timer)
                                {
                                    for (int u = 0; u < Animations[i].AniPattern.Count; u++)
                                    {
                                        result[SW1start + (int)Animations[i].AniPattern[u]] = Animations[i].AniColor[u];
                                    }

                                    break;
                                }
                                else if (timer > frameTimer && i == Animations.Count - 1)
                                {
                                    frameCounter = 0;
                                    break;
                                }
                            }

                            if (frameCounter != 0)
                            {
                                frameCounter++;
                            }

                        }
                    }
                    void fillerLoopSW1(List<AnimationSW1> animations, ref int frameCounter, bool condition, List<PatternSW1> clearPattern, bool? clearAll = null)
                    {
                        List<AnimationSW1> Animations = new List<AnimationSW1> { };
                        bool Condition = condition;

                        List<PatternSW1> ClearPattern = new List<PatternSW1> { };
                        ClearPattern = clearPattern;

                        Animations = animations;

                        double timer = frameCounter * 1000 / 60;
                        double frameTimer = 0;
                        bool ClearAll = clearAll ?? false;

                        if (condition)
                        {
                            frameCounter++;
                        }
                        else
                        {
                            frameCounter = 0;
                        }

                        if (frameCounter > 0)
                        {
                            if (ClearAll)
                            {
                                for (int i = 0; i < ClearPattern.Count; i++)
                                {
                                    result[SW1start + (int)ClearPattern[i]] = Color.Black;
                                }
                            }

                            for (int i = 0; i < Animations.Count; i++)
                            {
                                frameTimer = frameTimer + Animations[i].FrameDuration;

                                if (frameTimer >= timer)
                                {
                                    for (int u = 0; u < Animations[i].AniPattern.Count; u++)
                                    {
                                        result[SW1start + (int)Animations[i].AniPattern[u]] = Animations[i].AniColor[u];
                                    }

                                    break;
                                }
                                else if (timer >= frameTimer && i == Animations.Count - 1)
                                {
                                    frameCounter = 0;
                                    break;
                                }
                            }
                        }
                    }

                    bool changed(int delay, string value)
                    {
                        string Value = value;
                        int Delay = delay;

                        if (referenceString != Value)
                        {
                            referenceString = Value;
                            triggerTime = globalClock;
                        }

                        if (globalClock.TotalMilliseconds - triggerTime.TotalMilliseconds < Delay)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }


                    }
                    bool isIncreasing(int delay, double value)
                    {
                        double Value = value;
                        int Delay = delay;

                        if (referenceDouble != Value)
                        {
                            referenceDouble = Value;
                            if (Value > referenceDouble)
                            {
                                triggerTime = globalClock;
                            }
                        }

                        if (globalClock.TotalMilliseconds - triggerTime.TotalMilliseconds < Delay)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    bool isDecreasing(int delay, double value)
                    {
                        double Value = value;
                        int Delay = delay;

                        if (referenceDouble != Value)
                        {
                            referenceDouble = Value;
                            if (Value < referenceDouble)
                            {
                                triggerTime = globalClock;
                            }
                        }

                        if (globalClock.TotalMilliseconds - triggerTime.TotalMilliseconds < Delay)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    //----------------------------------
                    //--------PUSH ANIMATIONS-----------
                    //----------------------------------


                    //----------------------------------
                    //---------------DDU----------------
                    //----------------------------------

                    if (DDUEnabled)
                    {

                        //---------------------------------------------------------
                        //---------------REV LIGHT/ANIMATION TYPING----------------
                        //---------------------------------------------------------

                        switch (animationType)
                        {
                            case AnimationType.Analog:

                                fillerRevDDU(LeftToRight, analogColors, analogThresholds, 0, 3, false);
                                for (int i = 0; i < 3; i++)
                                {
                                    double fractions = (revLim - shiftLight) / 3;
                                    if (RPM > shiftLight + (i + 1) * fractions)
                                    {
                                        result[DDUstart + (int)LeftToRight[i + 14]] = Color.Red;
                                    }

                                }
                                break;

                            case AnimationType.MclarenF1:

                                if (gear == "8")
                                {
                                    fillerRevDDU(MclarenF1, AllIOrangeRed, analogThresholds);
                                }
                                else
                                {
                                    fillerRevDDU(MclarenF1, MclarenF1Colors, MclarenF1Thresholds);
                                }
                                for (int i = 0; i < DRSStatus; i++)
                                {
                                    result[DDUstart + (int)LeftToRight[i]] = Color.Green;
                                }
                                break;

                            case AnimationType.Ferrari488:

                                fillerRevDDU(LeftToRight, Ferrari488Colors, Ferrari488Thresholds, 0, 0, true, Color.Red, 300);

                                break;

                            case AnimationType.AMGGT3:

                                fillerRevDDU(AMGGT3, AMGGT3Colors, AMGGT3Thresholds, 0, 0, true, Color.Red, 250, true, Color.Red);

                                break;

                            case AnimationType.Porsche:

                                fillerRevDDU(LeftToRight, PorscheColors, PorscheThresholds, 0, 0, true, Color.Red, 250, true, Color.Blue);

                                break;

                            case AnimationType.PorscheGT3R:
                                fillerRevDDU(LeftToRight, PorscheColors, PorscheThresholds);
                                fillerTriggerDDU(LeftToRight, AllBlue, RPM > shiftLight, LeftToRight, true, 200);

                                break;

                            case AnimationType.Indycar:
                                fillerRevDDU(LeftToRight, IndyColors, FullRange, 0, 0, true, Color.Red, 0, true, Color.Blue);
                                fillerTriggerDDU(TwoTopLeftRight, AllBlue, RPM > shiftLight, LeftAndRight);
                                fillerTriggerDDU(TwoTopLeftRight, AllRed, RPM > revLim, LeftAndRight, true);

                                fillerTriggerDDU(TwoTopLeftRight, AllPurple, RPM > shiftLight && p2p, LeftAndRight);
                                fillerTriggerDDU(LeftToRight, AllPurple, RPM > shiftLight && p2p, LeftToRight);
                                break;

                            case AnimationType.Linear:
                                fillerRevDDU(LeftToRight, AllRed, FullRange, 0, 0, true, Color.Red, 250, true, Color.Green);
                                break;

                            case AnimationType.FormulaRenault:
                                fillerTriggerDDU(AMGGT3Braking, AllRed, voltage > 0 && RPM < 300, EmptyDDU);
                                fillerRevDDU(FormulaRenault, AllRed, FormulaRenaultThresholds, 0, 0, true, Color.Red, 250, true, Color.Red);
                                break;

                            case AnimationType.MX5:
                                fillerRevDDU(LeftToRight, MX5, MX5Thresholds);
                                fillerTriggerDDU(LeftToRight, MX5, RPM > revLim, LeftToRight, true, 200);
                                break;

                            case AnimationType.Vee:
                                fillerRevDDU(LeftToRight, AllRed, FiveSteps, 6, 6, true, Color.Red, 250, true, Color.Blue);
                                break;

                            case AnimationType.LMP2:
                                fillerTriggerDDU(TwoTopLeftRight, AllRed, voltage > 0 && RPM < 300, EmptyDDU, false, 500);
                                fillerRevDDU(LeftToRight, DallaraLMP2Colors, DallaraLMP2);
                                break;

                            case AnimationType.AudiR8:
                                fillerRevDDU(AudiR8, AudiR8Colors, AudiR8Thresholds, 0, 0, true, Color.Red, 250, false);
                                break;

                            case AnimationType.LamboGT3:
                                fillerTriggerDDU(LeftToRight, AllRed, voltage > 0 && RPM < 300, EmptyDDU, false, 500, 0, 15);
                                fillerRevDDU(AudiR8, LamboColors, AudiR8Thresholds, 0, 0, true, Color.Red, 250, true, Color.Blue);
                                break;

                            case AnimationType.Supercar:
                                fillerRevDDU(Supercar, AllGreen, SupercarThresholds, 0, 9);
                                double last = (double)Threshold.FourStep4 / 1000;
                                fillerTriggerDDU(Supercar, AllIOrangeRed, (RPM > shiftLight * last), Supercar, true,0, 0, 5);
                                fillerTriggerDDU(Supercar, AllRed, RPM > shiftLight, Supercar, true);
                                fillerTriggerDDU(Supercar, AllRed, !idle && voltage != 0 && (RPM <= idleRPM * 0.6), Supercar, true, 500, 8, 2);
                                break;

                            case AnimationType.F4:
                                fillerRevDDU(F4, F4Colors, F4Thresholds);
                                fillerTriggerDDU(F4, F4Colors, RPM > shiftLight, LeftToRight, true, 250);
                                break;

                            case AnimationType.ToyotaGR86:
                                fillerRevDDU(ToyotaGR, ToyotaGRColors, ToyotaGRThresholds, 0, 0, false, Color.Red, 0, true, Color.Blue);
                                break;

                        }

                        //---------------------TC Active----------------------------

                        fillerTriggerDDU(TCandABS, AllOrange, TCActive, EmptyDDU);

                        //-----------------ABS and braking--------------------------
                        if (animationType == AnimationType.AMGGT3)
                        {
                            fillerTriggerDDU(LeftToRight, AllBlack, brake > 10 && speed > 10, EmptyDDU);
                            fillerTriggerDDU(AMGGT3Braking, AllRed, brake > 30 && speed > 10, EmptyDDU);
                            fillerTriggerDDU(AMGGT3ABS, AllBlue, ABSActive, EmptyDDU);
                        }
                        else
                        {
                            fillerTriggerDDU(TCandABS, AllBlue, ABSActive, EmptyDDU);
                        }

                        //BOOST and OVERTAKE

                        fillerTriggerDDU(TCandABS, AllPurple, p2p, LeftAndRight);

                        //PIT lane and limiter

                        if (voltage > 0 && referenceVoltage > 0 && ignCounter == 0)
                        {
                            if (!isExcempt)
                            {
                                fillerTriggerDDU(TCandABS, AllYellow, pitBoxPosition > 0 && !pitLimiterOn, EmptyDDU);
                                fillerTriggerDDU(TCandABS, AllBlue, pitBoxPosition > 0 && pitLimiterOn, EmptyDDU);
                                fillerLoopDDU(PitNoLim, ref pitCounter, !pitLimiterOn && isInPit && pitBoxPosition == 0 && !pitSpeeding, AllDDULEDs, true);
                                fillerLoopDDU(PitLim, ref pitLimCounter, pitLimiterOn && isInPit && pitBoxPosition == 0, LeftAndRight, true);
                                fillerLoopDDU(PitLimOut, ref pitLimOutCounter, (pitLimiterOn && !isInPit && !pitEntry) || pitSpeeding, AllDDULEDs, true);
                                fillerTriggerDDU(AMGGT3ABS, AllRed, (pitLimiterOn && !isInPit && !pitEntry) || pitSpeeding, LeftToRight, true, 200);
                            }
                            else
                            {
                                switch (animationType)
                                {
                                    case AnimationType.AudiR8:
                                        fillerTriggerDDU(AudiR8, AllBlue, pitLimiterOn, LeftToRight, true, 500, 0, 8);
                                        fillerTriggerDDU(AudiR8, AllRed, pitLimiterOn && !isInPit && !pitEntry, LeftToRight, true, 250, 0, 8);
                                        break;

                                    case AnimationType.LMP2:
                                        fillerTriggerDDU(ThreeTopLeftRight, AllGreen, pitLimiterOn, LeftAndRight, true, 500);
                                        fillerTriggerDDU(ThreeTopLeftRight, AllRed, pitLimiterOn && !isInPit && !pitEntry, LeftAndRight, true, 250);
                                        break;
                                    case AnimationType.LamboGT3:
                                        if (RPM > 300)
                                        {
                                            fillerTriggerDDU(AudiR8, AllBlue, pitLimiterOn, LeftToRight, true, 500, 3, 3);
                                            fillerTriggerDDU(AudiR8, AllRed, pitLimiterOn && !isInPit && !pitEntry, LeftToRight, true, 250, 3, 3);
                                        }
                                        break;
                                }
                            }

                        }

                        //Wheel slip, only left/right relevant
                        if (!(referenceTime.TimeOfDay.TotalMilliseconds % slipBlinkDuration * 2 > slipBlinkDuration))

                       { 
                                double threshold;
                       

                        if (slipLF == 100 || slipRF == 100)
                        {
                            slipRF = 100;
                            slipLF = 100;
                        }
                      

                            if (slipLF > slipRF && slipLF < 60)
                            {
                                for (int i = 2; i < 6; i++)
                                {
                                    threshold = (double)SlipThresholds[i];
                                    if (slipLF > threshold)
                                    {
                                        result[DDUstart + (int)Left[i]] = Color.DarkTurquoise;
                                    }
                                }
                            }
                            else if (slipLF < slipRF && slipRF < 60)
                            {
                                for (int i = 2; i < 6; i++)
                                {
                                    threshold = (double)SlipThresholds[i];
                                    if (slipRF > threshold)
                                    {
                                        result[DDUstart + (int)Right[i]] = Color.DarkTurquoise;
                                    }
                                }
                            }

                            if (slipLF > 60)
                            {
                                for (int i = 2; i < 6; i++)
                                {
                                    threshold = (double)SlipThresholds[i];
                                    if (slipLF > threshold)
                                    {
                                        result[DDUstart + (int)Left[i]] = Color.DarkTurquoise;
                                    }
                                }

                                for (int i = 2; i < 6; i++)
                                {
                                    threshold = (double)SlipThresholds[i];
                                    if (slipRF > threshold)
                                    {
                                        result[DDUstart + (int)Right[i]] = Color.DarkTurquoise;
                                    }
                                }
                            }

                            if (slipRF > 60)
                            {
                                for (int i = 2; i < 6; i++)
                                {
                                    threshold = (double)SlipThresholds[i];
                                    if (slipRF > threshold)
                                    {
                                        result[DDUstart + (int)Right[i]] = Color.DarkTurquoise;
                                    }
                                }

                                for (int i = 2; i < 6; i++)
                                {
                                    threshold = (double)SlipThresholds[i];
                                    if (slipLF > threshold)
                                    {
                                        result[DDUstart + (int)Left[i]] = Color.DarkTurquoise;
                                    }
                                }
                            }
                    }

                    //High power launch

                    if ((highPower && sessionState == 2 || highPower && launchScreen) && !(pitLimiterOn && !isInPit))
                        {
                            fillerTriggerDDU(PowerLaunch, AllYellow, throttle < (launchThrottle - 1), AllDDULEDs, true);
                            fillerTriggerDDU(PowerLaunch, AllGreen, throttle > (launchThrottle - 1) && throttle < (launchThrottle + 1), AllDDULEDs, true);
                            fillerTriggerDDU(PowerLaunch, AllRed, throttle > (launchThrottle + 1), AllDDULEDs, true);
                        }


                        //Engine warnings
                        fillerTriggerDDU(Right, AllRed, waterTemp && voltage != 0, EmptyDDU, false, 250, 5, 0);
                        //Neutral warning
                        fillerTriggerDDU(ThreeTopLeftRight, AllOrange, neutralButtonSW1, LeftAndRight, true, 100);

                        //SPOTTER

                        fillerTriggerDDU(Left, AllBlack, opponentPassingLeft > -1, EmptyDDU);
                        fillerTriggerDDU(Left, AllBlack, opponentPassingRight > -1, EmptyDDU);
                        fillerTriggerDDU(Left, AllRed, spotLeft, Left, true, 200);
                        fillerTriggerDDU(Right, AllRed, spotRight, Right, true, 200);

                        if (spotLeft)
                        {
                            for (int i = 0; i < SpotLeft.Count; i++)
                            {
                                if (i == opponentPassingLeft)
                                {
                                    for (int e = 0; e < SpotLeft[i].AniPattern.Count; e++)
                                    {
                                        result[DDUstart + (int)SpotLeft[i].AniPattern[e]] = SpotLeft[i].AniColor[e];
                                    }
                                    break;
                                }
                            }
                        }
                        if (spotRight)
                        {
                            for (int i = 0; i < SpotRight.Count; i++)
                            {
                                if (i == opponentPassingRight)
                                {
                                    for (int e = 0; e < SpotRight[i].AniPattern.Count; e++)
                                    {
                                        result[DDUstart + (int)SpotRight[i].AniPattern[e]] = SpotRight[i].AniColor[e];
                                    }
                                    break;
                                }
                            }
                        }



                        //Animations
                        fillerAnimationDDU(Ignition, ref ignCounter, voltage == 0, AllDDULEDs, true);
                        fillerAnimationDDU(PitEntry, ref pitEntryCounter, !pitLimiterOn || pitSpeeding, AllDDULEDs, true);
                        fillerLoopDDU(Radio, ref radioCounter, radioName.Length > 0, LeftAndRight, true);
                    }

                    //----------------------------------
                    //---------------SW1----------------
                    //----------------------------------

                    if (SW1Enabled)
                    {
                        //Ignition

                        fillerAnimationSW1(IgnitionSW1, ref ignCounterSW1, voltage == 0, IgnSW1, true);

                        //Pit lane and limiter
                        if (voltage == 0)
                        {
                            result[SW1start + (int)PatternSW1.SWTOPLEFT] = Color.Red;
                        }
                        else
                        {
                            fillerTriggerSW1(AllSW1LEDs, AllYellow, !pitLimiterOn && isInPit, EmptySW1, false, 750, 1, 2);
                            fillerTriggerSW1(AllSW1LEDs, AllBlue, (pitLimiterOn && isInPit) || pitEntry, EmptySW1, false, 0, 1, 2);
                            fillerTriggerSW1(AllSW1LEDs, AllRed, (pitLimiterOn && !isInPit && !pitEntry) || pitSpeeding, EmptySW1, false, 200, 1, 2);
                            fillerTriggerSW1(AllSW1LEDs, AllOrange, launchScreen, EmptySW1, false, 500, 0, 3);
                            fillerTriggerSW1(AllSW1LEDs, AllBlue, radio, EmptySW1, false);

                            fillerTriggerSW1(SW1Tops, AllOrange, TCActive, EmptySW1, false);
                            fillerTriggerSW1(SW1Tops, AllBlue, ABSActive, EmptySW1, false);
                            fillerTriggerSW1(AllSW1LEDs, AllDarkTurquoise, slipRF > 40, EmptySW1, false, 0, 1, 2);
                            fillerTriggerSW1(AllSW1LEDs, AllDarkTurquoise, slipLF > 40, EmptySW1, false, 0, 2, 1);

                            fillerLoopSW1(BiteSetting1, ref biteCounter1, biteSettingSW1 == 1, AllSW1LEDs, true);
                            fillerLoopSW1(BiteSetting2, ref biteCounter2, biteSettingSW1 == 2, AllSW1LEDs, true);
                            fillerLoopSW1(BiteSetting3, ref biteCounter3, biteSettingSW1 == 3, AllSW1LEDs, true);

                            fillerLoopSW1(PitNoLimSW1, ref pitSW1Counter, !pitLimiterOn && isInPit && pitBoxPosition == 0 && !pitSpeeding, AllSW1LEDs, true);



                            fillerTriggerSW1(SW1Tops, AllOrange, neutralButtonSW1, SW1Tops, true, 100);

                        }

                        //BOOST and Overtake

                        fillerTriggerSW1(SW1Tops, AllPurple, p2p, AllSW1LEDs);
                        fillerTriggerSW1(SW1Tops, AllGreen, DRSState == "Open", AllSW1LEDs);

                        //Spotter calls
                        fillerTriggerSW1(AllSW1LEDs, AllRed, opponentPassingLeft > -1, EmptySW1, true, 200, 2, 0);
                        fillerTriggerSW1(AllSW1LEDs, AllRed, opponentPassingRight > -1, EmptySW1, true, 200, 0, 2);

                        fillerTriggerSW1(AllSW1LEDs, AllGreen, opponentPassingLeft > 4, EmptySW1, false, 0, 2, 1);
                        fillerTriggerSW1(AllSW1LEDs, AllGreen, opponentPassingLeft < 2 && opponentPassingLeft > -1, EmptySW1, false, 0, 3, 0);

                        fillerTriggerSW1(AllSW1LEDs, AllGreen, opponentPassingRight > 4, EmptySW1, false, 0, 1, 3);
                        fillerTriggerSW1(AllSW1LEDs, AllGreen, opponentPassingRight < 2 && opponentPassingRight > -1, EmptySW1, false, 0, 0, 4);

                    }


                    //Obtaining trigger info
                    if (!pitLimiterOn && isInPit && pitBoxPosition == 0 && pitCounter == 0)
                    {
                        pitCounter++;
                    }
                    if (pitEntry && pitEntryCounter == 0)
                    {
                        pitEntryCounter++;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// User control for settings editing.
        /// </summary>
        public override UserControl MainLedsEditor
        {
            get
            {
                return new DahlLedEffectEditControl { DataContext = this };
            }
        }

        /// <summary>
        /// Returns a status indicating if the whole effect is currently active.
        /// </summary>
        /// <param name="data">Game data.</param>
        /// <returns></returns>
        protected override bool IsActive(LedsGameData data)
        {
            return true;
        }

        /// <summary>
        /// Not used when MainLedsEditor is used
        /// </summary>
        /// <returns>null</returns>
        protected override UserControl GetEditControl()
        {
            return null;
        }
    }
}
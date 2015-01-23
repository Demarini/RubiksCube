using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace Cube.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        string currentCube = "";
        int totalMoves = 0;
        private string _tlBack;
        private string _tmBack;
        private string _trBack;
        private string _mlBack;
        private string _mmBack;
        private string _mrBack;
        private string _blBack;
        private string _bmBack;
        private string _brBack;
        private string[,] front = new string[3, 3];
        private string[,] left = new string[3, 3];
        private string[,] right = new string[3, 3];
        private string[,] back = new string[3, 3];
        private string[,] bottom = new string[3, 3];
        private string[,] top = new string[3, 3];
        private string currentFace = "front";
        int lineupcenter = 0;
        private System.Windows.Visibility colorVisibility = System.Windows.Visibility.Hidden;
        public MainWindowViewModel()
        {
            ClearCube();
            ShowFace(front);
        }

        public void SolveCube()
        {
            totalMoves = 0;
            Stopwatch t = new Stopwatch();
            t.Start();
            List<string> crossPieces = FindCrossPieces();
            List<string> openCross = FindOpenCross();
            //if (lineupcenter == 0)
            //{
            //    PlaceCrossPieces(openCross, crossPieces, 0);
            //    lineupcenter++;
            //}
            //else
            //{
            //    LineUpCenter();
            //}
            PlaceCrossPieces(openCross, crossPieces, 0);
            int k = totalMoves;
            LineUpCenter();
            t.Stop();
            long totalTime = t.ElapsedMilliseconds;
            int j = totalMoves;
            FillInCorners();
            FillSecondLayer();
            CreateFinalCross();
            LineUpCenter2();
            FillLastCorners();
            FinishCube();
        }
        public void FinishCube()
        {
            while (front[0, 2] != "White")
            {
                if (front[0, 2] != "White")
                {
                    LeftInverted();
                    Top();
                    Left();
                    TopInverted();
                }
            }
            BottomInverted();
            while (front[0, 2] != "Orange")
            {
                if (front[0, 2] != "Orange")
                {
                    LeftInverted();
                    Top();
                    Left();
                    TopInverted();
                }
            }
            BottomInverted();
            while (front[0, 2] != "Yellow")
            {
                if (front[0, 2] != "Yellow")
                {
                    LeftInverted();
                    Top();
                    Left();
                    TopInverted();
                }
            }
            BottomInverted();
            while (front[0, 2] != "Red")
            {
                if (front[0, 2] != "Red")
                {
                    LeftInverted();
                    Top();
                    Left();
                    TopInverted();
                }
            }
            int interval = 0;
            while (front[2, 2] != "White" || interval > 1000)
            {
                interval++;
                if (front[2, 2] != "White")
                {
                    BottomInverted();
                }
            }
        }
        
        public void FillLastCorners()
        {
            bool checkCorners = CheckCornerPlacement();
            while (!checkCorners)
            {
                checkCorners = CheckCornerPlacement();
            }
        }
        public void ShuffleCorners(string face)
        {
            switch (face)
            {
                case "front":
                    Bottom();
                    Left();
                    BottomInverted();
                    RightInverted();
                    Bottom();
                    LeftInverted();
                    BottomInverted();
                    Right();
                    break;
                case "back":
                    Bottom();
                    Right();
                    BottomInverted();
                    LeftInverted();
                    Bottom();
                    RightInverted();
                    BottomInverted();
                    Left();
                    break;
                case "left":
                    Bottom();
                    Back();
                    BottomInverted();
                    FrontInverted();
                    Bottom();
                    BackInverted();
                    BottomInverted();
                    Front();
                    break;
                case "right":
                    Bottom();
                    Front();
                    BottomInverted();
                    BackInverted();
                    Bottom();
                    FrontInverted();
                    BottomInverted();
                    Back();
                    break;
            }
        }
        public bool CheckCornerPlacement()
        {
            string whiteRedCorner = front[0, 2] + "|" + left[2, 2] + "|" + bottom[0, 0];
            string[] whiteRedCornerSplit = whiteRedCorner.Split('|');

            string whiteOrangeCorner = front[2, 2] + "|" + right[0, 2] + "|" + bottom[2, 0];
            string[] whiteOrangeCornerSplit = whiteOrangeCorner.Split('|');

            string yellowOrangeCorner = back[2, 0] + "|" + right[2, 2] + "|" + bottom[2, 2];
            string[] yellowOrangeCornerSplit = yellowOrangeCorner.Split('|');

            string yellowRedCorner = back[0, 0] + "|" + left[0, 2] + "|" + bottom[0, 2];
            string[] yellowRedCornerSplit = yellowRedCorner.Split('|');

            if (whiteRedCornerSplit.Contains("Red") && whiteRedCornerSplit.Contains("Blue") && whiteRedCornerSplit.Contains("White"))
            {
                if (whiteOrangeCornerSplit.Contains("Orange") && whiteOrangeCornerSplit.Contains("Blue") && whiteOrangeCornerSplit.Contains("White"))
                {
                    if (yellowOrangeCornerSplit.Contains("Orange") && yellowOrangeCornerSplit.Contains("Blue") && yellowOrangeCornerSplit.Contains("Yellow"))
                    {
                        if (yellowRedCornerSplit.Contains("Red") && yellowRedCornerSplit.Contains("Blue") && yellowRedCornerSplit.Contains("Yellow"))
                        {
                            return true;
                        }
                        else
                        {
                            ShuffleCorners("front");
                        }
                    }
                    else
                    {
                        ShuffleCorners("front");
                    }
                }
                else
                {
                    ShuffleCorners("front");
                }
            }
            else if (whiteOrangeCornerSplit.Contains("Orange") && whiteOrangeCornerSplit.Contains("Blue") && whiteOrangeCornerSplit.Contains("White"))
            {
                if (whiteRedCornerSplit.Contains("Red") && whiteRedCornerSplit.Contains("Blue") && whiteRedCornerSplit.Contains("White"))
                {
                    if (yellowOrangeCornerSplit.Contains("Orange") && yellowOrangeCornerSplit.Contains("Blue") && yellowOrangeCornerSplit.Contains("Yellow"))
                    {
                        if (yellowRedCornerSplit.Contains("Red") && yellowRedCornerSplit.Contains("Blue") && yellowRedCornerSplit.Contains("Yellow"))
                        {
                            return true;
                        }
                        else
                        {
                            ShuffleCorners("right");
                        }
                    }
                    else
                    {
                        ShuffleCorners("right");
                    }
                }
                else
                {
                    ShuffleCorners("right");
                }
            }
            else if (yellowOrangeCornerSplit.Contains("Orange") && yellowOrangeCornerSplit.Contains("Blue") && yellowOrangeCornerSplit.Contains("Yellow"))
            {
                if (whiteRedCornerSplit.Contains("Red") && whiteRedCornerSplit.Contains("Blue") && whiteRedCornerSplit.Contains("White"))
                {
                    if (whiteOrangeCornerSplit.Contains("Orange") && whiteOrangeCornerSplit.Contains("Blue") && whiteOrangeCornerSplit.Contains("White"))
                    {
                        if (yellowRedCornerSplit.Contains("Red") && yellowRedCornerSplit.Contains("Blue") && yellowRedCornerSplit.Contains("Yellow"))
                        {
                            return true;
                        }
                        else
                        {
                            ShuffleCorners("back");
                        }
                    }
                    else
                    {
                        ShuffleCorners("back");
                    }
                }
                else
                {
                    ShuffleCorners("back");
                }
            }
            else if (yellowRedCornerSplit.Contains("Red") && yellowRedCornerSplit.Contains("Blue") && yellowRedCornerSplit.Contains("Yellow"))
            {
                if (whiteRedCornerSplit.Contains("Red") && whiteRedCornerSplit.Contains("Blue") && whiteRedCornerSplit.Contains("White"))
                {
                    if (whiteOrangeCornerSplit.Contains("Orange") && whiteOrangeCornerSplit.Contains("Blue") && whiteOrangeCornerSplit.Contains("White"))
                    {
                        if (yellowOrangeCornerSplit.Contains("Orange") && yellowOrangeCornerSplit.Contains("Blue") && yellowOrangeCornerSplit.Contains("Yellow"))
                        {
                            return true;
                        }
                        else
                        {
                            ShuffleCorners("left");
                        }
                    }
                    else
                    {
                        ShuffleCorners("left");
                    }
                }
                else
                {
                    ShuffleCorners("left");
                }
            }
            else
            {
                ShuffleCorners("front");
            }
            return false;
        }
        public void CreateFinalCross()
        {
            bool finalCross = CheckFinalCross();
            while (!finalCross)
            {
                string face = DetectAlgorithmFace();
                FillLastCross(face);
                finalCross = CheckFinalCross();
            }
        }
        public void FillLastCross(string face)
        {
            if (face == "front")
            {
                Front();
                Left();
                Bottom();
                LeftInverted();
                BottomInverted();
                FrontInverted();
            }
            else if (face == "back")
            {
                Back();
                Right();
                Bottom();
                RightInverted();
                BottomInverted();
                BackInverted();
            }
            else if (face == "left")
            {
                Left();
                Back();
                Bottom();
                BackInverted();
                BottomInverted();
                LeftInverted();
            }
            else if (face == "right")
            {
                Right();
                Front();
                Bottom();
                FrontInverted();
                BottomInverted();
                RightInverted();
            }
            else if (face == "frontsingledot")
            {
                Front();
                Left();
                Bottom();
                LeftInverted();
                BottomInverted();
                FrontInverted();
            }
        }
        public string DetectAlgorithmFace()
        {
            if ((bottom[0, 1] == "Blue" && bottom[1, 0] == "Blue") || (bottom[2, 1] == "Blue" && bottom[0, 1] == "Blue"))
            {
                return "front";
            }
            if ((bottom[2, 1] == "Blue" && bottom[1, 0] == "Blue") || (bottom[1, 2] == "Blue" && bottom[1, 0] == "Blue"))
            {
                return "left";
            }
            if ((bottom[0, 1] == "Blue" && bottom[1, 2] == "Blue"))
            {
                return "right";
            }
            if ((bottom[1, 2] == "Blue" && bottom[2, 1] == "Blue"))
            {
                return "back";
            }
            return "frontsingledot";
        }
        public bool CheckFinalCross()
        {
            if (bottom[1, 0] == "Blue" && bottom[2, 1] == "Blue" && bottom[0, 1] == "Blue" && bottom[1, 2] == "Blue")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void FillSecondLayer()
        {
            bool isFilled = DetectSecondLayerFilled();
            List<string> detectedCubes = DetectSecondLayerCubes();
            int iterations = 0;
            while (!isFilled && iterations < 1000)
            {
                iterations++;
                if (detectedCubes.Count != 0)
                {
                    FillLayers(detectedCubes);
                }
                detectedCubes = DetectSecondLayerCubes();
                isFilled = DetectSecondLayerFilled();
                if (isFilled == false && detectedCubes.Count == 0)
                {
                    FindAnomaly();
                    detectedCubes = DetectSecondLayerCubes();
                }
            }
        }
        public void FindAnomaly()
        {
            if (front[0, 1] != "White" && left[2, 1] != "Red")
            {
                Bottom();
                Left();
                BottomInverted();
                LeftInverted();
                BottomInverted();
                FrontInverted();
                Bottom();
                Front();
            }
            else if (front[2, 1] != "White" && right[0, 1] != "Orange")
            {
                BottomInverted();
                RightInverted();
                Bottom();
                Right();
                Bottom();
                Front();
                BottomInverted();
                FrontInverted();
            }
            else if (left[0, 1] != "Red" && back[0, 1] != "Yellow")
            {
                Bottom();
                Back();
                BottomInverted();
                BackInverted();
                BottomInverted();
                LeftInverted();
                Bottom();
                Left();
            }
            else if (right[2, 1] != "Orange" && back[2, 1] != "Yellow")
            {
                Bottom();
                Right();
                BottomInverted();
                RightInverted();
                BottomInverted();
                BackInverted();
                Bottom();
                Back();
            }
        }
        public void FillLayers(List<string> detectCubes)
        {
            detectCubes[0].Split('|');
            string face = detectCubes[0].Split('|')[0].Split('_')[0];
            string faceColor = detectCubes[0].Split('|')[0].Split('_')[1];
            string bottomColor = detectCubes[0].Split('|')[1].Split('_')[1];
            switch (face)
            {
                case "front":
                    switch (faceColor)
                    {
                        case "White":
                            switch (bottomColor)
                            {
                                case "Red":
                                    //whitered
                                    Bottom();
                                    Left();
                                    BottomInverted();
                                    LeftInverted();
                                    BottomInverted();
                                    FrontInverted();
                                    Bottom();
                                    Front();
                                    break;
                                case "Orange":
                                    //whiteorange
                                    BottomInverted();
                                    RightInverted();
                                    Bottom();
                                    Right();
                                    Bottom();
                                    Front();
                                    BottomInverted();
                                    FrontInverted();
                                    break;
                            }
                            break;
                        case "Red":
                            BottomInverted();
                            switch (bottomColor)
                            {
                                case "White":
                                    //redwhite
                                    BottomInverted();
                                    FrontInverted();
                                    Bottom();
                                    Front();
                                    Bottom();
                                    Left();
                                    BottomInverted();
                                    LeftInverted();
                                    break;
                                case "Yellow":
                                    //redyellow
                                    Bottom();
                                    Back();
                                    BottomInverted();
                                    BackInverted();
                                    BottomInverted();
                                    LeftInverted();
                                    Bottom();
                                    Left();
                                    break;
                            }
                            break;
                        case "Yellow":
                            BottomInverted();
                            BottomInverted();
                            switch (bottomColor)
                            {
                                case "Red":
                                    //yellowred
                                    BottomInverted();
                                    LeftInverted();
                                    Bottom();
                                    Left();
                                    Bottom();
                                    Back();
                                    BottomInverted();
                                    BackInverted();
                                    break;
                                case "Orange":
                                    //yelloworange
                                    Bottom();
                                    Right();
                                    BottomInverted();
                                    RightInverted();
                                    BottomInverted();
                                    BackInverted();
                                    Bottom();
                                    Back();
                                    break;
                            }
                            break;
                        case "Orange":
                            Bottom();
                            switch (bottomColor)
                            {
                                case "Yellow":
                                    //orangeyellow
                                    BottomInverted();
                                    BackInverted();
                                    Bottom();
                                    Back();
                                    Bottom();
                                    Right();
                                    BottomInverted();
                                    RightInverted();
                                    break;
                                case "White":
                                    //orangewhite
                                    Bottom();
                                    Front();
                                    BottomInverted();
                                    FrontInverted();
                                    BottomInverted();
                                    RightInverted();
                                    Bottom();
                                    Right();
                                    break;
                            }
                            break;
                    }
                    break;
                case "left":
                    switch (faceColor)
                    {
                        case "White":
                            Bottom();
                            switch (bottomColor)
                            {
                                case "Red":
                                    //whitered
                                    Bottom();
                                    Left();
                                    BottomInverted();
                                    LeftInverted();
                                    BottomInverted();
                                    FrontInverted();
                                    Bottom();
                                    Front();
                                    break;
                                case "Orange":
                                    //whiteorange
                                    BottomInverted();
                                    RightInverted();
                                    Bottom();
                                    Right();
                                    Bottom();
                                    Front();
                                    BottomInverted();
                                    FrontInverted();
                                    break;
                            }
                            break;
                        case "Red":
                            switch (bottomColor)
                            {
                                case "White":
                                    //redwhite
                                    BottomInverted();
                                    FrontInverted();
                                    Bottom();
                                    Front();
                                    Bottom();
                                    Left();
                                    BottomInverted();
                                    LeftInverted();
                                    break;
                                case "Yellow":
                                    //redyellow
                                    Bottom();
                                    Back();
                                    BottomInverted();
                                    BackInverted();
                                    BottomInverted();
                                    LeftInverted();
                                    Bottom();
                                    Left();
                                    break;
                            }
                            break;
                        case "Yellow":
                            BottomInverted();
                            switch (bottomColor)
                            {
                                case "Red":
                                    //yellowred
                                    BottomInverted();
                                    LeftInverted();
                                    Bottom();
                                    Left();
                                    Bottom();
                                    Back();
                                    BottomInverted();
                                    BackInverted();
                                    break;
                                case "Orange":
                                    //yelloworange
                                    Bottom();
                                    Right();
                                    BottomInverted();
                                    RightInverted();
                                    BottomInverted();
                                    BackInverted();
                                    Bottom();
                                    Back();
                                    break;
                            }
                            break;
                        case "Orange":
                            Bottom();
                            Bottom();
                            switch (bottomColor)
                            {
                                case "Yellow":
                                    //orangeyellow
                                    BottomInverted();
                                    BackInverted();
                                    Bottom();
                                    Back();
                                    Bottom();
                                    Right();
                                    BottomInverted();
                                    RightInverted();
                                    break;
                                case "White":
                                    //orangewhite
                                    Bottom();
                                    Front();
                                    BottomInverted();
                                    FrontInverted();
                                    BottomInverted();
                                    RightInverted();
                                    Bottom();
                                    Right();
                                    break;
                            }
                            break;
                    }
                    break;
                case "back":
                    switch (faceColor)
                    {
                        case "White":
                            Bottom();
                            Bottom();
                            switch (bottomColor)
                            {
                                case "Red":
                                    //whitered
                                    Bottom();
                                    Left();
                                    BottomInverted();
                                    LeftInverted();
                                    BottomInverted();
                                    FrontInverted();
                                    Bottom();
                                    Front();
                                    break;
                                case "Orange":
                                    //whiteorange
                                    BottomInverted();
                                    RightInverted();
                                    Bottom();
                                    Right();
                                    Bottom();
                                    Front();
                                    BottomInverted();
                                    FrontInverted();
                                    break;
                            }
                            break;
                        case "Red":
                            Bottom();
                            switch (bottomColor)
                            {
                                case "White":
                                    //redwhite
                                    BottomInverted();
                                    FrontInverted();
                                    Bottom();
                                    Front();
                                    Bottom();
                                    Left();
                                    BottomInverted();
                                    LeftInverted();
                                    break;
                                case "Yellow":
                                    //redyellow
                                    Bottom();
                                    Back();
                                    BottomInverted();
                                    BackInverted();
                                    BottomInverted();
                                    LeftInverted();
                                    Bottom();
                                    Left();
                                    break;
                            }
                            break;
                        case "Yellow":
                            switch (bottomColor)
                            {
                                case "Red":
                                    //yellowred
                                    BottomInverted();
                                    LeftInverted();
                                    Bottom();
                                    Left();
                                    Bottom();
                                    Back();
                                    BottomInverted();
                                    BackInverted();
                                    break;
                                case "Orange":
                                    //yelloworange
                                    Bottom();
                                    Right();
                                    BottomInverted();
                                    RightInverted();
                                    BottomInverted();
                                    BackInverted();
                                    Bottom();
                                    Back();
                                    break;
                            }
                            break;
                        case "Orange":
                            BottomInverted();
                            switch (bottomColor)
                            {
                                case "Yellow":
                                    //orangeyellow
                                    BottomInverted();
                                    BackInverted();
                                    Bottom();
                                    Back();
                                    Bottom();
                                    Right();
                                    BottomInverted();
                                    RightInverted();
                                    break;
                                case "White":
                                    //orangewhite
                                    Bottom();
                                    Front();
                                    BottomInverted();
                                    FrontInverted();
                                    BottomInverted();
                                    RightInverted();
                                    Bottom();
                                    Right();
                                    break;
                            }
                            break;
                    }
                    break;
                case "right":
                    switch (faceColor)
                    {
                        case "White":
                            BottomInverted();
                            switch (bottomColor)
                            {
                                case "Red":
                                    //whitered
                                    Bottom();
                                    Left();
                                    BottomInverted();
                                    LeftInverted();
                                    BottomInverted();
                                    FrontInverted();
                                    Bottom();
                                    Front();
                                    break;
                                case "Orange":
                                    //whiteorange
                                    BottomInverted();
                                    RightInverted();
                                    Bottom();
                                    Right();
                                    Bottom();
                                    Front();
                                    BottomInverted();
                                    FrontInverted();
                                    break;
                            }
                            break;
                        case "Red":
                            BottomInverted();
                            BottomInverted();
                            switch (bottomColor)
                            {
                                case "White":
                                    //redwhite
                                    BottomInverted();
                                    FrontInverted();
                                    Bottom();
                                    Front();
                                    Bottom();
                                    Left();
                                    BottomInverted();
                                    LeftInverted();
                                    break;
                                case "Yellow":
                                    //redyellow
                                    Bottom();
                                    Back();
                                    BottomInverted();
                                    BackInverted();
                                    BottomInverted();
                                    LeftInverted();
                                    Bottom();
                                    Left();
                                    break;
                            }
                            break;
                        case "Yellow":
                            Bottom();
                            switch (bottomColor)
                            {
                                case "Red":
                                    //yellowred
                                    BottomInverted();
                                    LeftInverted();
                                    Bottom();
                                    Left();
                                    Bottom();
                                    Back();
                                    BottomInverted();
                                    BackInverted();
                                    break;
                                case "Orange":
                                    //yelloworange
                                    Bottom();
                                    Right();
                                    BottomInverted();
                                    RightInverted();
                                    BottomInverted();
                                    BackInverted();
                                    Bottom();
                                    Back();
                                    break;
                            }
                            break;
                        case "Orange":
                            switch (bottomColor)
                            {
                                case "Yellow":
                                    //orangeyellow
                                    BottomInverted();
                                    BackInverted();
                                    Bottom();
                                    Back();
                                    Bottom();
                                    Right();
                                    BottomInverted();
                                    RightInverted();
                                    break;
                                case "White":
                                    //orangewhite
                                    Bottom();
                                    Front();
                                    BottomInverted();
                                    FrontInverted();
                                    BottomInverted();
                                    RightInverted();
                                    Bottom();
                                    Right();
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }
        public bool DetectSecondLayerFilled()
        {
            bool isFilled = true;
            if (front[0, 1] == "White" && left[2, 1] == "Red")
            {
                if (front[2, 1] == "White" && right[0, 1] == "Orange")
                {
                    if (right[2, 1] == "Orange" && back[2, 1] == "Yellow")
                    {
                        if (left[0, 1] == "Red" && back[0, 1] == "Yellow")
                        {
                            isFilled = true;
                        }
                        else
                        {
                            isFilled = false;
                        }
                    }
                    else
                    {
                        isFilled = false;
                    }
                }
                else
                {
                    isFilled = false;
                }
            }
            else
            {
                isFilled = false;
            }
            return isFilled;
        }
        public List<string> DetectSecondLayerCubes()
        {
            List<string> detectedCubes = new List<string>();
            if (front[1, 2] != "Blue" && bottom[1, 0] != "Blue")
            {
                detectedCubes.Add("front_" + front[1, 2] + "|" + "bottom_" + bottom[1, 0]);
            }
            if (left[1, 2] != "Blue" && bottom[0, 1] != "Blue")
            {
                detectedCubes.Add("left_" + left[1, 2] + "|" + "bottom_" + bottom[0, 1]);
            }
            if (right[1, 2] != "Blue" && bottom[2, 1] != "Blue")
            {
                detectedCubes.Add("right_" + right[1, 2] + "|" + "bottom_" + bottom[2, 1]);
            }
            if (back[1, 0] != "Blue" && bottom[1, 2] != "Blue")
            {
                detectedCubes.Add("back_" + back[1, 0] + "|" + "bottom_" + bottom[1, 2]);
            }
            return detectedCubes;
        }
        public void FillInCorners()
        {
            List<string> cornersList = new List<string>();
            cornersList = GetGreenCorners();
            while (cornersList.Count != 0)
            {
                string[] colors;
                for (int i = 0; i < cornersList.Count; i++)
                {
                    string[] splitFace = cornersList[i].Split('_');
                    switch (splitFace[0])
                    {
                        case "top00":
                            colors = splitFace[1].Split('|');
                            if (colors.Contains("Yellow") && colors.Contains("Red"))
                            {
                                cornersList.RemoveAt(i);
                            }
                            break;
                        case "top20":
                            colors = splitFace[1].Split('|');
                            if (colors.Contains("Yellow") && colors.Contains("Orange"))
                            {
                                cornersList.RemoveAt(i);
                            }
                            break;
                        case "top02":
                            colors = splitFace[1].Split('|');
                            if (colors.Contains("White") && colors.Contains("Red"))
                            {
                                cornersList.RemoveAt(i);
                            }
                            break;
                        case "top22":
                            colors = splitFace[1].Split('|');
                            if (colors.Contains("White") && colors.Contains("Orange"))
                            {
                                cornersList.RemoveAt(i);
                            }
                            break;
                    }
                }
                if (cornersList.Count != 0)
                {
                    string[] cornerLocation = cornersList[0].Split('_');
                    colors = cornerLocation[1].Split('|');
                    switch (cornerLocation[0])
                    {
                        case "front00":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                FrontInverted();
                                BottomInverted();
                                Front();
                                BottomInverted();
                                BottomInverted();
                                RightInverted();
                                BottomInverted();
                                Right();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                FrontInverted();
                                Bottom();
                                Bottom();
                                BackInverted();
                                BottomInverted();
                                Back();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                FrontInverted();
                                BottomInverted();
                                Front();
                                Bottom();
                                FrontInverted();
                                BottomInverted();
                                Front();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                FrontInverted();
                                BottomInverted();
                                Front();
                                LeftInverted();
                                BottomInverted();
                                Left();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "front20":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                Front();
                                Bottom();
                                FrontInverted();
                                BottomInverted();
                                Front();
                                Bottom();
                                FrontInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                Front();
                                Bottom();
                                FrontInverted();
                                Right();
                                Bottom();
                                RightInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                Front();
                                BottomInverted();
                                BottomInverted();
                                FrontInverted();
                                Bottom();
                                Left();
                                Bottom();
                                LeftInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                Front();
                                BottomInverted();
                                BottomInverted();
                                FrontInverted();
                                Back();
                                Bottom();
                                BackInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "front02":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                Bottom();
                                RightInverted();
                                BottomInverted();
                                Right();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                Bottom();
                                Bottom();
                                BackInverted();
                                BottomInverted();
                                Back();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                FrontInverted();
                                BottomInverted();
                                Front();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                BottomInverted();
                                LeftInverted();
                                BottomInverted();
                                Left();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "front22":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                Front();
                                Bottom();
                                FrontInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                Bottom();
                                Right();
                                Bottom();
                                RightInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                BottomInverted();
                                Left();
                                Bottom();
                                LeftInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                BottomInverted();
                                BottomInverted();
                                Back();
                                Bottom();
                                BackInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "back00":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                Bottom();
                                Bottom();
                                //Front22 Placement
                                Front();
                                Bottom();
                                FrontInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                BottomInverted();
                                //Right22 Placement
                                Right();
                                Bottom();
                                RightInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                Bottom();
                                //Left22 Placement
                                Left();
                                Bottom();
                                LeftInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                //Back00 Placement
                                Back();
                                Bottom();
                                BackInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "back20":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                BottomInverted();
                                //Right02 Placement
                                RightInverted();
                                BottomInverted();
                                Right();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                //Back20 Placement
                                BackInverted();
                                BottomInverted();
                                Back();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                BottomInverted();
                                BottomInverted();
                                //Front02 Placement
                                FrontInverted();
                                BottomInverted();
                                Front();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                Bottom();
                                //Left02 Placement
                                LeftInverted();
                                BottomInverted();
                                Left();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "back02":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                Back();
                                BottomInverted();
                                BottomInverted();
                                BackInverted();
                                //front22
                                Front();
                                Bottom();
                                FrontInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                Back();
                                BottomInverted();
                                BottomInverted();
                                BackInverted();
                                Bottom();
                                //right22
                                Right();
                                Bottom();
                                RightInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                Back();
                                Bottom();
                                BackInverted();
                                //left22
                                Left();
                                Bottom();
                                LeftInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                Back();
                                Bottom();
                                BackInverted();
                                BottomInverted();
                                //Back00
                                Back();
                                Bottom();
                                BackInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "back22":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                BackInverted();
                                BottomInverted();
                                Back();
                                //Right02
                                RightInverted();
                                BottomInverted();
                                Right();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                BackInverted();
                                BottomInverted();
                                Back();
                                Bottom();
                                //back20
                                BackInverted();
                                BottomInverted();
                                Back();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                BackInverted();
                                BottomInverted();
                                BottomInverted();
                                Back();
                                //front02
                                FrontInverted();
                                BottomInverted();
                                Front();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                BackInverted();
                                Bottom();
                                Bottom();
                                Back();
                                BottomInverted();
                                //left02
                                LeftInverted();
                                BottomInverted();
                                Left();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "left00":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                LeftInverted();
                                Bottom();
                                Bottom();
                                Left();
                                //right00
                                RightInverted();
                                BottomInverted();
                                Right();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                LeftInverted();
                                BottomInverted();
                                Left();
                                //back20
                                BackInverted();
                                BottomInverted();
                                Back();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                LeftInverted();
                                Bottom();
                                Bottom();
                                Left();
                                BottomInverted();
                                //front02
                                FrontInverted();
                                BottomInverted();
                                Front();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                LeftInverted();
                                BottomInverted();
                                Left();
                                Bottom();
                                //left02
                                LeftInverted();
                                BottomInverted();
                                Left();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "left20":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                Left();
                                Bottom();
                                LeftInverted();
                                //front22
                                Front();
                                Bottom();
                                FrontInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                Left();
                                Bottom();
                                Bottom();
                                LeftInverted();
                                //right22
                                Right();
                                Bottom();
                                RightInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                Left();
                                Bottom();
                                LeftInverted();
                                Bottom();
                                //left22
                                Left();
                                Bottom();
                                LeftInverted();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                Left();
                                Bottom();
                                LeftInverted();
                                BottomInverted();
                                BottomInverted();
                                //back00
                                Back();
                                Bottom();
                                BackInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "left02":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                Bottom();
                                Bottom();
                                //right02
                                RightInverted();
                                BottomInverted();
                                Right();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                BottomInverted();
                                //back20
                                BackInverted();
                                BottomInverted();
                                Back();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                Bottom();
                                //front02
                                FrontInverted();
                                BottomInverted();
                                Front();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                //left02
                                LeftInverted();
                                BottomInverted();
                                Left();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "left22":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                Bottom();
                                //front22
                                Front();
                                Bottom();
                                FrontInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                Bottom();
                                Bottom();
                                //right22
                                Right();
                                Bottom();
                                RightInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                Left();
                                Bottom();
                                LeftInverted();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                BottomInverted();
                                //back00
                                Back();
                                Bottom();
                                BackInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "right00":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                RightInverted();
                                BottomInverted();
                                Right();
                                Bottom();
                                //right02
                                RightInverted();
                                BottomInverted();
                                Right();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                RightInverted();
                                BottomInverted();
                                Right();
                                Bottom();
                                Bottom();
                                //back20
                                BackInverted();
                                BottomInverted();
                                Back();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                RightInverted();
                                BottomInverted();
                                Right();
                                //front02
                                FrontInverted();
                                BottomInverted();
                                Front();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                RightInverted();
                                BottomInverted();
                                Right();
                                BottomInverted();
                                //left02
                                LeftInverted();
                                BottomInverted();
                                Left();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "right20":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                Right();
                                BottomInverted();
                                BottomInverted();
                                RightInverted();
                                Bottom();
                                //front22
                                Front();
                                Bottom();
                                FrontInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                Right();
                                Bottom();
                                RightInverted();
                                BottomInverted();
                                //right22
                                Right();
                                Bottom();
                                RightInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                Right();
                                Bottom();
                                RightInverted();
                                Bottom();
                                //left22
                                Left();
                                Bottom();
                                LeftInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                Right();
                                Bottom();
                                RightInverted();
                                //back00
                                Back();
                                Bottom();
                                BackInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "right02":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                RightInverted();
                                BottomInverted();
                                Right();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                Bottom();
                                //back20
                                BackInverted();
                                BottomInverted();
                                Back();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                BottomInverted();
                                //front02
                                FrontInverted();
                                BottomInverted();
                                Front();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                BottomInverted();
                                BottomInverted();
                                //left02
                                LeftInverted();
                                BottomInverted();
                                Left();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "right22":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                BottomInverted();
                                //front22
                                Front();
                                Bottom();
                                FrontInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                Right();
                                Bottom();
                                RightInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                Bottom();
                                Bottom();
                                //left22
                                Left();
                                Bottom();
                                LeftInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                BottomInverted();
                                //back00
                                Back();
                                Bottom();
                                BackInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "top00":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                Back();
                                Bottom();
                                BackInverted();
                                Bottom();
                                //right02
                                RightInverted();
                                BottomInverted();
                                Right();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                Back();
                                BottomInverted();
                                BottomInverted();
                                BackInverted();
                                Bottom();
                                //back20
                                BackInverted();
                                BottomInverted();
                                Back();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                Back();
                                Bottom();
                                BackInverted();
                                //front02
                                FrontInverted();
                                BottomInverted();
                                Front();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "top20":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                BackInverted();
                                BottomInverted();
                                Back();
                                //right02
                                RightInverted();
                                BottomInverted();
                                Right();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                BackInverted();
                                BottomInverted();
                                BottomInverted();
                                Back();
                                //left22
                                Left();
                                Bottom();
                                LeftInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                BackInverted();
                                Bottom();
                                Bottom();
                                Back();
                                BottomInverted();
                                //back00
                                Back();
                                Bottom();
                                BackInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "top02":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                Left();
                                Bottom();
                                LeftInverted();
                                //right02
                                RightInverted();
                                BottomInverted();
                                Right();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                Left();
                                Bottom();
                                Bottom();
                                LeftInverted();
                                //back20
                                BackInverted();
                                BottomInverted();
                                Back();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                FrontInverted();
                                BottomInverted();
                                Front();
                                //back00
                                Back();
                                Bottom();
                                BackInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "top22":
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                Front();
                                Bottom();
                                FrontInverted();
                                //back20
                                BackInverted();
                                BottomInverted();
                                Back();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                RightInverted();
                                BottomInverted();
                                Right();
                                //left22
                                Left();
                                Bottom();
                                LeftInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                RightInverted();
                                BottomInverted();
                                BottomInverted();
                                Right();
                                //back00
                                Back();
                                Bottom();
                                BackInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "bottom00":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                Left();
                                BottomInverted();
                                LeftInverted();
                                BottomInverted();
                                //front22
                                Front();
                                Bottom();
                                FrontInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                Left();
                                BottomInverted();
                                LeftInverted();
                                //right22
                                Right();
                                Bottom();
                                RightInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                Left();
                                BottomInverted();
                                LeftInverted();
                                Bottom();
                                Bottom();
                                //left22
                                Left();
                                Bottom();
                                LeftInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                Left();
                                BottomInverted();
                                LeftInverted();
                                Bottom();
                                //back00
                                Back();
                                Bottom();
                                BackInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "bottom20":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                RightInverted();
                                BottomInverted();
                                BottomInverted();
                                Right();
                                Bottom();
                                //right02
                                RightInverted();
                                BottomInverted();
                                Right();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                Front();
                                Bottom();
                                FrontInverted();
                                //back20
                                BackInverted();
                                BottomInverted();
                                Back();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                RightInverted();
                                BottomInverted();
                                BottomInverted();
                                Right();
                                //front02
                                FrontInverted();
                                BottomInverted();
                                Front();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                RightInverted();
                                Bottom();
                                Right();
                                //left02
                                LeftInverted();
                                BottomInverted();
                                Left();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "bottom02":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                LeftInverted();
                                Bottom();
                                Left();
                                //right02
                                RightInverted();
                                BottomInverted();
                                Right();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                LeftInverted();
                                Bottom();
                                Left();
                                Bottom();
                                //back20
                                BackInverted();
                                BottomInverted();
                                Back();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                LeftInverted();
                                Bottom();
                                Left();
                                BottomInverted();
                                //front02
                                FrontInverted();
                                BottomInverted();
                                Front();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                LeftInverted();
                                Bottom();
                                Left();
                                BottomInverted();
                                BottomInverted();
                                //left02
                                LeftInverted();
                                BottomInverted();
                                Left();
                                Bottom();
                                cornersList = GetGreenCorners();
                            }
                            break;
                        case "bottom22":
                            if (colors.Contains("Orange") && colors.Contains("White"))
                            {
                                Right();
                                BottomInverted();
                                RightInverted();
                                Bottom();
                                //front22
                                Front();
                                Bottom();
                                FrontInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Orange") && colors.Contains("Yellow"))
                            {
                                Right();
                                BottomInverted();
                                RightInverted();
                                Bottom();
                                Bottom();
                                //right22
                                Right();
                                Bottom();
                                RightInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("White"))
                            {
                                Right();
                                BottomInverted();
                                RightInverted();
                                //left22
                                Left();
                                Bottom();
                                LeftInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            if (colors.Contains("Red") && colors.Contains("Yellow"))
                            {
                                Right();
                                BottomInverted();
                                RightInverted();
                                BottomInverted();
                                //back00
                                Back();
                                Bottom();
                                BackInverted();
                                BottomInverted();
                                cornersList = GetGreenCorners();
                            }
                            break;
                    }
                }
            }
        }
        public List<string> GetGreenCorners()
        {
            List<string> greenCornerList = new List<string>();
            if (front[0, 0] == "Green")
            {
                greenCornerList.Add("front00_" + left[2, 0] + "|" + top[0, 2]);
            }
            if (front[2, 0] == "Green")
            {
                greenCornerList.Add("front20_" + right[0, 0] + "|" + top[2, 2]);
            }
            if (front[0, 2] == "Green")
            {
                greenCornerList.Add("front02_" + left[2, 2] + "|" + bottom[0, 0]);
            }
            if (front[2, 2] == "Green")
            {
                greenCornerList.Add("front22_" + right[0, 2] + "|" + bottom[2, 0]);
            }
            if (back[0, 0] == "Green")
            {
                greenCornerList.Add("back00_" + left[0, 2] + "|" + bottom[0, 2]);
            }
            if (back[2, 0] == "Green")
            {
                greenCornerList.Add("back20_" + right[2, 2] + "|" + bottom[2, 2]);
            }
            if (back[0, 2] == "Green")
            {
                greenCornerList.Add("back02_" + left[0, 0] + "|" + top[0, 0]);
            }
            if (back[2, 2] == "Green")
            {
                greenCornerList.Add("back22_" + right[2, 0] + "|" + top[2, 0]);
            }
            if (left[0, 0] == "Green")
            {
                greenCornerList.Add("left00_" + top[0, 0] + "|" + back[0, 2]);
            }
            if (left[2, 0] == "Green")
            {
                greenCornerList.Add("left20_" + top[0, 2] + "|" + front[0, 0]);
            }
            if (left[0, 2] == "Green")
            {
                greenCornerList.Add("left02_" + bottom[0, 2] + "|" + back[0, 0]);
            }
            if (left[2, 2] == "Green")
            {
                greenCornerList.Add("left22_" + bottom[0, 0] + "|" + front[0, 2]);
            }
            if (right[0, 0] == "Green")
            {
                greenCornerList.Add("right00_" + top[2, 2] + "|" + front[2, 0]);
            }
            if (right[2, 0] == "Green")
            {
                greenCornerList.Add("right20_" + top[2, 0] + "|" + back[2, 2]);
            }
            if (right[0, 2] == "Green")
            {
                greenCornerList.Add("right02_" + bottom[2, 0] + "|" + front[2, 2]);
            }
            if (right[2, 2] == "Green")
            {
                greenCornerList.Add("right22_" + bottom[2, 2] + "|" + back[2, 0]);
            }
            if (top[0, 0] == "Green")
            {
                greenCornerList.Add("top00_" + back[0, 2] + "|" + left[0, 0]);
            }
            if (top[2, 0] == "Green")
            {
                greenCornerList.Add("top20_" + right[2, 0] + "|" + back[2, 2]);
            }
            if (top[0, 2] == "Green")
            {
                greenCornerList.Add("top02_" + front[0, 0] + "|" + left[2, 0]);
            }
            if (top[2, 2] == "Green")
            {
                greenCornerList.Add("top22_" + front[2, 0] + "|" + right[0, 0]);
            }
            if (bottom[0, 0] == "Green")
            {
                greenCornerList.Add("bottom00_" + front[0, 2] + "|" + left[2, 2]);
            }
            if (bottom[2, 0] == "Green")
            {
                greenCornerList.Add("bottom20_" + front[2, 2] + "|" + right[0, 2]);
            }
            if (bottom[0, 2] == "Green")
            {
                greenCornerList.Add("bottom02_" + back[0, 0] + "|" + left[0, 2]);
            }
            if (bottom[2, 2] == "Green")
            {
                greenCornerList.Add("bottom22_" + back[2, 0] + "|" + right[2, 2]);
            }
            return greenCornerList;
        }
        public bool CheckLineUpCenter()
        {
            if (front[1, 0] == "White")
            {
                if (left[1, 0] == "Red")
                {
                    if (right[1, 0] == "Orange")
                    {
                        if (back[1, 2] == "Yellow")
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool CheckLineUpCenter2()
        {
            if (front[1, 2] == "White")
            {
                if (left[1, 2] == "Red")
                {
                    if (right[1, 2] == "Orange")
                    {
                        if (back[1, 0] == "Yellow")
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public void LineUpCenter()
        {
            bool lineUpCenter = CheckLineUpCenter();
            while (!lineUpCenter)
            {
                if (front[1, 0] == "White")
                {
                    if (right[1, 0] == "Orange")
                    {
                        Front();
                        Top();
                        FrontInverted();
                        Top();
                        Front();
                        Top();
                        Top();
                        FrontInverted();
                        Top();
                    }
                    else if (back[1, 2] == "Yellow")
                    {
                        Right();
                        Top();
                        RightInverted();
                        Top();
                        Right();
                        Top();
                        Top();
                        RightInverted();
                        //Top();
                        // LineUpCenter();
                    }
                    else if (left[1, 0] == "Red")
                    {
                        Left();
                        Top();
                        LeftInverted();
                        Top();
                        Left();
                        Top();
                        Top();
                        LeftInverted();
                        Top();
                    }
                    else
                    {
                        Top();
                        // LineUpCenter();
                    }
                }
                else if (right[1, 0] == "Orange")
                {
                    if (back[1, 2] == "Yellow")
                    {
                        Right();
                        Top();
                        RightInverted();
                        Top();
                        Right();
                        Top();
                        Top();
                        RightInverted();
                        Top();
                    }
                    else if (left[1, 0] == "Red")
                    {
                        Front();
                        Top();
                        FrontInverted();
                        Top();
                        Front();
                        Top();
                        Top();
                        FrontInverted();
                        //Top();
                        //LineUpCenter();
                    }
                    else if (front[1, 0] == "White")
                    {
                        Front();
                        Top();
                        FrontInverted();
                        Top();
                        Front();
                        Top();
                        Top();
                        FrontInverted();
                        Top();
                    }
                    else
                    {
                        Top();
                        //LineUpCenter();
                    }
                }
                else if (back[1, 2] == "Yellow")
                {
                    if (left[1, 0] == "Red")
                    {
                        Back();
                        Top();
                        BackInverted();
                        Top();
                        Back();
                        Top();
                        Top();
                        BackInverted();
                        Top();
                    }
                    else if (front[1, 0] == "White")
                    {
                        Right();
                        Top();
                        RightInverted();
                        Top();
                        Right();
                        Top();
                        Top();
                        RightInverted();
                        //Top();
                        //LineUpCenter();
                    }
                    else if (right[1, 0] == "Orange")
                    {
                        Right();
                        Top();
                        RightInverted();
                        Top();
                        Right();
                        Top();
                        Top();
                        RightInverted();
                        Top();
                    }
                    else
                    {
                        Top();
                        //LineUpCenter();
                    }
                }
                else if (left[1, 0] == "Red")
                {
                    if (front[1, 0] == "White")
                    {
                        Left();
                        Top();
                        LeftInverted();
                        Top();
                        Left();
                        Top();
                        Top();
                        LeftInverted();
                        Top();
                    }
                    else if (right[1, 0] == "Orange")
                    {
                        Front();
                        Top();
                        FrontInverted();
                        Top();
                        Front();
                        Top();
                        Top();
                        FrontInverted();
                        //Top();
                        //LineUpCenter();
                    }
                    else if (back[1, 2] == "Yellow")
                    {
                        Back();
                        Top();
                        BackInverted();
                        Top();
                        Back();
                        Top();
                        Top();
                        BackInverted();
                        Top();
                    }
                    else
                    {
                        Top();
                        //LineUpCenter();
                    }
                }
                else
                {
                    Top();
                    //LineUpCenter();
                }
                lineUpCenter = CheckLineUpCenter();
            }
            RefreshScreen(currentFace);
        }
        public void LineUpCenter2()
        {
            bool lineUpCenter = CheckLineUpCenter2();
            while (!lineUpCenter)
            {
                if (front[1, 2] == "White")
                {
                    if (right[1, 2] == "Orange")//good
                    {
                        Right();
                        Bottom();
                        RightInverted();
                        Bottom();
                        Right();
                        Bottom();
                        Bottom();
                        RightInverted();
                        Bottom();
                    }
                    else if (back[1, 0] == "Yellow")//good
                    {
                        Left();
                        Bottom();
                        LeftInverted();
                        Bottom();
                        Left();
                        Bottom();
                        Bottom();
                        LeftInverted();
                        //Bottom();
                        //Top();
                        // LineUpCenter();
                    }
                    else if (left[1, 2] == "Red")//good
                    {
                        Front();
                        Bottom();
                        FrontInverted();
                        Bottom();
                        Front();
                        Bottom();
                        Bottom();
                        FrontInverted();
                        Bottom();
                    }
                    else
                    {
                        Bottom();
                        // LineUpCenter();
                    }
                }
                else if (right[1, 2] == "Orange")
                {
                    if (back[1, 0] == "Yellow")//good
                    {
                        Back();
                        Bottom();
                        BackInverted();
                        Bottom();
                        Back();
                        Bottom();
                        Bottom();
                        BackInverted();
                        Bottom();
                    }
                    else if (left[1, 2] == "Red")//good
                    {
                        Back();
                        Bottom();
                        BackInverted();
                        Bottom();
                        Back();
                        Bottom();
                        Bottom();
                        BackInverted();
                        //Bottom();
                    }
                    else if (front[1, 2] == "White")//good
                    {
                        Left();
                        Bottom();
                        LeftInverted();
                        Bottom();
                        Left();
                        Bottom();
                        Bottom();
                        LeftInverted();
                        Bottom();
                    }
                    else
                    {
                        Bottom();
                        //LineUpCenter();
                    }
                }
                else if (back[1, 0] == "Yellow")
                {//
                    if (left[1, 2] == "Red")
                    {
                        Left();
                        Bottom();
                        LeftInverted();
                        Bottom();
                        Left();
                        Bottom();
                        Bottom();
                        LeftInverted();
                        Bottom();
                    }
                    else if (front[1, 2] == "White")//good
                    {
                        Left();
                        Bottom();
                        LeftInverted();
                        Bottom();
                        Left();
                        Bottom();
                        Bottom();
                        LeftInverted();
                        //Top();
                        //LineUpCenter();
                    }
                    else if (right[1, 2] == "Orange")
                    {
                        Back();
                        Bottom();
                        BackInverted();
                        Bottom();
                        Back();
                        Bottom();
                        Bottom();
                        BackInverted();
                        Bottom();
                    }
                    else
                    {
                        Bottom();
                        //LineUpCenter();
                    }
                }
                else if (left[1, 2] == "Red")
                {
                    if (front[1, 2] == "White")
                    {
                        Front();
                        Bottom();
                        FrontInverted();
                        Bottom();
                        Front();
                        Bottom();
                        Bottom();
                        FrontInverted();
                        Bottom();
                    }
                    else if (right[1, 2] == "Orange")
                    {
                        Back();
                        Bottom();
                        BackInverted();
                        Bottom();
                        Back();
                        Bottom();
                        Bottom();
                        BackInverted();
                    }
                    else if (back[1, 0] == "Yellow")
                    {
                        Left();
                        Bottom();
                        LeftInverted();
                        Bottom();
                        Left();
                        Bottom();
                        Bottom();
                        LeftInverted();
                        Bottom();
                    }
                    else
                    {
                        Bottom();
                        //LineUpCenter();
                    }
                }
                else
                {
                    Bottom();
                    //LineUpCenter();
                }
                lineUpCenter = CheckLineUpCenter2();
            }
            RefreshScreen(currentFace);
        }
        public void PlaceCrossPieces(List<string> openCross, List<string> crossPieces, int crossListNum)
        {
            int iterations = 0;
            while (openCross.Count != 0 && iterations < 1000)
            {
                iterations++;
                while (crossPieces.Count != 0 && iterations < 1000)
                {
                    iterations++;
                    switch (crossPieces[crossListNum])
                    {
                        case "front[1,0]":
                            FrontInverted();
                            Top();
                            LeftInverted();
                            TopInverted();
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "front[1,2]":
                            if (top[1, 2] != "Green")
                            {
                                Front();
                                Top();
                                LeftInverted();
                                TopInverted();
                            }
                            else if (openCross.Contains("top[1,0]"))
                            {
                                Front();
                                TopInverted();
                                LeftInverted();
                                Top();
                            }
                            else if (openCross.Contains("top[0,1]"))
                            {
                                Front();
                                LeftInverted();
                                FrontInverted();
                            }
                            else if (openCross.Contains("top[2,1]"))
                            {
                                FrontInverted();
                                Right();
                                Front();
                            }
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "front[0,1]":
                            if (top[0, 1] != "Green")
                            {
                                LeftInverted();
                            }
                            else if (openCross.Contains("top[1,0]"))
                            {
                                TopInverted();
                                LeftInverted();
                            }
                            else if (openCross.Contains("top[1,2]"))
                            {
                                Top();
                                LeftInverted();
                            }
                            else if (openCross.Contains("top[2,1]"))
                            {
                                Top();
                                Top();
                                LeftInverted();
                            }
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "front[2,1]":
                            if (top[2, 1] != "Green")
                            {
                                Right();
                            }
                            else if (openCross.Contains("top[1,0]"))
                            {
                                Top();
                                Right();
                            }
                            else if (openCross.Contains("top[1,2]"))
                            {
                                TopInverted();
                                Right();
                            }
                            else if (openCross.Contains("top[0,1]"))
                            {
                                Top();
                                Top();
                                Right();
                            }
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "back[1,0]":
                            if (top[1, 0] != "Green")
                            {
                                Back();
                                Top();
                                RightInverted();
                                TopInverted();
                            }
                            else if (openCross.Contains("top[2,1]"))
                            {
                                Back();
                                RightInverted();
                                BackInverted();
                            }
                            else if (openCross.Contains("top[1,2]"))
                            {
                                Back();
                                TopInverted();
                                RightInverted();
                                Top();
                                BackInverted();
                            }
                            else if (openCross.Contains("top[0,1]"))
                            {
                                BackInverted();
                                Left();
                                Back();
                            }
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "back[1,2]":
                            BackInverted();
                            Top();
                            RightInverted();
                            TopInverted();
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "back[0,1]":
                            if (top[0, 1] != "Green")
                            {
                                Left();
                            }
                            else if (openCross.Contains("top[2,1]"))
                            {
                                Top();
                                Top();
                                Left();
                            }
                            else if (openCross.Contains("top[1,2]"))
                            {
                                Top();
                                Left();
                            }
                            else if (openCross.Contains("top[1,0]"))
                            {
                                TopInverted();
                                Left();
                            }
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "back[2,1]":
                            if (top[2, 1] != "Green")
                            {
                                RightInverted();
                            }
                            else if (openCross.Contains("top[0,1]"))
                            {
                                Top();
                                Top();
                                RightInverted();
                            }
                            else if (openCross.Contains("top[1,2]"))
                            {
                                TopInverted();
                                RightInverted();
                            }
                            else if (openCross.Contains("top[1,0]"))
                            {
                                Top();
                                RightInverted();
                            }
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "left[1,0]":
                            Left();
                            TopInverted();
                            Front();
                            Top();
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "left[1,2]":
                            if (top[0, 1] != "Green")
                            {
                                LeftInverted();
                                TopInverted();
                                Front();
                                Top();
                            }
                            else if (openCross.Contains("top[2,1]"))
                            {
                                LeftInverted();
                                Top();
                                Front();
                                TopInverted();
                                Left();
                            }
                            else if (openCross.Contains("top[1,2]"))
                            {
                                LeftInverted();
                                Front();
                                Left();
                            }
                            else if (openCross.Contains("top[1,0]"))
                            {
                                Left();
                                BackInverted();
                                LeftInverted();
                            }
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "left[0,1]":
                            if (top[1, 0] != "Green")
                            {
                                BackInverted();
                            }
                            else if (openCross.Contains("top[2,1]"))
                            {
                                TopInverted();
                                BackInverted();
                            }
                            else if (openCross.Contains("top[1,2]"))
                            {
                                Top();
                                Top();
                                BackInverted();
                            }
                            else if (openCross.Contains("top[0,1]"))
                            {
                                Top();
                                BackInverted();
                            }
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "left[2,1]":
                            if (top[1, 2] != "Green")
                            {
                                Front();
                            }
                            else if (openCross.Contains("top[1,0]"))
                            {
                                Top();
                                Top();
                                Front();
                            }
                            else if (openCross.Contains("top[2,1]"))
                            {
                                Top();
                                Front();
                            }
                            else if (openCross.Contains("top[0,1]"))
                            {
                                TopInverted();
                                Front();
                            }
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "right[1,0]":
                            RightInverted();
                            Top();
                            FrontInverted();
                            TopInverted();
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "right[1,2]":
                            if (top[2, 1] != "Green")
                            {
                                Right();
                                Top();
                                FrontInverted();
                                TopInverted();
                                RightInverted();
                            }
                            else if (openCross.Contains("top[1,0]"))
                            {
                                RightInverted();
                                Back();
                                Right();
                            }
                            else if (openCross.Contains("top[1,2]"))
                            {
                                Right();
                                FrontInverted();
                                RightInverted();
                            }
                            else if (openCross.Contains("top[0,1]"))
                            {
                                TopInverted();
                                Right();
                                FrontInverted();
                                RightInverted();
                            }
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "right[0,1]":
                            if (top[1, 2] != "Green")
                            {
                                FrontInverted();
                            }
                            else if (openCross.Contains("top[1,0]"))
                            {
                                Top();
                                Top();
                                FrontInverted();
                            }
                            else if (openCross.Contains("top[2,1]"))
                            {
                                Top();
                                FrontInverted();
                            }
                            else if (openCross.Contains("top[0,1]"))
                            {
                                TopInverted();
                                FrontInverted();
                            }
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "right[2,1]":
                            if (top[1, 0] != "Green")
                            {
                                Back();
                            }
                            else if (openCross.Contains("top[1,2]"))
                            {
                                Top();
                                Top();
                                Back();
                            }
                            else if (openCross.Contains("top[2,1]"))
                            {
                                TopInverted();
                                Back();
                            }
                            else if (openCross.Contains("top[0,1]"))
                            {
                                Top();
                                Back();
                            }
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "bottom[1,0]":
                            if (top[1, 2] != "Green")
                            {
                                Front();
                                Front();
                            }
                            else if (openCross.Contains("top[1,0]"))
                            {
                                Bottom();
                                Bottom();
                                Back();
                                Back();
                            }
                            else if (openCross.Contains("top[2,1]"))
                            {
                                Bottom();
                                Right();
                                Right();
                            }
                            else if (openCross.Contains("top[0,1]"))
                            {
                                BottomInverted();
                                Left();
                                Left();
                            }
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "bottom[1,2]":
                            if (top[1, 0] != "Green")
                            {
                                Back();
                                Back();
                            }
                            else if (openCross.Contains("top[1,2]"))
                            {
                                Bottom();
                                Bottom();
                                Front();
                                Front();
                            }
                            else if (openCross.Contains("top[2,1]"))
                            {
                                BottomInverted();
                                Right();
                                Right();
                            }
                            else if (openCross.Contains("top[0,1]"))
                            {
                                Bottom();
                                Left();
                                Left();
                            }
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "bottom[0,1]":
                            if (top[0, 1] != "Green")
                            {
                                Left();
                                Left();
                            }
                            else if (openCross.Contains("top[1,2]"))
                            {
                                Bottom();
                                Front();
                                Front();
                            }
                            else if (openCross.Contains("top[2,1]"))
                            {
                                Bottom();
                                Bottom();
                                Right();
                                Right();
                            }
                            else if (openCross.Contains("top[1,0]"))
                            {
                                BottomInverted();
                                Back();
                                Back();
                            }
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                        case "bottom[2,1]":
                            if (top[2, 1] != "Green")
                            {
                                Right();
                                Right();
                            }
                            else if (openCross.Contains("top[1,2]"))
                            {
                                BottomInverted();
                                Front();
                                Front();
                            }
                            else if (openCross.Contains("top[0,1]"))
                            {
                                Bottom();
                                Bottom();
                                Left();
                                Left();
                            }
                            else if (openCross.Contains("top[1,0]"))
                            {
                                Bottom();
                                Back();
                                Back();
                            }
                            openCross = FindOpenCross();
                            crossPieces = FindCrossPieces();
                            break;
                    }
                }
            }
        }
        public List<string> FindOpenCross()
        {
            List<string> openCrossLocation = new List<string>();
            if (top[1, 0] != "Green")
            {
                openCrossLocation.Add("top[1,0]");
            }
            if (top[1, 2] != "Green")
            {
                openCrossLocation.Add("top[1,2]");
            }
            if (top[0, 1] != "Green")
            {
                openCrossLocation.Add("top[0,1]");
            }
            if (top[2, 1] != "Green")
            {
                openCrossLocation.Add("top[2,1]");
            }
            return openCrossLocation;
        }
        public List<string> FindCrossPieces()
        {
            List<string> crossPieceLocation = new List<string>();
            if (front[1, 0] == "Green")
            {
                crossPieceLocation.Add("front[1,0]");
            }
            if (front[1, 2] == "Green")
            {
                crossPieceLocation.Add("front[1,2]");
            }
            if (front[0, 1] == "Green")
            {
                crossPieceLocation.Add("front[0,1]");
            }
            if (front[2, 1] == "Green")
            {
                crossPieceLocation.Add("front[2,1]");
            }
            if (back[1, 0] == "Green")
            {
                crossPieceLocation.Add("back[1,0]");
            }
            if (back[1, 2] == "Green")
            {
                crossPieceLocation.Add("back[1,2]");
            }
            if (back[0, 1] == "Green")
            {
                crossPieceLocation.Add("back[0,1]");
            }
            if (back[2, 1] == "Green")
            {
                crossPieceLocation.Add("back[2,1]");
            }
            if (left[1, 0] == "Green")
            {
                crossPieceLocation.Add("left[1,0]");
            }
            if (left[1, 2] == "Green")
            {
                crossPieceLocation.Add("left[1,2]");
            }
            if (left[0, 1] == "Green")
            {
                crossPieceLocation.Add("left[0,1]");
            }
            if (left[2, 1] == "Green")
            {
                crossPieceLocation.Add("left[2,1]");
            }
            if (right[1, 0] == "Green")
            {
                crossPieceLocation.Add("right[1,0]");
            }
            if (right[1, 2] == "Green")
            {
                crossPieceLocation.Add("right[1,2]");
            }
            if (right[0, 1] == "Green")
            {
                crossPieceLocation.Add("right[0,1]");
            }
            if (right[2, 1] == "Green")
            {
                crossPieceLocation.Add("right[2,1]");
            }
            if (bottom[1, 0] == "Green")
            {
                crossPieceLocation.Add("bottom[1,0]");
            }
            if (bottom[1, 2] == "Green")
            {
                crossPieceLocation.Add("bottom[1,2]");
            }
            if (bottom[0, 1] == "Green")
            {
                crossPieceLocation.Add("bottom[0,1]");
            }
            if (bottom[2, 1] == "Green")
            {
                crossPieceLocation.Add("bottom[2,1]");
            }
            return crossPieceLocation;
        }
        public void Right()
        {
            totalMoves++;
            string[,] tempRight = CopyFace(right);
            string[,] tempFront = CopyFace(front);
            string[,] tempBack = CopyFace(back);
            string[,] tempTop = CopyFace(top);
            string[,] tempBottom = CopyFace(bottom);
            string[,] tempLeft = CopyFace(left);

            right[0, 0] = tempRight[0, 2];
            right[1, 0] = tempRight[0, 1];
            right[2, 0] = tempRight[0, 0];
            right[2, 1] = tempRight[1, 0];
            right[2, 2] = tempRight[2, 0];
            right[0, 2] = tempRight[2, 2];
            right[0, 1] = tempRight[1, 2];
            right[1, 1] = tempRight[1, 1];
            right[1, 2] = tempRight[2, 1];

            front[2, 0] = tempBottom[2, 0];
            front[2, 1] = tempBottom[2, 1];
            front[2, 2] = tempBottom[2, 2];

            top[2, 0] = tempFront[2, 0];
            top[2, 1] = tempFront[2, 1];
            top[2, 2] = tempFront[2, 2];

            back[2, 0] = tempTop[2, 0];
            back[2, 1] = tempTop[2, 1];
            back[2, 2] = tempTop[2, 2];

            bottom[2, 0] = tempBack[2, 0];
            bottom[2, 1] = tempBack[2, 1];
            bottom[2, 2] = tempBack[2, 2];

            RefreshScreen(currentFace);
        }

        public void RightInverted()
        {
            totalMoves++;
            string[,] tempRight = CopyFace(right);
            string[,] tempFront = CopyFace(front);
            string[,] tempBack = CopyFace(back);
            string[,] tempTop = CopyFace(top);
            string[,] tempBottom = CopyFace(bottom);
            string[,] tempLeft = CopyFace(left);

            right[0, 0] = tempRight[2, 0];
            right[1, 0] = tempRight[2, 1];
            right[2, 0] = tempRight[2, 2];
            right[2, 1] = tempRight[1, 2];
            right[2, 2] = tempRight[0, 2];
            right[0, 2] = tempRight[0, 0];
            right[0, 1] = tempRight[1, 0];
            right[1, 1] = tempRight[1, 1];
            right[1, 2] = tempRight[0, 1];

            bottom[2, 0] = tempFront[2, 0];
            bottom[2, 1] = tempFront[2, 1];
            bottom[2, 2] = tempFront[2, 2];

            front[2, 0] = tempTop[2, 0];
            front[2, 1] = tempTop[2, 1];
            front[2, 2] = tempTop[2, 2];

            top[2, 0] = tempBack[2, 0];
            top[2, 1] = tempBack[2, 1];
            top[2, 2] = tempBack[2, 2];

            back[2, 0] = tempBottom[2, 0];
            back[2, 1] = tempBottom[2, 1];
            back[2, 2] = tempBottom[2, 2];

            RefreshScreen(currentFace);
        }

        public void LeftInverted()
        {
            totalMoves++;
            string[,] tempRight = CopyFace(right);
            string[,] tempFront = CopyFace(front);
            string[,] tempBack = CopyFace(back);
            string[,] tempTop = CopyFace(top);
            string[,] tempBottom = CopyFace(bottom);
            string[,] tempLeft = CopyFace(left);

            left[0, 0] = tempLeft[2, 0];
            left[1, 0] = tempLeft[2, 1];
            left[2, 0] = tempLeft[2, 2];
            left[2, 1] = tempLeft[1, 2];
            left[2, 2] = tempLeft[0, 2];
            left[0, 2] = tempLeft[0, 0];
            left[0, 1] = tempLeft[1, 0];
            left[1, 1] = tempLeft[1, 1];
            left[1, 2] = tempLeft[0, 1];

            front[0, 0] = tempBottom[0, 0];
            front[0, 1] = tempBottom[0, 1];
            front[0, 2] = tempBottom[0, 2];

            top[0, 0] = tempFront[0, 0];
            top[0, 1] = tempFront[0, 1];
            top[0, 2] = tempFront[0, 2];

            back[0, 0] = tempTop[0, 0];
            back[0, 1] = tempTop[0, 1];
            back[0, 2] = tempTop[0, 2];

            bottom[0, 0] = tempBack[0, 0];
            bottom[0, 1] = tempBack[0, 1];
            bottom[0, 2] = tempBack[0, 2];

            RefreshScreen(currentFace);
        }
        public void Left()
        {
            totalMoves++;
            string[,] tempRight = CopyFace(right);
            string[,] tempFront = CopyFace(front);
            string[,] tempBack = CopyFace(back);
            string[,] tempTop = CopyFace(top);
            string[,] tempBottom = CopyFace(bottom);
            string[,] tempLeft = CopyFace(left);

            left[0, 0] = tempLeft[0, 2];
            left[1, 0] = tempLeft[0, 1];
            left[2, 0] = tempLeft[0, 0];
            left[2, 1] = tempLeft[1, 0];
            left[2, 2] = tempLeft[2, 0];
            left[0, 2] = tempLeft[2, 2];
            left[0, 1] = tempLeft[1, 2];
            left[1, 1] = tempLeft[1, 1];
            left[1, 2] = tempLeft[2, 1];

            bottom[0, 0] = tempFront[0, 0];
            bottom[0, 1] = tempFront[0, 1];
            bottom[0, 2] = tempFront[0, 2];

            front[0, 0] = tempTop[0, 0];
            front[0, 1] = tempTop[0, 1];
            front[0, 2] = tempTop[0, 2];

            top[0, 0] = tempBack[0, 0];
            top[0, 1] = tempBack[0, 1];
            top[0, 2] = tempBack[0, 2];

            back[0, 0] = tempBottom[0, 0];
            back[0, 1] = tempBottom[0, 1];
            back[0, 2] = tempBottom[0, 2];

            RefreshScreen(currentFace);
        }

        public void Top()
        {
            totalMoves++;
            string[,] tempRight = CopyFace(right);
            string[,] tempFront = CopyFace(front);
            string[,] tempBack = CopyFace(back);
            string[,] tempTop = CopyFace(top);
            string[,] tempBottom = CopyFace(bottom);
            string[,] tempLeft = CopyFace(left);

            top[0, 0] = tempTop[0, 2];
            top[1, 0] = tempTop[0, 1];
            top[2, 0] = tempTop[0, 0];
            top[2, 1] = tempTop[1, 0];
            top[2, 2] = tempTop[2, 0];
            top[0, 2] = tempTop[2, 2];
            top[0, 1] = tempTop[1, 2];
            top[1, 1] = tempTop[1, 1];
            top[1, 2] = tempTop[2, 1];

            front[0, 0] = tempRight[0, 0];
            front[1, 0] = tempRight[1, 0];
            front[2, 0] = tempRight[2, 0];

            left[0, 0] = tempFront[0, 0];
            left[1, 0] = tempFront[1, 0];
            left[2, 0] = tempFront[2, 0];

            back[2, 2] = tempLeft[0, 0];
            back[1, 2] = tempLeft[1, 0];
            back[0, 2] = tempLeft[2, 0];

            right[0, 0] = tempBack[2, 2];
            right[1, 0] = tempBack[1, 2];
            right[2, 0] = tempBack[0, 2];

            RefreshScreen(currentFace);
        }

        public void TopInverted()
        {
            totalMoves++;
            string[,] tempRight = CopyFace(right);
            string[,] tempFront = CopyFace(front);
            string[,] tempBack = CopyFace(back);
            string[,] tempTop = CopyFace(top);
            string[,] tempBottom = CopyFace(bottom);
            string[,] tempLeft = CopyFace(left);

            top[0, 0] = tempTop[2, 0];
            top[1, 0] = tempTop[2, 1];
            top[2, 0] = tempTop[2, 2];
            top[2, 1] = tempTop[1, 2];
            top[2, 2] = tempTop[0, 2];
            top[0, 2] = tempTop[0, 0];
            top[0, 1] = tempTop[1, 0];
            top[1, 1] = tempTop[1, 1];
            top[1, 2] = tempTop[0, 1];

            right[0, 0] = tempFront[0, 0];
            right[1, 0] = tempFront[1, 0];
            right[2, 0] = tempFront[2, 0];

            front[0, 0] = tempLeft[0, 0];
            front[1, 0] = tempLeft[1, 0];
            front[2, 0] = tempLeft[2, 0];

            left[2, 0] = tempBack[0, 2];
            left[1, 0] = tempBack[1, 2];
            left[0, 0] = tempBack[2, 2];

            back[0, 2] = tempRight[2, 0];
            back[1, 2] = tempRight[1, 0];
            back[2, 2] = tempRight[0, 0];

            RefreshScreen(currentFace);
        }

        public void BottomInverted()
        {
            totalMoves++;
            string[,] tempRight = CopyFace(right);
            string[,] tempFront = CopyFace(front);
            string[,] tempBack = CopyFace(back);
            string[,] tempTop = CopyFace(top);
            string[,] tempBottom = CopyFace(bottom);
            string[,] tempLeft = CopyFace(left);

            bottom[0, 0] = tempBottom[2, 0];
            bottom[1, 0] = tempBottom[2, 1];
            bottom[2, 0] = tempBottom[2, 2];
            bottom[2, 1] = tempBottom[1, 2];
            bottom[2, 2] = tempBottom[0, 2];
            bottom[0, 2] = tempBottom[0, 0];
            bottom[0, 1] = tempBottom[1, 0];
            bottom[1, 1] = tempBottom[1, 1];
            bottom[1, 2] = tempBottom[0, 1];

            front[0, 2] = tempRight[0, 2];
            front[1, 2] = tempRight[1, 2];
            front[2, 2] = tempRight[2, 2];

            left[0, 2] = tempFront[0, 2];
            left[1, 2] = tempFront[1, 2];
            left[2, 2] = tempFront[2, 2];

            back[0, 0] = tempLeft[2, 2];
            back[1, 0] = tempLeft[1, 2];
            back[2, 0] = tempLeft[0, 2];

            right[2, 2] = tempBack[0, 0];
            right[1, 2] = tempBack[1, 0];
            right[0, 2] = tempBack[2, 0];

            RefreshScreen(currentFace);
        }

        public void Bottom()
        {
            totalMoves++;
            string[,] tempRight = CopyFace(right);
            string[,] tempFront = CopyFace(front);
            string[,] tempBack = CopyFace(back);
            string[,] tempTop = CopyFace(top);
            string[,] tempBottom = CopyFace(bottom);
            string[,] tempLeft = CopyFace(left);

            bottom[0, 0] = tempBottom[0, 2];
            bottom[1, 0] = tempBottom[0, 1];
            bottom[2, 0] = tempBottom[0, 0];
            bottom[2, 1] = tempBottom[1, 0];
            bottom[2, 2] = tempBottom[2, 0];
            bottom[0, 2] = tempBottom[2, 2];
            bottom[0, 1] = tempBottom[1, 2];
            bottom[1, 1] = tempBottom[1, 1];
            bottom[1, 2] = tempBottom[2, 1];

            right[0, 2] = tempFront[0, 2];
            right[1, 2] = tempFront[1, 2];
            right[2, 2] = tempFront[2, 2];

            front[0, 2] = tempLeft[0, 2];
            front[1, 2] = tempLeft[1, 2];
            front[2, 2] = tempLeft[2, 2];

            left[2, 2] = tempBack[0, 0];
            left[1, 2] = tempBack[1, 0];
            left[0, 2] = tempBack[2, 0];

            back[0, 0] = tempRight[2, 2];
            back[1, 0] = tempRight[1, 2];
            back[2, 0] = tempRight[0, 2];

            RefreshScreen(currentFace);
        }

        public void Front()
        {
            totalMoves++;
            string[,] tempRight = CopyFace(right);
            string[,] tempFront = CopyFace(front);
            string[,] tempBack = CopyFace(back);
            string[,] tempTop = CopyFace(top);
            string[,] tempBottom = CopyFace(bottom);
            string[,] tempLeft = CopyFace(left);

            front[0, 0] = tempFront[0, 2];
            front[1, 0] = tempFront[0, 1];
            front[2, 0] = tempFront[0, 0];
            front[2, 1] = tempFront[1, 0];
            front[2, 2] = tempFront[2, 0];
            front[0, 2] = tempFront[2, 2];
            front[0, 1] = tempFront[1, 2];
            front[1, 1] = tempFront[1, 1];
            front[1, 2] = tempFront[2, 1];

            top[0, 2] = tempLeft[2, 2];
            top[1, 2] = tempLeft[2, 1];
            top[2, 2] = tempLeft[2, 0];

            right[0, 0] = tempTop[0, 2];
            right[0, 1] = tempTop[1, 2];
            right[0, 2] = tempTop[2, 2];

            bottom[0, 0] = tempRight[0, 2];
            bottom[1, 0] = tempRight[0, 1];
            bottom[2, 0] = tempRight[0, 0];

            left[2, 0] = tempBottom[0, 0];
            left[2, 1] = tempBottom[1, 0];
            left[2, 2] = tempBottom[2, 0];

            RefreshScreen(currentFace);
        }

        public void FrontInverted()
        {
            totalMoves++;
            string[,] tempRight = CopyFace(right);
            string[,] tempFront = CopyFace(front);
            string[,] tempBack = CopyFace(back);
            string[,] tempTop = CopyFace(top);
            string[,] tempBottom = CopyFace(bottom);
            string[,] tempLeft = CopyFace(left);

            front[0, 0] = tempFront[2, 0];
            front[1, 0] = tempFront[2, 1];
            front[2, 0] = tempFront[2, 2];
            front[2, 1] = tempFront[1, 2];
            front[2, 2] = tempFront[0, 2];
            front[0, 2] = tempFront[0, 0];
            front[0, 1] = tempFront[1, 0];
            front[1, 1] = tempFront[1, 1];
            front[1, 2] = tempFront[0, 1];

            left[2, 2] = tempTop[0, 2];
            left[2, 1] = tempTop[1, 2];
            left[2, 0] = tempTop[2, 2];

            top[0, 2] = tempRight[0, 0];
            top[1, 2] = tempRight[0, 1];
            top[2, 2] = tempRight[0, 2];

            right[0, 2] = tempBottom[0, 0];
            right[0, 1] = tempBottom[1, 0];
            right[0, 0] = tempBottom[2, 0];

            bottom[0, 0] = tempLeft[2, 0];
            bottom[1, 0] = tempLeft[2, 1];
            bottom[2, 0] = tempLeft[2, 2];

            RefreshScreen(currentFace);
        }

        public void BackInverted()
        {
            totalMoves++;
            string[,] tempRight = CopyFace(right);
            string[,] tempFront = CopyFace(front);
            string[,] tempBack = CopyFace(back);
            string[,] tempTop = CopyFace(top);
            string[,] tempBottom = CopyFace(bottom);
            string[,] tempLeft = CopyFace(left);

            back[0, 0] = tempBack[2, 0];
            back[1, 0] = tempBack[2, 1];
            back[2, 0] = tempBack[2, 2];
            back[2, 1] = tempBack[1, 2];
            back[2, 2] = tempBack[0, 2];
            back[0, 2] = tempBack[0, 0];
            back[0, 1] = tempBack[1, 0];
            back[1, 1] = tempBack[1, 1];
            back[1, 2] = tempBack[0, 1];

            left[0, 2] = tempBottom[2, 2];
            left[0, 1] = tempBottom[1, 2];  //CORRECT
            left[0, 0] = tempBottom[0, 2];

            top[0, 0] = tempLeft[0, 2];
            top[1, 0] = tempLeft[0, 1]; //CORRECT
            top[2, 0] = tempLeft[0, 0];

            right[2, 2] = tempTop[2, 0];
            right[2, 1] = tempTop[1, 0];
            right[2, 0] = tempTop[0, 0];

            bottom[0, 2] = tempRight[2, 2];
            bottom[1, 2] = tempRight[2, 1];
            bottom[2, 2] = tempRight[2, 0];

            RefreshScreen(currentFace);
        }

        public void Back()
        {
            totalMoves++;
            string[,] tempRight = CopyFace(right);
            string[,] tempFront = CopyFace(front);
            string[,] tempBack = CopyFace(back);
            string[,] tempTop = CopyFace(top);
            string[,] tempBottom = CopyFace(bottom);
            string[,] tempLeft = CopyFace(left);

            back[0, 0] = tempBack[0, 2];
            back[1, 0] = tempBack[0, 1];
            back[2, 0] = tempBack[0, 0];
            back[2, 1] = tempBack[1, 0];
            back[2, 2] = tempBack[2, 0];
            back[0, 2] = tempBack[2, 2];
            back[0, 1] = tempBack[1, 2];
            back[1, 1] = tempBack[1, 1];
            back[1, 2] = tempBack[2, 1];

            left[0, 2] = tempTop[0, 0];
            left[0, 1] = tempTop[1, 0];
            left[0, 0] = tempTop[2, 0];

            top[0, 0] = tempRight[2, 0];
            top[1, 0] = tempRight[2, 1];
            top[2, 0] = tempRight[2, 2];

            right[2, 2] = tempBottom[0, 2];
            right[2, 1] = tempBottom[1, 2];
            right[2, 0] = tempBottom[2, 2];

            bottom[0, 2] = tempLeft[0, 0];
            bottom[1, 2] = tempLeft[0, 1];
            bottom[2, 2] = tempLeft[0, 2];

            RefreshScreen(currentFace);
        }
        public void RefreshScreen(string face)
        {
            switch (face)
            {
                case "front":
                    ShowFace(front);
                    break;
                case "left":
                    ShowFace(left);
                    break;
                case "right":
                    ShowFace(right);
                    break;
                case "top":
                    ShowFace(top);
                    break;
                case "back":
                    ShowFace(back);
                    break;
                case "bottom":
                    ShowFace(bottom);
                    break;
            }
        }
        public void ShowFace(string[,] side)
        {
            tlBack = side[0, 0];
            tmBack = side[1, 0];
            trBack = side[2, 0];
            mlBack = side[0, 1];
            mmBack = side[1, 1];
            mrBack = side[2, 1];
            blBack = side[0, 2];
            bmBack = side[1, 2];
            brBack = side[2, 2];
        }
        public void SetOriginalColor(string[,] side, string color)
        {
            for (int i = 0; i < side.GetLength(0); i++)
            {
                for (int j = 0; j < side.GetLength(1); j++)
                {
                    side[i, j] = color;
                }
            }
        }

        public string[,] CopyFace(string[,] copiedFace)
        {
            string[,] returnedFace = new string[3, 3];
            for (int i = 0; i < copiedFace.GetLength(0); i++)
            {
                for (int j = 0; j < copiedFace.GetLength(1); j++)
                {
                    returnedFace[i, j] = copiedFace[i, j];
                }
            }
            return returnedFace;
        }
        public ICommand FrontClick
        {
            get
            {
                return new RelayCommand(() =>
                {
                    currentFace = "front";
                    ShowFace(front);
                });
            }
        }
        public ICommand LeftClick
        {
            get
            {
                return new RelayCommand(() =>
                {
                    currentFace = "left";
                    ShowFace(left);
                });
            }
        }
        public ICommand RightClick
        {
            get
            {
                return new RelayCommand(() =>
                {
                    currentFace = "right";
                    ShowFace(right);
                });
            }
        }
        public ICommand BottomClick
        {
            get
            {
                return new RelayCommand(() =>
                {
                    currentFace = "bottom";
                    ShowFace(bottom);
                });
            }
        }
        public ICommand TopClick
        {
            get
            {
                return new RelayCommand(() =>
                {
                    currentFace = "top";
                    ShowFace(top);
                });
            }
        }
        public ICommand BackClick
        {
            get
            {
                return new RelayCommand(() =>
                {
                    currentFace = "back";
                    ShowFace(back);
                });
            }
        }

        public ICommand FrontCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Front();
                });
            }
        }

        public ICommand FrontInvertedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    FrontInverted();
                });
            }
        }

        public ICommand RightCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Right();
                });
            }
        }

        public ICommand RightInvertedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    RightInverted();
                });
            }
        }

        public ICommand LeftCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Left();
                });
            }
        }

        public ICommand LeftInvertedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    LeftInverted();
                });
            }
        }

        public ICommand TopCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Top();
                });
            }
        }

        public ICommand TopInvertedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    TopInverted();
                });
            }
        }

        public ICommand BottomCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Bottom();
                });
            }
        }

        public ICommand BottomInvertedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    BottomInverted();
                });
            }
        }

        public ICommand BackCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Back();
                });
            }
        }

        public ICommand BackInvertedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    BackInverted();
                });
            }
        }

        public ICommand Clear
        {
            get
            {
                return new RelayCommand(() =>
                {
                    ColorVisibility = System.Windows.Visibility.Hidden;
                    ClearCube();
                });
            }
        }
        public ICommand Scramble
        {
            get
            {
                return new RelayCommand(() =>
                {
                    ColorVisibility = System.Windows.Visibility.Hidden;
                    ScrambleCube();
                    lineupcenter = 0;
                });
            }
        }
        public ICommand Solve
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SolveCube();
                });
            }
        }

        public ICommand SelectTopLeftColor
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SelectColor("tl");
                });
            }
        }
        public ICommand SelectTopMiddleColor
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SelectColor("tm");
                });
            }
        }
        public ICommand SelectTopRightColor
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SelectColor("tr");
                });
            }
        }
        public ICommand SelectMiddleLeftColor
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SelectColor("ml");
                });
            }
        }
        public ICommand SelectMiddleRightColor
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SelectColor("mr");
                });
            }
        }
        public ICommand SelectBottomLeftColor
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SelectColor("bl");
                });
            }
        }
        public ICommand SelectBottomMiddleColor
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SelectColor("bm");
                });
            }
        }
        public ICommand SelectBottomRightColor
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SelectColor("br");
                });
            }
        }
        public ICommand SelectWhite
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SetCubeColor("White");
                });
            }
        }
        public ICommand SelectYellow
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SetCubeColor("Yellow");
                });
            }
        }
        public ICommand SelectRed
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SetCubeColor("Red");
                });
            }
        }
        public ICommand SelectOrange
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SetCubeColor("Orange");
                });
            }
        }
        public ICommand SelectGreen
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SetCubeColor("Green");
                });
            }
        }
        public ICommand SelectBlue
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SetCubeColor("Blue");
                });
            }
        }
        public void SetCubeColor(string color)
        {
            switch (currentCube)
            {
                case "fronttl":
                    front[0, 0] = color;
                    break;
                case "fronttm":
                    front[1, 0] = color;
                    break;
                case "fronttr":
                    front[2, 0] = color;
                    break;
                case "frontml":
                    front[0, 1] = color;
                    break;
                case "frontmr":
                    front[2, 1] = color;
                    break;
                case "frontbl":
                    front[0, 2] = color;
                    break;
                case "frontbm":
                    front[1, 2] = color;
                    break;
                case "frontbr":
                    front[2, 2] = color;
                    break;
                case "backtl":
                    back[0, 0] = color;
                    break;
                case "backtm":
                    back[1, 0] = color;
                    break;
                case "backtr":
                    back[2, 0] = color;
                    break;
                case "backml":
                    back[0, 1] = color;
                    break;
                case "backmr":
                    back[2, 1] = color;
                    break;
                case "backbl":
                    back[0, 2] = color;
                    break;
                case "backbm":
                    back[1, 2] = color;
                    break;
                case "backbr":
                    back[2, 2] = color;
                    break;
                case "toptl":
                    top[0, 0] = color;
                    break;
                case "toptm":
                    top[1, 0] = color;
                    break;
                case "toptr":
                    top[2, 0] = color;
                    break;
                case "topml":
                    top[0, 1] = color;
                    break;
                case "topmr":
                    top[2, 1] = color;
                    break;
                case "topbl":
                    top[0, 2] = color;
                    break;
                case "topbm":
                    top[1, 2] = color;
                    break;
                case "topbr":
                    top[2, 2] = color;
                    break;
                case "bottomtl":
                    bottom[0, 0] = color;
                    break;
                case "bottomtm":
                    bottom[1, 0] = color;
                    break;
                case "bottomtr":
                    bottom[2, 0] = color;
                    break;
                case "bottomml":
                    bottom[0, 1] = color;
                    break;
                case "bottommr":
                    bottom[2, 1] = color;
                    break;
                case "bottombl":
                    bottom[0, 2] = color;
                    break;
                case "bottombm":
                    bottom[1, 2] = color;
                    break;
                case "bottombr":
                    bottom[2, 2] = color;
                    break;
                case "lefttl":
                    left[0, 0] = color;
                    break;
                case "lefttm":
                    left[1, 0] = color;
                    break;
                case "lefttr":
                    left[2, 0] = color;
                    break;
                case "leftml":
                    left[0, 1] = color;
                    break;
                case "leftmr":
                    left[2, 1] = color;
                    break;
                case "leftbl":
                    left[0, 2] = color;
                    break;
                case "leftbm":
                    left[1, 2] = color;
                    break;
                case "leftbr":
                    left[2, 2] = color;
                    break;
                case "righttl":
                    right[0, 0] = color;
                    break;
                case "righttm":
                    right[1, 0] = color;
                    break;
                case "righttr":
                    right[2, 0] = color;
                    break;
                case "rightml":
                    right[0, 1] = color;
                    break;
                case "rightmr":
                    right[2, 1] = color;
                    break;
                case "rightbl":
                    right[0, 2] = color;
                    break;
                case "rightbm":
                    right[1, 2] = color;
                    break;
                case "rightbr":
                    right[2, 2] = color;
                    break;
            }
            ColorVisibility = System.Windows.Visibility.Hidden;
            RefreshScreen(currentFace);
        }
        public void SelectColor(string face)
        {
            currentCube = currentFace + face;
            ColorVisibility = System.Windows.Visibility.Visible;
        }
        public void ClearCube()
        {
            SetOriginalColor(front, "White");
            SetOriginalColor(left, "Red");
            SetOriginalColor(back, "Yellow");
            SetOriginalColor(right, "Orange");
            SetOriginalColor(top, "Green");
            SetOriginalColor(bottom, "Blue");
            ShowFace(front);
            lineupcenter = 0;
        }
        public void ScrambleCube()
        {
            string finalString = "";
            Random r = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int move = r.Next(12);
                switch (move)
                {
                    case 0:
                        Right();
                        finalString += "R,";
                        break;
                    case 1:
                        RightInverted();
                        finalString += "Ri,";
                        break;
                    case 2:
                        Left();
                        finalString += "L,";
                        break;
                    case 3:
                        LeftInverted();
                        finalString += "Li,";
                        break;
                    case 4:
                        Front();
                        finalString += "F,";
                        break;
                    case 5:
                        FrontInverted();
                        finalString += "Fi,";
                        break;
                    case 6:
                        Back();
                        finalString += "B,";
                        break;
                    case 7:
                        BackInverted();
                        finalString += "Bi,";
                        break;
                    case 8:
                        Top();
                        finalString += "U,";
                        break;
                    case 9:
                        TopInverted();
                        finalString += "Ui,";
                        break;
                    case 10:
                        Bottom();
                        finalString += "D,";
                        break;
                    case 11:
                        BottomInverted();
                        finalString += "Di,";
                        break;
                }
            }
            WriteScramble(finalString);
        }

        public void WriteScramble(string scrambleString)
        {
            // WriteAllLines creates a file, writes a collection of strings to the file, 
            // and then closes the file.
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("scramble.txt", true))
            {
                file.WriteLine(scrambleString);
            }
        }
        public System.Windows.Visibility ColorVisibility
        {
            get
            {
                return colorVisibility;
            }
            set
            {
                colorVisibility = value;
                OnPropertyChanged("ColorVisibility");
            }
        }
        public string tlBack
        {
            get
            {
                return _tlBack;
            }
            set
            {
                _tlBack = value;
                OnPropertyChanged("tlBack");
            }
        }
        public string tmBack
        {
            get
            {
                return _tmBack;
            }
            set
            {
                _tmBack = value;
                OnPropertyChanged("tmBack");
            }
        }
        public string trBack
        {
            get
            {
                return _trBack;
            }
            set
            {
                _trBack = value;
                OnPropertyChanged("trBack");
            }
        }
        public string mlBack
        {
            get
            {
                return _mlBack;
            }
            set
            {
                _mlBack = value;
                OnPropertyChanged("mlBack");
            }
        }
        public string mmBack
        {
            get
            {
                return _mmBack;
            }
            set
            {
                _mmBack = value;
                OnPropertyChanged("mmBack");
            }
        }
        public string mrBack
        {
            get
            {
                return _mrBack;
            }
            set
            {
                _mrBack = value;
                OnPropertyChanged("mrBack");
            }
        }
        public string blBack
        {
            get
            {
                return _blBack;
            }
            set
            {
                _blBack = value;
                OnPropertyChanged("blBack");
            }
        }
        public string bmBack
        {
            get
            {
                return _bmBack;
            }
            set
            {
                _bmBack = value;
                OnPropertyChanged("bmBack");
            }
        }
        public string brBack
        {
            get
            {
                return _brBack;
            }
            set
            {
                _brBack = value;
                OnPropertyChanged("brBack");
            }
        }

        #region PropertyChangedEvent
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
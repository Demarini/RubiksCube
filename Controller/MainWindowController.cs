using Cube.Model;
using Cube.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube.Controller
{
    public class MainWindowController
    {
        int totalMoves;
        string finalString = "";
        string currentCube = "";
        string currentFace = "front";
        private MainWindowViewModel _vm;
        private MainWindowModel _m;
        public MainWindowController(MainWindowViewModel vm, MainWindowModel m)
        {
            _vm = vm;
            _m = m;
        }
        public void SolveCube()
        {
            finalString = "";
            totalMoves = 0;
            Stopwatch t = new Stopwatch();
            t.Start();
            List<string> crossPieces = FindCrossPieces();
            List<string> openCross = FindOpenCross();
            PlaceCrossPieces(openCross, crossPieces, 0);
            int k = totalMoves;
            LineUpCenter();
            t.Stop();
            long totalTime = t.ElapsedMilliseconds;
            FillInCorners();
            FillSecondLayer();
            CreateFinalCross();
            LineUpCenter2();
            FillLastCorners();
            FinishCube();
            File.Delete("solve.txt");
            OptimizeSolveString(finalString);
            WriteSolve(finalString);
            int j = totalMoves;
        }
        public void FinishCube()
        {
            while (_m.BottomFace[0, 0] != "Blue")
            {
                if (_m.BottomFace[0, 0] != "Blue")
                {
                    LeftInverted();
                    Top();
                    Left();
                    TopInverted();
                }
            }
            BottomInverted();
            while (_m.BottomFace[0, 0] != "Blue")
            {
                if (_m.BottomFace[0, 0] != "Blue")
                {
                    LeftInverted();
                    Top();
                    Left();
                    TopInverted();
                }
            }
            BottomInverted();
            while (_m.BottomFace[0, 0] != "Blue")
            {
                if (_m.BottomFace[0, 0] != "Blue")
                {
                    LeftInverted();
                    Top();
                    Left();
                    TopInverted();
                }
            }
            BottomInverted();
            while (_m.BottomFace[0, 0] != "Blue")
            {
                if (_m.BottomFace[0, 0] != "Blue")
                {
                    LeftInverted();
                    Top();
                    Left();
                    TopInverted();
                }
            }
            int interval = 0;
            while ((_m.FrontFace[2, 2] != "White") && interval < 1000)
            {
                interval++;

                BottomInverted();

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
            string whiteRedCorner = _m.FrontFace[0, 2] + "|" + _m.LeftFace[2, 2] + "|" + _m.BottomFace[0, 0];
            string[] whiteRedCornerSplit = whiteRedCorner.Split('|');

            string whiteOrangeCorner = _m.FrontFace[2, 2] + "|" + _m.RightFace[0, 2] + "|" + _m.BottomFace[2, 0];
            string[] whiteOrangeCornerSplit = whiteOrangeCorner.Split('|');

            string yellowOrangeCorner = _m.BackFace[2, 0] + "|" + _m.RightFace[2, 2] + "|" + _m.BottomFace[2, 2];
            string[] yellowOrangeCornerSplit = yellowOrangeCorner.Split('|');

            string yellowRedCorner = _m.BackFace[0, 0] + "|" + _m.LeftFace[0, 2] + "|" + _m.BottomFace[0, 2];
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
            if ((_m.BottomFace[0, 1] == "Blue" && _m.BottomFace[1, 0] == "Blue") || (_m.BottomFace[2, 1] == "Blue" && _m.BottomFace[0, 1] == "Blue"))
            {
                return "front";
            }
            if ((_m.BottomFace[2, 1] == "Blue" && _m.BottomFace[1, 0] == "Blue") || (_m.BottomFace[1, 2] == "Blue" && _m.BottomFace[1, 0] == "Blue"))
            {
                return "left";
            }
            if ((_m.BottomFace[0, 1] == "Blue" && _m.BottomFace[1, 2] == "Blue"))
            {
                return "right";
            }
            if ((_m.BottomFace[1, 2] == "Blue" && _m.BottomFace[2, 1] == "Blue"))
            {
                return "back";
            }
            return "frontsingledot";
        }
        public bool CheckFinalCross()
        {
            if (_m.BottomFace[1, 0] == "Blue" && _m.BottomFace[2, 1] == "Blue" && _m.BottomFace[0, 1] == "Blue" && _m.BottomFace[1, 2] == "Blue")
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
            if (_m.FrontFace[0, 1] != "White" && _m.LeftFace[2, 1] != "Red")
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
            else if (_m.FrontFace[2, 1] != "White" && _m.RightFace[0, 1] != "Orange")
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
            else if (_m.LeftFace[0, 1] != "Red" && _m.BackFace[0, 1] != "Yellow")
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
            else if (_m.RightFace[2, 1] != "Orange" && _m.BackFace[2, 1] != "Yellow")
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
            if (_m.FrontFace[0, 1] == "White" && _m.LeftFace[2, 1] == "Red")
            {
                if (_m.FrontFace[2, 1] == "White" && _m.RightFace[0, 1] == "Orange")
                {
                    if (_m.RightFace[2, 1] == "Orange" && _m.BackFace[2, 1] == "Yellow")
                    {
                        if (_m.LeftFace[0, 1] == "Red" && _m.BackFace[0, 1] == "Yellow")
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
            if (_m.FrontFace[1, 2] != "Blue" && _m.BottomFace[1, 0] != "Blue")
            {
                detectedCubes.Add("front_" + _m.FrontFace[1, 2] + "|" + "bottom_" + _m.BottomFace[1, 0]);
            }
            if (_m.LeftFace[1, 2] != "Blue" && _m.BottomFace[0, 1] != "Blue")
            {
                detectedCubes.Add("left_" + _m.LeftFace[1, 2] + "|" + "bottom_" + _m.BottomFace[0, 1]);
            }
            if (_m.RightFace[1, 2] != "Blue" && _m.BottomFace[2, 1] != "Blue")
            {
                detectedCubes.Add("right_" + _m.RightFace[1, 2] + "|" + "bottom_" + _m.BottomFace[2, 1]);
            }
            if (_m.BackFace[1, 0] != "Blue" && _m.BottomFace[1, 2] != "Blue")
            {
                detectedCubes.Add("back_" + _m.BackFace[1, 0] + "|" + "bottom_" + _m.BottomFace[1, 2]);
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
            if (_m.FrontFace[0, 0] == "Green")
            {
                greenCornerList.Add("front00_" + _m.LeftFace[2, 0] + "|" + _m.TopFace[0, 2]);
            }
            if (_m.FrontFace[2, 0] == "Green")
            {
                greenCornerList.Add("front20_" + _m.RightFace[0, 0] + "|" + _m.TopFace[2, 2]);
            }
            if (_m.FrontFace[0, 2] == "Green")
            {
                greenCornerList.Add("front02_" + _m.LeftFace[2, 2] + "|" + _m.BottomFace[0, 0]);
            }
            if (_m.FrontFace[2, 2] == "Green")
            {
                greenCornerList.Add("front22_" + _m.RightFace[0, 2] + "|" + _m.BottomFace[2, 0]);
            }
            if (_m.BackFace[0, 0] == "Green")
            {
                greenCornerList.Add("back00_" + _m.LeftFace[0, 2] + "|" + _m.BottomFace[0, 2]);
            }
            if (_m.BackFace[2, 0] == "Green")
            {
                greenCornerList.Add("back20_" + _m.RightFace[2, 2] + "|" + _m.BottomFace[2, 2]);
            }
            if (_m.BackFace[0, 2] == "Green")
            {
                greenCornerList.Add("back02_" + _m.LeftFace[0, 0] + "|" + _m.TopFace[0, 0]);
            }
            if (_m.BackFace[2, 2] == "Green")
            {
                greenCornerList.Add("back22_" + _m.RightFace[2, 0] + "|" + _m.TopFace[2, 0]);
            }
            if (_m.LeftFace[0, 0] == "Green")
            {
                greenCornerList.Add("left00_" + _m.TopFace[0, 0] + "|" + _m.BackFace[0, 2]);
            }
            if (_m.LeftFace[2, 0] == "Green")
            {
                greenCornerList.Add("left20_" + _m.TopFace[0, 2] + "|" + _m.FrontFace[0, 0]);
            }
            if (_m.LeftFace[0, 2] == "Green")
            {
                greenCornerList.Add("left02_" + _m.BottomFace[0, 2] + "|" + _m.BackFace[0, 0]);
            }
            if (_m.LeftFace[2, 2] == "Green")
            {
                greenCornerList.Add("left22_" + _m.BottomFace[0, 0] + "|" + _m.FrontFace[0, 2]);
            }
            if (_m.RightFace[0, 0] == "Green")
            {
                greenCornerList.Add("right00_" + _m.TopFace[2, 2] + "|" + _m.FrontFace[2, 0]);
            }
            if (_m.RightFace[2, 0] == "Green")
            {
                greenCornerList.Add("right20_" + _m.TopFace[2, 0] + "|" + _m.BackFace[2, 2]);
            }
            if (_m.RightFace[0, 2] == "Green")
            {
                greenCornerList.Add("right02_" + _m.BottomFace[2, 0] + "|" + _m.FrontFace[2, 2]);
            }
            if (_m.RightFace[2, 2] == "Green")
            {
                greenCornerList.Add("right22_" + _m.BottomFace[2, 2] + "|" + _m.BackFace[2, 0]);
            }
            if (_m.TopFace[0, 0] == "Green")
            {
                greenCornerList.Add("top00_" + _m.BackFace[0, 2] + "|" + _m.LeftFace[0, 0]);
            }
            if (_m.TopFace[2, 0] == "Green")
            {
                greenCornerList.Add("top20_" + _m.RightFace[2, 0] + "|" + _m.BackFace[2, 2]);
            }
            if (_m.TopFace[0, 2] == "Green")
            {
                greenCornerList.Add("top02_" + _m.FrontFace[0, 0] + "|" + _m.LeftFace[2, 0]);
            }
            if (_m.TopFace[2, 2] == "Green")
            {
                greenCornerList.Add("top22_" + _m.FrontFace[2, 0] + "|" + _m.RightFace[0, 0]);
            }
            if (_m.BottomFace[0, 0] == "Green")
            {
                greenCornerList.Add("bottom00_" + _m.FrontFace[0, 2] + "|" + _m.LeftFace[2, 2]);
            }
            if (_m.BottomFace[2, 0] == "Green")
            {
                greenCornerList.Add("bottom20_" + _m.FrontFace[2, 2] + "|" + _m.RightFace[0, 2]);
            }
            if (_m.BottomFace[0, 2] == "Green")
            {
                greenCornerList.Add("bottom02_" + _m.BackFace[0, 0] + "|" + _m.LeftFace[0, 2]);
            }
            if (_m.BottomFace[2, 2] == "Green")
            {
                greenCornerList.Add("bottom22_" + _m.BackFace[2, 0] + "|" + _m.RightFace[2, 2]);
            }
            return greenCornerList;
        }
        public bool CheckLineUpCenter()
        {
            if (_m.FrontFace[1, 0] == "White")
            {
                if (_m.LeftFace[1, 0] == "Red")
                {
                    if (_m.RightFace[1, 0] == "Orange")
                    {
                        if (_m.BackFace[1, 2] == "Yellow")
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
            if (_m.FrontFace[1, 2] == "White")
            {
                if (_m.LeftFace[1, 2] == "Red")
                {
                    if (_m.RightFace[1, 2] == "Orange")
                    {
                        if (_m.BackFace[1, 0] == "Yellow")
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
                if (_m.FrontFace[1, 0] == "White")
                {
                    if (_m.RightFace[1, 0] == "Orange")
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
                    else if (_m.BackFace[1, 2] == "Yellow")
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
                    else if (_m.LeftFace[1, 0] == "Red")
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
                else if (_m.RightFace[1, 0] == "Orange")
                {
                    if (_m.BackFace[1, 2] == "Yellow")
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
                    else if (_m.LeftFace[1, 0] == "Red")
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
                    else if (_m.FrontFace[1, 0] == "White")
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
                else if (_m.BackFace[1, 2] == "Yellow")
                {
                    if (_m.LeftFace[1, 0] == "Red")
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
                    else if (_m.FrontFace[1, 0] == "White")
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
                    else if (_m.RightFace[1, 0] == "Orange")
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
                else if (_m.LeftFace[1, 0] == "Red")
                {
                    if (_m.FrontFace[1, 0] == "White")
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
                    else if (_m.RightFace[1, 0] == "Orange")
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
                    else if (_m.BackFace[1, 2] == "Yellow")
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
            RefreshScreen(_m.CurrentFace);
        }
        public void LineUpCenter2()
        {
            bool lineUpCenter = CheckLineUpCenter2();
            while (!lineUpCenter)
            {
                if (_m.FrontFace[1, 2] == "White")
                {
                    if (_m.RightFace[1, 2] == "Orange")//good
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
                    else if (_m.BackFace[1, 0] == "Yellow")//good
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
                    else if (_m.LeftFace[1, 2] == "Red")//good
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
                else if (_m.RightFace[1, 2] == "Orange")
                {
                    if (_m.BackFace[1, 0] == "Yellow")//good
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
                    else if (_m.LeftFace[1, 2] == "Red")//good
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
                    else if (_m.FrontFace[1, 2] == "White")//good
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
                else if (_m.BackFace[1, 0] == "Yellow")
                {//
                    if (_m.LeftFace[1, 2] == "Red")
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
                    else if (_m.FrontFace[1, 2] == "White")//good
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
                    else if (_m.RightFace[1, 2] == "Orange")
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
                else if (_m.LeftFace[1, 2] == "Red")
                {
                    if (_m.FrontFace[1, 2] == "White")
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
                    else if (_m.RightFace[1, 2] == "Orange")
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
                    else if (_m.BackFace[1, 0] == "Yellow")
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
            RefreshScreen(_m.CurrentFace);
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
                            if (_m.TopFace[1, 2] != "Green")
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
                            if (_m.TopFace[0, 1] != "Green")
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
                            if (_m.TopFace[2, 1] != "Green")
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
                            if (_m.TopFace[1, 0] != "Green")
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
                            if (_m.TopFace[0, 1] != "Green")
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
                            if (_m.TopFace[2, 1] != "Green")
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
                            if (_m.TopFace[0, 1] != "Green")
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
                            if (_m.TopFace[1, 0] != "Green")
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
                            if (_m.TopFace[1, 2] != "Green")
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
                            if (_m.TopFace[2, 1] != "Green")
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
                            if (_m.TopFace[1, 2] != "Green")
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
                            if (_m.TopFace[1, 0] != "Green")
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
                            if (_m.TopFace[1, 2] != "Green")
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
                            if (_m.TopFace[1, 0] != "Green")
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
                            if (_m.TopFace[0, 1] != "Green")
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
                            if (_m.TopFace[2, 1] != "Green")
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
            if (_m.TopFace[1, 0] != "Green")
            {
                openCrossLocation.Add("top[1,0]");
            }
            if (_m.TopFace[1, 2] != "Green")
            {
                openCrossLocation.Add("top[1,2]");
            }
            if (_m.TopFace[0, 1] != "Green")
            {
                openCrossLocation.Add("top[0,1]");
            }
            if (_m.TopFace[2, 1] != "Green")
            {
                openCrossLocation.Add("top[2,1]");
            }
            return openCrossLocation;
        }
        public List<string> FindCrossPieces()
        {
            List<string> crossPieceLocation = new List<string>();
            if (_m.FrontFace[1, 0] == "Green")
            {
                crossPieceLocation.Add("front[1,0]");
            }
            if (_m.FrontFace[1, 2] == "Green")
            {
                crossPieceLocation.Add("front[1,2]");
            }
            if (_m.FrontFace[0, 1] == "Green")
            {
                crossPieceLocation.Add("front[0,1]");
            }
            if (_m.FrontFace[2, 1] == "Green")
            {
                crossPieceLocation.Add("front[2,1]");
            }
            if (_m.BackFace[1, 0] == "Green")
            {
                crossPieceLocation.Add("back[1,0]");
            }
            if (_m.BackFace[1, 2] == "Green")
            {
                crossPieceLocation.Add("back[1,2]");
            }
            if (_m.BackFace[0, 1] == "Green")
            {
                crossPieceLocation.Add("back[0,1]");
            }
            if (_m.BackFace[2, 1] == "Green")
            {
                crossPieceLocation.Add("back[2,1]");
            }
            if (_m.LeftFace[1, 0] == "Green")
            {
                crossPieceLocation.Add("left[1,0]");
            }
            if (_m.LeftFace[1, 2] == "Green")
            {
                crossPieceLocation.Add("left[1,2]");
            }
            if (_m.LeftFace[0, 1] == "Green")
            {
                crossPieceLocation.Add("left[0,1]");
            }
            if (_m.LeftFace[2, 1] == "Green")
            {
                crossPieceLocation.Add("left[2,1]");
            }
            if (_m.RightFace[1, 0] == "Green")
            {
                crossPieceLocation.Add("right[1,0]");
            }
            if (_m.RightFace[1, 2] == "Green")
            {
                crossPieceLocation.Add("right[1,2]");
            }
            if (_m.RightFace[0, 1] == "Green")
            {
                crossPieceLocation.Add("right[0,1]");
            }
            if (_m.RightFace[2, 1] == "Green")
            {
                crossPieceLocation.Add("right[2,1]");
            }
            if (_m.BottomFace[1, 0] == "Green")
            {
                crossPieceLocation.Add("bottom[1,0]");
            }
            if (_m.BottomFace[1, 2] == "Green")
            {
                crossPieceLocation.Add("bottom[1,2]");
            }
            if (_m.BottomFace[0, 1] == "Green")
            {
                crossPieceLocation.Add("bottom[0,1]");
            }
            if (_m.BottomFace[2, 1] == "Green")
            {
                crossPieceLocation.Add("bottom[2,1]");
            }
            return crossPieceLocation;
        }
        public void Right()
        {
            finalString += "R, ";
            totalMoves++;
            string[,] tempRight = CopyFace(_m.RightFace);
            string[,] tempFront = CopyFace(_m.FrontFace);
            string[,] tempBack = CopyFace(_m.BackFace);
            string[,] tempTop = CopyFace(_m.TopFace);
            string[,] tempBottom = CopyFace(_m.BottomFace);
            string[,] tempLeft = CopyFace(_m.LeftFace);

            _m.RightFace[0, 0] = tempRight[0, 2];
            _m.RightFace[1, 0] = tempRight[0, 1];
            _m.RightFace[2, 0] = tempRight[0, 0];
            _m.RightFace[2, 1] = tempRight[1, 0];
            _m.RightFace[2, 2] = tempRight[2, 0];
            _m.RightFace[0, 2] = tempRight[2, 2];
            _m.RightFace[0, 1] = tempRight[1, 2];
            _m.RightFace[1, 1] = tempRight[1, 1];
            _m.RightFace[1, 2] = tempRight[2, 1];

            _m.FrontFace[2, 0] = tempBottom[2, 0];
            _m.FrontFace[2, 1] = tempBottom[2, 1];
            _m.FrontFace[2, 2] = tempBottom[2, 2];

            _m.TopFace[2, 0] = tempFront[2, 0];
            _m.TopFace[2, 1] = tempFront[2, 1];
            _m.TopFace[2, 2] = tempFront[2, 2];

            _m.BackFace[2, 0] = tempTop[2, 0];
            _m.BackFace[2, 1] = tempTop[2, 1];
            _m.BackFace[2, 2] = tempTop[2, 2];

            _m.BottomFace[2, 0] = tempBack[2, 0];
            _m.BottomFace[2, 1] = tempBack[2, 1];
            _m.BottomFace[2, 2] = tempBack[2, 2];

            RefreshScreen(_m.CurrentFace);
        }

        public void RightInverted()
        {
            finalString += "Ri, ";
            totalMoves++;
            string[,] tempRight = CopyFace(_m.RightFace);
            string[,] tempFront = CopyFace(_m.FrontFace);
            string[,] tempBack = CopyFace(_m.BackFace);
            string[,] tempTop = CopyFace(_m.TopFace);
            string[,] tempBottom = CopyFace(_m.BottomFace);
            string[,] tempLeft = CopyFace(_m.LeftFace);

            _m.RightFace[0, 0] = tempRight[2, 0];
            _m.RightFace[1, 0] = tempRight[2, 1];
            _m.RightFace[2, 0] = tempRight[2, 2];
            _m.RightFace[2, 1] = tempRight[1, 2];
            _m.RightFace[2, 2] = tempRight[0, 2];
            _m.RightFace[0, 2] = tempRight[0, 0];
            _m.RightFace[0, 1] = tempRight[1, 0];
            _m.RightFace[1, 1] = tempRight[1, 1];
            _m.RightFace[1, 2] = tempRight[0, 1];

            _m.BottomFace[2, 0] = tempFront[2, 0];
            _m.BottomFace[2, 1] = tempFront[2, 1];
            _m.BottomFace[2, 2] = tempFront[2, 2];

            _m.FrontFace[2, 0] = tempTop[2, 0];
            _m.FrontFace[2, 1] = tempTop[2, 1];
            _m.FrontFace[2, 2] = tempTop[2, 2];

            _m.TopFace[2, 0] = tempBack[2, 0];
            _m.TopFace[2, 1] = tempBack[2, 1];
            _m.TopFace[2, 2] = tempBack[2, 2];

            _m.BackFace[2, 0] = tempBottom[2, 0];
            _m.BackFace[2, 1] = tempBottom[2, 1];
            _m.BackFace[2, 2] = tempBottom[2, 2];

            RefreshScreen(_m.CurrentFace);
        }

        public void LeftInverted()
        {
            finalString += "Li, ";
            totalMoves++;
            string[,] tempRight = CopyFace(_m.RightFace);
            string[,] tempFront = CopyFace(_m.FrontFace);
            string[,] tempBack = CopyFace(_m.BackFace);
            string[,] tempTop = CopyFace(_m.TopFace);
            string[,] tempBottom = CopyFace(_m.BottomFace);
            string[,] tempLeft = CopyFace(_m.LeftFace);

            _m.LeftFace[0, 0] = tempLeft[2, 0];
            _m.LeftFace[1, 0] = tempLeft[2, 1];
            _m.LeftFace[2, 0] = tempLeft[2, 2];
            _m.LeftFace[2, 1] = tempLeft[1, 2];
            _m.LeftFace[2, 2] = tempLeft[0, 2];
            _m.LeftFace[0, 2] = tempLeft[0, 0];
            _m.LeftFace[0, 1] = tempLeft[1, 0];
            _m.LeftFace[1, 1] = tempLeft[1, 1];
            _m.LeftFace[1, 2] = tempLeft[0, 1];

            _m.FrontFace[0, 0] = tempBottom[0, 0];
            _m.FrontFace[0, 1] = tempBottom[0, 1];
            _m.FrontFace[0, 2] = tempBottom[0, 2];

            _m.TopFace[0, 0] = tempFront[0, 0];
            _m.TopFace[0, 1] = tempFront[0, 1];
            _m.TopFace[0, 2] = tempFront[0, 2];

            _m.BackFace[0, 0] = tempTop[0, 0];
            _m.BackFace[0, 1] = tempTop[0, 1];
            _m.BackFace[0, 2] = tempTop[0, 2];

            _m.BottomFace[0, 0] = tempBack[0, 0];
            _m.BottomFace[0, 1] = tempBack[0, 1];
            _m.BottomFace[0, 2] = tempBack[0, 2];

            RefreshScreen(_m.CurrentFace);
        }
        public void Left()
        {
            finalString += "L, ";
            totalMoves++;
            string[,] tempRight = CopyFace(_m.RightFace);
            string[,] tempFront = CopyFace(_m.FrontFace);
            string[,] tempBack = CopyFace(_m.BackFace);
            string[,] tempTop = CopyFace(_m.TopFace);
            string[,] tempBottom = CopyFace(_m.BottomFace);
            string[,] tempLeft = CopyFace(_m.LeftFace);

            _m.LeftFace[0, 0] = tempLeft[0, 2];
            _m.LeftFace[1, 0] = tempLeft[0, 1];
            _m.LeftFace[2, 0] = tempLeft[0, 0];
            _m.LeftFace[2, 1] = tempLeft[1, 0];
            _m.LeftFace[2, 2] = tempLeft[2, 0];
            _m.LeftFace[0, 2] = tempLeft[2, 2];
            _m.LeftFace[0, 1] = tempLeft[1, 2];
            _m.LeftFace[1, 1] = tempLeft[1, 1];
            _m.LeftFace[1, 2] = tempLeft[2, 1];

            _m.BottomFace[0, 0] = tempFront[0, 0];
            _m.BottomFace[0, 1] = tempFront[0, 1];
            _m.BottomFace[0, 2] = tempFront[0, 2];

            _m.FrontFace[0, 0] = tempTop[0, 0];
            _m.FrontFace[0, 1] = tempTop[0, 1];
            _m.FrontFace[0, 2] = tempTop[0, 2];

            _m.TopFace[0, 0] = tempBack[0, 0];
            _m.TopFace[0, 1] = tempBack[0, 1];
            _m.TopFace[0, 2] = tempBack[0, 2];

            _m.BackFace[0, 0] = tempBottom[0, 0];
            _m.BackFace[0, 1] = tempBottom[0, 1];
            _m.BackFace[0, 2] = tempBottom[0, 2];

            RefreshScreen(_m.CurrentFace);
        }

        public void Top()
        {
            finalString += "U, ";
            totalMoves++;
            string[,] tempRight = CopyFace(_m.RightFace);
            string[,] tempFront = CopyFace(_m.FrontFace);
            string[,] tempBack = CopyFace(_m.BackFace);
            string[,] tempTop = CopyFace(_m.TopFace);
            string[,] tempBottom = CopyFace(_m.BottomFace);
            string[,] tempLeft = CopyFace(_m.LeftFace);

            _m.TopFace[0, 0] = tempTop[0, 2];
            _m.TopFace[1, 0] = tempTop[0, 1];
            _m.TopFace[2, 0] = tempTop[0, 0];
            _m.TopFace[2, 1] = tempTop[1, 0];
            _m.TopFace[2, 2] = tempTop[2, 0];
            _m.TopFace[0, 2] = tempTop[2, 2];
            _m.TopFace[0, 1] = tempTop[1, 2];
            _m.TopFace[1, 1] = tempTop[1, 1];
            _m.TopFace[1, 2] = tempTop[2, 1];

            _m.FrontFace[0, 0] = tempRight[0, 0];
            _m.FrontFace[1, 0] = tempRight[1, 0];
            _m.FrontFace[2, 0] = tempRight[2, 0];

            _m.LeftFace[0, 0] = tempFront[0, 0];
            _m.LeftFace[1, 0] = tempFront[1, 0];
            _m.LeftFace[2, 0] = tempFront[2, 0];

            _m.BackFace[2, 2] = tempLeft[0, 0];
            _m.BackFace[1, 2] = tempLeft[1, 0];
            _m.BackFace[0, 2] = tempLeft[2, 0];

            _m.RightFace[0, 0] = tempBack[2, 2];
            _m.RightFace[1, 0] = tempBack[1, 2];
            _m.RightFace[2, 0] = tempBack[0, 2];

            RefreshScreen(_m.CurrentFace);
        }

        public void TopInverted()
        {
            finalString += "Ui, ";
            totalMoves++;
            string[,] tempRight = CopyFace(_m.RightFace);
            string[,] tempFront = CopyFace(_m.FrontFace);
            string[,] tempBack = CopyFace(_m.BackFace);
            string[,] tempTop = CopyFace(_m.TopFace);
            string[,] tempBottom = CopyFace(_m.BottomFace);
            string[,] tempLeft = CopyFace(_m.LeftFace);

            _m.TopFace[0, 0] = tempTop[2, 0];
            _m.TopFace[1, 0] = tempTop[2, 1];
            _m.TopFace[2, 0] = tempTop[2, 2];
            _m.TopFace[2, 1] = tempTop[1, 2];
            _m.TopFace[2, 2] = tempTop[0, 2];
            _m.TopFace[0, 2] = tempTop[0, 0];
            _m.TopFace[0, 1] = tempTop[1, 0];
            _m.TopFace[1, 1] = tempTop[1, 1];
            _m.TopFace[1, 2] = tempTop[0, 1];

            _m.RightFace[0, 0] = tempFront[0, 0];
            _m.RightFace[1, 0] = tempFront[1, 0];
            _m.RightFace[2, 0] = tempFront[2, 0];

            _m.FrontFace[0, 0] = tempLeft[0, 0];
            _m.FrontFace[1, 0] = tempLeft[1, 0];
            _m.FrontFace[2, 0] = tempLeft[2, 0];

            _m.LeftFace[2, 0] = tempBack[0, 2];
            _m.LeftFace[1, 0] = tempBack[1, 2];
            _m.LeftFace[0, 0] = tempBack[2, 2];

            _m.BackFace[0, 2] = tempRight[2, 0];
            _m.BackFace[1, 2] = tempRight[1, 0];
            _m.BackFace[2, 2] = tempRight[0, 0];

            RefreshScreen(_m.CurrentFace);
        }

        public void BottomInverted()
        {
            finalString += "Di, ";
            totalMoves++;
            string[,] tempRight = CopyFace(_m.RightFace);
            string[,] tempFront = CopyFace(_m.FrontFace);
            string[,] tempBack = CopyFace(_m.BackFace);
            string[,] tempTop = CopyFace(_m.TopFace);
            string[,] tempBottom = CopyFace(_m.BottomFace);
            string[,] tempLeft = CopyFace(_m.LeftFace);

            _m.BottomFace[0, 0] = tempBottom[2, 0];
            _m.BottomFace[1, 0] = tempBottom[2, 1];
            _m.BottomFace[2, 0] = tempBottom[2, 2];
            _m.BottomFace[2, 1] = tempBottom[1, 2];
            _m.BottomFace[2, 2] = tempBottom[0, 2];
            _m.BottomFace[0, 2] = tempBottom[0, 0];
            _m.BottomFace[0, 1] = tempBottom[1, 0];
            _m.BottomFace[1, 1] = tempBottom[1, 1];
            _m.BottomFace[1, 2] = tempBottom[0, 1];

            _m.FrontFace[0, 2] = tempRight[0, 2];
            _m.FrontFace[1, 2] = tempRight[1, 2];
            _m.FrontFace[2, 2] = tempRight[2, 2];

            _m.LeftFace[0, 2] = tempFront[0, 2];
            _m.LeftFace[1, 2] = tempFront[1, 2];
            _m.LeftFace[2, 2] = tempFront[2, 2];

            _m.BackFace[0, 0] = tempLeft[2, 2];
            _m.BackFace[1, 0] = tempLeft[1, 2];
            _m.BackFace[2, 0] = tempLeft[0, 2];

            _m.RightFace[2, 2] = tempBack[0, 0];
            _m.RightFace[1, 2] = tempBack[1, 0];
            _m.RightFace[0, 2] = tempBack[2, 0];

            RefreshScreen(_m.CurrentFace);
        }

        public void Bottom()
        {
            finalString += "D, ";
            totalMoves++;
            string[,] tempRight = CopyFace(_m.RightFace);
            string[,] tempFront = CopyFace(_m.FrontFace);
            string[,] tempBack = CopyFace(_m.BackFace);
            string[,] tempTop = CopyFace(_m.TopFace);
            string[,] tempBottom = CopyFace(_m.BottomFace);
            string[,] tempLeft = CopyFace(_m.LeftFace);

            _m.BottomFace[0, 0] = tempBottom[0, 2];
            _m.BottomFace[1, 0] = tempBottom[0, 1];
            _m.BottomFace[2, 0] = tempBottom[0, 0];
            _m.BottomFace[2, 1] = tempBottom[1, 0];
            _m.BottomFace[2, 2] = tempBottom[2, 0];
            _m.BottomFace[0, 2] = tempBottom[2, 2];
            _m.BottomFace[0, 1] = tempBottom[1, 2];
            _m.BottomFace[1, 1] = tempBottom[1, 1];
            _m.BottomFace[1, 2] = tempBottom[2, 1];

            _m.RightFace[0, 2] = tempFront[0, 2];
            _m.RightFace[1, 2] = tempFront[1, 2];
            _m.RightFace[2, 2] = tempFront[2, 2];

            _m.FrontFace[0, 2] = tempLeft[0, 2];
            _m.FrontFace[1, 2] = tempLeft[1, 2];
            _m.FrontFace[2, 2] = tempLeft[2, 2];

            _m.LeftFace[2, 2] = tempBack[0, 0];
            _m.LeftFace[1, 2] = tempBack[1, 0];
            _m.LeftFace[0, 2] = tempBack[2, 0];

            _m.BackFace[0, 0] = tempRight[2, 2];
            _m.BackFace[1, 0] = tempRight[1, 2];
            _m.BackFace[2, 0] = tempRight[0, 2];

            RefreshScreen(_m.CurrentFace);
        }

        public void Front()
        {
            finalString += "F, ";
            totalMoves++;
            string[,] tempRight = CopyFace(_m.RightFace);
            string[,] tempFront = CopyFace(_m.FrontFace);
            string[,] tempBack = CopyFace(_m.BackFace);
            string[,] tempTop = CopyFace(_m.TopFace);
            string[,] tempBottom = CopyFace(_m.BottomFace);
            string[,] tempLeft = CopyFace(_m.LeftFace);

            _m.FrontFace[0, 0] = tempFront[0, 2];
            _m.FrontFace[1, 0] = tempFront[0, 1];
            _m.FrontFace[2, 0] = tempFront[0, 0];
            _m.FrontFace[2, 1] = tempFront[1, 0];
            _m.FrontFace[2, 2] = tempFront[2, 0];
            _m.FrontFace[0, 2] = tempFront[2, 2];
            _m.FrontFace[0, 1] = tempFront[1, 2];
            _m.FrontFace[1, 1] = tempFront[1, 1];
            _m.FrontFace[1, 2] = tempFront[2, 1];

            _m.TopFace[0, 2] = tempLeft[2, 2];
            _m.TopFace[1, 2] = tempLeft[2, 1];
            _m.TopFace[2, 2] = tempLeft[2, 0];

            _m.RightFace[0, 0] = tempTop[0, 2];
            _m.RightFace[0, 1] = tempTop[1, 2];
            _m.RightFace[0, 2] = tempTop[2, 2];

            _m.BottomFace[0, 0] = tempRight[0, 2];
            _m.BottomFace[1, 0] = tempRight[0, 1];
            _m.BottomFace[2, 0] = tempRight[0, 0];

            _m.LeftFace[2, 0] = tempBottom[0, 0];
            _m.LeftFace[2, 1] = tempBottom[1, 0];
            _m.LeftFace[2, 2] = tempBottom[2, 0];

            RefreshScreen(_m.CurrentFace);
        }

        public void FrontInverted()
        {
            finalString += "Fi, ";
            totalMoves++;
            string[,] tempRight = CopyFace(_m.RightFace);
            string[,] tempFront = CopyFace(_m.FrontFace);
            string[,] tempBack = CopyFace(_m.BackFace);
            string[,] tempTop = CopyFace(_m.TopFace);
            string[,] tempBottom = CopyFace(_m.BottomFace);
            string[,] tempLeft = CopyFace(_m.LeftFace);

            _m.FrontFace[0, 0] = tempFront[2, 0];
            _m.FrontFace[1, 0] = tempFront[2, 1];
            _m.FrontFace[2, 0] = tempFront[2, 2];
            _m.FrontFace[2, 1] = tempFront[1, 2];
            _m.FrontFace[2, 2] = tempFront[0, 2];
            _m.FrontFace[0, 2] = tempFront[0, 0];
            _m.FrontFace[0, 1] = tempFront[1, 0];
            _m.FrontFace[1, 1] = tempFront[1, 1];
            _m.FrontFace[1, 2] = tempFront[0, 1];

            _m.LeftFace[2, 2] = tempTop[0, 2];
            _m.LeftFace[2, 1] = tempTop[1, 2];
            _m.LeftFace[2, 0] = tempTop[2, 2];

            _m.TopFace[0, 2] = tempRight[0, 0];
            _m.TopFace[1, 2] = tempRight[0, 1];
            _m.TopFace[2, 2] = tempRight[0, 2];

            _m.RightFace[0, 2] = tempBottom[0, 0];
            _m.RightFace[0, 1] = tempBottom[1, 0];
            _m.RightFace[0, 0] = tempBottom[2, 0];

            _m.BottomFace[0, 0] = tempLeft[2, 0];
            _m.BottomFace[1, 0] = tempLeft[2, 1];
            _m.BottomFace[2, 0] = tempLeft[2, 2];

            RefreshScreen(_m.CurrentFace);
        }

        public void BackInverted()
        {
            finalString += "Bi, ";
            totalMoves++;
            string[,] tempRight = CopyFace(_m.RightFace);
            string[,] tempFront = CopyFace(_m.FrontFace);
            string[,] tempBack = CopyFace(_m.BackFace);
            string[,] tempTop = CopyFace(_m.TopFace);
            string[,] tempBottom = CopyFace(_m.BottomFace);
            string[,] tempLeft = CopyFace(_m.LeftFace);

            _m.BackFace[0, 0] = tempBack[2, 0];
            _m.BackFace[1, 0] = tempBack[2, 1];
            _m.BackFace[2, 0] = tempBack[2, 2];
            _m.BackFace[2, 1] = tempBack[1, 2];
            _m.BackFace[2, 2] = tempBack[0, 2];
            _m.BackFace[0, 2] = tempBack[0, 0];
            _m.BackFace[0, 1] = tempBack[1, 0];
            _m.BackFace[1, 1] = tempBack[1, 1];
            _m.BackFace[1, 2] = tempBack[0, 1];

            _m.LeftFace[0, 2] = tempBottom[2, 2];
            _m.LeftFace[0, 1] = tempBottom[1, 2];  //CORRECT
            _m.LeftFace[0, 0] = tempBottom[0, 2];

            _m.TopFace[0, 0] = tempLeft[0, 2];
            _m.TopFace[1, 0] = tempLeft[0, 1]; //CORRECT
            _m.TopFace[2, 0] = tempLeft[0, 0];

            _m.RightFace[2, 2] = tempTop[2, 0];
            _m.RightFace[2, 1] = tempTop[1, 0];
            _m.RightFace[2, 0] = tempTop[0, 0];

            _m.BottomFace[0, 2] = tempRight[2, 2];
            _m.BottomFace[1, 2] = tempRight[2, 1];
            _m.BottomFace[2, 2] = tempRight[2, 0];

            RefreshScreen(_m.CurrentFace);
        }

        public void Back()
        {
            finalString += "B, ";
            totalMoves++;
            string[,] tempRight = CopyFace(_m.RightFace);
            string[,] tempFront = CopyFace(_m.FrontFace);
            string[,] tempBack = CopyFace(_m.BackFace);
            string[,] tempTop = CopyFace(_m.TopFace);
            string[,] tempBottom = CopyFace(_m.BottomFace);
            string[,] tempLeft = CopyFace(_m.LeftFace);

            _m.BackFace[0, 0] = tempBack[0, 2];
            _m.BackFace[1, 0] = tempBack[0, 1];
            _m.BackFace[2, 0] = tempBack[0, 0];
            _m.BackFace[2, 1] = tempBack[1, 0];
            _m.BackFace[2, 2] = tempBack[2, 0];
            _m.BackFace[0, 2] = tempBack[2, 2];
            _m.BackFace[0, 1] = tempBack[1, 2];
            _m.BackFace[1, 1] = tempBack[1, 1];
            _m.BackFace[1, 2] = tempBack[2, 1];

            _m.LeftFace[0, 2] = tempTop[0, 0];
            _m.LeftFace[0, 1] = tempTop[1, 0];
            _m.LeftFace[0, 0] = tempTop[2, 0];

            _m.TopFace[0, 0] = tempRight[2, 0];
            _m.TopFace[1, 0] = tempRight[2, 1];
            _m.TopFace[2, 0] = tempRight[2, 2];

            _m.RightFace[2, 2] = tempBottom[0, 2];
            _m.RightFace[2, 1] = tempBottom[1, 2];
            _m.RightFace[2, 0] = tempBottom[2, 2];

            _m.BottomFace[0, 2] = tempLeft[0, 0];
            _m.BottomFace[1, 2] = tempLeft[0, 1];
            _m.BottomFace[2, 2] = tempLeft[0, 2];

            RefreshScreen(_m.CurrentFace);
        }
        public void RefreshScreen(string face)
        {
            switch (face)
            {
                case "front":
                    ShowFace(_m.FrontFace);
                    break;
                case "left":
                    ShowFace(_m.LeftFace);
                    break;
                case "right":
                    ShowFace(_m.RightFace);
                    break;
                case "top":
                    ShowFace(_m.TopFace);
                    break;
                case "back":
                    ShowFace(_m.BackFace);
                    break;
                case "bottom":
                    ShowFace(_m.BottomFace);
                    break;
            }
        }
        public void ShowFace(string[,] side)
        {
            _m.tlBack = side[0, 0];
            _m.tmBack = side[1, 0];
            _m.trBack = side[2, 0];
            _m.mlBack = side[0, 1];
            _m.mmBack = side[1, 1];
            _m.mrBack = side[2, 1];
            _m.blBack = side[0, 2];
            _m.bmBack = side[1, 2];
            _m.brBack = side[2, 2];
        }
        public string[,] SetOriginalColor(string color)
        {
            string[,] side = new string[3, 3];
            for (int i = 0; i < side.GetLength(0); i++)
            {
                for (int j = 0; j < side.GetLength(1); j++)
                {
                    side[i, j] = color;
                }
            }
            return side;
        }
        public void SetCubeColor(string color)
        {
            switch (_m.CurrentCube)
            {
                case "fronttl":
                    _m.FrontFace[0, 0] = color;
                    break;
                case "fronttm":
                    _m.FrontFace[1, 0] = color;
                    break;
                case "fronttr":
                    _m.FrontFace[2, 0] = color;
                    break;
                case "frontml":
                    _m.FrontFace[0, 1] = color;
                    break;
                case "frontmr":
                    _m.FrontFace[2, 1] = color;
                    break;
                case "frontbl":
                    _m.FrontFace[0, 2] = color;
                    break;
                case "frontbm":
                    _m.FrontFace[1, 2] = color;
                    break;
                case "frontbr":
                    _m.FrontFace[2, 2] = color;
                    break;
                case "backtl":
                    _m.BackFace[0, 0] = color;
                    break;
                case "backtm":
                    _m.BackFace[1, 0] = color;
                    break;
                case "backtr":
                    _m.BackFace[2, 0] = color;
                    break;
                case "backml":
                    _m.BackFace[0, 1] = color;
                    break;
                case "backmr":
                    _m.BackFace[2, 1] = color;
                    break;
                case "backbl":
                    _m.BackFace[0, 2] = color;
                    break;
                case "backbm":
                    _m.BackFace[1, 2] = color;
                    break;
                case "backbr":
                    _m.BackFace[2, 2] = color;
                    break;
                case "toptl":
                    _m.TopFace[0, 0] = color;
                    break;
                case "toptm":
                    _m.TopFace[1, 0] = color;
                    break;
                case "toptr":
                    _m.TopFace[2, 0] = color;
                    break;
                case "topml":
                    _m.TopFace[0, 1] = color;
                    break;
                case "topmr":
                    _m.TopFace[2, 1] = color;
                    break;
                case "topbl":
                    _m.TopFace[0, 2] = color;
                    break;
                case "topbm":
                    _m.TopFace[1, 2] = color;
                    break;
                case "topbr":
                    _m.TopFace[2, 2] = color;
                    break;
                case "bottomtl":
                    _m.BottomFace[0, 0] = color;
                    break;
                case "bottomtm":
                    _m.BottomFace[1, 0] = color;
                    break;
                case "bottomtr":
                    _m.BottomFace[2, 0] = color;
                    break;
                case "bottomml":
                    _m.BottomFace[0, 1] = color;
                    break;
                case "bottommr":
                    _m.BottomFace[2, 1] = color;
                    break;
                case "bottombl":
                    _m.BottomFace[0, 2] = color;
                    break;
                case "bottombm":
                    _m.BottomFace[1, 2] = color;
                    break;
                case "bottombr":
                    _m.BottomFace[2, 2] = color;
                    break;
                case "lefttl":
                    _m.LeftFace[0, 0] = color;
                    break;
                case "lefttm":
                    _m.LeftFace[1, 0] = color;
                    break;
                case "lefttr":
                    _m.LeftFace[2, 0] = color;
                    break;
                case "leftml":
                    _m.LeftFace[0, 1] = color;
                    break;
                case "leftmr":
                    _m.LeftFace[2, 1] = color;
                    break;
                case "leftbl":
                    _m.LeftFace[0, 2] = color;
                    break;
                case "leftbm":
                    _m.LeftFace[1, 2] = color;
                    break;
                case "leftbr":
                    _m.LeftFace[2, 2] = color;
                    break;
                case "righttl":
                    _m.RightFace[0, 0] = color;
                    break;
                case "righttm":
                    _m.RightFace[1, 0] = color;
                    break;
                case "righttr":
                    _m.RightFace[2, 0] = color;
                    break;
                case "rightml":
                    _m.RightFace[0, 1] = color;
                    break;
                case "rightmr":
                    _m.RightFace[2, 1] = color;
                    break;
                case "rightbl":
                    _m.RightFace[0, 2] = color;
                    break;
                case "rightbm":
                    _m.RightFace[1, 2] = color;
                    break;
                case "rightbr":
                    _m.RightFace[2, 2] = color;
                    break;
            }
            _m.ColorVisibility = System.Windows.Visibility.Hidden;
            RefreshScreen(_m.CurrentFace);
        }
        public void SelectColor(string face)
        {
            _m.CurrentCube = _m.CurrentFace + face;
            _m.ColorVisibility = System.Windows.Visibility.Visible;
        }
        public void ClearCube()
        {
            _m.FrontFace = SetOriginalColor("White");
            _m.LeftFace = SetOriginalColor("Red");
            _m.BackFace = SetOriginalColor("Yellow");
            _m.RightFace = SetOriginalColor("Orange");
            _m.TopFace = SetOriginalColor("Green");
            _m.BottomFace = SetOriginalColor("Blue");
            ShowFace(_m.FrontFace);
        }
        public void ScrambleCube()
        {
            string finalString = "";
            Random r = new Random();
            for (int i = 0; i < 10; i++)
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
        public string OptimizeSolveString(string solveString)
        {
            string[] splitString = solveString.Split(',');
            List<string> listString = splitString.ToList();

            for (int i = 0; i < listString.Count; i++)
            {
                listString[i] = listString[i].Trim();
            }
            for (int i = 0; i < listString.Count; i++)
            {
                if (CheckFourConsecutive(listString, i))
                {
                    listString.RemoveAt(i);
                    listString.RemoveAt(i + 1);
                    listString.RemoveAt(i + 2);
                    listString.RemoveAt(i + 3);
                }
                if (CheckFlipFlop(listString, i, listString[i]))
                {
                    listString.RemoveAt(i);
                    listString.RemoveAt(i + 1);
                }
                if (CheckThreeConsecutive(listString, i))
                {
                    string s = listString[i];
                    switch (s)
                    {
                        case "F":
                            listString.RemoveAt(i);
                            listString.RemoveAt(i + 1);
                            listString.RemoveAt(i + 2);
                            listString.Insert(i, "Fi");
                            break;
                        case "Fi":
                            listString.RemoveAt(i);
                            listString.RemoveAt(i + 1);
                            listString.RemoveAt(i + 2);
                            listString.Insert(i, "F");
                            break;
                        case "B":
                            listString.RemoveAt(i);
                            listString.RemoveAt(i + 1);
                            listString.RemoveAt(i + 2);
                            listString.Insert(i, "Bi");
                            break;
                        case "Bi":
                            listString.RemoveAt(i);
                            listString.RemoveAt(i + 1);
                            listString.RemoveAt(i + 2);
                            listString.Insert(i, "B");
                            break;
                        case "L":
                            listString.RemoveAt(i);
                            listString.RemoveAt(i + 1);
                            listString.RemoveAt(i + 2);
                            listString.Insert(i, "Li");
                            break;
                        case "Li":
                            listString.RemoveAt(i);
                            listString.RemoveAt(i + 1);
                            listString.RemoveAt(i + 2);
                            listString.Insert(i, "L");
                            break;
                        case "R":
                            listString.RemoveAt(i);
                            listString.RemoveAt(i + 1);
                            listString.RemoveAt(i + 2);
                            listString.Insert(i, "Ri");
                            break;
                        case "Ri":
                            listString.RemoveAt(i);
                            listString.RemoveAt(i + 1);
                            listString.RemoveAt(i + 2);
                            listString.Insert(i, "R");
                            break;
                        case "D":
                            listString.RemoveAt(i);
                            listString.RemoveAt(i + 1);
                            listString.RemoveAt(i + 2);
                            listString.Insert(i, "Di");
                            break;
                        case "Di":
                            listString.RemoveAt(i);
                            listString.RemoveAt(i + 1);
                            listString.RemoveAt(i + 2);
                            listString.Insert(i, "D");
                            break;
                        case "U":
                            listString.RemoveAt(i);
                            listString.RemoveAt(i + 1);
                            listString.RemoveAt(i + 2);
                            listString.Insert(i, "Ui");
                            break;
                        case "Ui":
                            listString.RemoveAt(i);
                            listString.RemoveAt(i + 1);
                            listString.RemoveAt(i + 2);
                            listString.Insert(i, "U");
                            break;
                    }
                }
            }
            solveString = "";
            foreach (string s in listString)
            {
                solveString += s + ", ";
            }
            return solveString;
        }
        public bool CheckThreeConsecutive(List<string> list, int index)
        {
            if (index < list.Count - 3)
            {
                if (list[index] == list[index + 1] && list[index + 1] == list[index + 2] && list[index + 2] != list[index + 3])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool CheckFlipFlop(List<string> list, int index, string move)
        {
            switch (move)
            {
                case "F":
                    if (list[index + 1] == "Fi")
                    {
                        return true;
                    }
                    break;
                case "Fi":
                    if (list[index + 1] == "F")
                    {
                        return true;
                    }
                    break;
                case "B":
                    if (list[index + 1] == "Bi")
                    {
                        return true;
                    }
                    break;
                case "Bi":
                    if (list[index + 1] == "B")
                    {
                        return true;
                    }
                    break;
                case "L":
                    if (list[index + 1] == "Li")
                    {
                        return true;
                    }
                    break;
                case "Li":
                    if (list[index + 1] == "L")
                    {
                        return true;
                    }
                    break;
                case "R":
                    if (list[index + 1] == "Ri")
                    {
                        return true;
                    }
                    break;
                case "Ri":
                    if (list[index + 1] == "R")
                    {
                        return true;
                    }
                    break;
                case "D":
                    if (list[index + 1] == "Di")
                    {
                        return true;
                    }
                    break;
                case "Di":
                    if (list[index + 1] == "D")
                    {
                        return true;
                    }
                    break;
                case "U":
                    if (list[index + 1] == "Ui")
                    {
                        return true;
                    }
                    break;
                case "Ui":
                    if (list[index + 1] == "U")
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }
        public bool CheckFourConsecutive(List<string> list, int index)
        {
            if (index < list.Count - 4)
            {
                if (list[index] == list[index + 1] && list[index + 1] == list[index + 2] && list[index + 2] == list[index + 3])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public void WriteSolve(string solveString)
        {
            // WriteAllLines creates a file, writes a collection of strings to the file, 
            // and then closes the file.
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("solve.txt", true))
            {
                file.WriteLine(solveString);
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
    }
}

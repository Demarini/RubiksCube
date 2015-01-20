using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cube.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
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
        public MainWindowViewModel()
        {
            ClearCube();
            ShowFace(front);
        }
        public void Right()
        {
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



            RefreshScreen(currentFace);
        }

        public void Back()
        {
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
            string[,] returnedFace = new string[3,3];
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
                    ScrambleCube();
                });
            }
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
        }
        public void ScrambleCube()
        {
            Random r = new Random();
            for (int i = 0; i < 10000; i++)
            {
                int move = r.Next(12);
                switch (move)
                {
                    case 0:
                        Right();
                        break;
                    case 1:
                        RightInverted();
                        break;
                    case 2:
                        Left();
                        break;
                    case 3:
                        LeftInverted();
                        break;
                    case 4:
                        Front();
                        break;
                    case 5:
                        FrontInverted();
                        break;
                    case 6:
                        Back();
                        break;
                    case 7:
                        BackInverted();
                        break;
                    case 8:
                        Top();
                        break;
                    case 9:
                        TopInverted();
                        break;
                    case 10:
                        Bottom();
                        break;
                    case 11:
                        BottomInverted();
                        break;
                }
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
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

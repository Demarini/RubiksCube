using Cube.Controller;
using Cube.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace Cube.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        string finalString = "";
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
        MainWindowModel m;
        MainWindowController mc;
        #region Constructor
        public MainWindowViewModel()
        {
            m = new MainWindowModel(this);
            m.CurrentFace = "front";
            mc = new MainWindowController(this, m);
            mc.ClearCube();
            mc.ShowFace(front);
        }
        #endregion
        #region CubeMethods

        #endregion
        #region Commands
        public ICommand FrontClick
        {
            get
            {
                return new RelayCommand(() =>
                {
                    m.CurrentFace = "front";
                    mc.ShowFace(front);
                });
            }
        }
        public ICommand LeftClick
        {
            get
            {
                return new RelayCommand(() =>
                {
                    m.CurrentFace = "left";
                    mc.ShowFace(left);
                });
            }
        }
        public ICommand RightClick
        {
            get
            {
                return new RelayCommand(() =>
                {
                    m.CurrentFace = "right";
                    mc.ShowFace(right);
                });
            }
        }
        public ICommand BottomClick
        {
            get
            {
                return new RelayCommand(() =>
                {
                    m.CurrentFace = "bottom";
                    mc.ShowFace(bottom);
                });
            }
        }
        public ICommand TopClick
        {
            get
            {
                return new RelayCommand(() =>
                {
                    m.CurrentFace = "top";
                    mc.ShowFace(top);
                });
            }
        }
        public ICommand BackClick
        {
            get
            {
                return new RelayCommand(() =>
                {
                    m.CurrentFace = "back";
                    mc.ShowFace(back);
                });
            }
        }

        public ICommand FrontCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.Front();
                });
            }
        }

        public ICommand FrontInvertedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.FrontInverted();
                });
            }
        }

        public ICommand RightCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.Right();
                });
            }
        }

        public ICommand RightInvertedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.RightInverted();
                });
            }
        }

        public ICommand LeftCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.Left();
                });
            }
        }

        public ICommand LeftInvertedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.LeftInverted();
                });
            }
        }

        public ICommand TopCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.Top();
                });
            }
        }

        public ICommand TopInvertedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.TopInverted();
                });
            }
        }

        public ICommand BottomCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.Bottom();
                });
            }
        }

        public ICommand BottomInvertedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.BottomInverted();
                });
            }
        }

        public ICommand BackCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.Back();
                });
            }
        }

        public ICommand BackInvertedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.BackInverted();
                });
            }
        }

        public ICommand Clear
        {
            get
            {
                return new RelayCommand(() =>
                {
                    m.ColorVisibility = System.Windows.Visibility.Hidden;
                    mc.ClearCube();
                });
            }
        }
        public ICommand Scramble
        {
            get
            {
                return new RelayCommand(() =>
                {
                    m.ColorVisibility = System.Windows.Visibility.Hidden;
                    mc.ScrambleCube();
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
                    mc.SolveCube();
                });
            }
        }

        public ICommand SelectTopLeftColor
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.SelectColor("tl");
                });
            }
        }
        public ICommand SelectTopMiddleColor
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.SelectColor("tm");
                });
            }
        }
        public ICommand SelectTopRightColor
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.SelectColor("tr");
                });
            }
        }
        public ICommand SelectMiddleLeftColor
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.SelectColor("ml");
                });
            }
        }
        public ICommand SelectMiddleRightColor
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.SelectColor("mr");
                });
            }
        }
        public ICommand SelectBottomLeftColor
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.SelectColor("bl");
                });
            }
        }
        public ICommand SelectBottomMiddleColor
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.SelectColor("bm");
                });
            }
        }
        public ICommand SelectBottomRightColor
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.SelectColor("br");
                });
            }
        }
        public ICommand SelectWhite
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.SetCubeColor("White");
                });
            }
        }
        public ICommand SelectYellow
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.SetCubeColor("Yellow");
                });
            }
        }
        public ICommand SelectRed
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.SetCubeColor("Red");
                });
            }
        }
        public ICommand SelectOrange
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.SetCubeColor("Orange");
                });
            }
        }
        public ICommand SelectGreen
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.SetCubeColor("Green");
                });
            }
        }
        public ICommand SelectBlue
        {
            get
            {
                return new RelayCommand(() =>
                {
                    mc.SetCubeColor("Blue");
                });
            }
        }
        #endregion
        #region Attributes
        public string CurrentFace
        {
            get
            {
                return currentFace;
            }
            set
            {
                currentFace = value;
            }
        }
        public string CurrentCube
        {
            get
            {
                return currentCube;
            }
            set
            {
                currentCube = value;
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
        public string[,] FrontFace
        {
            get
            {
                return front;
            }
            set
            {
                front = value;
            }
        }
        public string[,] BackFace
        {
            get
            {
                return back;
            }
            set
            {
                back = value;
            }
        }
        public string[,] LeftFace
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
            }
        }
        public string[,] RightFace
        {
            get
            {
                return right;
            }
            set
            {
                right = value;
            }
        }
        public string[,] TopFace
        {
            get
            {
                return top;
            }
            set
            {
                top = value;
            }
        }
        public string[,] BottomFace
        {
            get
            {
                return bottom;
            }
            set
            {
                bottom = value;
            }
        }
        #endregion
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
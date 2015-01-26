using Cube.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube.Model
{
    public class MainWindowModel
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
        private System.Windows.Visibility colorVisibility;
        private MainWindowViewModel _vmRef;
        private string currentFace;
        private string currentCube;
        public MainWindowModel(MainWindowViewModel vmRef)
        {
            _vmRef = vmRef;
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
                _vmRef.ColorVisibility = value;
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
                _vmRef.tlBack = value;
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
                _vmRef.tmBack = value;
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
                _vmRef.trBack = value;
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
                _vmRef.mlBack = value;
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
                _vmRef.mmBack = value;
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
                _vmRef.mrBack = value;
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
                _vmRef.blBack = value;
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
                _vmRef.bmBack = value;
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
                _vmRef.brBack = value;
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
                _vmRef.FrontFace = value;
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
                _vmRef.BackFace = value;
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
                _vmRef.LeftFace = value;
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
                _vmRef.RightFace = value;
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
                _vmRef.TopFace = value;
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
                _vmRef.BottomFace = value;
                bottom = value;
            }
        }
        public string CurrentFace
        {
            get
            {
                return currentFace;
            }
            set
            {
                _vmRef.CurrentFace = value;
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
                _vmRef.CurrentCube = value;
                currentCube = value;
            }
        }
    }
}

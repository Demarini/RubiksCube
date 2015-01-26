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
        private System.Windows.Visibility colorVisibility;
        private MainWindowViewModel _vmRef;
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
    }
}

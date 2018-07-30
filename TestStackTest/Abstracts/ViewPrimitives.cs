using System;
using System.Collections.Generic;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;
using System.Linq;
using TestStackTest.Controls;
using TestStackTest.TestBehaviours;

namespace TestStackTest.Abstracts {
    public class TestViewBase : ITestView {
        private Application _application;
        private string _windowName;
        private List<string> _reportDetails;        
        private Window _window;
        private ControlFactory _controlFactory = new ControlFactory();
        public WindowTools windowTools = new WindowTools();
        public Dictionary<string, IControl> controls = new Dictionary<string, IControl>();
        public ControlFactory controlFactory { get { return _controlFactory; } }


        public Application application {
            get {
                return _application;
            }

            set {
                _application = value;
            }
        }

        public List<string> reportDetails {
            get {
                return _reportDetails;
            }

            set {
                _reportDetails.Concat(value);
            }
        }

        public Window window {
            get {
                return _window;            
            }
            set {
                _window = value;
            }
        }
        public virtual void Report() { }

        public virtual void SetUp(Application app, WindowNames windowName) 
        {   application = app;            
            Enum name = windowName;
            _windowName = name.ToString();
            GetWindow(_windowName);
        }

        public virtual void SetUp(Application app, int windowNumber) {
            application = app;
            GetWindow(windowNumber);
        }

        public void GetWindow(string windowName) {
            window = application.GetWindows().Where(x=> x.Name.Replace(" ","").Replace(@"/","") == windowName).FirstOrDefault();            
        }

        public void GetWindow(int windowNumber) {
            List<Window> windows = application.GetWindows();
            if (windowNumber < windows.Count) { window = windows[windowNumber]; } 
            else { window = windows[0];}
        }


    }
}

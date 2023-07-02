/*
 Recently you implemented a very simple traffic light. Your new task is to improve that project so that it uses OOP 
principles. Your classes should have sufficient information to draw a traffic light and should not be inherited from 
any existing .NET classes (like UserControl, Panel, etc.).



Once you finish an object-oriented traffic light, create a few instances of that class and synchronize them like they are 
a real crossroad: two traffic lights should show Red, and two should show Green. Then they do the opposite thing: two traffic
lights should show Green and the other two should show Red. Both regular and pedestrian modes should be synchronized, 
but no need to implement mixed modes (regular+pedestrian).



As in the previous traffic light task, use the Paint event of any suitable control to draw your objects. You may want to pass
a reference to the parent control to your Traffic Light task to subscribe to its Paint event inside that class.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace TrafficLights2
{
    // Advanced TrafficLight user control which can create up to 4 traffic lights written using OOP.
    public partial class Form1 : Form
    {
        private TrafficLight trafficLight1;
        public Form1()
        {
            trafficLight1 = new TrafficLight(4);
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Controls.Add(trafficLight1);
        }
        private void ChangeModeButton_Click(object sender, EventArgs e)
        {
            trafficLight1.ChangeMode();
        }
        private void SwitchButton_Click(object sender, EventArgs e)
        {
            trafficLight1.SwitchToNextLight();
        }
    }
}

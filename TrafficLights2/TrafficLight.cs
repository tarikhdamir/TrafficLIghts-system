using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficLights2
{
    public partial class TrafficLight : UserControl
    {
        private string mode;
        private int numOfTrafficLights;
        public int NumOfTrafficLights
        {
            get { return numOfTrafficLights; }
            set { numOfTrafficLights = value; }
        }
        private TrafficLightBox[] transportBox;
        public TrafficLightBox[] TransportBox
        {
            get { return transportBox; }
            set { transportBox = value; }
        }
        private TrafficLightBox[] pedestrianBox; 
        public TrafficLightBox[] PedestrianBox
        {
            get { return pedestrianBox; }
            set { pedestrianBox = value; }
        }
        private TrafficLightBox currentBox;
        public TrafficLight()
        {
            currentBox = new TrafficLightBox();
            transportBox = new TrafficLightBox[1];
            pedestrianBox = new TrafficLightBox[1];
            transportBox[0] = new TrafficLightBox("transport", 0);
            pedestrianBox[0] = new TrafficLightBox("pedestrian", 0);
            this.numOfTrafficLights = 4;
            SetMode("transport");
            InitializeComponent();
        }
        public TrafficLight(int numOfTrafficLights)
        {
            currentBox = new TrafficLightBox();
            transportBox = new TrafficLightBox[numOfTrafficLights];
            pedestrianBox = new TrafficLightBox[numOfTrafficLights];
            for (int i = 0; i < numOfTrafficLights; i++)
            {
                transportBox[i] = new TrafficLightBox("transport", i);
                pedestrianBox[i] = new TrafficLightBox("pedestrian", i);
            }
            this.numOfTrafficLights = numOfTrafficLights;
            SetMode("transport");
            InitializeComponent();
        }

        private void TrafficLight_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            timer.Start();
        }
        public string GetMode()
        {
            return mode;
        }
        public void TrafficLight_Paint(object sender, PaintEventArgs e)
        {
            DrawTrafficLights(e.Graphics);
            if (mode == "transport")
                CheckTransportState();
            else CheckPedestrianState();
            LightSwitch(e.Graphics);
        }
        public void SetMode(string mode)
        {
            this.mode = mode;
        }
        private void DrawTrafficLights(Graphics g)
        {
            for (int i = 0; i < numOfTrafficLights; i++)
            {
                if (mode == "transport")
                    currentBox = transportBox[i];
                else currentBox = pedestrianBox[i];
                SolidBrush black = new SolidBrush(Color.Black);
                SolidBrush gray = new SolidBrush(Color.Gray);
                Graphics formGraphics = g;
                Rectangle rect = new Rectangle(currentBox.PosX, currentBox.PosY, currentBox.Width, currentBox.Height);
                formGraphics.FillRectangle(black, rect);
                int count = 0;
                foreach (Point point in currentBox.BoxColors.LightPositions)
                {
                    if (count > currentBox.BoxColors.Colors.Count)
                        break;
                    rect = new Rectangle(point.X, point.Y, currentBox.LightWidth, currentBox.LightHeight);
                    formGraphics.FillEllipse(gray, rect);
                    count++;
                }
                black.Dispose();
                gray.Dispose();
            }
        }

        private void LightSwitch(Graphics formGraphics)
        {
            for (int i = 0; i < numOfTrafficLights; i++)
            {
                if (mode == "transport")
                    currentBox = transportBox[i];
                else currentBox = pedestrianBox[i];
                Rectangle rect = new Rectangle();
                SolidBrush brush = new SolidBrush(currentBox.CurrentState);
                foreach (KeyValuePair<Point, Color> entry in currentBox.BoxColors.ColorPositions)
                {
                    if (entry.Value.Equals(currentBox.CurrentState))
                        rect = new Rectangle(entry.Key.X, entry.Key.Y, currentBox.LightWidth, currentBox.LightHeight);
                }
                formGraphics.FillEllipse(brush, rect);

                brush.Dispose();
            }
        }
        public void ResetTimer()
        {
            for(int i = 0; i < numOfTrafficLights; i++)
            {
                transportBox[i].Time = 0;
                pedestrianBox[i].Time = 0;
            }
            timer.Stop();
            timer.Start();
        }
        public void CheckTransportState()
        {
            for (int i = 0; i < numOfTrafficLights; i++)
            {
                if (transportBox[i].TimeToSwitchLight())
                {
                    SwitchToNextLight(transportBox[i]);
                }
            }
        }
        public void CheckPedestrianState()
        {
            for (int i = 0; i < numOfTrafficLights; i++)
            {
                if (pedestrianBox[i].TimeToSwitchLight())
                {
                    SwitchToNextLight(pedestrianBox[i]);
                }
            }
        }
        public void SwitchToNextLight(TrafficLightBox currentBox)
        {
            currentBox.SetNextColor();
        }
        public void SwitchToNextLight()
        {
            for(int i = 0; i < numOfTrafficLights; i++)
            {
                if(mode == "transport")
                    SwitchToNextLight(transportBox[i]);
                else SwitchToNextLight(pedestrianBox[i]);
            }
            ResetTimer();
            Invalidate();
        }
        private void TrafficLightTimerTick(object sender, EventArgs e)
        {
            Invalidate();
            for(int i = 0; i < numOfTrafficLights; i++)
            {
                if (mode == "transport")
                    transportBox[i].Time++;
                else pedestrianBox[i].Time++;
            }
        }

        public void ChangeMode()
        {
            ResetTimer();
            if (mode == "transport")
            {
                mode = "pedestrian";
            }
            else
            {
                mode = "transport";
            }
            for (int i = 0; i < numOfTrafficLights; i++)
            {
                if (mode == "transport")
                    currentBox = TransportBox[i];
                else currentBox = PedestrianBox[i];
                currentBox.SetCurrentState(currentBox.BoxColors.ColorOrder[0]);
            }
            Invalidate();
        }
    }
    public class TrafficLightBox
    {
        private string mode;
        private int posX;
        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }
        private int posY;
        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }
        private int width;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        private int height;
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        private int padding;
        public int Padding
        {
            get { return padding; }
            set { padding = value; }
        }
        private int lightWidth;
        public int LightWidth
        {
            get { return lightWidth; }
            set { lightWidth = value; }
        }
        private int lightHeight;
        public int LightHeight
        {
            get { return lightHeight; }
            set { lightHeight = value; }
        }
        private Lights boxColors;
        public Lights BoxColors
        {
            get { return boxColors; }
            set { boxColors = value; }
        }
        private Color currentState;
        public Color CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }
        private int numOfTrafficLightBox;
        public int NumOfTrafficLightBox
        {
            get { return numOfTrafficLightBox; }
            set { numOfTrafficLightBox = value; }
        }
        private int time;
        public int Time
        {
            get { return time; }
            set { time = value; }
        }

        public TrafficLightBox()
        {
            this.mode = "transport";
            SetPosition(275, 140);
            int temporaryHeight = 71;
            if (mode == "pedestrian")
            {
                temporaryHeight = 51;
            }
            SetDimensions(25, temporaryHeight);
            time = 0;
        }
        public TrafficLightBox(string mode, int numOfTrafficLightBox)
        {
            this.mode = mode;
            this.NumOfTrafficLightBox = numOfTrafficLightBox;
            switch (numOfTrafficLightBox)
            {
                case 0:
                    SetPosition(275, 40);
                    break;
                case 1:
                    SetPosition(175, 140);
                    break;
                case 2:
                    SetPosition(275, 240);
                    break;
                case 3:
                    SetPosition(375, 140);
                    break;
                default:
                    break;
            }
            int temporaryHeight = 71;
            if (mode == "pedestrian")
            {
                temporaryHeight = 51;
            }
            SetDimensions(25, temporaryHeight);
            boxColors = new Lights(mode, this);
            currentState = BoxColors.ColorOrder[0];
            time = 0;
        }

        public void SetNextColor()
        {
            bool found = false;
            foreach (Color color in boxColors.ColorOrder)
            {
                if (found)
                {
                    SetCurrentState(color);
                    return;
                }
                if (color == currentState)
                    found = true;
            }
            SetCurrentState(boxColors.ColorOrder[0]);
        }
        public void SetPosition(int posX, int posY)
        {
            this.PosX = posX;
            this.PosY = posY;
        }
        public void SetDimensions(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            padding = height / 50;
            SetLightDimensions();
        }
        public void SetLightDimensions()
        {
            lightWidth = width - 2 * padding;
            if (mode == "transport")
                lightHeight = (height - 4 * padding) / 3;
            else lightHeight = (height - 3 * padding) / 2;
        }
        public void SetCurrentState(Color state)
        {
            currentState = state;
        }
        public bool TimeToSwitchLight()
        {
            if (this.time >= boxColors.LightDelay[currentState])
                return true;
            return false;
        }
    }
    public class Lights : TrafficLightBox
    {
        private List<Color> colors;
        public List<Color> Colors
        {
            get { return colors; }
            set { colors = value; }
        }
        private List<Color> colorOrder;
        public List<Color> ColorOrder
        {
            get { return colorOrder; }
            set { colorOrder = value; }
        }
        private List<Point> lightPositions;
        public List<Point> LightPositions
        {
            get { return lightPositions; }
            set { lightPositions = value; }
        }
        private Dictionary<Point, Color> colorPositions;
        public Dictionary<Point, Color> ColorPositions
        {
            get { return colorPositions; }
            set { colorPositions = value; }
        }
        private Dictionary<Color, int> lightDelay;
        public Dictionary<Color, int> LightDelay
        {
            get { return lightDelay; }
            set { lightDelay = value; }
        }
        
        public Lights() : base()
        {
            colors = new List<Color>();
            colorOrder = new List<Color>();
            lightPositions = new List<Point>();
            colorPositions = new Dictionary<Point, Color>();
            lightDelay = new Dictionary<Color, int>();
        }
        public Lights(string mode, TrafficLightBox box) : base()
        {
            colors = new List<Color>();
            colorOrder = new List<Color>();
            lightPositions = new List<Point>();
            colorPositions = new Dictionary<Point, Color>();
            lightDelay = new Dictionary<Color, int>();

            colors.Add(Color.Red);
            colors.Add(Color.Yellow);
            colors.Add(Color.Green);
            if (box.NumOfTrafficLightBox % 2 == 0)
            {
                colorOrder.Add(Color.Red);
                colorOrder.Add(Color.Green);
                colorOrder.Add(Color.Yellow);
            }
            else
            {
                colorOrder.Add(Color.Green);
                colorOrder.Add(Color.Yellow);
                colorOrder.Add(Color.Red);
            }
            if (mode == "pedestrian")
            {
                colors.Remove(Color.Yellow);
                colorOrder.Remove(Color.Yellow);
            }
            SetLightPositions(colors, box);
            SetLightDelay(3, colors);
        }
        public void SetColorOrder(List<Color> colorOrder)
        {
            colorOrder.Clear();
            foreach(Color color in colorOrder) 
                colorOrder.Add(color);
        }
        public void SetLightPositions(List<Color> colors, TrafficLightBox box)
        {
            int position = 1;
            foreach (Color color in colors)
            {
                Point point = new Point(box.PosX + box.Padding, box.PosY + box.Padding * position + box.LightHeight * (position - 1));
                lightPositions.Add(point);
                if (!colorPositions.ContainsKey(point))
                    colorPositions.Add(point, color);
                else colorPositions[point] = color;
                position++;
            }
        }
        public void SetLightDelay(int newDelay, List<Color> colors)
        {
            foreach (Color color in colors)
            {
                if (!lightDelay.ContainsKey(color))
                    lightDelay.Add(color, newDelay);
                else lightDelay[color] = newDelay;
            }
        }
    }
}
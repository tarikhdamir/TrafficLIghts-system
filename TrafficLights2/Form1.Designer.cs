using System.Drawing.Text;

namespace TrafficLights2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            //this.trafficLight1 = new TrafficLights2.TrafficLight();
            this.ChangeModeButton = new System.Windows.Forms.Button();
            this.SwitchButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // trafficLight1
            // 
            //this.trafficLight1.Location = new System.Drawing.Point(0, -1);
            //this.trafficLight1.Name = "trafficLight1";
            //this.trafficLight1.Size = new System.Drawing.Size(800, 600);
            //this.trafficLight1.TabIndex = 0;
            // 
            // ChangeModeButton
            // 
            this.ChangeModeButton.AutoSize = true;
            this.ChangeModeButton.Location = new System.Drawing.Point(230, 490);
            this.ChangeModeButton.Name = "ChangeModeButton";
            this.ChangeModeButton.Size = new System.Drawing.Size(300, 50);
            this.ChangeModeButton.TabIndex = 0;
            this.ChangeModeButton.Text = "Change to pedestrian Traffic Light";
            this.ChangeModeButton.UseVisualStyleBackColor = true;
            this.ChangeModeButton.Click += new System.EventHandler(this.ChangeModeButton_Click);
            // 
            // SwitchButton
            // 
            this.SwitchButton.Location = new System.Drawing.Point(230, 546);
            this.SwitchButton.Name = "SwitchButton";
            this.SwitchButton.Size = new System.Drawing.Size(300, 50);
            this.SwitchButton.TabIndex = 1;
            this.SwitchButton.Text = "Switch to next light";
            this.SwitchButton.UseVisualStyleBackColor = true;
            this.SwitchButton.Click += new System.EventHandler(this.SwitchButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.ChangeModeButton);
            this.Controls.Add(this.SwitchButton);
            //this.Controls.Add(this.trafficLight1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            //this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);

        }
        //private TrafficLight trafficLight1;
        private System.Windows.Forms.Button ChangeModeButton;
        private System.Windows.Forms.Button SwitchButton;
    }
    #endregion
}


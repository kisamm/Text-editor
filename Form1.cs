using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Speech;
using System.Speech.Synthesis;
namespace edytor_textu
{
    public partial class Form1 : Form

    {
        public Form1()
        {
            InitializeComponent();
            toolStrip1.Renderer = new ToolStripStripeRemoval();
        }

        SpeechSynthesizer sp = new SpeechSynthesizer();

        public int linia = 0;
        public int kolumna = 0;
        private string strMyOriginalText;

        public class ToolStripStripeRemoval : ToolStripSystemRenderer
            {

            public ToolStripStripeRemoval () { }
            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                
            }
        }

        private void nowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (.txt)|*.txt";
            ofd.Title = "Open a file";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(ofd.FileName);
                richTextBox1.Text = sr.ReadToEnd();
                sr.Close();

            }
        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void cofnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();

        }

        private void ponówToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void wytnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void kopiujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void wklejToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void zaznaczwszystkoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void zapiszjakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Plik tekstowy (*.txt)|*.txt";
            dialog.Title = ("Save as...");
            dialog.ShowDialog();
            if (dialog.FileName != "")
            {
                dialog.FileName = dialog.FileName;
                StreamWriter sw = new StreamWriter(dialog.FileName);
                
                sw.Write(richTextBox1.Text);
                sw.Close();
            }
        }

        private void drukujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (.txt)|*.txt";
            ofd.Title = "Open a file";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(ofd.FileName);
                richTextBox1.Text = sr.ReadToEnd();
                sr.Close();

            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Plik tekstowy (*.txt)|*.txt";
            dialog.Title = ("Save as...");
            dialog.ShowDialog();
            if (dialog.FileName != "")
            {
                dialog.FileName = dialog.FileName;
                StreamWriter sw = new StreamWriter(dialog.FileName);

                sw.Write(richTextBox1.Text);
                sw.Close();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
           
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void czcionka_Click(object sender, EventArgs e)
        {
            if(fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fontDialog1.Font;

            }
        }

        private void kolor_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.ForeColor = colorDialog1.Color;
            }
        }

        private void narzędziaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            linia = 1 + richTextBox1.GetLineFromCharIndex(richTextBox1.GetFirstCharIndexOfCurrentLine());
            kolumna = 1 + richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine();
            toolStripStatusLabel1.Text = "line: " + linia.ToString() + " | column: " + kolumna.ToString();
        }

        private void przeskoczNaDółToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void przeskoczNaGóręToolStripMenuItem_Click(object sender, EventArgs e)
        {

            richTextBox1.SelectionStart = 0;
            richTextBox1.ScrollToCaret();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string keywords = textBox1.Text;
            MatchCollection keywordMatches = Regex.Matches(richTextBox1.Text, keywords);

            int originalIndex = richTextBox1.SelectionStart;
            int originalLength = richTextBox1.SelectionLength;

            
            textBox1.Focus();

            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectionLength = richTextBox1.Text.Length;
            richTextBox1.SelectionBackColor = Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));

            foreach (Match m in keywordMatches)
            {
                richTextBox1.SelectionStart = m.Index;
                richTextBox1.SelectionLength = m.Length;
                richTextBox1.SelectionColor = Color.White;
                richTextBox1.SelectionBackColor = Color.DodgerBlue;
            }

            richTextBox1.SelectionStart = originalIndex;
            richTextBox1.SelectionLength = originalLength;
            richTextBox1.SelectionBackColor = Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != null && !string.IsNullOrWhiteSpace(textBox2.Text) && textBox3.Text != null && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                richTextBox1.Text = richTextBox1.Text.Replace(textBox2.Text, textBox3.Text);
                textBox2.Text = "";
                textBox3.Text = "";
            }
        }

        private void dostosujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Focus(); 
        }

        private void opcjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox2.Focus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (textBox1.Text != strMyOriginalText)
                if (MessageBox.Show("Chcesz zapisać przed wyjściem ?", "Pirat Editor",
                                 MessageBoxButtons.YesNo) == DialogResult.Yes)
                   

            
                 
            {
                e.Cancel = true;
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "Plik tekstowy (*.txt)|*.txt";
                    dialog.Title = ("Zapisz jako...");
                    dialog.ShowDialog();
                    if (dialog.FileName != "")
                    {
                        dialog.FileName = dialog.FileName;
                        StreamWriter sw = new StreamWriter(dialog.FileName);

                        sw.Write(richTextBox1.Text);
                        sw.Close();
                    }
                

            }
        }

        private void FormClosingEventCancle_Closing(object sender, CancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton9_Click_1(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;

        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void drukujToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            if(printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }


        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, new Font("Arial", 40, FontStyle.Bold), Brushes.Black,150,125);
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont.Bold)

            {

                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style ^ FontStyle.Bold);

            }

            else

            {

                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style | FontStyle.Bold);

            }

        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Font.Style != FontStyle.Italic)
                richTextBox1.Font = new Font(richTextBox1.Font, FontStyle.Italic);
            else
                richTextBox1.Font = new Font(richTextBox1.Font, FontStyle.Regular);

        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Font.Style != FontStyle.Underline)
                richTextBox1.Font = new Font(richTextBox1.Font, FontStyle.Underline);
            else
                richTextBox1.Font = new Font(richTextBox1.Font, FontStyle.Regular);
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Font.Style != FontStyle.Strikeout)
                richTextBox1.Font = new Font(richTextBox1.Font, FontStyle.Strikeout);
            else
                richTextBox1.Font = new Font(richTextBox1.Font, FontStyle.Regular);
        }

        private void godzinaDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime myValue = DateTime.Now;
            richTextBox1.Text = (myValue.ToString());



            }

        private void iVONAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(richTextBox1.Text != null)
            {
                sp.Dispose();
                sp = new SpeechSynthesizer();
                sp.SpeakAsync(richTextBox1.Text);
            }
            else
            {
                MessageBox.Show("Wpisz wpierw tekst!");

            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
                TopMost = false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class LineDrawingForm : Form
    {
        private Point? lineStart = null;
        private List<Line> _lines = new List<Line>();
        private Color _currentColour = Color.Black;

        public LineDrawingForm()
        {
            //Set up data grid
            InitializeComponent();
            LinesBox.ColumnCount = 5;
            LinesBox.Columns[0].Name = "First X";
            LinesBox.Columns[1].Name = "First Y";
            LinesBox.Columns[2].Name = "Second X";
            LinesBox.Columns[3].Name = "Second Y";
            LinesBox.Columns[4].Name = "Colour";
            LinesBox.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LinesBox.Columns[4].ReadOnly = true;
            LinesBox.AllowUserToAddRows = false;
            LinesBox.Rows.Add();//starting row
        }

        private void LinesBox_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {   //set colour of the cell to colour of line
                if (int.TryParse(LinesBox[e.ColumnIndex, e.RowIndex].Value.ToString(), out _))
                {
                    LinesBox[4, e.RowIndex].Value = _currentColour.ToArgb().ToString();
                    LinesBox.InvalidateCell(4, e.RowIndex);
                }
            }

            //only does checks or adds line when all cells are filled
            for (int i = 0; i < 4; i++)
            {
                if (LinesBox[i, e.RowIndex].Value == null || string.IsNullOrWhiteSpace(LinesBox[i, e.RowIndex].Value.ToString()))
                {
                    return;
                }
            }

            //read cells, if not int gives error and doesnst draw line
            if (!int.TryParse(LinesBox[0, e.RowIndex].Value.ToString(), out int firstX) ||
                !int.TryParse(LinesBox[1, e.RowIndex].Value.ToString(), out int firstY) ||
                !int.TryParse(LinesBox[2, e.RowIndex].Value.ToString(), out int secondX) ||
                !int.TryParse(LinesBox[3, e.RowIndex].Value.ToString(), out int secondY))
            {
                MessageBox.Show("X and Y values must be integers. Please enter valid values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Color lineColour;

            if (e.RowIndex >= _lines.Count)//adds new line as it's index isnt in the list
            {
                lineColour = _currentColour;
                Line newLine = new Line((firstX, firstY), (secondX, secondY), lineColour);
                AddLine(newLine);
            }
            else//updates line with new information
            {
                lineColour = _lines[e.RowIndex].Colour;
                Line newLine = new Line((firstX, firstY), (secondX, secondY), lineColour);
                UpdateLine(newLine, e.RowIndex);
            }
        }

        private void LinesBox_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //formats the colour cell
            if (e.ColumnIndex == 4)
            {
                if (e.Value != null)
                {
                    Color cellColour = Color.FromArgb(int.Parse(e.Value.ToString()));
                    e.CellStyle.BackColor = cellColour;
                    e.CellStyle.SelectionBackColor = cellColour;
                    e.CellStyle.ForeColor = cellColour;
                    e.CellStyle.SelectionForeColor = cellColour;
                }
            }
        }

        private void LinesBox_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //checks if colour cell is clicked
            if (e.ColumnIndex == 4 && e.RowIndex >= 0)
            {
                for (int i = 0; i < 4; i++)
                {//only can select colour cell if line exists
                    if (LinesBox[i, e.RowIndex].Value == null || string.IsNullOrWhiteSpace(LinesBox[i, e.RowIndex].Value.ToString()))
                    {
                        MessageBox.Show("Please enter X and Y values before selecting a colour.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }//opens colour 
                ColorDialog colorDialog = new ColorDialog();
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColour = colorDialog.Color;
                    LinesBox[e.ColumnIndex, e.RowIndex].Value = selectedColour.ToArgb().ToString();
                    _lines[e.RowIndex].Colour = selectedColour;
                    lineCanvas.Refresh();
                    foreach (Line line in _lines)
                    {
                        AddLineCanvas(line);//changes line colour
                    }
                    LinesBox.InvalidateCell(e.ColumnIndex, e.RowIndex);//changes cell to new colour
                }
            }
        }

        private void AddLine(Line line)//adds new line to table and canvas
        {
            _lines.Add(line);
            AddLineCanvas(line);
            LinesBox.Rows.Add();
        }

        private void UpdateLine(Line newLine, int index)//changes existing line on table and canvas
        {   
            _lines[index] = newLine;
            lineCanvas.Refresh();
            foreach (Line line in _lines)
            {
                AddLineCanvas(line);
            }
        }
        private void AddLineCanvas(Line line)//paints line on canvas
        {
            Pen pen = new Pen(line.Colour);

            Point startPoint = new Point(line.Start.x, line.Start.y);
            Point endPoint = new Point(line.End.x, line.End.y);

            lineCanvas.CreateGraphics().DrawLine(pen, startPoint, endPoint);

        }
        private void RemoveLine(int index)//removes line at given index on table and canvas
        {
            _lines.RemoveAt(index);
            lineCanvas.Refresh();
            foreach (Line line in _lines)
            {
                AddLineCanvas(line);
            }
        }


        private void lineCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (LinesBox.IsCurrentCellInEditMode)//doesnt allow new lines drawn when editing as this messed up the indexing for removing
                {
                    MessageBox.Show("Please finish editing the current line before drawing a new one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (lineStart == null)
                {
                    lineStart = e.Location;//on first mouse up it records start location
                }
                else
                {//second mouse up it records second location and adds to table and draws the line
                    LinesBox.Rows.RemoveAt(LinesBox.Rows.Count - 1);
                    Point lineEnd = e.Location;
                    Line newLine = new Line((lineStart.Value.X, lineStart.Value.Y), (lineEnd.X, lineEnd.Y), _currentColour);
                    _lines.Add(newLine);
                    AddLineCanvas(newLine);
                    LinesBox.Rows.Add(newLine.Start.x, newLine.Start.y, newLine.End.x, newLine.End.y, newLine.Colour.ToArgb().ToString());
                    LinesBox.Rows.Add();

                    lineStart = null;
                }
            }
        }

        private void Colour_Click(object sender, EventArgs e)
        {//opens dialog and changes default colour
            ColorDialog colourDialog = new ColorDialog();
            if (colourDialog.ShowDialog() == DialogResult.OK)
            {
                _currentColour = colourDialog.Color;
                Colour.ForeColor = _currentColour;
            }
        }

        private void Remove_Click(object sender, EventArgs e)
        {//gives error if no line selected , removes line from table and canvas
            int rowIndex = LinesBox.SelectedCells[0].RowIndex;
            if (LinesBox.SelectedCells.Count == 0 || rowIndex >= _lines.Count)
            {
                MessageBox.Show("There is no selected row to remove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LinesBox.Rows.RemoveAt(rowIndex);
            RemoveLine(rowIndex);
        }
        private void FindIntersections_Click(object sender, EventArgs e)
        {//compares each line then does calculation to determine whether they intersect

            for (int i = 0; i < _lines.Count; i++)
            {
                Line firstLine = _lines[i];

                for (int j = i + 1; j < _lines.Count; j++)
                {
                    Line secondLine = _lines[j];

                    if (intersectionCalculation(firstLine, secondLine, out Point intersection))
                    {
                        if (isPointInLine(intersection, firstLine) && isPointInLine(intersection, secondLine))
                        {
                            drawIntersection(intersection);
                        }
                    }
                }
            }
        }

        private bool intersectionCalculation(Line firstLine, Line secondLine, out Point intersection)
        {//uses given calculation to work out of theres an intersection, takes each lione

            intersection = Point.Empty;
            float x1 = firstLine.Start.x;
            float y1 = firstLine.Start.y;
            float x2 = firstLine.End.x;
            float y2 = firstLine.End.y;
            float x3 = secondLine.Start.x;
            float y3 = secondLine.Start.y;
            float x4 = secondLine.End.x;
            float y4 = secondLine.End.y;

            float bottom = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

            if (bottom == 0)
            {
                return false;
            }
            else
            {
                float intersectionX = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / bottom;
                float intersectionY = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / bottom;
                intersection = new Point((int)intersectionX, (int)intersectionY);
                return true;
            }
        }

        private bool isPointInLine(Point point, Line line)
        {//makes sure intersection is a point on both lines and if they overlap
            float minX = Math.Min(line.Start.x, line.End.x);
            float minY = Math.Min(line.Start.y, line.End.y);
            float maxX = Math.Max(line.Start.x, line.End.x);
            float maxY= Math.Max(line.Start.y, line.End.y);

            return point.X >= minX && point.X <= maxX && point.Y >= minY && point.Y <= maxY;
        }

        private void drawIntersection(Point intersection)
        {//draws small circles in default colour around each intersection
            Pen pen = new Pen(_currentColour);

            int circleRadius = 10;
            int circleDiameter = circleRadius * 2;
            int circleX = intersection.X - circleRadius;
            int circleY = intersection.Y - circleRadius;

            lineCanvas.CreateGraphics().DrawEllipse(pen, circleX, circleY, circleDiameter, circleDiameter);

        }
    }
}
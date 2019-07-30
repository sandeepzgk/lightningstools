using F4SharedMem.Headers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMSVectorsharedMemTestTool
{
    public partial class frmMain : Form
    {
        private Color _foreColor = Color.Green;
        private Color _backColor = Color.Black;

        private Brush _brush = new SolidBrush(Color.Green);
        private Pen _pen = new Pen(Color.Green, width: 1);
        private ImageAttributes _imageAttrs;

        private string _fontTexture;
        private HashSet<BmsFont> _bmsFonts = new HashSet<BmsFont>();
        private Image _HUDImage;
        private Image _RWRImage;
        private Image _HMSImage;
        private string _fontDir;

        private F4SharedMem.Reader _smReader = new F4SharedMem.Reader();

        public frmMain()
        {
            InitializeComponent();
            pbHUD.Image = _HUDImage;
            pbRWR.Image = _RWRImage;
            pbHMS.Image = _HMSImage;

            pbHUD.Refresh();
            pbRWR.Refresh();
            pbHMS.Refresh();

            timer1.Start();
        }

        private string RemoveSurroundingQuotes(string text)
        {
            var toReturn = text;
            if (toReturn.StartsWith("\""))
            {
                toReturn = toReturn.Substring(1, toReturn.Length - 1);
            }
            if (toReturn.EndsWith("\""))
            {
                toReturn = toReturn.Substring(0, toReturn.Length - 1);
            }
            return toReturn;
        }
        private string UnescapeComma(string line)
        {
            return line.Replace("&comma;", ",");
        }
        private string EscapeQuotedComma(string line)
        {
            bool quoteOpen = false;
            var toReturn = new StringBuilder();
            for (var i = 0; i < line.Length; i++)
            {

                if (line[i] == '"')
                {
                    quoteOpen = !quoteOpen;
                    toReturn.Append(line[i]);
                }
                else if (line[i] == ',' && quoteOpen)
                {
                    toReturn.Append("&comma;");
                }
                else
                {
                    toReturn.Append(line[i]);
                }
            }
            return toReturn.ToString();
        }
        private Color ColorFromPackedABGR(uint packedABGR)
        {
            return Color.FromArgb(alpha: (int)((packedABGR & 0xFF000000) >> 24), blue: (int)((packedABGR & 0xFF0000) >> 16), green: (int)((packedABGR & 0xFF00) >> 8), red: (int)(packedABGR & 0xFF)); ;
        }
        private void SetBackgroundColor(uint packedABGR)
        {
            var backgroundColor = ColorFromPackedABGR(packedABGR);
            SetBackgroundColor(backgroundColor);
        }
        private void SetBackgroundColor(Color backgroundColor)
        {
            _backColor = backgroundColor;
        }
        private void SetForegroundColor(uint packedABGR)
        {
            var foregroundColor = ColorFromPackedABGR(packedABGR);
            SetForegroundColor(foregroundColor);
        }

        private void SetForegroundColor(Color foregroundColor)
        {
            _foreColor = foregroundColor;
            _pen = new Pen(_foreColor, width: 1);
            _brush = new SolidBrush(_foreColor);
            _imageAttrs = new ImageAttributes();
            var colorMatrix = new ColorMatrix
            (
                new float[][]
                {
                        new float[] {_foreColor.R/255.0f, 0, 0, 0, 0}, //red %
                        new float[] { 0,_foreColor.G/255.0f, 0, 0, 0 }, //green
                        new float[] {0, 0, _foreColor.B/255.0f, 0, 0}, //blue %
                        new float[] {0, 0, 0, _foreColor.A/255.0f, 0}, //alpha %
                        new float[] {0, 0, 0, 0, 1} //add
                }
            );
            _imageAttrs.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default);
        }

        private void SetFont(string fontFile)
        {
            LoadBmsFont(fontFile);
            _fontTexture = fontFile;
        }
        private void DrawPoint(float x1, float y1, Graphics g)
        {
            g.DrawLine(_pen, x1, y1, x1, y1);
        }
        private void DrawLine(float x1, float y1, float x2, float y2, Graphics g)
        {
            g.DrawLine(_pen, x1, y1, x2, y2);
        }
        private void DrawTri(float x1, float y1, float x2, float y2, float x3, float y3, Graphics g)
        {
            g.FillPolygon(_brush, new[] { new PointF(x1, y1), new PointF(x2, y2), new PointF(x3, y3) });
        }
        private void DrawString(float xLeft, float yTop, string textString, byte invert, Graphics g)
        {
            if (xLeft < -10000 || yTop < -10000) return; //prevent overflow errors when exiting BMS flying
            var curX = xLeft;
            var curY = yTop;
            var font = _bmsFonts.Where(x => string.Equals(Path.GetFileName(x.TextureFile), _fontTexture, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (font == null) return;
            var originalForegroundColor = _foreColor;
            var originalBackgroundColor = _backColor;
            if (invert == 1) //invert text
            {
                //swap foreground and background colors
                SetForegroundColor(originalBackgroundColor);
                SetBackgroundColor(originalForegroundColor);
            }
            foreach (var character in UnescapeComma(textString).ToCharArray())
            {
                var charMetric = font.FontMetrics.Where(x => x.idx == character).First();
                curX += charMetric.lead;
                var destRect = new Rectangle((int)curX, (int)curY, charMetric.width, charMetric.height);
                var srcRect = new RectangleF(charMetric.left, charMetric.top, charMetric.width, charMetric.height);
                if (invert == 1)
                {
                    g.FillRectangle(new SolidBrush(originalForegroundColor), destRect);
                }
                g.DrawImage(font.Texture, destRect, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, _imageAttrs);
                curX += charMetric.width;
                curX += charMetric.trail;
            }
            if (invert == 1) //invert text
            {
                //swap foreground and background colors back to originals
                SetForegroundColor(originalForegroundColor);
                SetBackgroundColor(originalBackgroundColor);
            }
        }
        private void DrawStringRotated(float xLeft, float yTop, string textString, float angle, Graphics g)
        {
            var origTransform = g.Transform;
            g.TranslateTransform(xLeft, yTop);
            g.RotateTransform((float)(angle * (180.0 / Math.PI)));
            g.TranslateTransform(-xLeft, -yTop);
            DrawString(xLeft, yTop, textString, 0, g);
            g.Transform = origTransform;

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            var curData = _smReader.GetCurrentData();
            var stringData = curData != null ? curData.StringData : null;
            var stringDataData = stringData != null ? stringData.data : null;

            var vectorDisplayDrawingData = curData != null ? curData.VectorDisplayDrawingData : null;
            var drawingCommands = vectorDisplayDrawingData != null ? vectorDisplayDrawingData.data : null;
            if (drawingCommands == null) return;
            var cockpitArtDir = stringDataData != null && stringDataData.Any(sd => sd.strId == (uint)StringIdentifier.ThrCockpitdir)
                                ? stringDataData.Where(sd => sd.strId == (uint)StringIdentifier.ThrCockpitdir).First().value
                                : "";
            _fontDir = Path.Combine(cockpitArtDir, "3DFont");

            Draw(drawingCommands);
        }
        private void Draw(IEnumerable<VectorDisplayDrawingData.VectorDisplayDrawingCommand> commands)
        {

            Image renderTarget = null;
            PictureBox pictureBox = null;
            Graphics g = null;

            foreach (var command in commands)
            {
                try
                {
                    switch ((VectorDisplayDrawingData.VectorDisplayDrawingCommandType)command.commandType)
                    {
                        case VectorDisplayDrawingData.VectorDisplayDrawingCommandType.SetDisplayType:
                            {
                                var displayType = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_SetDisplayType).displayType;
                                switch ((VectorDisplayDrawingData.VectorDisplayType) displayType)
                                {
                                    case VectorDisplayDrawingData.VectorDisplayType.HUD:
                                        renderTarget = _HUDImage;
                                        pictureBox = pbHUD;
                                        g = Graphics.FromImage(renderTarget);
                                        break;
                                    case VectorDisplayDrawingData.VectorDisplayType.RWR:
                                        renderTarget = _RWRImage;
                                        pictureBox = pbRWR;
                                        break;
                                    case VectorDisplayDrawingData.VectorDisplayType.HMS:
                                        renderTarget = _HMSImage;
                                        pictureBox = pbHMS;
                                        break;
                                }
                                if (renderTarget != null)
                                {
                                    g = Graphics.FromImage(renderTarget);
                                    g.Clear(Color.Black);
                                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    g.TextRenderingHint = TextRenderingHint.AntiAlias;
                                    g.CompositingQuality = CompositingQuality.HighQuality;
                                }
                            }
                            break;
                        case VectorDisplayDrawingData.VectorDisplayDrawingCommandType.SetResolution:
                            {
                                var width = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_SetResolution).width;
                                var height = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_SetResolution).height;

                                if (renderTarget == null || renderTarget.Width != width || renderTarget.Height != height)
                                {
                                    renderTarget = new Bitmap((int)width, (int)height);
                                    if (pictureBox != null)
                                    {
                                        pictureBox.Image = renderTarget;
                                        pictureBox.Refresh();
                                    }
                                }
                            }
                            break;
                        case VectorDisplayDrawingData.VectorDisplayDrawingCommandType.SetForegroundColor:
                            {
                                var packedABGR = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_SetForegroundColor).packedABGR;
                                SetForegroundColor(packedABGR);
                            }
                            break;
                        case VectorDisplayDrawingData.VectorDisplayDrawingCommandType.SetBackgroundColor:
                            {
                                var packedABGR = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_SetBackgroundColor).packedABGR;
                                SetBackgroundColor(packedABGR);
                            }
                            break;
                        case VectorDisplayDrawingData.VectorDisplayDrawingCommandType.SetFont:
                            {
                                _fontTexture = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_SetFont).fontFile;
                            }
                            break;
                        case VectorDisplayDrawingData.VectorDisplayDrawingCommandType.DrawPoint:
                            {
                                var x = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawPoint).x;
                                var y = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawPoint).y;
                                DrawPoint(x, y, g);
                            }
                            break;
                        case VectorDisplayDrawingData.VectorDisplayDrawingCommandType.DrawLine:
                            {
                                var x1 = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawLine).x1;
                                var y1 = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawLine).y1;
                                var x2 = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawLine).x2;
                                var y2 = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawLine).y2;
                                DrawLine(x1, y1, x2, y2, g);
                            }
                            break;
                        case VectorDisplayDrawingData.VectorDisplayDrawingCommandType.DrawTri:
                            {
                                var x1 = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawTri).x1;
                                var y1 = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawTri).y1;
                                var x2 = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawTri).x2;
                                var y2 = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawTri).y2;
                                var x3 = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawTri).x3;
                                var y3 = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawTri).y3;
                                DrawTri(x1, y1, x2, y2, x3, y3, g);
                            }
                            break;
                        case VectorDisplayDrawingData.VectorDisplayDrawingCommandType.DrawString:
                            {
                                var xLeft = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawString).xLeft;
                                var yTop = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawString).yTop;
                                var invert = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawString).invert;
                                var textString = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawString).textString;
                                DrawString(xLeft, yTop, textString, invert, g);
                            }
                            break;
                        case VectorDisplayDrawingData.VectorDisplayDrawingCommandType.DrawStringRotated:
                            {
                                var xLeft = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawStringRotated).xLeft;
                                var yTop = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawStringRotated).yTop;
                                var angle = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawStringRotated).angle;
                                var textString = (command as VectorDisplayDrawingData.VectorDisplayDrawingCommand_DrawStringRotated).textString;
                                DrawStringRotated(xLeft, yTop, textString, angle, g);
                            }
                            break;
                    }

                }
                catch { }
            }


            pictureBox.Update();
            pictureBox.Refresh();
        }

        private BmsFont LoadBmsFont(string fontFile)
        {
            var alreadyLoadedFont = _bmsFonts.Where(x => String.Equals(Path.GetFileName(x.TextureFile), fontFile, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (alreadyLoadedFont != null) return alreadyLoadedFont;
            var fontFullPath = Path.Combine(_fontDir, fontFile);
            var rctPath = Path.Combine(_fontDir, Path.GetFileNameWithoutExtension(fontFile) + ".rct");
            var bmsFont = new BmsFont(fontFullPath, rctPath);
            _bmsFonts.Add(bmsFont);
            return bmsFont;
        }
    }
}

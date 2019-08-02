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
        private const float IMAGE_OVERRUN_PADDING_PIXELS= 0;
        private Color _foreColor = Color.Green;
        private Color _backColor = Color.Black;

        private Brush _brush = new SolidBrush(Color.Green);
        private Pen _pen = new Pen(Color.Green, width: 1);
        private ImageAttributes _imageAttrs;

        private string _fontFile;
        private HashSet<BmsFont> _bmsFonts = new HashSet<BmsFont>();
        private Image _HUDImage;
        private Image _RWRImage;
        private Image _HMSImage;
        private Image _LMFDImage;
        private Image _RMFDImage;
        private string _fontDir;
        private byte _displayType;
        private uint _hudDataSize;
        private uint _rwrDataSize;
        private uint _hmsDataSize;
        private uint _lmfdDataSize;
        private uint _rmfdDataSize;
        private F4SharedMem.Reader _smReader = new F4SharedMem.Reader();

        public frmMain()
        {
            InitializeComponent();
            pbHUD.Image = _HUDImage;
            pbRWR.Image = _RWRImage;
            pbHMS.Image = _HMSImage;
            pbLMFD.Image = _LMFDImage;
            pbRMFD.Image = _RMFDImage;

            pbHUD.Refresh();
            pbRWR.Refresh();
            pbHMS.Refresh();
            pbLMFD.Refresh();
            pbRMFD.Refresh();

            SetForegroundColor(Color.Green);
            SetBackgroundColor(Color.Black);
            timer1.Start();
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
            if (string.IsNullOrWhiteSpace(fontFile) || string.IsNullOrWhiteSpace(_fontDir)) return;
            LoadBmsFont(fontFile);
            _fontFile = fontFile;
        }
        private void DrawPoint(float x1, float y1, Graphics g)
        {
            if (IsAnyOutOfRange(x1, y1)) return;
            g.DrawLine(_pen, x1, y1, x1, y1);
        }
        private void DrawLine(float x1, float y1, float x2, float y2, Graphics g)
        {
            if (IsAnyOutOfRange(x1, y1,x2,y2)) return;
            g.DrawLine(_pen, x1, y1, x2, y2);
        }
        private void DrawTri(float x1, float y1, float x2, float y2, float x3, float y3, Graphics g)
        {
            if (IsAnyOutOfRange(x1,y1,x2,y2,x3,y3)) return;
            g.FillPolygon(_brush, new[] { new PointF(x1, y1), new PointF(x2, y2), new PointF(x3, y3) });
        }
        private void DrawString(float xLeft, float yTop, string textString, byte invert, Graphics g)
        {
            if (IsAnyOutOfRange(xLeft, yTop)) return;
            if (xLeft < 0 || yTop < 0 || float.IsNaN(xLeft) || float.IsNaN(yTop)) return;

            var curX = xLeft;
            var curY = yTop;
            var font = _bmsFonts.Where(x => string.Equals(Path.GetFileName(x.TextureFile), _fontFile, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (font == null) return;
            var originalForegroundColor = _foreColor;
            var originalBackgroundColor = _backColor;
            if (invert == 1) //invert text
            {
                //swap foreground and background colors
                SetForegroundColor(originalBackgroundColor);
                SetBackgroundColor(originalForegroundColor);
            }

            if (invert == 1) //draw inverted-color bounding rectangle 
            {
                var left = xLeft;
                var top = yTop;
                var width = 0f;
                var height = 0f;
                foreach (var character in textString.ToCharArray())
                {
                    var charMetric = font.FontMetrics.Where(x => x.idx == character).First();
                    width += charMetric.lead + charMetric.width + charMetric.trail;
                    height = Math.Max(height, charMetric.height);
                }
                g.FillRectangle(new SolidBrush(originalForegroundColor), xLeft-1, yTop-1, width+2, height);
            }

            //draw the text itself
            foreach (var character in textString.ToCharArray())
            {
                var charMetric = font.FontMetrics.Where(x => x.idx == character).First();
                curX += charMetric.lead;
                var destRect = new Rectangle((int)curX, (int)curY, charMetric.width, charMetric.height);
                var srcRect = new RectangleF(charMetric.left, charMetric.top, charMetric.width, charMetric.height);
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
            if (IsAnyOutOfRange(xLeft, yTop)) return;

            var origTransform = g.Transform;
            g.TranslateTransform(xLeft, yTop);
            g.RotateTransform((float)(angle * (180.0 / Math.PI)));
            g.TranslateTransform(-xLeft, -yTop);
            DrawString(xLeft, yTop, textString, 0, g);
            g.Transform = origTransform;

        }
        private bool IsAnyOutOfRange(params float[] parms)
        {
            foreach (var parm in parms)
            {
                if (parm < 0 || float.IsNaN(parm) || float.IsInfinity(parm)) return true;
            }
            return false;
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            F4SharedMem.FlightData flightData =null;
            try { flightData = _smReader.GetCurrentData(); } catch { }
            StringData stringData = null;
            try { stringData = flightData != null ? flightData.StringData : null; } catch { } 
            var stringDataData = stringData != null ? stringData.data : null;
            DrawingData drawingData = null;
            try { drawingData = flightData != null ? flightData.DrawingData : null; } catch { }
            var drawingCommands = drawingData != null ? drawingData.data : null;
            if (drawingCommands == null || drawingCommands.Count() ==0) return;
            var cockpitArtDir = stringDataData != null && stringDataData.Any(sd => sd.strId == (uint)StringIdentifier.ThrCockpitdir)
                                ? stringDataData.Where(sd => sd.strId == (uint)StringIdentifier.ThrCockpitdir).First().value
                                : "";
            _fontDir = string.IsNullOrWhiteSpace(cockpitArtDir) ? "" : Path.Combine(cockpitArtDir, "3DFont");

            Draw(drawingCommands);
        }
        private void Draw(IEnumerable<DrawingData.DrawingCommand> commands)
        {
            Image renderTarget = null;
            PictureBox pictureBox = null;
            Graphics g = null;

            foreach (var command in commands)
            {
                try
                {
                    switch ((DrawingData.CommandType)command.commandType)
                    {
                        case DrawingData.CommandType.SetDisplayType:
                            {
                                _displayType = (command as DrawingData.DrawingCommand_SetDisplayType).displayType;
                                switch ((DrawingData.DisplayType)_displayType)
                                {
                                    case DrawingData.DisplayType.HUD:
                                        _hudDataSize = 0;
                                        break;
                                    case DrawingData.DisplayType.RWR:
                                        _rwrDataSize = 0;
                                        break;
                                    case DrawingData.DisplayType.HMS:
                                        _hmsDataSize = 0;
                                        break;
                                    case DrawingData.DisplayType.LMFD:
                                        _lmfdDataSize = 0;
                                        break;
                                    case DrawingData.DisplayType.RMFD:
                                        _rmfdDataSize = 0;
                                        break;
                                }
                            }
                            break;
                        case DrawingData.CommandType.SetResolution:
                            {
                                var width = (command as DrawingData.DrawingCommand_SetResolution).width;
                                var height = (command as DrawingData.DrawingCommand_SetResolution).height;

                                switch ((DrawingData.DisplayType)_displayType)
                                {
                                    case DrawingData.DisplayType.HUD:
                                        renderTarget = _HUDImage != null && _HUDImage.Width == width + IMAGE_OVERRUN_PADDING_PIXELS && _HUDImage.Height == height + IMAGE_OVERRUN_PADDING_PIXELS ? _HUDImage : new Bitmap((int)(width + IMAGE_OVERRUN_PADDING_PIXELS), (int)(height + IMAGE_OVERRUN_PADDING_PIXELS));
                                        _HUDImage = renderTarget;
                                        pictureBox = pbHUD;
                                        pictureBox.Image = _HUDImage;
                                        break;
                                    case DrawingData.DisplayType.RWR:
                                        renderTarget = _RWRImage != null && _RWRImage.Width == width + IMAGE_OVERRUN_PADDING_PIXELS && _RWRImage.Height == height + IMAGE_OVERRUN_PADDING_PIXELS ? _RWRImage : new Bitmap((int)(width + IMAGE_OVERRUN_PADDING_PIXELS), (int)(height + IMAGE_OVERRUN_PADDING_PIXELS));

                                        _RWRImage = renderTarget;
                                        pictureBox = pbRWR;
                                        pictureBox.Image = _RWRImage;
                                        break;
                                    case DrawingData.DisplayType.HMS:
                                        renderTarget = _HMSImage != null && _HMSImage.Width == width + IMAGE_OVERRUN_PADDING_PIXELS && _HMSImage.Height == height  + IMAGE_OVERRUN_PADDING_PIXELS ? _HMSImage : new Bitmap((int)(width + IMAGE_OVERRUN_PADDING_PIXELS), (int)(height + IMAGE_OVERRUN_PADDING_PIXELS));

                                        _HMSImage = renderTarget;
                                        pictureBox = pbHMS;
                                        pictureBox.Image = _HMSImage;
                                        break;
                                    case DrawingData.DisplayType.LMFD:
                                        renderTarget = _LMFDImage != null && _LMFDImage.Width == width + IMAGE_OVERRUN_PADDING_PIXELS && _LMFDImage.Height == height + IMAGE_OVERRUN_PADDING_PIXELS ? _LMFDImage : new Bitmap((int)(width + IMAGE_OVERRUN_PADDING_PIXELS), (int)(height + IMAGE_OVERRUN_PADDING_PIXELS));

                                        _LMFDImage = renderTarget;
                                        pictureBox = pbLMFD;
                                        pictureBox.Image = _LMFDImage;
                                        break;
                                    case DrawingData.DisplayType.RMFD:
                                        renderTarget = _RMFDImage != null && _RMFDImage.Width == width + IMAGE_OVERRUN_PADDING_PIXELS && _RMFDImage.Height == height + IMAGE_OVERRUN_PADDING_PIXELS ? _RMFDImage : new Bitmap((int)(width + IMAGE_OVERRUN_PADDING_PIXELS), (int)(height + IMAGE_OVERRUN_PADDING_PIXELS));

                                        _RMFDImage = renderTarget;
                                        pictureBox = pbRMFD;
                                        pictureBox.Image = _RMFDImage;
                                        break;
                                    default:
                                        renderTarget = null;
                                        pictureBox = null;
                                        break;
                                }
                                if (pictureBox != null)
                                {
                                    pictureBox.Update();
                                    pictureBox.Refresh();
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
                        case DrawingData.CommandType.SetForegroundColor:
                            {
                                var packedABGR = (command as DrawingData.DrawingCommand_SetForegroundColor).packedABGR;
                                SetForegroundColor(packedABGR);
                            }
                            break;
                        case DrawingData.CommandType.SetBackgroundColor:
                            {
                                var packedABGR = (command as DrawingData.DrawingCommand_SetBackgroundColor).packedABGR;
                                SetBackgroundColor(packedABGR);
                            }
                            break;
                        case DrawingData.CommandType.SetFont:
                            {
                                _fontFile = (command as DrawingData.DrawingCommand_SetFont).fontFile;
                                SetFont(_fontFile);
                            }
                            break;
                        case DrawingData.CommandType.DrawPoint:
                            {
                                var x = (command as DrawingData.DrawingCommand_DrawPoint).x;
                                var y = (command as DrawingData.DrawingCommand_DrawPoint).y;
                                DrawPoint(x, y, g);
                            }
                            break;
                        case DrawingData.CommandType.DrawLine:
                            {
                                var x1 = (command as DrawingData.DrawingCommand_DrawLine).x1;
                                var y1 = (command as DrawingData.DrawingCommand_DrawLine).y1;
                                var x2 = (command as DrawingData.DrawingCommand_DrawLine).x2;
                                var y2 = (command as DrawingData.DrawingCommand_DrawLine).y2;
                                DrawLine(x1, y1, x2, y2, g);
                            }
                            break;
                        case DrawingData.CommandType.DrawTri:
                            {
                                var x1 = (command as DrawingData.DrawingCommand_DrawTri).x1;
                                var y1 = (command as DrawingData.DrawingCommand_DrawTri).y1;
                                var x2 = (command as DrawingData.DrawingCommand_DrawTri).x2;
                                var y2 = (command as DrawingData.DrawingCommand_DrawTri).y2;
                                var x3 = (command as DrawingData.DrawingCommand_DrawTri).x3;
                                var y3 = (command as DrawingData.DrawingCommand_DrawTri).y3;
                                DrawTri(x1, y1, x2, y2, x3, y3, g);
                            }
                            break;
                        case DrawingData.CommandType.DrawString:
                            {
                                var xLeft = (command as DrawingData.DrawingCommand_DrawString).xLeft;
                                var yTop = (command as DrawingData.DrawingCommand_DrawString).yTop;
                                var invert = (command as DrawingData.DrawingCommand_DrawString).invert;
                                var textString = (command as DrawingData.DrawingCommand_DrawString).textString;
                                DrawString(xLeft, yTop, textString, invert, g);
                            }
                            break;
                        case DrawingData.CommandType.DrawStringRotated:
                            {
                                var xLeft = (command as DrawingData.DrawingCommand_DrawStringRotated).xLeft;
                                var yTop = (command as DrawingData.DrawingCommand_DrawStringRotated).yTop;
                                var angle = (command as DrawingData.DrawingCommand_DrawStringRotated).angle;
                                var textString = (command as DrawingData.DrawingCommand_DrawStringRotated).textString;
                                DrawStringRotated(xLeft, yTop, textString, angle, g);
                            }
                            break;
                    }

                    switch ((DrawingData.DisplayType)_displayType)
                    {
                        case DrawingData.DisplayType.HUD:
                            _hudDataSize +=command.commandDataSize;
                            break;
                        case DrawingData.DisplayType.RWR:
                            _rwrDataSize += command.commandDataSize;
                            break;
                        case DrawingData.DisplayType.HMS:
                            _hmsDataSize += command.commandDataSize;
                            break;
                        case DrawingData.DisplayType.LMFD:
                            _lmfdDataSize += command.commandDataSize;
                            break;
                        case DrawingData.DisplayType.RMFD:
                            _rmfdDataSize += command.commandDataSize;
                            break;
                    }
                }
                catch { }
            }
            if (pictureBox != null)
            {
                pictureBox.Update();
                pictureBox.Refresh();
            }
            lblHUDDataSize.Text = $"Data Size: { (_hudDataSize / 1024.0).ToString("0.0") } KB";
            lblRWRDataSize.Text = $"Data Size: { (_rwrDataSize / 1024.0).ToString("0.0") } KB";
            lblHMSDataSize.Text = $"Data Size: { (_hmsDataSize / 1024.0).ToString("0.0") } KB";
            lblLMFDDataSize.Text = $"Data Size: { (_lmfdDataSize / 1024.0).ToString("0.0") } KB";
            lblRMFDDataSize.Text = $"Data Size: { (_rmfdDataSize / 1024.0).ToString("0.0") } KB";

        }

        private void LoadBmsFont(string fontFile)
        {
            if (string.IsNullOrWhiteSpace(fontFile)) return;
            var alreadyLoadedFont = _bmsFonts.Where(x => String.Equals(Path.GetFileName(x.TextureFile), fontFile, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (alreadyLoadedFont != null) return;
            var fontFullPath = Path.Combine(_fontDir, fontFile);
            var rctPath = Path.Combine(_fontDir, Path.GetFileNameWithoutExtension(fontFile) + ".rct");
            var bmsFont = new BmsFont(fontFullPath, rctPath);
            _bmsFonts.Add(bmsFont);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace F4SharedMem.Headers
{
    [Serializable]
    public class DrawingData
    {
        [Serializable]
        public enum CommandType : byte
        {
            SetDisplayType=0, //Set display type (see DisplayType)
            SetResolution, //Set canvas resolution
            SetForegroundColor, //Set foreground (text and line) color
            SetBackgroundColor, //Set background (text and line) color
            SetFont, //Set font
            DrawPoint, //Draw point
            DrawLine, //Draw line
            DrawTri, //Draw filled triangle
            DrawString, //Draw string
            DrawStringRotated, //Draw string with rotated text
            DrawingCommand_DIM // (number of identifiers; add new IDs only *above* this one)
        };
        [Serializable]
        public enum DisplayType : byte
        {
            HUD=0,
            RWR,
            LMFD,
            RMFD,
            HMS,
            Drawing2DDisplayType_DIM // (number of identifiers; add new IDs only *above* this one)
        };

        [Serializable]
        public class DrawingCommand
        {
            public byte commandType; 
            public uint commandDataSize;
        };
        [Serializable]
        public class DrawingCommand_SetDisplayType : DrawingCommand
        {
            public byte displayType; 
        };
        [Serializable]
        public class DrawingCommand_SetResolution : DrawingCommand
        {
            public uint width; //canvas width, in pixels
            public uint height; //canvas height, in pixels
        };
        [Serializable]
        public class DrawingCommand_SetFont : DrawingCommand
        {
            public string fontFile;
        };
        [Serializable]
        public class DrawingCommand_DrawPoint : DrawingCommand
        {
            public float x; //X coordinate
            public float y; //Y coordinate
        };
        [Serializable]
        public class DrawingCommand_DrawLine : DrawingCommand
        {
            public float x1; //starting X coordinate
            public float y1; //starting Y coordinate
            public float x2; //ending X coordinate
            public float y2; //ending Y coordinate
        };
        [Serializable]
        public class DrawingCommand_DrawTri : DrawingCommand
        {
            public float x1; //vertex 1 X coordinate
            public float y1; //vertex 1 Y coordinate
            public float x2; //vertex 2 X coordinate
            public float y2; //vertex 2 Y coordinate
            public float x3; //vertex 3 X coordinate
            public float y3; //vertex 3 Y coordinate
        };
        [Serializable]
        public class DrawingCommand_DrawString : DrawingCommand
        {
            public float xLeft; //X coordinate of left of text box
            public float yTop; //Y coordinate of top of text box
            public byte invert; // 0=draw text normally; 1=draw text with inverted background/foreground shading
            public string textString; //string to draw
        };
        [Serializable]
        public class DrawingCommand_DrawStringRotated : DrawingCommand
        {
            public float xLeft; //X coordinate of left of text box
            public float yTop; //Y coordinate of top of text box
            public float angle; //rotation angle(radians)
            public string textString; //string to draw
        };
        [Serializable]
        public class DrawingCommand_SetForegroundColor : DrawingCommand
        {
            public uint packedABGR; //foreground color in packed Alpha - Blue - Green - Red bit order(8 bits for each component)
                             //alpha: bits 24-31 (most significant 8 bits)
                             //blue:  bits 16-23
                             //green: bits 8-15
                             //red:   bits 0-7 (least significant 8 bits)
        };
        [Serializable]
        public class DrawingCommand_SetBackgroundColor : DrawingCommand
        {
            public uint packedABGR; //background color in packed Alpha - Blue - Green - Red bit order(8 bits for each component)
                             //alpha: bits 24-31 (most significant 8 bits)
                             //blue:  bits 16-23
                             //green: bits 8-15
                             //red:   bits 0-7 (least significant 8 bits)
        };


        public int VersionNum;          // Version of the DrawingCommands shared memory area 
        public uint NoOfCommands;         //Number of commands
        public uint dataSize;             // The overall size of the "data" blob that follows
        public IEnumerable<DrawingCommand> data; // 2D drawing commands

        internal static DrawingData GetDrawingData(byte[] inputbuffer)
        {
            var result=new DrawingData();

            if (inputbuffer !=null)
            {
                int offset = 0;
                result.VersionNum = BitConverter.ToInt32(inputbuffer, offset);
                offset += sizeof(int);
                result.NoOfCommands = BitConverter.ToUInt32(inputbuffer, offset);
                offset += sizeof(uint);
                result.dataSize = BitConverter.ToUInt32(inputbuffer, offset);
                offset += sizeof(uint);
                result.data = new List<DrawingCommand>();


                for (var i = 0u;i<result.NoOfCommands;i++)
                {
                    var commandType =inputbuffer[offset];
                    offset++;

                    var commandDataSize = BitConverter.ToUInt32(inputbuffer, offset);
                    offset+=sizeof(uint);

                    var startingOffsetThisCommandData = offset;
                    switch ((CommandType)commandType)
                    {
                        case CommandType.SetDisplayType:
                            {
                                var cmd = new DrawingCommand_SetDisplayType();
                                cmd.commandType = commandType;
                                cmd.commandDataSize = commandDataSize;
                                cmd.displayType = inputbuffer[offset];
                                offset++;
                                ((List<DrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                        case CommandType.SetResolution:
                            {
                                var cmd = new DrawingCommand_SetResolution();
                                cmd.commandType = commandType;
                                cmd.commandDataSize = commandDataSize;
                                cmd.width = BitConverter.ToUInt32(inputbuffer, offset);
                                offset += sizeof(uint);
                                cmd.height = BitConverter.ToUInt32(inputbuffer, offset);
                                offset += sizeof(uint);
                                ((List<DrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                        case CommandType.SetForegroundColor:
                            {
                                var cmd = new DrawingCommand_SetForegroundColor();
                                cmd.commandType = commandType;
                                cmd.commandDataSize = commandDataSize;

                                cmd.packedABGR = BitConverter.ToUInt32(inputbuffer, offset);
                                offset += sizeof(uint);

                                ((List<DrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                        case CommandType.SetBackgroundColor:
                            {
                                var cmd = new DrawingCommand_SetBackgroundColor();
                                cmd.commandType = commandType;
                                cmd.commandDataSize = commandDataSize;

                                cmd.packedABGR = BitConverter.ToUInt32(inputbuffer, offset);
                                offset += sizeof(uint);

                                ((List<DrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                        case CommandType.SetFont:
                            {
                                var cmd = new DrawingCommand_SetFont();
                                cmd.commandType = commandType;
                                cmd.commandDataSize = commandDataSize;

                                var fontFileLen = BitConverter.ToUInt32(inputbuffer, offset);
                                offset += sizeof(uint);

                                cmd.fontFile = Encoding.Default.GetString(inputbuffer, offset, (int)fontFileLen);
                                offset += (int)fontFileLen;

                                ((List<DrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                        case CommandType.DrawPoint:
                            {
                                var cmd = new DrawingCommand_DrawPoint();
                                cmd.commandType = commandType;
                                cmd.commandDataSize = commandDataSize;

                                cmd.x = BitConverter.ToSingle(inputbuffer,offset);
                                offset += sizeof(float);

                                cmd.y = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                ((List<DrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                        case CommandType.DrawLine:
                            {
                                var cmd = new DrawingCommand_DrawLine();
                                cmd.commandType = commandType;
                                cmd.commandDataSize = commandDataSize;

                                cmd.x1 = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                cmd.y1 = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                cmd.x2 = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                cmd.y2 = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                ((List<DrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                        case CommandType.DrawTri:
                            {
                                var cmd = new DrawingCommand_DrawTri();
                                cmd.commandType = commandType;
                                cmd.commandDataSize = commandDataSize;

                                cmd.x1 = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                cmd.y1 = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                cmd.x2 = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                cmd.y2 = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                cmd.x3 = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                cmd.y3 = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                ((List<DrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                        case CommandType.DrawString:
                            {
                                var cmd = new DrawingCommand_DrawString();
                                cmd.commandType = commandType;
                                cmd.commandDataSize = commandDataSize;

                                cmd.xLeft = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                cmd.yTop = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                cmd.invert = inputbuffer[offset];
                                offset++;

                                var strLen = BitConverter.ToUInt32(inputbuffer, offset);
                                offset += sizeof(uint);

                                cmd.textString = Encoding.Default.GetString(inputbuffer,offset, (int)strLen);
                                offset += (int)strLen;

                                ((List<DrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                        case CommandType.DrawStringRotated:
                            {
                                var cmd = new DrawingCommand_DrawStringRotated();
                                cmd.commandType = commandType;
                                cmd.commandDataSize = commandDataSize;

                                cmd.xLeft = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                cmd.yTop = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                cmd.angle = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                var strLen = BitConverter.ToUInt32(inputbuffer, offset);
                                offset += sizeof(uint);

                                cmd.textString = Encoding.Default.GetString(inputbuffer, offset, (int)strLen);
                                offset += (int)strLen;

                                ((List<DrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                    }
                    offset = startingOffsetThisCommandData + (int)commandDataSize;
                }
            }

            return result;
        }
    }
}

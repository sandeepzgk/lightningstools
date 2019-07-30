using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace F4SharedMem.Headers
{
    [Serializable]
    public class VectorDisplayDrawingData
    {
        [Serializable]
        public enum VectorDisplayDrawingCommandType : byte
        {
            SetDisplayType = 0, //Set display type (see VectorDisplayType)
            SetResolution, //Set canvas resolution
            SetForegroundColor, //Set foreground (text and line) color
            SetBackgroundColor, //Set background (text and line) color
            SetFont, //Set font
            DrawPoint, //Draw point
            DrawLine, //Draw line
            DrawTri, //Draw filled triangle
            DrawString, //Draw string
            DrawStringRotated, //Draw string with rotated text
            VectorDisplayDrawingCommand_DIM // (number of identifiers; add new IDs only *above* this one)
        };
        [Serializable]
        public enum VectorDisplayType : byte
        {
            HUD = 0,
            RWR,
            HMS,
            Drawing2DDisplayType_DIM // (number of identifiers; add new IDs only *above* this one)
        };

        [Serializable]
        public class VectorDisplayDrawingCommand
        {
            public byte commandType; 
        };
        [Serializable]
        public class VectorDisplayDrawingCommand_SetDisplayType : VectorDisplayDrawingCommand
        {
            public byte displayType; 
        };
        [Serializable]
        public class VectorDisplayDrawingCommand_SetResolution : VectorDisplayDrawingCommand
        {
            public uint width; //canvas width, in pixels
            public uint height; //canvas height, in pixels
        };
        [Serializable]
        public class VectorDisplayDrawingCommand_SetFont : VectorDisplayDrawingCommand
        {
            public string fontFile;
        };
        [Serializable]
        public class VectorDisplayDrawingCommand_DrawPoint : VectorDisplayDrawingCommand
        {
            public float x; //X coordinate
            public float y; //Y coordinate
        };
        [Serializable]
        public class VectorDisplayDrawingCommand_DrawLine : VectorDisplayDrawingCommand
        {
            public float x1; //starting X coordinate
            public float y1; //starting Y coordinate
            public float x2; //ending X coordinate
            public float y2; //ending Y coordinate
        };
        [Serializable]
        public class VectorDisplayDrawingCommand_DrawTri : VectorDisplayDrawingCommand
        {
            public float x1; //vertex 1 X coordinate
            public float y1; //vertex 1 Y coordinate
            public float x2; //vertex 2 X coordinate
            public float y2; //vertex 2 Y coordinate
            public float x3; //vertex 3 X coordinate
            public float y3; //vertex 3 Y coordinate
        };
        [Serializable]
        public class VectorDisplayDrawingCommand_DrawString : VectorDisplayDrawingCommand
        {
            public float xLeft; //X coordinate of left of text box
            public float yTop; //Y coordinate of top of text box
            public byte invert; // 0=draw text normally; 1=draw text with inverted background/foreground shading
            public string textString; //string to draw
        };
        [Serializable]
        public class VectorDisplayDrawingCommand_DrawStringRotated : VectorDisplayDrawingCommand
        {
            public float xLeft; //X coordinate of left of text box
            public float yTop; //Y coordinate of top of text box
            public float angle; //rotation angle(radians)
            public string textString; //string to draw
        };
        [Serializable]
        public class VectorDisplayDrawingCommand_SetForegroundColor : VectorDisplayDrawingCommand
        {
            public uint packedABGR; //foreground color in packed Alpha - Blue - Green - Red bit order(8 bits for each component)
                             //alpha: bits 24-31 (most significant 8 bits)
                             //blue:  bits 16-23
                             //green: bits 8-15
                             //red:   bits 0-7 (least significant 8 bits)
        };
        [Serializable]
        public class VectorDisplayDrawingCommand_SetBackgroundColor : VectorDisplayDrawingCommand
        {
            public uint packedABGR; //background color in packed Alpha - Blue - Green - Red bit order(8 bits for each component)
                             //alpha: bits 24-31 (most significant 8 bits)
                             //blue:  bits 16-23
                             //green: bits 8-15
                             //red:   bits 0-7 (least significant 8 bits)
        };


        public uint VersionNum;          // Version of the Vectors shared memory area 
        public uint NoOfCommands;          // How many commands do we have?
        public uint dataSize;             // The overall size of the "data" blob that follows
        public IEnumerable<VectorDisplayDrawingCommand> data; // Data storage blob for all the commands
                     // If you parse the data yourself, it should be handled as char[dataSize] by external apps, vector is only used in BMS internally for storage purposes
                     // Note that this can NOT be treated as VectorDisplayDrawingCommand[NoOfCommands] by external apps, due to the flexible size of VectorDisplayDrawingCommand!

        internal static VectorDisplayDrawingData GetVectorDisplayDrawingData(byte[] inputbuffer)
        {
            var result=new VectorDisplayDrawingData();

            if (inputbuffer !=null)
            {
                int offset = 0;
                result.VersionNum = BitConverter.ToUInt32(inputbuffer, offset);
                offset += sizeof(uint);
                result.NoOfCommands = BitConverter.ToUInt32(inputbuffer, offset);
                offset += sizeof(uint);
                result.dataSize = BitConverter.ToUInt32(inputbuffer, offset);
                offset += sizeof(uint);
                result.data = new List<VectorDisplayDrawingCommand>();


                for (uint i = 0; i < result.NoOfCommands; i++)
                {
                    byte commandType =inputbuffer[offset];
                    offset++;

                    switch ((VectorDisplayDrawingCommandType)commandType)
                    {
                        case VectorDisplayDrawingCommandType.SetDisplayType:
                            {
                                var cmd = new VectorDisplayDrawingCommand_SetDisplayType();
                                cmd.commandType = commandType;
                                cmd.displayType = inputbuffer[offset];
                                offset++;
                                ((List<VectorDisplayDrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                        case VectorDisplayDrawingCommandType.SetResolution:
                            {
                                var cmd = new VectorDisplayDrawingCommand_SetResolution();
                                cmd.commandType = commandType;
                                cmd.width = BitConverter.ToUInt32(inputbuffer, offset);
                                offset += sizeof(uint);
                                cmd.height = BitConverter.ToUInt32(inputbuffer, offset);
                                offset += sizeof(uint);
                                ((List<VectorDisplayDrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                        case VectorDisplayDrawingCommandType.SetForegroundColor:
                            {
                                var cmd = new VectorDisplayDrawingCommand_SetForegroundColor();
                                cmd.commandType = commandType;
                                cmd.packedABGR = BitConverter.ToUInt32(inputbuffer, offset);
                                offset += sizeof(uint);
                                ((List<VectorDisplayDrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                        case VectorDisplayDrawingCommandType.SetBackgroundColor:
                            {
                                var cmd = new VectorDisplayDrawingCommand_SetBackgroundColor();
                                cmd.commandType = commandType;
                                cmd.packedABGR = BitConverter.ToUInt32(inputbuffer, offset);
                                offset += sizeof(uint);
                                ((List<VectorDisplayDrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                        case VectorDisplayDrawingCommandType.SetFont:
                            {
                                var cmd = new VectorDisplayDrawingCommand_SetFont();
                                cmd.commandType = commandType;
                                var fontFileLen = BitConverter.ToUInt32(inputbuffer, offset);
                                offset += sizeof(uint);

                                cmd.fontFile = Encoding.Default.GetString(inputbuffer, offset, (int)fontFileLen);
                                offset += (int)fontFileLen;
                                ((List<VectorDisplayDrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                        case VectorDisplayDrawingCommandType.DrawPoint:
                            {
                                var cmd = new VectorDisplayDrawingCommand_DrawPoint();
                                cmd.commandType = commandType;

                                cmd.x = BitConverter.ToSingle(inputbuffer,offset);
                                offset += sizeof(float);

                                cmd.y = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                ((List<VectorDisplayDrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                        case VectorDisplayDrawingCommandType.DrawLine:
                            {
                                var cmd = new VectorDisplayDrawingCommand_DrawLine();
                                cmd.commandType = commandType;

                                cmd.x1 = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                cmd.y1 = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                cmd.x2 = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                cmd.y2 = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                ((List<VectorDisplayDrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                        case VectorDisplayDrawingCommandType.DrawTri:
                            {
                                var cmd = new VectorDisplayDrawingCommand_DrawTri();
                                cmd.commandType = commandType;
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

                                ((List<VectorDisplayDrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                        case VectorDisplayDrawingCommandType.DrawString:
                            {
                                var cmd = new VectorDisplayDrawingCommand_DrawString();
                                cmd.commandType = commandType;

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

                                ((List<VectorDisplayDrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                        case VectorDisplayDrawingCommandType.DrawStringRotated:
                            {
                                var cmd = new VectorDisplayDrawingCommand_DrawStringRotated();
                                cmd.commandType = commandType;

                                cmd.xLeft = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                cmd.yTop = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                cmd.angle = BitConverter.ToSingle(inputbuffer, offset);
                                offset += sizeof(float);

                                var strLen = (uint)Marshal.ReadInt32(inputbuffer, offset);
                                offset += sizeof(uint);

                                cmd.textString = Encoding.Default.GetString(inputbuffer, offset, (int)strLen);
                                offset += (int)strLen;

                                ((List<VectorDisplayDrawingCommand>)result.data).Add(cmd);
                            }
                            break;
                    }
                }
            }

            return result;
        }
    }
}

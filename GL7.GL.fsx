(**
 * This file has no copyright assigned and is placed in the Public Domain.
 * This file is ported from part of the mingw-w64 runtime package.
 * No warranty is given; refer to the file DISCLAIMER.PD within this package.
 *)

module GL7.GL

open System
open System.Runtime.InteropServices

type Pin(o) =
    let gch = GCHandle.Alloc(o, GCHandleType.Pinned)
    member x.Addr = gch.AddrOfPinnedObject()
    interface IDisposable with member x.Dispose() = gch.Free()

type GLenum     = int  // uint32
type GLboolean  = bool // byte
type GLbitfield = int  // uint32
type GLbyte     = sbyte
type GLshort    = int16
type GLint      = int
type GLsizei    = int
type GLubyte    = byte
type GLushort   = uint16
type GLuint     = uint32
type GLfloat    = float32
type GLclampf   = float32
type GLdouble   = float
type GLclampd   = float

[<Literal>]
let DLL = "opengl32.dll"

let GL_VERSION_1_1 = 1

let GL_ACCUM = 0x0100
let GL_LOAD = 0x0101
let GL_RETURN = 0x0102
let GL_MULT = 0x0103
let GL_ADD = 0x0104

let GL_NEVER = 0x0200
let GL_LESS = 0x0201
let GL_EQUAL = 0x0202
let GL_LEQUAL = 0x0203
let GL_GREATER = 0x0204
let GL_NOTEQUAL = 0x0205
let GL_GEQUAL = 0x0206
let GL_ALWAYS = 0x0207

let GL_CURRENT_BIT = 0x00000001
let GL_POINT_BIT = 0x00000002
let GL_LINE_BIT = 0x00000004
let GL_POLYGON_BIT = 0x00000008
let GL_POLYGON_STIPPLE_BIT = 0x00000010
let GL_PIXEL_MODE_BIT = 0x00000020
let GL_LIGHTING_BIT = 0x00000040
let GL_FOG_BIT = 0x00000080
let GL_DEPTH_BUFFER_BIT = 0x00000100
let GL_ACCUM_BUFFER_BIT = 0x00000200
let GL_STENCIL_BUFFER_BIT = 0x00000400
let GL_VIEWPORT_BIT = 0x00000800
let GL_TRANSFORM_BIT = 0x00001000
let GL_ENABLE_BIT = 0x00002000
let GL_COLOR_BUFFER_BIT = 0x00004000
let GL_HINT_BIT = 0x00008000
let GL_EVAL_BIT = 0x00010000
let GL_LIST_BIT = 0x00020000
let GL_TEXTURE_BIT = 0x00040000
let GL_SCISSOR_BIT = 0x00080000
let GL_ALL_ATTRIB_BITS = 0x000fffff

let GL_POINTS = 0x0000
let GL_LINES = 0x0001
let GL_LINE_LOOP = 0x0002
let GL_LINE_STRIP = 0x0003
let GL_TRIANGLES = 0x0004
let GL_TRIANGLE_STRIP = 0x0005
let GL_TRIANGLE_FAN = 0x0006
let GL_QUADS = 0x0007
let GL_QUAD_STRIP = 0x0008
let GL_POLYGON = 0x0009

let GL_ZERO = 0
let GL_ONE = 1
let GL_SRC_COLOR = 0x0300
let GL_ONE_MINUS_SRC_COLOR = 0x0301
let GL_SRC_ALPHA = 0x0302
let GL_ONE_MINUS_SRC_ALPHA = 0x0303
let GL_DST_ALPHA = 0x0304
let GL_ONE_MINUS_DST_ALPHA = 0x0305

let GL_DST_COLOR = 0x0306
let GL_ONE_MINUS_DST_COLOR = 0x0307
let GL_SRC_ALPHA_SATURATE = 0x0308

let GL_TRUE = 1
let GL_FALSE = 0

let GL_CLIP_PLANE0 = 0x3000
let GL_CLIP_PLANE1 = 0x3001
let GL_CLIP_PLANE2 = 0x3002
let GL_CLIP_PLANE3 = 0x3003
let GL_CLIP_PLANE4 = 0x3004
let GL_CLIP_PLANE5 = 0x3005

let GL_BYTE = 0x1400
let GL_UNSIGNED_BYTE = 0x1401
let GL_SHORT = 0x1402
let GL_UNSIGNED_SHORT = 0x1403
let GL_INT = 0x1404
let GL_UNSIGNED_INT = 0x1405
let GL_FLOAT = 0x1406
let GL_2_BYTES = 0x1407
let GL_3_BYTES = 0x1408
let GL_4_BYTES = 0x1409
let GL_DOUBLE = 0x140A

let GL_NONE = 0
let GL_FRONT_LEFT = 0x0400
let GL_FRONT_RIGHT = 0x0401
let GL_BACK_LEFT = 0x0402
let GL_BACK_RIGHT = 0x0403
let GL_FRONT = 0x0404
let GL_BACK = 0x0405
let GL_LEFT = 0x0406
let GL_RIGHT = 0x0407
let GL_FRONT_AND_BACK = 0x0408
let GL_AUX0 = 0x0409
let GL_AUX1 = 0x040A
let GL_AUX2 = 0x040B
let GL_AUX3 = 0x040C

let GL_NO_ERROR = 0
let GL_INVALID_ENUM = 0x0500
let GL_INVALID_VALUE = 0x0501
let GL_INVALID_OPERATION = 0x0502
let GL_STACK_OVERFLOW = 0x0503
let GL_STACK_UNDERFLOW = 0x0504
let GL_OUT_OF_MEMORY = 0x0505

let GL_2D = 0x0600
let GL_3D = 0x0601
let GL_3D_COLOR = 0x0602
let GL_3D_COLOR_TEXTURE = 0x0603
let GL_4D_COLOR_TEXTURE = 0x0604

let GL_PASS_THROUGH_TOKEN = 0x0700
let GL_POINT_TOKEN = 0x0701
let GL_LINE_TOKEN = 0x0702
let GL_POLYGON_TOKEN = 0x0703
let GL_BITMAP_TOKEN = 0x0704
let GL_DRAW_PIXEL_TOKEN = 0x0705
let GL_COPY_PIXEL_TOKEN = 0x0706
let GL_LINE_RESET_TOKEN = 0x0707

let GL_EXP = 0x0800
let GL_EXP2 = 0x0801

let GL_CW = 0x0900
let GL_CCW = 0x0901

let GL_COEFF = 0x0A00
let GL_ORDER = 0x0A01
let GL_DOMAIN = 0x0A02

let GL_CURRENT_COLOR = 0x0B00
let GL_CURRENT_INDEX = 0x0B01
let GL_CURRENT_NORMAL = 0x0B02
let GL_CURRENT_TEXTURE_COORDS = 0x0B03
let GL_CURRENT_RASTER_COLOR = 0x0B04
let GL_CURRENT_RASTER_INDEX = 0x0B05
let GL_CURRENT_RASTER_TEXTURE_COORDS = 0x0B06
let GL_CURRENT_RASTER_POSITION = 0x0B07
let GL_CURRENT_RASTER_POSITION_VALID = 0x0B08
let GL_CURRENT_RASTER_DISTANCE = 0x0B09
let GL_POINT_SMOOTH = 0x0B10
let GL_POINT_SIZE = 0x0B11
let GL_POINT_SIZE_RANGE = 0x0B12
let GL_POINT_SIZE_GRANULARITY = 0x0B13
let GL_LINE_SMOOTH = 0x0B20
let GL_LINE_WIDTH = 0x0B21
let GL_LINE_WIDTH_RANGE = 0x0B22
let GL_LINE_WIDTH_GRANULARITY = 0x0B23
let GL_LINE_STIPPLE = 0x0B24
let GL_LINE_STIPPLE_PATTERN = 0x0B25
let GL_LINE_STIPPLE_REPEAT = 0x0B26
let GL_LIST_MODE = 0x0B30
let GL_MAX_LIST_NESTING = 0x0B31
let GL_LIST_BASE = 0x0B32
let GL_LIST_INDEX = 0x0B33
let GL_POLYGON_MODE = 0x0B40
let GL_POLYGON_SMOOTH = 0x0B41
let GL_POLYGON_STIPPLE = 0x0B42
let GL_EDGE_FLAG = 0x0B43
let GL_CULL_FACE = 0x0B44
let GL_CULL_FACE_MODE = 0x0B45
let GL_FRONT_FACE = 0x0B46
let GL_LIGHTING = 0x0B50
let GL_LIGHT_MODEL_LOCAL_VIEWER = 0x0B51
let GL_LIGHT_MODEL_TWO_SIDE = 0x0B52
let GL_LIGHT_MODEL_AMBIENT = 0x0B53
let GL_SHADE_MODEL = 0x0B54
let GL_COLOR_MATERIAL_FACE = 0x0B55
let GL_COLOR_MATERIAL_PARAMETER = 0x0B56
let GL_COLOR_MATERIAL = 0x0B57
let GL_FOG = 0x0B60
let GL_FOG_INDEX = 0x0B61
let GL_FOG_DENSITY = 0x0B62
let GL_FOG_START = 0x0B63
let GL_FOG_END = 0x0B64
let GL_FOG_MODE = 0x0B65
let GL_FOG_COLOR = 0x0B66
let GL_DEPTH_RANGE = 0x0B70
let GL_DEPTH_TEST = 0x0B71
let GL_DEPTH_WRITEMASK = 0x0B72
let GL_DEPTH_CLEAR_VALUE = 0x0B73
let GL_DEPTH_FUNC = 0x0B74
let GL_ACCUM_CLEAR_VALUE = 0x0B80
let GL_STENCIL_TEST = 0x0B90
let GL_STENCIL_CLEAR_VALUE = 0x0B91
let GL_STENCIL_FUNC = 0x0B92
let GL_STENCIL_VALUE_MASK = 0x0B93
let GL_STENCIL_FAIL = 0x0B94
let GL_STENCIL_PASS_DEPTH_FAIL = 0x0B95
let GL_STENCIL_PASS_DEPTH_PASS = 0x0B96
let GL_STENCIL_REF = 0x0B97
let GL_STENCIL_WRITEMASK = 0x0B98
let GL_MATRIX_MODE = 0x0BA0
let GL_NORMALIZE = 0x0BA1
let GL_VIEWPORT = 0x0BA2
let GL_MODELVIEW_STACK_DEPTH = 0x0BA3
let GL_PROJECTION_STACK_DEPTH = 0x0BA4
let GL_TEXTURE_STACK_DEPTH = 0x0BA5
let GL_MODELVIEW_MATRIX = 0x0BA6
let GL_PROJECTION_MATRIX = 0x0BA7
let GL_TEXTURE_MATRIX = 0x0BA8
let GL_ATTRIB_STACK_DEPTH = 0x0BB0
let GL_CLIENT_ATTRIB_STACK_DEPTH = 0x0BB1
let GL_ALPHA_TEST = 0x0BC0
let GL_ALPHA_TEST_FUNC = 0x0BC1
let GL_ALPHA_TEST_REF = 0x0BC2
let GL_DITHER = 0x0BD0
let GL_BLEND_DST = 0x0BE0
let GL_BLEND_SRC = 0x0BE1
let GL_BLEND = 0x0BE2
let GL_LOGIC_OP_MODE = 0x0BF0
let GL_INDEX_LOGIC_OP = 0x0BF1
let GL_COLOR_LOGIC_OP = 0x0BF2
let GL_AUX_BUFFERS = 0x0C00
let GL_DRAW_BUFFER = 0x0C01
let GL_READ_BUFFER = 0x0C02
let GL_SCISSOR_BOX = 0x0C10
let GL_SCISSOR_TEST = 0x0C11
let GL_INDEX_CLEAR_VALUE = 0x0C20
let GL_INDEX_WRITEMASK = 0x0C21
let GL_COLOR_CLEAR_VALUE = 0x0C22
let GL_COLOR_WRITEMASK = 0x0C23
let GL_INDEX_MODE = 0x0C30
let GL_RGBA_MODE = 0x0C31
let GL_DOUBLEBUFFER = 0x0C32
let GL_STEREO = 0x0C33
let GL_RENDER_MODE = 0x0C40
let GL_PERSPECTIVE_CORRECTION_HINT = 0x0C50
let GL_POINT_SMOOTH_HINT = 0x0C51
let GL_LINE_SMOOTH_HINT = 0x0C52
let GL_POLYGON_SMOOTH_HINT = 0x0C53
let GL_FOG_HINT = 0x0C54
let GL_TEXTURE_GEN_S = 0x0C60
let GL_TEXTURE_GEN_T = 0x0C61
let GL_TEXTURE_GEN_R = 0x0C62
let GL_TEXTURE_GEN_Q = 0x0C63
let GL_PIXEL_MAP_I_TO_I = 0x0C70
let GL_PIXEL_MAP_S_TO_S = 0x0C71
let GL_PIXEL_MAP_I_TO_R = 0x0C72
let GL_PIXEL_MAP_I_TO_G = 0x0C73
let GL_PIXEL_MAP_I_TO_B = 0x0C74
let GL_PIXEL_MAP_I_TO_A = 0x0C75
let GL_PIXEL_MAP_R_TO_R = 0x0C76
let GL_PIXEL_MAP_G_TO_G = 0x0C77
let GL_PIXEL_MAP_B_TO_B = 0x0C78
let GL_PIXEL_MAP_A_TO_A = 0x0C79
let GL_PIXEL_MAP_I_TO_I_SIZE = 0x0CB0
let GL_PIXEL_MAP_S_TO_S_SIZE = 0x0CB1
let GL_PIXEL_MAP_I_TO_R_SIZE = 0x0CB2
let GL_PIXEL_MAP_I_TO_G_SIZE = 0x0CB3
let GL_PIXEL_MAP_I_TO_B_SIZE = 0x0CB4
let GL_PIXEL_MAP_I_TO_A_SIZE = 0x0CB5
let GL_PIXEL_MAP_R_TO_R_SIZE = 0x0CB6
let GL_PIXEL_MAP_G_TO_G_SIZE = 0x0CB7
let GL_PIXEL_MAP_B_TO_B_SIZE = 0x0CB8
let GL_PIXEL_MAP_A_TO_A_SIZE = 0x0CB9
let GL_UNPACK_SWAP_BYTES = 0x0CF0
let GL_UNPACK_LSB_FIRST = 0x0CF1
let GL_UNPACK_ROW_LENGTH = 0x0CF2
let GL_UNPACK_SKIP_ROWS = 0x0CF3
let GL_UNPACK_SKIP_PIXELS = 0x0CF4
let GL_UNPACK_ALIGNMENT = 0x0CF5
let GL_PACK_SWAP_BYTES = 0x0D00
let GL_PACK_LSB_FIRST = 0x0D01
let GL_PACK_ROW_LENGTH = 0x0D02
let GL_PACK_SKIP_ROWS = 0x0D03
let GL_PACK_SKIP_PIXELS = 0x0D04
let GL_PACK_ALIGNMENT = 0x0D05
let GL_MAP_COLOR = 0x0D10
let GL_MAP_STENCIL = 0x0D11
let GL_INDEX_SHIFT = 0x0D12
let GL_INDEX_OFFSET = 0x0D13
let GL_RED_SCALE = 0x0D14
let GL_RED_BIAS = 0x0D15
let GL_ZOOM_X = 0x0D16
let GL_ZOOM_Y = 0x0D17
let GL_GREEN_SCALE = 0x0D18
let GL_GREEN_BIAS = 0x0D19
let GL_BLUE_SCALE = 0x0D1A
let GL_BLUE_BIAS = 0x0D1B
let GL_ALPHA_SCALE = 0x0D1C
let GL_ALPHA_BIAS = 0x0D1D
let GL_DEPTH_SCALE = 0x0D1E
let GL_DEPTH_BIAS = 0x0D1F
let GL_MAX_EVAL_ORDER = 0x0D30
let GL_MAX_LIGHTS = 0x0D31
let GL_MAX_CLIP_PLANES = 0x0D32
let GL_MAX_TEXTURE_SIZE = 0x0D33
let GL_MAX_PIXEL_MAP_TABLE = 0x0D34
let GL_MAX_ATTRIB_STACK_DEPTH = 0x0D35
let GL_MAX_MODELVIEW_STACK_DEPTH = 0x0D36
let GL_MAX_NAME_STACK_DEPTH = 0x0D37
let GL_MAX_PROJECTION_STACK_DEPTH = 0x0D38
let GL_MAX_TEXTURE_STACK_DEPTH = 0x0D39
let GL_MAX_VIEWPORT_DIMS = 0x0D3A
let GL_MAX_CLIENT_ATTRIB_STACK_DEPTH = 0x0D3B
let GL_SUBPIXEL_BITS = 0x0D50
let GL_INDEX_BITS = 0x0D51
let GL_RED_BITS = 0x0D52
let GL_GREEN_BITS = 0x0D53
let GL_BLUE_BITS = 0x0D54
let GL_ALPHA_BITS = 0x0D55
let GL_DEPTH_BITS = 0x0D56
let GL_STENCIL_BITS = 0x0D57
let GL_ACCUM_RED_BITS = 0x0D58
let GL_ACCUM_GREEN_BITS = 0x0D59
let GL_ACCUM_BLUE_BITS = 0x0D5A
let GL_ACCUM_ALPHA_BITS = 0x0D5B
let GL_NAME_STACK_DEPTH = 0x0D70
let GL_AUTO_NORMAL = 0x0D80
let GL_MAP1_COLOR_4 = 0x0D90
let GL_MAP1_INDEX = 0x0D91
let GL_MAP1_NORMAL = 0x0D92
let GL_MAP1_TEXTURE_COORD_1 = 0x0D93
let GL_MAP1_TEXTURE_COORD_2 = 0x0D94
let GL_MAP1_TEXTURE_COORD_3 = 0x0D95
let GL_MAP1_TEXTURE_COORD_4 = 0x0D96
let GL_MAP1_VERTEX_3 = 0x0D97
let GL_MAP1_VERTEX_4 = 0x0D98
let GL_MAP2_COLOR_4 = 0x0DB0
let GL_MAP2_INDEX = 0x0DB1
let GL_MAP2_NORMAL = 0x0DB2
let GL_MAP2_TEXTURE_COORD_1 = 0x0DB3
let GL_MAP2_TEXTURE_COORD_2 = 0x0DB4
let GL_MAP2_TEXTURE_COORD_3 = 0x0DB5
let GL_MAP2_TEXTURE_COORD_4 = 0x0DB6
let GL_MAP2_VERTEX_3 = 0x0DB7
let GL_MAP2_VERTEX_4 = 0x0DB8
let GL_MAP1_GRID_DOMAIN = 0x0DD0
let GL_MAP1_GRID_SEGMENTS = 0x0DD1
let GL_MAP2_GRID_DOMAIN = 0x0DD2
let GL_MAP2_GRID_SEGMENTS = 0x0DD3
let GL_TEXTURE_1D = 0x0DE0
let GL_TEXTURE_2D = 0x0DE1
let GL_FEEDBACK_BUFFER_POINTER = 0x0DF0
let GL_FEEDBACK_BUFFER_SIZE = 0x0DF1
let GL_FEEDBACK_BUFFER_TYPE = 0x0DF2
let GL_SELECTION_BUFFER_POINTER = 0x0DF3
let GL_SELECTION_BUFFER_SIZE = 0x0DF4

let GL_TEXTURE_WIDTH = 0x1000
let GL_TEXTURE_HEIGHT = 0x1001
let GL_TEXTURE_INTERNAL_FORMAT = 0x1003
let GL_TEXTURE_BORDER_COLOR = 0x1004
let GL_TEXTURE_BORDER = 0x1005

let GL_DONT_CARE = 0x1100
let GL_FASTEST = 0x1101
let GL_NICEST = 0x1102

let GL_LIGHT0 = 0x4000
let GL_LIGHT1 = 0x4001
let GL_LIGHT2 = 0x4002
let GL_LIGHT3 = 0x4003
let GL_LIGHT4 = 0x4004
let GL_LIGHT5 = 0x4005
let GL_LIGHT6 = 0x4006
let GL_LIGHT7 = 0x4007

let GL_AMBIENT = 0x1200
let GL_DIFFUSE = 0x1201
let GL_SPECULAR = 0x1202
let GL_POSITION = 0x1203
let GL_SPOT_DIRECTION = 0x1204
let GL_SPOT_EXPONENT = 0x1205
let GL_SPOT_CUTOFF = 0x1206
let GL_CONSTANT_ATTENUATION = 0x1207
let GL_LINEAR_ATTENUATION = 0x1208
let GL_QUADRATIC_ATTENUATION = 0x1209

let GL_COMPILE = 0x1300
let GL_COMPILE_AND_EXECUTE = 0x1301

let GL_CLEAR = 0x1500
let GL_AND = 0x1501
let GL_AND_REVERSE = 0x1502
let GL_COPY = 0x1503
let GL_AND_INVERTED = 0x1504
let GL_NOOP = 0x1505
let GL_XOR = 0x1506
let GL_OR = 0x1507
let GL_NOR = 0x1508
let GL_EQUIV = 0x1509
let GL_INVERT = 0x150A
let GL_OR_REVERSE = 0x150B
let GL_COPY_INVERTED = 0x150C
let GL_OR_INVERTED = 0x150D
let GL_NAND = 0x150E
let GL_SET = 0x150F

let GL_EMISSION = 0x1600
let GL_SHININESS = 0x1601
let GL_AMBIENT_AND_DIFFUSE = 0x1602
let GL_COLOR_INDEXES = 0x1603

let GL_MODELVIEW = 0x1700
let GL_PROJECTION = 0x1701
let GL_TEXTURE = 0x1702

let GL_COLOR = 0x1800
let GL_DEPTH = 0x1801
let GL_STENCIL = 0x1802

let GL_COLOR_INDEX = 0x1900
let GL_STENCIL_INDEX = 0x1901
let GL_DEPTH_COMPONENT = 0x1902
let GL_RED = 0x1903
let GL_GREEN = 0x1904
let GL_BLUE = 0x1905
let GL_ALPHA = 0x1906
let GL_RGB = 0x1907
let GL_RGBA = 0x1908
let GL_LUMINANCE = 0x1909
let GL_LUMINANCE_ALPHA = 0x190A

let GL_BITMAP = 0x1A00

let GL_POINT = 0x1B00
let GL_LINE = 0x1B01
let GL_FILL = 0x1B02

let GL_RENDER = 0x1C00
let GL_FEEDBACK = 0x1C01
let GL_SELECT = 0x1C02

let GL_FLAT = 0x1D00
let GL_SMOOTH = 0x1D01

let GL_KEEP = 0x1E00
let GL_REPLACE = 0x1E01
let GL_INCR = 0x1E02
let GL_DECR = 0x1E03

let GL_VENDOR = 0x1F00
let GL_RENDERER = 0x1F01
let GL_VERSION = 0x1F02
let GL_EXTENSIONS = 0x1F03

let GL_S = 0x2000
let GL_T = 0x2001
let GL_R = 0x2002
let GL_Q = 0x2003

let GL_MODULATE = 0x2100
let GL_DECAL = 0x2101

let GL_TEXTURE_ENV_MODE = 0x2200
let GL_TEXTURE_ENV_COLOR = 0x2201

let GL_TEXTURE_ENV = 0x2300

let GL_EYE_LINEAR = 0x2400
let GL_OBJECT_LINEAR = 0x2401
let GL_SPHERE_MAP = 0x2402

let GL_TEXTURE_GEN_MODE = 0x2500
let GL_OBJECT_PLANE = 0x2501
let GL_EYE_PLANE = 0x2502

let GL_NEAREST = 0x2600
let GL_LINEAR = 0x2601

let GL_NEAREST_MIPMAP_NEAREST = 0x2700
let GL_LINEAR_MIPMAP_NEAREST = 0x2701
let GL_NEAREST_MIPMAP_LINEAR = 0x2702
let GL_LINEAR_MIPMAP_LINEAR = 0x2703

let GL_TEXTURE_MAG_FILTER = 0x2800
let GL_TEXTURE_MIN_FILTER = 0x2801
let GL_TEXTURE_WRAP_S = 0x2802
let GL_TEXTURE_WRAP_T = 0x2803

let GL_CLAMP = 0x2900
let GL_REPEAT = 0x2901

let GL_CLIENT_PIXEL_STORE_BIT = 0x00000001
let GL_CLIENT_VERTEX_ARRAY_BIT = 0x00000002
let GL_CLIENT_ALL_ATTRIB_BITS = 0xffffffff

let GL_POLYGON_OFFSET_FACTOR = 0x8038
let GL_POLYGON_OFFSET_UNITS = 0x2A00
let GL_POLYGON_OFFSET_POINT = 0x2A01
let GL_POLYGON_OFFSET_LINE = 0x2A02
let GL_POLYGON_OFFSET_FILL = 0x8037

let GL_ALPHA4 = 0x803B
let GL_ALPHA8 = 0x803C
let GL_ALPHA12 = 0x803D
let GL_ALPHA16 = 0x803E
let GL_LUMINANCE4 = 0x803F
let GL_LUMINANCE8 = 0x8040
let GL_LUMINANCE12 = 0x8041
let GL_LUMINANCE16 = 0x8042
let GL_LUMINANCE4_ALPHA4 = 0x8043
let GL_LUMINANCE6_ALPHA2 = 0x8044
let GL_LUMINANCE8_ALPHA8 = 0x8045
let GL_LUMINANCE12_ALPHA4 = 0x8046
let GL_LUMINANCE12_ALPHA12 = 0x8047
let GL_LUMINANCE16_ALPHA16 = 0x8048
let GL_INTENSITY = 0x8049
let GL_INTENSITY4 = 0x804A
let GL_INTENSITY8 = 0x804B
let GL_INTENSITY12 = 0x804C
let GL_INTENSITY16 = 0x804D
let GL_R3_G3_B2 = 0x2A10
let GL_RGB4 = 0x804F
let GL_RGB5 = 0x8050
let GL_RGB8 = 0x8051
let GL_RGB10 = 0x8052
let GL_RGB12 = 0x8053
let GL_RGB16 = 0x8054
let GL_RGBA2 = 0x8055
let GL_RGBA4 = 0x8056
let GL_RGB5_A1 = 0x8057
let GL_RGBA8 = 0x8058
let GL_RGB10_A2 = 0x8059
let GL_RGBA12 = 0x805A
let GL_RGBA16 = 0x805B
let GL_TEXTURE_RED_SIZE = 0x805C
let GL_TEXTURE_GREEN_SIZE = 0x805D
let GL_TEXTURE_BLUE_SIZE = 0x805E
let GL_TEXTURE_ALPHA_SIZE = 0x805F
let GL_TEXTURE_LUMINANCE_SIZE = 0x8060
let GL_TEXTURE_INTENSITY_SIZE = 0x8061
let GL_PROXY_TEXTURE_1D = 0x8063
let GL_PROXY_TEXTURE_2D = 0x8064

let GL_TEXTURE_PRIORITY = 0x8066
let GL_TEXTURE_RESIDENT = 0x8067
let GL_TEXTURE_BINDING_1D = 0x8068
let GL_TEXTURE_BINDING_2D = 0x8069

let GL_VERTEX_ARRAY = 0x8074
let GL_NORMAL_ARRAY = 0x8075
let GL_COLOR_ARRAY = 0x8076
let GL_INDEX_ARRAY = 0x8077
let GL_TEXTURE_COORD_ARRAY = 0x8078
let GL_EDGE_FLAG_ARRAY = 0x8079
let GL_VERTEX_ARRAY_SIZE = 0x807A
let GL_VERTEX_ARRAY_TYPE = 0x807B
let GL_VERTEX_ARRAY_STRIDE = 0x807C
let GL_NORMAL_ARRAY_TYPE = 0x807E
let GL_NORMAL_ARRAY_STRIDE = 0x807F
let GL_COLOR_ARRAY_SIZE = 0x8081
let GL_COLOR_ARRAY_TYPE = 0x8082
let GL_COLOR_ARRAY_STRIDE = 0x8083
let GL_INDEX_ARRAY_TYPE = 0x8085
let GL_INDEX_ARRAY_STRIDE = 0x8086
let GL_TEXTURE_COORD_ARRAY_SIZE = 0x8088
let GL_TEXTURE_COORD_ARRAY_TYPE = 0x8089
let GL_TEXTURE_COORD_ARRAY_STRIDE = 0x808A
let GL_EDGE_FLAG_ARRAY_STRIDE = 0x808C
let GL_VERTEX_ARRAY_POINTER = 0x808E
let GL_NORMAL_ARRAY_POINTER = 0x808F
let GL_COLOR_ARRAY_POINTER = 0x8090
let GL_INDEX_ARRAY_POINTER = 0x8091
let GL_TEXTURE_COORD_ARRAY_POINTER = 0x8092
let GL_EDGE_FLAG_ARRAY_POINTER = 0x8093
let GL_V2F = 0x2A20
let GL_V3F = 0x2A21
let GL_C4UB_V2F = 0x2A22
let GL_C4UB_V3F = 0x2A23
let GL_C3F_V3F = 0x2A24
let GL_N3F_V3F = 0x2A25
let GL_C4F_N3F_V3F = 0x2A26
let GL_T2F_V3F = 0x2A27
let GL_T4F_V4F = 0x2A28
let GL_T2F_C4UB_V3F = 0x2A29
let GL_T2F_C3F_V3F = 0x2A2A
let GL_T2F_N3F_V3F = 0x2A2B
let GL_T2F_C4F_N3F_V3F = 0x2A2C
let GL_T4F_C4F_N3F_V4F = 0x2A2D

let GL_EXT_vertex_array = 1
let GL_EXT_bgra = 1
let GL_EXT_paletted_texture = 1
let GL_WIN_swap_hint = 1
let GL_WIN_draw_range_elements = 1

let GL_VERTEX_ARRAY_EXT = 0x8074
let GL_NORMAL_ARRAY_EXT = 0x8075
let GL_COLOR_ARRAY_EXT = 0x8076
let GL_INDEX_ARRAY_EXT = 0x8077
let GL_TEXTURE_COORD_ARRAY_EXT = 0x8078
let GL_EDGE_FLAG_ARRAY_EXT = 0x8079
let GL_VERTEX_ARRAY_SIZE_EXT = 0x807A
let GL_VERTEX_ARRAY_TYPE_EXT = 0x807B
let GL_VERTEX_ARRAY_STRIDE_EXT = 0x807C
let GL_VERTEX_ARRAY_COUNT_EXT = 0x807D
let GL_NORMAL_ARRAY_TYPE_EXT = 0x807E
let GL_NORMAL_ARRAY_STRIDE_EXT = 0x807F
let GL_NORMAL_ARRAY_COUNT_EXT = 0x8080
let GL_COLOR_ARRAY_SIZE_EXT = 0x8081
let GL_COLOR_ARRAY_TYPE_EXT = 0x8082
let GL_COLOR_ARRAY_STRIDE_EXT = 0x8083
let GL_COLOR_ARRAY_COUNT_EXT = 0x8084
let GL_INDEX_ARRAY_TYPE_EXT = 0x8085
let GL_INDEX_ARRAY_STRIDE_EXT = 0x8086
let GL_INDEX_ARRAY_COUNT_EXT = 0x8087
let GL_TEXTURE_COORD_ARRAY_SIZE_EXT = 0x8088
let GL_TEXTURE_COORD_ARRAY_TYPE_EXT = 0x8089
let GL_TEXTURE_COORD_ARRAY_STRIDE_EXT = 0x808A
let GL_TEXTURE_COORD_ARRAY_COUNT_EXT = 0x808B
let GL_EDGE_FLAG_ARRAY_STRIDE_EXT = 0x808C
let GL_EDGE_FLAG_ARRAY_COUNT_EXT = 0x808D
let GL_VERTEX_ARRAY_POINTER_EXT = 0x808E
let GL_NORMAL_ARRAY_POINTER_EXT = 0x808F
let GL_COLOR_ARRAY_POINTER_EXT = 0x8090
let GL_INDEX_ARRAY_POINTER_EXT = 0x8091
let GL_TEXTURE_COORD_ARRAY_POINTER_EXT = 0x8092
let GL_EDGE_FLAG_ARRAY_POINTER_EXT = 0x8093
let GL_DOUBLE_EXT = GL_DOUBLE

let GL_BGR_EXT = 0x80E0
let GL_BGRA_EXT = 0x80E1

let GL_COLOR_TABLE_FORMAT_EXT = 0x80D8
let GL_COLOR_TABLE_WIDTH_EXT = 0x80D9
let GL_COLOR_TABLE_RED_SIZE_EXT = 0x80DA
let GL_COLOR_TABLE_GREEN_SIZE_EXT = 0x80DB
let GL_COLOR_TABLE_BLUE_SIZE_EXT = 0x80DC
let GL_COLOR_TABLE_ALPHA_SIZE_EXT = 0x80DD
let GL_COLOR_TABLE_LUMINANCE_SIZE_EXT = 0x80DE
let GL_COLOR_TABLE_INTENSITY_SIZE_EXT = 0x80DF

let GL_COLOR_INDEX1_EXT = 0x80E2
let GL_COLOR_INDEX2_EXT = 0x80E3
let GL_COLOR_INDEX4_EXT = 0x80E4
let GL_COLOR_INDEX8_EXT = 0x80E5
let GL_COLOR_INDEX12_EXT = 0x80E6
let GL_COLOR_INDEX16_EXT = 0x80E7

let GL_MAX_ELEMENTS_VERTICES_WIN = 0x80E8
let GL_MAX_ELEMENTS_INDICES_WIN = 0x80E9

let GL_PHONG_WIN = 0x80EA
let GL_PHONG_HINT_WIN = 0x80EB

let GL_FOG_SPECULAR_TEXTURE_WIN = 0x80EC

let GL_LOGIC_OP = GL_INDEX_LOGIC_OP
let GL_TEXTURE_COMPONENTS = GL_TEXTURE_INTERNAL_FORMAT

[<DllImport(DLL)>]extern void glAccum(GLenum op,GLfloat value)
[<DllImport(DLL)>]extern void glAlphaFunc(GLenum func,GLclampf ref)
[<DllImport(DLL)>]extern GLboolean glAreTexturesResident(GLsizei n,GLuint[] textures,GLboolean[] residences)
[<DllImport(DLL)>]extern void glArrayElement(GLint i)
[<DllImport(DLL)>]extern void glBegin(GLenum mode)
[<DllImport(DLL)>]extern void glBindTexture(GLenum target,GLuint texture)
[<DllImport(DLL)>]extern void glBitmap(GLsizei width,GLsizei height,GLfloat xorig,GLfloat yorig,GLfloat xmove,GLfloat ymove,GLubyte[] bitmap)
[<DllImport(DLL)>]extern void glBlendFunc(GLenum sfactor,GLenum dfactor)
[<DllImport(DLL)>]extern void glCallList(GLuint list)
[<DllImport(DLL)>]extern void glCallLists(GLsizei n,GLenum ``type``,nativeint lists)
[<DllImport(DLL)>]extern void glClear(GLbitfield mask)
[<DllImport(DLL)>]extern void glClearAccum(GLfloat red,GLfloat green,GLfloat blue,GLfloat alpha)
[<DllImport(DLL)>]extern void glClearColor(GLclampf red,GLclampf green,GLclampf blue,GLclampf alpha)
[<DllImport(DLL)>]extern void glClearDepth(GLclampd depth)
[<DllImport(DLL)>]extern void glClearIndex(GLfloat c)
[<DllImport(DLL)>]extern void glClearStencil(GLint s)
[<DllImport(DLL)>]extern void glClipPlane(GLenum plane,GLdouble[] equation)
[<DllImport(DLL)>]extern void glColor3b(GLbyte red,GLbyte green,GLbyte blue)
[<DllImport(DLL)>]extern void glColor3bv(GLbyte[] v)
[<DllImport(DLL)>]extern void glColor3d(GLdouble red,GLdouble green,GLdouble blue)
[<DllImport(DLL)>]extern void glColor3dv(GLdouble[] v)
[<DllImport(DLL)>]extern void glColor3f(GLfloat red,GLfloat green,GLfloat blue)
[<DllImport(DLL)>]extern void glColor3fv(GLfloat[] v)
[<DllImport(DLL)>]extern void glColor3i(GLint red,GLint green,GLint blue)
[<DllImport(DLL)>]extern void glColor3iv(GLint[] v)
[<DllImport(DLL)>]extern void glColor3s(GLshort red,GLshort green,GLshort blue)
[<DllImport(DLL)>]extern void glColor3sv(GLshort[] v)
[<DllImport(DLL)>]extern void glColor3ub(GLubyte red,GLubyte green,GLubyte blue)
[<DllImport(DLL)>]extern void glColor3ubv(GLubyte[] v)
[<DllImport(DLL)>]extern void glColor3ui(GLuint red,GLuint green,GLuint blue)
[<DllImport(DLL)>]extern void glColor3uiv(GLuint[] v)
[<DllImport(DLL)>]extern void glColor3us(GLushort red,GLushort green,GLushort blue)
[<DllImport(DLL)>]extern void glColor3usv(GLushort[] v)
[<DllImport(DLL)>]extern void glColor4b(GLbyte red,GLbyte green,GLbyte blue,GLbyte alpha)
[<DllImport(DLL)>]extern void glColor4bv(GLbyte[] v)
[<DllImport(DLL)>]extern void glColor4d(GLdouble red,GLdouble green,GLdouble blue,GLdouble alpha)
[<DllImport(DLL)>]extern void glColor4dv(GLdouble[] v)
[<DllImport(DLL)>]extern void glColor4f(GLfloat red,GLfloat green,GLfloat blue,GLfloat alpha)
[<DllImport(DLL)>]extern void glColor4fv(GLfloat[] v)
[<DllImport(DLL)>]extern void glColor4i(GLint red,GLint green,GLint blue,GLint alpha)
[<DllImport(DLL)>]extern void glColor4iv(GLint[] v)
[<DllImport(DLL)>]extern void glColor4s(GLshort red,GLshort green,GLshort blue,GLshort alpha)
[<DllImport(DLL)>]extern void glColor4sv(GLshort[] v)
[<DllImport(DLL)>]extern void glColor4ub(GLubyte red,GLubyte green,GLubyte blue,GLubyte alpha)
[<DllImport(DLL)>]extern void glColor4ubv(GLubyte[] v)
[<DllImport(DLL)>]extern void glColor4ui(GLuint red,GLuint green,GLuint blue,GLuint alpha)
[<DllImport(DLL)>]extern void glColor4uiv(GLuint[] v)
[<DllImport(DLL)>]extern void glColor4us(GLushort red,GLushort green,GLushort blue,GLushort alpha)
[<DllImport(DLL)>]extern void glColor4usv(GLushort[] v)
[<DllImport(DLL)>]extern void glColorMask(GLboolean red,GLboolean green,GLboolean blue,GLboolean alpha)
[<DllImport(DLL)>]extern void glColorMaterial(GLenum face,GLenum mode)
[<DllImport(DLL)>]extern void glColorPointer(GLint size,GLenum ``type``,GLsizei stride,nativeint pointer)
[<DllImport(DLL)>]extern void glCopyPixels(GLint x,GLint y,GLsizei width,GLsizei height,GLenum ``type``)
[<DllImport(DLL)>]extern void glCopyTexImage1D(GLenum target,GLint level,GLenum internalFormat,GLint x,GLint y,GLsizei width,GLint border)
[<DllImport(DLL)>]extern void glCopyTexImage2D(GLenum target,GLint level,GLenum internalFormat,GLint x,GLint y,GLsizei width,GLsizei height,GLint border)
[<DllImport(DLL)>]extern void glCopyTexSubImage1D(GLenum target,GLint level,GLint xoffset,GLint x,GLint y,GLsizei width)
[<DllImport(DLL)>]extern void glCopyTexSubImage2D(GLenum target,GLint level,GLint xoffset,GLint yoffset,GLint x,GLint y,GLsizei width,GLsizei height)
[<DllImport(DLL)>]extern void glCullFace(GLenum mode)
[<DllImport(DLL)>]extern void glDeleteLists(GLuint list,GLsizei range)
[<DllImport(DLL)>]extern void glDeleteTextures(GLsizei n,GLuint[] textures)
[<DllImport(DLL)>]extern void glDepthFunc(GLenum func)
[<DllImport(DLL)>]extern void glDepthMask(GLboolean flag)
[<DllImport(DLL)>]extern void glDepthRange (GLclampd zNear,GLclampd zFar)
[<DllImport(DLL)>]extern void glDisable(GLenum cap)
[<DllImport(DLL)>]extern void glDisableClientState(GLenum array)
[<DllImport(DLL)>]extern void glDrawArrays(GLenum mode,GLint first,GLsizei count)
[<DllImport(DLL)>]extern void glDrawBuffer(GLenum mode)
[<DllImport(DLL)>]extern void glDrawElements(GLenum mode,GLsizei count,GLenum ``type``,nativeint indices)
[<DllImport(DLL)>]extern void glDrawPixels(GLsizei width,GLsizei height,GLenum format,GLenum ``type``,nativeint pixels)
[<DllImport(DLL)>]extern void glEdgeFlag(GLboolean flag)
[<DllImport(DLL)>]extern void glEdgeFlagPointer(GLsizei stride,nativeint pointer)
[<DllImport(DLL)>]extern void glEdgeFlagv(GLboolean[] flag)
[<DllImport(DLL)>]extern void glEnable(GLenum cap)
[<DllImport(DLL)>]extern void glEnableClientState(GLenum array)
[<DllImport(DLL)>]extern void glEnd()
[<DllImport(DLL)>]extern void glEndList()
[<DllImport(DLL)>]extern void glEvalCoord1d(GLdouble u)
[<DllImport(DLL)>]extern void glEvalCoord1dv(GLdouble[] u)
[<DllImport(DLL)>]extern void glEvalCoord1f(GLfloat u)
[<DllImport(DLL)>]extern void glEvalCoord1fv(GLfloat[] u)
[<DllImport(DLL)>]extern void glEvalCoord2d(GLdouble u,GLdouble v)
[<DllImport(DLL)>]extern void glEvalCoord2dv(GLdouble[] u)
[<DllImport(DLL)>]extern void glEvalCoord2f(GLfloat u,GLfloat v)
[<DllImport(DLL)>]extern void glEvalCoord2fv(GLfloat[] u)
[<DllImport(DLL)>]extern void glEvalMesh1(GLenum mode,GLint i1,GLint i2)
[<DllImport(DLL)>]extern void glEvalMesh2(GLenum mode,GLint i1,GLint i2,GLint j1,GLint j2)
[<DllImport(DLL)>]extern void glEvalPoint1(GLint i)
[<DllImport(DLL)>]extern void glEvalPoint2(GLint i,GLint j)
[<DllImport(DLL)>]extern void glFeedbackBuffer(GLsizei size,GLenum ``type``,GLfloat[] buffer)
[<DllImport(DLL)>]extern void glFinish()
[<DllImport(DLL)>]extern void glFlush()
[<DllImport(DLL)>]extern void glFogf(GLenum pname,GLfloat param)
[<DllImport(DLL)>]extern void glFogfv(GLenum pname,GLfloat[] ``params``)
[<DllImport(DLL)>]extern void glFogi(GLenum pname,GLint param)
[<DllImport(DLL)>]extern void glFogiv(GLenum pname,GLint[] ``params``)
[<DllImport(DLL)>]extern void glFrontFace(GLenum mode)
[<DllImport(DLL)>]extern void glFrustum(GLdouble left,GLdouble right,GLdouble bottom,GLdouble top,GLdouble zNear,GLdouble zFar)
[<DllImport(DLL)>]extern GLuint glGenLists(GLsizei range)
[<DllImport(DLL)>]extern void glGenTextures(GLsizei n,GLuint[] textures)
[<DllImport(DLL)>]extern void glGetBooleanv(GLenum pname,GLboolean[] ``params``)
[<DllImport(DLL)>]extern void glGetClipPlane(GLenum plane,GLdouble[] equation)
[<DllImport(DLL)>]extern void glGetDoublev(GLenum pname,GLdouble[] ``params``)
[<DllImport(DLL)>]extern GLenum glGetError()
[<DllImport(DLL)>]extern void glGetFloatv(GLenum pname,GLfloat[] ``params``)
[<DllImport(DLL)>]extern void glGetIntegerv(GLenum pname,GLint[] ``params``)
[<DllImport(DLL)>]extern void glGetLightfv(GLenum light,GLenum pname,GLfloat[] ``params``)
[<DllImport(DLL)>]extern void glGetLightiv(GLenum light,GLenum pname,GLint[] ``params``)
[<DllImport(DLL)>]extern void glGetMapdv(GLenum target,GLenum query,GLdouble[] v)
[<DllImport(DLL)>]extern void glGetMapfv(GLenum target,GLenum query,GLfloat[] v)
[<DllImport(DLL)>]extern void glGetMapiv(GLenum target,GLenum query,GLint[] v)
[<DllImport(DLL)>]extern void glGetMaterialfv(GLenum face,GLenum pname,GLfloat[] ``params``)
[<DllImport(DLL)>]extern void glGetMaterialiv(GLenum face,GLenum pname,GLint[] ``params``)
[<DllImport(DLL)>]extern void glGetPixelMapfv(GLenum map,GLfloat[] values)
[<DllImport(DLL)>]extern void glGetPixelMapuiv(GLenum map,GLuint[] values)
[<DllImport(DLL)>]extern void glGetPixelMapusv(GLenum map,GLushort[] values)
[<DllImport(DLL)>]extern void glGetPointerv(GLenum pname,nativeint[] ``params``)
[<DllImport(DLL)>]extern void glGetPolygonStipple(GLubyte[] mask)
[<DllImport(DLL)>]extern GLubyte[] glGetString(GLenum name)
[<DllImport(DLL)>]extern void glGetTexEnvfv(GLenum target,GLenum pname,GLfloat[] ``params``)
[<DllImport(DLL)>]extern void glGetTexEnviv(GLenum target,GLenum pname,GLint[] ``params``)
[<DllImport(DLL)>]extern void glGetTexGendv(GLenum coord,GLenum pname,GLdouble[] ``params``)
[<DllImport(DLL)>]extern void glGetTexGenfv(GLenum coord,GLenum pname,GLfloat[] ``params``)
[<DllImport(DLL)>]extern void glGetTexGeniv(GLenum coord,GLenum pname,GLint[] ``params``)
[<DllImport(DLL)>]extern void glGetTexImage(GLenum target,GLint level,GLenum format,GLenum ``type``,nativeint pixels)
[<DllImport(DLL)>]extern void glGetTexLevelParameterfv(GLenum target,GLint level,GLenum pname,GLfloat[] ``params``)
[<DllImport(DLL)>]extern void glGetTexLevelParameteriv(GLenum target,GLint level,GLenum pname,GLint[] ``params``)
[<DllImport(DLL)>]extern void glGetTexParameterfv(GLenum target,GLenum pname,GLfloat[] ``params``)
[<DllImport(DLL)>]extern void glGetTexParameteriv(GLenum target,GLenum pname,GLint[] ``params``)
[<DllImport(DLL)>]extern void glHint(GLenum target,GLenum mode)
[<DllImport(DLL)>]extern void glIndexMask(GLuint mask)
[<DllImport(DLL)>]extern void glIndexPointer(GLenum ``type``,GLsizei stride,nativeint pointer)
[<DllImport(DLL)>]extern void glIndexd(GLdouble c)
[<DllImport(DLL)>]extern void glIndexdv(GLdouble[] c)
[<DllImport(DLL)>]extern void glIndexf(GLfloat c)
[<DllImport(DLL)>]extern void glIndexfv(GLfloat[] c)
[<DllImport(DLL)>]extern void glIndexi(GLint c)
[<DllImport(DLL)>]extern void glIndexiv(GLint[] c)
[<DllImport(DLL)>]extern void glIndexs(GLshort c)
[<DllImport(DLL)>]extern void glIndexsv(GLshort[] c)
[<DllImport(DLL)>]extern void glIndexub(GLubyte c)
[<DllImport(DLL)>]extern void glIndexubv(GLubyte[] c)
[<DllImport(DLL)>]extern void glInitNames()
[<DllImport(DLL)>]extern void glInterleavedArrays(GLenum format,GLsizei stride,nativeint pointer)
[<DllImport(DLL)>]extern GLboolean glIsEnabled(GLenum cap)
[<DllImport(DLL)>]extern GLboolean glIsList(GLuint list)
[<DllImport(DLL)>]extern GLboolean glIsTexture(GLuint texture)
[<DllImport(DLL)>]extern void glLightModelf(GLenum pname,GLfloat param)
[<DllImport(DLL)>]extern void glLightModelfv(GLenum pname,GLfloat[] ``params``)
[<DllImport(DLL)>]extern void glLightModeli(GLenum pname,GLint param)
[<DllImport(DLL)>]extern void glLightModeliv(GLenum pname,GLint[] ``params``)
[<DllImport(DLL)>]extern void glLightf(GLenum light,GLenum pname,GLfloat param)
[<DllImport(DLL)>]extern void glLightfv(GLenum light,GLenum pname,GLfloat[] ``params``)
[<DllImport(DLL)>]extern void glLighti(GLenum light,GLenum pname,GLint param)
[<DllImport(DLL)>]extern void glLightiv(GLenum light,GLenum pname,GLint[] ``params``)
[<DllImport(DLL)>]extern void glLineStipple(GLint factor,GLushort pattern)
[<DllImport(DLL)>]extern void glLineWidth(GLfloat width)
[<DllImport(DLL)>]extern void glListBase(GLuint ``base``)
[<DllImport(DLL)>]extern void glLoadIdentity()
[<DllImport(DLL)>]extern void glLoadMatrixd(GLdouble[] m)
[<DllImport(DLL)>]extern void glLoadMatrixf(GLfloat[] m)
[<DllImport(DLL)>]extern void glLoadName(GLuint name)
[<DllImport(DLL)>]extern void glLogicOp(GLenum opcode)
[<DllImport(DLL)>]extern void glMap1d(GLenum target,GLdouble u1,GLdouble u2,GLint stride,GLint order,GLdouble[] points)
[<DllImport(DLL)>]extern void glMap1f(GLenum target,GLfloat u1,GLfloat u2,GLint stride,GLint order,GLfloat[] points)
[<DllImport(DLL)>]extern void glMap2d(GLenum target,GLdouble u1,GLdouble u2,GLint ustride,GLint uorder,GLdouble v1,GLdouble v2,GLint vstride,GLint vorder,GLdouble[] points)
[<DllImport(DLL)>]extern void glMap2f(GLenum target,GLfloat u1,GLfloat u2,GLint ustride,GLint uorder,GLfloat v1,GLfloat v2,GLint vstride,GLint vorder,GLfloat[] points)
[<DllImport(DLL)>]extern void glMapGrid1d(GLint un,GLdouble u1,GLdouble u2)
[<DllImport(DLL)>]extern void glMapGrid1f(GLint un,GLfloat u1,GLfloat u2)
[<DllImport(DLL)>]extern void glMapGrid2d(GLint un,GLdouble u1,GLdouble u2,GLint vn,GLdouble v1,GLdouble v2)
[<DllImport(DLL)>]extern void glMapGrid2f(GLint un,GLfloat u1,GLfloat u2,GLint vn,GLfloat v1,GLfloat v2)
[<DllImport(DLL)>]extern void glMaterialf(GLenum face,GLenum pname,GLfloat param)
[<DllImport(DLL)>]extern void glMaterialfv(GLenum face,GLenum pname,GLfloat[] ``params``)
[<DllImport(DLL)>]extern void glMateriali(GLenum face,GLenum pname,GLint param)
[<DllImport(DLL)>]extern void glMaterialiv(GLenum face,GLenum pname,GLint[] ``params``)
[<DllImport(DLL)>]extern void glMatrixMode(GLenum mode)
[<DllImport(DLL)>]extern void glMultMatrixd(GLdouble[] m)
[<DllImport(DLL)>]extern void glMultMatrixf(GLfloat[] m)
[<DllImport(DLL)>]extern void glNewList(GLuint list,GLenum mode)
[<DllImport(DLL)>]extern void glNormal3b (GLbyte nx,GLbyte ny,GLbyte nz)
[<DllImport(DLL)>]extern void glNormal3bv(GLbyte[] v)
[<DllImport(DLL)>]extern void glNormal3d(GLdouble nx,GLdouble ny,GLdouble nz)
[<DllImport(DLL)>]extern void glNormal3dv(GLdouble[] v)
[<DllImport(DLL)>]extern void glNormal3f(GLfloat nx,GLfloat ny,GLfloat nz)
[<DllImport(DLL)>]extern void glNormal3fv(GLfloat[] v)
[<DllImport(DLL)>]extern void glNormal3i(GLint nx,GLint ny,GLint nz)
[<DllImport(DLL)>]extern void glNormal3iv(GLint[] v)
[<DllImport(DLL)>]extern void glNormal3s(GLshort nx,GLshort ny,GLshort nz)
[<DllImport(DLL)>]extern void glNormal3sv(GLshort[] v)
[<DllImport(DLL)>]extern void glNormalPointer(GLenum ``type``,GLsizei stride,nativeint pointer)
[<DllImport(DLL)>]extern void glOrtho(GLdouble left,GLdouble right,GLdouble bottom,GLdouble top,GLdouble zNear,GLdouble zFar)
[<DllImport(DLL)>]extern void glPassThrough(GLfloat token)
[<DllImport(DLL)>]extern void glPixelMapfv(GLenum map,GLsizei mapsize,GLfloat[] values)
[<DllImport(DLL)>]extern void glPixelMapuiv(GLenum map,GLsizei mapsize,GLuint[] values)
[<DllImport(DLL)>]extern void glPixelMapusv(GLenum map,GLsizei mapsize,GLushort[] values)
[<DllImport(DLL)>]extern void glPixelStoref(GLenum pname,GLfloat param)
[<DllImport(DLL)>]extern void glPixelStorei(GLenum pname,GLint param)
[<DllImport(DLL)>]extern void glPixelTransferf(GLenum pname,GLfloat param)
[<DllImport(DLL)>]extern void glPixelTransferi(GLenum pname,GLint param)
[<DllImport(DLL)>]extern void glPixelZoom(GLfloat xfactor,GLfloat yfactor)
[<DllImport(DLL)>]extern void glPointSize(GLfloat size)
[<DllImport(DLL)>]extern void glPolygonMode(GLenum face,GLenum mode)
[<DllImport(DLL)>]extern void glPolygonOffset(GLfloat factor,GLfloat units)
[<DllImport(DLL)>]extern void glPolygonStipple(GLubyte[] mask)
[<DllImport(DLL)>]extern void glPopAttrib()
[<DllImport(DLL)>]extern void glPopClientAttrib()
[<DllImport(DLL)>]extern void glPopMatrix()
[<DllImport(DLL)>]extern void glPopName()
[<DllImport(DLL)>]extern void glPrioritizeTextures(GLsizei n,GLuint[] textures,GLclampf[] priorities)
[<DllImport(DLL)>]extern void glPushAttrib(GLbitfield mask)
[<DllImport(DLL)>]extern void glPushClientAttrib(GLbitfield mask)
[<DllImport(DLL)>]extern void glPushMatrix()
[<DllImport(DLL)>]extern void glPushName(GLuint name)
[<DllImport(DLL)>]extern void glRasterPos2d(GLdouble x,GLdouble y)
[<DllImport(DLL)>]extern void glRasterPos2dv(GLdouble[] v)
[<DllImport(DLL)>]extern void glRasterPos2f(GLfloat x,GLfloat y)
[<DllImport(DLL)>]extern void glRasterPos2fv(GLfloat[] v)
[<DllImport(DLL)>]extern void glRasterPos2i(GLint x,GLint y)
[<DllImport(DLL)>]extern void glRasterPos2iv(GLint[] v)
[<DllImport(DLL)>]extern void glRasterPos2s(GLshort x,GLshort y)
[<DllImport(DLL)>]extern void glRasterPos2sv(GLshort[] v)
[<DllImport(DLL)>]extern void glRasterPos3d(GLdouble x,GLdouble y,GLdouble z)
[<DllImport(DLL)>]extern void glRasterPos3dv(GLdouble[] v)
[<DllImport(DLL)>]extern void glRasterPos3f(GLfloat x,GLfloat y,GLfloat z)
[<DllImport(DLL)>]extern void glRasterPos3fv(GLfloat[] v)
[<DllImport(DLL)>]extern void glRasterPos3i(GLint x,GLint y,GLint z)
[<DllImport(DLL)>]extern void glRasterPos3iv(GLint[] v)
[<DllImport(DLL)>]extern void glRasterPos3s(GLshort x,GLshort y,GLshort z)
[<DllImport(DLL)>]extern void glRasterPos3sv(GLshort[] v)
[<DllImport(DLL)>]extern void glRasterPos4d(GLdouble x,GLdouble y,GLdouble z,GLdouble w)
[<DllImport(DLL)>]extern void glRasterPos4dv(GLdouble[] v)
[<DllImport(DLL)>]extern void glRasterPos4f(GLfloat x,GLfloat y,GLfloat z,GLfloat w)
[<DllImport(DLL)>]extern void glRasterPos4fv(GLfloat[] v)
[<DllImport(DLL)>]extern void glRasterPos4i(GLint x,GLint y,GLint z,GLint w)
[<DllImport(DLL)>]extern void glRasterPos4iv(GLint[] v)
[<DllImport(DLL)>]extern void glRasterPos4s(GLshort x,GLshort y,GLshort z,GLshort w)
[<DllImport(DLL)>]extern void glRasterPos4sv(GLshort[] v)
[<DllImport(DLL)>]extern void glReadBuffer(GLenum mode)
[<DllImport(DLL)>]extern void glReadPixels(GLint x,GLint y,GLsizei width,GLsizei height,GLenum format,GLenum ``type``,nativeint pixels)
[<DllImport(DLL)>]extern void glRectd(GLdouble x1,GLdouble y1,GLdouble x2,GLdouble y2)
[<DllImport(DLL)>]extern void glRectdv(GLdouble[] v1,GLdouble[] v2)
[<DllImport(DLL)>]extern void glRectf(GLfloat x1,GLfloat y1,GLfloat x2,GLfloat y2)
[<DllImport(DLL)>]extern void glRectfv(GLfloat[] v1,GLfloat[] v2)
[<DllImport(DLL)>]extern void glRecti(GLint x1,GLint y1,GLint x2,GLint y2)
[<DllImport(DLL)>]extern void glRectiv(GLint[] v1,GLint[] v2)
[<DllImport(DLL)>]extern void glRects(GLshort x1,GLshort y1,GLshort x2,GLshort y2)
[<DllImport(DLL)>]extern void glRectsv(GLshort[] v1,GLshort[] v2)
[<DllImport(DLL)>]extern GLint glRenderMode(GLenum mode)
[<DllImport(DLL)>]extern void glRotated(GLdouble angle,GLdouble x,GLdouble y,GLdouble z)
[<DllImport(DLL)>]extern void glRotatef(GLfloat angle,GLfloat x,GLfloat y,GLfloat z)
[<DllImport(DLL)>]extern void glScaled(GLdouble x,GLdouble y,GLdouble z)
[<DllImport(DLL)>]extern void glScalef(GLfloat x,GLfloat y,GLfloat z)
[<DllImport(DLL)>]extern void glScissor(GLint x,GLint y,GLsizei width,GLsizei height)
[<DllImport(DLL)>]extern void glSelectBuffer(GLsizei size,GLuint[] buffer)
[<DllImport(DLL)>]extern void glShadeModel(GLenum mode)
[<DllImport(DLL)>]extern void glStencilFunc(GLenum func,GLint ref,GLuint mask)
[<DllImport(DLL)>]extern void glStencilMask(GLuint mask)
[<DllImport(DLL)>]extern void glStencilOp(GLenum fail,GLenum zfail,GLenum zpass)
[<DllImport(DLL)>]extern void glTexCoord1d(GLdouble s)
[<DllImport(DLL)>]extern void glTexCoord1dv(GLdouble[] v)
[<DllImport(DLL)>]extern void glTexCoord1f(GLfloat s)
[<DllImport(DLL)>]extern void glTexCoord1fv(GLfloat[] v)
[<DllImport(DLL)>]extern void glTexCoord1i(GLint s)
[<DllImport(DLL)>]extern void glTexCoord1iv(GLint[] v)
[<DllImport(DLL)>]extern void glTexCoord1s(GLshort s)
[<DllImport(DLL)>]extern void glTexCoord1sv(GLshort[] v)
[<DllImport(DLL)>]extern void glTexCoord2d(GLdouble s,GLdouble t)
[<DllImport(DLL)>]extern void glTexCoord2dv(GLdouble[] v)
[<DllImport(DLL)>]extern void glTexCoord2f(GLfloat s,GLfloat t)
[<DllImport(DLL)>]extern void glTexCoord2fv(GLfloat[] v)
[<DllImport(DLL)>]extern void glTexCoord2i(GLint s,GLint t)
[<DllImport(DLL)>]extern void glTexCoord2iv(GLint[] v)
[<DllImport(DLL)>]extern void glTexCoord2s(GLshort s,GLshort t)
[<DllImport(DLL)>]extern void glTexCoord2sv(GLshort[] v)
[<DllImport(DLL)>]extern void glTexCoord3d(GLdouble s,GLdouble t,GLdouble r)
[<DllImport(DLL)>]extern void glTexCoord3dv(GLdouble[] v)
[<DllImport(DLL)>]extern void glTexCoord3f(GLfloat s,GLfloat t,GLfloat r)
[<DllImport(DLL)>]extern void glTexCoord3fv(GLfloat[] v)
[<DllImport(DLL)>]extern void glTexCoord3i(GLint s,GLint t,GLint r)
[<DllImport(DLL)>]extern void glTexCoord3iv(GLint[] v)
[<DllImport(DLL)>]extern void glTexCoord3s(GLshort s,GLshort t,GLshort r)
[<DllImport(DLL)>]extern void glTexCoord3sv(GLshort[] v)
[<DllImport(DLL)>]extern void glTexCoord4d(GLdouble s,GLdouble t,GLdouble r,GLdouble q)
[<DllImport(DLL)>]extern void glTexCoord4dv(GLdouble[] v)
[<DllImport(DLL)>]extern void glTexCoord4f(GLfloat s,GLfloat t,GLfloat r,GLfloat q)
[<DllImport(DLL)>]extern void glTexCoord4fv(GLfloat[] v)
[<DllImport(DLL)>]extern void glTexCoord4i(GLint s,GLint t,GLint r,GLint q)
[<DllImport(DLL)>]extern void glTexCoord4iv(GLint[] v)
[<DllImport(DLL)>]extern void glTexCoord4s(GLshort s,GLshort t,GLshort r,GLshort q)
[<DllImport(DLL)>]extern void glTexCoord4sv(GLshort[] v)
[<DllImport(DLL)>]extern void glTexCoordPointer(GLint size,GLenum ``type``,GLsizei stride,nativeint pointer)
[<DllImport(DLL)>]extern void glTexEnvf(GLenum target,GLenum pname,GLfloat param)
[<DllImport(DLL)>]extern void glTexEnvfv(GLenum target,GLenum pname,GLfloat[] ``params``)
[<DllImport(DLL)>]extern void glTexEnvi(GLenum target,GLenum pname,GLint param)
[<DllImport(DLL)>]extern void glTexEnviv(GLenum target,GLenum pname,GLint[] ``params``)
[<DllImport(DLL)>]extern void glTexGend(GLenum coord,GLenum pname,GLdouble param)
[<DllImport(DLL)>]extern void glTexGendv(GLenum coord,GLenum pname,GLdouble[] ``params``)
[<DllImport(DLL)>]extern void glTexGenf(GLenum coord,GLenum pname,GLfloat param)
[<DllImport(DLL)>]extern void glTexGenfv(GLenum coord,GLenum pname,GLfloat[] ``params``)
[<DllImport(DLL)>]extern void glTexGeni(GLenum coord,GLenum pname,GLint param)
[<DllImport(DLL)>]extern void glTexGeniv(GLenum coord,GLenum pname,GLint[] ``params``)
[<DllImport(DLL)>]extern void glTexImage1D(GLenum target,GLint level,GLint internalformat,GLsizei width,GLint border,GLenum format,GLenum ``type``,nativeint pixels)
[<DllImport(DLL)>]extern void glTexImage2D(GLenum target,GLint level,GLint internalformat,GLsizei width,GLsizei height,GLint border,GLenum format,GLenum ``type``,nativeint pixels)
[<DllImport(DLL)>]extern void glTexParameterf(GLenum target,GLenum pname,GLfloat param)
[<DllImport(DLL)>]extern void glTexParameterfv(GLenum target,GLenum pname,GLfloat[] ``params``)
[<DllImport(DLL)>]extern void glTexParameteri(GLenum target,GLenum pname,GLint param)
[<DllImport(DLL)>]extern void glTexParameteriv(GLenum target,GLenum pname,GLint[] ``params``)
[<DllImport(DLL)>]extern void glTexSubImage1D(GLenum target,GLint level,GLint xoffset,GLsizei width,GLenum format,GLenum ``type``,nativeint pixels)
[<DllImport(DLL)>]extern void glTexSubImage2D(GLenum target,GLint level,GLint xoffset,GLint yoffset,GLsizei width,GLsizei height,GLenum format,GLenum ``type``,nativeint pixels)
[<DllImport(DLL)>]extern void glTranslated(GLdouble x,GLdouble y,GLdouble z)
[<DllImport(DLL)>]extern void glTranslatef(GLfloat x,GLfloat y,GLfloat z)
[<DllImport(DLL)>]extern void glVertex2d(GLdouble x,GLdouble y)
[<DllImport(DLL)>]extern void glVertex2dv(GLdouble[] v)
[<DllImport(DLL)>]extern void glVertex2f(GLfloat x,GLfloat y)
[<DllImport(DLL)>]extern void glVertex2fv(GLfloat[] v)
[<DllImport(DLL)>]extern void glVertex2i(GLint x,GLint y)
[<DllImport(DLL)>]extern void glVertex2iv(GLint[] v)
[<DllImport(DLL)>]extern void glVertex2s(GLshort x,GLshort y)
[<DllImport(DLL)>]extern void glVertex2sv(GLshort[] v)
[<DllImport(DLL)>]extern void glVertex3d(GLdouble x,GLdouble y,GLdouble z)
[<DllImport(DLL)>]extern void glVertex3dv(GLdouble[] v)
[<DllImport(DLL)>]extern void glVertex3f(GLfloat x,GLfloat y,GLfloat z)
[<DllImport(DLL)>]extern void glVertex3fv(GLfloat[] v)
[<DllImport(DLL)>]extern void glVertex3i(GLint x,GLint y,GLint z)
[<DllImport(DLL)>]extern void glVertex3iv(GLint[] v)
[<DllImport(DLL)>]extern void glVertex3s(GLshort x,GLshort y,GLshort z)
[<DllImport(DLL)>]extern void glVertex3sv(GLshort[] v)
[<DllImport(DLL)>]extern void glVertex4d(GLdouble x,GLdouble y,GLdouble z,GLdouble w)
[<DllImport(DLL)>]extern void glVertex4dv(GLdouble[] v)
[<DllImport(DLL)>]extern void glVertex4f(GLfloat x,GLfloat y,GLfloat z,GLfloat w)
[<DllImport(DLL)>]extern void glVertex4fv(GLfloat[] v)
[<DllImport(DLL)>]extern void glVertex4i(GLint x,GLint y,GLint z,GLint w)
[<DllImport(DLL)>]extern void glVertex4iv(GLint[] v)
[<DllImport(DLL)>]extern void glVertex4s(GLshort x,GLshort y,GLshort z,GLshort w)
[<DllImport(DLL)>]extern void glVertex4sv(GLshort[] v)
[<DllImport(DLL)>]extern void glVertexPointer(GLint size,GLenum ``type``,GLsizei stride,nativeint pointer)
[<DllImport(DLL)>]extern void glViewport(GLint x,GLint y,GLsizei width,GLsizei height)

type PFNGLARRAYELEMENTEXTPROC = delegate of GLint -> unit
type PFNGLDRAWARRAYSEXTPROC = delegate of GLenum * GLint * GLsizei -> unit
type PFNGLVERTEXPOINTEREXTPROC = delegate of GLint * GLenum * GLsizei * GLsizei * nativeint -> unit
type PFNGLNORMALPOINTEREXTPROC = delegate of GLenum * GLsizei * GLsizei * nativeint -> unit
type PFNGLCOLORPOINTEREXTPROC = delegate of GLint * GLenum * GLsizei * GLsizei * nativeint -> unit
type PFNGLINDEXPOINTEREXTPROC = delegate of GLenum * GLsizei * GLsizei * nativeint -> unit
type PFNGLTEXCOORDPOINTEREXTPROC = delegate of GLint * GLenum * GLsizei * GLsizei * nativeint -> unit
type PFNGLEDGEFLAGPOINTEREXTPROC = delegate of GLsizei * GLsizei * GLboolean[] -> unit
type PFNGLGETPOINTERVEXTPROC = delegate of GLenum * nativeint -> unit
type PFNGLARRAYELEMENTARRAYEXTPROC = delegate of GLenum * GLsizei * nativeint -> unit
type PFNGLDRAWRANGEELEMENTSWINPROC = delegate of GLenum * GLuint * GLuint * GLsizei * GLenum * nativeint -> unit
type PFNGLADDSWAPHINTRECTWINPROC = delegate of GLint * GLint * GLsizei * GLsizei -> unit
type PFNGLCOLORTABLEEXTPROC = delegate of GLenum * GLenum * GLsizei * GLenum * GLenum * nativeint -> unit
type PFNGLCOLORSUBTABLEEXTPROC = delegate of GLenum * GLsizei * GLsizei * GLenum * GLenum * nativeint -> unit
type PFNGLGETCOLORTABLEEXTPROC = delegate of GLenum * GLenum * GLenum * nativeint -> unit
type PFNGLGETCOLORTABLEPARAMETERIVEXTPROC = delegate of GLenum * GLenum * GLint[] -> unit
type PFNGLGETCOLORTABLEPARAMETERFVEXTPROC = delegate of GLenum * GLenum * GLfloat[] -> unit


[<Literal>]
let DLLU = "glu32.dll"

[<DllImport(DLLU)>]extern void gluPerspective(GLdouble fovy,GLdouble aspect,GLdouble zNear,GLdouble zFar)
[<DllImport(DLLU)>]extern void gluLookAt(GLdouble eyex,GLdouble eyey,GLdouble eyez,GLdouble centerx,GLdouble centery,GLdouble centerz,GLdouble upx,GLdouble upy,GLdouble upz)

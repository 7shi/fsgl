(**
 * This file has no copyright assigned and is placed in the Public Domain.
 * This file is ported from part of the mingw-w64 runtime package.
 * No warranty is given; refer to the file DISCLAIMER.PD within this package.
 *)

module GL7.GL

open System
open System.Runtime.InteropServices

type GLenum     = int  // uint32
type GLboolean  = byte
type GLbitfield = uint32
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
[<Literal>]
let DLLU = "glu32.dll"

let GL_POINTS                   = 0x0000
let GL_LINES                    = 0x0001
let GL_LINE_LOOP                = 0x0002
let GL_LINE_STRIP               = 0x0003
let GL_TRIANGLES                = 0x0004
let GL_TRIANGLE_STRIP           = 0x0005
let GL_TRIANGLE_FAN             = 0x0006
let GL_QUADS                    = 0x0007
let GL_QUAD_STRIP               = 0x0008
let GL_POLYGON                  = 0x0009
let GL_FRONT_LEFT               = 0x0400
let GL_FRONT_RIGHT              = 0x0401
let GL_BACK_LEFT                = 0x0402
let GL_BACK_RIGHT               = 0x0403
let GL_FRONT                    = 0x0404
let GL_BACK                     = 0x0405
let GL_LEFT                     = 0x0406
let GL_RIGHT                    = 0x0407
let GL_FRONT_AND_BACK           = 0x0408
let GL_AUX0                     = 0x0409
let GL_AUX1                     = 0x040A
let GL_AUX2                     = 0x040B
let GL_AUX3                     = 0x040C
let GL_CULL_FACE                = 0x0B44
let GL_LIGHTING                 = 0x0B50
let GL_LIGHT_MODEL_LOCAL_VIEWER = 0x0B51
let GL_LIGHT_MODEL_TWO_SIDE     = 0x0B52
let GL_LIGHT_MODEL_AMBIENT      = 0x0B53
let GL_DEPTH_TEST               = 0x0B71
let GL_NORMALIZE                = 0x0BA1
let GL_AUTO_NORMAL              = 0x0D80
let GL_MAP2_VERTEX_3            = 0x0DB7
let GL_MAP2_VERTEX_4            = 0x0DB8
let GL_AMBIENT                  = 0x1200
let GL_DIFFUSE                  = 0x1201
let GL_SPECULAR                 = 0x1202
let GL_POSITION                 = 0x1203
let GL_COMPILE                  = 0x1300
let GL_UNSIGNED_SHORT           = 0x1403
let GL_FLOAT                    = 0x1406
let GL_EMISSION                 = 0x1600
let GL_SHININESS                = 0x1601
let GL_AMBIENT_AND_DIFFUSE      = 0x1602
let GL_MODELVIEW                = 0x1700
let GL_PROJECTION               = 0x1701
let GL_POINT                    = 0x1B00
let GL_LINE                     = 0x1B01
let GL_FILL                     = 0x1B02
let GL_FLAT                     = 0x1D00
let GL_SMOOTH                   = 0x1D01
let GL_VENDOR                   = 0x1F00
let GL_RENDERER                 = 0x1F01
let GL_VERSION                  = 0x1F02
let GL_EXTENSIONS               = 0x1F03
let GL_LIGHT0                   = 0x4000
let GL_POLYGON_OFFSET_FILL      = 0x8037
let GL_VERTEX_ARRAY             = 0x8074
let GL_NORMAL_ARRAY             = 0x8075
let GL_COLOR_ARRAY              = 0x8076
let GL_INDEX_ARRAY              = 0x8077
let GL_TEXTURE_COORD_ARRAY      = 0x8078

let GL_DEPTH_BUFFER_BIT = 0x00000100u
let GL_COLOR_BUFFER_BIT = 0x00004000u

[<DllImport(DLL)>]extern void glBegin(GLenum mode)
[<DllImport(DLL)>]extern void glCallList(GLuint list)
[<DllImport(DLL)>]extern void glClear(GLbitfield mask)
[<DllImport(DLL)>]extern void glClearColor(GLfloat red,GLfloat green,GLfloat blue,GLfloat alpha)
[<DllImport(DLL)>]extern void glColor3f(GLfloat red,GLfloat green,GLfloat blue)
[<DllImport(DLL)>]extern void glColor3fv(GLfloat[] v)
[<DllImport(DLL)>]extern void glDeleteLists(GLuint list,GLsizei range)
[<DllImport(DLL)>]extern void glDisable(GLenum cap)
[<DllImport(DLL)>]extern void glDisableClientState(GLenum array)
[<DllImport(DLL)>]extern void glDrawArrays(GLenum mode,GLint first,GLsizei count)
[<DllImport(DLL)>]extern void glDrawElements(GLenum mode,GLsizei count,GLenum ``type``,GLushort[] indices)
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
[<DllImport(DLL)>]extern void glFlush()
[<DllImport(DLL)>]extern void glFrustum(GLdouble left,GLdouble right,GLdouble bottom,GLdouble top,GLdouble near_val,GLdouble far_val)
[<DllImport(DLL)>]extern GLuint glGenLists(GLsizei range)
[<DllImport(DLL,EntryPoint = "glGetString")>]extern nativeint _glGetString(GLenum name)
let glGetString name = Marshal.PtrToStringAnsi(_glGetString(name))
[<DllImport(DLL)>]extern void glMapGrid2f(GLint un,GLfloat u1,GLfloat u2,GLint vn,GLfloat v1,GLfloat v2)
[<DllImport(DLL)>]extern void glLightfv(GLenum light,GLenum pname,GLfloat[] ``params``)
[<DllImport(DLL)>]extern void glLightModelf(GLenum pname,GLfloat param)
[<DllImport(DLL)>]extern void glLightModelfv(GLenum pname,GLfloat[] ``params``)
[<DllImport(DLL)>]extern void glLightModeli(GLenum pname,GLint param)
[<DllImport(DLL)>]extern void glLightModeliv(GLenum pname,GLint[]``*params``)
[<DllImport(DLL)>]extern void glLoadIdentity()
[<DllImport(DLL)>]extern void glMap2f(GLenum target,GLfloat u1,GLfloat u2,GLint ustride,GLint uorder,GLfloat v1,GLfloat v2,GLint vstride,GLint vorder,GLfloat[] points)
[<DllImport(DLL)>]extern void glMaterialf(GLenum face,GLenum pname,GLfloat param)
[<DllImport(DLL)>]extern void glMaterialfv(GLenum face,GLenum pname,GLfloat[] ``params``)
[<DllImport(DLL)>]extern void glMateriali(GLenum face,GLenum pname,GLint param)
[<DllImport(DLL)>]extern void glMaterialiv(GLenum face,GLenum pname,GLfloat[] ``params``)
[<DllImport(DLL)>]extern void glMatrixMode(GLenum mode)
[<DllImport(DLL)>]extern void glNewList(GLuint list,GLenum mode)
[<DllImport(DLL)>]extern void glNormal3f(GLfloat nx,GLfloat ny,GLfloat nz)
[<DllImport(DLL)>]extern void glNormalPointer(GLenum ``type``,GLsizei stride,GLfloat[] pointer)
[<DllImport(DLL)>]extern void glOrtho(GLdouble left,GLdouble right,GLdouble bottom,GLdouble top,GLdouble zNear,GLdouble zFar)
[<DllImport(DLL)>]extern void glPolygonMode(GLenum face,GLenum mode)
[<DllImport(DLL)>]extern void glPolygonOffset(GLfloat factor,GLfloat units)
[<DllImport(DLL)>]extern void glPopMatrix()
[<DllImport(DLL)>]extern void glPushMatrix()
[<DllImport(DLL)>]extern void glRectf(GLfloat x1,GLfloat y1,GLfloat x2,GLfloat y2)
[<DllImport(DLL)>]extern void glRotatef(GLfloat angle,GLfloat x,GLfloat y,GLfloat z)
[<DllImport(DLL)>]extern void glShadeModel(GLenum mode)
[<DllImport(DLL)>]extern void glTexCoordPointer(GLint size,GLenum ``type``,GLsizei stride,GLfloat[] pointer)
[<DllImport(DLL)>]extern void glTranslatef(GLfloat x,GLfloat y,GLfloat z)
[<DllImport(DLL)>]extern void glVertex3f(GLfloat x,GLfloat y,GLfloat z)
[<DllImport(DLL)>]extern void glVertexPointer(GLint size,GLenum ``type``,GLsizei stride,GLfloat[] pointer)
[<DllImport(DLL)>]extern void glViewport(GLint x,GLint y,GLsizei width,GLsizei height)

[<DllImport(DLLU)>]extern void gluPerspective(GLdouble fovy,GLdouble aspect,GLdouble zNear,GLdouble zFar)
[<DllImport(DLLU)>]extern void gluLookAt(GLdouble eyex,GLdouble eyey,GLdouble eyez,GLdouble centerx,GLdouble centery,GLdouble centerz,GLdouble upx,GLdouble upy,GLdouble upz)

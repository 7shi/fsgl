(*
 * 3-D gear wheels.  This program is in the public domain.
 *
 * Brian Paul
 *)

// Conversion to GLUT by Mark J. Kilgard
// https://www.opengl.org/archives/resources/code/samples/glut_examples/mesademos/gears.c

// Conversion to F# by 7shi

#load "GL7.fsx"
#load "GL7.GL.fsx"

open System
open System.Drawing
open System.Drawing.Imaging
open System.IO
open System.Windows.Forms
open GL7
open GL7.GL

let M_PI = float32 Math.PI

(**

  Draw a gear wheel.  You'll probably want to call this function when
  building a display list since we do a lot of trig here.
 
  Input:  inner_radius - radius of hole at center
          outer_radius - radius at center of teeth
          width - width of gear
          teeth - number of teeth
          tooth_depth - depth of tooth

 **)

let gear inner_radius outer_radius width teeth tooth_depth =
    let r0 = inner_radius
    let r1 = outer_radius - tooth_depth / 2.0f
    let r2 = outer_radius + tooth_depth / 2.0f

    let da = 2.0f * M_PI / float32 teeth / 4.0f

    glShadeModel(GL_FLAT)

    glNormal3f(0.0f, 0.0f, 1.0f)

    // draw front face
    glBegin(GL_QUAD_STRIP)
    for i = 0 to teeth do
        let angle = float32 i * 2.0f * M_PI / float32 teeth
        glVertex3f(r0 * cos(angle), r0 * sin(angle), width * 0.5f)
        glVertex3f(r1 * cos(angle), r1 * sin(angle), width * 0.5f)
        glVertex3f(r0 * cos(angle), r0 * sin(angle), width * 0.5f)
        glVertex3f(r1 * cos(angle + 3.f * da), r1 * sin(angle + 3.f * da), width * 0.5f)
    glEnd()

    // draw front sides of teeth
    glBegin(GL_QUADS)
    let da = 2.0f * M_PI / float32 teeth / 4.0f
    for i = 0 to teeth - 1 do
        let angle = float32 i * 2.0f * M_PI / float32 teeth
        glVertex3f(r1 * cos(angle), r1 * sin(angle), width * 0.5f)
        glVertex3f(r2 * cos(angle + da), r2 * sin(angle + da), width * 0.5f)
        glVertex3f(r2 * cos(angle + 2.f * da), r2 * sin(angle + 2.f * da), width * 0.5f)
        glVertex3f(r1 * cos(angle + 3.f * da), r1 * sin(angle + 3.f * da), width * 0.5f)
    glEnd()

    glNormal3f(0.0f, 0.0f, -1.0f)

    // draw back face
    glBegin(GL_QUAD_STRIP)
    for i = 0 to teeth do
        let angle = float32 i * 2.0f * M_PI / float32 teeth
        glVertex3f(r1 * cos(angle), r1 * sin(angle), -width * 0.5f)
        glVertex3f(r0 * cos(angle), r0 * sin(angle), -width * 0.5f)
        glVertex3f(r1 * cos(angle + 3.f * da), r1 * sin(angle + 3.f * da), -width * 0.5f)
        glVertex3f(r0 * cos(angle), r0 * sin(angle), -width * 0.5f)
    glEnd()

    // draw back sides of teeth
    glBegin(GL_QUADS)
    let da = 2.0f * M_PI / float32 teeth / 4.0f
    for i = 0 to teeth - 1 do
        let angle = float32 i * 2.0f * M_PI / float32 teeth
        glVertex3f(r1 * cos(angle + 3.f * da), r1 * sin(angle + 3.f * da), -width * 0.5f)
        glVertex3f(r2 * cos(angle + 2.f * da), r2 * sin(angle + 2.f * da), -width * 0.5f)
        glVertex3f(r2 * cos(angle + da), r2 * sin(angle + da), -width * 0.5f)
        glVertex3f(r1 * cos(angle), r1 * sin(angle), -width * 0.5f)
    glEnd()

    // draw outward faces of teeth
    glBegin(GL_QUAD_STRIP)
    for i = 0 to teeth - 1 do
        let angle = float32 i * 2.0f * M_PI / float32 teeth
        glVertex3f(r1 * cos(angle), r1 * sin(angle),  width * 0.5f)
        glVertex3f(r1 * cos(angle), r1 * sin(angle), -width * 0.5f)
        let u = r2 * cos(angle + da) - r1 * cos(angle)
        let v = r2 * sin(angle + da) - r1 * sin(angle)
        let len = sqrt(u * u + v * v)
        let u = u / len
        let v = v / len
        glNormal3f(v, -u, 0.0f)
        glVertex3f(r2 * cos(angle + da), r2 * sin(angle + da),  width * 0.5f)
        glVertex3f(r2 * cos(angle + da), r2 * sin(angle + da), -width * 0.5f)
        glNormal3f(cos(angle), sin(angle), 0.0f)
        glVertex3f(r2 * cos(angle + 2.f * da), r2 * sin(angle + 2.f * da),  width * 0.5f)
        glVertex3f(r2 * cos(angle + 2.f * da), r2 * sin(angle + 2.f * da), -width * 0.5f)
        let u = r1 * cos(angle + 3.f * da) - r2 * cos(angle + 2.f * da)
        let v = r1 * sin(angle + 3.f * da) - r2 * sin(angle + 2.f * da)
        glNormal3f(v, -u, 0.0f)
        glVertex3f(r1 * cos(angle + 3.f * da), r1 * sin(angle + 3.f * da),  width * 0.5f)
        glVertex3f(r1 * cos(angle + 3.f * da), r1 * sin(angle + 3.f * da), -width * 0.5f)
        glNormal3f(cos(angle), sin(angle), 0.0f)

    glVertex3f(r1 * cos(0.f), r1 * sin(0.f),  width * 0.5f)
    glVertex3f(r1 * cos(0.f), r1 * sin(0.f), -width * 0.5f)

    glEnd()

    glShadeModel(GL_SMOOTH)

    // draw inside radius cylinder
    glBegin(GL_QUAD_STRIP)
    for i = 0 to teeth do
        let angle = float32 i * 2.0f * M_PI / float32 teeth
        glNormal3f(-cos(angle), -sin(angle), 0.0f)
        glVertex3f(r0 * cos(angle), r0 * sin(angle), -width * 0.5f)
        glVertex3f(r0 * cos(angle), r0 * sin(angle),  width * 0.5f)
    glEnd()

let mutable view_rotx, view_roty, view_rotz = 20.0f, 30.0f, 0.0f
let mutable gear1, gear2, gear3 = 0u, 0u, 0u
let mutable angle = 0.0f

let draw() =
    glClear(GL_COLOR_BUFFER_BIT ||| GL_DEPTH_BUFFER_BIT)

    glPushMatrix()
    glRotatef(view_rotx, 1.0f, 0.0f, 0.0f)
    glRotatef(view_roty, 0.0f, 1.0f, 0.0f)
    glRotatef(view_rotz, 0.0f, 0.0f, 1.0f)

    glPushMatrix()
    glTranslatef(-3.0f, -2.0f, 0.0f)
    glRotatef(angle, 0.0f, 0.0f, 1.0f)
    glCallList(gear1)
    glPopMatrix()

    glPushMatrix()
    glTranslatef(3.1f, -2.0f, 0.0f)
    glRotatef(-2.0f * angle - 9.0f, 0.0f, 0.0f, 1.0f)
    glCallList(gear2)
    glPopMatrix()

    glPushMatrix()
    glTranslatef(-3.1f, 4.2f, 0.0f)
    glRotatef(-2.0f * angle - 25.0f, 0.0f, 0.0f, 1.0f)
    glCallList(gear3)
    glPopMatrix()

    glPopMatrix()

// change view angle, exit upon ESC
let key (f:Form) = function
| 'z' -> view_rotz <- view_rotz + 5.0f
| 'Z' -> view_rotz <- view_rotz - 5.0f
| '\u001b' (* Escape *) -> f.Close()
| _ -> ()

// change view angle
let special = function
| Keys.Up    -> view_rotx <- view_rotx + 5.0f
| Keys.Down  -> view_rotx <- view_rotx - 5.0f
| Keys.Left  -> view_roty <- view_roty + 5.0f
| Keys.Right -> view_roty <- view_roty - 5.0f
| _ -> ()

// new window size or exposure
let reshape width height =
    let h = float height / float width
    glViewport(0, 0, width, height)
    glMatrixMode(GL_PROJECTION)
    glLoadIdentity()
    glFrustum(-1.0, 1.0, -h, h, 5.0, 60.0)
    glMatrixMode(GL_MODELVIEW)
    glLoadIdentity()
    glTranslatef(0.0f, 0.0f, -40.0f)

let init() =
    let pos   = [| 5.0f; 5.0f; 10.0f; 0.0f |]
    let red   = [| 0.8f; 0.1f;  0.0f; 1.0f |]
    let green = [| 0.0f; 0.8f;  0.2f; 1.0f |]
    let blue  = [| 0.2f; 0.2f;  1.0f; 1.0f |]

    glLightfv(GL_LIGHT0, GL_POSITION, pos)
    glEnable(GL_CULL_FACE)
    glEnable(GL_LIGHTING)
    glEnable(GL_LIGHT0)
    glEnable(GL_DEPTH_TEST)

    // make the gears
    gear1 <- glGenLists(1)
    glNewList(gear1, GL_COMPILE)
    glMaterialfv(GL_FRONT, GL_AMBIENT_AND_DIFFUSE, red)
    gear 1.0f 4.0f 1.0f 20 0.7f
    glEndList()

    gear2 <- glGenLists(1)
    glNewList(gear2, GL_COMPILE);
    glMaterialfv(GL_FRONT, GL_AMBIENT_AND_DIFFUSE, green)
    gear 0.5f 2.0f 2.0f 10 0.7f
    glEndList()

    gear3 <- glGenLists(1)
    glNewList(gear3, GL_COMPILE)
    glMaterialfv(GL_FRONT, GL_AMBIENT_AND_DIFFUSE, blue)
    gear 1.3f 2.0f 0.5f 10 0.7f
    glEndList()

    glEnable(GL_NORMALIZE)

#if OUTPUT_PNG
let dir = "output"
if not <| Directory.Exists dir then
    ignore <| Directory.CreateDirectory dir
let bmp = new GLBitmap(256, 256)
do  use raii = bmp.MakeCurrent()
    init()
    reshape bmp.Width bmp.Height
for i = 0 to 8 do
    angle <- float32 i * 2.f
    do use raii = bmp.MakeCurrent() in draw()
    use img = Image.FromHbitmap bmp.Handle
    let fn = sprintf "%02d.png" i
    printfn "%s" fn
    img.Save(Path.Combine(dir, fn), ImageFormat.Png)
#else
[<EntryPoint; STAThread>] do
let size = Size(256, 256)
let f = new Form(Text = "Gears (Off-Screen Rendering)", ClientSize = size)
let bmp = new GLBitmap(size.Width, size.Height)
do
    use raii = bmp.MakeCurrent() in draw()
    init()
    reshape size.Width size.Height
let p = new PictureBox(Dock = DockStyle.Fill)
p.Paint.Add <| fun e ->
    draw()
    angle <- angle + 0.1f  //2.0f
    do use raii = bmp.MakeCurrent() in draw()
    use img = Image.FromHbitmap bmp.Handle
    e.Graphics.DrawImage(img, (p.Width - size.Width) / 2, (p.Height - size.Height) / 2)
    p.Invalidate()
f.Controls.Add p
Application.Run f
#endif

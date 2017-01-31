(*
 * Copyright (c) 1993-1997, Silicon Graphics, Inc.
 * ALL RIGHTS RESERVED 
 * Permission to use, copy, modify, and distribute this software for 
 * any purpose and without fee is hereby granted, provided that the above
 * copyright notice appear in all copies and that both the copyright notice
 * and this permission notice appear in supporting documentation, and that 
 * the name of Silicon Graphics, Inc. not be used in advertising
 * or publicity pertaining to distribution of the software without specific,
 * written prior permission. 
 *
 * THE MATERIAL EMBODIED ON THIS SOFTWARE IS PROVIDED TO YOU "AS-IS"
 * AND WITHOUT WARRANTY OF ANY KIND, EXPRESS, IMPLIED OR OTHERWISE,
 * INCLUDING WITHOUT LIMITATION, ANY WARRANTY OF MERCHANTABILITY OR
 * FITNESS FOR A PARTICULAR PURPOSE.  IN NO EVENT SHALL SILICON
 * GRAPHICS, INC.  BE LIABLE TO YOU OR ANYONE ELSE FOR ANY DIRECT,
 * SPECIAL, INCIDENTAL, INDIRECT OR CONSEQUENTIAL DAMAGES OF ANY
 * KIND, OR ANY DAMAGES WHATSOEVER, INCLUDING WITHOUT LIMITATION,
 * LOSS OF PROFIT, LOSS OF USE, SAVINGS OR REVENUE, OR THE CLAIMS OF
 * THIRD PARTIES, WHETHER OR NOT SILICON GRAPHICS, INC.  HAS BEEN
 * ADVISED OF THE POSSIBILITY OF SUCH LOSS, HOWEVER CAUSED AND ON
 * ANY THEORY OF LIABILITY, ARISING OUT OF OR IN CONNECTION WITH THE
 * POSSESSION, USE OR PERFORMANCE OF THIS SOFTWARE.
 * 
 * US Government Users Restricted Rights 
 * Use, duplication, or disclosure by the Government is subject to
 * restrictions set forth in FAR 52.227.19(c)(2) or subparagraph
 * (c)(1)(ii) of the Rights in Technical Data and Computer Software
 * clause at DFARS 252.227-7013 and/or in similar or successor
 * clauses in the FAR or the DOD or NASA FAR Supplement.
 * Unpublished-- rights reserved under the copyright laws of the
 * United States.  Contractor/manufacturer is Silicon Graphics,
 * Inc., 2011 N.  Shoreline Blvd., Mountain View, CA 94039-7311.
 *
 * OpenGL(R) is a registered trademark of Silicon Graphics, Inc.
 *)

(*  bezmesh.c
 *  https://www.opengl.org/archives/resources/code/samples/redbook/bezmesh.c
 *  This program renders a lighted, filled Bezier surface,
 *  using two-dimensional evaluators.
 *)

#load "GL7.fsx"
#load "GL7.GL.fsx"

open System
open System.Drawing
open System.IO
open System.Windows.Forms
open GL7
open GL7.GL

let ctrlpoints =
    [ [ [-1.5f; -1.5f;  4.0f]
        [-0.5f; -1.5f;  2.0f]
        [ 0.5f; -1.5f; -1.0f]
        [ 1.5f; -1.5f;  2.0f] ]
      [ [-1.5f; -0.5f;  1.0f]
        [-0.5f; -0.5f;  3.0f]
        [ 0.5f; -0.5f;  0.0f]
        [ 1.5f; -0.5f; -1.0f] ]
      [ [-1.5f;  0.5f;  4.0f]
        [-0.5f;  0.5f;  0.0f]
        [ 0.5f;  0.5f;  3.0f]
        [ 1.5f;  0.5f;  4.0f] ]
      [ [-1.5f;  1.5f; -2.0f]
        [-0.5f;  1.5f; -2.0f]
        [ 0.5f;  1.5f;  0.0f]
        [ 1.5f;  1.5f; -1.0f] ] ]
    |> Seq.concat |> Seq.concat |> Seq.toArray

let initlights() =
    let ambient       = [|0.2f; 0.2f; 0.2f; 1.0f|]
    let position      = [|0.0f; 0.0f; 2.0f; 1.0f|]
    let mat_diffuse   = [|0.6f; 0.6f; 0.6f; 1.0f|]
    let mat_specular  = [|1.0f; 1.0f; 1.0f; 1.0f|]
    let mat_shininess = [|50.0f|]

    glEnable(GL_LIGHTING);
    glEnable(GL_LIGHT0);

    glLightfv(GL_LIGHT0, GL_AMBIENT, ambient)
    glLightfv(GL_LIGHT0, GL_POSITION, position)

    glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_diffuse)
    glMaterialfv(GL_FRONT, GL_SPECULAR, mat_specular)
    glMaterialfv(GL_FRONT, GL_SHININESS, mat_shininess)

let display() =
   glClear(GL_COLOR_BUFFER_BIT ||| GL_DEPTH_BUFFER_BIT)
   glPushMatrix()
   glRotatef(85.0f, 1.0f, 1.0f, 1.0f)
   glEvalMesh2(GL_FILL, 0, 20, 0, 20)
   glPopMatrix()
   glFlush()

let init() =
   glClearColor(0.0f, 0.0f, 0.0f, 0.0f)
   glEnable(GL_DEPTH_TEST)
   glMap2f(GL_MAP2_VERTEX_3, 0.f, 1.f, 3, 4,
           0.f, 1.f, 12, 4, ctrlpoints)
   glEnable(GL_MAP2_VERTEX_3)
   glEnable(GL_AUTO_NORMAL)
   glMapGrid2f(20, 0.0f, 1.0f, 20, 0.0f, 1.0f)
   initlights()  // for lighted version only

let reshape (f:GLForm) =
    let cr = f.ClientSize
    let w, h = cr.Width, cr.Height
    glViewport(0, 0, w, h)
    glMatrixMode(GL_PROJECTION)
    glLoadIdentity()
    if w <= h then
        glOrtho(-4.0, 4.0, -4.0 * float h / float w,
                 4.0 * float h / float w, -4.0, 4.0)
    else
        glOrtho(-4.0 * float w / float h,
                 4.0 * float w / float h, -4.0, 4.0, -4.0, 4.0)
    glMatrixMode(GL_MODELVIEW)
    glLoadIdentity()

let keyboard (f:GLForm) = function
| '\u001b' (* Escape *) -> f.Close()
| _ -> ()

[<EntryPoint; STAThread>]
let main args =
    let argv0 = Path.GetFileNameWithoutExtension Application.ExecutablePath
    let f = new GLForm(Text = argv0, ClientSize = Size(500, 500))
    f.Load.Add <| fun _ ->
        use raii = f.MakeCurrent()
        init()
        reshape f
    f.Resize.Add <| fun _ ->
        use raii = f.MakeCurrent()
        reshape f
    f.Paint.Add <| fun _ ->
        display()
    f.KeyPress.Add <| fun e ->
        keyboard f e.KeyChar
    Application.Run f
    0

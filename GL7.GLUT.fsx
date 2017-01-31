(*
 * fg_geometry.c
 *
 * Freeglut geometry rendering methods.
 *
 * Copyright (c) 1999-2000 Pawel W. Olszta. All Rights Reserved.
 * Written by Pawel W. Olszta, <olszta@sourceforge.net>
 * Creation date: Fri Dec 3 1999
 *
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
 * OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.  IN NO EVENT SHALL
 * PAWEL W. OLSZTA BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 *)

module GL7.GLUT

#load "GL7.GL.fsx"

open System
open System.Runtime.InteropServices
open GL7.GL

(* Drawing geometry:
 * Explanation of the functions has to be separate for the polyhedra and
 * the non-polyhedra (objects with a circular cross-section).
 * Polyhedra:
 *   - We have only implemented the five platonic solids and the rhomboid
 *     dodecahedron. If you need more types of polyhedra, please see
 *     CPolyhedron in MRPT
 *   - Solids are drawn by glDrawArrays if composed of triangular faces
 *     (the tetrahedron, octahedron, and icosahedron), or are first
 *     decomposed into triangles and then drawn by glDrawElements if its
 *     faces are squares or pentagons (cube, dodecahedron and rhombic
 *     dodecahedron) as some vertices are repeated in that case.
 *   - WireFrame drawing is done using a GL_LINE_LOOP per face, and thus
 *     issuing one draw call per face. glDrawArrays is always used as no
 *     triangle decomposition is needed to draw faces. We use the "first"
 *     parameter in glDrawArrays to go from face to face.
 * 
 * Non-polyhedra:
 *   - We have implemented the sphere, cylinder, cone and torus.
 *   - All shapes are characterized by two parameters: the number of
 *     subdivisions along two axes used to construct the shape's vertices 
 *     (e.g. stacks and slices for the sphere).
 *     As different subdivisions are most suitable for different shapes,
 *     and are thus also named differently, I wont provide general comments
 *     on them here.
 *   - Solids are drawn using glDrawArrays and GL_TRIANGLE_STRIP. Each
 *     strip covers one revolution around one of the two subdivision axes
 *     of the shape.
 *   - WireFrame drawing is done for the subdivisions along the two axes 
 *     separately, usually using GL_LINE_LOOP. Vertex index arrays are
 *     built containing the vertices to be drawn for each loop, which are
 *     then drawn using multiple calls to glDrawElements. As the number of
 *     subdivisions along the two axes is not guaranteed to be equal, the
 *     vertex indices for e.g. stacks and slices are stored in separate
 *     arrays, which makes the input to the drawing function a bit clunky,
 *     but allows for the same drawing function to be used for all shapes.
 *)

(**
 * Draw geometric shape in wire mode (only edges)
 *
 * Arguments:
 * GLfloat *vertices, GLfloat *normals, GLsizei numVertices
 *   The vertex coordinate and normal buffers, and the number of entries in
 *   those
 * GLushort *vertIdxs
 *   a vertex indices buffer, optional (never passed for the polyhedra)
 * GLsizei numParts, GLsizei numVertPerPart
 *   polyhedra: number of faces, and the number of vertices for drawing
 *     each face
 *   non-polyhedra: number of edges to draw for first subdivision (not
 *     necessarily equal to number of subdivisions requested by user, e.g.
 *     as each subdivision is enclosed by two edges), and number of
 *     vertices for drawing each
 *   numParts * numVertPerPart gives the number of entries in the vertex
 *     array vertIdxs
 * GLenum vertexMode
 *   vertex drawing mode (e.g. always GL_LINE_LOOP for polyhedra, varies
 *   for others)
 * GLushort *vertIdxs2, GLsizei numParts2, GLsizei numVertPerPart2
 *   non-polyhedra only: same as the above, but now for subdivisions along
 *   the other axis. Always drawn as GL_LINE_LOOP.
 *
 * Feel free to contribute better naming ;)
 *)
let private fghDrawGeometryWire11(vertices, normals,
                                  vertIdxs , numParts , numVertPerPart, vertexMode,
                                  vertIdxs2, numParts2, numVertPerPart2) =
    glEnableClientState(GL_VERTEX_ARRAY)
    glEnableClientState(GL_NORMAL_ARRAY)

    glVertexPointer(3, GL_FLOAT, 0, vertices)
    glNormalPointer(GL_FLOAT, 0, normals)

    if vertIdxs = [||] then
        // Draw per face (TODO: could use glMultiDrawArrays if available)
        for i = 0 to numParts - 1 do
            glDrawArrays(vertexMode, i * numVertPerPart, numVertPerPart)
    else
        for i = 0 to numParts - 1 do
            glDrawElements(vertexMode, numVertPerPart, GL_UNSIGNED_SHORT, vertIdxs.[i])

    if vertIdxs2 <> [||] then
        for i = 0 to numParts2 - 1 do
            glDrawElements(GL_LINE_LOOP, numVertPerPart2, GL_UNSIGNED_SHORT, vertIdxs2.[i])

    glDisableClientState(GL_VERTEX_ARRAY)
    glDisableClientState(GL_NORMAL_ARRAY)

let private fghDrawGeometryWire(vertices, normals, numVertices,
                                vertIdxs , numParts , numVertPerPart, vertexMode,
                                vertIdxs2, numParts2, numVertPerPart2) =
    fghDrawGeometryWire11(vertices, normals,
                          vertIdxs, numParts, numVertPerPart, vertexMode,
                          vertIdxs2, numParts2, numVertPerPart2)

(* Draw the geometric shape with filled triangles
 *
 * Arguments:
 * GLfloat *vertices, GLfloat *normals, GLfloat *textcs, GLsizei numVertices
 *   The vertex coordinate, normal and texture coordinate buffers, and the
 *   number of entries in those
 * GLushort *vertIdxs
 *   a vertex indices buffer, optional (not passed for the polyhedra with
 *   triangular faces)
 * GLsizei numParts, GLsizei numVertPerPart
 *   polyhedra: not used for polyhedra with triangular faces
       (numEdgePerFace==3), as each vertex+normal pair is drawn only once,
       so no vertex indices are used.
       Else, the shape was triangulated (DECOMPOSE_TO_TRIANGLE), leading to
       reuse of some vertex+normal pairs, and thus the need to draw with
       glDrawElements. numParts is always 1 in this case (we can draw the
       whole object with one call to glDrawElements as the vertex index
       array contains separate triangles), and numVertPerPart indicates
       the number of vertex indices in the vertex array.
 *   non-polyhedra: number of parts (GL_TRIANGLE_STRIPs) to be drawn
       separately (numParts calls to glDrawElements) to create the object.
       numVertPerPart indicates the number of vertex indices to be
       processed at each draw call.
 *   numParts * numVertPerPart gives the number of entries in the vertex
 *     array vertIdxs
 *)
let private fghDrawGeometrySolid11(vertices, normals, textcs, numVertices,
                                   vertIdxs, numParts, numVertIdxsPerPart) =
    glEnableClientState(GL_VERTEX_ARRAY)
    glEnableClientState(GL_NORMAL_ARRAY)

    glVertexPointer(3, GL_FLOAT, 0, vertices)
    glNormalPointer(GL_FLOAT, 0, normals)

    if textcs <> [||] then
        glEnableClientState(GL_TEXTURE_COORD_ARRAY)
        glTexCoordPointer(2, GL_FLOAT, 0, textcs)

    if vertIdxs = [||] then
        glDrawArrays(GL_TRIANGLES, 0, numVertices)
    elif numParts > 1 then
        for i = 0 to numParts - 1 do
            glDrawElements(GL_TRIANGLE_STRIP, numVertIdxsPerPart, GL_UNSIGNED_SHORT, vertIdxs.[i])
    else
        glDrawElements(GL_TRIANGLES, numVertIdxsPerPart, GL_UNSIGNED_SHORT, vertIdxs.[0])

    glDisableClientState(GL_VERTEX_ARRAY)
    glDisableClientState(GL_NORMAL_ARRAY)
    if textcs <> [||] then
        glDisableClientState(GL_TEXTURE_COORD_ARRAY)

let private fghDrawGeometrySolid(vertices, normals, textcs, numVertices,
                                 vertIdxs, numParts, numVertIdxsPerPart) =
    fghDrawGeometrySolid11(vertices, normals, textcs, numVertices,
                           vertIdxs, numParts, numVertIdxsPerPart)

// -- Now the various non-polyhedra (shapes involving circles) --

(*
 * Compute lookup table of cos and sin values forming a circle
 * (or half circle if halfCircle==TRUE)
 *
 * Notes:
 *    It is the responsibility of the caller to free these tables
 *    The size of the table is (n+1) to form a connected loop
 *    The last entry is exactly the same as the first
 *    The sign of n can be flipped to get the reverse loop
 *)
let private fghCircleTable(n, halfCircle) =
    // Table size, the sign of n flips the circle direction
    let size = abs n

    // Determine the angle between samples
    let angle = (if halfCircle then 1.f else 2.f) * float32 Math.PI / float32 (if n = 0 then 1 else n)

    // Compute cos and sin around the circle
    [|  yield 0.0f, 1.0f  // sin 0, cos 0
        for i in 1 .. size - 1 -> sin(angle * float32 i), cos(angle * float32 i)
        yield
            if halfCircle then 0.0f, -1.0f  // sin PI, cos PI
            else 0.0f, 1.0f  // Last sample is duplicate of the first (sin or cos of 2 PI)
    |] |> Array.unzip

let private fghGenerateSphere(radius, slices, stacks) =
    let mutable idx = 0  // idx into vertex/normal buffer

    // number of unique vertices
    if slices = 0 || stacks < 2 then
        // nothing to generate
        [||], [||], 0
    else
    let nVert = slices * (stacks - 1) + 2
    if nVert > 65535 then
        // limit of glushort, thats 256*256 subdivisions, should be enough in practice. See note above
        failwith "fghGenerateSphere: too many slices or stacks requested, indices will wrap"

    // precompute values on unit circle
    let sint1, cost1 = fghCircleTable(-slices, false)
    let sint2, cost2 = fghCircleTable( stacks, true )

    // Allocate vertex and normal buffers, bail out if memory allocation fails
    let vertices, normals =
     [| // top
        yield! [0.f, 0.f; 0.f, 0.f; radius, 1.f]

        // each stack
        for i in 1 .. stacks - 1 do
            for j in 0 .. slices - 1 do
                let x = cost1.[j] * sint2.[i]
                let y = sint1.[j] * sint2.[i]
                let z = cost2.[i]
                yield! [x * radius, x; y * radius, y; z * radius, z]

        // bottom
        yield! [0.f, 0.f; 0.f, 0.f; -radius, -1.f] |] |> Array.unzip

    vertices, normals, nVert

let private fghGenerateCone(bas:float32, height:float32, slices, stacks) =
    let zStep = height / float32 (max 1 stacks)
    let rStep = bas    / float32 (max 1 stacks)

    // Scaling factors for vertex normals
    let cosn = (height / sqrt(height * height + bas * bas))
    let sinn = (bas    / sqrt(height * height + bas * bas))

    // number of unique vertices
    if slices = 0 || stacks < 1 then
        // nothing to generate
        [||], [||], 0
    else
    let nVert = slices * (stacks + 2) + 1  // need an extra stack for closing off bottom with correct normals
    if nVert > 65535 then
        // limit of glushort, thats 256*256 subdivisions, should be enough in practice. See note above
        failwith "fghGenerateCone: too many slices or stacks requested, indices will wrap"

    // Pre-computed circle
    let sint, cost = fghCircleTable(-slices, false)

    // Allocate vertex and normal buffers, bail out if memory allocation fails
    let vertices, normals =
     [| // bottom
        yield! [0.f, 0.f; 0.f, 0.f; 0.f, -1.f]

        // other on bottom (get normals right)
        for j in 0 .. slices - 1 do
            yield! [cost.[j] * bas, 0.f; sint.[j] * bas, 0.f; 0.f, -1.f]

        // each stack
        for i in 0 .. stacks do
            let z, r = zStep * float32 i, bas - rStep * float32 i
            for j in 0 .. slices - 1 do
                yield! [cost.[j] * r, cost.[j] * cosn; sint.[j] * r, sint.[j] * cosn; z, sinn]
     |] |> Array.unzip

    vertices, normals, nVert

let private fghGenerateTorus(dInnerRadius, dOuterRadius, nSides, nRings) =
    let iradius = dInnerRadius
    let oradius = dOuterRadius

    // number of unique vertices
    if nSides < 2 || nRings < 2 then
        // nothing to generate
        [||], [||], 0
    else
    let nVert = nSides * nRings
    if nVert > 65535 then
        // limit of glushort, thats 256*256 subdivisions, should be enough in practice. See note above
        failwith "fghGenerateTorus: too many slices or stacks requested, indices will wrap"

    // precompute values on unit circle
    let spsi, cpsi = fghCircleTable( nRings, false)
    let sphi, cphi = fghCircleTable(-nSides, false)

    // Allocate vertex and normal buffers, bail out if memory allocation fails
    let vertices, normals =
     [| for j in 0 .. nRings - 1 do
        for i in 0 .. nSides - 1 do
        let offset = 3 * (j * nSides + i)
        yield cpsi.[j] * (oradius + cphi.[i] * iradius), cpsi.[j] * cphi.[i]
        yield spsi.[j] * (oradius + cphi.[i] * iradius), spsi.[j] * cphi.[i]
        yield                       sphi.[i] * iradius ,            sphi.[i]
     |] |> Array.unzip

    vertices, normals, nVert

// -- INTERNAL DRAWING functions ---------------------------------------

let private fghSphere(radius, slices, stacks, useWireMode) =
    // Generate vertices and normals
    let vertices, normals, nVert = fghGenerateSphere(radius,slices,stacks)
    
    if nVert = 0 then
        // nothing to draw
        ()

    elif useWireMode then
        (* First, generate vertex index arrays for drawing with glDrawElements
         * We have a bunch of line_loops to draw for each stack, and a
         * bunch for each slice.
         *)

        // generate for each stack
        let stackIdx = [|
            for i in 0 .. stacks - 2 ->
             [| let offset = 1 + i * slices  // start at 1 (0 is top vertex), and we advance one stack down as we go along
                for j in 0 .. slices - 1 -> offset + j |> uint16 |]|]

        // generate for each slice
        let sliceIdx = [|
            for i in 0 .. slices - 1 ->
             [| let offset = 1 + i  // start at 1 (0 is top vertex), and we advance one slice as we go along */
                yield 0us           // vertex on top
                for j in 0 .. stacks - 2 -> offset + j * slices |> uint16
                yield nVert - 1 |> uint16  // zero based index, last element in array...
             |]|]

        // draw
        fghDrawGeometryWire(vertices, normals, nVert,
            sliceIdx, slices, stacks + 1, GL_LINE_STRIP,
            stackIdx, stacks - 1, slices)

    else
        (* First, generate vertex index arrays for drawing with glDrawElements
         * All stacks, including top and bottom are covered with a triangle
         * strip.
         *)

        // Allocate buffers for indices, bail out if memory allocation fails
        let stripIdx = [|

            // top stack
            yield [|
                for j in 0 .. slices - 1 do
                    yield j + 1 |> uint16  // 0 is top vertex, 1 is first for first stack
                    yield 0us
                yield 1us  // repeat first slice's idx for closing off shape
                yield 0us |]

            // middle stacks:
            // Strip indices are relative to first index belonging to strip, NOT relative to first vertex/normal pair in array
            for i in 0 .. stacks - 3 ->
             [| let offset = 1 + i * slices  // triangle_strip indices start at 1 (0 is top vertex), and we advance one stack down as we go along
                for j in 0 .. slices - 1 do
                    yield offset + j + slices |> uint16
                    yield offset + j |> uint16
                yield offset + slices |> uint16  // repeat first slice's idx for closing off shape
                yield offset |> uint16 |]

            // bottom stack
            yield [|
                let offset = 1 + (stacks - 2) * slices  // triangle_strip indices start at 1 (0 is top vertex), and we advance one stack down as we go along
                for j in 0 .. slices - 1 do
                    yield nVert  - 1 |> uint16  // zero based index, last element in array (bottom vertex)...
                    yield offset + j |> uint16
                yield nVert - 1 |> uint16       // repeat first slice's idx for closing off shape
                yield offset    |> uint16 |]|]

        // draw
        fghDrawGeometrySolid(vertices, normals, [||], nVert, stripIdx, stacks, (slices + 1) * 2)

let private fghCone(bas, height, slices, stacks, useWireMode) =
    // Generate vertices and normals
    // Note, (stacks+1)*slices vertices for side of object, slices+1 for top and bottom closures
    let vertices, normals, nVert = fghGenerateCone(bas, height, slices, stacks)

    if nVert = 0 then
        // nothing to draw
        ()

    elif useWireMode then
        (* First, generate vertex index arrays for drawing with glDrawElements
         * We have a bunch of line_loops to draw for each stack, and a
         * bunch for each slice.
         *)

        // generate for each stack
        let stackIdx = [|
            for i in 0 .. stacks - 1 ->
             [| let offset = 1 + (i + 1) * slices  // start at 1 (0 is top vertex), and we advance one stack down as we go along
                for j in 0 .. slices - 1 -> offset + j |> uint16 |]|]

        // generate for each slice
        let sliceIdx = [|
         [| for i in 0 .. slices - 1 do
            let offset = 1 + i  // start at 1 (0 is top vertex), and we advance one slice as we go along
            yield offset + slices                |> uint16
            yield offset + (stacks + 1) * slices |> uint16 |]|]

        // draw
        fghDrawGeometryWire(vertices, normals, nVert,
            sliceIdx, 1, slices * 2, GL_LINES,
            stackIdx, stacks, slices)

    else
        (* First, generate vertex index arrays for drawing with glDrawElements
         * All stacks, including top and bottom are covered with a triangle
         * strip.
         *)

        // Allocate buffers for indices, bail out if memory allocation fails
        let stripIdx = [|

            // top stack
            yield [|
                for j in 0 .. slices - 1 do
                    yield 0us
                    yield j + 1 |> uint16  // 0 is top vertex, 1 is first for first stack
                yield 0us  // repeat first slice's idx for closing off shape
                yield 1us |]

            // middle stacks:
            // Strip indices are relative to first index belonging to strip, NOT relative to first vertex/normal pair in array
            for i in 0 .. stacks - 1 ->
             [| let offset = 1 + (i + 1) * slices  // triangle_strip indices start at 1 (0 is top vertex), and we advance one stack down as we go along
                for j in 0 .. slices - 1 do
                    yield offset + j          |> uint16
                    yield offset + j + slices |> uint16
                yield offset          |> uint16  // repeat first slice's idx for closing off shape
                yield offset + slices |> uint16 |]|]

        // draw
        fghDrawGeometrySolid(vertices, normals, [||], nVert, stripIdx, stacks + 1, (slices + 1) * 2)

let private fghTorus(dInnerRadius, dOuterRadius, nSides, nRings, useWireMode) =
    // Generate vertices and normals
    let vertices, normals, nVert = fghGenerateTorus(dInnerRadius, dOuterRadius, nSides, nRings)

    if nVert = 0 then
        // nothing to draw
        ()

    elif useWireMode then
        (* First, generate vertex index arrays for drawing with glDrawElements
         * We have a bunch of line_loops to draw each side, and a
         * bunch for each ring.
         *)

        // generate for each ring
        let ringIdx = [|
            for j in 0 .. nRings - 1 ->
             [| for i in 0 .. nSides - 1 -> j * nSides + i |> uint16 |]|]

        // generate for each side
        let sideIdx = [|
            for i in 0 .. nSides - 1 ->
             [| for j in 0 .. nRings - 1 -> j * nSides + i |> uint16 |]|]

        // draw
        fghDrawGeometryWire(vertices, normals, nVert,
            ringIdx, nRings, nSides, GL_LINE_LOOP,
            sideIdx, nSides, nRings)

    else
        (* First, generate vertex index arrays for drawing with glDrawElements
         * All stacks, including top and bottom are covered with a triangle
         * strip.
         *)

        // Allocate buffers for indices, bail out if memory allocation fails
        let stripIdx = [|
            for i in 0 .. nSides - 1 ->
             [| let ioff = if i = nSides - 1 then -i else 1
                for j in 0 .. nRings - 1 do
                    let offset = j * nSides + i
                    yield offset        |> uint16
                    yield offset + ioff |> uint16
                // repeat first to close off shape
                yield i        |> uint16
                yield i + ioff |> uint16 |]|]

        // draw
        fghDrawGeometrySolid(vertices, normals, [||], nVert, stripIdx, nSides, (nRings + 1) * 2)

// -- INTERFACE FUNCTIONS ----------------------------------------------

// Draws a solid sphere
let glutSolidSphere(radius:float, slices, stacks) =
    fghSphere(float32 radius, slices, stacks, false)

// DDraws a wire sphere
let glutWireSphere(radius:float, slices, stacks) =
    fghSphere(float32 radius, slices, stacks, true)

// Draws a solid cone
let glutSolidCone(bas:float, height:float, slices, stacks) =
    fghCone(float32 bas, float32 height, slices, stacks, false)

// Draws a wire cone
let glutWireCone(bas:float, height:float, slices, stacks) =
    fghCone(float32 bas, float32 height, slices, stacks, true)

// Draws a wire torus
let glutWireTorus(dInnerRadius:float, dOuterRadius:float, nSides, nRings) =
    fghTorus(float32 dInnerRadius, float32 dOuterRadius, nSides, nRings, true)

// Draws a solid torus
let glutSolidTorus(dInnerRadius:float, dOuterRadius:float, nSides, nRings) =
    fghTorus(float32 dInnerRadius, float32 dOuterRadius, nSides, nRings, false)

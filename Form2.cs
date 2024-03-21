using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Net.Mail;
using System.Text.Json.Serialization.Metadata;
using System.Security.Cryptography.Xml;

//MathF.Cos ect use radians
//draw cylinders first so they dont go over the top of spheres
namespace Prototype_3
{
    public partial class Form2 : Form
    {
        List<Element> elements = Form1.elements;
        List<bond> bonds = Form1.bonds;
        List<List<int>> adjacencylist = Form1.adjacencylist;
        List<triangle> mesh = new List<triangle>();
        coor3d vcamera, origin, center = new coor3d();
        float distance = 15;
        bool isdown = false;
        float theta, thetax, thetay = 90f;
        int oldX, oldY = 0;
        bool[] bdrawn = new bool[Form1.bonds.Count];
        bool[] edrawn = new bool[Form1.elements.Count];
        int count = 1; 
        int row = 1;
      
        public Form2()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(panel_scroll);
            origin = (coor3d)makecoor(0, 0, 0);
            vcamera = (coor3d)makecoor(0, 0, 0);


        }
        void makemoleculeshapes()
        {



            coor3d[] centres = new coor3d[elements.Count];  //array of all centres drawn
            {
                //for (int i = 0; i < elements.Count; i++) 
                //{
                //    e = elements[i];
                //    if (i == 0)
                //    {
                //        coor3d c = new coor3d();
                //        c = (coor3d)makecoor(0, 0, 0);
                //        makesphere(c, 1);
                //        centres[i] = c;
                //    }
                //    else //if it isnt the first element being drawn
                //    {
                //        int maxbsize = e.maxbonds - adjacencylist[i].Count + 1;  //finds maximum bond size of element
                //        if (maxbsize == 2)
                //        {
                //            for (int j = 0; j < adjacencylist[i].Count; j++)
                //            {
                //                coor3d newc = new coor3d();
                //                newc = centres[i];
                //                newc.x += 3;
                //                makesphere(newc, 1); ;
                //                centres[adjacencylist[i][j]] = newc;

                //                if (j == 1)
                //                {
                //                    newc = centres[adjacencylist[i][j - 1]];
                //                    Matrix4x4 rotz = new Matrix4x4();
                //                    rotz = (Matrix4x4)rotatez(0.666666f * MathF.PI);
                //                    newc = (coor3d)matrixmult(rotz, newc);
                //                   makesphere(newc, 1);
                //                }
                //                else if (j == 2)
                //                {
                //                    newc = centres[adjacencylist[i][j - 1]];
                //                    Matrix4x4 rotz = new Matrix4x4();
                //                    rotz = (Matrix4x4)rotatez(1.333333333333f * MathF.PI);
                //                    newc = (coor3d)matrixmult(rotz, newc);
                //                    makesphere(newc, 1);
                //                }

                //            }

                //        }
                //    }
                //}
            }


            {


                coor3d centre, c2, c3, c4, c5 = new coor3d();
                centre = (coor3d)makecoor(0, 0, 0);

                c2 = (coor3d)makecoor(3 * MathF.Sin(109.5f * MathF.PI / 180), 0, 3 * MathF.Cos(109.5f * MathF.PI / 180)); //fisrt point but add sidelength in each direction

                Matrix4x4 rotz, roty = new Matrix4x4();
                float radtheta = (float)makerad(109.5);
                float rad120 = (float)makerad(120);
                rotz = (Matrix4x4)rotatex(radtheta);

                c3 = (coor3d)makecoor(3 * MathF.Sin(109.5f * MathF.PI / 180) * MathF.Cos(0.6666667f * MathF.PI), 3 * MathF.Sin(109.5f * MathF.PI / 180) * MathF.Sin(0.66666667f * MathF.PI), 3 * MathF.Cos(109.5f * MathF.PI / 180));


                roty = (Matrix4x4)rotatey(radtheta);

                Element e = new Element();
                e.e = "H";
                c4 = (coor3d)makecoor(3 * MathF.Sin(109.5f * MathF.PI / 180) * MathF.Cos(1.3333333f * MathF.PI), 3 * MathF.Sin(109.5f * MathF.PI / 180) * MathF.Sin(1.333333f * MathF.PI), 3 * MathF.Cos(109.5f * MathF.PI / 180));
                // c5 = (coor3d)makecoor(3 * MathF.Sin(109.5f * MathF.PI / 180) * MathF.Cos(2f * MathF.PI), 3 * MathF.Sin(109.5f * MathF.PI / 180) * MathF.Sin(2f * MathF.PI), 3 * MathF.Cos(109.5f * MathF.PI / 180));
                c5 = (coor3d)makecoor(0, 0, 3);
                //connectspheres(centre, c3, 1);
                //connectspheres(centre, c2, 1);
                //connectspheres(centre, c4, 1);
                //connectspheres(centre, c5, 1);
                makesphere(centre, 1f, e);
                makesphere(c2, 1f, e);
                makesphere(c3, 1f, e);
                makesphere(c4, 1f, e);
                makesphere(c5, 1f   , e);
                sort_mesh();

                //float rad240 = (float)makerad(240);  //trigonal planar
                //coor3d e1, e2, e3, e4 = new coor3d();
                //int l = 3;
                //e1 = (coor3d)makecoor(l, 0, 0);
                //e2 = (coor3d)makecoor(l * MathF.Cos(rad120), l * MathF.Sin(rad120), 0);
                //e3 = (coor3d)makecoor(l * MathF.Cos(rad240), l * MathF.Sin(rad240), 0);
                //e4 = (coor3d)makecoor(0, 0, 0);
                //makesphere(e1, 1);
                //makesphere(e2, 1);
                //makesphere(e3, 1);
                //makesphere(e4, 1);
            }
        }
        private void drawmol_Click(object sender, EventArgs e)
        {
         
            int mbsize;
            Element wild;
            coor3d nextc = origin;
            for (int i = 0; i < elements.Count; i++)
            {
                Element el = elements[i];
                if (el.e == "C")
                {
                    makesphere(origin, 0.5f, el);
                    edrawn[i] = true;
                    mbsize = el.maxbonds - el.bondsformed + 1;
                    mbsize = 1;
                    if (mbsize == 1)
                    {
                        nextc = (coor3d)maketetrahedral(i, nextc);
                        for (int j = 0; j < adjacencylist[i].Count; j++)
                        {


                            if (elements[adjacencylist[i][j]].e == "C")
                            {
                                nextc = (coor3d)maketetrahedral(i, nextc);
                            }
                            edrawn[adjacencylist[i][j]] = true;
                        }

                    }
                    else if (mbsize == 2)
                    {


                    }
                }



            }



        }
        private void rec_Click(object sender, EventArgs e)
        {
            Element el = new Element();
            el.e = "C";
            makesphere(origin, 0.9f, el);
            edrawn[0] = true;
            drawcarbon(0, origin);

            
        }
        void drawcarbon(int pos, coor3d ccentre)
        {      float radtheta = (float)makerad(109.5);
                float rad120 = (float)makerad(120);
         
            int mbsize = elements[pos].maxbonds - (elements[pos].atomsbonded) + 1;
            if (mbsize == 1)
            {


                coor3d c2, c3, c4, c5 = new coor3d();
                Matrix4x4 rotz, roty = new Matrix4x4();
               
                rotz = (Matrix4x4)rotatex(radtheta);
                roty = (Matrix4x4)rotatey(radtheta);
                c2 = (coor3d)makecoor(3 * MathF.Sin(109.5f * MathF.PI / 180), 0, 3 * MathF.Cos(109.5f * MathF.PI / 180)); //fisrt point but add sidelength in each direction
                c3 = (coor3d)makecoor(3 * MathF.Sin(109.5f * MathF.PI / 180) * MathF.Cos(0.6666667f * MathF.PI), 3 * MathF.Sin(109.5f * MathF.PI / 180) * MathF.Sin(0.66666667f * MathF.PI), 3 * MathF.Cos(109.5f * MathF.PI / 180));
                c4 = (coor3d)makecoor(3 * MathF.Sin(109.5f * MathF.PI / 180) * MathF.Cos(1.3333333f * MathF.PI), 3 * MathF.Sin(109.5f * MathF.PI / 180) * MathF.Sin(1.333333f * MathF.PI), 3 * MathF.Cos(109.5f * MathF.PI / 180));
                c5 = (coor3d)makecoor(0, 0, 3);  //calculate other centres
                coor3d[] c = new coor3d[4];
                c[0] = c2;
                c[1] = c3;
                c[2] = c4;
                c[3] = c5;

                for (int i = 0; i < adjacencylist[pos].Count; i++)
                {

                    if (elements[adjacencylist[pos][i]].e == "C" && edrawn[adjacencylist[pos][i]] == false)
                    {
                        float r = 1;
                        if (elements[adjacencylist[pos][i]].e == "H")
                        {
                            r = 120 / 170;

                        }
                        else if (elements[adjacencylist[pos][i]].e == "O")
                        {
                            r = 152 / 170;

                        }
                        makesphere((coor3d)addcoor(c[i], ccentre), r, elements[adjacencylist[pos][i]]);
                        edrawn[adjacencylist[pos][i]] = true;
                        connectspheres((coor3d)addcoor(c[i], ccentre), ccentre, 1, elements[adjacencylist[pos][i]]);
                       
                        drawcarbon(adjacencylist[pos][i], (coor3d)addcoor(c[i], ccentre));

                    }
                    else if (edrawn[adjacencylist[pos][i]] == false)
                    {

                        float r = 1;
                        if (elements[adjacencylist[pos][i]].e == "H")
                        {
                            r = 120 / 170;

                        }
                        else if (elements[adjacencylist[pos][i]].e == "O")
                        {
                            r = 152 / 170;

                        }
                        makesphere((coor3d)addcoor(c[i], ccentre), r, elements[adjacencylist[pos][i]]);
                        // connectspheres(ccentre, (coor3d)addcoor(c[i], ccentre));
                        edrawn[adjacencylist[pos][i]] = true;
                        for (int j = 0; j < bonds.Count; j++) //for every bond
                        {
                            bond b1 = bonds[j];

                            if ((b1.enum1 == pos && b1.enum2 == adjacencylist[pos][i] || b1.enum1 == adjacencylist[pos][i] && b1.enum2 == pos) && bdrawn[j] == false) //if bond exists
                            {
                                bdrawn[j] = true;
                                connectspheres(ccentre, (coor3d)addcoor(c[i], ccentre), 1, elements[adjacencylist[pos][i]]);

                            }
                        }
                    }

                }
            }
            else if (mbsize == 2) 
            { row = row* -1;
                if (elements[pos].atomsbonded == 3) //only one double bond
                {
                    float rad240 = (float)makerad(240);  //trigonal planar
                    coor3d e1, e2, e3, e4 = new coor3d();
                    int l = 3;
                    e1 = ccentre;
                    e2 = (coor3d)makecoor(l * MathF.Cos(rad120), l * MathF.Sin(rad120), 0);
                   
                    e3 = (coor3d)makecoor(l * MathF.Cos(rad240), l * MathF.Sin(rad240), 0);
                    e4 = (coor3d)makecoor(l, 0, 0);
                    coor3d[] c = new coor3d[3];
                    c[0] = e2;
                    c[1] = e3;
                    c[2] = e4;
                    for (int i = 0; i < 3; i++)
                    {
                        c[i] = (coor3d)multscalar(c[i], row); //invert it for each side of double bond
                    }

                    { }
                    for (int i = 0; i < adjacencylist[pos].Count; i++)
                    {
                        if (elements[adjacencylist[pos][i]].e == "C" && edrawn[adjacencylist[pos][i]] == false)
                        {
                            float r = 1;
                            if (elements[adjacencylist[pos][i]].e == "H")
                            {
                                r = (float)120 / 170;

                            }
                            else if (elements[adjacencylist[pos][i]].e == "O")
                            {
                                r = (float)152 / 170;

                            }
                            makesphere((coor3d)addcoor(c[i], ccentre), r, elements[adjacencylist[pos][i]]);
                            edrawn[adjacencylist[pos][i]] = true;
                            for (int j = 0; j < bonds.Count; j++) //for every bond
                            {
                                bond b1 = bonds[j];

                                if ((b1.enum1 == pos && b1.enum2 == adjacencylist[pos][i] || b1.enum1 == adjacencylist[pos][i] && b1.enum2 == pos) && bdrawn[j] == false) //if bond exists
                                {
                                    bdrawn[j] = true;
                                    connectspheres(ccentre, (coor3d)addcoor(c[i], ccentre), b1.size, elements[adjacencylist[pos][i]]);

                                }
                            }
                            //connectspheres((coor3d)addcoor(c[i], ccentre), ccentre);
                            reposition();
                            drawcarbon(adjacencylist[pos][i], (coor3d)addcoor(c[i], ccentre));

                        }
                        else if (edrawn[adjacencylist[pos][i]] == false)
                        {

                            float r = 1;
                            if (elements[adjacencylist[pos][i]].e == "H")
                            {
                                r = (float)120 / 170;

                            }
                            else if (elements[adjacencylist[pos][i]].e == "O")
                            {
                                r = (float)152 / 170;

                            }
                            makesphere((coor3d)addcoor(c[i], ccentre), r, elements[adjacencylist[pos][i]]);
                            // connectspheres(ccentre, (coor3d)addcoor(c[i], ccentre));
                            edrawn[adjacencylist[pos][i]] = true;
                            for (int j = 0; j < bonds.Count; j++) //for every bond
                            {
                                bond b1 = bonds[j];

                                if ((b1.enum1 == pos && b1.enum2 == adjacencylist[pos][i] || b1.enum1 == adjacencylist[pos][i] && b1.enum2 == pos) && bdrawn[j] == false) //if bond exists
                                {
                                    bdrawn[j] = true;
                                    connectspheres(ccentre, (coor3d)addcoor(c[i], ccentre), b1.size, elements[adjacencylist[pos][i]]);

                                }
                            }
                        }
                       
                    }

                    //makesphere(e1, 1);
                    //makesphere(e2, 1);
                    //makesphere(e3, 1);
                    //makesphere(e4, 1);

                }
                  
            }
            sort_mesh();
        }
        public object maketetrahedral(int listplace, coor3d centre)
        {
            Element e = new Element();
            coor3d c2, c3, c4, c5 = new coor3d();

            Matrix4x4 rotz, roty = new Matrix4x4();
            float radtheta = (float)makerad(109.5);
            float rad120 = (float)makerad(120);
            rotz = (Matrix4x4)rotatex(radtheta);
            roty = (Matrix4x4)rotatey(radtheta);




            c2 = (coor3d)makecoor(3 * MathF.Sin(109.5f * MathF.PI / 180), 0, 3 * MathF.Cos(109.5f * MathF.PI / 180)); //fisrt point but add sidelength in each direction
            c3 = (coor3d)makecoor(3 * MathF.Sin(109.5f * MathF.PI / 180) * MathF.Cos(0.6666667f * MathF.PI), 3 * MathF.Sin(109.5f * MathF.PI / 180) * MathF.Sin(0.66666667f * MathF.PI), 3 * MathF.Cos(109.5f * MathF.PI / 180));
            c4 = (coor3d)makecoor(3 * MathF.Sin(109.5f * MathF.PI / 180) * MathF.Cos(1.3333333f * MathF.PI), 3 * MathF.Sin(109.5f * MathF.PI / 180) * MathF.Sin(1.333333f * MathF.PI), 3 * MathF.Cos(109.5f * MathF.PI / 180));
            c5 = (coor3d)makecoor(0, 0, 3);


            c2 = (coor3d)addcoor(c2, centre);
            c3 = (coor3d)addcoor(c3, centre);
            c4 = (coor3d)addcoor(c4, centre);
            c5 = (coor3d)addcoor(c5, centre);
                connectspheres(centre, c3, 1, e);
            connectspheres(centre, c2, 1, e);
            connectspheres(centre, c4, 1, e);
            connectspheres(centre, c5, 1, e);
            makesphere(centre, 0.5f, e);
            makesphere(c2, 0.5f, e);
            makesphere(c3, 0.5f, e);
            makesphere(c4, 0.5f, e      );
            makesphere(c5, 0.5f, e);
            coor3d newcentre = origin;

            for (int i = 0; i < 4; i++)
            {
                Element w = elements[adjacencylist[listplace][i]];
                if (w.e == "C" & edrawn[adjacencylist[listplace][i]] == false) //gets centre for carbon next in chain
                {
                    if (i == 0)
                    { newcentre = c2; }
                    else if (i == 1)
                    { newcentre = c3; }
                    else if (i == 2)
                    { newcentre = c4; }
                    else if (i == 3)
                    { newcentre = c5; }
                    else { newcentre = origin; }
                }
            }


            return newcentre;
        }
        void connectspheres(coor3d c1, coor3d c2,int bsize, Element e)
        {
            float r = 1;
            if (e.e == "H")
            {
                r = 120 / 170;

            }
            else if (e.e == "O")
            {
                r = 152 / 170;
            
            }
            if (bsize == 1)
            {
                makecylinder(0.25f, c1, c2);
            }
            else if (bsize == 2)
            {
                coor3d c11 = c1;
                coor3d c12 = c1;
                c11.y += 0.5f;
                c12.y -= 0.5f;

                coor3d c21 = c2;
                coor3d c22 = c2;
                c21.y += 0.5f;
                c22.y -= 0.5f;
                makecylinder(0.2f, c11, c21);
                makecylinder(0.2f, c12, c22);
            }

        }
        void sort_mesh()
        {
            //triangle c, n, temp = new triangle();
            //for (int i = 0; i < tempmesh.Count - 1; i++)
            //{
            //    c = tempmesh[i];
            //    n = tempmesh[i + 1];
            //    if (c.v1.z > n.v1.z)
            //    {
            //        temp = n;
            //        n = c;
            //        c = temp;
            //        tempmesh[i] = c;
            //        tempmesh[i + 1] = n;
            //    }

            //}


        }
        void reposition()
        {
            float a = (float)panel1.Height / (float)panel1.Width;
            int angle = 90;
            float fnear = 0.1f;
            float ffar = 1000.0f;
            float f = ((float)(1 / (Math.Tan(((angle * 0.5) / 180) * 3.142))));

            Matrix4x4 proj = new Matrix4x4();

            proj.M11 = a * f;  //projection matrix
            proj.M22 = f;
            proj.M33 = ffar / (ffar / fnear);
            proj.M43 = (-ffar * fnear) / (ffar - fnear);
            proj.M34 = 1;
            proj.M44 = 0;

            Matrix4x4 rotX = new Matrix4x4();
            rotX = (Matrix4x4)rotatex(thetax);

            Matrix4x4 rotZ = new Matrix4x4();
            rotZ = (Matrix4x4)rotatey(thetay);

            Matrix4x4 rotY = new Matrix4x4();
            rotY = (Matrix4x4)rotatey(thetay);
            triangle triprojected, trirot = new triangle();
            List<triangle> tempmesh = new List<triangle>();

            for (int i = 0; i < mesh.Count; i++)
            {
                if (i == 0)
                { panel1.Refresh(); }

                trirot = mesh[i];
                trirot.v1 = (coor3d)matrixmult(rotY, mesh[i].v1);
                trirot.v2 = (coor3d)matrixmult(rotY, mesh[i].v2);
                trirot.v3 = (coor3d)matrixmult(rotY, mesh[i].v3);


                trirot.v1 = (coor3d)matrixmult(rotX, trirot.v1);
                trirot.v2 = (coor3d)matrixmult(rotX, trirot.v2);
                trirot.v3 = (coor3d)matrixmult(rotX, trirot.v3);

                trirot.v1.x += center.x;
                trirot.v1.y += center.y;
                trirot.v1.z += center.z;
                trirot.v2.x += center.x;
                trirot.v2.y += center.y;
                trirot.v2.z += center.z;
                trirot.v3.x += center.x;
                trirot.v3.y += center.y;
                trirot.v3.z += center.z;


                trirot.v1.z = trirot.v1.z + distance;
                trirot.v2.z = trirot.v2.z + distance;
                trirot.v3.z = trirot.v3.z + distance;

                coor3d normal, line1, line2 = new coor3d();
                line1.x = trirot.v2.x - trirot.v1.x;
                line1.y = trirot.v2.y - trirot.v1.y;
                line1.z = trirot.v2.z - trirot.v1.z;
                line2.x = trirot.v3.x - trirot.v1.x;
                line2.y = trirot.v3.y - trirot.v1.y;
                line2.z = trirot.v3.z - trirot.v1.z;
                normal.x = line1.y * line2.z - line1.z * line2.y;
                normal.y = line1.z * line2.x - line1.x * line2.z;
                normal.z = line1.x * line2.y - line1.y * line2.x;


                float l = (float)Math.Sqrt(normal.x * normal.x + normal.y * normal.y + normal.z * normal.z);
                int blue = (int)(normal.z * 5);
                normal.x /= l;
                normal.y /= l;
                normal.z /= l; //unit vectors
                coor3d asda = new coor3d();
                asda.x = normal.x;
                asda.y = normal.y;
                asda.z = normal.z;
                //if (normal.z < 0.0f)

                if (normal.x * (trirot.v1.x - vcamera.x) + normal.y * (trirot.v1.y - vcamera.y) + normal.z * (trirot.v1.z - vcamera.z) < 0)
                {
                    tempmesh.Add(trirot);
                    //illumination
                    coor3d lightdirec = new coor3d();
                    lightdirec = (coor3d)makecoor(0, 0, -1);
                    float k = (float)Math.Sqrt(lightdirec.x * lightdirec.x + lightdirec.y * lightdirec.y + lightdirec.z * lightdirec.z);

                    lightdirec.x /= k;
                    lightdirec.y /= k;
                    lightdirec.z /= k;
                    float dp = (float)dotproduct(lightdirec, asda);

                    triprojected.v1 = (coor3d)matrixmult(proj, trirot.v1);
                    triprojected.v2 = (coor3d)matrixmult(proj, trirot.v2);
                    triprojected.v3 = (coor3d)matrixmult(proj, trirot.v3);

                    //scale into view

                    triprojected.v1.x += 1; triprojected.v1.y += 1;
                    triprojected.v2.x += 1; triprojected.v2.y += 1;
                    triprojected.v3.x += 1; triprojected.v3.y += 1;

                    triprojected.v1.x *= 0.5f * (float)panel1.Width;
                    triprojected.v1.y *= 0.5f * (float)panel1.Height;
                    triprojected.v2.x *= 0.5f * (float)panel1.Width;
                    triprojected.v2.y *= 0.5f * (float)panel1.Height;
                    triprojected.v3.x *= 0.5f * (float)panel1.Width;
                    triprojected.v3.y *= 0.5f * (float)panel1.Height;


                    float b = dp * 75;
                    { }

                    drawtri(new Point((int)triprojected.v1.x, (int)triprojected.v1.y), new Point((int)triprojected.v2.x, (int)triprojected.v2.y), new Point((int)triprojected.v3.x, (int)triprojected.v3.y), (int)b, mesh[i]);

                }
            }

            //triangle c, n, temp = new triangle();
            //for (int i = 0; i < tempmesh.Count - 1; i++)
            //{
            //    c = tempmesh[i];
            //    n = tempmesh[i + 1];
            //    if (c.v1.z < n.v1.z)
            //    {
            //        temp = n;
            //        n = c;
            //        c = temp;
            //        tempmesh[i] = c;
            //        tempmesh[i + 1] = n;
            //    }

            //}
            for (int i = tempmesh.Count - 1; i > 0; i--)
            {
                coor3d normal, line1, line2 = new coor3d();
                trirot = tempmesh[i];
                line1.x = trirot.v2.x - trirot.v1.x;
                line1.y = trirot.v2.y - trirot.v1.y;
                line1.z = trirot.v2.z - trirot.v1.z;
                line2.x = trirot.v3.x - trirot.v1.x;
                line2.y = trirot.v3.y - trirot.v1.y;
                line2.z = trirot.v3.z - trirot.v1.z;
                normal.x = line1.y * line2.z - line1.z * line2.y;
                normal.y = line1.z * line2.x - line1.x * line2.z;
                normal.z = line1.x * line2.y - line1.y * line2.x;


                float l = (float)Math.Sqrt(normal.x * normal.x + normal.y * normal.y + normal.z * normal.z);
                int blue = (int)(normal.z * 5);
                normal.x /= l;
                normal.y /= l;
                normal.z /= l; //unit vectors
                coor3d asda = new coor3d();
                asda.x = normal.x;
                asda.y = normal.y;
                asda.z = normal.z;
                coor3d lightdirec = new coor3d();
                lightdirec = (coor3d)makecoor(0, 0, -1);

                float dp = (float)dotproduct(lightdirec, asda);

                triprojected = tempmesh[i];

                triprojected.v1 = (coor3d)matrixmult(proj, trirot.v1);
                triprojected.v2 = (coor3d)matrixmult(proj, trirot.v2);
                triprojected.v3 = (coor3d)matrixmult(proj, trirot.v3);

                //scale into view

                triprojected.v1.x += 1; triprojected.v1.y += 1;
                triprojected.v2.x += 1; triprojected.v2.y += 1;
                triprojected.v3.x += 1; triprojected.v3.y += 1;

                triprojected.v1.x *= 0.5f * (float)panel1.Width;
                triprojected.v1.y *= 0.5f * (float)panel1.Height;
                triprojected.v2.x *= 0.5f * (float)panel1.Width;
                triprojected.v2.y *= 0.5f * (float)panel1.Height;
                triprojected.v3.x *= 0.5f * (float)panel1.Width;
                triprojected.v3.y *= 0.5f * (float)panel1.Height;


                float b = dp * 75;

                drawtri(new Point((int)triprojected.v1.x, (int)triprojected.v1.y), new Point((int)triprojected.v2.x, (int)triprojected.v2.y), new Point((int)triprojected.v3.x, (int)triprojected.v3.y), (int)b, tempmesh[i]);
            }

            tempmesh.Clear();
        }
        void drawtri(Point p1, Point p2, Point p3, int blue, triangle colour)
        {
            Graphics g = panel1.CreateGraphics();
            
            Color clr = Color.FromArgb(75 + blue+colour.r, 75 + blue + colour.g, 75 + blue + colour.b);
         
            SolidBrush b = new SolidBrush(clr);
            Pen p = new Pen(Color.White, 1);
            //g.DrawLine(p, p1, p2);
            //g.DrawLine(p, p1, p3);
            //g.DrawLine(p, p2, p3);
            Point[] points = { p1, p2, p3 };
            g.FillPolygon(b, points);
        }
        void makesphere(coor3d centre, float r, Element e)
        {
            int g = 0;
            int b = 0;
            int red = 0;  //colours are +115
            if(e.e =="C")
            {
                g = 25;
                b = 25;
                red = 25;

            }
            else if (e.e == "O")
            {

                g = 0;
                b = 0;
                red = 105;

            }
            else if (e.e == "H")
            {

                g = 105;
                b = 105;
                red = 105;

            }
            float latparts = 9;
            float longparts = 9;
            List<coor3d> vertices = new List<coor3d>();
            for (int i = 0; i <= latparts; i++)  //go up vertical circle then every point along vertical cirlce
            {
                float angle = (float)(i * (Math.PI / latparts));
                for (int j = 0; j <= longparts; j++)
                {
                    float longangle = (float)(j * 2 * (Math.PI / longparts)); //2 pi since going round whole circle not half
                    coor3d v = new coor3d();
                    v.x = r * MathF.Sin(angle) * MathF.Cos(longangle) + centre.x;
                    v.y = r * MathF.Cos(angle) + centre.y;
                    v.z = r * MathF.Sin(angle) * MathF.Sin(longangle) + centre.z;
                    vertices.Add(v);
                }
            }
            // make triangles
            for (int i = 0; i < latparts; i++)
            {
                for (int j = 0; j < longparts; j++)
                {
                    int v1 = i * ((int)longparts + 1) + j;
                    int v2 = v1 + (int)longparts + 1;
                    triangle t1, t2 = new triangle();

                    t1.v1 = vertices[v1]; t1.v2 = vertices[v2]; t1.v3 = vertices[v1 + 1];
                    t2.v1 = vertices[v2]; t2.v2 = vertices[v2 + 1]; t2.v3 = vertices[v1 + 1];
                    t1.r = red; t1.g = g; t1.b = b;
                    t2.r = red; t2.g = g; t2.b = b;
                    mesh.Add(t1);
                    mesh.Add(t2);
                }
            }
        }
        void makecylinder(float r, coor3d s, coor3d e)
        {
            int g = 105;
            int b = 1;
            int red = 1;
           
            coor3d c1, dv, a, initialdv = new coor3d();
            float dp, a1, a2, shift1, shift2;
            int biggest;
            float hstraight;
            
            if (s.x > e.x)
            {

                biggest = -1;

            }
            else { biggest = 1; }

            hstraight = (float)getsize((coor3d)subcoor(e, s));
            shift1 = s.z;
            shift2 = e.z;
            s.z = 0;
            e.z = 0;
            initialdv = (coor3d)subcoor(e, s);

            dv = (coor3d)subcoor(e, s);
            dv.z = 0;
          
            a = (coor3d)makecoor(0, 0, 1);
            float h = (float)getsize(dv);
            dv = (coor3d)normalise(dv);
            dp = (float)dotproduct(a, dv);

            a1 = MathF.Acos(dp / ((float)getsize(a) * (float)getsize(dv)));


            a = (coor3d)makecoor(0, 1, 0);
            dp = (float)dotproduct(a, dv);
            a2 = MathF.Acos(dp / ((float)getsize(a) * (float)getsize(dv)));

            int sections = 16;
            int hsections = 1; //squares going upwards
            List<coor3d> points = new List<coor3d>();
            float theta = 0;
            Matrix4x4 rotZ = new Matrix4x4();
            rotZ = (Matrix4x4)rotatex(MathF.PI);

            if (initialdv.x == 0 & initialdv.y == 0 && (s.x == e.x || s.y == e.y)) //if up in starigh line
            {
                       //if ((float)getsize(e) == 0)
                       //{
                       //       c1 = s;
                       //         s = e;
                       //          e = c1;
                   
                       //}
                for (int i = 0; i <= sections; i++)
                {
                    theta = (4 * MathF.PI * i) / sections;
                    s.z = shift1; e.z = shift2;
                    h = hstraight;
                    for (int j = 0; j <= hsections; j++)
                    {
                        coor3d p = new coor3d();
                        p.x = r * MathF.Cos(theta) + s.x;
                        p.y = r * MathF.Sin(theta) + s.y;
                        p.z = (h) * j;
                        if (j == 0)
                        {
                            p.z = shift1;

                        }
                        else if (j == 1) { p.z = shift2; }

                        points.Add(p);
                    }
                }

            }
            else

            {
                for (int i = 0; i <= sections; i++)
                {
                    theta = (4 * MathF.PI * i) / sections;

                    for (int j = 0; j <= hsections; j++)
                    {
                        coor3d p = new coor3d();
                        p.x = r * MathF.Cos(theta);
                        p.y = r * MathF.Sin(theta);
                        p.z = (h) * j;
                        Matrix4x4 z = (Matrix4x4)rotatex(-a1);
                        p = (coor3d)matrixmult(z, p);
                        Matrix4x4 z2 = (Matrix4x4)rotatez(-a2 * biggest);
                        p = (coor3d)matrixmult(z2, p);
                        p.x += s.x;
                        p.y += s.y;
                        if (j == 0)
                        {
                            p.z += shift1;

                        }
                        else if (j == 1) { p.z += shift2; }
                        points.Add(p);
                    }
                }

            }
            for (int i = 0; i < sections; i++)
            {
                for (int j = 0; j < hsections; j++)
                {
                    int v1 = i * ((int)hsections + 1) + j;
                    int v2 = v1 + (int)hsections + 1;
                    triangle t1, t2 = new triangle();

                    t1.v1 = points[v1]; t1.v2 = points[v2]; t1.v3 = points[v1 + 1];
                    t2.v1 = points[v2]; t2.v2 = points[v2 + 1]; t2.v3 = points[v1 + 1];
                    t1.r = red; t1.g = g; t1.b = b;
                    t2.r = red; t2.g = g; t2.b = b;
                    mesh.Add(t1);
                    mesh.Add(t2);

                }
              
               
            }
            count++;
        }
        void makefirstsquare()
        {
            triangle t1 = new triangle();
            coor3d w = new coor3d();
            w = (coor3d)makecoor(0, 0, 0);
            vcamera = w;
            t1.v1 = (coor3d)makecoor(0, 0, 0);
            t1.v2 = (coor3d)makecoor(0, 1, 0);
            t1.v3 = (coor3d)makecoor(1, 1, 0);
            mesh.Add(t1);
            t1.v1 = (coor3d)makecoor(0, 0, 0);
            t1.v2 = (coor3d)makecoor(1, 1, 0);
            t1.v3 = (coor3d)makecoor(1, 0, 0);
            mesh.Add(t1);
            //east
            t1.v1 = (coor3d)makecoor(1, 0, 0);
            t1.v2 = (coor3d)makecoor(1, 1, 0);
            t1.v3 = (coor3d)makecoor(1, 1, 1);
            mesh.Add(t1);
            t1.v1 = (coor3d)makecoor(1, 0, 0);
            t1.v2 = (coor3d)makecoor(1, 1, 1);
            t1.v3 = (coor3d)makecoor(1, 0, 1);
            mesh.Add(t1);
            //north
            t1.v1 = (coor3d)makecoor(1, 0, 1);
            t1.v2 = (coor3d)makecoor(1, 1, 1);
            t1.v3 = (coor3d)makecoor(0, 1, 1);
            mesh.Add(t1);
            t1.v1 = (coor3d)makecoor(1, 0, 1);
            t1.v2 = (coor3d)makecoor(0, 1, 1);
            t1.v3 = (coor3d)makecoor(0, 0, 1);
            mesh.Add(t1);
            //west
            t1.v1 = (coor3d)makecoor(0, 0, 1);
            t1.v2 = (coor3d)makecoor(0, 1, 1);
            t1.v3 = (coor3d)makecoor(0, 1, 0);
            mesh.Add(t1);
            t1.v1 = (coor3d)makecoor(0, 0, 1);
            t1.v2 = (coor3d)makecoor(0, 1, 0);
            t1.v3 = (coor3d)makecoor(0, 0, 0);
            mesh.Add(t1);
            //top
            t1.v1 = (coor3d)makecoor(0, 1, 0);
            t1.v2 = (coor3d)makecoor(0, 1, 1);
            t1.v3 = (coor3d)makecoor(1, 1, 1);
            mesh.Add(t1);
            t1.v1 = (coor3d)makecoor(0, 1, 0);
            t1.v2 = (coor3d)makecoor(1, 1, 1);
            t1.v3 = (coor3d)makecoor(1, 1, 0);
            mesh.Add(t1);
            //bottom
            t1.v1 = (coor3d)makecoor(1, 0, 1);
            t1.v2 = (coor3d)makecoor(0, 0, 1);
            t1.v3 = (coor3d)makecoor(0, 0, 0);
            mesh.Add(t1);
            t1.v1 = (coor3d)makecoor(1, 0, 1);
            t1.v2 = (coor3d)makecoor(0, 0, 0);
            t1.v3 = (coor3d)makecoor(1, 0, 0);
            mesh.Add(t1);
            for (int i = 0; i < 12; i++)
            {
                triangle tri = mesh[i];
                center.x += (tri.v1.x + tri.v2.x + tri.v3.x) / 3.0f;
                center.y += (tri.v1.y + tri.v2.y + tri.v3.y) / 3.0f;
                center.z += (tri.v1.z + tri.v2.z + tri.v3.z) / 3.0f;
            }
            center.x /= mesh.Count;
            center.y /= mesh.Count;
            center.z /= mesh.Count;

            for (int i = 0; i < 12; i++)
            {
                triangle t = mesh[i];
                t.v1.x -= center.x;
                t.v1.y -= center.y;
                t.v1.z -= center.z;
                t.v2.x -= center.x;
                t.v2.y -= center.y;
                t.v2.z -= center.z;
                t.v3.x -= center.x;
                t.v3.y -= center.y;
                t.v3.z -= center.z;
                mesh[i] = t;

            }
            reposition();

        }
        private void cube_Click(object sender, EventArgs e)
        {
            mesh.Clear();
            makefirstsquare();
            reposition();
        }
        private void sphere_Click(object sender, EventArgs e)
        {
           
            
            coor3d c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11 = new coor3d();
            c1 = (coor3d)makecoor(0, 0, 0);
            c2 = (coor3d)makecoor(5, 0, 0);
            c3 = (coor3d)makecoor(-5, 0, 0);
            c4 = (coor3d)makecoor(0, 5, 0);
            c5 = (coor3d)makecoor(0, -5, 0);
            c6 = (coor3d)makecoor(5, -5, 0);
            c7 = (coor3d)makecoor(5, 5, 0);
            c8 = (coor3d)makecoor(-5, -5, 0);
            c9 = (coor3d)makecoor(-5, 5, 0);
            c10 = (coor3d)makecoor(-10, 0, 0);
            c11 = (coor3d)makecoor(10, 0, 0);
          //  makesphere(c1, 1);
            //makesphere(c2, 1);
            //makesphere(c3, 1);
            //makesphere(c4, 0.7f);
            //makesphere(c5, 0.7f);
            //makesphere(c6, 0.7f);
            //makesphere(c7, 0.7f);
            //makesphere(c8, 0.7f);
            //makesphere(c9, 0.7f);
            //makesphere(c10, 0.7f);
            //makesphere(c11, 0.7f);
        }

        private void cylinder_Click(object sender, EventArgs e)
        {
            coor3d end = (coor3d)makecoor(1, 2, 3);
            coor3d start = (coor3d)makecoor(3, 6, 4);
            //makesphere(end, 1);
            //makesphere(start, 1);
            makecylinder(0.5f, start, end);


        }
        public object addcoor(coor3d in1, coor3d in2)    //add
        {
            coor3d output = new coor3d();
            output.x = in1.x + in2.x;
            output.y = in1.y + in2.y;
            output.z = in1.z + in2.z;
            return output;
        }
        public object crossproduct(coor3d in1, coor3d in2)
        {
            coor3d output = new coor3d();
            output.x = in1.y * in2.z - in1.z * in2.y;
            output.y = in1.z * in2.x - in1.x * in2.z;
            output.z = in1.x * in2.y - in1.y * in2.x;
            return output;
        }
        public object normalise(coor3d in1)
        {
            coor3d output = new coor3d();
            float length = (float)getsize(in1);
            output.x = in1.x / length; output.y = in1.y / length; output.z = in1.z / length;
            return output;
        }
        object matrixmult(Matrix4x4 factor, coor3d input)
        {
            coor3d output = new coor3d();
            output.x = input.x * factor.M11 + input.y * factor.M21 + input.z * factor.M31 + factor.M41;
            output.y = input.x * factor.M12 + input.y * factor.M22 + input.z * factor.M32 + factor.M42;
            output.z = input.x * factor.M13 + input.y * factor.M23 + input.z * factor.M33 + factor.M43;
            float w = input.x * factor.M14 + input.y * factor.M24 + input.z * factor.M34 + factor.M44;

            if (w != 0)
            {
                output.x = output.x / w;
                output.y = output.y / w;
                output.z = output.z / w;
            }
            return output;
        }
        public object dotproduct(coor3d in1, coor3d in2)   //calculate dot product
        {
            float output;
            output = in1.x * in2.x + in1.y * in2.y + in1.z * in2.z;
            return output;
        }
        private object rotatez(float theta)
        {
            //theta = (float)((theta / 180) * 3.142);
            //Matrix4x4 rotZ = new Matrix4x4();
            //rotZ.M11 = MathF.Cos(theta *0.5f);
            //rotZ.M12 = MathF.Sin(theta * 0.5f);
            //rotZ.M21 = -MathF.Sin(theta * 0.5f);
            //rotZ.M22 = MathF.Cos(theta * 0.5f);
            //rotZ.M33 = 1;
            //rotZ.M44 = 1f;
            //return rotZ;
            Matrix4x4 rotZ = new Matrix4x4();
            rotZ.M11 = MathF.Cos(theta);
            rotZ.M12 = MathF.Sin(theta);
            rotZ.M21 = -MathF.Sin(theta);
            rotZ.M22 = MathF.Cos(theta);
            rotZ.M33 = 1;
            rotZ.M44 = 1f;
            return rotZ;
        }
        private object makerad(double degrees)
        {
            float theta = (float)((degrees / 180) * MathF.PI);
            return theta;
        }
        private object rotatex(float theta)
        {
            //theta = (float)((theta / 180) * 3.142);
            //Matrix4x4 rotX = new Matrix4x4();
            //rotX.M11 = 1;
            //rotX.M22 = MathF.Cos(theta * 0.5f);
            //rotX.M23 = MathF.Sin(theta * 0.5f);
            //rotX.M32 = -MathF.Sin(theta * 0.5f);
            //rotX.M33 = MathF.Cos(theta * 0.5f);
            //rotX.M44 = 1f;
            //return rotX;
            Matrix4x4 rotX = new Matrix4x4();
            rotX.M11 = 1;
            rotX.M22 = MathF.Cos(theta);
            rotX.M23 = MathF.Sin(theta);
            rotX.M32 = -MathF.Sin(theta);
            rotX.M33 = MathF.Cos(theta);
            rotX.M44 = 1f;
            return rotX;
        }
        private object rotatey(float theta)
        {
            //theta = (float)((theta / 180) * 3.142);
            //Matrix4x4 rotZ = new Matrix4x4();
            //rotZ.M11 = MathF.Cos(theta * 0.5f);
            //rotZ.M12 = MathF.Sin(theta * 0.5f);
            //rotZ.M21 = -MathF.Sin(theta * 0.5f);
            //rotZ.M22 = MathF.Cos(theta * 0.5f);
            //rotZ.M33 = 1;
            //rotZ.M44 = 1f;
            //return rotZ;
            Matrix4x4 rotZ = new Matrix4x4();
            rotZ.M11 = MathF.Cos(theta);
            rotZ.M12 = MathF.Sin(theta);
            rotZ.M21 = -MathF.Sin(theta);
            rotZ.M22 = MathF.Cos(theta);
            rotZ.M33 = 1;
            rotZ.M44 = 1f;
            return rotZ;
        }
        public object makecoor(float a, float b, float c)
        {
            coor3d p = new coor3d();
            p.x = a;
            p.y = b;
            p.z = c;
            return p;
        }
        void panel_scroll(object sender, MouseEventArgs e)
        {
            label1.Text = distance.ToString();
            if ((distance + -e.Delta / 100) > 0)
            {
                distance += -e.Delta / 100;
            }
            reposition();
        }
        void panel_mousemove(object sender, MouseEventArgs e)
        {
            if (isdown)
            {

                thetay += (float)(e.X - oldX) / 50; //wrong way round makes it work
                thetax += (float)(e.Y - oldY) / 50;
                oldX = e.X;
                oldY = e.Y;
                label1.Text = (thetay.ToString());
                reposition();
            }
        }
        void panel_mousedown(object sender, MouseEventArgs e)
        {
            isdown = true;
            oldX = e.X;
            oldY = e.Y;
        }
        void panel_mouseup(object sender, MouseEventArgs e)
        {
            isdown = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void getdata_Click(object sender, EventArgs e)
        {
             makemoleculeshapes();
            //coor3d a, b, c, d = new coor3d();
            //a = (coor3d)makecoor(0, 0, 3);
            //makesphere(origin, 1);
            //makesphere(a, 1);
            //connectspheres(a, origin);
        }
        public object getsize(coor3d in1)
        {
            float output = MathF.Sqrt(in1.x * in1.x + in1.y * in1.y + in1.z * in1.z);
            return output;
        }
        public object subcoor(coor3d in1, coor3d in2)
        {
            coor3d output = new coor3d();
            output.x = in1.x - in2.x;
            output.y = in1.y - in2.y;
            output.z = in1.z - in2.z;
            return output;
        }
        public object multscalar(coor3d in1, float scalar)  //multiply by scalar
        {
            coor3d output = new coor3d();
            output.x = in1.x * scalar;
            output.y = in1.y * scalar;
            output.z = in1.z * scalar;
            return output;
        }

    }
    public struct coor3d
    {
        public float x;
        public float y;
        public float z;
        public float w;
    }
    public struct triangle
    {

        public coor3d v1;
        public coor3d v2;
        public coor3d v3;
        
        public int r;
        public int g;
        public int b;

        //= new coor3d[2]; //array
    }
    //private void Form2_Load(object sender, EventArgs e)
    //   {

    //   }

}


using System.Diagnostics.Eventing.Reader;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;

namespace Prototype_3
{
    public partial class Form1 : Form
    {
        private Form2 secondForm;
        List<Button> commandbuttons = new List<Button>();
        List<Label> labels = new List<Label>();
        public static  List<List<int>> adjacencylist = new List<List<int>>();
        public static List<Element> elements = new List<Element>();
        public static  List<bond> bonds = new List<bond>();
        int total = -1;
        string command = ""; // what to do with s1 and s2
        int oldX, oldY = 0;
        bool isdown = false;
        int totalbonds = -1;
        int s1, s2, snum = 0; //need to figure out how to set as null for resetting
        public Form1()
        {
            InitializeComponent();
            commandbuttons.Add(bond);
            commandbuttons.Add(doublebond);
            commandbuttons.Add(triplebond);
            commandbuttons.Add(deleteelement);
            commandbuttons.Add(deletebonds);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void carbon_Click(object sender, EventArgs e)
        {
            createAtom("C");
            Element element2 = new Element();
            element2.e = "C";
            element2.maxbonds = 4;
            element2.bondsformed = 0;
            element2.lonepairs = 0;
            element2.number = total + 1;
            element2.atomsbonded = 0;
            elements.Add(element2);
        }
        private void oxygen_Click(object sender, EventArgs e)
        {
            createAtom("O");
            Element element2 = new Element();
            element2.e = "O";
            element2.maxbonds = 2;
            element2.bondsformed = 0;
            element2.lonepairs = 2;
            element2.number = total + 1;
            element2.atomsbonded = 0;
            elements.Add(element2);
        }
        private void hydrogen_Click(object sender, EventArgs e)
        {
            createAtom("H");
            Element element1 = new Element();
            element1.e = "H";
            element1.maxbonds = 1;
            element1.bondsformed = 0;
            element1.number = total + 1;
            element1.atomsbonded = 0;
            elements.Add(element1);
        }
        private void nitrogen_Click(object sender, EventArgs e)
        {
            createAtom("N");
            Element element2 = new Element();
            element2.e = "N";
            element2.maxbonds = 3;
            element2.bondsformed = 0;
            element2.lonepairs = 1;
            element2.number = total + 1;
            element2.atomsbonded = 0;
            elements.Add(element2);
        }
        void createAtom(string element)
        {

            total++;
            labels.Add(new Label());
            labels[total].Text = element;
            labels[total].Parent = panel1;
            labels[total].Location = new Point(100, 100);
            labels[total].Size = new Size(26, 30);
            labels[total].AllowDrop = true;
            labels[total].MouseDown += new MouseEventHandler(label_mousedown);
            labels[total].MouseMove += new MouseEventHandler(label_mousemove);
            labels[total].MouseUp += new MouseEventHandler(label_mouseup);
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            labels[total].Font = new Font(labels[total].Font.FontFamily, 20);
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            labels[total].MouseClick += new MouseEventHandler(label_mouseclick);
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            List<int> temp = new List<int>();
            adjacencylist.Add(temp);
            labels[total].SendToBack();
            resetselected();
        }
        void adjencylistadd(int e1, int e2)
        {
            adjacencylist[e1].Add(e2);
            adjacencylist[e2].Add(e1);
        }
        private void bond_Click(object sender, EventArgs e)
        {
            changecommand(0, "bond");
        }
        private void doublebond_Click(object sender, EventArgs e)
        {
            changecommand(1, "doublebond");
        }
        private void triplebond_Click(object sender, EventArgs e)
        {
            changecommand(2, "triplebond");
        }
        private void deleteelement_Click(object sender, EventArgs e)
        {
            changecommand(3, "delete element");
        }
        private void deletebonds_Click(object sender, EventArgs e)
        {
            changecommand(4, "deletebond");
        }
        private void changecommand(int but, string action)  //change which command is in use
        {
            if (command == action) //if a selected button is pressed again
            {
                command = "bond";
                commandbuttons[but].BackColor = SystemColors.ControlLight;
            }
            else
            {
                command = action; //sets what to do with s1 and s2
                Button in_use = commandbuttons[but];
                in_use.BackColor = Color.Wheat;
                commandbuttons[but] = in_use;
                for (int i = 0; i < commandbuttons.Count; i++)
                {
                    if (i != but)
                    {
                        commandbuttons[i].BackColor = SystemColors.ControlLight;
                    }
                }
            }
            if (action == "reset")  //resets state of buttons and deselects any commands
            {
                for (int i = 0; i < commandbuttons.Count; i++)
                {
                    commandbuttons[i].BackColor = SystemColors.ControlLight;
                }
                command = "bond";
            }
        }
        void label_mousedown(object sender, MouseEventArgs e)
        {
            isdown = true;
            oldX = e.X;
            oldY = e.Y;
        }
        void label_mouseup(object sender, MouseEventArgs e)
        {
            isdown = false;
        }
        void label_mousemove(object sender, MouseEventArgs e)
        {
            Label lb = (Label)sender;
            if (isdown)
            {
                int lb1 = 0;
                for (int i = 0; i <= total; i++)  // gets the element id of label moved
                {
                    if (lb == labels[i])
                    {
                        lb1 = i;

                    }
                }
                Element moved = elements[lb1];
                if (moved.atomsbonded > 0)

                {
                    moveattempt(e.X - oldX, e.Y - oldY, lb1);
                }
                else
                {
                    lb.Left += e.X - oldX;
                    lb.Top += e.Y - oldY;
                }
            }
        }
        void label_mouseclick(object sender, MouseEventArgs e)
        {
            Label lb = (Label)sender;
            int labelnum = 0;
            for (int i = 0; i <= total; i++)   //finding which label
            {
                if (lb == labels[i])
                {
                    labelnum = i;
                }
            }
            if (command != "")
            {
                if (snum == 0)  //selecting first elemnt
                {
                    s1 = labelnum;
                    labels[labelnum].ForeColor = Color.Red;
                    snum++;
                }
                else if (snum == 1 && lb != labels[s1]) //if second element selected
                {
                    s2 = labelnum;
                    labels[labelnum].ForeColor = Color.Red;
                    snum++;
                    executecommand(); //does command of button wheat coloured with labels s1 and s2
                    resetselected();  //rests snum but not s1 and s2
                }
                else if (lb == labels[s1])
                {
                    s1 = -4; // deselcted s1
                    lb.ForeColor = Color.Black;
                    snum = 0;

                }
                if (command == "delete element")  //for commands that only need one atom
                {
                    executecommand(); //does command of button wheat coloured with labels s1 and s2
                    resetselected();  //rests snum and s1 and s2
                }
            }
        }
        void executecommand()
        {
            if (command == "bond")
            {
                createnewbond(1);
            }
            else if (command == "doublebond")
            {
                createnewbond(2);
            }
            else if (command == "delete element")
            {
                labels[s1].Dispose();
                removefromlist(s1);
            }
            else if (command == "deletebond")
            {
                deletebond();
            }
            else if (command == "triplebond")
            {
                createnewbond(3);
            }

        }
        void deletebond()
        {
            deletebonds.BackColor = SystemColors.ControlLight;
            int deletenum = 0;
            //draw over bond
            for (int i = 0; i <= totalbonds; i++) //for every bond
            {
                bond b1 = bonds[i];

                if ((b1.enum1 == s1 && b1.enum2 == s2 || b1.enum1 == s2 && b1.enum2 == s1)) //if bond exists
                {
                    deletenum = i;
                }
            }
            drawoverbond(bonds[deletenum]);
            //remove from index
            bond b2 = bonds[deletenum];
            int bsize = b2.size;
            b2.removed = true;
            bonds[deletenum] = b2;
            //amend bonds formed in index
            Element e1 = elements[s1];
            Element e2 = elements[s2];
            e1.atomsbonded--;
            e1.bondsformed = e1.bondsformed - bsize;
            e2.atomsbonded--;
            e2.bondsformed = e2.bondsformed - bsize;
            elements[s1] = e1;
            elements[s2] = e2;
        }//needs work for triple bonds
        void removefromlist(int lb)
        {
            for (int i = 0; i <= total; i++) //for every atom
            {
                Element e = elements[i];

                for (int j = 0; j < adjacencylist[i].Count; j++) //for every atom that first atom is bonded to
                {
                    if (adjacencylist[i][j] == lb)   //if j is atom to be removed from list
                    {

                        adjacencylist[i].Remove(lb);
                        e.atomsbonded--;
                        break;
                    }

                }
                elements[i] = e; //adjusts atoms bonded for element i
            }
            for (int j = 0; j <= adjacencylist[lb].Count; j++) //removing from list where lb is first field
            {
                adjacencylist[lb].Remove(j);
            }
            for (int i = 0; i <= totalbonds; i++) //deleting bonds attached to the atom deleted
            {
                bond b = bonds[i];
                if (b.enum1 == lb || b.enum2 == lb)
                {

                    drawoverbond(b);
                    Element e = elements[b.enum1];
                    e.bondsformed = e.bondsformed - b.size;
                    elements[b.enum1] = e;
                    Element e2 = elements[b.enum2];
                    e2.bondsformed = e2.bondsformed - b.size;
                    elements[b.enum2] = e2; // means elemnts with a bond removed from deleting elemnt can form more bonds
                }

            } //draws over bonds. Worked first time          
        }
        void createnewbond(int bondsize)  //needs chemical checking in it, could be done in execute command method
        {
            if ((elements[s1].bondsformed - 1 + bondsize) < elements[s1].maxbonds && (elements[s2].bondsformed - 1 + bondsize) < elements[s2].maxbonds) //janky if statement is if atom has enough space for i
            {
                bond b = new bond();
                b.enum1 = s1;
                b.enum2 = s2;
                b.size = bondsize;
                b.epoint1 = new Point(labels[s1].Left + labels[s1].Width / 2, labels[s1].Top + labels[s1].Height / 2); //moves points in bond to middle of label
                b.epoint2 = new Point(labels[s2].Left + labels[s2].Width / 2, labels[s2].Top + labels[s2].Height / 2);
                bonds.Add(b);
                drawbond(b);
                Element ms = elements[s1];         // edits the data for an element. takes it out changes it and puts it back in
                ms.bondsformed = ms.bondsformed + bondsize;
                ms.atomsbonded++;
                elements[s1] = ms;
                Element ms2 = elements[s2];
                ms2.bondsformed = ms2.bondsformed + bondsize;
                ms2.atomsbonded++;
                elements[s2] = ms2;
                adjencylistadd(s1, s2);
                totalbonds++;
            }
            else
            {
                MessageBox.Show("Atom[s] can't support that many bonds");
            }
        }
        void resetselected()
        {
            command = "";
            for (int i = 0; i <= total; i++) //so can use in create atom method
            {
                labels[i].ForeColor = Color.Black;
            }
            snum = 0;
            s1 = -3;
            s2 = -2; //equivalent of setting to null since no label number will ever be negative

            changecommand(0, "reset");
        }
        void moveattempt(int x, int y, int firste)
        {
            bool[] atomsbonded = new bool[total + 1];
            atomsbonded[firste] = true;
            bool[] bondsused = new bool[totalbonds + 1];
            Element e = elements[firste];
            if (e.atomsbonded > 0)
            {
                for (int i = 0; i < e.atomsbonded; i++) //for each bond it has
                {
                    atomsbonded[adjacencylist[firste][i]] = true;
                }
            }  // works up to here. gets atoms bonded to firste
            for (int t = 0; t < 3; t++) //make t bigger for longer chains going backwards
            {
                for (int j = 0; j < total; j++) //for every element
                {
                    e = elements[j];
                    if (atomsbonded[j] && e.atomsbonded > 1)  //if that element has a bond
                    {
                        for (int i = 0; i < e.atomsbonded; i++) //add its bonded atoms to atomsbonded
                        {
                            atomsbonded[adjacencylist[j][i]] = true;
                        }
                    }
                }
                for (int j = total; j >= 0; j--) //for every element
                {
                    e = elements[j];
                    if (atomsbonded[j] && e.atomsbonded > 1)  //if that element is involed
                    {

                        for (int i = 0; i < e.atomsbonded; i++) //add its bonded atoms to atomsbonded
                        {
                            atomsbonded[adjacencylist[j][i]] = true;
                        }
                    }
                }
            } //have all atoms involved
              // i think this works. atleast some of time. doesnt go far enough down tree without making t bigger
            for (int i = 0; i < total; i++) //finds bonds involed
            {
                for (int j = 0; j <= total; j++)
                {
                    if (atomsbonded[i] && atomsbonded[j])
                    {
                        for (int b = 0; b <= totalbonds; b++)
                        {
                            bond b1 = bonds[b];
                            if (b1.enum1 == i && b1.enum2 == j || b1.enum1 == j && b1.enum2 == i)
                            {
                                bondsused[b] = true;
                            }
                        }
                    }
                }
            } // have all atoms and bonds used
              //works sometimes
            for (int i = 0; i <= total; i++) // moves atoms
            {
                if (atomsbonded[i])
                {
                    labels[i].Left = labels[i].Left + x; // move label i
                    labels[i].Top = labels[i].Top + y;
                }

            }
            for (int b = 0; b <= totalbonds; b++)
            {
                if (bondsused[b])
                {
                    bond b1 = bonds[b];
                    drawoverbond(b1);
                    b1.epoint1.X += x;   // changes bond start and end points to new position
                    b1.epoint1.Y += y;
                    b1.epoint2.X += x;
                    b1.epoint2.Y += y;
                    bonds[b] = b1;
                    drawbond(b1);  //re draw bond in new posotion
                }
            }
        }
        private void drawbond(bond b1)
        {
            if (b1.removed == false)
            {
                if (b1.size == 1)
                {
                    Graphics g = panel1.CreateGraphics();
                    Pen bondnew = new Pen(Color.Black, 2);
                    g.DrawLine(bondnew, b1.epoint1, b1.epoint2);
                }
                else if (b1.size == 2)
                {
                    Graphics g = panel1.CreateGraphics();
                    Point top1 = new Point(b1.epoint1.X + 5, b1.epoint1.Y + 5);
                    Point top2 = new Point(b1.epoint2.X + 5, b1.epoint2.Y + 5);
                    Point bottom1 = new Point(b1.epoint1.X - 5, b1.epoint1.Y - 5);
                    Point bottom2 = new Point(b1.epoint2.X - 5, b1.epoint2.Y - 5);
                    Pen bondnew = new Pen(Color.Black, 2);
                    g.DrawLine(bondnew, top1, top2);
                    g.DrawLine(bondnew, bottom1, bottom2);
                }
                else if (b1.size == 3) //combines lines drawn in 2 above
                {
                    Graphics g = panel1.CreateGraphics();
                    Point top1 = new Point(b1.epoint1.X + 5, b1.epoint1.Y + 5);
                    Point top2 = new Point(b1.epoint2.X + 5, b1.epoint2.Y + 5);
                    Point bottom1 = new Point(b1.epoint1.X - 5, b1.epoint1.Y - 5);
                    Point bottom2 = new Point(b1.epoint2.X - 5, b1.epoint2.Y - 5);
                    Pen bondnew = new Pen(Color.Black, 2);
                    g.DrawLine(bondnew, top1, top2);
                    g.DrawLine(bondnew, b1.epoint1, b1.epoint2);
                    g.DrawLine(bondnew, bottom1, bottom2);
                }
            }
        }
        private void drawoverbond(bond b)
        {
            if (b.removed == false)
            {
                if (b.size == 1)
                {
                    Graphics g = panel1.CreateGraphics();
                    Pen bond = new Pen(panel1.BackColor, 2);
                    g.DrawLine(bond, b.epoint1, b.epoint2);
                }
                else if (b.size == 2)
                {
                    Graphics g = panel1.CreateGraphics();
                    Pen bond = new Pen(panel1.BackColor, 2);
                    Point top1 = new Point(b.epoint1.X + 5, b.epoint1.Y + 5);
                    Point top2 = new Point(b.epoint2.X + 5, b.epoint2.Y + 5);
                    Point bottom1 = new Point(b.epoint1.X - 5, b.epoint1.Y - 5);
                    Point bottom2 = new Point(b.epoint2.X - 5, b.epoint2.Y - 5);
                    g.DrawLine(bond, top1, top2);
                    g.DrawLine(bond, bottom1, bottom2); //rid of old
                }
                else if (b.size == 3) //combines two above
                {
                    Graphics g = panel1.CreateGraphics();
                    Pen bond = new Pen(panel1.BackColor, 2);
                    Point top1 = new Point(b.epoint1.X + 5, b.epoint1.Y + 5);
                    Point top2 = new Point(b.epoint2.X + 5, b.epoint2.Y + 5);
                    Point bottom1 = new Point(b.epoint1.X - 5, b.epoint1.Y - 5);
                    Point bottom2 = new Point(b.epoint2.X - 5, b.epoint2.Y - 5);
                    g.DrawLine(bond, top1, top2);
                    g.DrawLine(bond, bottom1, bottom2); //rid of old
                    g.DrawLine(bond, b.epoint1, b.epoint2);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form2 f = new Form2();
            //f.Dispose();
            //f.Show();
           
            for (int i = 0; i < elements.Count; i++) 
            {
                Element w = new Element();
                w = elements[i];
                w.x = labels[i].Left;
                w.y = labels[i].Top;
                elements[i] = w;
            }
            if (secondForm == null || secondForm.IsDisposed)
            {
                secondForm = new Form2();
            }

            // Show the second form
            secondForm.Show();


        }
    }
}
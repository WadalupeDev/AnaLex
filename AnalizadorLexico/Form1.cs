using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace AnalizadorLexico {
    public partial class Form1 : Form {

        List<string> palabras = new List<string>();
        List<string> lexemas = new List<string>();
        List<string> instrucciones = new List<string>();
        List<string> keywords = new List<string>();
        List<string> variables = new List<string>();
        List<string> metodos = new List<string>();
        List<string> delimitador = new List<string>();
        List<int[]> posicion = new List<int[]>();

        string noComments;
        public Form1() {
            InitializeComponent();
            keywords = File.ReadAllText("Keywords.txt").Split(" ").ToList();
        }

        private void GenerateLineNumbers() {
            int linecount = Entrada.GetLineFromCharIndex(Entrada.TextLength) + 1;
            if (linecount != maxLC) {
                EntradaFilaNum.Clear();
                for (int i = 1; i < linecount + 1; i++) {
                    EntradaFilaNum.AppendText(Convert.ToString(i) + Environment.NewLine);
                }
                maxLC = linecount;
            }
        }

        private void NuevoX_Click(object sender, EventArgs e) {
            if (Entrada.Text.Length > 0) {
                var result = MessageBox.Show("¿Quieres gaurdar tu archivo actual?", "Cambios sin guardar",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);
                if (result == DialogResult.Yes) GaurdarMetodo();
            }
            Entrada.Text = "";
        }

        private void CargarX_Click(object sender, EventArgs e) {
            OpenFileDialog CargarDialog = new OpenFileDialog();
            CargarDialog.Filter = "java (*.java)|*.java";
            CargarDialog.FilterIndex = 2;
            CargarDialog.RestoreDirectory = true;
            if (CargarDialog.ShowDialog() == DialogResult.OK) {
                Entrada.Text = File.ReadAllText(CargarDialog.FileName);
            }
            GenerateLineNumbers();
        }

        private void GuardarX_Click(object sender, EventArgs e) {
            GaurdarMetodo();
            GenerateLineNumbers();
        }

        private void GuardarComoX_Click(object sender, EventArgs e) {
            GaurdarMetodo();
            GenerateLineNumbers();
        }

        private void CompilarX_Click(object sender, EventArgs e) {

            Tabla.Rows.Clear();
            Tabla.Refresh();

            GenerateLineNumbers();


            analisisLexico();
            analisisSintactico();
        }

        private void GaurdarMetodo() {
            Stream myStream;
            SaveFileDialog GuardarDialog = new SaveFileDialog();

            GuardarDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*|java files (*.java)|*.java";
            GuardarDialog.FilterIndex = 2;
            GuardarDialog.RestoreDirectory = true;

            GuardarDialog.OverwritePrompt = true;

            if (GuardarDialog.ShowDialog() == DialogResult.OK) {
                if ((myStream = GuardarDialog.OpenFile()) != null) {
                    Entrada.SaveFile(myStream, RichTextBoxStreamType.PlainText);
                    myStream.WriteByte(13);
                    myStream.Close();
                }
            }
        }

        private void Guardar_Click(object sender, EventArgs e) {
            
        }

        private void Cargar_Click(object sender, EventArgs e) {
            
        }
        private void Nuevo_Click(object sender, EventArgs e) {
            
        }

        private void GuardarComo_Click(object sender, EventArgs e) {
        }

        int maxLC = 1; 
        private void Compilar_Click(object sender, EventArgs e) {
            
        }

        private void analisisLexico() {
            string cont = "";
            int columna, fila = 0;

            var blockComments = @"/\*(.*?)\*/";
            var lineComments = @"//(.*?)\r?\n";
            var strings = @"""((\\[^\n]|[^""\n])*)""";
            var verbatimStrings = @"@(""[^""]*"")+";

            noComments = Regex.Replace(Entrada.Text, blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings, me => {
                if (me.Value.StartsWith("/*") || me.Value.StartsWith("//"))
                    return me.Value.StartsWith("//") ? Environment.NewLine : "";

                return me.Value;
            },
        RegexOptions.Singleline);

            Regex re = new Regex(@" {{
                [^{}]*
                (
                    (
                    (?<Open>{{)
                    [^{}]*
                    )+
                (
                    (?<Close-Open>}})
                    [^{}]*
                    )+
                )*
            (?(Open)(?!))
            }}", RegexOptions.IgnorePatternWhitespace);

            string headers = re.Replace(noComments, "");
            List<string> imports = Regex.Matches(headers, @"(?<=\bimport\s)[^;]*(?=;)\b").OfType<Match>().Select(m => m.Value).ToList();

            delimitador = Regex.Matches(headers, @"{|}|\(|\)|\[|\]").OfType<Match>().Select(m => m.Value).ToList();

            int lineLenght = 0, filaCache = 1, filaSizeCache = 0;
            string TempString = Entrada.Text;
            palabras = Regex.Matches(noComments, @"((?<=\bimport\s)[^;]*(?=;)\b)|(\w+-?)|""(.*?)""|(?<![!=])[!=]=(?!=)|[!#$%&'()*+,.\:;<=>?@\\^_`\[\]{|}~]").OfType<Match>().Select(m => m.Value).ToList();
            variables.AddRange(Regex.Matches(noComments, @"(?:\w+\s+)([a-zA-Z_][a-zA-Z0-9]+)((?=\s*=)|(?=\s*;))").OfType<Match>().Select(m => m.Value).ToList());
            metodos.AddRange(Regex.Matches(noComments, @"([a-zA-Z_{1}][a-zA-Z0-9_]+)(\s*)(?:\().*(?:\))").OfType<Match>().Select(m => m.Groups[1].Value).ToList());
            metodos = metodos.Distinct().ToList();
            metodos = metodos.Except(keywords).ToList();

            foreach (string importedFile in imports) {
                string path = importedFile.Replace('.', '/');
                path += ".txt";
                if (File.Exists(path)) {
                    variables.AddRange(Regex.Matches(File.ReadAllText(path), @"(?:\w+\s+)([a-zA-Z_][a-zA-Z0-9]+)((?=\s*=)|(?=\s*;))").OfType<Match>().Select(m => m.Value).ToList());
                    metodos.AddRange(Regex.Matches(File.ReadAllText(path), @"([a-zA-Z_{1}][a-zA-Z0-9_]+)(\s*)(?:\().*(?:\))\s*(?:\{)").OfType<Match>().Select(m => m.Groups[1].Value).ToList());
                    metodos = metodos.Distinct().ToList();
                    metodos = metodos.Except(keywords).ToList();
                }
            }

            for (int k = 0; k < variables.Count; k++) {
                variables[k] = variables[k].Substring(variables[k].LastIndexOf(" ") + 1);
            }

            for (int i = 0; i < palabras.Count; i++) {

                columna = Entrada.Find(palabras[i]);
                fila = Entrada.GetLineFromCharIndex(columna);
                System.Diagnostics.Debug.WriteLine("fila: " + fila + "filaCache: " + filaCache);
                if (fila > filaCache) {
                    filaSizeCache = columna;
                    filaCache = fila;
                };
                columna = Math.Abs(filaSizeCache - columna);
                Entrada.Select(Entrada.Find(palabras[i]), palabras[i].Length);
                string tempStr = new string('*', palabras[i].Length);
                Entrada.SelectedText = tempStr;

                posicion.Add(new int[] { fila, columna });

                if (keywords.Contains(palabras[i])) cont = "KEYWORD";
                else {
                    switch (palabras[i]) {
                        case "{":
                            cont = "LLAVE I";
                            break;
                        case "}":
                            cont = "LLAVE D";
                            break;
                        case "(":
                            cont = "PARENTESIS I";
                            break;
                        case ")":
                            cont = "PARENTESIS D";
                            break;
                        case "[":
                            cont = "CORCHETE I";
                            break;
                        case "]":
                            cont = "CORCHETE D";
                            break;
                        case ";":
                            cont = "PUNTO Y COMA";
                            break;
                        case ".":
                            cont = "PUNTO";
                            break;
                        case ",":
                            cont = "SEPARADOR";
                            break;
                        case "=":
                            if (i + 1 < palabras.Count && palabras[i + 1].Equals("=")) {
                                cont = "COMPARACION";
                                palabras[i] += palabras[i + 1];
                                palabras.RemoveAt(i + 1);
                            }
                            else if (i + 2 < palabras.Count && palabras[i + 2].Equals("=")) { cont = "ERROR"; }
                            else cont = "ASIGNACION";
                            break;
                        case "+":
                            cont = "OPERADOR SUMA";
                            break;
                        case "-":
                            cont = "OPERADOR RESTA";
                            break;
                        case "*":
                            cont = "OPERADOR MULT";
                            break;
                        case "/":
                            cont = "OPERADOR DIVISION";
                            break;
                        case "%":
                            cont = "OPERADOR MODULO";
                            break;
                        case "!":
                            cont = "UNARIO NOT";
                            break;
                        case "&":
                            if (i+1 < palabras.Count && palabras[i + 1].Equals("&")) { 
                                cont = "OPERADOR AND"; 
                                palabras[i] += palabras[i + 1];
                                palabras.RemoveAt(i+1);
                            }
                            else if (i+2 < palabras.Count && palabras[i + 2].Equals("&")) { cont = "ERROR"; }
                            else cont = "BITWISE AND";
                            break;
                        case "|":
                            if (i + 1 < palabras.Count && palabras[i + 1].Equals("|")) {
                                cont = "OPERADOR OR";
                                palabras[i] += palabras[i + 1];
                                palabras.RemoveAt(i + 1);
                            }
                            else if (i + 2 < palabras.Count && palabras[i + 2].Equals("|")) { cont = "ERROR"; }
                            else cont = "BITWISE OR";
                            break;
                        case "<":
                            cont = "MENOR";
                            break;
                        case ">":
                            cont = "MAYOR";
                            break;
                        case ">=":
                            cont = "MAYOR IGUAL";
                            break;
                        case "<=":
                            cont = "MENOR IGUAL";
                            break;
                        case "!=":
                            cont = "OPERADOR DIFERENTE DE";
                            break;
                        default:
                            int tempInt;
                            double tempDouble;
                            float tempFloat;
                            if (int.TryParse(palabras[i], out tempInt)) cont = "DATATYPE ENTERO";
                            else if (double.TryParse(palabras[i], out tempDouble)) cont = "DATATYPE DOUBLE";
                            else if (float.TryParse(palabras[i], out tempFloat)) cont = "DATATYPE FLOTANTE";
                            else if (palabras[i].First().Equals('\"') && palabras[i].Last().Equals('\"')) cont = "CADENA";
                            else if (variables.Contains(palabras[i]) || metodos.Contains(palabras[i])) cont = "IDENTIFICADOR";
                            else cont = "IDENTIFICADOR";
                            break;
                    }
                }
                Tabla.Rows.Add(cont, palabras[i], fila + ", " + columna);
                lexemas.Add(cont);
            }
            Entrada.Text = TempString;
        }

        private void analisisSintactico() {

            int cor = 0, par = 0, llav = 0;


            BalancedBrackets brackets = new BalancedBrackets();
            if (!brackets.areBracketsBalanced(String.Join("", delimitador).ToCharArray())) {
                ConsolaUnused.AppendText("SyntaxError");
                ConsolaUnused.AppendText(Environment.NewLine);
            }


            string strTemp = "", lexemaCompuesto = "";

            for (int i = 0; i < palabras.Count; i++) {
                if (i + 1 < lexemas.Count) {
                    string nextLex = lexemas[i + 1]; 
                    string nextLexCrop = nextLex; 
                    string lexCrop = lexemas[i];
                    if (nextLex.Any(Char.IsWhiteSpace)) nextLexCrop = nextLex.Split(' ').First();
                    if (nextLex.Any(Char.IsWhiteSpace)) lexCrop = lexCrop.Split(' ').First();
                    if (lexemas[i] == "IDENTIFICADOR" && !(
                        nextLexCrop.Equals("IDENTIFICADOR") ||
                        nextLexCrop.Equals("PUNTO") ||
                        nextLexCrop.Equals("SEPARADOR") ||
                        nextLexCrop.Equals("CORCHETE") ||
                        nextLexCrop.Equals("PARENTESIS") ||
                        nextLexCrop.Equals("LLAVE") ||
                        nextLexCrop.Equals("COMPARACION") ||
                        nextLexCrop.Equals("MAYOR") ||
                        nextLexCrop.Equals("MENOR") ||
                        nextLexCrop.Equals("ASIGNACION") ||
                        nextLexCrop.Equals("DATATYPE") ||
                        nextLexCrop.Equals("OPERADOR"))) {

                        ConsolaUnused.AppendText("Error en " + posicion[i][0] + ", " + posicion[i][1] + " Cerca de:  " + lexemas[i] + " " + lexemas[i+1]);
                        ConsolaUnused.AppendText(Environment.NewLine);
                    }else if (lexemas[i] == "ASIGNACION" && !(
                        nextLexCrop.Equals("IDENTIFICADOR") ||
                        nextLexCrop.Equals("CORCHETE") ||
                        nextLexCrop.Equals("KEYWORD") ||
                        nextLexCrop.Equals("PARENTESIS") ||
                        nextLexCrop.Equals("LLAVE") ||
                        nextLexCrop.Equals("DATATYPE"))) {

                        ConsolaUnused.AppendText("Error en " + posicion[i][0] + ", " + posicion[i][1] + " Cerca de:  " + lexemas[i] + " " + lexemas[i+1]);
                        ConsolaUnused.AppendText(Environment.NewLine);
                    }
                    else if (lexemas[i] == "IDENTIFICADOR" && !(variables.Contains(palabras[i]) || metodos.Contains(palabras[i]))) {

                        if (i + 1 < lexemas.Count && lexemas[i + 1].Equals("PUNTO")) {
                            strTemp += palabras[i] + palabras[i + 1];
                        }
                        else if (i > 0 && lexemas[i - 1].Equals("PUNTO") && !String.IsNullOrEmpty(strTemp)) {
                            lexemaCompuesto = strTemp.Remove(strTemp.Length - 1);
                            strTemp = "";
                            if (!String.IsNullOrEmpty(lexemaCompuesto)) {
                                if (!File.Exists(lexemaCompuesto.Replace('.', '/'))) {
                                    ConsolaUnused.AppendText("Error en " + posicion[i][0] + ", " + posicion[i][1] + " \t" + palabras[i] + " No se reconoce como un identificador valido \t" + lexemaCompuesto);
                                    ConsolaUnused.AppendText(Environment.NewLine);
                                }
                                lexemaCompuesto = "";
                            }
                        }
                        
                                   
                    }
                    else if ((lexemas[i] == "DATATYPE ENTERO" || lexemas[i] == "DATATYPE DOUBLE" || lexemas[i] == "DATATYPE FLOTANTE") && !(
                        nextLex.Equals("KEYWORD") ||
                        nextLexCrop.Equals("SEPARADOR") ||
                        nextLexCrop.Equals("OPERADOR") ||
                        nextLexCrop.Equals("MAYOR") ||
                        nextLexCrop.Equals("MENOR") ||
                        nextLexCrop.Equals("CORCHETE") ||
                        nextLexCrop.Equals("PARENTESIS") ||
                        nextLexCrop.Equals("PUNTO") ||
                        nextLexCrop.Equals("LLAVE") ||
                        nextLexCrop.Equals("COMPARACION") ||
                        nextLex.Equals("IDENTIFICADOR"))) {

                        ConsolaUnused.AppendText("Error en " + posicion[i][0] + ", " + posicion[i][1] + " Cerca de:  " + lexemas[i] + " " + lexemas[i + 1]);
                        ConsolaUnused.AppendText(Environment.NewLine);
                    }
                    else if (lexemas[i] == "PUNTO" && !(
                        nextLex.Equals("KEYWORD") ||
                        nextLexCrop.Equals("DATATYPE") ||
                        nextLex.Equals("IDENTIFICADOR"))){

                        ConsolaUnused.AppendText("Error en " + posicion[i][0] + ", " + posicion[i][1] + " Cerca de:  " + lexemas[i] + " " + lexemas[i + 1]);
                        ConsolaUnused.AppendText(Environment.NewLine);
                    }else if (lexemas[i] == "KEYWORD" && !(
                        nextLexCrop.Equals("IDENTIFICADOR") ||
                        nextLexCrop.Equals("KEYWORD") ||
                        nextLexCrop.Equals("PUNTO") ||
                        nextLexCrop.Equals("SEPARADOR") ||
                        nextLexCrop.Equals("CORCHETE") ||
                        nextLexCrop.Equals("PARENTESIS") ||
                        nextLexCrop.Equals("LLAVE") ||
                        nextLexCrop.Equals("COMPARACION") ||
                        nextLexCrop.Equals("MAYOR") ||
                        nextLexCrop.Equals("MENOR") ||
                        nextLexCrop.Equals("ASIGNACION") ||
                        nextLexCrop.Equals("DATATYPE") ||
                        nextLexCrop.Equals("OPERADOR"))) {

                        ConsolaUnused.AppendText("Error en " + posicion[i][0] + ", " + posicion[i][1] + " Cerca de:  " + lexemas[i] + " " + lexemas[i + 1]);
                        ConsolaUnused.AppendText(Environment.NewLine);
                    }
                    else if (lexemas[i] == "SEPARADOR" && !(
                        nextLexCrop.Equals("DATATYPE") ||
                        nextLex.Equals("KEYWORD") ||
                        nextLex.Equals("LLAVE I") ||
                        nextLex.Equals("IDENTIFICADOR"))) {

                        ConsolaUnused.AppendText("Error en " + posicion[i][0] + ", " + posicion[i][1] + " Cerca de:  " + lexemas[i] + " " + lexemas[i + 1]);
                        ConsolaUnused.AppendText(Environment.NewLine);
                    }
                    else if (lexemas[i] == "MENOR" && !(
                        nextLexCrop.Equals("DATATYPE") ||
                        nextLex.Equals("IDENTIFICADOR"))) {

                        ConsolaUnused.AppendText("Error en " + posicion[i][0] + ", " + posicion[i][1] + " Cerca de:  " + lexemas[i] + " " + lexemas[i + 1]);
                        ConsolaUnused.AppendText(Environment.NewLine);
                    }
                    else if (lexemas[i] == "MAYOR" && !(
                        nextLexCrop.Equals("DATATYPE") ||
                        nextLex.Equals("IDENTIFICADOR"))) {

                        ConsolaUnused.AppendText("Error en " + posicion[i][0] + ", " + posicion[i][1] + " Cerca de:  " + lexemas[i] + " " + lexemas[i + 1]);
                        ConsolaUnused.AppendText(Environment.NewLine);
                    }
                    else if (lexemas[i] == "MENOR QUE" && !(
                        nextLexCrop.Equals("DATATYPE") ||
                        nextLex.Equals("IDENTIFICADOR"))) {

                        ConsolaUnused.AppendText("Error en " + posicion[i][0] + ", " + posicion[i][1] + " Cerca de:  " + lexemas[i] + " " + lexemas[i + 1]);
                        ConsolaUnused.AppendText(Environment.NewLine);
                    }
                    else if (lexemas[i] == "MAYOR QUE" && !(
                        nextLexCrop.Equals("DATATYPE") ||
                        nextLex.Equals("IDENTIFICADOR"))) {

                        ConsolaUnused.AppendText("Error en " + posicion[i][0] + ", " + posicion[i][1] + " Cerca de:  " + lexemas[i] + " " + lexemas[i + 1]);
                        ConsolaUnused.AppendText(Environment.NewLine);
                    }
                    else if (lexemas[i] == "PARENTESIS D" && !(
                        nextLexCrop.Equals("PUNTO") ||
                        nextLexCrop.Equals("OPERADOR") ||
                        nextLexCrop.Equals("MAYOR") ||
                        nextLexCrop.Equals("MENOR") ||
                        nextLexCrop.Equals("ASIGNACION") ||
                        nextLexCrop.Equals("LLAVE"))) {

                        ConsolaUnused.AppendText("Error en " + posicion[i][0] + ", " + posicion[i][1] + " Cerca de:  " + lexemas[i] + " " + lexemas[i + 1]);
                        ConsolaUnused.AppendText(Environment.NewLine);
                    }
                    else if (lexemas[i].Split(' ').First() == "OPERADOR" && !(
                        nextLexCrop.Equals("DATATYPE") ||
                        nextLexCrop.Equals("CADENA") ||
                        nextLex.Equals("IDENTIFICADOR"))) {

                        ConsolaUnused.AppendText("Error en " + posicion[i][0] + ", " + posicion[i][1] + " Cerca de:  " + lexemas[i] + " " + lexemas[i + 1]);
                        ConsolaUnused.AppendText(Environment.NewLine);
                    }
                }
                if (i > 0) {
                    if (lexemas[i] == "ASIGNACION" && !(
                        lexemas[i-1].Equals("IDENTIFICADOR") ||
                        lexemas[i-1].Equals("CORCHETE D") ||
                        lexemas[i-1].Equals("PARENTESIS D"))) {

                        ConsolaUnused.AppendText("Error en " + posicion[i][0] + ", " + posicion[i][1] + " Cerca de:  " + lexemas[i] + " " + lexemas[i + 1]);
                        ConsolaUnused.AppendText(Environment.NewLine);
                    }
                    else if (lexemas[i].Split(' ').First() == "OPERADOR" && !(
                        lexemas[i - 1].Equals("DATATYPE") ||
                        lexemas[i - 1].Equals("CADENA") ||
                        lexemas[i - 1].Equals("IDENTIFICADOR"))) {

                        ConsolaUnused.AppendText("Error en " + posicion[i][0] + ", " + posicion[i][1] + " Cerca de:  " + lexemas[i] + " " + lexemas[i + 1]);
                        ConsolaUnused.AppendText(Environment.NewLine);
                    }
                }


                if (lexemas[i].Equals("KEYWORD")) {
                    int indexRes = 0;
                    switch (palabras[i]) {
                        case "for":
                            indexRes = i;
                            if (!(lexemas[i + 1].Equals("PARENTESIS I") && lexemas[i + 2].Equals("KEYWORD") && lexemas[i + 3].Equals("IDENTIFICADOR") && lexemas[i + 4].Equals("ASIGNACION") && lexemas[i + 5].Equals("DATATYPE ENTERO") && lexemas[i + 6].Equals("PUNTO Y COMA") && lexemas[i + 7].Equals("IDENTIFICADOR") && (lexemas[i + 8].Split(' ').First().Equals("OPERADOR") || lexemas[i + 8].Split(' ').First().Equals("MAYOR") || lexemas[i + 8].Split(' ').First().Equals("MENOR")) && (lexemas[i + 9].Equals("IDENTIFICADOR")|| lexemas[i + 9].Equals("DATATYPE ENTERO")) && lexemas[i + 10].Equals("PUNTO Y COMA") && lexemas[i + 11].Equals("IDENTIFICADOR"))) {
                                ConsolaUnused.AppendText("Error en " + posicion[i][0] + ", " + posicion[i][1] + " Cerca de:  " + lexemas[i] + " " + lexemas[i + 1] + " Estructura incorrecta de la instruccion for");
                                ConsolaUnused.AppendText(Environment.NewLine);
                            }
                            break;
                        case "while":
                            indexRes = i;
                            if (!(lexemas[i + 1].Equals("PARENTESIS I") && (lexemas[i + 2].Equals("IDENTIFICADOR")|| lexemas[i + 2].Equals("UNARIO NOT")))) {
                                ConsolaUnused.AppendText("Error en " + posicion[i][0] + ", " + posicion[i][1] + " Cerca de:  " + lexemas[i] + " " + lexemas[i + 1] + " Estructura incorrecta de la instruccion for");
                                ConsolaUnused.AppendText(Environment.NewLine);
                            }
                            break;
                        case "if":
                            indexRes = i;
                            if (((lexemas[i + 1].Equals("PARENTESIS I")) && (lexemas[i + 1].Equals("PARENTESIS D"))||(!lexemas[i + 1].Equals("PARENTESIS I")))) {
                                ConsolaUnused.AppendText("Error en " + posicion[i][0] + ", " + posicion[i][1] + " Cerca de:  " + lexemas[i] + " " + lexemas[i + 1] + " Estructura incorrecta de la instruccion for");
                                ConsolaUnused.AppendText(Environment.NewLine);
                            }
                            break;
                    }
                }
            }
            ConsolaUnused.AppendText("Compilacion terminada");
            ConsolaUnused.AppendText(Environment.NewLine+Environment.NewLine);
            palabras.Clear();
            variables.Clear();
            metodos.Clear();
            lexemas.Clear();
            delimitador.Clear();
            posicion.Clear();
        }

        public enum ScrollBarType : uint {
            SbHorz = 0,
            SbVert = 1,
            SbCtl = 2,
            SbBoth = 3
        }

        public enum Message : uint {
            WM_VSCROLL = 0x0115
        }

        public enum ScrollBarCommands : uint {
            SB_THUMBPOSITION = 4
        }

        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        void Entrada_TextChanged(object sender, EventArgs e) {

        }

        void MainPanel_Paint(object sender, PaintEventArgs e) {

        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }

        private void Entrada_MouseDown(object sender, MouseEventArgs e) {

        }

        private void Entrada_VScroll(object sender, EventArgs e) {
            int nPos = GetScrollPos(Entrada.Handle, (int)ScrollBarType.SbVert);
            nPos <<= 16;
            uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;
            SendMessage(EntradaFilaNum.Handle, (int)Message.WM_VSCROLL, new IntPtr(wParam), new IntPtr(0));
        }
    }
}
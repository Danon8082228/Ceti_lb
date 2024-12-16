using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {

        Dictionary<char, string> charToCP866 = new Dictionary<char, string>()
        {
            {'А', "0x80"}, {'Б', "0x81"}, {'В', "0x82"}, {'Г', "0x83"}, {'Д', "0x84"}, {'Е', "0x85"}, {'Ж', "0x86"}, {'З', "0x87"},
            {'И', "0x88"}, {'Й', "0x89"}, {'К', "0x8A"}, {'Л', "0x8B"}, {'М', "0x8C"}, {'Н', "0x8D"}, {'О', "0x8E"}, {'П', "0x8F"},
            {'Р', "0x90"}, {'С', "0x91"}, {'Т', "0x92"}, {'У', "0x93"}, {'Ф', "0x94"}, {'Х', "0x95"}, {'Ц', "0x96"}, {'Ч', "0x97"},
            {'Ш', "0x98"}, {'Щ', "0x99"}, {'Ъ', "0x9A"}, {'Ы', "0x9B"}, {'Ь', "0x9C"}, {'Э', "0x9D"}, {'Ю', "0x9E"}, {'Я', "0x9F"},

            {'а', "0xA0"}, {'б', "0xA1"}, {'в', "0xA2"}, {'г', "0xA3"}, {'д', "0xA4"}, {'е', "0xA5"}, {'ж', "0xA6"}, {'з', "0xA7"},
            {'и', "0xA8"}, {'й', "0xA9"}, {'к', "0xAA"}, {'л', "0xAB"}, {'м', "0xAC"}, {'н', "0xAD"}, {'о', "0xAE"}, {'п', "0xAF"},
            {'р', "0xE0"}, {'с', "0xE1"}, {'т', "0xE2"}, {'у', "0xE3"}, {'ф', "0xE4"}, {'х', "0xE5"}, {'ц', "0xE6"}, {'ч', "0xE7"},
            {'ш', "0xE8"}, {'щ', "0xE9"}, {'ъ', "0xEA"}, {'ы', "0xEB"}, {'ь', "0xEC"}, {'э', "0xED"}, {'ю', "0xEE"}, {'я', "0xEF"},

            {'A', "0x41"}, {'B', "0x42"}, {'C', "0x43"}, {'D', "0x44"}, {'E', "0x45"}, {'F', "0x46"}, {'G', "0x47"}, {'H', "0x48"},
            {'I', "0x49"}, {'J', "0x4A"}, {'K', "0x4B"}, {'L', "0x4C"}, {'M', "0x4D"}, {'N', "0x4E"}, {'O', "0x4F"}, {'P', "0x50"},
            {'Q', "0x51"}, {'R', "0x52"}, {'S', "0x53"}, {'T', "0x54"}, {'U', "0x55"}, {'V', "0x56"}, {'W', "0x57"}, {'X', "0x58"},
            {'Y', "0x59"}, {'Z', "0x5A"}, 
            
            {'a', "0x61"}, {'b', "0x62"}, {'c', "0x63"}, {'d', "0x64"}, {'e', "0x65"}, {'f', "0x66"}, {'g', "0x67"}, {'h', "0x68"},
            {'i', "0x69"}, {'j', "0x6A"}, {'k', "0x6B"}, {'l', "0x6C"}, {'m', "0x6D"}, {'n', "0x6E"}, {'o', "0x6F"}, {'p', "0x70"}, 
            {'q', "0x71"}, {'r', "0x72"}, {'s', "0x73"}, {'t', "0x74"}, {'u', "0x75"}, {'v', "0x76"}, {'w', "0x77"}, {'x', "0x78"}, 
            {'y', "0x79"}, {'z', "0x7A"}, 
        
        };

        Dictionary<string, char> CP866ToChar = new Dictionary<string, char>()
        {
            { "0x80", 'А' }, { "0x81", 'Б' }, { "0x82", 'В' }, { "0x83", 'Г' }, { "0x84", 'Д' }, { "0x85", 'Е' }, { "0x86", 'Ж' }, { "0x87", 'З' },
            { "0x88", 'И' }, { "0x89", 'Й' }, { "0x8A", 'К' }, { "0x8B", 'Л' }, { "0x8C", 'М' }, { "0x8D", 'Н' }, { "0x8E", 'О' }, { "0x8F", 'П' },
            { "0x90", 'Р' }, { "0x91", 'С' }, { "0x92", 'Т' }, { "0x93", 'У' }, { "0x94", 'Ф' }, { "0x95", 'Х' }, { "0x96", 'Ц' }, { "0x97", 'Ч' },
            { "0x98", 'Ш' }, { "0x99", 'Щ' }, { "0x9A", 'Ъ' }, { "0x9B", 'Ы' }, { "0x9C", 'Ь' }, { "0x9D", 'Э' }, { "0x9E", 'Ю' }, { "0x9F", 'Я' },

            { "0xA0", 'а' }, { "0xA1", 'б' }, { "0xA2", 'в' }, { "0xA3", 'г' }, { "0xA4", 'д' }, { "0xA5", 'е' }, { "0xA6", 'ж' }, { "0xA7", 'з' },
            { "0xA8", 'и' }, { "0xA9", 'й' }, { "0xAA", 'к' }, { "0xAB", 'л' }, { "0xAC", 'м' }, { "0xAD", 'н' }, { "0xAE", 'о' }, { "0xAF", 'п' },
            { "0xB0", 'р' }, { "0xB1", 'с' }, { "0xB2", 'т' }, { "0xB3", 'у' }, { "0xB4", 'ф' }, { "0xB5", 'х' }, { "0xB6", 'ц' }, { "0xB7", 'ч' },
            { "0xB8", 'ш' }, { "0xB9", 'щ' }, { "0xBA", 'ъ' }, { "0xBB", 'ы' }, { "0xBC", 'ь' }, { "0xBD", 'э' }, { "0xBE", 'ю' }, { "0xBF", 'я' },

            { "0x41", 'A' }, { "0x42", 'B' }, { "0x43", 'C' }, { "0x44", 'D' }, { "0x45", 'E' }, { "0x46", 'F' }, { "0x47", 'G' }, { "0x48", 'H' },
            { "0x49", 'I' }, { "0x4A", 'J' }, { "0x4B", 'K' }, { "0x4C", 'L' }, { "0x4D", 'M' }, { "0x4E", 'N' }, { "0x4F", 'O' }, { "0x50", 'P' },
            { "0x51", 'Q' }, { "0x52", 'R' }, { "0x53", 'S' }, { "0x54", 'T' }, { "0x55", 'U' }, { "0x56", 'V' }, { "0x57", 'W' }, { "0x58", 'X' },
            { "0x59", 'Y' }, { "0x5A", 'Z' },

            { "0x61", 'a' }, { "0x62", 'b' }, { "0x63", 'c' }, { "0x64", 'd' }, { "0x65", 'e' }, { "0x66", 'f' }, { "0x67", 'g' }, { "0x68", 'h' },
            { "0x69", 'i' }, { "0x6A", 'j' }, { "0x6B", 'k' }, { "0x6C", 'l' }, { "0x6D", 'm' }, { "0x6E", 'n' }, { "0x6F", 'o' }, { "0x70", 'p' },
            { "0x71", 'q' }, { "0x72", 'r' }, { "0x73", 's' }, { "0x74", 't' }, { "0x75", 'u' }, { "0x76", 'v' }, { "0x77", 'w' }, { "0x78", 'x' },
            { "0x79", 'y' }, { "0x7A", 'z' }
        };


        public string[] encode(string text)
        {
            string[] res = {"", ""};

            int strLength = text.Length;

            for (int i = 0; i < strLength; i++)
            {
                if (charToCP866.ContainsKey(text[i]))
                {
                    res[0] += charToCP866[text[i]];
                    res[1] += Convert.ToString(text[i], 2);
                    res[1] += " ";
                }
            }

            return res;
        }

        public string decode(string text)
        {
            string spltd = "";
            int strLength = text.Length;

            for (int i = 0; i < strLength; i++)
            {
                if (i % 4 == 0)
                {
                    spltd += " ";
                }
                spltd += text[i];
            }

            string res = "";
            string[] test = spltd.Split(' ');

            Console.WriteLine("test");
            
            foreach (string i in test)
            {
                if (CP866ToChar.ContainsKey(i))
                {
                    res += CP866ToChar[i];
                }
            }

            return res;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string textToEncode = textBox1.Text;
            string[] encoded = encode(textToEncode);
            textBox2.Text = encoded[0];
            textBox3.Text = encoded[1];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string textToDecode = textBox2.Text;
            textBox1.Text = decode(textToDecode);
        }
    }
}

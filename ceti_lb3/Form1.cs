using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Dictionary<char, string> charToWindows1251 = new Dictionary<char, string>()
        {
            {'А', "0xC0"}, {'Б', "0xC1"}, {'В', "0xC2"}, {'Г', "0xC3"}, {'Д', "0xC4"}, {'Е', "0xC5"}, {'Ж', "0xC6"}, {'З', "0xC7"},
            {'И', "0xC8"}, {'Й', "0xC9"}, {'К', "0xCA"}, {'Л', "0xCB"}, {'М', "0xCC"}, {'Н', "0xCD"}, {'О', "0xCE"}, {'П', "0xCF"},
            {'Р', "0xD0"}, {'С', "0xD1"}, {'Т', "0xD2"}, {'У', "0xD3"}, {'Ф', "0xD4"}, {'Х', "0xD5"}, {'Ц', "0xD6"}, {'Ч', "0xD7"},
            {'Ш', "0xD8"}, {'Щ', "0xD9"}, {'Ъ', "0xDA"}, {'Ы', "0xDB"}, {'Ь', "0xDC"}, {'Э', "0xDD"}, {'Ю', "0xDE"}, {'Я', "0xDF"},

            {'а', "0xE0"}, {'б', "0xE1"}, {'в', "0xE2"}, {'г', "0xE3"}, {'д', "0xE4"}, {'е', "0xE5"}, {'ж', "0xE6"}, {'з', "0xE7"},
            {'и', "0xE8"}, {'й', "0xE9"}, {'к', "0xEA"}, {'л', "0xEB"}, {'м', "0xEC"}, {'н', "0xED"}, {'о', "0xEE"}, {'п', "0xEF"},
            {'р', "0xF0"}, {'с', "0xF1"}, {'т', "0xF2"}, {'у', "0xF3"}, {'ф', "0xF4"}, {'х', "0xF5"}, {'ц', "0xF6"}, {'ч', "0xF7"},
            {'ш', "0xF8"}, {'щ', "0xF9"}, {'ъ', "0xFA"}, {'ы', "0xFB"}, {'ь', "0xFC"}, {'э', "0xFD"}, {'ю', "0xFE"}, {'я', "0xFF"},

            {'A', "0x41"}, {'B', "0x42"}, {'C', "0x43"}, {'D', "0x44"}, {'E', "0x45"}, {'F', "0x46"}, {'G', "0x47"}, {'H', "0x48"},
            {'I', "0x49"}, {'J', "0x4A"}, {'K', "0x4B"}, {'L', "0x4C"}, {'M', "0x4D"}, {'N', "0x4E"}, {'O', "0x4F"}, {'P', "0x50"},
            {'Q', "0x51"}, {'R', "0x52"}, {'S', "0x53"}, {'T', "0x54"}, {'U', "0x55"}, {'V', "0x56"}, {'W', "0x57"}, {'X', "0x58"},
            {'Y', "0x59"}, {'Z', "0x5A"},

            {'a', "0x61"}, {'b', "0x62"}, {'c', "0x63"}, {'d', "0x64"}, {'e', "0x65"}, {'f', "0x66"}, {'g', "0x67"}, {'h', "0x68"},
            {'i', "0x69"}, {'j', "0x6A"}, {'k', "0x6B"}, {'l', "0x6C"}, {'m', "0x6D"}, {'n', "0x6E"}, {'o', "0x6F"}, {'p', "0x70"},
            {'q', "0x71"}, {'r', "0x72"}, {'s', "0x73"}, {'t', "0x74"}, {'u', "0x75"}, {'v', "0x76"}, {'w', "0x77"}, {'x', "0x78"},
            {'y', "0x79"}, {'z', "0x7A"}, {':', "0x3A"}
        };

        Dictionary<string, char> Windows1251ToChar = new Dictionary<string, char>()
        {
            { "0xC0", 'А' }, { "0xC1", 'Б' }, { "0xC2", 'В' }, { "0xC3", 'Г' }, { "0xC4", 'Д' }, { "0xC5", 'Е' }, { "0xC6", 'Ж' }, { "0xC7", 'З' },
            { "0xC8", 'И' }, { "0xC9", 'Й' }, { "0xCA", 'К' }, { "0xCB", 'Л' }, { "0xCC", 'М' }, { "0xCD", 'Н' }, { "0xCE", 'О' }, { "0xCF", 'П' },
            { "0xD0", 'Р' }, { "0xD1", 'С' }, { "0xD2", 'Т' }, { "0xD3", 'У' }, { "0xD4", 'Ф' }, { "0xD5", 'Х' }, { "0xD6", 'Ц' }, { "0xD7", 'Ч' },
            { "0xD8", 'Ш' }, { "0xD9", 'Щ' }, { "0xDA", 'Ъ' }, { "0xDB", 'Ы' }, { "0xDC", 'Ь' }, { "0xDD", 'Э' }, { "0xDE", 'Ю' }, { "0xDF", 'Я' },

            { "0xE0", 'а' }, { "0xE1", 'б' }, { "0xE2", 'в' }, { "0xE3", 'г' }, { "0xE4", 'д' }, { "0xE5", 'е' }, { "0xE6", 'ж' }, { "0xE7", 'з' },
            { "0xE8", 'и' }, { "0xE9", 'й' }, { "0xEA", 'к' }, { "0xEB", 'л' }, { "0xEC", 'м' }, { "0xED", 'н' }, { "0xEE", 'о' }, { "0xEF", 'п' },
            { "0xF0", 'р' }, { "0xF1", 'с' }, { "0xF2", 'т' }, { "0xF3", 'у' }, { "0xF4", 'ф' }, { "0xF5", 'х' }, { "0xF6", 'ц' }, { "0xF7", 'ч' },
            { "0xF8", 'ш' }, { "0xF9", 'щ' }, { "0xFA", 'ъ' }, { "0xFB", 'ы' }, { "0xFC", 'ь' }, { "0xFD", 'э' }, { "0xFE", 'ю' }, { "0xFF", 'я' },

            { "0x41", 'A' }, { "0x42", 'B' }, { "0x43", 'C' }, { "0x44", 'D' }, { "0x45", 'E' }, { "0x46", 'F' }, { "0x47", 'G' }, { "0x48", 'H' },
            { "0x49", 'I' }, { "0x4A", 'J' }, { "0x4B", 'K' }, { "0x4C", 'L' }, { "0x4D", 'M' }, { "0x4E", 'N' }, { "0x4F", 'O' }, { "0x50", 'P' },
            { "0x51", 'Q' }, { "0x52", 'R' }, { "0x53", 'S' }, { "0x54", 'T' }, { "0x55", 'U' }, { "0x56", 'V' }, { "0x57", 'W' }, { "0x58", 'X' },
            { "0x59", 'Y' }, { "0x5A", 'Z' },

            { "0x61", 'a' }, { "0x62", 'b' }, { "0x63", 'c' }, { "0x64", 'd' }, { "0x65", 'e' }, { "0x66", 'f' }, { "0x67", 'g' }, { "0x68", 'h' },
            { "0x69", 'i' }, { "0x6A", 'j' }, { "0x6B", 'k' }, { "0x6C", 'l' }, { "0x6D", 'm' }, { "0x6E", 'n' }, { "0x6F", 'o' }, { "0x70", 'p' },
            { "0x71", 'q' }, { "0x72", 'r' }, { "0x73", 's' }, { "0x74", 't' }, { "0x75", 'u' }, { "0x76", 'v' }, { "0x77", 'w' }, { "0x78", 'x' },
            { "0x79", 'y' }, { "0x7A", 'z' }, {"0x3A", ':'}
        };



        public string[] encode(string text)
        {
            string[] res = { "", "" }; // res[0] - закодированная строка, res[1] - бинарное представление
            int strLength = text.Length;

            for (int i = 0; i < strLength; i++)
            {
                if (charToWindows1251.ContainsKey(text[i]))
                {
                    res[0] += charToWindows1251[text[i]]; // Добавляем код символа
                    res[1] += Convert.ToString(charToWindows1251[text[i]][0], 2) + " ";
                }
                else
                {
                    // Для символов, не входящих в словарь (например, пробелы), оставляем их как есть
                    res[0] += text[i];
                    res[1] += " "; // Пробелы добавляем отдельно
                }
            }

            return res;
        }

        public string decode(string text)
        {
            string res = "";
            int i = 0;
            int strLength = text.Length;

            while (i < strLength)
            {
                // Проверяем, является ли текущий символ частью кода "0x"
                if (i + 3 < strLength && text[i] == '0' && text[i + 1] == 'x')
                {
                    string key = text.Substring(i, 4); // Получаем следующую группу из 4 символов (например, "0xC0")

                    // Проверяем, есть ли ключ в словаре
                    if (Windows1251ToChar.ContainsKey(key))
                    {
                        res += Windows1251ToChar[key]; // Добавляем соответствующий символ
                        i += 4; // Пропускаем обработанные 4 символа
                    }
                    else
                    {
                        i++; // Если ключ не найден, пропускаем текущий символ
                    }
                }
                else
                {
                    // Если текущий символ не часть кода (например, пробел), добавляем его как есть
                    res += text[i];
                    i++;
                }
            }

            return res; // Возвращаем раскодированную строку
        }


   

        string localIP;
        int localPort = 11000;

        string serverIp;
        int serverPort;


        public Form1()
        {
            InitializeComponent();
        }




        private void button1_Click(object sender, EventArgs e)
        {

            TcpClient client = new TcpClient();
            client.Connect(serverIp, serverPort);

            NetworkStream stream = client.GetStream();
            string message = textBox3.Text;

            string userName = textBox7.Text + " ";
            string fullMessage = $"{userName}: {message}";

            byte[] sendBytes = Encoding.GetEncoding("windows-1251").GetBytes(encode(fullMessage)[0]);
            stream.Write(sendBytes, 0, sendBytes.Length);

            Invoke((MethodInvoker)(() => textBox1.Text += Environment.NewLine + $"{localPort} (Вы): {fullMessage}"));
            Invoke((MethodInvoker)(() => textBox2.Text += Environment.NewLine + $"{localPort} (Вы): {encode(fullMessage)[0]}"));
        }


        public void serverThread()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, localPort);
            listener.Start();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                string fullMessage = Encoding.GetEncoding("windows-1251").GetString(buffer, 0, bytesRead);

                string decodedMessage = decode(fullMessage);
                Console.WriteLine(decodedMessage);
                string[] parts = decodedMessage.Split(new[] { ":" }, 2, StringSplitOptions.None);
                string userName = parts[0];
                string message = parts[1]; // Извлекаем сообщение


                Invoke((MethodInvoker)(() => textBox1.Text += $"{Environment.NewLine}{serverPort} ({userName}): {message}"));
                Invoke((MethodInvoker)(() => textBox2.Text += $"{Environment.NewLine}{serverPort} ({userName}): {fullMessage}"));

                stream.Close();
                client.Close();
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;

                localIP = endPoint.Address.ToString();


            }



            this.Text = localIP;
            serverIp = "192.168.1.106";
            MessageBox.Show("Серверный IP установлен: " + serverIp);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            serverPort = Convert.ToInt32(textBox4.Text);
            localPort = Convert.ToInt32(textBox5.Text);
            serverIp = textBox6.Text;

            Thread server = new Thread(new ThreadStart(serverThread));
            server.Start();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
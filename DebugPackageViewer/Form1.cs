using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;

using DomainObjects;

namespace DebugPackageViewer
{
    public partial class Form1 : Form
    {
        private string currentPackageFile;

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Debug Package Files (*.pkg)|*.pkg";
            openFile.InitialDirectory = Path.Combine(RunParameters.DataDirectory, "Debug Packages");
            openFile.RestoreDirectory = true;
            openFile.Title = "EPG Collector - Find Debug Package File";

            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            processFile(openFile.FileName);
        }

        private void processFile(string fileName)
        {
            currentPackageFile = fileName;
            tbPath.Text = fileName;

            lbFiles.Items.Clear();

            BinaryReader reader = new BinaryReader(File.OpenRead(fileName));
            bool eof = false;

            while (!eof)
            {
                Tuple<int, bool> nameLength = getLength(reader, 1);
                if (nameLength == null)
                    eof = true;
                else
                {
                    byte[] nameBytes = getBytes(reader, nameLength.Item1);
                    if (nameBytes == null)
                        eof = true;
                    else
                    {
                        string name = Encoding.ASCII.GetString(nameBytes);

                        Tuple<int, bool> dataLength = getLength(reader, 4);
                        if (dataLength == null)
                            eof = true;
                        else
                        {
                            byte[] dataBytes = new byte[dataLength.Item1];
                            int bytesRead = reader.Read(dataBytes, 0, dataLength.Item1);
                            if (bytesRead != dataLength.Item1)
                                eof = true;

                            if (dataLength.Item2)
                            {
                                byte[] uncompressedBytes = uncompressBytes(dataBytes);
                                lbFiles.Items.Add(name + " size = " + getSizeDescription(uncompressedBytes.Length));
                            }
                            else
                                lbFiles.Items.Add(name + " size = " + getSizeDescription(dataLength.Item1));                            
                        }
                    }
                }
            }

            reader.Close();
        }

        private Tuple<int, bool> getLength(BinaryReader reader, int length)
        {
            byte[] buffer = new byte[length + 1];
            int bytesRead = reader.Read(buffer, 0, buffer.Length);
            if (bytesRead != buffer.Length)
                return null;

            int result = 0;

            for (int index = length - 1; index > -1; index--)
                result = (result * 256) + buffer[index];

            return Tuple.Create<int, bool>(result, buffer[buffer.Length - 1] != 0);
        }

        private byte[] getBytes(BinaryReader reader, int length)
        {
            byte[] buffer = new byte[length];
            int bytesRead = reader.Read(buffer, 0, length);
            if (bytesRead != buffer.Length)
                return null;

            return buffer;
        }

        private byte[] uncompressBytes(byte[] buffer)
        {
            MemoryStream decompressedStream = new MemoryStream();
            MemoryStream compressedStream = new MemoryStream(buffer);
            GZipStream decompressionStream = new GZipStream(compressedStream, CompressionMode.Decompress, false);
            decompressionStream.CopyTo(decompressedStream);
            decompressionStream.Flush();
            
            decompressedStream.Position = 0;
            byte[] decompressedBytes = new byte[decompressedStream.Length];
            decompressedStream.Read(decompressedBytes, 0, decompressedBytes.Length);

            decompressedStream.Close();
            
            return decompressedBytes;
        }

        private string getSizeDescription(int size)
        {
            if (size < 1024)
                return size + " bytes";

            if (size < 1024 * 1024)
                return (size / 1024).ToString() + "KB";

            return ((double)size / (1024 * 1024)).ToString("0.00") + "MB";            
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool extractAllFiles = false;

            if (lbFiles.CheckedItems.Count == 0)
            {
                DialogResult result = MessageBox.Show("No files selected." + Environment.NewLine + Environment.NewLine +
                    "Are all files to be extracted?", "EPG Collector - Debug Package Viewer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                    return;

                extractAllFiles = result == DialogResult.Yes;
            }

            FolderBrowserDialog browseFile = new FolderBrowserDialog();
            browseFile.Description = "EPG Collector - Find Output File Directory";
            browseFile.SelectedPath = RunParameters.DataDirectory;
            DialogResult browseResult = browseFile.ShowDialog();
            if (browseResult == DialogResult.Cancel)
                return;

            BinaryReader reader = new BinaryReader(File.OpenRead(currentPackageFile));
            bool eof = false;

            int filesExtracted = 0;
            int bytesExtracted = 0;

            while (!eof)
            {
                Tuple<int, bool> nameLength = getLength(reader, 1);
                if (nameLength == null)
                    eof = true;
                else
                {
                    byte[] nameBytes = getBytes(reader, nameLength.Item1);
                    if (nameBytes == null)
                        eof = true;
                    else
                    {
                        Tuple<int, bool> dataLength = getLength(reader, 4);
                        if (dataLength == null)
                            eof = true;
                        else
                        {
                            string name = Encoding.ASCII.GetString(nameBytes);

                            if (extractAllFiles || checkNameSelected(name))
                            {
                                string fullName = Path.Combine(browseFile.SelectedPath, name);
                                Logger.Instance.Write("Extracting " + fullName + " data length = " + dataLength);

                                byte[] dataBytes = new byte[dataLength.Item1];
                                int bytesRead = reader.Read(dataBytes, 0, dataLength.Item1);
                                if (bytesRead != dataLength.Item1)
                                    eof = true;

                                if (dataLength.Item2)
                                {
                                    byte[] uncompressedBytes = uncompressBytes(dataBytes);
                                    Logger.Instance.Write("Uncompressed size = " + uncompressedBytes.Length);

                                    FileStream writer = new FileInfo(fullName).OpenWrite();
                                    writer.Write(uncompressedBytes, 0, uncompressedBytes.Length);
                                    writer.Close();

                                    bytesExtracted += uncompressedBytes.Length;
                                }
                                else
                                {
                                    FileStream writer = new FileInfo(fullName).OpenWrite();
                                    writer.Write(dataBytes, 0, dataBytes.Length);
                                    writer.Close();

                                    bytesExtracted += dataBytes.Length;
                                }

                                filesExtracted++;
                            }
                            else
                                reader.BaseStream.Position += dataLength.Item1;
                        }
                    }
                }
            }

            reader.Close();

            MessageBox.Show("Files extracted = " + filesExtracted + Environment.NewLine + Environment.NewLine +
                "Bytes extracted = " + getSizeDescription(bytesExtracted), "EPG Collector - Debug Package Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool checkNameSelected(string name)
        {
            foreach (string listName in lbFiles.CheckedItems)
            {
                int index = listName.IndexOf(" size");
                if (index != -1)
                {
                    if (listName.Substring(0, index) == name)
                        return true;
                }
            }

            return false;
        }
    }
}

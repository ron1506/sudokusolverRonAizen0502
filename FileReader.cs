using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace sudokusolverRonAizen
{
    public class FileReader : IReader
    {
        private string _filePath; // the path of the file from whom we read the values of the board. 
        public string FilePath // get and set.
        {
            get { return this._filePath; }
            set { this._filePath = value; }
        }

        /// <summary>
        /// opens a window of the file explorer, the user can choose a text file to read the values from.
        /// </summary>
        /// <returns> the values of the board in a string format.</returns>
        public string Read()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) // creating new fileDialog Object.
            {
                openFileDialog.InitialDirectory = "c:\\"; //setting its initial directory, on what directory ot will open.
                openFileDialog.Filter = "txt files (*.txt)|*.txt"; // what types of files can be chosen.
                openFileDialog.Multiselect = false; // the user can't selet severl files.
                if (openFileDialog.ShowDialog() == DialogResult.OK) // if the coosing of the file went well
                {
                    //Get the path of specified file
                    _filePath = openFileDialog.FileName; // initializing the file's path.
                    if (!File.Exists(_filePath)) // if the file doesn't exist
                    {
                        throw new FileNotFoundException("file not found");
                    }
                }
                else // in case there was a problem with choosing the file.
                {
                    throw new FileChoosingException("had a problem choosing the file from the file dialog.");
                }
                return File.ReadAllText(_filePath); // return all the file's content.
            }
        }
    }
}
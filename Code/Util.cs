using System.Web;
using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using MemeBox2000.Models;
using System.Xml.Serialization;


namespace MemeBox2000
{
    public static class Util
    {
        /// <summary>
        /// Determines a name id for the meme file based on what other files ids are already there.
        /// </summary>
        /// <returns></returns>
        public static int GetNewMemeID(List<Meme> memes)
        {
            for (int i = 1; i < int.MaxValue; i++)
            {
                if (memes.Where(m => m.ID == Convert.ToString(i)).Count() == 0)
                    return i;
            }

            throw new InvalidOperationException("No Id could be determined for the meme.");
        }

        /// <summary>
        /// The current path the servers app data folder.
        /// </summary>
        public static string UploadsPath
        {
            get { return HttpContext.Current.ApplicationInstance.Server.MapPath("~/Content/uploads"); }
        }

        private static string _memeDatabasePath;
        public static string MemeDatabasePath
        {
            get
            {
                if(_memeDatabasePath == null)
                    _memeDatabasePath = Path.Combine(Util.UploadsPath, "MemeDatabase.xml");

                return _memeDatabasePath;
            }
        }

        /// <summary>
        /// Takes a filename such as photo.jpg and returns photo
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string TruncateExtension(string input)
        {
            if (input != null && input.Contains("."))
                return input.Substring(0, input.LastIndexOf('.'));
            else
                return input;
        }

        /// <summary>
        /// Takes a filename such as photo.jpg and returns jpg
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetExtension(string input)
        {
            if (input != null && input.Contains("."))
                return input.Substring(input.LastIndexOf('.')+1, input.Length - input.LastIndexOf('.')-1);
            else
                return input;
        }        

        public static List<Meme> DeserializeMemeXml()
        {
            // Construct an instance of the XmlSerializer with the type
            // of object that is being deserialized.
            XmlSerializer mySerializer = new XmlSerializer(typeof(List<Meme>));
            // To read the file, create a FileStream.
            using (FileStream myFileStream = new FileStream(MemeDatabasePath, FileMode.OpenOrCreate))
            {
                // Call the Deserialize method and cast to the object type.
                try
                {
                    return (List<Meme>)mySerializer.Deserialize(myFileStream);
                }
                catch (InvalidOperationException e)
                {
                    //couldn't deserialize due to mal formed xml
                    return new List<Meme>();
                }
            }            
        }

        public static void SerializeMemeXml(List<Meme> memes)
        {
            // Insert code to set properties and fields of the object.
            XmlSerializer mySerializer = new XmlSerializer(typeof(List<Meme>));
            // To write to a file, create a StreamWriter object.
            using (StreamWriter myWriter = new StreamWriter(MemeDatabasePath, false))
            {
                mySerializer.Serialize(myWriter, memes);
            }
        }

        public static bool IsEveryFourth(int test)
        {
            return test % 4 == 0;
        }
    }
}
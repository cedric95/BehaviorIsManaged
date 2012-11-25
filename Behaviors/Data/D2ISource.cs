﻿#region License GNU GPL
// D2ISource.cs
// 
// Copyright (C) 2012 - BehaviorIsManaged
// 
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the Free Software Foundation;
// either version 2 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
// See the GNU General Public License for more details. 
// You should have received a copy of the GNU General Public License along with this program; 
// if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BiM.Core.I18n;
using BiM.Protocol.Data;
using BiM.Protocol.Tools;
using NLog;

namespace BiM.Behaviors.Data
{
    public class D2ISource : IDataSource
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static Dictionary<string, Languages> m_langsShortcuts = new Dictionary<string, Languages>()
        {
            {"fr", Languages.French},
            {"de", Languages.German},
            {"en", Languages.English},
            {"es", Languages.Spanish},
            {"it", Languages.Italian},
            {"ja", Languages.Japanish},
            {"nl", Languages.Dutsh},
            {"pt", Languages.Portugese},
            {"ru", Languages.Russish},
        };

        public D2ISource(Languages defaultLanguage)
        {
            DefaultLanguage = defaultLanguage;
        }

        public Languages DefaultLanguage
        {
            get;
            set;
        }

        private Dictionary<Languages, D2IFile> m_readers = new Dictionary<Languages, D2IFile>(); 

        public void AddReaders(string directory)
        {
            foreach (var d2iFile in Directory.EnumerateFiles(directory).Where(entry => entry.EndsWith(".d2i")))
            {
                var reader = new D2IFile(d2iFile);

                AddReader(reader);
            }
        }

        public void AddReader(D2IFile d2iFile)
        {
            var file = Path.GetFileNameWithoutExtension(d2iFile.FilePath);

            if (!file.Contains("_"))
                throw new Exception(string.Format("Cannot found character '_' in file name {0}, cannot deduce the file lang", file));

            var lang = file.Split('_')[1];

            if (!m_langsShortcuts.ContainsKey(lang.ToLower()))
                throw new Exception(string.Format("Unknown lang symbol {0} in file {1}", lang, file));

            AddReader(d2iFile, m_langsShortcuts[lang.ToLower()]);
        }

        public void AddReader(D2IFile d2iFile, Languages language)
        {
            m_readers.Add(language, d2iFile);

            logger.Info("File added : {0}", Path.GetFileName(d2iFile.FilePath));
        }

        public IEnumerable<T> EnumerateObjects<T>(params object[] keys) where T : class
        {
            if (!DoesHandleType(typeof(T)))
                throw new ArgumentException("typeof(T)");

            var language = keys.Length == 1 ? (Languages)keys[0] : DefaultLanguage;

            return m_readers[language].GetAllText().Values as IEnumerable<T>;
        }

        public bool DoesHandleType(Type type)
        {
            return type == typeof(string);
        }

        public T ReadObject<T>(params object[] keys) where T : class
        {
            if (keys.Length < 0 || keys.Length > 2 || (!( keys[0] is IConvertible)) || (keys.Length == 2 && !(keys[1] is Languages)) )
                throw new ArgumentException("D2OSource needs a int key or a string key, use ReadObject(int) or ReadObject(string)." +
                    "You can also specify a language ReadObject(int, Languages) or ReadObject(string, Languages)");

            if (!DoesHandleType(typeof(T)))
                throw new ArgumentException("typeof(T)");

            var language = keys.Length == 2 ? (Languages)keys[1] : DefaultLanguage;

            if (keys[0] is string)
            {
                string key = keys[0] as string;

                if (!m_readers.ContainsKey(language))
                    throw new Exception(string.Format("I18nFile for {0} is not found", language));

                return m_readers[language].GetText(key) as T;
            }
            else
            {
                int key = Convert.ToInt32(keys[0]);

                if (!m_readers.ContainsKey(language))
                    throw new Exception(string.Format("I18nFile for {0} is not found", language));

                return m_readers[language].GetText(key) as T;

            }
        } 
    }
}
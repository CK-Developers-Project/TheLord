using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Developers.Table
{
    public class BaseTable
    {
        protected static string _splite_return = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        protected static string _line_splite_return = @"\r\n|\n\r|\n|\r";
        protected static char[] _trim_chars = { '\"' };

        protected Dictionary<string, List<Dictionary<string, object>>> table;

        public BaseTable(string file)
        {
            table = Read(file);
        }

        protected Dictionary<string, List<Dictionary<string, object>>> Read(string file )
        {
            // TODO : 이부분 Address로 가져오든지 그래야할듯...
            TextAsset data = Resources.Load<TextAsset> ( file );

            var lines = Regex.Split ( data.text, _line_splite_return );
            string sheetName = "";
            string[] header = null;
            string[] types = null;

            try
            {
                for ( int i = 0; i < lines.Length; ++i )
                {
                    if ( lines[i].Length > 2 && lines[i].Substring ( 0, 2 ).Equals ( "//" ) )
                    {
                        sheetName = lines[i].Substring ( 2 );
                        header = Regex.Split ( lines[i + 1], _splite_return );
                        types = Regex.Split ( lines[i + 2], _splite_return );
                        i += 2;
                        table.Add ( sheetName, new List<Dictionary<string, object>> ( ) );
                        continue;
                    }

                    var values = Regex.Split ( lines[i], _splite_return );
                    if ( values.Length == 0 || values[0] == "" )
                    {
                        continue;
                    }

                    var entry = new Dictionary<string, object> ( );
                    for ( int j = 0; j < header.Length; ++j )
                    {
                        string value = values[j];

                        if ( types[j].Contains ( "List" ) )
                        {
                            List<string> list = value.Split ( '#' ).ToList ( );
                            for ( int a = 0; a < list.Count; ++a )
                            {
                                string temp = list[a];
                                temp = temp.TrimStart ( _trim_chars ).TrimEnd ( _trim_chars ).Replace ( "\\", "" );
                                list[a] = temp;
                            }
                            entry.Add ( header[j], list );
                        }
                        else
                        {
                            value = value.TrimStart ( _trim_chars ).TrimEnd ( _trim_chars ).Replace ( "\\", "" );
                            value = value.Replace ( "<br>", "\n" );
                            value = value.Replace ( "<c>", "," );

                            if ( types[j].Contains ( "int32" ) )
                            {
                                entry.Add ( header[j], Int32.Parse ( value ) );
                            }
                            else if ( types[j].Contains ( "float" ) )
                            {
                                entry.Add ( header[j], float.Parse ( value ) );
                            }
                            else
                            {
                                entry.Add ( header[j], value );
                            }
                        }
                    }
                    table[sheetName].Add ( entry );
                }
            }
            catch ( System.Exception e )
            {
                Debug.LogError ( e.Message );
            }
        }
    }
}
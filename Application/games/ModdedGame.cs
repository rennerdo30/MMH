using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Application.games
{
    internal abstract class ModdedGame
    {
        private readonly string installDir;
        public string InstallDir { get { return installDir; } }

        protected string infoPrefix = "";
        protected readonly string modDBFilename = "_ModInfo.csv";



        public ModdedGame(string installDir)
        {
            this.installDir = installDir;

            findMissingMods();
        }

        public abstract List<GameMod> findMissingMods();

        public String getURLForMod(GameMod mod)
        {
            using (TextFieldParser parser = new TextFieldParser($"MOD_DB/{infoPrefix}{modDBFilename}"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters("=");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    if (fields != null && fields.Length == 2 && fields[0].Trim() == mod.Name.Trim())
                    {
                        return fields[1];
                    }
                }
            }

            return "https://www.google.com/search?q=" + HttpUtility.UrlEncode($"\"{mod.Name}\"");
        }
    }
}
